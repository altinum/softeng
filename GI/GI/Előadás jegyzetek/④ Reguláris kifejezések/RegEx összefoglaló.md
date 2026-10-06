> [!note]
> A dokumentum fő célja az, hogy tudd, hogy létezik egy ilyen technológia, és ismerd a lehetőségeit. Szövegfeldolgozási és -ellenőrzési feladatoknál nem érdemes feltalálni a kereket. Az adatvezérelt világban pedig gyakoriak a szövegfeldolgozási feladatok. A nagy nyelvi modellek meglehetősen jók reguláris kifejezések írásában. 
## Mi az a RegEx?

Ez a szakasz az [itt](http://vbence.web.elte.hu/regex_leiras.html) található cikk kivonatolt változatára épül. (Már nem érhető el kívülről.) 

A **RegEx** vagy **RegExp** a **Regular Expression** rövid formája. Magyarul talán a "szabályos kifejezés" lenne a megfelelő fordítás. Ez a leírás azért született, hogy elindítson a technika megismerésében, de korántsem tekinthető teljesnek.

> "*Mindig babonás félelemmel néztem a RegEx mintáira. Volt dolgom egy-két nyelvvel, de ez valami egészen bizarr dolog volt. Nem hinném, hogy létezik olyan tapasztalat, aminek birtokában felfedezhető a RegEx sajátos logikája, szintaktikája, viszont az operátorok elolvasása után szinte arcul csap a felismerés: "ennyi az egész?" Legalább is ez lenne a cikk célja :)*"

A RegEx lehetőséget ad szabályok, azaz minták egyszerű leírására. Ezekkel a mintákkal aztán sok hasznos dolgot tehetünk. Kereshetünk rájuk egy string-ben, vagy kicserélhetjük őket valamilyen szabály szerint. Használhatjuk adatellenőrzésre vagy szerkezetek (pl. dátum) szétdarabolására, értelmezésére.


## RegEx operátorok

Mielőtt beépítenéd valamilyen alkalmazásba, online is ki lehet próbálni: 
https://www.w3schools.com/tools/tool_regex.php
### Karakter megfeleltetés

### Többszörözés
 jel|leírás 
-|-
**?**|A megelőző minta 0 vagy 1 alkalommal fordul elő: A  **borda.?**  kifejezés igaz a  **borda**  és a  **bordal**  szavakra is.
**+**|A megelőző minta 1 vagy több alkalommal fordul elő: A  **bo.+ka**  kifejezésnek megfelel a  **boróka**, a  **boka**  viszont  **nem**.
*|A megelőző minta 0 vagy több alkalommal fordul elő: A  **bo.*ka**  kifejezésnek már megfelel a  **boka**  és  **boróka**  is.
**{m,n}**|Segítségével megadható minimum és maximum vagy pontosan megadott számú előfordulás - {3} pontosan 3 előfordulás; {3,} legalább 3 előfordulás; {2,5} legalább 2 legfeljebb 5 előfordulás; {,10} legfeljebb 10 előfordulás. A d.{,5}ány igaz minden esetben, ha legfeljebb 5 karaktert kell helyettesíteni, például a dolmány esetén. A diszkópatkány viszont már nem akad fenn rajta.

### Horgonyok

Az előzőekben nem szemléltettem, de a felsorolt kifejezések akkor is igazak, ha a vizsgált string belsejében találhatók meg. A b.ka igaz a bikaviadal mintára is.

 jel|leírás 
-|-
^|A minta eleje: Ezzel jelezhetjük, hogy a kifejezést a minta elején keressük. A  **^béka**  kifejezésnek megfelel a  **békanyál**  minta, a  **kecskebéka**  viszont  **nem**.
$|A minta vége: Az előző horgonyhoz hasonlóan a minta végét testesíti meg. A  **ék$**  mintának megfelel minden erre végződő szó (**kerék**,  **pék**).

Természetesen kombinálhatók is. A  **^p.k$**  kifejezés csak akkor igaz, ha az input pontosan egy hárombetűs szó. A **legpikánsabb** nem megoldása, ahogy a **pikáns** sem. A  **pék**  vagy  **pók**  viszont jó.

### Logika

 jel|leírás 
-|-
**\|**|Vagylagos egyezés: Két lehetőség közé téve bármelyikkel való egyezés találatot produkál. Gyakorlati példához picit előre kell ugorjunk, a normál (kerek) zárójelekre, jelen felhasználás viszont nem kíván különösebb magyarázatot:  **ka(lap\|bát)**
**( )**|Kifejezések csoportosítása: Nem csak a vagylagos egyezés az egyetlen lehetséges felhasználás. Egy csoportot létrehozva elláthatjuk paraméterrel például a  **(hókusz)?pók**  segítségével a  **hókuszpók**  és  **pók**  szavak is megtalálhatók. A csoportokra később hivatkozhatunk is, ez cserénél vagy string-ek értelmezésénél lesz hasznos.

### Különleges szekvenciák
A `\` karaktert követő néhány betű különleges jelentéssel bír:

jel|jelentés |példa 
-|-|-
`\d`|Olyan egyezőt ad, amelyben a karakterlánc számjegyeket tartalmaz (számok 0-9-ig). 
`\D`| Egyezést ad, ahol a karakterlánc NEM tartalmaz számjegyeket.   
`\s` |Egyezést ad, ahol a karakterlánc szóköz karaktert tartalmaz.  
`\S` |Egyezést ad, ahol a karakterlánc NEM tartalmaz szóköz karaktert. 
`\w` |Egy egyezést ad, amelyben a karakterlánc bármilyen szókaraktert tartalmaz (karakterek a-tól Z-ig, számjegyek 0-9-ig, és az aláhúzás _ karakter) 
`\W` |Olyan egyezőt ad, amelyben a karakterlánc NEM tartalmaz szókaraktereket  

## Példák reguláris kifejezésekre

| Adattípus                                     | Példa                      | Regex                                      |
| --------------------------------------------- | -------------------------- | ------------------------------------------ |
| **Neptun-kód**                                | `A1B2C3`                   | `^[A-Z0-9]{6}$`                            |
| **Neptun-kód**                                | `12AB34`                   | `^[A-Z0-9]{6}$`                            |
| **Személyi igazolvány száma**                 | `AB123456`                 | `^[A-Z]{2}[0-9]{6}$`                       |
| **Személyi igazolvány száma**                 | `123456AB`                 | `^[0-9]{6}[A-Z]{2}$`                       |
| **Személyi igazolvány száma – mindkét forma** | `AB123456` vagy `123456AB` | `^([A-Z]{2}[0-9]{6})\|([0-9]{6}[A-Z]{2})$` |
| **Neptun-kód**                                | `ABC123`                   | `^[A-Z]{3}[0-9]{3}$`                       |
| **Magyar személyi szám**                      | `123456AB`                 | `^[0-9]{6}[A-Z]{2}$`                       |
| **Személyi igazolvány száma**                 | `123456AB`                 | `^[0-9]{6}[A-Z]{2}$`                       |
| **Irányítószám**                              | `1051`                     | `^[0-9]{4}$`                               |
| **Magyar telefonszám**                        | `+36301234567`             | `^\+36[0-9]{9}$`                           |
| **E-mail-cím**                                | `valaki@example.com`       | `^[^@\s]+@[^@\s]+\.[^@\s]+$`               |
| **Magyar rendszám (régi)**                    | `ABC-123`                  | `^[A-Z]{3}-[0-9]{3}$`                      |
| **Dátum**                                     | `2026-10-05`               | `^[0-9]{4}-[0-9]{2}-[0-9]{2}$`             |
| **Egyszerű egész szám**                       | `-12345`                   | `^-?[0-9]+$`                               |
| **Pozitív egész szám**                        | `12345`                    | `^[0-9]+$`                                 |
| **Hexadecimális szám**                        | `7FA2`                     | `^[0-9A-Fa-f]+$`                           |
| **IPv4-cím – egyszerű változat**              | `192.168.1.10`             | `^[0-9]{1,3}(\.[0-9]{1,3}){3}$`            |

## Mire jó ez az egész a gyakorlatban? 

| Terület                                | Mire használjuk?                                   | Példa                                                                  |
| -------------------------------------- | -------------------------------------------------- | ---------------------------------------------------------------------- |
| **Űrlapok validálása**                 | Adatok formátumának ellenőrzése                    | e-mail-cím, telefonszám, irányítószám, Neptun-kód, rendszám, termékkód |
| **Jelszóellenőrzés**                   | Egyszerű formai követelmények vizsgálata           | legalább 8 karakter, szám és nagybetű                                  |
| **Keresés szövegben**                  | Összetett minták megtalálása                       | telefonszámok vagy e-mail-címek keresése egy szövegben                 |
| **Logfájlok elemzése**                 | Hibák, IP-címek, időpontok, HTTP-kérések kinyerése | `ERROR`, `404`, IP-címek                                               |
| **Adattisztítás**                      | Hibás vagy eltérő formátumú adatok felismerése     | `06-30-123-4567` → telefonszámok egységesítése                         |
| **CSV / szöveges adatok feldolgozása** | Adatok felismerése és kinyerése                    | azonosítók, dátumok, kódok                                             |
| **Fájlok átnevezése**                  | Fájlnevek csoportosítása                           | `IMG_20261005_1234.jpg`                                                |
| **Webfejlesztés**                      | HTML/URL/űrlapadatok feldolgozása                  | URL-ek, query paraméterek                                              |
| **E-mail feldolgozás**                 | Címek vagy tárgyak felismerése                     | `From:`, e-mail-címek                                                  |
| **IDE-k / szövegszerkesztők**          | Keresés és tömeges csere                           | `Ctrl+H` + regex                                                       |
| **Adatbázis-exportok**                 | Strukturálatlan szövegből adatok kinyerése         | naplózott tranzakciók                                                  |
| **DevOps / rendszergazdai munka**      | Konfigurációk és naplók vizsgálata                 | Linux logok, webserver-logok                                           |

És végezetül nagyon fontos: az MSSQL Szerveren 2025 óta lekérdezéseknél is használható:

``` SQL
SELECT *
FROM Hallgato
WHERE REGEXP_LIKE(NeptunKod, '^[A-Z0-9]{6}$');
```

## Reguláris kifejezések Pythonban

> [!note]
> Ez a rész csak egy visszatekintés: példa arra, hogy az előző félévben tanult Python nyelven hogyan működnek a dolgok. 

### Egyszerű elfogadás / elvetés

``` python
import re
text =  "Budapesti Corvinus Egyetem"
talalat = re.match("Bud", text)
print(talalat)

if talalat: # inkább így használjuk
print("Van benne Bud")
```
```
<re.Match object; span=(0, 3), match='Bud'> 
Van benne Bud
```
### Csoportok kezelése

#### 1. példa:

``` python
text =  "Kapcsos zárójelekben van egy <első>, egy <második> és egy <harmadik> érték."
talalat = re.search("<([^<>]*)>", text)

print(talalat.group(0))
```
```
<első>
```
#### 2. példa:

``` python
text =  "Kapcsos zárójelekben van egy <első>, egy <második> és egy <harmadik> érték."
talalat = re.search("<([^<>]*)>.*<([^<>]*)>.*<([^<>]*)>", text)

print(talalat.groups())
``` 
``` 
('első', 'második', 'harmadik')
``` 

#### 3. példa:


``` python
text =  "Kacsacsőrben van egy <első>, egy <második> és egy <harmadik> érték."
talalat = re.findall("<[^<>]*>", text)

print(talalat)
``` 
``` 
['<első>', '<második>', '<harmadik>']
``` 

### Csere

Mindent, ami nem az angol abc kis- vagy nagybetűje vagy nem szám, betűközre cserélünk. 

``` python
def  remove_punctuation(s):
	return re.sub('[^A-Za-z0-9]+', ' ', s) 
```
Kipróbálva:
``` python
a = remove_punctuation("Ez + ez = az ő.")
print(a)
```
```
'Ez ez az '
```
Így már megy majd ékezetre is:

``` python
def  remove_punctuation(s):
	return re.sub('[^\w]+', ' ', s) #használjuk \w-t
```

# Feladatok

> [!important]
> Ezeket a feladatokat csak gyakorlásra szántuk érdeklődőknek. 

❶ Adott az alábbi karakterlánc. Nyerd ki belőle a dátum és az idő elemeit, és építs `datetime.datetime` objektumot!
``` python
text = "2020-03-22 01:10:47.707 [Information] [000453][840b55d0619f3cd9260a9132a7c1a5c1;adatb/gyak01/;2] CLASSROOM: #840b55d0619f3cd9260a9132a7c1a5c1 just joined classroom (adatb/gyak01/) -> 2 user(s) online"
```

❷ Adott az fenti karakterlánc. Nyerd ki belőle a # és a betűköz karakter közti számot!

❸ Írj függvényt, ami eltávolítja a dupla betűközöket! 