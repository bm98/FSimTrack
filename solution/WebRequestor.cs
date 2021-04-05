using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSimTrack
{
  /// <summary>
  /// Implements a thread pool to issue GET requests
  /// for a WebService 
  /// Route is /api/track/LAT,LON,ALT,HDG
  /// </summary>
  class WebRequestor : IDisposable
  {
    /// <summary>
    /// A struct containing the track request data
    /// </summary>
    public struct TrackData
    {
      public double lat;
      public double lon;
      public double alt_msl;
      public double thdg;
      public double gs_kt;
      public double vs_fpm;
    }


    // Note: this is const and expected to be setup in the WebServer...
    private HttpClient  m_client = null;
    private string m_url = "";
    private string m_reply = "";

    private ClientConnectionPool m_connectionPool;
    private const int c_connectionPoolMax = 3;     // Maximum queued uploads
    private Thread m_clientTask = null;
    private bool m_continueProcess = false;

    public bool WebClientRunning { get; private set; } = false;


    /// <summary>
    /// cTor: submit the webserver as host:port (localhost:8080  or 192.168.1.10:9000 ..)
    /// </summary>
    /// <param name="host_port">Host and port to use in http request</param>
    public WebRequestor( )
    {
      m_client = new HttpClient( );
      m_connectionPool = new ClientConnectionPool( );
    }

    /// <summary>
    /// Contains the reply from the upload request
    /// </summary>
    public string Reply { get => m_reply; }

    public void Dispose( )
    {
      if ( m_continueProcess ) {
        this.StopService( );
      }

      ( (IDisposable)m_client )?.Dispose( );
    }



    /// <summary>
    /// Start the service
    /// providing and url such as: http://IP:PORT/
    /// </summary>
    /// <param name="url">An URL to connect to</param>
    public void StartService( string url )
    {
      m_url = url;
      if ( !m_url.EndsWith( "/" ) ) m_url += "/";

      m_clientTask = new Thread( new ThreadStart( this.Process ) );
      m_continueProcess = true;
      m_clientTask.Start( );
      if ( m_clientTask.IsAlive ) {
        WebClientRunning = true;
        WebRequestorStatus.Instance.SetSvrStatus( WebRequestorStatus.WCliStatus.Running );
        Console.WriteLine( "WebRequestor: running" );
      }
    }

    public void StopService( )
    {
      this.Stop( );
    }

    /// <summary>
    /// Shut the ClientService
    /// </summary>
    public void Stop( )
    {
      WebClientRunning = false;
      WebRequestorStatus.Instance.SetSvrStatus( WebRequestorStatus.WCliStatus.Shutdown );
      Console.WriteLine( "WebRequestor: shutdown" );
      m_continueProcess = false; // m_clientTask should die now

      // Close all pending client connections in the pool
      while ( m_connectionPool.Count > 0 ) {
        UploadService client = m_connectionPool.Dequeue( );
      }
      WebRequestorStatus.Instance.SetSvrStatus( WebRequestorStatus.WCliStatus.Idle );
      Console.WriteLine( "WebRequestor: idle" );
    }

    /// <summary>
    /// Request Tracking
    /// Note: this is a async call and will execute through a queue
    /// </summary>
    /// <returns>True if successfully dispatched, else false</returns>
    public bool Upload( TrackData data )
    {
      m_reply = "Service already shutdown or not running ??!!";
      if ( !m_continueProcess ) return false;
      // some sanity..
      m_reply = "Client not created or vanished ??!!";
      if ( m_client == null ) return false;
      m_reply = "Too many pending requests - upload canceled - try later";
      if ( m_connectionPool.Count >= c_connectionPoolMax ) return false;
      m_reply = "";

      // create a service client that will do the upload
      var client = new UploadService( m_client, m_url, data  );
      m_connectionPool.Enqueue( client );
      return true;
    }

    /// <summary>
    /// The client thread environment
    /// Get a client connection from the pool and handles it
    /// </summary>
    private void Process( )
    {
      Console.WriteLine( "WebRequestor: servicing" );
      while ( m_continueProcess ) {
        // get a Job and do the upload
        UploadService client = null;
        lock ( m_connectionPool.SyncRoot ) {
          if ( m_connectionPool.Count > 0 ) client = m_connectionPool.Dequeue( );
        }
        if ( client != null ) {
          client.ProcessData( ); // Provoke client
        }

        Thread.Sleep( 100 ); // dispatches clients every 100ms
      }
    }

  }


  #region Class UploadService

  /// <summary>
  /// Creates a number of services that will handle the clients waiting in the queue
  /// </summary>
  class UploadService
  {
    private const string m_uploadString = @"api/track/"; // this completes the URI route

    private HttpClient  m_clientRef = null;
    private string m_uri = "";
    private string m_reply = "";

    /// <summary>
    /// Contains the reply from the upload request
    /// </summary>
    public string Reply { get => m_reply; }
    /// <summary>
    /// True if an error is detected
    /// </summary>
    public bool Error { get; private set; } = false;


    /// <summary>
    /// cTor: for a GET request
    /// </summary>
    /// <param name="webClient">A valid client connection</param>
    /// <param name="url">The URL to upload to</param>
    /// <param name="trackData">The Data to send</param>
    public UploadService( HttpClient webClient, string url, WebRequestor.TrackData trackData )
    {
      m_clientRef = webClient;
      // create the request here: http://IP:PORT/api/track/LAT,LON,ALT,HDG,GS,VS
      // Formatting should be dot as decimal point (locale set in Program.cs for the application)
      m_uri = url + m_uploadString
        + $"{trackData.lat:#0.0000000},{trackData.lon:#0.0000000},{trackData.alt_msl:#0},{trackData.thdg:#0},{trackData.gs_kt:#0},{trackData.vs_fpm:#0}";
    }

    /// <summary>
    /// Issue the HTTP Request
    /// returns if a reply is received
    /// </summary>
    async public void ProcessData( )
    {
      try {
        m_reply = await m_clientRef.GetStringAsync( m_uri );
        //WebUploaderStatus.Instance.SetPing( );
        if ( m_reply.ToLowerInvariant( ).StartsWith( "error" ) ) {
          WebRequestorStatus.Instance.Debug( m_reply );
        }
        WebRequestorStatus.Instance.SetPing();
        return;
      }
      catch ( HttpRequestException e ) {
        m_reply = e.Message;
        if ( e.HResult == -2146233088 /*0x80131500*/ ) {
          m_reply = "Server not available: " + e.Message;
        }
        WebRequestorStatus.Instance.Debug( m_reply );
        return;
      }
      catch ( Exception e ) {
        WebRequestorStatus.Instance.Debug( m_reply );
        m_reply = e.Message;
      }
    }
  }
  #endregion


  #region Class ClientConnectionPool

  class ClientConnectionPool
  {
    // Creates a synchronized wrapper around the Queue.
    private Queue SyncdQ = Queue.Synchronized( new Queue( ) );

    public void Enqueue( UploadService client )
    {
      SyncdQ.Enqueue( client );
    }

    public UploadService Dequeue( )
    {
      return (UploadService)( SyncdQ.Dequeue( ) );
    }

    public int Count {
      get { return SyncdQ.Count; }
    }

    public object SyncRoot {
      get { return SyncdQ.SyncRoot; }
    }

  } // class ClientConnectionPool

  #endregion

}
