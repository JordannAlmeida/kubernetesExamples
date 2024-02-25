using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace applicationB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapPost("/v1/callSync", ([FromBody] CallAsyncRequest callAsyncRequest) =>
            {
                Thread thread = new(async () => //simulate async process
                {
                    Thread.Sleep(5000);
                    X509Certificate2 certificate = getCertificate();
                    string url = callAsyncRequest.webhookUrl;
                    var body = new { result = "Webhook returned!" };
                    string jsonBody = JsonSerializer.Serialize(body);
                    using var handler = new HttpClientHandler();
                    handler.ClientCertificates.Add(certificate);
                    using var client = new HttpClient(handler);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    Console.WriteLine($"Response webhook: {response.StatusCode}"); //Use logs here
                });
                return Results.NoContent();
            });

            app.Run();
        }

        private static X509Certificate2 getCertificate()
        {
            string certificatePath = Environment.GetEnvironmentVariable("certificatePath");
            string passCert = Environment.GetEnvironmentVariable("passCert");
            X509Certificate2 certificate = new X509Certificate2(certificatePath, passCert);
            return certificate;
        }
    }
}
