using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class BusinessSocials
    {
        [Required]
        public virtual string ID { get; set; }

        //display the name of attribute 
        [DisplayName("Website Link")]
        public string WebsiteLink { get; set; }

        [DisplayName("Facebook Account")]
        public string FacebookLink { get; set; }

        [DisplayName("Instagram Account")]
        public string InstagramLink { get; set; }

        [DisplayName("Whatsapp Number")]
        public int WhatsappLink { get; set; }

        [ForeignKey("ID")]
        public virtual Business Business { get; set; }
    }
}
