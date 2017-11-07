using System.Threading.Tasks;

using PipServices.Commons.Data;
using PipServices.Net.Rest;

namespace PipServices.Quotes.Client.Version1
{
    public class QuotesHttpClientV1 : CommandableHttpClient, IQuotesClientV1
    {
        public QuotesHttpClientV1()
            : base("quotes")
        {
        }

        public Task<DataPage<QuoteV1>> GetQuotesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<DataPage<QuoteV1>>("get_quotes", correlationId, new
                {
                    correlation_id = correlationId,
                    filter = filter ?? new FilterParams(),
                    paging = paging ?? new PagingParams()
                });
            }
        }

        public Task<QuoteV1> GetRandomQuoteAsync(string correlationId, FilterParams filter)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<QuoteV1>("get_random_quote", correlationId, new
                {
                    correlation_id = correlationId,
                    filter = filter ?? new FilterParams(),
                });
            }
        }

        public Task<QuoteV1> GetQuoteByIdAsync(string correlationId, string quoteId)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<QuoteV1>("get_quote_by_id", correlationId, new
                {
                    correlation_id = correlationId,
                    quote_id = quoteId
                });
            }
        }

        public Task<QuoteV1> CreateQuoteAsync(string correlationId, QuoteV1 quote)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<QuoteV1>("create_quote", correlationId, new
                {
                    correlation_id = correlationId,
                    quote = quote
                });
            }
        }

        public Task<QuoteV1> UpdateQuoteAsync(string correlationId, QuoteV1 quote)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<QuoteV1>("update_quote", correlationId, new
                {
                    correlation_id = correlationId,
                    quote = quote
                });
            }
        }

        public Task<QuoteV1> DeleteQuoteByIdAsync(string correlationId, string quoteId)
        {
            using (var timing = Instrument(correlationId))
            {
                return CallCommand<QuoteV1>("delete_quote_by_id", correlationId, new
                {
                    correlation_id = correlationId,
                    quote_id = quoteId
                });
            }
        }
    }
}
