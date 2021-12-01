![logo_name](https://user-images.githubusercontent.com/12818337/144273485-86bc6a9f-237b-4c03-8562-e99889c1a38a.PNG)
# Sonar Sound Studio (Prototype)

## Purpose
We created this prototype for the "Devpost: Build The World with Dolby.io" Hackathon in November 2021.

## Motivation
The podcast industry is a multi-billion dollar market that has experienced tremendous growth over the past decade. In the United States alone, there are an estimated 120 million podcast listeners in 2021 and is forecasted to continue growing year over year. This phenomenon speaks to the importance of podcasting in American media culture.

Podcast content is incredibly diverse. This “playground” of audio content paired with the capabilities provided by the industry leader in audio technology and experiences inspired the Sonar Sound Studio.

With the emergence of the metaverse, the nature of how people engage with one another is being reshaped. We wanted to bring the communal and informative characteristics of podcasting to the future of the web in a truly immersive and innovative way.

Future work on Sonar Sound Studio will extend offerings to other audio-related content creation.

## Architecture
These are the core components of the Sonar Sound Studio prototype.

### Core Technologies
- Unity 2019.4.31f
- Mozilla WebXR Unity Asset
- Dolby.io Communication API

### Dolby.io API Usage
- Communications API
- Javascript SDK
- ConferenceService

### Unity WebXR Integration
- The Dolby.io JavascriptSDK was integrated with our Unity project via .jslib files located in Assets > Plugins
- The html template used is located in Asstes > WebGLTemplates > WebXR

## How it Works
Sonar Sound Studio is a WebXR application that is used with a WebXR-compatible web browser and Virtual Reality (VR) headset. 

### Project Workflow
1) This prototype is a single scene application, everything is ran from the "Prodcast_WebXR" Unity scene.
2) Visiting the url in the web browser will load the application and put the user in the studio environment once it finishes loading.
3) The user will need to provide a name to the visible input box "Your Name".
4) Press the "Login" button. The Login button calls the [DolbyManager.Initialize()](https://github.com/ccpddp21/dolby_webxr/blob/a30cc1e8b8feeccbb3100d51c0560096304dd1f1/Dolby%20WebXR/Assets/_Scripts/Dolby/DolbyManager.cs#L38) function and passes the entered name value to the [dolby.jslib](https://github.com/ccpddp21/dolby_webxr/blob/main/Dolby%20WebXR/Assets/Plugins/dolby.jslib) file.
--- The [dolby.jslib](https://github.com/ccpddp21/dolby_webxr/blob/main/Dolby%20WebXR/Assets/Plugins/dolby.jslib) file is responsible for communicating outside of the built Unity WebGL instance to external custom and native javascript web browser functions. This is how the user's name is passed to the [VoxeetSDK.initialize()](https://github.com/ccpddp21/dolby_webxr/blob/main/Dolby%20WebXR/Assets/WebGLTemplates/WebXR/client.js) function. A native alert() is fired to singal successful initialization.
5) On success, new options will appear on the window for entering a Room Name, joining an existing conference, or hosting a new conference. --- Joining and Hosting use the Room Name as the "conferenceAlias" and supplies the value (using the same dolby.jslib communication method) to the [VoxeetSDK.conference.create()](https://github.com/ccpddp21/dolby_webxr/blob/main/Dolby%20WebXR/Assets/WebGLTemplates/WebXR/ui.js) function. A native alert() is fired to singal successful joining or creating. --- We had to use VoxeetSDK.conference.create() for both joining and creating because we could not store the necessary "conferenceId" value from the VoxeetSDK.conference.create() payload to then be used as the needed parameter for the VoxeetSDK.conference.fetch() function.
6) When successfully entered into the conference room the option to Leave becomes available. If pressed, a native alert() is fired to singal successful leaving for the room.

### How to Run Locally
#### Via Unity Editor
*** Note: The WebGL module will need to be installed on you Unity install
1) Open Unity Hub
2) [Add] the Unity project "Unity WebXR"
3) Say "NO" to the new Input System window
4) Open the "Podcast_WebXR" Unity scene in Assets > _Scenes
5) Navigate to File > Build Settings
6) Make sure the target platform is WebGL
7) Click [Build and Run]
8) Choose you file destination and press [OK] (Unity's build time for WebGL is a little long)
9) When completed, Unity will create a localhost server and open the project in a web browser window
  
#### Via Local Development Server
*** Note: You will need a development server like NodeJS
1) Create a folder named 'public' in your dev server's project structure

*** Note: The WebGL module will need to be installed on you Unity install
1) Open Unity Hub
2) [Add] the Unity project "Unity WebXR"
3) Say "NO" to the new Input System window
4) Open the "Podcast_WebXR" Unity scene in Assets > _Scenes
5) Navigate to File > Build Settings
6) Make sure the target platform is WebGL
7) Click [Build]
8) Choose the 'public' folder of your dev server
9) When complete, start your dev server and open your browser to served url
  
#### AWS Hosted
  - Sonar Sound Studio is temporarily hosted on AWS at this url: https://d3pedbln4nezy9.cloudfront.net/

## Known Issues
  - Dynamic furniture enabling based on Dolby conference state [host / listener / none] is broken in WebGL build
