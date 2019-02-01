using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Kharid //: ElasticDocument
    {
        public string Id { get; set; }

        public string KalaId { get; set; }
        public string KharidarUsername { get; set; }
        public string ForoshandeUsername { get; set; }

        public KharidState State { get; set; }

        public ModelType DataType { get; set; } = ModelType.Kharid;
    }

    public enum KharidState : byte
    {
        // دقت کنید خریدار اپروو می کند
        WaitForApprove,
        Completed
    }

}