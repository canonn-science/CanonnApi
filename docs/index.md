# Ruins documentation

## Database

The ruins database contains all data for ruin layouts, their variants, obelisks, their status and actual ruins sites.

For details see the [database documentation](./design/db/index.md).

## API

API will be ASP.NET Web API.

## Frontend

Frontend will be a Angular 4 SPA (single page application).
It will have two modes:

1. Readonly website, presenting public data from the database
2. Admin-interface after login, to alter data in the system

See [RuinsApp](../RuinsApp/README.md) for details.

## Auth

Current idea with authorization is to use Google Firebase for login.
This should yield a JWT token we can use in the API to authenticate a user.

For authorization we then use data from the token to map it to a local user store (email address / roles), to check what permissions the user has.

See (Secure an ASP.NET Core API with Firebase)[https://blog.markvincze.com/secure-an-asp-net-core-api-with-firebase/].
See also (Firebase quicksstarts)[https://github.com/firebase/quickstart-js].