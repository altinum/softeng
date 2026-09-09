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