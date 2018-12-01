using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Kala
    {
        public string Id { get; set; }

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
        public DateTime CreationTime { get; set; }

    }
}