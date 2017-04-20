# Ruins-DB

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![Ruins-DB](http://www.plantuml.com/plantuml/svg/7St13S8m3030VwU00OZXYuGeOfB4jbfBgHF5Thi-5SzxtMoUXyTMfwierY2EKI-hUxSdmCX7MiJNA64yiAcpUiAs0MPjgZT3OKM6xv-VBa2ySsqTgA6TC5aKgC08KQEI-bczzzVZFisiTE87.svg)

_Hint: When updating the PUML file, change this link to include a new version parameter, because github is cacheing aggressively._

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate / update the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -Context RuinsContext -OutputDir DatabaseModels -Force
```

# Database upgrades / versioning

Database upgrades are automated using [DbUp](https://github.com/DbUp/DbUp).

This is done in the project `Canonn.Database`. The single versioning scripts are included in this project as embedded resources. This allows for automatic db migration during automated deployments.

Please use the existing scripts as a template for creating new versions of the db.
Make sure, that the state of the database always matches with the state of the API project.
