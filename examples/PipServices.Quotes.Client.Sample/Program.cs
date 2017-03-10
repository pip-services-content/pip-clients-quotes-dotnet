using PipServices.Quotes.Client.Version1;
using PipServices.Runtime.Config;
using PipServices.Runtime.Data;
using System;

namespace PipServices.Quotes.Client.Sample
{
    class Program
    {
        private static string ToEnglish(MultiString value)
        {
            return value != null ? value.En : "<No Value>";
        }

        static void Main(string[] args)
        {
            ComponentConfig config = ComponentConfig.FromTuples(
                "endpoint.type", "http",
                "endpoint.host", "localhost",
                "endpoint.port", 8002
            );

            var client = new QuotesRestClient(config);

            try
            {
                client.Open();

                Quote quote = client.GetRandomQuote(
                    null,
                    FilterParams.FromTuples(
                        "status", "completed",
                        "tags", "goals"
                    )
                );

                if (quote != null)
                    Console.Out.WriteLine("'{0}' by {1}", ToEnglish(quote.Text), ToEnglish(quote.Author));
                else
                    Console.Out.WriteLine("No quote was returned. Come up with your own...");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}
