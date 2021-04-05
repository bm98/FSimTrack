# FSimTrack V 0.9.0.2

### Tracking App complementing the Mapping WebServer found on Docker bm98ch/fsimpngtiles   

https://hub.docker.com/r/bm98ch/fsimpngtiles   
https://github.com/bm98/FSimPngTiles   


* Provides a connection to MSFS2020 - retrieves tracking data
* Provides a WebRequest Client to submit tracking queries

-----

# Flight Sim Tracker (.Net 4.7.2)

## FlightSim Live Data Source
Connects to the FlightSim, polls essential aircraft tracking data 
NOTE: Right now only an interface with MSFS2020 is implemented, an XPlane 11 interface is still in development.

## WebRequest Client
Interfaces the mapping webserver found as Docker image  bm98ch/fsimpngtiles   
Issues tracking get requests to the web server with LAT,LON,ALT,THDG,GS,VS

-----

# Usage 

Prerequisite - have the Mapping Server running (Docker image bm98ch/fsimpngtiles)   
see also:  https://github.com/bm98/FSimPngTiles/blob/main/HowTo-V0.9.pdf   

* Deploy the release zip content in a folder (no installer provided or needed)
* Start FSimTrack.exe
* Enter the Mapping Server IP and port

* Start MSFS2020 and connect when the flight is about to start  

If connected the button will turn green and the Status changes.   
Else the issue is shown below the button (usually when connecting to early)   

When connected, FSimTrack will repeatedly issue requests to the WebServer in order to track the aircraft.   

My FlightSim Libraries (included in the release package)
* SimConnectClient.dll        -- FlightSim interface to MSFS2020 SimConnect
* FSimClientIF.dll            -- Generic FSim Client interface definition
* FSimIF.dll                  -- Generic FSim interface definition
* bm98_Switches.dll           -- A GUI controls library

From MSFS2020 Developer Kit:
* SimConnect.cfg
* Microsoft.FlightSimulator.SimConnect.dll 
* SimConnect.dll
  
-----

### General note for builders
The Project files expect referenced Libraries which have no NuGet package reference in a Solution directory  "Redist"  
To integrate with MSFS2020 SimConnect the solution must be built as x64 binary!   

So far the sources from the used Libraries are not on GitHub (yet)
  