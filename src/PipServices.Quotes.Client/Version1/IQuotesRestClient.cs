using PipServices.Runtime.Data;
using PipServices.Runtime.Errors;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PipServices.Quotes.Client.Version1
{
    [ServiceKnownType(typeof(Quote))]
    [ServiceContract]
    public interface IQuotesRestClient
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "quotes?correlation_id={correlationId}&author={author}&status={status}&tags={tags}&except_ids={exceptIds}&skip={skip}&take={take}&total={total}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        DataPage<Quote> GetQuotes(string correlationId, string author = null, string status = null, string tags = null, string exceptIds = null, 
            string skip = null, string take = null, string total = null);

        [OperationContract]
        [WebGet(
            UriTemplate = "quotes/random?correlation_id={correlationId}&author={author}&status={status}&tags={tags}&except_ids={exceptIds}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        Quote GetRandomQuote(string correlationId, string author = null, string status = null, string tags = null, string exceptIds = null);

        [OperationContract]
        [WebGet(
            UriTemplate = "quotes/{quoteId}?correlation_id={correlationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        Quote GetQuoteById(string correlationId, string quoteId);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "quotes?correlation_id={correlationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        Quote CreateQuote(string correlationId, Quote Quote);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "quotes/{quoteId}?correlation_id={correlationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        Quote UpdateQuote(string correlationId, string quoteId, object quote);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "quotes/{quoteId}?correlation_id={correlationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [FaultContract(typeof(FaultData))]
        void DeleteQuote(string correlationId, string quoteId);
    }
}
