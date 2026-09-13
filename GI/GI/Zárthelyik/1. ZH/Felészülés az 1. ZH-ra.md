# Felészülés az 1. ZH-ra


> [!warn]
A feladtaleírás a ZH-ban lényegretörő lesz, akár képenyőkép szintű. Az egyes feladatok nem lesznek elemi lépésekre bontva, mit a gyakorlati feladatsorok.

## Emlékeztetőül

Ezt a ZH alatt is használhatjátok vágólapon.

``` powershell
Scaffold-DbContext "[Conncection string]" Microsoft.EntityFrameworkCore.SqlServer -OutputDir [Mappa] -Context [Context osztály neve]
```

Azok, akik unják a NuGet grafikus felületén kitallózni a csomagokat, a PM Console segítségével is hozzáadhatják a projekthez:

``` powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer  
Install-Package Microsoft.EntityFrameworkCore.Tools
```

> [!info]
> A ZH megírásához egy darab **névvel ellátott** A4-es **kézzel írott** lap felhasználható, más, mint internetes dokumentáció vagy tananyag illnetve kódolást segítő **LLM nem vehető igénybe.** Másolat nem hozható be!
## Az 1. ZH témakörei

Az első ZH nem tartogat meglepetéseket, gyakorlással tökéletesen fel lehet **készülni** rá.

- Az alábbi lista azokat a főbb feladattípusokat foglalja össze, amelyek az 1. ZH-ban nagy valószínűséggel előfordulhatnak. Azokat, akik az alábbiakat érik, és használni is tudják, nagy valószínűséggel nem éri meglepetés! A lista nem teljeskörű!

    1. **Adatbázis tervezés**: Szövegesen megfogalmazott követelmények alapján a Mermaid ER diagram építése. 
	2. **Object Relational Mapping:** `Scaffold-DbContext` használata: az adatbázis sémáját leképező objektum létrehozása `[Adatbázisnév]Context` néven.
    3. **Alkalmazáskeret építése:** a 6. előadásról ismert elrendezés megalkotása, mely a `Form1`-en elhelyezett gombok lenyomására `UserControl`-okat cserélget egy `Panel` felületén. A ZH egyes részfeladatait külön `UserControl`-okon kell megoldani.
        
        1. lépés: Hozz létre egy _Windows Forms_ alapú alkalmazást!
            
        2. lépés: Az induláskor megjelenő űrlap baloldalán helyezz el három gombot és egy panelt!
            
        3. lépés: A Panel kerete legyen vékony vonal!
            
        4. lépés: A Panel az űrlap átméretezésekor kövesse az űrlap méretét!
            
        5. lépés: Addj három `UserControl`t a projekthez, elnevezésük tetszőleges.
            
        6. lépés: A gombok megnyomására ürítsd a panel tartalmát, és helyezd el a megfelelő `UserControl`t úgy, hogy a `UserControl` kitöltse a `Panel` teljes területét átméretezéskor is.
            
    4. **Idegen kulcsokat is tartalmazó tábla adatainak megjelenítése DataGridView-ban**, az idegen kulcsok helyén legördülő dobozokkal. Feladat: jelenítsd meg a rendeléseket tartalmazó tábla adatait táblázatban.
        
    5. **Tábla adatainak megjelenítése ListBox-ban** Feladat: jelenítsd meg a hallgatók nevét `ListBox`-ban!
        
    6. **Szűrhető lista készítése** Feladat: az előző feladatban szereplő lista fölé helyezz szövegdobozt, melynek módosításakor a lista tartalma szűrhető
        
    7. **Új rekord felvétele kódból:** rekordot leíró osztály példányosítása, tulajdonságainak beállítása, majd rögzítése. A rögzítés történhet kétféleképpen. Feladat: a `ListBox` alatt helyezz el egy szövegdobozt és egy gombot! A gomb megnyomására a szövegdobozba írt név kerüljön az adatbázisba!
        
    8. **Rekord törlése kódból:** a fentiekhez hasonló gondolatmenetet követve. Feladat: a `ListBox` alatt helyezz el egy gombot, amelyre kattintva a felhasználó törölheti a kiválasztott hallgatót az adatbázisból (megerősítés után).
        
    9. **Adatok mentése az adatbázisba** hibakezeléssel (`_context.SaveChanges()`+ `try-catch` blokk). Feladat: ha az adatok mentése közben kivétel keletkezik, a hibaüzenet jelenjen meg felugró ablakban!
        
    10. **Master-detail relációk kezelése:** egy-a-többhöz, vagy több-a-többhöz típusú kapcsolatok kezelése ListBox és DataGridView segítségével: csak a master szerepet betöltő ListBox-ban kiválasztott rekordhoz kapcsolódó adatok jelennek meg a detail szerepet betöltő DataGridView-ban. Például csak a masterben kiválasztott fogáshoz tartozó receptbejegyzések jelennek meg a detail-ben. Az idegen kulcsok helyén legördülő dobozok használata válhat szükségessé.
        
    11. **Egyetlen értéket visszaadó LINQ-lekérdezés:** például egy összegzés vagy átlagszámítás.
        
        1. feladat: számold ki a rendelhető könyvek átlagárát!
            
        2. feladat: jelenítsd meg, hogy hány rendelés van rögzítve az adatbázisban! (Azaz: a keresett érték a tábla sorainak száma.)
            
    12. **Egy vagy több rekordot visszaadó LINQ-lekérdezés**, melynek eredménye táblázatban jeleníthető meg. Feladat: jelenítsd meg a rendelések adatait táblázatban! A táblázat három oszlopot tartalmazzon, az egyikbe a hallgató neve, a másik kettőbe a rendelt könyv címe és ára kerüljön!
        
    13. **XML dokumentum építése és mentése** SQL adatbázisban tárolt struktúra alapján.
        
    14. **Excel dokumentum építése és mentése** SQL adatbázisban tárolt tábla adataival.
        
    15. **Összetettebb feladat** a videóban is szereplő tankönyv-támogatásos példához hasonlóan.
        
    16. **Megerősítő kérdés:** az alkalmazásból csak megerősítő kérdés után lehessen kilépni.
        
    17. **Adatbázis tervezése** feladatleírás alapján. Leadandó a Mermaid diagram kódja vagy az adatbázist felépítő SQL mondatok.
        
    

A ZH-ban használandó adatbázisokhoz conncetion stringet biztosítunk, valamint Scaffold-DbContext példa is rendelkezésre áll.
# Az 1. ZH felépítése

Az első zárthelyi két részből fog állni:

1. Az első rész egy adatbázis-tervezési feladat, melyet Markdown `erDiagram` formájában kell elkészíteni és beadni.
2. A második rész egy adott adatbázishoz felhasználói felület tervezése lesz. 

## 1. rész: Adatbázis tervezési feladat

Gyakorló feladatok:

[[db_terv_fogaszat]]
[[db_terv_korcsolya]]
## 2. rész: UI építése

A felhasználói felület tervezés gyakorlására az alábbi feladatokat javasoljuk: 

[[db_bikestore]]
[[db_chinook]]
[[db_cokctail]]
[[db_etkeztetes]]
[[db_pets]]
[[db_tanulmanyi]]

A ZH-n olyan user interfészt kell majd készíteni, ahol gombok lenyomására jelennek meg különböző `UserControl`-ok az egyes részfeladatokkal. Az alábbi két videó ennek a felépítésében nyújt segítséget. 

<video controls src="https://storage.altinum.hu/usercontrol_ui/Tobbablakos_1_Panel.m4v"></video> 

<video controls src="https://storage.altinum.hu/usercontrol_ui/Tobbablakos_2_Gomb.m4v"></video>
