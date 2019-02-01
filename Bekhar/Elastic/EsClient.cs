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
        public static ElasticClient GetInstance(string indexName)
        {
            if (innerClient == null)
            {
                //var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("bekhar").DisableDirectStreaming();

                var settings = new ConnectionSettings(new Uri("https://elastic:1IeomFYO4yVyqzlrO95SpXFy@a8769f2176114de3ac5fb6371bd61b21.eu-central-1.aws.cloud.es.io:9243")).DefaultIndex(indexName).DisableDirectStreaming();
                innerClient = new ElasticClient(settings);
            }

            return innerClient;
        }

    }
}