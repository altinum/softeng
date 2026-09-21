A `Microsoft.EntityFrameworkCore.Tools` és a `Microsoft.EntityFrameworkCore.SqlServer` a NuGet konzolán keresztül ezzel a két paranccsal is hozzáadható a projekthez:

```powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```

A Scaffold parancsot a ZH-n is készen kapjátok:

```powershell
Scaffold-DbContext "Data Source=bit.uni-corvinus.hu;Initial Catalog=se_bikestore;Persist Security Info=True;User ID=hallgato;Password=Password123;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir BikestoreModels
```

Emlékeztetőül a séma:
[![image-20250429181520395](https://altinum.github.io/docfx_softeng/softtech/bikestore_mintazh/image-20250429181520395.png)](https://altinum.github.io/docfx_softeng/softtech/bikestore_mintazh/image-20250429181520395.png)

