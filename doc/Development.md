# Development and Testing Guide <br/> Quotes Microservice Client SDK for .NET

This document provides high-level instructions on how to build and test the client SDK.

* [Environment Setup](#setup)
* [Installing](#install)
* [Building](#build)
* [Testing](#test)
* [Contributing](#contrib) 

## <a name="setup"></a> Environment Setup

This is a .NET project and you have to install Visual Studio tools. 
You can download free community edition from official Microsoft website: http://www.microsoft.com/visualstudio/eng/downloads 

To work with GitHub code repository you need to install Git from: https://git-scm.com/downloads

## <a name="install"></a> Installing

After your environment is ready you can check out microservice source code from the Github repository:
```bash
git clone git@github.com:pip-services/pip-clients-quotes-dotnet.git
```

## <a name="build"></a> Building

Make sure you satisfy dependency to PipServices.Commons and PipServices.Net. If you don't have it readily available
you can download and rebuild it from sourcecode from https://github.com/pip-services/pip-services-commons-dotnet and 
https://github.com/pip-services/pip-services-net-dotnet

Then open the solution in Visual Studio and execute **Rebuild All** command. 

## <a name="test"></a> Testing

Unit testing in the .NET client SDK doesn't run microservice and relies on external .NET microservice instance.
So, follow instructions to install and run the microservice at https://github.com/pip-services-content/pip-services-quotes-dotnet

Make sure you enable REST API endpoint in the microservice. The default microservice HTTP REST port is 8080.
Then check rest configuration in unit tests to match the microservice port. 

When .NET microservice is up and running you can execute **All Tests** command in Visual Studio to run the tests.

## <a name="contrib"></a> Contributing

Developers interested in contributing should read the following instructions:

- [How to Contribute](http://www.pipservices.org/contribute/)
- [Guidelines](http://www.pipservices.org/contribute/guidelines)
- [Styleguide](http://www.pipservices.org/contribute/styleguide)
- [ChangeLog](CHANGELOG.md)

> Please do **not** ask general questions in an issue. Issues are only to report bugs, request
  enhancements, or request new features. For general questions and discussions, use the
  [Contributors Forum](http://www.pipservices.org/forums/forum/contributors/).

It is important to note that for each release, the [ChangeLog](CHANGELOG.md) is a resource that will
itemize all:

- Bug Fixes
- New Features
- Breaking Changes