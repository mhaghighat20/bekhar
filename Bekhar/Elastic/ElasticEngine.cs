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

        internal static void DeleteKala(string id)
        {
            var query = new IdsQuery()
            {
                Values = new List<Id>()
                {
                    id
                }
            };

            var response = EsClient.GetInstance().DeleteByQuery<Kala>(d =>
                d.Query(
                    q => query
                ));

            ValidateResponse(response);
            EsClient.GetInstance().Refresh("bekhar");
        }

        internal static List<Kala> GetKalaBySearchParam(SearchParameter searchParameter)
        {
            QueryContainer query = GetQuery(searchParameter);
            var response = EsClient.GetInstance().Search<Kala>(s =>
                s.Query(
                    q => query
                ));
            // TODO سورت بر اساس زمان به صورت نزولی اضافه شود

            ValidateResponse(response);

            var result = response.Documents.ToList();
            return AssignIds(response, result);
        }

        internal static List<Transaction> GetTransactionByUsername(string username)
        {
            var query = new MatchQuery() { Query = username, Field = "username" };
            var response = EsClient.GetInstance().Search<Transaction>(s =>
                s.Query(
                    q => query
                    ));

            ValidateResponse(response);

            var result = response.Documents.ToList();
            return result; // TODO Assign Ids
        }

        internal static void AddTranasction(Transaction transaction)
        {
            var response = EsClient.GetInstance().IndexDocument<Transaction>(transaction);
            ValidateResponse(response);
            EsClient.GetInstance().Refresh("bekhar");
        }

        public static QueryContainer GetQuery(SearchParameter searchParameter)
        {
            var result = new List<QueryContainer>();

            if (!string.IsNullOrWhiteSpace(searchParameter.Keyword))
                result.Add(new MultiMatchQuery() { Query = searchParameter.Keyword, Fields = new[] { "description", "name" }, Fuzziness = Fuzziness.EditDistance(1) });

            if (!string.IsNullOrWhiteSpace(searchParameter.Category))
                result.Add(new MatchQuery() { Query = searchParameter.Category, Field = "category" });

            if (!string.IsNullOrWhiteSpace(searchParameter.City))
                result.Add(new MatchQuery() { Query = searchParameter.City, Field = "city" });

            if (!string.IsNullOrWhiteSpace(searchParameter.Location))
                result.Add(new MatchQuery() { Query = searchParameter.Location, Field = "location" });

            if (searchParameter.PriceMin.HasValue)
                result.Add(new LongRangeQuery() { Field = "price", GreaterThanOrEqualTo = searchParameter.PriceMin, Relation = RangeRelation.Within });

            if (searchParameter.PriceMax.HasValue)
                result.Add(new LongRangeQuery() { Field = "price", LessThanOrEqualTo = searchParameter.PriceMax });

            if (!string.IsNullOrWhiteSpace(searchParameter.Username))
                result.Add(new MatchQuery() { Query = searchParameter.Username, Field = "username" });

            if (result.Count == 0)
                return new MatchAllQuery();
            else if (result.Count == 1)
                return result.First();
            else
                return new BoolQuery() { Must = result };
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
