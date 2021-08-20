using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    { 

        static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddHttpClientInstrumentation()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebApiClient"))
            .AddOtlpExporter(otlpOptions =>
                {
                     Uri uri = new Uri("http://localhost:55680");
                     otlpOptions.Endpoint = uri;
                })
            .Build();

           

            Console.WriteLine("press any key to continue!");
            Console.ReadLine();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44309/api/Values");
                var activity = Activity.Current;


                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine($"response error code : {response.StatusCode}");
                }

            }

            
            
            
        }
        

    }
}
