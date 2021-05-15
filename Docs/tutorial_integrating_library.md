# Tutorial:  Integrating ImmVisClientLibraryUnity to a Unity project
This tutorial covers covers how to integrate the `ImmVisClientLibraryUnity` library to a Unity project.

## Requirements

- Operating system: Windows / Linux / Mac OS
- [Unity](https://unity.com/) `2020.3.8f1`. We recommend the usage of [Unity Hub](https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html) to perform the installation.
- **Optional but recommended**: [Visual Studio Code](https://code.visualstudio.com/). Follow [these instructions](https://code.visualstudio.com/docs/other/unity) to make Unity work with Visual Studio Code. Some operating systems might have problems with Intellisense (code auto-completion) and if that is your case, consider installing these softwares: 
    - [.NET Framework 4.7.1](https://dotnet.microsoft.com/download/dotnet-framework/net471) (Windows)
    - [.NET 5.0](https://dotnet.microsoft.com/download) (Linux, Mac OS)
    - [Mono](https://www.mono-project.com/download/stable/#download-lin) (Linux).

## Steps

1. Follow the [setup steps from ImmVisServerPython](https://github.com/imdavi/ImmVisServerPython/blob/main/docs/tutorial_setup.md) to setup and run the server project.
1. Open your project and use one of the following alternatives to import the `ImmVisClientLibraryUnity`:
    - Download the latest version of `ImmVisClientLibraryUnity.unitypackage` available on the [Releases page](https://github.com/imdavi/immvis-client-grpc-unity/releases) and [importing the asset into your project](https://docs.unity3d.com/Manual/AssetPackagesImport.html)
    - Download or clone this repository and copy the `Assets/ImmVisClientLibraryUnity` folder to the `Assets` folder of your project
1. Use the [Package Manager](https://docs.unity3d.com/Manual/upm-ui.html) to install the `XR Interaction Toolkit` package to your project. Please note that you might need to select to search inside the `Unity Registry` for these packages. If `XR Interaction Toolkit` is not appearing on your Package Manager, consider enabling `Enable Preview Packages` inside the package manager advanced settings.
1. Follow this steps to check if everything worked fine:
    - Run the `ImmVisServerPython` as described [here](https://github.com/imdavi/ImmVisServerPython/blob/main/docs/tutorial_setup.md#running-immvis-server)
    - On Unity Project tab, bavigate to the Basic Usage sample folder (`Assets/ImmVisClientGrpcUnity/Examples/BasicUsage`)
    - Open the main scene of the sample (`ExampleScene.unity`). If you are prompted with the TextMesh Pro dialog, click on `Import TMP Essentials`.
    - Press play to run the sample and check if any messages appeared on console.
1. You are now ready to use the ImmVis Framework inside your Unity project, including the `ImmVisGrpcClientManager` prefab (`Assets/ImmVisClientGrpcUnity/Prefabs/ImmVisGrpcClientManager.unity`). Check out the samples inside `Assets/ImmVisClientGrpcUnity/Examples` to understand how to use the framework.
