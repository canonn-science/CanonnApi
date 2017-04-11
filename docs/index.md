# Ruins documentation

## Database

The canonn api database contains all data for ruin layouts, their variants, obelisks, their status and actual ruins sites.

For details see the [database documentation](./design/db/index.md).

## API

The API is an ASP.NET CORE WebApi project, build with Visual Studio 2017.

## Frontend

The frontend is an Angular 4 SPA (single page application). It offers two modes:

1. Readonly website, presenting public data from the database
2. Admin-interface after login, to alter data in the system

See [RuinsApp](../RuinsApp/README.md) for details.

## Authentication

The CanonnApi frontend and backend API both rely on [Auth0.com](Auth0.com) for authentication.
See [Auth0 Setup](./configuration/auth0/auth0-setup.md) for details on how Auth0 should be set up to work with CanonnApi.

## Versioning

The API and the Frontend are versioned independently.
Locally, no version information will be applied to the actual projects. For development, both are always a 1.0.0.
It is intended to maintain the version number in the file `version.txt` for each project. The actual build and deployment process will make use of the Canonn TeamCity / Octopus Deploy and here the version information will be included in the actual projects, based on the version.txt information as well as branch and build number info.
