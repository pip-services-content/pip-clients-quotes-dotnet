# Client API (version 1) <br/> Quotes Microservices Client SDK for .NET

.NET client API for Quotes microservice is a thin layer on the top of
communication protocols. It hides details related to specific protocol implementation
and provides high-level API to access the microservice for simple and productive development.

* [Installation](#install)
* [Getting started](#get_started)
* [MultiString class](#class1)
* [Quote class](#class2)
* [QuotePage class](#class3)
* [IQuoteClient interface](#interface)
    - [Init()](#operation1)
    - [Open()](#operation2)
    - [Close()](#operation3)
    - [GetQuotes()](#operation4)
    - [GetRandomQuote()](#operation5)
    - [GetQuoteById()](#operation6)
    - [CreateQuote()](#operation7)
    - [UpdateQuote()](#operation8)
    - [DeleteQuote()](#operation9)
* [QuotesRestClient class](#client_rest)

## <a name="install"></a> Installation

To work with the client SDK add references to PipServices.Runtime.dll and PipServices.Quotes.Client.dll

If you don't have them readily available then download and build them from sourcecode
- Pip.Services .NET Runtime: https://github.com/pip-services/pip-services-runtime-dotnet
- This .NET Client SDK for Quotes microservices: https://github.com/pip-services/pip-clients-quotes-dotnet 

## <a name="get_started"></a> Getting started

This is a simple example on how to work with the microservice using REST client:

```cs
using PipServices.Quotes.Client.Version1;
using PipServices.Runtime;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Client configuration
        var config = new DynamicMap(
             "transport.type", "http",
             "transport.host", "localhost", 
             "transport.port", 8002
        };
  
        // Create the client instance
        var client = QuotesRestClient(config);
        
        try
        {
             // Open client connection to the microservice
             client.Open();             
             Console.Out.WriteLine("Opened connection");

             // Create a new quote
             var quote = new Quote {
                Text = new MiltiString { En = "Get in hurry slowly" },
                Author = new MultiString { En = "Russian proverb" },
                Tags = string[] { "time management" },
                Status = "completed"
            };

            quote = client.CreateQuote(quote);
            Console.Out.WriteLine("Create quote is");
            Console.Out.WriteLine(quote);

            // Get the list of quotes on 'time management' topic
            var quotesPage = client.GetQuotes(
                new FilterParams(
                    "tags", "time management",
                    "status", "completed"
                ),
                new PagingParams(
                    paging: true,
                    skip: 0,
                    take: 10
                )
            );

            Console.Out.WriteLine("Quotes on time management are");
            Console.Out.WriteLine(quotesPage.Data);
            
            // Close connection
            client.Close(); 
        }
        catch (Exception ex) 
        {
            Console.Error.WriteLine(ex);
        }
    }
}
```

### <a name="class1"></a> MultiString class

String that contains versions in multiple languages

**Properties:**
- En: string - English version of the string
- Sp: string - Spanish version of the string
- De: string - German version of the string
- Fr: string - Franch version of the string
- Pt: string - Portuguese version of the string
- Ru: string - Russian version of the string
- .. - other languages can be added here

### <a name="class2"></a> Quote class

Represents an inspirational quote

**Properties:**
- Id: string - unique quote id
- Text: MultiString - quote text in different languages
- Author: MultiString - name of the quote author in different languages
- Status: string - editing status of the quote: 'new', 'writing', 'translating', 'completed' (default: 'new')
- Tags: string[] - (optional) search tags that represent topics associated with the quote
- AllTags: string[] - (read only) explicit and hash tags in normalized format for searching  

### <a name="class3"></a> QuotePage class

Represents a paged result with subset of requested quotes

**Properties:**
- Data: Quote[] - array of retrieved Quote page
- Total: int? - total number of objects in retrieved resultset

## <a name="interface"></a> IQuotesClient interface

IQuotesClient as a common interface across all client implementations. 

```cs
public interface IQuotesClient {
    public void Init(References refs);
    public void Open();
    public void Close();
    public DataPage<Quote> GetQuotes(FilterParams filter, PagingParams paging);
    public Quote GetRandomQuote(FilterParams filter);
    public Quote GetQuoteById(string quoteId);
    public Quote CreateQuote(Quote quote);
    public Quote UpdateQuote(string quoteId, Quote quote);
    public void DeleteQuote(string quoteId);
}
```

### <a name="operation1"></a> Init(refs)

Initializes client references. This method is optional. It is used to set references 
to logger or performance counters.

**Arguments:**
- refs: References - references to other components 
  - log: ILog - reference to logger
  - countes: ICounters - reference to performance counters

### <a name="operation2"></a> Open()

Opens connection to the microservice

### <a name="operation3"></a> Close()

Closes connection to the microservice

### <a name="operation4"></a> GetQuotes(filter, paging)

Retrieves a collection of quotes according to specified criteria

**Arguments:** 
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
  - DataPage<Quote> - retrieved quotes in paged format

### <a name="operation5"></a> GetRandomQuote(filter)

Retrieves a random quote from filtered resultset

**Arguments:** 
- filter: any - filter parameters
  - tags: string[] - (optional) list tags with topic names
  - status: string - (optional) quote editing status
  - author: string - (optional) author name in any language
  - except_ids: string[] - (optional) quote ids to exclude
  
**Returns** 
- Quote - random quote, null if object wasn't found 

### <a name="operation6"></a> GetQuoteById(quoteId)

Retrieves a single quote specified by its unique id

**Arguments:** 
- quoteId: string - unique Quote id

**Returns**
- Quote - retrieved quote, null if object wasn't found 

### <a name="operation7"></a> CreateQuote(quote)

Creates a new quote

**Arguments:** 
- quote: Quote - Quote object to be created. If object id is not defined it is assigned automatically.

**Returns**
- Quote - created quote object

### <a name="operation8"></a> UpdateQuote(quoteId, quote)

Updates quote specified by its unique id

**Arguments:** 
- quoteId: string - unique quote id
- quote: Quote - quote object with new values. Partial updates are supported

**Returns**
- Quote - updated quote object 

### <a name="operation9"></a> DeleteQuote(quoteId)

Deletes quote specified by its unique id

**Arguments:** 
- quoteId: string - unique quote id
 
## <a name="client_rest"></a> QuotesRestClient class

QuotesRestClient is a client that implements HTTP/REST protocol

```cs
public class QuotesRestClient: RestClient, IQuotesClient {
    public QuotesRestClient(DynamicMap config);
    public void Init(References refs);
    public void Open();
    public void Close();
    public DataPage<Quote> GetQuotes(FilterParams filter, PagingParams paging);
    public Quote GetRandomQuote(FilterParams filter);
    public Quote GetQuoteById(string quoteId);
    public Quote CreateQuote(Quote quote);
    public Quote UpdateQuote(string quoteId, Quote quote);
    public void DeleteQuote(string quoteId);
}
```

**Constructor config properties:** 
- transport: object - HTTP transport configuration options
  - type: string - HTTP protocol - 'http' or 'https' (default is 'http')
  - host: string - IP address/hostname binding (default is '0.0.0.0')
  - port: int - HTTP port number
