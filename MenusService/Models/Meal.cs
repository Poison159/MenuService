using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MenusService.Models
{
    public class Meal
    {
        public Meal()
        {
            imgPath = "~/Content/user.png";
        }
        public int id { get; set; }
        public int resturantId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double price { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime endDate { get; set; }
        [Required]
        [Display(Name = "meal Image")]
        public string imgPath { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageUpload { get; set; }
    }
}