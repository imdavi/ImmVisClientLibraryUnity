# ImmVis Unity Client library

This project 

## Supported deployment platforms

This library can be used to target games/applications for the following platforms:
- Windows
- Linux
- MacOS
- Android (ARM and x86)

If you need support for iOS, please consider downloading the [Grpc.Core Package 2.26.0](https://www.nuget.org/packages/Grpc.Core/2.26.0) and copy the iOS binaries to the `Assets\ImmVisClientGrpcUnity/Plugins/Grpc.Core/native/ios` folder


## Development Setup

Recommended tools for development:
- [Unity 2019.4.8f1](https://unity3d.com/pt/get-unity/download) (We recommend the usage of Unity Hub to ease the install of different Unity versions)
- [Visual Studio Code](https://code.visualstudio.com/)
- To use Visual Studio Code with Unity, please follow [this instructions](https://code.visualstudio.com/docs/other/unity)


## Adding this library to an existing project

There are two ways of adding this library to your Unity project:

- Download the latest version of `ImmVisClientGrpcUnity.unitypackage` available on the [Releases page](https://github.com/imdavi/immvis-client-grpc-unity/releases) and [importing the asset into your project](https://docs.unity3d.com/Manual/AssetPackagesImport.html)
- Download or clone this repository and copy the `Assets/ImmVisClientGrpcUnity` folder to the `Assets` folder of your project

## Using the library

Unfortunately we still don't provide a complete documentation about using the library yet. To partially remediate that, we included two samples ([Basic Usage](https://github.com/imdavi/immvis-client-grpc-unity/tree/master/Assets/ImmVisClientGrpcUnity/Examples/BasicUsage) and [Scatterplot](https://github.com/imdavi/immvis-client-grpc-unity/tree/master/Assets/ImmVisClientGrpcUnity/Examples/Scatterplot)), where you can find how the current features of ImmVis can be implemented.

## Adding new features

Please refer to the documentation available on [immvis-server-grpc](https://github.com/imdavi/immvis-server-grpc) page in order to add new functionalities to this project.

## Contributing

Our model of contribution is similar to the one described on https://github.com/firstcontributions/first-contributions, with the difference that members of IMDAVI don't need to create a fork from this repository, only create a branch here to submit their pull-requests.