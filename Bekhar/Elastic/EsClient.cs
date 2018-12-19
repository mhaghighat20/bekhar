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

                var settings = new ConnectionSettings(new Uri("https://elastic:8A7zj21veRIuRkCD5gdjpCI6@8c3696f388494c3c8ddf23655b10c0d8.eu-central-1.aws.cloud.es.io:9243/")).DefaultIndex("bekhar").DisableDirectStreaming();
                innerClient = new ElasticClient(settings);
            }

            return innerClient;
        }

    }
}