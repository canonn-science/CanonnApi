# Projects for the Canonn API system(s)

Please see the [documentation](./docs/index.md) start page for more detailed documentation.

## Getting started

### External dependencies

The CanonnApi Frontend and Backend together rely on [Auth0.com](Auth0.com) for authentication.

To locally develop with authenticated users, please sign up to Auth0 (free plan is fine), make yourself comfortable with it, and then follow the steps explained in [Auth0 Setup](docs/configuration/auth0/auth0-setup.md).

### API

The API is a [.NET Core](https://www.microsoft.com/net/core#windowsvs2015) ASP.NET Core WebApi project. You can develop on the API solution with the free [Visual Studio 2017 Community Edition](https://www.visualstudio.com/de/vs/visual-studio-2017-rc/).

The database migration tool is a .NET 4.6 command line project. This can also be build with Visual Studio 2017 community edition.

These are the steps required to locally set up the api:

1. Create a new MariaDb (or MySQL) database.
   1. Create a user that has sufficient privileges on that database to insert, read, update and delete data in tables in this database.
   2. Create a second user that has sufficient privileges to edit the DB schema for the db updates.
2. In the Visual Studio project `\CanonnApi.Backend\CanonnApi.Database.AutoUpdater` open the file `config.json` and enter the connection string with name `connectionString` in the format `server=SERVERNAME;database=DBNAME;userid=DBUSERNAME;password=DBUSERPASSWORD`. Use the user name / password for the user that may create the database schema (step 1.2) here as well as the database name created in step 1.
3. Build and exeute the project. This will create the required tables in your database and seed the initial data.
4. Copy the file `appsettings.Secrets.json` from the folder `docs\configuration\api` to the folder `CanonnApi.Backend\CanonnApi.Web` and fill the empty double quotes with your Auth0 domain and client id. Also, fill the connection string for `ruinsDb` in the format `server=SERVERNAME;database=DBNAME;userid=DBUSERNAME;password=DBUSERPASSWORD` with the user created in step 1.1 and the database name form step 1.

The API will locally run on port 52685.  
You can either start the API from within Visual Studio or from the commandline. For that, go to the `CanonnApi.Backend\CanonnApi.Web` folder and call the `start_dev.bat`. This will set an environment variable so that the API runs in Development mode, and compile and start the API project.

### Client Application / Web Application

The client application / web site is an [Angular](http://Angular.io) Single Page Application (SPA), built with Angular 4.

Prerequisites to build the web application:
1. [Node.js](https://nodejs.org) (ideally the latest 6.x LTS version).
2. [Angular-CLI](https://cli.angular.io/).  
  When Node.js is installed, enter `npm install -g angular-cli` on the commandline to install the angular CLI tooling.
3. On the commandline, go to the folder `CanonnApi.Frontend` and run the command `npm install`. 
  This will install all required additional dependencies for the RuinsApp.

To start the development web server and to run the web app:

1. On the commandline, go to the folder `CanonnApi.Frontend`.
2. Start the development web server with the command `ng serve`.  
  This will compile the application and start the development web server. Any changes on the web apps files will automatically be detected, compiled, and any instance of the running app will be automatically updated.
3. Point your web browser to `http://localhost:4200` to run the app.
