# Client API (version 1) <br/> Quotes Microservices Client SDK for .NET

.NET client API for Quotes microservice is a thin layer on the top of
communication protocols. It hides details related to specific protocol implementation
and provides high-level API to access the microservice for simple and productive development.

* [Installation](#install)
* [Getting started](#get_started)
* [Quote class](#class)
* [IQuoteClientV1 interface](#interface)
    - [GetQuotesAsync()](#operation1)
    - [GetRandomQuoteAsync()](#operation2)
    - [GetQuoteByIdAsync()](#operation3)
    - [CreateQuoteAsync()](#operation4)
    - [UpdateQuoteAsync()](#operation5)
    - [DeleteQuoteAsync()](#operation6)
* [QuotesHttpClientV1 class](#CommandableHttpClient)

## <a name="install"></a> Installation

To work with the client SDK add references to PipServices.Commons, PipServices.Net and PipServices.Quotes.Client

If you don't have them readily available then download and build them from sourcecode
- Pip.Services.Commons : https://github.com/pip-services/pip-services-commons-dotnet
- Pip.Services.Net : https://github.com/pip-services/pip-services-net-dotnet
- This .NET Client SDK for Quotes microservices: https://github.com/pip-services-content/pip-clients-quotes-dotnet 

## <a name="get_started"></a> Getting started

This is a simple example on how to work with the microservice using HTTP client:

```cs
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
                var correlationId = "123";
                var config = ConfigParams.FromTuples(
                    "connection.type", "http",
                    "connection.host", "localhost",
                    "connection.port", 8080
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
```

### <a name="class"></a> Quote class

Represents an inspirational quote

**Properties:**
- Id: string - unique quote id
- Text: MultiString - quote text in different languages
- Author: MultiString - name of the quote author in different languages
- Status: string - editing status of the quote: 'new', 'writing', 'translating', 'completed' (default: 'new')
- Tags: string[] - (optional) search tags that represent topics associated with the quote
- All_Tags: string[] - (read only) explicit and hash tags in normalized format for searching  

## <a name="interface"></a> IQuotesClientV1 interface

IQuotesClientV1 as a common interface across all client implementations. 

```cs
public interface IQuotesClientV1
{
	Task<DataPage<QuoteV1>> GetQuotesAsync(string correlationId, FilterParams filter, PagingParams paging);
	Task<QuoteV1> GetRandomQuoteAsync(string correlationId, FilterParams filter);
	Task<QuoteV1> GetQuoteByIdAsync(string correlationId, string quoteId);
	Task<QuoteV1> CreateQuoteAsync(string correlationId, QuoteV1 quote);
	Task<QuoteV1> UpdateQuoteAsync(string correlationId, QuoteV1 quote);
	Task<QuoteV1> DeleteQuoteByIdAsync(string correlationId, string quoteId);
}
```

### <a name="operation1"></a> GetQuotesAsync(correlationId, filter, paging)

Retrieves a collection of quotes according to specified criteria

**Arguments:** 
- correlationId: correlation id
- filter: any - filter parameters
  - tags: string[] - (optional) list tags with topic names
  - status: string - (optional) quote editing status
  - author: string - (optional) author name in any language 
  - except_ids: string[] - (optional) quote ids to exclude 
- paging: any - paging parameters
  - skip: int - (optional) start of page (default: 0). Operation returns paged result
  - take: int - (optional) page length (max: 100). Operation returns paged result
  - paging: bool - (optional) true to enable paging and return total count

**Returns**
  - DataPage<QuoteV1> - retrieved quotes in paged format

### <a name="operation2"></a> GetRandomQuoteAsync(correlationId, filter)

Retrieves a random quote from filtered resultset

**Arguments:** 
- correlationId: correlation id
- filter: any - filter parameters
  - tags: string[] - (optional) list tags with topic names
  - status: string - (optional) quote editing status
  - author: string - (optional) author name in any language
  - except_ids: string[] - (optional) quote ids to exclude
  
**Returns** 
- Quote - random quote, null if object wasn't found 

### <a name="operation3"></a> GetQuoteByIdAsync(correlationId, quoteId)

Retrieves a single quote specified by its unique id

**Arguments:** 
- correlationId: correlation id
- quoteId: string - unique Quote id

**Returns**
- Quote - retrieved quote, null if object wasn't found 

### <a name="operation4"></a> CreateQuoteAsync(correlationId, quote)

Creates a new quote

**Arguments:** 
- correlationId: correlation id
- quote: Quote - Quote object to be created. If object id is not defined it is assigned automatically.

**Returns**
- Quote - created quote object

### <a name="operation5"></a> UpdateQuoteAsync(correlationId, quoteId, quote)

Updates quote specified by its unique id

**Arguments:** 
- correlationId: correlation id
- quoteId: string - unique quote id
- quote: Quote - quote object with new values. Partial updates are supported

**Returns**
- Quote - updated quote object 

### <a name="operation6"></a> DeleteQuoteAsync(correlationId, quoteId)

Deletes quote specified by its unique id

**Arguments:** 
- correlationId: correlation id
- quoteId: string - unique quote id
 
## <a name="CommandableHttpClient"></a> QuotesHttpClientV1 class

QuotesHttpClientV1 is a client that implements commandable HTTP/REST protocol

```cs
public class QuotesHttpClientV1 : CommandableHttpClient, IQuotesClientV1
{
	public QuotesHttpClientV1();
	public Task<DataPage<QuoteV1>> GetQuotesAsync(string correlationId, FilterParams filter, PagingParams paging);
	public Task<QuoteV1> GetRandomQuoteAsync(string correlationId, FilterParams filter);
	public Task<QuoteV1> GetQuoteByIdAsync(string correlationId, string quoteId);
	public Task<QuoteV1> CreateQuoteAsync(string correlationId, QuoteV1 quote);
	public Task<QuoteV1> UpdateQuoteAsync(string correlationId, QuoteV1 quote);
	public Task<QuoteV1> DeleteQuoteByIdAsync(string correlationId, string quoteId);
}
```
