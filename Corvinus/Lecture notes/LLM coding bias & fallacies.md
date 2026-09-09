**Bias:** _„Az agyam bizonyos módon torzítja az információ feldolgozását.”_  
**Fallacy:** _„Az érvelésem szerkezetileg hibás.”_

| #      | Torzítás / hiba                               | Mit jelent?                                                                                              | LLM-es példa                                                                                                                      |
| ------ | --------------------------------------------- | -------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| **1**  | **Salience bias** – kiemelkedőségi torzítás   | A feltűnő előnyöket túlértékeljük, a kevésbé látható költségeket alulértékeljük.                         | „Ezt a 300 soros modult 5 perc alatt megírta.” → Nem számoljuk hozzá a review, tesztelés és későbbi hibakeresés költségét.        |
| **2**  | **Automation bias** – automatizálási torzítás | Hajlamosak vagyunk elfogadni egy automatizált rendszer döntését anélkül, hogy megfelelően ellenőriznénk. | „A Copilot/ChatGPT generálta, biztosan jó.” → A kód bekerül review nélkül.                                                        |
| **3**  | **Omission bias** – mulasztási torzítás       | A nem cselekvést kevésbé érezzük kockázatosnak, mint az aktív cselekvést.                                | „Most nem írunk unit teszteket, majd később.” → A tesztelés elmaradása kevésbé tűnik veszélyesnek, mint egy hibás teszt megírása. |
| **4**  | **Optimism bias** – optimizmus-torzítás       | Alábecsüljük a negatív kimenetelek valószínűségét.                                                       | „Ez egyszerű CRUD kód, nem nagyon tud hibás lenni.”                                                                               |
| **5**  | **Confirmation bias** – megerősítési torzítás | Azt az információt keressük, amely igazolja az előzetes elképzelésünket.                                 | Az első öt teszteset működik → „Jó a kód.” A ritka edge case-eket már nem vizsgáljuk.                                             |
| **6**  | **Halo effect** – holdudvarhatás              | Egy pozitív tulajdonság miatt más tulajdonságokat is pozitívan ítélünk meg.                              | „A GPT nagyon jól magyarázza a kódot, tehát maga a kód is jó minőségű.”                                                           |
| **7**  | **Authority bias** – tekintélytorzítás        | Egy tekintélyesnek érzett forrás állítását kritika nélkül elfogadjuk.                                    | „Az AI ezt a design patternt javasolta, tehát ez a helyes megoldás.”<br><br>Elon Musk és a Mars kolonizációja                     |
| **8**  | **Focusing effect** – fókuszhatás             | Egyetlen szempont túl nagy súlyt kap a döntésben.                                                        | „Az AI 10× gyorsabb.” → A döntésnél csak a fejlesztési sebességet nézzük, nem a teljes lifecycle költségét.                       |
| **9**  | **Planning fallacy** – tervezési téveszme     | Alábecsüljük a feladat teljes idejét és költségét.                                                       | „AI-val ezt délutánra megcsináljuk.” → A valóságban két nap megy el a generált kód megértésére és javítására.                     |
| **10** | **Dunning–Kruger effect**                     | Kevés tudással nehéz felismerni, hogy mit nem tudunk.                                                    | Junior fejlesztő elfogad egy elegáns, de hibás concurrency-megoldást, mert nem látja, mi benne a probléma.                        |

> „Nem megyek el rákszűrésre, mert ha nem vizsgáltatom meg magam, legalább nem tudom meg, hogy beteg vagyok.”


Itt az illető **az információ megszerzését is elkerüli**, mert a pozitív diagnózis pszichológiai terhét nagyobbnak érzi, mint annak előnyét, hogy időben kiderülhet a betegség.

> „Nem nézem meg, mennyibe került összesen, mert akkor nem tudom tovább azt mondani, hogy jó vétel volt.”


Ez több jelenség metszete:

- **Omission bias** – _„Ha nem teszek semmit, legalább nem én okozom a rossz következményt.”_
- **Information avoidance / információkerülés** – szándékosan elkerüljük a számunkra kellemetlen vagy fenyegető információ megszerzését.
- **Anticipated regret** – előre tartunk attól, hogy később megbánnánk, ha megtudnánk a rossz hírt.
- **Optimism bias** – _„Valószínűleg nincs semmi bajom.”_
- **Status quo bias** – _„Most jól vagyok, minek változtatni ezen?”_

A **legpontosabb megnevezés szerintem az _information avoidance_ (információkerülés)**.

A gondolkodás lényege:

> **„A tudatlanságot választom, mert maga a negatív információ rosszabbnak tűnik számomra, mint annak a kockázata, amit az információ hiánya jelent.”**

Ez egy nagyon érdekes párja az előző LLM-es példádnak is:

**LLM-kód:**

> „Nem tesztelem → nem találok hibát → nincs probléma.”

**Szűrés:**

> „Nem vizsgáltatom meg → nem kapok pozitív diagnózist → nincs rossz hír.”

Mindkettőben megjelenik az **információkerülés**, csak az elsőnél a hibáról, a másodiknál az egészségi állapotról szóló információt kerüljük.



Szép és okos vagy gazdag lányokat kérdezd le egy adatbázisból. 