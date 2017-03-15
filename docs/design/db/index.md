# Ruins-DB

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![Ruins-DB](http://www.plantuml.com/plantuml/svg/9Ot12SCm303_cI8Te1RqMIZ9AhOa4e4Z6qjArsz2VtVSurRpF3oEEZr4iGPnZ3hlxjtU02D_qoQ-HmdZWKsTrHEs0p1hKtqQ2YioZ10r83wvjWxKy1QJJO7AFr99_Teiv-SrhQnqyGy0.svg)

_Hint: When updating the PUML file, change this link to include a new version parameter, because github is cacheing aggressively._

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate / update the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -Context RuinsContext -OutputDir DatabaseModels -Force
```