# Ruins-DB

The database layout is currently designed as follows.

Please note the currently implemented elements already reflect the maria db DML. The not yet DML-like entries are not implemented and this is preliminary design.

![Ruins-DB](http://www.plantuml.com/plantuml/svg/9Scx3SCm30N0FbCa0qJiiur44ZO1wSdW1rxVHQwzZu5dYPRFzvbvnO_8-zqEoxD6XJflf28RxqNSiO5li2bOoSiGj7gaEidc8D6m0oJZVpBQLMk-7W00.svg?v=1)

# Model generation

Using the NuGet packages `Pomelo.EntityFrameworkCore.MySql` and `Pomelo.EntityFrameworkCore.MySql.Design` you can generate the model using this command:

```
Scaffold-DbContext "server=DB-HOST;database=DB-NAME;userid=DB-USER;password=DB-PASSWORD" Pomelo.EntityFrameworkCore.MySql -OutputDir DatabaseModels
```