
Tervezz relációs adatbázist egy városi és elővárosi közösségi közlekedési rendszer adatainak tárolására. Tervet MarkDown `erDiagram` formájában készítsd el. Dolgozzatok párokban!

## A feladat háttere

A gyakorlatokon előforduló feladatok kapcsán érezhetitek úgy, hogy a feladatok a valóság leegyszerűsített változatát tárják elénk. A mai feladat nem ilyen. A gyakorlat végén meg tudom mutatni a valódi budapesti adatbázist, valós, naprakész adatokkal feltöltve. 

## A feladat

Az adatbázisnak az alábbi követelményeket kell kielégítenie.

#### 1. Közlekedési szolgáltatók

A rendszerben több közlekedési szolgáltató adatai tárolhatók. Egy szolgáltatóhoz tartozzon:

- név,
- weboldal,

Elképzelhető, hogy a városban bizonyos járatokat más-más szolgáltatók üzemeltetnek. Budapesten nem ez a helyzet, de például Londonban a metróvonalak régen különböző társaságok tulajdonában álltak. 

#### 2. Megállóhelyek

A közlekedési hálózat megállóhelyekből áll. Minden megállóhelyhez tárolni kell:

- nevet,
- földrajzi szélességet és hosszúságot,
- opcionális egyedi azonosítót,
- Egy megállóhely opcionálisan egy másik, magasabb szintű megállóhelyhez tartozhat. Ez lehetővé teszi olyan közlekedési csomópontok modellezését, amelyek több, fizikailag vagy funkcionálisan elkülönülő megállóhelyből állnak. A kapcsolat önmagára hivatkozó kapcsolat legyen.

Például egy nagyobb közlekedési csomópont esetén:

```
Deák Ferenc tér
├── Deák Ferenc tér M1
├── Deák Ferenc tér M2
├── Deák Ferenc tér M3
└── Deák Ferenc tér – buszmegálló
```

Ilyenkor a kisebb, fizikailag vagy logikailag elkülönített megállóhelyek egy közös **szülőmegállóhelyhez** tartozhatnak.

>A megállóhelyek között hierarchikus kapcsolat is előfordulhat. Egy megállóhely egy nagyobb közlekedési csomópont vagy megállóhely része lehet.

#### 4. Járatok / viszonylatok (`routes`)

A *járat* és a *viszonylat* szavakat egymás szinonimájaként használjuk az angol *route* megfelelőjeként. A `route` egy **utazási viszonylatot** ír le, nem egy konkrét indulást. Például a `7` vagy a `49` egy-egy járat. 

Minden járathoz tárolni kell:
- Az adott járatot üzemeltető szolgáltató azonosítója.
- Rövid, utasok számára megjelenő azonosító, pl. `7`, `M2`, `100E`.
- A járat hosszabb neve, tipikusan a végpontok vagy útvonal megnevezése. A `7`-es járatnál pl. `Újpalota, Nyírpalota út – Albertfalva vasútállomás`
- A közlekedési mód típusa, pl. busz, villamos, metró, vasút.
- Térképen történő megjelenítéshez használható szín.
#### 5. Egy konkrét járatmenet, vagy menet  (`trip` )

Minden járat, vagy `trip` már egy **konkrét szolgáltatási menetet** reprezentál.

Például:

```
7-es járat
  ├── 07:03 Albertfalva vasútállomás
  ├── 07:13 Újpalota, Nyírpalota út
  ├── 07:23 Albertfalva vasútállomás
  └── ...
```

Mindegyik egy-egy `trip`. 

- Minden menet vagy járatmenet (`trip`) egy járathoz (`route`) tartozik. 
- Minden menethez tartozik egy indulási időpont. Pl. `07:23`
- A meneteknek van egy úgynevezett `headsign`-je is. Pl. - `Albertfalva vasútállomás` vagy az ellenkező irányban `Újpalota, Nyírpalota út`
- Szükség van egy `direction_id` technikai mezőre is, amivel meg lehet különböztetni az irányokat. A gyakorlatban ez `0` vagy `1` értéket szokott felvenni, nincs túlbonyolítva. 

#### 6. Megállások időpontjai (`stop_times`)

Fontos, hogy a megállások időpontjai a menetrendben az egyes járatmenetekhez (`trip`) tartoznak, nem pedig a viszonylatokhoz (`route`).  

Egy különösen szemléletes eset a **hurokban közlekedő 276E**. A járat Rákoscsaba-újtelep környékén hurokban közlekedik, és a két irányban nem azonos útvonalon halad. 

- Ezért külön érdemes tárolni azt, hogy melyik menet (`trip`) melyik megállót (`stop`) milyen időpontban érinti. 
- A megállóba érkezés, és az indulás időpontját is kell tárolni. 
- A megállók neveit és földrajzi koordinátáit természetesen nem akarjuk redundánsan tárolni. 

Példa a viszonylat (`route`), a járat (`trip`), a megállók (`stop`)  és a megállási időpontok (`stop_times`) kapcsolatára: 

```
7-es járat
│
├── trip 07:23
│     direction_id = 0
│     headsign = "Albertfalva vasútállomás"
│     │
│     ├── Újpalota, Nyírpalota út       07:23
│     ├── Vásárcsarnok                  07:25
│     ├── Fő tér                        07:27
│     ├── Molnár Viktor utca            07:29
│     ├── Bosnyák tér                   07:33
│     ├── Keleti pályaudvar M           07:43
│     ├── Blaha Lujza tér M             07:46
│     ├── Astoria M                     07:49
│     ├── Ferenciek tere M              07:52
│     ├── Szent Gellért tér             08:01
│     └── Albertfalva vasútállomás      08:15
│
├── trip 07:33
│     direction_id = 0
│     headsign = "Albertfalva vasútállomás"
│     │
│     ├── Újpalota, Nyírpalota út       07:33
│     ├── Vásárcsarnok                  07:35
│     ├── Fő tér                        07:37
│     ├── Molnár Viktor utca            07:39
│     ├── Bosnyák tér                   07:43
│     ├── Keleti pályaudvar M           07:53
│     └── ...
│
└── trip 07:20
      direction_id = 1
      headsign = "Újpalota, Nyírpalota út"
      │
      ├── Albertfalva vasútállomás      07:20
      ├── ...
      ├── Szent Gellért tér             07:34
      ├── Ferenciek tere M              07:43
      ├── Astoria M                     07:46
      ├── Blaha Lujza tér M             07:49
      ├── Keleti pályaudvar M           07:52
      ├── Bosnyák tér                   08:02
      └── Újpalota, Nyírpalota út       08:12
```

