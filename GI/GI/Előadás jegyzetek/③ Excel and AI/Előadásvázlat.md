
## Adatok beolvasása Power Query-vel

Power Query használata: Adatbázis tábla beolvasása a Power Query segítségével. 

## Táblából Spill Array

**1. A betöltött táblázat (Table) nevének megadása**

Ha a Power Query eredményt táblázatként töltötted be:

1. Kattints bármelyik cellájába a betöltött táblázatnak.
2. A menüszalagon megjelenik a **Táblázateszközök → Tervezés** fül (angolul: _Table Design_).
3. Bal oldalon a **Táblázat neve** mezőbe írd be az új nevet, pl. `tblAdat`, és nyomj Entert.

A nevet szóköz nélkül add meg (betűvel kezdődjön, aláhúzás használható). Ezután a `=tblAdat` képlet az egész táblát kiteríti, és frissítéskor automatikusan igazodik: ha több lett a sor, a spill is hosszabb lesz, ha kevesebb, rövidebb.

Ha csak az adatsorokat akarod, fejléc nélkül, akkor `=tblAdat[#Adatok]` (angolul `tblAdat[#Data]`). A fejléccel együtt: `=tblAdat[#Összes]` (`tblAdat[#All]`).


## Dinamikus tömbök kezelése:

``` excel
={1,2;3,4}
CHOOSECOLS, CHOOSEROWS  
INDEX  
LET  
LAMBDA  
XLOOKUP  
HSTACK  
SORTBY  
UNIQUE  
FILTER
```

`= SEQUENCE()`
`= {1,2;3,4}` Azért fontos, mert kódból generált Excel táblánál lehet egyetlen string a cellában

Adattáblák kezelése, oszlopok kiolvasása:
``` excel
=categories[#all]
=categories[category_name]
```

Dinamikus tömbök oszlopainak, sorainak, illetve egyes celláinak kiválasztása: 
``` excel
=CHOOSECOLS(A1#,2)
=CHOOSECOLS(A1#,2,3)
=INDEX(A1#,,1)
=INDEX(A1#,1,2)
```

Táblák összekapcsolása:
``` excel
XLOOKUP
```

Bonyolultabb kifejezések és a `LET`:
``` excel
=LET(
ár, CHOOSECOLS(A1,6))
ár * 1.27
```

Saját függvények definiálása:
``` excel
LAMBDA
```

Táblák rendezése, szűrése:
``` excel
SORT
FILTER
```

`GROUP BY`-szerű csoportosítás és csoportonkénti összegzés:
``` excel
UNIQUE
SUMIFS
```


## JSON beolvasása Power Query-vel WebAPI-ból. 

Olvassuk be, és jelenítsük meg táblázatos formában az alábbi linken szereplő, CSV formátumú munkaszüneti napokat tartalmazó táblázatot:

https://date.nager.at/api/v3/PublicHolidays/2026/HU
