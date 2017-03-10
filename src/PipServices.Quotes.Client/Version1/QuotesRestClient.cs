using PipServices.Runtime;
using PipServices.Runtime.Clients;
using PipServices.Runtime.Config;
using PipServices.Runtime.Data;
using PipServices.Runtime.Util;

namespace PipServices.Quotes.Client.Version1
{
    public class QuotesRestClient : RestClient<IQuotesRestClient>, IQuotesClient
    {
        /// <summary>
        /// Unique descriptor for the QuotesRestClient component
        /// </summary>
        public readonly static ComponentDescriptor ClassDescriptor = new ComponentDescriptor(
            Category.Clients, "pip-services-quotes", "rest", "1.0"
        );

        public QuotesRestClient()
            : base(ClassDescriptor)
        { }

        public QuotesRestClient(ComponentConfig config)
            : this()
        {
            Configure(config);
            Link(new ComponentSet());
        }

        public QuotesRestClient(params object[] values)
            : this()
        {
            ComponentConfig config = new ComponentConfig();
            config.RawContent.SetTuplesArray(values);

            Configure(config);
            Link(new ComponentSet());
        }

        public DataPage<Quote> GetQuotes(string correlationId, FilterParams filter, PagingParams paging)
        {
            filter = filter ?? new FilterParams();
            paging = paging ?? new PagingParams();

            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.get_quotes"))
            {
                return _channel.GetQuotes(
                    correlationId: correlationId,
                    author: filter.GetNullableString("author"),
                    status: filter.GetNullableString("status"),
                    tags: filter.GetNullableString("tags"),
                    exceptIds: filter.GetNullableString("except_ids"),

                    skip: Converter.ToString(paging.Skip),
                    take: Converter.ToString(paging.Take),
                    total: Converter.ToString(paging.Total)
                );
            }
        }

        public Quote GetRandomQuote(string correlationId, FilterParams filter)
        {
            filter = filter ?? new FilterParams();

            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.get_random_quote"))
            {
                return _channel.GetRandomQuote(
                    correlationId: correlationId,
                    author: filter.GetNullableString("author"),
                    status: filter.GetNullableString("status"),
                    tags: filter.GetNullableString("tags"),
                    exceptIds: filter.GetNullableString("except_ids")
                );
            }
        }

        public Quote GetQuoteById(string correlationId, string quoteId)
        {
            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.get_quote_by_id"))
            {
                return _channel.GetQuoteById(correlationId, quoteId);
            }
        }

        public Quote CreateQuote(string correlationId, Quote quote)
        {
            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.create_quote"))
            {
                return _channel.CreateQuote(correlationId, quote);
            }
        }

        public Quote UpdateQuote(string correlationId, string quoteId, object quote)
        {
            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.update_quote"))
            {
                return _channel.UpdateQuote(correlationId, quoteId, quote);
            }
        }

        public void DeleteQuote(string correlationId, string quoteId)
        {
            CheckCurrentState(State.Opened);

            using (ITiming timing = Instrument(correlationId, "quotes.delete_quote"))
            {
                _channel.DeleteQuote(correlationId, quoteId);
            }
        }
    }
}
