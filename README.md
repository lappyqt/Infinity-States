<img src="https://github.com/lappyqt/Infinity-States/blob/main/Infinity%20States/wwwroot/img/icon.png" width="128" height="128" />

# Infinity-States

__Infinity States__ is a web service that allows you to read and publish content in the form of articles, posts and even documentation... In addition to creating public content, you can also create private content, such as to-do list, that will only be accessible to you and your friends.

## Installing the development environment

### IDE and Dotnet core

You can use any IDE like [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio for mac](https://visualstudio.microsoft.com/vs/mac/) or [Rider](https://www.jetbrains.com/rider/)... 

> #### Windows and macOS

[Download SDK from official website](https://dotnet.microsoft.com/en-us/download)


> #### Linux 

[Download SDK from official website](https://dotnet.microsoft.com/en-us/download?initial-os=linux)

__Or install manually (commands for Ubuntu)__

_Add PPA repository_

    wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

_Install_

    sudo apt-get update; \
    sudo apt-get install -y apt-transport-https && \
    sudo apt-get update && \
    sudo apt-get install -y dotnet-sdk-6.0
    
> #### Docker

[Install dotnetcore docker image](https://hub.docker.com/_/microsoft-dotnet)

> #### FreeBSD

_Unsupported_ 

### Databases


> #### Postgresql

[Postgresql can be downloaded for all operating systems on the website](https://www.postgresql.org/download/)

__Install on linux from repository (Debian based)__
    
    sudo apt-get update
    sudo apt-get install postgresql

## Architecture

For the architecture of this project, we use official guidelines from Microsoft _(with modifications)_. Please, download the book from the link below...

[ASP.NET Core architecture e-book](https://dotnet.microsoft.com/en-us/learn/aspnet/architecture)
