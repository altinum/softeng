using ExcelDna.Integration;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ExcelDNA_Examples
{
    public class Class1
    {
        [ExcelFunction(Description = "Latest exchange rate from Frankfurter (ECB data)")]
        public static object GetRate(string from, string to)
        {
            // callerParameters identifies this call, so different arguments get separate cached results
            return ExcelAsyncUtil.RunTask("GetRate", new object[] { from, to },
                () => GetRateAsync(from, to));
        }

        static async Task<double> GetRateAsync(string from, string to)
        {
            string url = $"https://api.frankfurter.dev/v1/latest?base={from}&symbols={to}";

            HttpClient httpClinet = new HttpClient(); 

            var jsonString = await httpClinet.GetStringAsync(url);
            var doc = JsonDocument.Parse(jsonString);

            return doc.RootElement
                      .GetProperty("rates")
                      .GetProperty(to)
                      .GetDouble();
        }

        static readonly HttpClient Http = new HttpClient();

        [ExcelFunction(Description = "Latest exchange rate from Frankfurter (ECB data)")]
        public static async Task<double> GetRate2(string from, string to)
        {
            string url = $"https://api.frankfurter.dev/v1/latest?base={from}&symbols={to}";

            string json = await Http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                      .GetProperty("rates")
                      .GetProperty(to)
                      .GetDouble();
        }


    }
}
