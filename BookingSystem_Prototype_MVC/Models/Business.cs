using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class Business
    {
        [Key]
        public string ID { get; set; }

        [Required(ErrorMessage = "Required")]
        //display the name of attribute 
        [DisplayName("Business Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "You must enter valid characters.")]
        //The Regular expression will allow only Numbers (Digits)
        //and the length will be exact 8 digits
        [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Please enter a valid number.")]
        public int Phone { get; set; }

        //not required because some businesses have one line
        [DataType(DataType.PhoneNumber, ErrorMessage = "You must enter valid characters.")]
        //The Regular expression will allow only Numbers (Digits)
        //and the length will be exact 8 digits
        [RegularExpression(@"^([0-9]{7})$", ErrorMessage = "Please enter a valid number.")]
        public int Telephone { get; set; }

        //needed as the bookings are to managed through email or cellphone
        [Required(ErrorMessage = "Required")]
        //validates the format of the email
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        [DisplayName("Business Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }

        //ADDED THEM HERE TO DISPLAY ALL THE BUSINESS INFORMATION IN THE MODEL
        [DisplayName("Website Link")]
        public string WebsiteLink { get; set; }

        [DisplayName("Facebook Account")]
        public string FacebookLink { get; set; }

        [DisplayName("Instagram Account")]
        public string InstagramLink { get; set; }

        [DisplayName("Whatsapp Number")]
        public string WhatsappLink { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload a profile picture for your business (optional).")]
        public IFormFile ImageFile { get; set; }

        [Required]
        /// <summary>
        /// At least one lower case letter,
        ///At least one upper case letter,
        ///At least special character,
        ///At least one number
        ///At least 8 characters length
        /// </summary>
        [RegularExpression(@"^(?=.*[!@#$%^()_-]{1})[a-zA-Z!@#$%^()_-]{8,12}$", ErrorMessage = "Password must contain: Minimum 4 to 8 characters and at least 1 Special Character")]
        public string Password { get; set; }

        [NotMapped]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [RegularExpression(@"^(?=.*[!@#$%^()_-]{1})[a-zA-Z!@#$%^()_-]{8,12}$", ErrorMessage = "Password must contain: Minimum 4 to 8 characters and at least 1 Special Character")]
        public string ConfirmPassword { get; set; }
    }
}
