using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Username { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "مبلغ")]
        public long Amount { get; set; }

        public ModelType DataType { get; set; } = ModelType.Transaction;
    }

    public enum ModelType : byte
    {
        Kala,
        Transaction,
        Kharid
    }
}