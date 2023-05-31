using System.Net.Http.Headers;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://openexchangerates.org/api/latest.json?app_id=13804999fb6e4d53b4007f57067a90a9"),
            Headers =
    {
        { "accept", "application/json" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(body);
            JsonElement root = doc.RootElement;
            double audRate = root.GetProperty("rates").GetProperty("AUD").GetDouble();

            //Console.WriteLine($"AUD Rate: {audRate}");
            Console.WriteLine(body);
        }
    }
}