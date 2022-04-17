using BookingSystem_Prototype_MVC.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class BusinessFacebook
    {
        [Required]
        public virtual string ID { get; set; }


        [DataType(DataType.PhoneNumber, ErrorMessage = "You must enter valid characters.")]
        //The Regular expression will allow only Numbers (Digits)
        //and the length will be exact 8 digits
        [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Please enter a valid number.")]

        [Required]
        public string Fb_Name { get; set; }

        [ForeignKey("ID")]
        public virtual Business Business { get; set; }
    }
}
