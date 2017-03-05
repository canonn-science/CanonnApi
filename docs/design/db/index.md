# Ruins-DB

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![Ruins-DB](http://www.plantuml.com/plantuml/svg/9Ot13OCm303_J447QAn-ArMmIc9RO2auKMpJzGlgxqxtkDKy3u-ZJWzHh44SeywxUxStmCZVj8dlKS9uO5DdzOJj0CpQL9-6meeC8qGDY4-khGCrlCLaKw3oZrHIlpRB-NbDAojT_0C0.svg)

_Hint: When updating the PUML file, change this link to include a new version parameter, because github is cacheing aggressively._

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -OutputDir DatabaseModels
```