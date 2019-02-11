using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Transaction : ElasticDocument
    {
        public string Username { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "مبلغ")]
        public long Amount { get; set; }

        public PurposeType Purpose { get; set; }

        public ModelType DataType { get; set; } = ModelType.Transaction;
    }

    public enum ModelType : byte
    {
        Kala,
        Transaction,
        Kharid,
        Mozayede
    }

    public enum PurposeType : byte
    {
        ChargeAccount,
        Buy,
        Sell
    }

    public class ElasticDocument
    {
        public string Id { get; set; }
    }
}