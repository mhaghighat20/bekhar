using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Kharid : ElasticDocument
    {
        [Display(Name = "زمان معامله")]
        public DateTime CreationTime { get; set; }

        [Display(Name="#")]
        public string KalaId { get; set; }

        [Display(Name = "خریدار")]
        public string KharidarUsername { get; set; }
        [Display(Name = "فروشنده")]
        public string ForoshandeUsername { get; set; }

        [Display(Name = "وضعیت معامله")]
        public KharidState State { get; set; }

        [JsonIgnore]
        public bool canBeApproved;

        [Display(Name = "نوع معامله")]
        public ModelType DataType { get; set; } = ModelType.Kharid;
    }

    public enum KharidState : byte
    {
        // دقت کنید خریدار اپروو می کند
        WaitForApprove,
        Completed
    }

}