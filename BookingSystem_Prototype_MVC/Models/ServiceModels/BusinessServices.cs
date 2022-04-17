using BookingSystem_Prototype_MVC.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Models.ServiceModels
{
    public class BusinessServices
    {
        [Key]
        public int ServiceID { get; set; }

        public virtual string ID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string ServiceName { get; set; }

        [Required]
        [DisplayName("Description")]
        public string ServiceDescription { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [ForeignKey("ID")]
        public virtual Business Business { get; set; }
    }
}
