# Ruins-DB

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![Ruins-DB](http://www.plantuml.com/plantuml/svg/9SrB2iCm303HVKxH7g0MtHPASnLR4ed0beC-xVKRo6xWBMRrg1Ov-d8NnPx4QVssHnp-0h3wAvl4dYsT3QS6QnIS0x3gL7qu2YioM8ewZEh11ZJHWTXbKw1s6RLod9VFz_rSLbOw-my0)

_Hint: When updating the PUML file, change this link to include a new version parameter, because github is cacheing aggressively._

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -OutputDir DatabaseModels
```