using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Elastic
{
    internal class EsClient
    {
        private static ElasticClient innerClient;
        public static ElasticClient GetInstance()
        {
            if (innerClient == null)
            {
                //var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("bekhar").DisableDirectStreaming();

                var settings = new ConnectionSettings(new Uri("https://elastic:Hq7ntRjOu8BQzQhhVB2ZSpws@58ef425b3817474986de023092358b39.eu-central-1.aws.cloud.es.io:9243")).DefaultIndex("bekhar").DisableDirectStreaming();
                innerClient = new ElasticClient(settings);
            }

            return innerClient;
        }

    }
}