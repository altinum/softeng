
## A "OK" és "Mégse" gombbal bezárt űrlapok közti különbségtétel

### From1

❶ Hozz létre egy új `Form`-ot `From2` néven!
❷ A `Form1`-en helyezz el egy gombot `Új hallgató` felirattal, majd a gombhoz rendelj eseménykiszolgálót. 
❸ Az eseménykiszolgálóban nyisd meg a `Form2`-t és attól függően, hogy a felhasználó melyik gombbal zárta be a `Form2`-t, jeleníts meg eltérő szöveget. (A bezáró gombok később készülnek majd.)

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


## Űrlap új diák rögzítéséhez

❹ Bedobunk az űrlapra egy `BindingSource`-ot, melynek az adatforrása Student típusú. Ha egyszerűen behúzod a tervezőről, `bindingSource1` lesz a neve, az adatforrását pedig a Properties ablakban tudod beállítani. 

❺ Ezután az osztályon belül létrehozunk egy tulajdonságot az alábbiak szerint. 

``` csharp
public partial class FormAddStudent : Form
{
	public Models.Student NewStudent { get; set; }

	public FormAddStudent()
	{
		InitializeComponent();
	}
	...

```

❻ Legyen ez a tulajdonság a binding source adatforrása:

``` c#
public partial class FormAddStudent : Form
{
	public Models.Student NewStudent { get; set; }

	public FormAddStudent()
	{
		InitializeComponent();
		bindingSource1.DataSource = NewStudent;
	}
    ...
```

❼ Ezután tervezzük meg az űrlapot a nevet és a Neptunkódot tartalmazó mezőkkel :

![1615051267868.png](GI/Gyakorlati%20feladatok/_④%20Adatok%20rögzítése%20validált%20űrlapon/1615051267868.png)

❽ A Properties ablakon keresztül a text boxok Text tulajdonságát összeköthetjük a BindingSource vonatkozó tulajdonságaival. Így a felhasználó által beírt szöveg az osztály `NewStudent` tulajdonságán keresztül lesz elérhető. 

❾ Valósítsuk meg a validációt is. 

``` csharp
public partial class FormAddStudent : Form
{
	public Models.Student NewStudent { get; set; }

	public FormAddStudent()
	{
		InitializeComponent();
		bindingSource1.DataSource = NewStudent;
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		Check();
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		Check();
	}

	void Check()
	{
		buttonOk.Enabled = true;

		if (string.IsNullOrEmpty(textBox1.Text))
		{
			textBox1.BackColor = Color.Salmon;
			buttonOk.Enabled = false;
		}
		else
		{
			textBox1.BackColor = Color.LightGreen;
		}

		//Neptn
		Regex r = new Regex(@"^[A-Z][A-Z0-9]{5}$");
		if (!r.IsMatch(textBox2.Text))
		{
			textBox2.BackColor = Color.Salmon;
			buttonOk.Enabled = false;
		}
		else
		{
			textBox2.BackColor = Color.LightGreen;                
		}

	}

	private void FormAddStudent_Load(object sender, EventArgs e)
	{
		Check();
	}
}
```


## Új diák rögzítése az űrlap adatai alapján

``` c#
private void buttonAdd_Click(object sender, EventArgs e)
{
	FormAddStudent fac = new FormAddStudent();
	if (fac.ShowDialog() == DialogResult.OK)
	{

		context.Students.Add(fac.NewStudent);
		context.SaveChanges();
		studentBindingSource.DataSource = context.Students.ToList();
	}

}
```

