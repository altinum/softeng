# Esettanulmány

## A feladat célja és lépései

Az első ZH egy adatbázis-tervezési feladatot is tartalmaz. Ez az esettanulmány egyben felkészülés a zárthelyire is.

1. Készítsd el adatbázistervet Markdownban, Mermaid erDiagram formájában. 

2. Tetszőleges nyelvi modell segítségével alakítsd T-SQL-scriptté a Mermaid-diagramot.

3. Hozz létre Azure-ban egy SQL-adatbázist!

4. Az adatbázisban hozza létre az SQL-szkript alapján a sémát.

5. LLM segítségével generálj mintaadatokat az adatbázisba.

> [!IMPORTANT]
>
> Nagyon figyelj arra, hogy az Urbanban fogadd el a korlátozott funkciókkal bíró ingyenes oktatási adatbázist! Az alapértelmezett beállításokkal egy dedikált SQL-szervered lesz nagy memóriával, háttértárral és CPU-teljesítménnyel, ami rövid időn belül elfogyasztja a rendelkezésedre álló, évi száz dolláros diák keretedet.

## Kontextus

Egy vállalat számlázási rendszert szeretne bevezetni az ügyfelek számlázásának kezelésére és a nyújtott szolgáltatásokért fizetett összegek nyomon követésére. A rendszernek több ügyfelet kell kezelnie, akik mindegyike különböző szolgáltatásokért kaphat számlát. Ezenkívül a rendszernek kezelnie kell a fizetési nyilvántartásokat és az esetleges adókat vagy kedvezményeket. 

## Részletek

A vállalat különböző **típusú** **szolgáltatásokat** kínál, mindegyik önálló *egységáron* kerül értékesítésre. Ezeket a **szolgáltatásokat** lehet eladni az **ügyfeleknek** egy **számla** részeként, annak kiállításával. Minden **számlán** fel kell tüntetni az alábbi adatokat*: a kiállítás dátuma*, *a fizetendő összeg*, valamint *a fizetési határidő* és a fizetendő végösszegre adott *kedvezmény*. Egy **számla** több **számlatételt** is tartalmazhat, amelyekben egy-egy **szolgáltatás** szerepelhet. A számlatételekben szerepelnek továbbá az az esetleges *adók,* a *mennyiség* és az aktuális *egységár*.

Az **ügyfeleknek** több **számlájuk** is lehet, és a rendszernek nyomon kell követnie az egyes **ügyfelek** *fennálló egyenlegét* és **fizetési** előzményeit. A **fizetések** részben vagy teljes egészében is teljesíthetők, és a rendszernek ennek megfelelően kell módosítania a kapcsolódó számla *állapotát:* A **számlák** *kifizetettként* jelölhetők meg, miután az **ügyfél** teljesítette a **fizetést**. 

A rendszernek kezelnie kell a **számlázásért** és a **fizetéseket** kezelő vállalati **alkalmazottakat** is. Az **alkalmazottak** számlákat állíthatnak ki, illetve rögzíthetik a **fizetéseket**. A rendszernek nyomon kell követnie az egyes **számlákat** *kiállítóját* és  **fizetések** *rögzítőjét*. 

Bizonyos esetekben az **ügyfelek** előfizethetnek s**zolgáltatásokra** bizonyos gyakorisággal (pl. havi előfizetések), és a rendszernek képesnek kell lennie ezeknek az ismétlődő számláknak kiállításának támogatására.

## Leadás 

 Az elkészült adatbázisterveket Moodle-ban kell leadni a hetedik heti előadásig. 

