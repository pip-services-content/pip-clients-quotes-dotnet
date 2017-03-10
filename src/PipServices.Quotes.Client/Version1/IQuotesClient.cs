using PipServices.Runtime.Data;

namespace PipServices.Quotes.Client.Version1
{
    public interface IQuotesClient
    {
        DataPage<Quote> GetQuotes(string correlationId, FilterParams filter, PagingParams paging);
        Quote GetRandomQuote(string correlationId, FilterParams filter);
        Quote GetQuoteById(string correlationId, string quoteId);
        Quote CreateQuote(string correlationId, Quote quote);
        Quote UpdateQuote(string correlationId, string quoteId, object quote);
        void DeleteQuote(string correlationId, string quoteId);
    }
}
