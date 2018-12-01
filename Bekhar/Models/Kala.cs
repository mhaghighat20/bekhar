using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Kala
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? Price { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }

    }
}