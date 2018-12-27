# Umbraco-hide-properties

[![Build status](https://ci.appveyor.com/api/projects/status/be9b1pdyn54rctto?svg=true)](https://ci.appveyor.com/project/JanvanHelvoort/umbraco-hide-properties)
[![NuGet release](https://img.shields.io/nuget/v/Our.Umbraco.HideProperties.svg)](https://www.nuget.org/packages/Our.Umbraco.HideProperties/)
[![Our Umbraco project page](https://img.shields.io/badge/our-umbraco-orange.svg)](https://our.umbraco.com/packages/backoffice-extensions/hide-properties//) 

> *Pre-release:* [![MyGet Pre Release](https://img.shields.io/myget/janvanhelvoort/vpre/Our.Umbraco.HideProperties.svg)](https://www.myget.org/feed/janvanhelvoort/package/nuget/Our.Umbraco.HideProperties)

This package makes it possible to create rules inside umbraco to hide tabs or properties for specific user groups.

## Getting Started

This package includes dutch and english translations. A translation can easily be added with xml files. [Languages](Source/Our.Umbraco.HideProperties/Client/lang/)

## Documentation ##

The documentation for this package can be found [here](Documentation/README.md)

### Installation

> *Note:* This package has been developed for **Umbraco v7.9.0** and will support that version and above.

#### NuGet package repository
To [install from NuGet](https://www.nuget.org/packages/Our.Umbraco.HideProperties), you can run the following command from within Visual Studio:

	PM> Install-Package Our.Umbraco.HideProperties

There is also a [MyGet build](https://www.myget.org/feed/janvanhelvoort/package/nuget/Our.Umbraco.HideProperties) - for development branch releases.

#### After installation 
of this package you will get a `hide properties` dashboard in the settings section of Umbraco. You can create and manage rules to hide tabs or properties for specific user groups.
![Dashboard](Documentation/Screenshots/Section%20Dashboard.png)

![Rule Editor](Documentation/Screenshots/Rule%20Editor.png)

## License
Licensed under the [MIT License](LICENSE.md)
