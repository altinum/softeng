## Prerequisites

- Visual Studio 2019 or later (any edition)
- .NET Framework 4.x or .NET 6/8 (both supported)
- Microsoft Excel installed

---

## Step 1 — Create a New Class Library Project

1. Open Visual Studio and choose **Create a new project**.
2. Select **Class Library** (C#) and click **Next**.
3. Name your project (e.g., `MyExcelAddIn`) and choose a location.
4. Select your target framework (.NET Framework 4.8 or .NET 6+) and click **Create**.

> ⚠️ Do **not** choose a Console App or WPF project — Excel-DNA requires a Class Library.

---

## Step 2 — Install the Excel-DNA NuGet Package

1. Right-click your project in **Solution Explorer** and select **Manage NuGet Packages**.
2. Search for **ExcelDna.AddIn**.
3. Click **Install** and accept any license prompts.

This installs the core Excel-DNA runtime and creates the necessary `.dna` manifest file automatically.

---

## Step 3 — Write Your First UDF (User-Defined Function)

Replace or add a class file (e.g., `Functions.cs`) with the following:


```csharp
using ExcelDna.Integration;

public static class MyFunctions
{
    [ExcelFunction(Description = "Multiplies two numbers together.")]
    public static double MultiplyTwo(double a, double b)
    {
        return a * b;
    }
}
```

## Practical examples
### Generating random numbers

Any `public static` method decorated with `[ExcelFunction]` will appear as a worksheet function in Excel.

``` csharp
[ExcelFunction(Description = "My random array")]
public static double[,] MyRandoms(int width, int height)
{
	var result = new double[width , height];
	Random rand = new Random();
	for (int col = 0; col < width; col++)
	{
		for (int row = 0; row < height; row++)
		{
			result[col,row] = rand.Next(10);
		}    
	}

	return result;
}
```

### Retrieving data from a database

``` c#
[ExcelFunction(Description = "Get instructors")]
        public static object[,] GetInstuctors()
        {
            StudiesModels.StudiesContext context = new StudiesModels.StudiesContext();
            var instuctors = context.Instructors.ToList();
            object[,] result = new object[instuctors.Count,2];
            for (int i = 0; i < instuctors.Count; i++)
            {
                result[i, 0] = instuctors[i].Name;
                result[i, 1] = instuctors[i].Salutation;
            }


            return result;
        }
```

### Even and odd numbers. 

``` csharp
[ExcelFunction(Description = "Determines whether each number in an array is even or odd")]
public static object[,] IsEvenOrOdd(double[,] numbers)
{
	int rows = numbers.GetLength(0);
	int cols = numbers.GetLength(1);

	var result = new object[rows, cols];

	for (int row = 0; row < rows; row++)
	{
		for (int col = 0; col < cols; col++)
		{
			int num = (int)numbers[row, col];
			result[row, col] = (num % 2 == 0) ? "Even" : "Odd";
		}
	}

	return result;
}
```

### Creating a histogram 

``` csharp
using ExcelDna.Integration;
using System.Linq;

public static class HistogramFunctions
{
    [ExcelFunction(Description = "Divides numbers into 10 bins and returns the frequencies")]
    public static object[,] HISTOGRAM(double[] data)
    {
        if (data.Length == 0)
            return new object[,] { { "No data" } };

        double min = data.Min();
        double max = data.Max();
        double binSize = (max - min) / 10.0;

        var result = new object[11, 2];
        result[0, 0] = "Bin";
        result[0, 1] = "Frequency";

        for (int k = 0; k < 10; k++)
        {
            double lower = min + k * binSize;
            double upper = min + (k + 1) * binSize;

            int count = data.Count(x =>
                k < 9
                    ? x >= lower && x < upper
                    : x >= lower && x <= upper);

            result[k + 1, 0] = $"{lower:F2} – {upper:F2}";
            result[k + 1, 1] = count;
        }

        return result;
    }
}
```

# Hasznos JSON API-k Excel-DNA függvényekhez  
  
| API | URL-minta | Kulcs | Mire jó |  
|---|---|---|---|  
| **Frankfurter** | `[https://api.frankfurter.dev/v1/latest?base=EUR&symbols=HUF`](https://api.frankfurter.dev/v1/latest?base=EUR&symbols=HUF) | nem | EUR/HUF és más devizaárfolyamok (ECB-adatok), történeti adatokkal |  
| **Open-Meteo** | `[https://api.open-meteo.com/v1/forecast?latitude=47.5&longitude=19.04&current=temperature_2m`](https://api.open-meteo.com/v1/forecast?latitude=47.5&longitude=19.04&current=temperature_2m) | nem | Időjárás bármely koordinátára |  
| **Nager.Date** | `[https://date.nager.at/api/v3/PublicHolidays/2026/HU`](https://date.nager.at/api/v3/PublicHolidays/2026/HU) | nem | Magyar ünnepnapok (az áthelyezett munkanapokat nem tartalmazza) |  
| **[szunetnapok.hu](http://szunetnapok.hu/)** | `[https://szunetnapok.hu/api/{kulcs}/2026/json`](https://szunetnapok.hu/api/%7Bkulcs%7D/2026/json) | igen (ingyenes csomag: 100 lekérdezés/hó) | Munkaszüneti napok és áthelyezett munkanapok (`type` 1 = szünnap, 2 = munkanap) |  
| **CoinGecko** | `[https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=huf`](https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=huf) | nem (rate limit van) | Kripto árfolyam forintban |  
| **Open Notify** | `[http://api.open-notify.org/iss-now.json`](http://api.open-notify.org/iss-now.json) | nem | A Nemzetközi Űrállomás aktuális pozíciója |  
| **PokéAPI** | `[https://pokeapi.co/api/v2/pokemon/pikachu`](https://pokeapi.co/api/v2/pokemon/pikachu) | nem | Összetett, beágyazott JSON a parszolás gyakorlásához |  
| **xkcd** | `[https://xkcd.com/info.0.json`](https://xkcd.com/info.0.json) | nem | Napi képregény metaadatai |  
| **JSONPlaceholder** | `[https://jsonplaceholder.typicode.com/todos/1`](https://jsonplaceholder.typicode.com/todos/1) | nem | Teszt-API fejlesztéshez |  
  
## Megjegyzések  
  
- Az ECB naponta egyszer, munkanapokon kb. 16:00 (CET) körül közöl árfolyamot, ezért a Frankfurternél a 60 percnél sűrűbb frissítés felesleges.  
- A [szunetnapok.hu](http://szunetnapok.hu/) 2027-es adatai az NGM rendelet megjelenése után válnak elérhetővé.  
- Az API-kulcsot ne tedd a forráskódba, használj környezeti változót vagy konfigurációs fájlt.

``` c#
public static class MyFunctions  
{  
    static readonly HttpClient Http = new HttpClient();  
  
    [ExcelFunction(Description = "Latest exchange rate from Frankfurter (ECB data)")]  
    public static async double GetRate(string from, string to)  
    {  
        string url = $"[https://api.frankfurter.dev/v1/latest?base={from}&symbols={to}](https://api.frankfurter.dev/v1/latest?base=%7Bfrom%7D&symbols=%7Bto%7D)";  
  
        string json = await Http.GetStringAsync(url);  
        using var doc = JsonDocument.Parse(json);  
  
        return doc.RootElement  
                  .GetProperty("rates")  
                  .GetProperty(to)  
                  .GetDouble();  
    }  
}
```

```
=LET(
    firstName, INDEX(A1#, , 1),
    salutation, INDEX(A1#, , 2),
    salutation &
        HA(
            HOSSZ(salutation) =
                0,
            "",
            " "
        ) & firstName
)
```

```
=SZŰRŐ(A1#, SZÁM(SZÖVEG.KERES("Dr.", INDEX(A1#, ,2))))
=FILTER(A1#, ISNUMBER(FIND("Dr.", CHOOSECOLS(A1#, 2))))
```
