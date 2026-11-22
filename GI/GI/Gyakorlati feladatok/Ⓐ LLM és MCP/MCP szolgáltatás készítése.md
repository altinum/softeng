# MCP szolgáltatás készítése ASP.NET Core-ban – lépésről lépésre

Ebben az útmutatóban egy saját MCP-szervert építünk Visual Studio-ban, kívülről elérhetővé tesszük Dev Tunnels segítségével, majd bekötjük a Claude-ba, hogy az LLM meg tudja hívni a saját C# függvényeinket.

**Amire szükséged lesz:**

- Visual Studio 2022 (17.5 vagy újabb) az *ASP.NET and web development* workloaddal
- .NET 8 SDK vagy újabb
- Microsoft- vagy GitHub-fiók (a Dev Tunnels-hez)
- Claude-fiók, amelyben egyéni konnektort (custom connector) lehet hozzáadni
- Azure-előfizetés (publikáláshoz)

---

## 0. Bevezető

### Mi az az MCP és mire használható?

Az **MCP (Model Context Protocol)** egy nyílt szabvány, amely egységes módon köti össze az LLM-alapú alkalmazásokat (Claude, ChatGPT, IDE-asszisztensek stb.) a külső eszközökkel és adatforrásokkal. Úgy érdemes elképzelni, mint a „USB-C-t az AI-hoz": egyszer megírsz egy szervert, és bármelyik MCP-t támogató kliens használni tudja.

Az MCP-szerver háromféle dolgot tehet elérhetővé:

| Elem                        | Mire való                                              | Példa                                                        |
| --------------------------- | ------------------------------------------------------ | ------------------------------------------------------------ |
| **Tools (eszközök)**        | Az LLM által meghívható függvények, műveletek          | „Hozz létre egy jegyet a hibakövetőben", „dobj egy kockával" |
| **Resources (erőforrások)** | Olvasható adatok, amelyeket a modell kontextusként kap | Dokumentumok, adatbázis-rekordok, fájlok                     |
| **Prompts (promptok)**      | Előre elkészített, paraméterezhető prompt-sablonok     | „Code review kérés" sablon                                   |

Tipikus felhasználási területek:

- **Céges rendszerek elérése:** CRM, ERP, jegykezelő, belső API-k – a modell a saját adataidból dolgozik.
- **Műveletvégzés:** nemcsak kérdezni lehet, hanem cselekedni is (e-mail küldése, rekord módosítása, deploy indítása).
- **Fejlesztői munkafolyamatok:** adatbázis-séma lekérdezése, logok olvasása, CI/CD vezérlése a kódoló asszisztensből.
- **Automatizálás:** több rendszer összekötése természetes nyelvű utasítással.

> [!important]
>  **Biztonsági megjegyzés:** az MCP-szerver olyan, mintha egy API-t adnál az LLM kezébe. Mindig gondold végig, milyen műveleteket engedsz meg, és éles környezetben kötelezően alkalmazz hitelesítést (lásd a 8. fejezetet).

### Mik azok a voice OS-ek? Mi a jövő?

A **voice OS** (hang-alapú operációs rendszer / interakciós réteg) olyan felület, ahol a **beszéd az elsődleges bemenet**, a képernyő, az egér és a billentyűzet pedig másodlagos. Nem egyetlen termékről van szó, hanem egy irányról: az asszisztens nem egy alkalmazás az eszközön belül, hanem maga a belépési pont a rendszerhez.

Mitől lett ez most reális?

- A modern beszédfelismerés és -szintézis természetes, alacsony késleltetésű beszélgetést tesz lehetővé, félbeszakítással együtt.
- Az LLM-ek értik a szándékot, nem kell fix parancsszavakat megtanulni.
- **Az MCP adja a „kezeket"**: a hangasszisztens az MCP-n keresztül tud valódi dolgokat csinálni (naptárfoglalás, rendelés, okosotthon-vezérlés, céges rendszerek lekérdezése).

Merre tart a fejlődés (iránymutatás, nem jóslat):

1. **Ambient, mindig elérhető asszisztensek:** autóban, fülhallgatóban, okosszemüvegben, telefonon – ahol a kéz és a szem foglalt.
2. **Alkalmazások helyett képességek:** a felhasználó nem appot nyit, hanem kimondja a célt; a háttérben az asszisztens eszközöket (MCP-szervereket) hív.
3. **Multimodalitás:** hang + kép + képernyő együtt (pl. „ezt nézd meg a kamerával, és foglalj szervizidőpontot").
4. **Új feladat a fejlesztőknek:** a szolgáltatásokat *agent-barát* módon kell kitenni (tiszta eszközleírások, biztonságos jogosultságok), hiszen a „felhasználó" egyre gyakrabban egy AI-ügynök.
5. **Nyitott kérdések:** adatvédelem (mindig hallgató eszközök), hitelesítés hanggal (deepfake-kockázat), megbízhatóság, és hogy ki irányítja a belépési pontot.

---

## 1. Projekt létrehozása

Egy MCP-alkalmazást Visual Studio-ban legegyszerűbben **ASP.NET Core Web API projektként, .NET 8 vagy újabb verzióval** érdemes elkészíteni. Ezt később Azure App Service (Azure Web App) szolgáltatásba publikálhatod, így az MCP-szervered egy publikus HTTPS-végpontként lesz elérhető.

1. Indítsd el a Visual Studio-t, és válaszd a **Create a new project** lehetőséget.
2. Keress rá: `ASP.NET Core Web API`, majd **Next**.
3. Projektnév például: `FunMcpServer`. **Next**.
4. Framework: **.NET 8.0** (vagy újabb).
5. Beállítások:
   - **Configure for HTTPS:** bekapcsolva
   - **Enable OpenAPI support:** nem szükséges (kikapcsolhatod)
   - **Use controllers:** nem szükséges (az MCP-hez nem kell)
6. **Create**.

A sablon egy `WeatherForecast` mintát is tartalmazhat; ezt nyugodtan törölheted, nincs rá szükség.

---

## 2. Dev Tunnels bekapcsolása

Ahhoz, hogy az MCP szolgáltatásunkat tesztelni tudjuk, a Claude-nak vagy a ChatGPT-nek el kell tudnia érni a webalkalmazásunkat. A lokális gépen futó, Visual Studio-ból indított webalkalmazás viszont nem érhető el a nyílt internet felől. Ezen segít a **Dev Tunnels**: a Visual Studio egy publikus HTTPS-címet (`https://valami-7237.euw.devtunnels.ms`) köt a géped `localhost` portjához.

### 2.1. Beállítás

1. Nyisd meg a projektet, és a felső eszköztáron a **Start** gomb melletti legördülő listában válaszd ki a **http** vagy **https** profilt (az **IIS Express** profil helyett).
2. Kattints a **Debug target** legördülő listára, és válaszd: **Dev Tunnels (no active tunnel) → Create A Tunnel…**
   (Ha nem látod: *Tools → Options → Environment → Preview Features* alatt kapcsold be a Dev Tunnels támogatást, vagy frissítsd a Visual Studio-t.)
3. A megjelenő ablakban add meg:
   - **Account:** jelentkezz be Microsoft- vagy GitHub-fiókkal
   - **Name:** pl. `fun-mcp`
   - **Tunnel Type:** *Temporary* (gyors teszthez) vagy *Persistent* (a cím állandó marad, így nem kell minden indításkor újra beállítani a Claude-ban)
   - **Access:** **Public**. Ez fontos: a Claude szervere nem tud bejelentkezni a te Microsoft-fiókoddal, ezért csak publikus tunnel működik.
4. **OK**. A tunnel mostantól a Debug target listában aktív.
5. Indítsd el a projektet (**F5**, vagy **Ctrl+F5** debug nélkül). Ha a Visual Studio rákérdez, engedélyezd a tunnel használatát.
6. Az **Output** ablakban (*Show output from: Dev Tunnels*) vagy a böngésző címsorában látod a publikus címet, például:
   `https://fun-mcp-7237.euw.devtunnels.ms`

Az MCP végpont címe a 3. lépés után: **`https://<a-te-tunneled>.devtunnels.ms/mcp`**

> **Tipp:** ha a projekt `launchSettings.json` fájlja több profilt tartalmaz, ellenőrizd, hogy a tunnelhez rendelt profil fut-e.

### 2.2. Tesztelés

Először azt kell ellenőrizni, hogy a tunnel maga működik-e, utána azt, hogy az MCP-szerver válaszol-e (ez utóbbit a 3–4. lépés elkészülte után tudod kipróbálni).

**a) A tunnel elérhetősége**

Nyisd meg a publikus címet egy böngészőben, lehetőleg inkognitó ablakban. Az első látogatáskor a Dev Tunnels egy figyelmeztető oldalt (anti-phishing interstitial) jeleníthet meg: kattints a **Continue** gombra. Ha a saját alkalmazásod válaszol (akár 404-gyel is), a tunnel él.

> A figyelmeztető oldal csak böngészőből érkező kéréseknél jelenik meg; az MCP-kliensek (Claude) számára ez nem akadály. Saját eszközből hívva a `X-Tunnel-Skip-AntiPhishing-Page: true` fejléccel kerülhető meg.


**b) Gyors ellenőrzés curl-lel**

```bash
curl -X POST https://<a-te-tunneled>.devtunnels.ms/mcp \
  -H "Content-Type: application/json" \
  -H "Accept: application/json, text/event-stream" \
  -d '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2025-03-26","capabilities":{},"clientInfo":{"name":"curl","version":"1.0"}}}'
```

Ha a válasz egy `serverInfo` és `capabilities` mezőket tartalmazó JSON (vagy SSE-esemény), az MCP-szerver működik. Az eszközlistát lekérő további hívásokhoz a válasz `Mcp-Session-Id` fejlécét kell visszaküldened.

**Gyakori hibák:**

| Tünet | Valószínű ok |
|---|---|
| 401 / bejelentkezési oldal | A tunnel nem *Public* hozzáférésű |
| A cím nem érhető el | A projekt nem fut, vagy nem a tunnelhez rendelt profil indult |
| 404 az `/mcp` címen | Hiányzik a `app.MapMcp("/mcp")` sor |
| 406 Not Acceptable | Hiányzik az `Accept: application/json, text/event-stream` fejléc |

---

## 3. NuGet csomag telepítése

Telepítsd a `ModelContextProtocol.AspNetCore` NuGet csomagot. Ez a csomag out-of-the-box megoldja azoknak az MCP szabványnak megfelelő API végpontoknak az előállítását, amelyekhez az LLM csatlakozni tud.

**Visual Studio-ban:** *Project → Manage NuGet Packages… → Browse*, keresd meg a `ModelContextProtocol.AspNetCore` csomagot, és telepítsd. (Ha nem találod, pipáld be az **Include prerelease** opciót.)

**Parancssorból:**

```bash
dotnet add package ModelContextProtocol.AspNetCore
```

A `Program.cs`-be ezeket a sorokat kell beszúrni a megfelelő helyekre.

```csharp
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();
```

```csharp
app.MapMcp("/mcp");
```

### Mire valók ezek a sorok?

| Sor | Szerepe |
|---|---|
| `AddMcpServer()` | Regisztrálja az MCP-szerver szolgáltatásait a függőséginjektálási (DI) konténerben. Ez az MCP-kezelés alapja. |
| `WithHttpTransport()` | Bekapcsolja a HTTP-alapú átvitelt (Streamable HTTP, illetve a régebbi SSE kompatibilitás). Enélkül az MCP-szerver csak helyi, stdio-alapú kliensekkel működne, a Claude pedig távolról, HTTP-n keresztül csatlakozik. |
| `WithToolsFromAssembly()` | Végigkeresi az assemblyt (a projekt lefordított kódját), és automatikusan regisztrál minden `[McpServerToolType]` attribútummal ellátott osztályt, benne a `[McpServerTool]` jelölésű metódusokkal. Így nem kell kézzel felsorolni az eszközöket. |
| `app.MapMcp("/mcp")` | Leképezi az MCP végpontot a megadott útvonalra. A kliensek ezen a címen (`https://…/mcp`) érik el a szervert. |

### Teljes `Program.cs` egy minimális példában

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapMcp("/mcp");

app.Run();
```

> A sablonból származó `AddControllers`, `AddOpenApi`, `UseAuthorization` stb. sorokra nincs szükség, nyugodtan törölheted őket. A `UseHttpsRedirection()` kihagyása is javasolt Dev Tunnels használatakor, mert a tunnel már HTTPS-t ad, és az átirányítás néha zavart okoz.

---

## 4. Mintafüggvények készítése

Teszteléshez ezeket a függvényeket tesszük elérhetővé az LLM számára. Hozz létre egy új osztályfájlt (például `Tools/FunTools.cs`), és másold bele az alábbi kódot:

```csharp
using System.ComponentModel;
using System.Security.Cryptography;
using ModelContextProtocol.Server;

[McpServerToolType]
public class FunTools
{
    private static readonly string[] Excuses =
    [
        "A macska ráfeküdt a billentyűzetre, és törölte a fél projektet.",
        "A build lefutott nálam, csak a CI szerver nem hitt benne.",
        "Tegnap még működött, ma viszont szerintem a gép megsértődött.",
        "A merge conflict olyan szép volt, hogy nem mertem hozzányúlni.",
        "A felhő épp esős időt jósolt, ezért nem deployoltam."
    ];

    [McpServerTool(Name = "roll_dice"), Description("Kockadobás. Megadható az oldalak száma és a dobások száma.")]
    public object RollDice(
        [Description("Hány oldalú a kocka (2-1000)")] int sides = 6,
        [Description("Hányszor dobjon (1-20)")] int count = 1)
    {
        sides = Math.Clamp(sides, 2, 1000);
        count = Math.Clamp(count, 1, 20);

        var rolls = Enumerable.Range(0, count)
            .Select(_ => RandomNumberGenerator.GetInt32(1, sides + 1))
            .ToArray();

        return new { Sides = sides, Rolls = rolls, Sum = rolls.Sum() };
    }

    [McpServerTool(Name = "flip_coin"), Description("Érmefeldobás: fej vagy írás.")]
    public string FlipCoin() =>
        RandomNumberGenerator.GetInt32(2) == 0 ? "Fej" : "Írás";

    [McpServerTool(Name = "generate_password"), Description("Véletlen jelszó generálása.")]
    public string GeneratePassword(
        [Description("Hossz (8-64)")] int length = 16,
        [Description("Tartalmazzon speciális karaktereket")] bool symbols = true)
    {
        length = Math.Clamp(length, 8, 64);
        var chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        if (symbols) chars += "!@#$%&*?";

        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)])
            .ToArray());
    }

    [McpServerTool(Name = "random_excuse"), Description("Fejlesztői kifogás, ha megint elcsúszott a határidő.")]
    public string RandomExcuse() =>
        Excuses[RandomNumberGenerator.GetInt32(Excuses.Length)];


    [McpServerTool(Name = "pick_one"), Description("Döntésképtelenség ellen: kiválaszt egyet a megadott lehetőségek közül.")]
    public string PickOne(
        [Description("Lehetőségek vesszővel elválasztva, pl.: pizza, gulyás, sushi")] string options)
    {
        var items = options.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return items.Length == 0
            ? "Adj meg legalább egy lehetőséget!"
            : items[RandomNumberGenerator.GetInt32(items.Length)];
    }
}
```

### Hogyan működik?

- **`[McpServerToolType]`**: ezzel jelölöd az osztályt, amelyben MCP-eszközök vannak. A `WithToolsFromAssembly()` ezeket találja meg.
- **`[McpServerTool(Name = "…")]`**: ezzel jelölöd az egyes metódusokat. A `Name` az eszköz azonosítója, amelyet az LLM lát.
- **`[Description("…")]`**: ez a **legfontosabb rész**. Az LLM ebből dönti el, mikor és hogyan használja az eszközt, ezért legyen pontos és egyértelmű. Ugyanez igaz a paraméterek leírására is.
- **Paraméterek és alapértékek**: a kliens ezekből kap JSON-sémát. Az alapértékek (pl. `sides = 6`) miatt a paraméter elhagyható.
- **`Math.Clamp`**: a bemenetet mindig validáld, mert az LLM bármilyen értéket küldhet.
- **`RandomNumberGenerator`**: kriptográfiailag biztonságos véletlen, ezért jelszógeneráláshoz is megfelelő. A jelszó-karakterkészletből szándékosan hiányoznak az összetéveszthető karakterek (`l`, `I`, `O`, `0`, `1`).

> **Megjegyzés:** a jelszógenerátor demonstrációs célú. Ne küldj érzékeny adatot LLM-en keresztül, és vedd figyelembe, hogy a generált jelszó megjelenik a beszélgetésben.

Indítsd újra a projektet, majd a 2.2 pont szerint ellenőrizd az Inspectorban, hogy az 5 eszköz megjelenik és működik.

---

## 5. Beállítás Claude-ban

Az MCP-szervert a Claude-ban **egyéni konnektorként (custom connector)** lehet hozzáadni. (A „plugin" helyett a Claude felületén ezt a nevet fogod látni.)

### 5.1. Konnektor hozzáadása

Előfeltétel: a projekt fut, a Dev Tunnel aktív és *Public*, az `/mcp` végpont az Inspectorban működik.

1. Nyisd meg a Claude-ot (claude.ai vagy az asztali alkalmazás), és menj a **Settings → Connectors** oldalra.
2. Kattints az **Add custom connector** gombra.
3. Add meg:
   - **Name:** `Fun Tools` (tetszőleges név)
   - **Remote MCP server URL:** `https://<a-te-tunneled>.devtunnels.ms/mcp`
   (Fontos, hogy az útvonal végén ott legyen az `/mcp`.)
4. **Add**.

> **Csapat- és vállalati csomagoknál (Team, Enterprise)** a konnektort először egy szervezeti tulajdonosnak kell hozzáadnia: *Organization settings → Connectors → Add → Custom → Web*. Utána a tagok a saját **Settings → Connectors** oldalukon tudnak csatlakozni hozzá. Hasonlóan, a ChatGPT-ben a beállítás a Developer mode bekapcsolása után az egyéni konnektorok (apps/connectors) között található; a pontos menüpontok változhatnak, ezért ott érdemes az aktuális dokumentációt követni.

### 5.2. Használat egy beszélgetésben

1. Nyiss új beszélgetést.
2. A beviteli mező melletti **+** (vagy az eszközök/konnektorok) menüben kapcsold be a **Fun Tools** konnektort.
3. Próbáld ki természetes nyelven:
   - „Dobj három hatoldalú kockával, és add össze az eredményt."
   - „Generálj egy 24 karakteres jelszót speciális karakterekkel."
   - „Nem tudunk dönteni: pizza, gulyás vagy sushi legyen?"
   - „Adj egy kifogást, miért késik a projekt."
4. Az első hívásnál a Claude jóváhagyást kérhet az eszköz használatához. Engedélyezd egyszer vagy mindig.
5. A válaszban látni fogod, melyik eszközt hívta meg a Claude, és milyen paraméterekkel.

### 5.3. Hibakeresés

| Probléma | Teendő |
|---|---|
| A konnektor hozzáadásakor hiba | Ellenőrizd az URL-t (`https`, `/mcp` a végén), és hogy a tunnel *Public* |
| Az eszközök nem jelennek meg | Indítsd újra a projektet, majd a konnektor beállításainál csatlakozz újra |
| Változtattál az eszközökön, de a Claude a régit látja | Töröld, majd add hozzá újra a konnektort, vagy kapcsold ki-be |
| A tunnel címe minden indításkor más | Használj *Persistent* tunnelt |
| A Claude nem használja az eszközt | Pontosítsd a `[Description]` szövegeket, vagy kérd meg kifejezetten az eszköz használatára |

---

## 6. Publikálás Azure App Service-be (opcionális)

A Dev Tunnels csak fejlesztéshez való. Tartós használathoz publikáld a szervert:

1. Solution Explorerben jobb klikk a projekten → **Publish…**
2. Target: **Azure → Azure App Service (Windows vagy Linux)**
3. Válassz meglévő App Service-t, vagy hozz létre újat (**Create new**).
4. **Publish**.
5. A kapott cím: `https://<appnév>.azurewebsites.net/mcp`. Ezt add meg a Claude-ban a konnektor URL-jeként a Dev Tunnels-cím helyett.

---

## 7. Saját eszköz hozzáadása (gyakorlás)

Egészítsd ki a `FunTools` osztályt egy saját eszközzel, például:

```csharp
[McpServerTool(Name = "get_server_time"), Description("A szerver aktuális ideje UTC-ben, ISO 8601 formátumban.")]
public string GetServerTime() => DateTime.UtcNow.ToString("O");
```

Indítsd újra az alkalmazást, frissítsd a konnektort a Claude-ban, és kérdezd meg: „Mennyi az idő a szerveren?"

További ötletek:

- Külső REST API hívása (időjárás, árfolyam) `HttpClient`-tel, DI-n keresztül
- Adatbázis-lekérdezés (csak olvasási joggal!)
- **Resources** vagy **Prompts** hozzáadása (`[McpServerResourceType]`, `[McpServerPromptType]`)

---

## 8. Éles használat – biztonság és jó gyakorlatok

- **Hitelesítés:** egy hitelesítés nélküli publikus MCP-végpontot bárki hívhat. Éles, érzékeny funkciókat kiszolgáló szervernél használj OAuth 2.1-alapú hitelesítést (a Claude támogatja az egyéni konnektorok OAuth-folyamatát).
- **Legkisebb jogosultság:** csak azt az adatot és műveletet add ki, amire tényleg szükség van. Az írási/törlési műveleteket külön gondold át.
- **Bemenet-ellenőrzés:** az LLM-től érkező paramétereket mindig kezeld nem megbízható adatként.
- **Prompt injection:** a külső forrásból (weboldal, e-mail) visszaadott szöveg tartalmazhat az LLM-nek szóló rejtett utasításokat. Ne adj olyan eszközt egy modellnek, amellyel ilyen utasítás nyomán kárt lehetne okozni jóváhagyás nélkül.
- **Naplózás és korlátozás:** logold a hívásokat, és alkalmazz rate limitet.
- **Jó leírások:** az eszköz neve és `Description`-je a „felhasználói felület" az LLM számára. Legyen rövid, egyértelmű, és írja le a paraméterek formátumát.
- **Tunnel higiénia:** a fejlesztés végén állítsd le vagy töröld a tunnelt, ne hagyd a gépedet feleslegesen publikusan elérhetően.

---

## Összefoglalás

1. ASP.NET Core Web API projekt létrehozása
2. Dev Tunnel beállítása (*Public*), tesztelés Inspectorral
3. `ModelContextProtocol.AspNetCore` telepítése, `AddMcpServer` + `MapMcp`
4. Eszközök megírása attribútumokkal és jó leírásokkal
5. Egyéni konnektor hozzáadása a Claude-ban az `/mcp` címmel
6. Publikálás Azure-ba, hitelesítés bevezetése éles használathoz
