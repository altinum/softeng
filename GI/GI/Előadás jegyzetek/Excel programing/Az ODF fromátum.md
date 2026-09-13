
Az ODF (Open Document Format) formátum (eredetileg: OASIS Open Document Format for Office Applications) egy nyílt formátumú _de facto_ formátum irodai programcsomagok dokumentumai számára. 2005 májusában jelent meg a szabvány, és már ugyanazon évben ez lett az OpenOffice.org 2.0 alapértelemzett formátuma.

A Microsoft Office programcsomag 2007-ig az eredeti OLE CF (Object Linking and Embedding Compound File) formátumát használta. Ez a komplex fájlstruktúra, és annak kiemelkedő előnyei tették annak idején az Office programcsomagot vezető irodai szoftverré.

Az eredeti office formátum egyik komoly újítása volt például a [gyors mentés][2] funkció. A kezdetekben jelentős erőforrásokat emésztett fel a nagyobb dokumentumok mentése a merevlemezre, ezért a Microsoft azt találta ki, hogy a verziókövetéshez hasonlóan csak deltákat (változásokat) tárol el a fájlban. A változások úgy 10-15 mentésig tárolódtak az egyedi tartalmak mellett, és ha elég sok gyűlt össze, akkor a következő mentésnél ténylegesen elmentődött az aktuális állapot. Ezzel néhány pillantra tudták gyorsítani az egyébként több másodperce mentési procedúrát. A megoldásban rejlő biztonsági kockázat miatt azonban 2003-ban megszűntették ezt a funkciót.

A Microsoft Office fájlformátumok nagy újítása 2007-ben történt meg. Ekkor jelentek meg a korábbi formátum megtartása mellet az új **x** végződésű (pl.: docx, xlsx) fájlkiterjsztések, melyek már ODF formátumon alapulnak. Az alap ODF formátum lényege, hogy valójában egy olyan tömörített (zip) állományt rejt el a felhasználó elől, amiben egy komplett fájlstruktúra található.

> Egy sima ODF formátumú fájl kiterjesztését átírva zip-re, megtekinthető a "mappa" tartalma. A kezdeti időkben ezzel a trükkel a fájlok jelszóvédelmét megkerülve így ki lehetett nyerni például a dokumentumban található ábrákat.

A Microsoft formátuma kibővíti az alap ODF fájlstruktúrát. Új mappákat és funkciókat vezet be, hogy az Office programcsomag egyedi megoldásait is elérhetővé tegye. A formátumokból adódóan egy Microsoft Office fájl viszonylag könnyen konvertálható ODF formátumra, a fordított irány viszont már nem triviális.

Ezek fényében alapvetően három féle megközelítés létezik a Microsoft Office, és ezen belül az Excel állományok kódból történő létrehozására:

