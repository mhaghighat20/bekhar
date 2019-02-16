using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Views.Kala
{
    /// <summary>
    /// Summary description for CategoryHandler
    /// </summary>
    public class CategoryHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}