using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    /// <summary>
    /// :Basic - use inheritance besause the Business class and Customer class
    /// both use the starting information
    /// </summary>
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }

        [Required]
        public int Phone { get; set; }
        public int Telephone { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
