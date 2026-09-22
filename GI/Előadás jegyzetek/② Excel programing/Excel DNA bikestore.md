``` csharp
 [ExcelFunction(Description = "Get bikes from DB")]
 public static object[,] GetBikes()
 {
     BikestoreModels.SeBikestoreContext context = new BikestoreModels.SeBikestoreContext();
     var bikes = context.Products.ToList();

     object[,] tömb = new object[bikes.Count(), 3];

     for (int i = 0;i<bikes.Count; i++)
     {
         tömb[i,0] = bikes[i].ProductName;
         tömb[i, 1] = bikes[i].ListPrice;
         tömb[i,2] = bikes[i].ModelYear;

     }

     return tömb;

 }
```