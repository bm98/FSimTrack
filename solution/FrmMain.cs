using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SC = SimConnectClient;

namespace FSimTrack
{
  public partial class FrmMain : Form
  {

    private bool m_initDone = false;
    private WebRequestor m_tracker = null;
    private long m_pings = 0;

    private string m_lastReqError = "";

    /// <summary>
    /// Checks if a rectangle is visible on any screen
    /// </summary>
    /// <param name="formRect"></param>
    /// <returns>True if visible</returns>
    private static bool IsOnScreen( Rectangle formRect )
    {
      Screen[] screens = Screen.AllScreens;
      foreach ( Screen screen in screens ) {
        if ( screen.WorkingArea.Contains( formRect ) ) {
          return true;
        }
      }
      return false;
    }

    public FrmMain( )
    {
      InitializeComponent( );

      // connect the SimConnect facility to receive updates
      SC.SimConnectClient.Instance.DataArrived += Instance_DataArrived;

      WebRequestorStatus.Instance.WCliPingEvent += Instance_WCliPingEvent;
      WebRequestorStatus.Instance.WCliDebugEvent += Instance_WCliDebugEvent;

      m_tracker = new WebRequestor( );
    }

    private void FrmMain_Load( object sender, EventArgs e )
    {
      AppSettings.Instance.Reload( );
      // Assign Size property - check if on screen, else use defaults
      if ( IsOnScreen( new Rectangle( AppSettings.Instance.FormLocation, this.Size ) ) ) {
        this.Location = AppSettings.Instance.FormLocation;
      }

      string version = Application.ProductVersion;  // get the version information
      // BETA VERSION; TODO -  comment out if not longer
      //lblTitle.Text += " - V " + version.Substring( 0, version.IndexOf( ".", version.IndexOf( "." ) + 1 ) ); // PRODUCTION
      lblVersion.Text = "Version: " + version + " beta"; // BETA

#if DEBUG
      button1.Visible = true;
#endif

      txRemIP.Text = AppSettings.Instance.RemoteIP.Trim( );
      txRemPort.Text = AppSettings.Instance.RemotePort.Trim( );

      lblWCliStatusTxt.Text = "idle";
      timer1.Interval = 100; // 100ms
      timer1.Enabled = true;
    }

    private void FrmMain_FormClosing( object sender, FormClosingEventArgs e )
    {
      if ( SC.SimConnectClient.Instance.IsConnected ) {
        SC.SimConnectClient.Instance.Disconnect( );
      }

      if ( m_tracker.WebClientRunning ) {
        m_tracker.StopService( );
      }
      m_tracker.Dispose( );

      // don't record minimized, maximized forms
      if ( this.WindowState == FormWindowState.Normal ) {
        AppSettings.Instance.FormLocation = this.Location;
      }
      AppSettings.Instance.RemoteIP = txRemIP.Text.Trim( );
      AppSettings.Instance.RemotePort = txRemPort.Text.Trim( );
      AppSettings.Instance.Save( );
    }


    private void btConnect_PushbuttonPressed( object sender, EventArgs e )
    {
      if ( SC.SimConnectClient.Instance.IsConnected ) {
        // Disconnect from Input and SimConnect
        m_initDone = false;
        SC.SimConnectClient.Instance.Disconnect( );
        m_tracker.StopService( );

        // GUI stuff
        lblWCliStatusTxt.Text = "Disconnected";
        lblAcft.Text = "...";
        btConnect.ButtonText = "Connect";
        btConnect.OnState = false;
      }
      else {
        // try to connect
        if ( SC.SimConnectClient.Instance.Connect( ) ) {
          lblAcft.Text = SC.SimConnectClient.Instance.AircraftModule.AcftID; // activate the aircraft module

          string url = $"http://{txRemIP.Text.Trim()}:{txRemPort.Text.Trim()}/";
          m_tracker.StartService( url );
          m_pings = 0;
          // GUI stuff
          lblWCliStatusTxt.Text = "FS2020 Connected";
          RTB.Text = "";
          btConnect.ButtonText = "Disconnect";
          btConnect.OnState = true;
          m_initDone = true;
        }
        else {
          // GUI stuff
          lblWCliStatusTxt.Text = "Connecting FS2020 failed";
          RTB.Text = SC.SimConnectClient.Instance.ErrorList.FirstOrDefault( );
        }
      }
    }


    #region Callback Handlers


    private void TestUpload( )
    {
      if ( !m_tracker.WebClientRunning ) {
        string url = $"http://{txRemIP.Text.Trim()}:{txRemPort.Text.Trim()}/";
        m_tracker.StartService( url );
      }
      m_tracker?.Upload( new WebRequestor.TrackData( ) { lat = -6, lon = 148, alt_msl = 5000, thdg = 270, gs_kt = 120, vs_fpm = -200 } );

    }

    private void Instance_DataArrived( object sender, FSimClientIF.ClientDataArrivedEventArgs e )
    {
      if ( m_tracker == null ) return;
      if ( !m_tracker.WebClientRunning ) return;
      // we should have a valid Tracker Service now
      if ( e.DataRefName == "Aircraft@Prefab" ) {
        // Data from Aircraft Module arrived
        m_tracker?.Upload(
          new WebRequestor.TrackData( ) {
            lat = SC.SimConnectClient.Instance.AircraftModule.Lat,
            lon = SC.SimConnectClient.Instance.AircraftModule.Lon,
            alt_msl = SC.SimConnectClient.Instance.AircraftModule.AltMsl_ft,
            thdg = SC.SimConnectClient.Instance.AircraftModule.HDG_true_deg,
            gs_kt = SC.SimConnectClient.Instance.AircraftModule.Groundspeed_kt,
            vs_fpm = SC.SimConnectClient.Instance.AircraftModule.VS_ftPmin
          } );
      }
    }

    private void Instance_WCliDebugEvent( object sender, WebRequestorStatus.WCliDebugEventArgs e )
    {
      // could handle
      m_lastReqError = e.Text;
    }

    private void Instance_WCliPingEvent( object sender )
    {
      m_pings++;
    }

    private int m_timer10sec = 100;
    /// <summary>
    /// Handle async WebRequestor stuff
    /// </summary>
    private void timer1_Tick( object sender, EventArgs e )
    {
      if ( !string.IsNullOrEmpty( m_lastReqError ) ) {
        RTB.Text += $"{m_lastReqError}\n";
        m_lastReqError = "";
      }
      lblPing.Text = m_pings.ToString( );

      // poll the acft ID but only at 10 sec intervals, may have changed after connecting..
      m_timer10sec--;
      if ( m_timer10sec<0 && SC.SimConnectClient.Instance.IsConnected ) {
        lblAcft.Text = SC.SimConnectClient.Instance.AircraftModule.AcftID; // activate the aircraft module
        m_timer10sec = 100; // reset
      }
    }

    #endregion

    private void button1_Click( object sender, EventArgs e )
    {
      TestUpload( );
    }

  }
}
