Suppose we have a dataset saved in a CSV file that contains the energy consumption of our household appliances. It looks something like this:

```
Date;Appliance;HoursUsed;PowerWatts;EnergyKWh
2026-09-01;Refrigerator;24.0;120;2.88
2026-09-01;Television;3.5;100;0.35
2026-09-01;Washing Machine;1.2;2100;2.52
2026-09-01;Dishwasher;1.8;1800;3.24
2026-09-01;Laptop;6.5;65;0.42
```

The first line is the header; we don’t need to deal with it. 

Let’s create an application that reads the CSV file into memory and displays it in tabular form on the form. 

## Step 0: Create a Windows Forms application. 

Just keep a few things in mind:
- Do not use a network drive to store your project. 
- Do not start the project name with a number. 
- I will use `ElecticityUsage` in this example.
- Using `PascalCase` for project names is polite. 

## Step 1: Create a class that describes a row in the data table

➊ Create a new class in a Windows Forms project:

1. In **Solution Explorer**, right-click the project where you want to create the class.
2. Select **Add → Class...**
3. Enter a name for the class, for example `PowerUsage.cs`.
4. Click **Add**.
5. Visual Studio creates a new C# class file containing the basic class definition:

``` csharp
internal class PowerUsage
{
}
```

❷ Don't forget to change the `internal` access modifier to `public`. If the excess modifier is not `public`, the data will not be displayed later in the data grid view.  

``` csharp
public class PowerUsage
{
}
```

❸ Now let's append a class with some properties that represent fields in a CSV file. You can use the _prop-tab-tab_ code snippet to make your life easier. I suggest something like this:

``` csharp
public class PowerUsage
{
	public DateTime Date { get; set; }
	public string Appliance { get; set; } 
	public double HoursUsed { get; set; } 
	public int PowerWatts { get; set; } 
	public double EnergyKWh { get; set; }
}
```

We have not yet mentioned the `DateTime`, but you will find out later how to use it :)

The properties correspond to the columns in the CSV file:

| CSV column   | C# property  | Data type  |
| ------------ | ------------ | ---------- |
| `Date`       | `Date`       | `DateTime` |
| `Appliance`  | `Appliance`  | `string`   |
| `HoursUsed`  | `HoursUsed`  | `double`   |
| `PowerWatts` | `PowerWatts` | `int`      |
| `EnergyKWh`  | `EnergyKWh`  | `double`   |

For example, one row from the CSV:

```
2026-09-01;Refrigerator;24.0;120;2.88
```

can be represented by the following C# object:

``` csharp
EnergyUsage usage = new EnergyUsage();

usage.Date = new DateTime(2026, 9, 1),
usage.Appliance = "Refrigerator",
usage.HoursUsed = 24.0,
usage.PowerWatts = 120,
usage.EnergyKWh = 2.88
```

> [!tip]
> The code above is just an example. You don't have to type it anywhere. 

This class can **later** be used to store all 100 rows of the CSV file in a `List<EnergyUsage>` and perform various data analysis operations.

> [!note]
> It doesn’t matter whether we read our data from a CSV file or an SQL database; we’ll need a class for each table to map its rows. With databases, life will be a little easier because we can have these classes generated automatically. 


# Step 2: Create a list that holds data in memory

Now go back to the code behind of Form1. You will see something like this:

``` csharp
namespace ElecticityUsage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}
```

Now let's create a list that can hold lines of the CSV file.  Create it at class level, since you want all the methods within the class to be able to access it:

``` csharp
namespace ElecticityUsage
{
    public partial class Form1 : Form
    {
        List<PowerUsage> powerUsages = new List<PowerUsage>();
        public Form1()
        {
            InitializeComponent();
        }
    }
}
```

When creating a list, you must specify what type of items it will contain. It’s good practice name your list in camel case and use the plural form of the type.

## Step 3: Parse your CSV file

There are several prebuilt libraries for reading CSV files, but we’re going to show a rather crude solution. This is also an example of processing and chunking text. 

❶ Download the CSV file and save it somewhere you can easily find. I recommend the `C:\temp` folder. 

![[power_usage.csv]]

❷ Create an event handler for the form’s Load event. 

``` c#
namespace ElecticityUsage
{
    public partial class Form1 : Form
    {
        List<PowerUsage> powerUsages = new List<PowerUsage>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
```

> [!important]
> Just copy-pasting the code above won’t work, since the event handler won’t be connected to the actual event. Just double-click the form in the designer or assign the event to the event handler using the Properties window!

❸ Read data from the file

First, we just provide the code example, then explain it line by line:

``` csharp
private void Form1_Load(object sender, EventArgs e)
{
    string[] lines = File.ReadAllLines("c:\\temp\\power_usage.csv");

    // Az első sor a fejléc
    for (int i = 1; i < lines.Length; i++)
    {
        string[] parts = lines[i].Split(';');

        PowerUsage pu = new PowerUsage();

		pu.Date = DateTime.Parse(parts[0]);
		pu.Appliance = parts[1];
		pu.HoursUsed = double.Parse(parts[2]);
		pu.PowerWatts = int.Parse(parts[3]);
		pu.EnergyKWh = double.Parse(parts[4]);

        powerUsages.Add(powerUsage);
    }
}
```

### Breaking it down

The following line reads all lines from a text file and stores them in an array:

``` csharp
string[] lines = File.ReadAllLines(@"C:\temp\power_usage.csv");
```

`File.ReadAllLines()` is a method from the **`System.IO`** namespace.

It:

- opens the specified text file,
- reads all lines from the file,
- closes the file automatically,
- and returns the lines as a `string[]` array.

For example, if the file contains:

```
Date;Appliance;HoursUsed;PowerWatts;EnergyKWh
2026-09-01;Refrigerator;24.0;120;2.88
2026-09-01;Television;3.5;100;0.35
```

then:

```
string[] lines = File.ReadAllLines(@"C:\temp\power_usage.csv");
```

produces an array containing:

```
lines[0] → "Date;Appliance;HoursUsed;PowerWatts;EnergyKWh"
lines[1] → "2026-09-01;Refrigerator;24.0;120;2.88"
lines[2] → "2026-09-01;Television;3.5;100;0.35"
```

So **each element of the array represents one line of the file**.

---

`string[]` means **an array of strings**.

The square brackets `[]` indicate that the variable can contain multiple `string` values.

For example:

``` csharp
string[] names = { "John", "Mary", "Peter" };
```

contains three strings:

```
names[0] → "John"
names[1] → "Mary"
names[2] → "Peter"
```

Therefore:

```
string[] lines
```

means that `lines` is an array in which every element is a line of text.

---

Normally, `\` has a special meaning in C# strings. For example: `\t` means a **tab character**, and `\n` means new line. 

Therefore, this Windows path `"C:\temp\power_usage.csv"` can cause a problem because `\t` is interpreted as a tab.

With `@`  C# treats the backslashes literally. In other words:

**The `@` tells C# not to interpret backslashes as escape characters in the string.**

Without `@` You have to escape every backslash: `"C:\\temp\\power_usage.csv"`

This is why you will frequently see `@` in C# code containing **Windows paths, regular expressions, or other text containing many backslashes**.

---

Using this for loop, we iterate through all the elements of the array:

``` c#
for (int i = 1; i < lines.Length; i++)
```

We initialize `i=1` instead of `0` since we skip the first line containing the header. 

---
The following line splits one line of the CSV file into its individual fields. Here, the separator is the semicolon: 

``` c#
string[] parts = lines[i].Split(';');
```

Therefore:

``` c#
lines[i].Split(';')
```

converts:

```
2026-09-01;Refrigerator;24.0;120;2.88
```

into five separate strings:

```
2026-09-01
Refrigerator
24.0
120
2.88
```

---

Inside the loop, we create a `PowerUsage` object from one row of the CSV file:

``` c#
PowerUsage pu = new PowerUsage();

pu.Date = DateTime.Parse(parts[0]);
pu.Appliance = parts[1];
pu.HoursUsed = double.Parse(parts[2]);
pu.PowerWatts = int.Parse(parts[3]);
pu.EnergyKWh = double.Parse(parts[4]);
```

The code below does exactly the same using the object initializer syntax:

``` c#
PowerUsage pu = new PowerUsage
{
    Date = DateTime.Parse(parts[0]),
    Appliance = parts[1],
    HoursUsed = double.Parse(parts[2]),
    PowerWatts = int.Parse(parts[3]),
    EnergyKWh = double.Parse(parts[4])
};
```

---

When reading numbers from the CSV file, we can use `CultureInfo.InvariantCulture` to make sure that the decimal point `.` is interpreted correctly, regardless of the computer's regional settings.

Our CSV file contains decimal numbers such as:

```
24.0
2.88
3.5
```

The decimal separator in the file is a **dot (`.`)**.

However, the decimal separator depends on the computer's culture settings. For example:

- English culture → `2.88`
- Hungarian culture → `2,88`

If we simply write:

```
double.Parse(parts[4])
```

C# uses the **current culture of the computer**. On a Hungarian computer, it may expect a comma instead of a dot. 

---
To make your code culture-proof as the second argument to `double.Parse`: 

``` c#
HoursUsed = double.Parse(parts[2], CultureInfo.InvariantCulture);
EnergyKWh = double.Parse(parts[4], CultureInfo.InvariantCulture);
```

If you want to use `CultureInfo` You need to refer to the namespace at the beginning of the file. 

``` c#
using System.Globalization;
```

---

The following line:

``` c#
powerUsages.Add(powerUsage);
```

adds the `powerUsage` object to the `powerUsages` list.

---

Congratulations, you made it to the end of the first part 😅

If you have any questions, don't hesitate to write to me on Teams! I really mean it! 


