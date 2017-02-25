# Projects for the Canonn ruins system(s)

Please see the [documentation](./docs/index.md) start page for more detailed documentation.

## Getting started

### External dependencies

RuinsApp and RuinsApi rely on [Auth0.com](Auth0.com) for authentication.

To locally develop with authenticated users, please sign up to Auth0 (free plan is fine), make yourself comfortable with it, and then follow the steps explained in [Auth0 Setup](configuration/auth0/auth0-setup.md).

### API

The API is a [.NET Core](https://www.microsoft.com/net/core#windowsvs2015) ASP.NET Web Api project. You can develop the API with the free [Visual Studio 2017 Community Edition](https://www.visualstudio.com/de/vs/visual-studio-2017-rc/).

A single step is required to locally set up the api:

1. Copy the file `appsettings.Secrets.json` from the folder `configuration\api` to the folder `RuinsApi\RuinsApi` and fill the empty double quotes with your Auth0 domain and client id (the client secret is currently not required).

The API will locally run on port 52685.
You can either start the API from within Visual Studio or from the commandline. For that, go to the `RuinsApi\RuinsApi` folder and call the `start_dev.bat`. This will set an environment variable so that the API runs in Development mode, and compile and start the API project.

### Client Application / Web Application

The client application / web site is an [Angular](http://Angular.io) Single Page Application (SPA), built with the latest Angular 4 beta.

Prerequisites to build the web application:
1. [Node.js](https://nodejs.org) (ideally the latest 6.x LTS version)
2. [Angular-CLI](https://cli.angular.io/)  
  When Node.js is installed, just enter `npm install -g angular-cli` on the commandline to install
3. On the commandline, go to the folder `RuinsApp` and run the command `npm install`. 
  This will install all required additional dependencies forthe RuinsApp

To start the development web server to run the web app:

1. On the commandline, go to the folder `RuinsApp`
2. Start the development web server with the command `ng serve`  
  This will compile the application and start the development web server. Any changes on the web apps files will automatically be detected, compiled, and any instance of the running app will be automatically updated
3. Point your web browser to `http://localhost:4200` to run the app