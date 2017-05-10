using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpfulFriend.Models
{
    public class JobOffers
    {
        public int ID { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        public string Location { get; set; }
        [Display(Name = "Job Date")]
        [DataType(DataType.Date)]
        public DateTime JobDate { get; set; }
        public double Price { get; set; }
        [Display(Name = "Estimated time")]
        public double EstTime { get; set; }
        public string Category { get; set; }
    }
}
