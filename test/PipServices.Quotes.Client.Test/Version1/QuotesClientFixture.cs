using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipServices.Runtime.Data;
using System.Linq;

namespace PipServices.Quotes.Client.Version1
{
    public class QuotesClientFixture
    {
        private readonly Quote QUOTE1 = new Quote
        {
            Text = new MultiString { En = "Text 1" },
            Author = new MultiString { En = "Author 1" },
            Status = "new"
        };
        private readonly Quote QUOTE2 = new Quote
        {
            Tags = new string[] { "TAG 1" },
            Text = new MultiString { En = "Text 2" },
            Author = new MultiString { En = "Author 2" },
            Status = "new"
        };

        private IQuotesClient _client;

        public QuotesClientFixture(IQuotesClient client)
        {
            Assert.IsNotNull(client);
            _client = client;
        }

        public void TestCrudOperations()
        {
            // Create one quote
            Quote quote1 = _client.CreateQuote(null, QUOTE1);

            Assert.IsNotNull(quote1);
            Assert.IsNotNull(quote1.Id);
            Assert.AreEqual(QUOTE1.Text.En, quote1.Text.En);
            Assert.AreEqual(QUOTE1.Author.En, quote1.Author.En);

            // Create another quote
            Quote quote2 = _client.CreateQuote(null, QUOTE2);

            Assert.IsNotNull(quote2);
            Assert.IsNotNull(quote2.Id);
            Assert.AreEqual(QUOTE2.Text.En, quote2.Text.En);
            Assert.AreEqual(QUOTE2.Author.En, quote2.Author.En);

            // Get all quotes
            DataPage<Quote> quotes = _client.GetQuotes(null, null, null);
            Assert.IsNotNull(quotes);
            Assert.IsTrue(quotes.Data.Count() >= 2);

            // Get random quote
            Quote randomQuote = _client.GetRandomQuote(null, null);
            Assert.IsNotNull(randomQuote);

            // Update the quote
            quote1.Text = new MultiString { En = "Updated Content 1" };
            Quote quote = _client.UpdateQuote(
                null,
                quote1.Id,
                quote1
            );

            Assert.IsNotNull(quote);
            Assert.AreEqual(quote1.Id, quote.Id);
            Assert.AreEqual(quote1.Author.En, quote.Author.En);
            Assert.AreEqual("Updated Content 1", quote.Text.En);

            // Delete the quote #1
            _client.DeleteQuote(null, quote1.Id);

            // Delete the quote #2
            _client.DeleteQuote(null, quote2.Id);

            // Try to get deleted quote
            quote = _client.GetQuoteById(null, quote1.Id);
            Assert.IsNull(quote);
        }
    }
}
