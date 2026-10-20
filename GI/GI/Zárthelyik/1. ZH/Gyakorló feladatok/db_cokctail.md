# "Cocktail" mintaadatbázis

## Az adatbázis elérése

🅐 Az adatbázist felépítő sql mondatok letölthetők innen, ha saját szerveren, vagy lokálisan szeretnéd felépíteni az adatbázist: [se_bikestore.sql](se_bikestore.sql) 

🅑 Ha az egyetemi szerveren lévő változatot használnád:

``` powershell
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

```powershell
Scaffold-DbContext "Data Source=bit.uni-corvinus.hu;Initial Catalog=se_cocktail;User ID=hallgato;Password=Password123;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

> [!WARNING]
>
> Az bit.uni-corvinus.hu csak VPN alól érhető el! 

## Mit érdemes gyakorolni?

❶ Helyezz el az űrlap két szélén egy-egy `ListBox`-ot. A baloldaliba kerüljenek a koktélok szövegdobozon keresztül névre szűrhetően, a jobboldaliba pedig a nyersanyagok - szintén szűrhetően.

❷ Középen helyezz el egy rácsot, melyben a kiválasztott koktélhoz szükséges nyersanyagok jelennek meg a nyersanyag nevének, mennyiségi egységének, és ha szükséges mennyiség feltüntetésével. Rács adatkötéséhez használja saját osztályt!

❸ Valósítsd meg egy szövegdoboz és gomb segítségével, hogy a felhasználó új recept bejegyzéseket tudjon felvenni, illetve törölni. (A szövegdobozba kerül a szükséges mennyiség.)

❹ Szövegdoboz mellett jelenítsd meg egy címkében a kiválasztott nyersanyag mennyiségegységét!



## Az adatbázis sémája

``` mermaid
erDiagram
    Cocktail ||--o{ Recipe : "uses"
    Material ||--o{ Recipe : "included in"
    Type ||--o{ Cocktail : "is typed as"
    Type ||--o{ Material : "is typed as"
    Unit ||--o{ Material : "measured in"
    MaterialType ||--o{ Material : "classified as"
```





``` mermaid
erDiagram
    Cocktail {
        int CocktailSK PK
        nvarchar Name
        tinyint TypeFK FK
        smallmoney Price
    }

    Material {
        smallint MaterialID PK
        nvarchar Name
        tinyint TypeFK FK
        tinyint UnitFK FK
        smallmoney Price
    }

    MaterialType {
        tinyint MaterialTypeID PK
        nvarchar Name
    }

    Recipe {
        int RecipeSK PK
        int CocktailFK FK
        smallint MaterialFK FK
        decimal Quantity
    }

    Type {
        tinyint TypeID PK
        nvarchar Name
    }

    Unit {
        tinyint UnitID PK
        nvarchar Name
    }

    Cocktail ||--o{ Recipe : "uses"
    Material ||--o{ Recipe : "included in"
    Type ||--o{ Cocktail : "is typed as"
    Type ||--o{ Material : "is typed as"
    Unit ||--o{ Material : "measured in"
    MaterialType ||--o{ Material : "classified as"
```

