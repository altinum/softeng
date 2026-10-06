
**Ez a dokumentum a `Validation` és `Validated` események valamint az `ErrorProvider` magyarázataként szolgál az előadáshoz, a megoldást feltölteni nem kell.**

A cél egy olyan felugró űrlap készítése, melyen keresztül a felhasználó adatokat vihet fel, majd az *OK* illetve *Mégse* gomb megnyomásával dönt a sorsukról -- azaz rögzíti az adatbázisban, vagy nem. Az űrlap csak akkor lehet bezárható az *OK* gombal, ha minden felvitt adat megfelel a formai követelményeknek.  

##  A "OK" és "Mégse" gombbal bezárt űrlapok közti különbségtétel

### From1

(+/-) Hozz létre egy új `Form`-ot `From2` néven!
(+/-) A `Form1`-en helyezz el egy gombot `Új hallgató` felirattal, majd a gombhoz rendelj eseménykiszolgálót. 
(+/-) Az eseménykiszolgálóban nyisd meg a `Form2`-t és attól függően, hogy a felhasználó melyik gombbal zárta be a `Form2`-t, jeleníts meg eltérő szöveget. (A bezáró gombok később készülnek majd.)

``` csharp
private void button1_Click(object sender, EventArgs e)
{
    Form2 f2 = new Form2();
    if (f2.ShowDialog() == DialogResult.OK)
    {
        MessageBox.Show("OK-val zárult");
    }
    else
    {
        MessageBox.Show("Nem OK-val zárult");
    }          
}
```

### From2
(+/-) Az űrlapon helyezd el a képnek megfelelő vezérlőket!

(+/-) Húzz az űrlapra még egy `ErrorProvider` vezérlőt is -- ez jelenít majd meg kis felkiáltójelet a hibásan kitöltött vezérlő (jelenleg csak TextBox) mellett. 

(+/-) Az *OK* gomb tulajdonságai között állítsd be az alábbiakat a *Prpoerties* panelen: `DialogResult`:`OK`. Innentől fogva, ha a felhasználó ezt a gombot nyomja meg, az űrlap automatikusan bezáródik, és a `Form1`-en a `ShowDialog()` metódus `DialogResult.OK` értékkel tér vissza.

(+/-) Az *Cancel* gomb tulajdonságai között állítsd be az alábbiakat a *Prpoerties* panelen: `DialogResult`:`Cancel`. Innentől fogva, ha a felhasználó ezt a gombot nyomja meg, az űrlap automatikusan bezáródik, és a `Form1`-en a `ShowDialog()` metódus `DialogResult.Cancel` értékkel tér vissza.

(+/-) Próbáld ki!

## Űralapon szereplő adatok ellenőrzése

(+/-) Ezután készítsd el az eseménykiszolgálóat mindkét `TextBox` `Validating` és `Validated` eseményeihez, és ellenőrizd, hogy *OK* gomb `CausesValidation` tulajdonsága az az alapértelmezett értéknek megfelelően `True`-e. 

A `Validating` esemény akkor következik be, ha valamilyen oknál fogva az érintett mezőben szereplő adatok érvényességének ellenőrzése szükségessé válik. Ilyen ok, ha a felhasználó egy olyan vezérlőre kattintott, aminek a `CausesValidation` tulajdonsága `True`.  Ilyen jelen pillanatban az *OK* gomb, de mindkét szövegdoboz is, mert  `CausesValidation` tulajdonságuk alapértelmezetten `True`. 

A *Mégse* gomb `CausesValidation` tulajdonságát viszont állítsd `False`-ra, különben később, amikor megírjuk az ellenőrzésekhez tartozó kódot, nem fogod tudni bezárni az ablakot, ha valamelyik validáció nem sikerül. Ez pedig bosszantó lehet, mert sokszor pont azért akarjuk bezárni az ablakot, mert rájövünk, hogy nincs meg minden adatunk a helyes kitöltéshez. 

A `Validated`  esemény akkor következik be, ha az ellenőrzés sikerrel zárult. Az osztály vélhetően most így néz ki:
``` csharp
public Form2()
    {
        InitializeComponent();
    }

    private void textBox1_Validating(object sender, CancelEventArgs e)
    {
    }

    private void textBox1_Validated(object sender, EventArgs e)
    {
    }

    private void textBox2_Validating(object sender, CancelEventArgs e)
    {
    }

    private void textBox2_Validated(object sender, EventArgs e)
    {
    }
}
```

(+/-) Próbáld ki, ha valamelyik `Validatig` eseményhez tartozó kiszolgálót az alábbiak szerint módosítod, nem lehet *OK* gombbal lezárni az űrlapot:

``` csharp
	private void textBox1_Validating(object sender, CancelEventArgs e)
	{
	        e.Cancel = true;
	}
```

(+/-) Az ellenőrzés megvalósításához készíts egy-egy függvényt a kétféle érték ellenőrzéséhez:

``` csharp
    private bool ValidNev(string név)
    {
        return !string.IsNullOrEmpty(név);
    }

    private bool ValidNeptun(string neptun)
    {
        Regex r = new Regex("^[A-Z0-9]{6}$");
        return r.IsMatch(neptun);
    }
```
(+/-) Majd a `Validating` eseményekhez tartozó kiszolgálókban ellenőrizd az értékeket:

``` csharp
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }

    private bool ValidNev(string név)
    {
        return !string.IsNullOrEmpty(név);
    }

    private bool ValidNeptun(string neptun)
    {
        Regex r = new Regex("^[A-Z0-9]{6}$");
        return r.IsMatch(neptun);
    }

    private void textBox1_Validating(object sender, CancelEventArgs e)
    {
        //Ha kell, megnézzük, érvényes-e a tartalom
        if (!ValidNev(textBox1.Text))
        {
            e.Cancel = true;
            errorProvider1.SetError(textBox1, "A név nem lehet üres");
        }
    }

    private void textBox1_Validated(object sender, EventArgs e)
    {
        //Sikerült a validáció
        errorProvider1.SetError(textBox1, "");
    }

    private void textBox2_Validating(object sender, CancelEventArgs e)
    {
        if (!ValidNeptun(textBox2.Text))
        {
            e.Cancel = true;
            errorProvider1.SetError(textBox2, "Érvénytelen Neptun kód");
        }
    }

    private void textBox2_Validated(object sender, EventArgs e)
    {
		//Ha üres stringre állítjuk a hiba szövegét, megszűnik a hiba jelölése is. 
        errorProvider1.SetError(textBox2, "");
    }
}
```

## ToolTip

A `ToolTip` segítségével a felhasználót segítő üzenetet lehet rendelni a vezérlőkhöz.

![1615051267868.png](GI/Gyakorlati%20feladatok/_④%20Adatok%20rögzítése%20validált%20űrlapon/1615051267868.png)

(+/-) Az űrlapon egyetlen `ToolTip` típusú objektumot érdemes elhelyezni, ez fogja kezelni az összes vezérlőhöz tartozó szöveget. A *Properties* panelben a megjelenés késleltetését, időtartamát, méretét, stb. egységesen a `toolTip1` tulajdonsági között lehet beállítani.  

![1615051616183.png](GI/Gyakorlati%20feladatok/_④%20Adatok%20rögzítése%20validált%20űrlapon/1615051616183.png)

(+/-) Ha van `ToolTip` az űrlapon, minden vezérlőhöz be lehet állítani a szöveget:

![1615051851465.png](GI/Gyakorlati%20feladatok/_④%20Adatok%20rögzítése%20validált%20űrlapon/1615051851465.png)

