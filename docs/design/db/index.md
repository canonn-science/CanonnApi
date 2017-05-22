# CanonnAPI Database

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![CanonnAPI DP](http://www.plantuml.com/plantuml/svg/7St12SCm3030VwPe1wX5leNIb4uIIo8HEBAnf6JzcZxlTOlvsZsECjr5iGHnz7BRtPiz0VfwfKryZnp67QiwgoUi1-2gLVLXAAp8yFtpqmI8Jowr0LMq0PDDWJBq43NAgOtgVRxcULfOQU07.svg)

_Hint: When updating the PUML file, change this link to include a new version parameter, because github is cacheing aggressively._

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate / update the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -Context RuinsContext -OutputDir DatabaseModels -Force
```

# Database upgrades / versioning

Database upgrades are automated using [DbUp](https://github.com/DbUp/DbUp).

This is done in the project `Canonn.Database.AutoUpdater`. The single versioning scripts are included in this project as embedded resources. This allows for automatic db migration during automated deployments.

Please use the existing scripts as a template for creating new versions of the db.
Make sure, that the state of the database always matches with the state of the API project.
