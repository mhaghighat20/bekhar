using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bekhar.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Bekhar.Elastic.Tests
{
    [TestClass()]
    public class ElasticEngineTests
    {
        [TestMethod()]
        public void GetQueryTest()
        {
            var keyword = "salam";
            var searchParameter = new SearchParameter()
            {
                Keyword = keyword
            };

            var query = ElasticEngine.GetQuery(searchParameter);

            QueryContainer expectedQuery = new MultiMatchQuery() { Query = searchParameter.Keyword, Fields = new[] { "description", "name" } };
            var visitor = new QueryVisitor()
            {
                Scope = VisitorScope.Query
            };

            query.Accept(visitor);

        }
    }
}