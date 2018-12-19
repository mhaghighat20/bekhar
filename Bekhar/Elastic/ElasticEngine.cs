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
            EsClient.GetInstance().Refresh("bekhar");
        }

        internal static List<Kala> GetAllKala()
        {
            var response = EsClient.GetInstance().Search<Kala>();
            ValidateResponse(response);

            var result = response.Documents.ToList();
            return AssignIds(response, result);
        }

        internal static List<Kala> GetKalaByKeyword(string keyword)
        {
            var response = EsClient.GetInstance().Search<Kala>(s =>
                s.Query(
                    q => q.MultiMatch(
                        m => m.Query(keyword).Fields(
                new List<string>() { "description", "name" }.ToArray()
                ))));

            ValidateResponse(response);

            var result = response.Documents.ToList();
            return AssignIds(response, result);
        }

        private static List<Kala> AssignIds(ISearchResponse<Kala> response, List<Kala> result)
        {
            int counter = 0;
            foreach (var id in response.Hits.Select(x => x.Id))
            {
                result[counter++].Id = id;
            }

            return result;
        }

        internal static Kala GetKalaById(string id)
        {
            var response = EsClient.GetInstance().Search<Kala>(s => s.Query(q => q.Ids(ids => ids.Values(new List<Id>() { id }))));
            ValidateResponse(response);

            return response.Hits.First().Source;
        }

        private static void ValidateResponse(IResponse reponse)
        {
            if (!reponse.IsValid)
                throw new Exception(reponse.DebugInformation);
        }
    }
}