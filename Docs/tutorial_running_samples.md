# Tutorial:  Running ImmVisClientLibraryUnity Samples

This tutorial covers how to set up and run the ImmVis Unity client library samples.

## Requirements

- Operating system: Windows / Linux / Mac OS
- [Unity](https://unity.com/) `2020.3.8f1`. We recommend the usage of [Unity Hub](https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html) to perform the installation.
- **Optional but recommended**: [Visual Studio Code](https://code.visualstudio.com/). Follow [these instructions](https://code.visualstudio.com/docs/other/unity) to make Unity work with Visual Studio Code. Some operating systems might have problems with Intellisense (code auto-completion) and if that is your case, consider installing these softwares: 
    - [.NET Framework 4.7.1](https://dotnet.microsoft.com/download/dotnet-framework/net471) (Windows)
    - [.NET 5.0](https://dotnet.microsoft.com/download) (Linux, Mac OS)
    - [Mono](https://www.mono-project.com/download/stable/#download-lin) (Linux).

## Running ImmVisClientLibraryUnity Samples

1. Follow the [setup steps from ImmVisServerPython](https://github.com/imdavi/ImmVisServerPython/blob/main/docs/tutorial_setup.md) to setup and run the server project.
1. Clone [ImmVisClientLibraryUnity](https://github.com/imdavi/ImmVisClientLibraryUnity) or download its source code
1. Open `ImmVisClientLibraryUnity` project using Unity or Unity Hub
1. If Unity shows errors when opening the project, please consider using the [Package Manager](https://docs.unity3d.com/Manual/upm-ui.html) to install the `XR Interaction Toolkit`. Please note that you might need to select to search inside the `Unity Registry` for the packages. If `XR Interaction Toolkit` is not appearing on your Package Manager, consider enabling `Enable Preview Packages` inside the package manager advanced settings.
1. On Unity `Project` tab, navigate to folder `Assets/ImmVisClientLibraryUnity/Examples` to find the samples included with this client library:
    * BasicUsage: simple Unity scene that show how to use the `ImmVisGrpcClientManager` prefab
    * DataAnalysis: an advanced sample that shows an VR application that uses ImmVis to create an interactive scatterplot
1. Open the main scene inside the selected sample folder (`ExampleScene.unity` for BasicUsage or `DataAnalysis.unity` for DataAnalysis). If you are prompted with the TextMesh Pro dialog, click on `Import TMP Essentials`.
1. Run the `ImmVisServerPython` as described [here](https://github.com/imdavi/ImmVisServerPython/blob/main/docs/tutorial_setup.md#running-immvis-server)
1. Go back to Unity and press the "play" button to run the sample.
