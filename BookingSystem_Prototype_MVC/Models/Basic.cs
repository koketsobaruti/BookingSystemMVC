using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models
{
    public class Basic
    {
        [Key]
        public string ID { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public int telephone { get; set; }
        public string email { get; set; }
    }
}
