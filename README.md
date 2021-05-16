This project is an example of a client library implementation for the [ImmVis Framework](https://github.com/imdavi/ImmVisServerPython).

## Samples

<img src="Imgs/DataAnalysisScreenshot.png" alt="drawing" width="480"/>

We included two samples inside this client library:

* **Basic Usage**: simple Unity scene that shows how to use the `ImmVisGrpcClientManager` prefab.
* **DataAnalysis**: an advanced sample that shows a VR application that uses ImmVis to create an interactive scatterplot

Please refer to the tutorials below to run the samples. 

## Tutorials

* [Running ImmVisClientLibraryUnity Samples](Docs/tutorial_running_samples.md)
* [Integrating ImmVisClientLibraryUnity to a Unity project](Docs/tutorial_integrating_library.md)

## Supported Platforms

Developers can use this library to create games/applications for the following platforms:
- Windows
- Linux
- macOS
- Android (ARM and x86)

If you need support for iOS, please consider downloading the [Grpc.Core Package 2.26.0](https://www.nuget.org/packages/Grpc.Core/2.26.0) and copy the iOS binaries to the `Assets/ImmVisClientLibraryUnity/Plugins/Grpc.Core/native/ios` folder.

## Development Setup

Recommended tools for development:
- Operating system: Windows, Linux, or macOS
- [Unity](https://unity.com/) `2020.3.8f1`. We recommend the usage of [Unity Hub](https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html) to perform the installation.
- **Optional but recommended**: [Visual Studio Code](https://code.visualstudio.com/). Follow [these instructions](https://code.visualstudio.com/docs/other/unity) to make Unity work with Visual Studio Code. Some operating systems might have problems with Intellisense (code auto-completion) and if that is your case, consider installing these softwares: 
    - [.NET Framework 4.7.1](https://dotnet.microsoft.com/download/dotnet-framework/net471) (Windows)
    - [.NET 5.0](https://dotnet.microsoft.com/download) (Linux, Mac OS)
    - [Mono](https://www.mono-project.com/download/stable/#download-lin) (Linux).

## Contributing

Our contribution model is similar to the one described on https://github.com/firstcontributions/first-contributions, with the difference that members of IMDAVI don't need to create a fork from this repository, only create a branch here to submit their pull-requests.
