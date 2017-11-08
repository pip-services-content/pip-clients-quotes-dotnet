# Quotes Microservice Client SDK for .NET

This is a .NET client SDK for [pip-services-quotes-dotnet] https://github.com/pip-services-content/pip-services-quotes-dotnet microservice.
It provides an easy to use abstraction over communication protocols:

* HTTP/REST client

<a name="links"></a> Quick Links:

* [Development Guide](doc/Development.md)
* [API Version 1](doc/DotNetClientApiV1.md)

## Install

Add PipServices.Quotes.Client.dll client SDK into your project references

## Use

Add **PipServices.Commons** and **PipServices.Quotes.Client.Version1** namespaces
```cs
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Quotes.Client.Version1;
```

Define client configuration parameters that match configuration of the microservice external API
```cs
// Client configuration
var config = ConfigParams.FromTuples(
	"connection.type", "http",
	"connection.host", "localhost",
	"connection.port", 8080
);
```

Instantiate the client and open connection to the microservice
```cs
// Create the client instance
var client = new QuotesHttpClientV1();

// Connect to the microservice
client.OpenAsync(null);
    
// Work with the microservice
...
```

Now the client is ready to perform operations
```cs
// Create a new quote
var quote = new QuoteV1
{
	Text = new MultiString("Get in hurry slowly"),
	Author = new MultiString("Russian proverb"),
	Status = "completed"
};

client.CreateQuoteAsync(null, quote);
```

```cs
// Get the list of quotes on 'time management' topic
var quotePage = client.GetQuotesAsync(null,
    FilterParams.FromTuples(
        "search", "hurry",
        "status", "completed"
    ),
    new PagingParams(
        Skip =  0,
        Take = 10
    )
).Result;
```    

## Acknowledgements

This client SDK was created and currently maintained by *Sergey Seroukhov*.