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
                var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("bekhar").DisableDirectStreaming();
                innerClient = new ElasticClient(settings);
            }

            return innerClient;
        }

    }
}