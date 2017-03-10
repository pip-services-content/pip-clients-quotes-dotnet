# Quotes Microservice Client SDK for .NET

This is a .NET client SDK for [pip-services-quotes](https://github.com/pip-services/pip-services-quotes) microservice.
It provides an easy to use abstraction over communication protocols:

* HTTP/REST client

<a name="links"></a> Quick Links:

* [Development Guide](doc/Development.md)
* [API Version 1](doc/DotNetClientApiV1.md)

## Install

Add PipServices.Quotes.Client.dll client SDK into your project references

## Use

Add **PipServices.Runtime** and **PipServices.Quotes.Client.Version1** namespaces
```cs
using PipServices.Quotes.Client.Version1;
using PipServices.Runtime;
```

Define client configuration parameters that match configuration of the microservice external API
```cs
// Client configuration
var config = new DynamicMap(
    "transport.type", "http",
    "transport.host", "localhost", 
    "transport.port", 8002
);
```

Instantiate the client and open connection to the microservice
```cs
// Create the client instance
var client = new QuotesRestClient(config);

// Connect to the microservice
client.Open();
    
// Work with the microservice
...
```

Now the client is ready to perform operations
```cs
// Create a new quote
var quote = new Quote {
    Text = new MultiString { En = "Get in hurry slowly" },
    Author = new MultiString { En = "Russian proverb" },
    Tags = new string[] { "time management" },
    Status = "completed"
};

quote = client.CreateQuote(quote);
```

```cs
// Get the list of quotes on 'time management' topic
var quotePage = client.GetQuotes(
    new FilterParams(
        "tags", "time management",
        "status", "completed"
    ),
    new Paging(
        paging: true,
        skip: 0,
        take: 10
    )
);
```    

## Acknowledgements

This client SDK was created and currently maintained by *Sergey Seroukhov*.

