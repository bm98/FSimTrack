using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSimTrack
{
  class AppSettings : ApplicationSettingsBase
  {

    // Singleton
    private static readonly Lazy<AppSettings> m_lazy = new Lazy<AppSettings>( () => new AppSettings( ) );
    public static AppSettings Instance { get => m_lazy.Value; }

    private AppSettings( )
    {
      if ( this.FirstRun ) {
        // migrate the settings to the new version if the app runs the first time
        try {
          this.Upgrade( );
        }
        catch { }
        this.FirstRun = false;
        this.Save( );
      }
    }

    #region Setting Properties

    // manages Upgrade
    [UserScopedSetting( )]
    [DefaultSettingValue( "True" )]
    public bool FirstRun {
      get { return (bool)this["FirstRun"]; }
      set { this["FirstRun"] = value; }
    }


    // Control bound settings
    [UserScopedSetting( )]
    [DefaultSettingValue( "10, 10" )]
    public Point FormLocation {
      get { return (Point)this["FormLocation"]; }
      set { this["FormLocation"] = value; }
    }

    // User Config Settings

    [UserScopedSetting( )]
    [DefaultSettingValue( "False" )]
    public bool Logging {
      get { return (bool)this["Logging"]; }
      set { this["Logging"] = value; }
    }

    [UserScopedSetting( )]
    [DefaultSettingValue( "127.0.0.1" )]
    public string RemoteIP {
      get { return (string)this["RemoteIP"]; }
      set { this["RemoteIP"] = value; }
    }

    [UserScopedSetting( )]
    [DefaultSettingValue( "8080" )]
    public string RemotePort {
      get { return (string)this["RemotePort"]; }
      set { this["RemotePort"] = value; }
    }

    #endregion


  }
}