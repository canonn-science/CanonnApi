# Auth0 setup for RuinsApplication

The following data needs to be configured in [Auth0](https://auth0.com) to be able to log in to the development environment.

You can sign up for the Auth0 free plan, and from there start with a client for the Ruins App. Create a new client, and add configure the following settings:

## Settings

1. Client Type: `Single Page Application`
2. Allowed Callback URL's: `http://localhost:4200`
3. Allowed Logout URL's: `http://localhost:4200`
4. Allowed Origins (CORS): `http://localhost:4200`
5. Use Auth0 instead of the IdP to do Single Sign On: `active`

### Advanced Settings

1. OAuth, JsonWebToken Signature Algorithm: `RS256`

## Auth0 Extensions

Please install the following Extension: **Auth0 Authorization**.

Then follow these steps:

1. Note/copy the `Client Id` of the Auth0 client you created above
2. Edit the `authorization-extension.data.json` in this folder, and replace all occurances of `YOUR_APPLICATION_ID_HERE` with the client id of your application from step 1.
3. From the Auth0 left hand navigation, select Extensions, and then select the `Auth0 Authorization` extension.
4. When the extension management page opened, select `configuration` from the top right drop down menu.
5. Go to tab `Import / Export`.
6. Select `Import` and import the file `authorization-extension.data.json` that now contains your client id.
7. Go to `Rule Configuration`, make sure the first three options are active, and click on 'Publish Rule`.
7. Last, assign the groups (or roles) you just imported to your users as required.

## Changes in this project

Copy the `api/appsettings.Secrets.json` file to the `RuinsApi` folder. Edit the file, and copy the client Domain and client ID from the Auth0 configuration to this file.
