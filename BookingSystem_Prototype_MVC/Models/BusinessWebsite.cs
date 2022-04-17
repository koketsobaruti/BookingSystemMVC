using BookingSystem_Prototype_MVC.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class BusinessWebsite
    {
        [Required]
        public virtual string ID { get; set; }

        [Required]
        public string Website { get; set; }

        [ForeignKey("ID")]
        public virtual Business Business { get; set; }
    }
}
