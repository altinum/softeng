## Edgar F. Codd – a relációs adatbázisok atyja

**Edgar Frank "Ted" Codd** (1923–2003) brit-amerikai matematikus és informatikus volt, akinek nevéhez a modern adatbázis-elmélet egyik legfontosabb áttörése, a **relációs adatmodell** megalkotása fűződik – anélkül talán ma sem léteznének az SQL-alapú adatbázisok, amiket szinte minden szoftverrendszer használ.

### Fiatalkor és háború

Codd 1923-ban született Portlandben, Dorset megyében, Angliában. Oxfordban matematikát és kémiát tanult, de tanulmányait félbeszakította a második világháború: pilótaként szolgált a Királyi Légierőnél (RAF). A háború után Kanadába, majd az Egyesült Államokba költözött.

### Az IBM-évek és a nagy ötlet

1949-ben csatlakozott az **IBM**-hez, ahol programozóként, majd kutatóként dolgozott. 1965-ben doktorált számítástechnikából a Michigani Egyetemen. Ekkoriban az adatbázisok még hierarchikus vagy hálós modelleket használtak, amelyek merevek voltak és mélyen a fizikai tárolási struktúrához kötődtek – ha megváltozott a háttérben az adatszerkezet, az alkalmazásokat is újra kellett írni.

Codd erre adott radikálisan új választ **1970-ben**, "A Relational Model of Data for Large Shared Data Banks" című cikkében. Az ötlete: az adatokat egyszerű, matematikailag megalapozott **táblákban (relációkban)** kell tárolni, a felhasználót pedig el kell választani attól, hogy tudnia kelljen, fizikailag hogyan vannak eltárolva az adatok. Ez az elgondolás vezetett később az **SQL** nyelv és gyakorlatilag az összes mai relációs adatbázis-kezelő rendszer (Oracle, MySQL, PostgreSQL, SQL Server stb.) megszületéséhez.

Érdekesség, hogy az IBM-en belül eleinte nem lelkesedtek túlzottan az ötletéért, mivel az veszélyeztette a cég meglévő, hierarchikus IMS adatbázis-termékét. Codd elméletét végül más cégek (pl. az Oracle elődje) hasznosították elsőként kereskedelmi termékben.

### Elismerés és később évek

1981-ben megkapta a számítástudomány legrangosabb kitüntetését, a **Turing-díjat**. Az 1990-es évek elején ő alkotta meg az **OLAP** (Online Analytical Processing) kifejezést is. 2003-ban hunyt el Williams Islanden, Floridában.

### Codd 12 szabálya

1985-ben Codd megfogalmazott egy szabályrendszert, amivel eldönthető, hogy egy adatbázis-kezelő rendszer valóban "relációsnak" tekinthető-e. Érdekesség, hogy valójában **13 szabály** van, mert egy 0-s szabállyal (alapszabály) kezdődik:

- **0. szabály (alapszabály):** A rendszernek relációs elveken kell kezelnie az adatokat a tárolástól a lekérdezésig.
- **1. Információs szabály:** Minden adatot kizárólag táblázatos formában, cellaértékekként kell tárolni.
- **2. Garantált hozzáférés:** Minden adatelem elérhető táblanév + sor (kulcs) + oszlopnév kombinációval.
- **3. NULL-értékek szisztematikus kezelése:** A hiányzó/ismeretlen adatokat egységesen kell kezelni, függetlenül az adattípustól.
- **4. Dinamikus online katalógus:** Az adatbázis felépítését (metaadatokat) magának az adatbázisnak kell tartalmaznia, ugyanazokkal az eszközökkel lekérdezhetően.
- **5. Átfogó adat-allanyelv:** Legyen legalább egy nyelv, amely támogatja az adatdefiníciót, -manipulációt, integritási megkötéseket és tranzakciókezelést (ez lett gyakorlatilag az SQL).
- **6. Nézetfrissítési szabály:** Minden elméletileg frissíthető nézetnek (view) frissíthetőnek kell lennie a rendszerben is.
- **7. Magas szintű beszúrás, frissítés, törlés:** Ezeket a műveleteket egész sorhalmazokra, ne csak egyetlen sorra lehessen elvégezni.
- **8. Fizikai adatfüggetlenség:** Az alkalmazások működése nem sérülhet, ha megváltozik az adatok fizikai tárolási módja.
- **9. Logikai adatfüggetlenség:** Az alkalmazások működése nem sérülhet, ha megváltozik a táblák logikai felépítése (pl. új oszlop hozzáadása).
- **10. Integritás-függetlenség:** Az integritási megkötéseket magában az adatbázisban kell definiálni, ne az alkalmazói kódban.
- **11. Elosztási függetlenség:** A felhasználói lekérdezéseknek ugyanúgy kell működniük, függetlenül attól, hogy az adatok egy vagy több fizikai helyen vannak-e tárolva.
- **12. Nonszubverziós szabály:** Ha a rendszer alacsonyabb szintű (rekordonkénti) hozzáférést is enged, az nem kerülheti meg vagy sértheti meg a relációs integritási szabályokat.

Érdekes adalék: ezeket a szabályokat részben azért fogalmazta meg, hogy leleplezze a piacon megjelenő, magukat "relációsnak" hirdető, de valójában csak felületesen táblázatos adatbázis-termékeket – vagyis egyfajta minőségbiztosítási eszközként is szolgáltak a kifejezés integritásának megvédésére.