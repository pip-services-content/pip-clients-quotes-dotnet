using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Quotes.Client.Version1;

using System;

namespace PipServices.Quotes.Client.Run
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var quotesProtocol = Environment.GetEnvironmentVariable("QUOTES_SERVICE_PROTOCOL") ?? "http";
                var quotesHost = Environment.GetEnvironmentVariable("QUOTES_SERVICE_HOST") ?? "localhost";
                var quotesPort = Environment.GetEnvironmentVariable("QUOTES_SERVICE_PORT") ?? "8080";

                var correlationId = "123";
                var config = ConfigParams.FromTuples(
                    "connection.protocol", quotesProtocol,
                    "connection.host", quotesHost,
                    "connection.port", quotesPort
                );
                var filterParams = FilterParams.FromTuples(
                    "status", "completed",
                    "search", "goal");

                var client = new QuotesHttpClientV1();
                client.Configure(config);
                client.OpenAsync(correlationId);

                var quote = client.GetRandomQuoteAsync(correlationId, filterParams).Result;

                if (quote != null)
                {
                    Console.WriteLine("'{0}' by {1}", ToEnglish(quote.Text), ToEnglish(quote.Author));
                }
                else
                {
                    Console.WriteLine("No quote was returned. Come up with your own...");
                }

                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();

                client.CloseAsync(string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string ToEnglish(MultiString value)
        {
            return value.ContainsKey("en") ? value["en"].ToString() : "<No Value>";
        }

    }
}
