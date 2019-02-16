using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Kala : ElasticDocument
    {

        [Display(Name = "عنوان کالا")]
        [Required(ErrorMessage = "وارد کردن عنوان ضروری است.")]
        public string Name { get; set; }

        [Display(Name = "توضیحات کالا")]
        public string Description { get; set; }

        [Display(Name = "قیمت کالا")]
        public long? Price { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن ایمیل ضروری است.")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر")]
        public string Email { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreationTime { get; set; }
        public string Category { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "محله")]
        public string Location { get; set; }

        public string Username { get; set; }

        public DateTime? Deadline { get; set; }

        [JsonIgnore]
        public long? DeadlineTimeStamp => ((Nullable<DateTimeOffset>)Deadline)?.ToUnixTimeMilliseconds();

        [Display(Name="تاریخ پایان مزایده")]
        public string DeadlineDate { get; set; }

        [Display(Name = "زمان پایان مزایده")]
        public string DeadlineTime { get; set; }

        public ModelType DataType { get; set; } = ModelType.Kala;

        //public Mozayede ConvertToMozayede()
        //{
        //    return new Mozayede()
        //    {
        //        Id = Id,
        //        Category = Category,
        //        City = City,
        //        CreationTime = CreationTime,
        //        DataType = DataType,
        //        Description = Description,
        //        Email = Email,
        //        Location = Location,
        //        Mobile = Mobile,
        //        Name = Name,
        //        Price = Price,
        //        Username = Username
        //    };
        //}
    }
}