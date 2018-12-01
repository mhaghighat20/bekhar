using Bekhar.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Elastic
{
    public static class ElasticEngine
    {
        public static void AddKala(Kala kala)
        {
            var response = EsClient.GetInstance().IndexDocument<Kala>(kala);
            ValidateResponse(response);
        }


        private static void ValidateResponse(IResponse reponse)
        {
            if (!reponse.IsValid)
                throw new Exception(reponse.DebugInformation);
        }
    }
}