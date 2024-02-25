using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace applicationA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapPost("/v1/callSync", async (HttpContext httpContext) =>
            {
                string url = "https://appB.example.com/v1/callAsync";
                var body = new { webhookUrl = "https://appA.example.com/webhook" };
                string jsonBody = JsonSerializer.Serialize(body);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Request executed with sucess"); //Use logs here
                        return Results.NoContent();
                    }
                    else
                    {
                        return Results.Problem(
                            title: $"Error calling external API: {response.ReasonPhrase}",
                            statusCode: (int)response.StatusCode
                        );
                    }
                }
            });

            app.MapPost("/webhook", ([FromBody] WebHookRequest webHookRequest) =>
            {
                Console.WriteLine(webHookRequest.result); //Use logs here
            });

            app.Run();
        }
    }
}
