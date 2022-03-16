using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class BusinessWhatsapp
    {
        [Required]
        public virtual string ID { get; set; }

        [Required]
        public int Whatsapp_Contact { get; set; }

        [ForeignKey("ID")]
        public virtual Business Business { get; set; }
    }
}
