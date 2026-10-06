# Tankönyvtámogatás – videók

## 1. Előkészítés

<video src="https://storage.altinum.hu/textbook/Textbook_1_Scaffold.m4v" controls preload="metadata" width="100%"></video>

## 2. A Server Explorer panel!

<video src="https://storage.altinum.hu/textbook/Textbook_2_Serverexplorer.m4v" controls preload="metadata" width="100%"></video>

## 3. Diákok listázása

<video src="https://storage.altinum.hu/textbook/Textbook_3_List_Students.m4v" controls preload="metadata" width="100%"></video>

``` csharp
private void GetStudents()
{
	var studentsFiltered = from x in context.Students.Local
						   where x.Name.Contains(textBoxStudentFilter.Text)
						   select x;

	studentBindingSource.DataSource = studentsFiltered.ToList();
}
```

## 4. Szűrés memóriából

A `context.Students.Local` használatával egyszer letölthetjük az adatokat a szerverrő, és a további szűréseket már a memóriából szolgáljuk ki.
Ezzel jelentősen csökkenthető a szerver terhelése.

<video src="https://storage.altinum.hu/textbook/Textbook_4_Local.m4v" controls preload="metadata" width="100%"></video>

## 5. Adatkötés szövegdobozra

<video src="https://storage.altinum.hu/textbook/Textbook_5_Textbox_Detebinding.m4v" controls preload="metadata" width="100%"></video>

## 6. Könyvek listázása

<video src="https://storage.altinum.hu/textbook/Textbook_6_Booklist.m4v" controls preload="metadata" width="100%"></video>

## 7. Master-detail: rendelések halgatónként

A feladat a kiválasztott hallgató rendeléseinek megjelenítése.

<video src="https://storage.altinum.hu/textbook/Textbook_7_Listorders.m4v" controls preload="metadata" width="100%"></video>

## 8. Megrendelés törlése

<video src="https://storage.altinum.hu/textbook/Textbook_8_Delete.m4v" controls preload="metadata" width="100%"></video>

## 9. Új megrendelés rögzítése

<video src="https://storage.altinum.hu/textbook/Textbook_9_Add.m4v" controls preload="metadata" width="100%"></video>
