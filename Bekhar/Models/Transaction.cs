using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public long Amount { get; set; }
    }
}