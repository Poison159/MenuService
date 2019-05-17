using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MenusService.Models
{
    public class Resturant
    {
        public Resturant()
        {
            imgPath = "~/Content/user.png";
            meals = new List<Meal>();
            guid = Guid.NewGuid().ToString().Split('-').First();
        }

        [Required]
        public int id { get; set; }
        [Required]
        public string guid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [Display(Name = "resImage")]
        public string imgPath { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageUpload { get; set; }
        public List<Meal> meals { get; set; }
    }
}