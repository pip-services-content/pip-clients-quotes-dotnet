using PipServices.Quotes.Client.Version1;

using System.Threading.Tasks;

using Xunit;

namespace PipServices.Quotes.Client.Test
{
    public class QuotesClientFixture
    {
        private readonly QuoteV1 _quote1 = new QuoteV1()
        {
            Text = new MultiString("Test Text 1"),
            Author = new MultiString("Test Author 1"),
            Status = "new"            
        };

        private readonly QuoteV1 _quote2 = new QuoteV1()
        {
            Text = new MultiString("Test Text 2"),
            Author = new MultiString("Test Author 2"),
            Status = "new"
        };

        private readonly IQuotesClientV1 _client;

        public QuotesClientFixture(IQuotesClientV1 client)
        {
            Assert.NotNull(client);
            _client = client;
        }

        public async Task TestCrudOperations()
        {
            // Create one quote
            var quote1 = await _client.CreateQuoteAsync(null, _quote1);

            Assert.NotNull(quote1);
            Assert.NotNull(quote1.Id);
            Assert.Equal(_quote1.Text, quote1.Text);
            Assert.Equal(_quote1.Author, quote1.Author);
            Assert.Equal(_quote1.Status, quote1.Status);

            // Create another quote
            var quote2 = await _client.CreateQuoteAsync(null, _quote2);

            Assert.NotNull(quote2);
            Assert.NotNull(quote2.Id);
            Assert.Equal(_quote2.Text, quote2.Text);
            Assert.Equal(_quote2.Author, quote2.Author);
            Assert.Equal(_quote2.Status, quote2.Status);

            // Get all quotes
            var quotes = await _client.GetQuotesAsync(null, null, null);
            Assert.NotNull(quotes);
            Assert.Equal(2, quotes.Data.Count);

            // Update the quote
            quote1.Status = "completed";
            var quote = await _client.UpdateQuoteAsync(null, quote1);

            Assert.NotNull(quote);
            Assert.Equal(quote1.Id, quote.Id);
            Assert.Equal(quote1.Status, quote.Status);

            // Delete the quote
            await _client.DeleteQuoteByIdAsync(null, quote1.Id);

            // Try to get deleted quote
            quote = await _client.GetQuoteByIdAsync(null, quote1.Id);
            Assert.Null(quote);
        }

    }
}
