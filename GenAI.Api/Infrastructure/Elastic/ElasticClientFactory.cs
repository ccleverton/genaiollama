using Nest;

namespace GenAI.Api.Infrastructure.Elastic;

public static class ElasticClientFactory
{
    public static IElasticClient Create()
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("docs");

        return new ElasticClient(settings);
    }
}