using BookingSystem_Prototype_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Data
{
    public class ApplicationDBContext : DbContext
    {
        //create a DBContext for the project
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        //create properties to push them to the db

        /// <summary>
        /// Add Business models to the database
        /// </summary>
        public DbSet<Business> Business { get; set; }
       /* public DbSet<BusinessFacebook> BusinessFacebook { get; set; }
        public DbSet<BusinessInstagram> BusinessInstagram { get; set; }
        public DbSet<BusinessWebsite> BusinessWebsite { get; set; }
        public DbSet<BusinessSocials> BusinessSocials { get; set; }

        
        //not sure how to add whatsapp yet
        public DbSet<BusinessWhatsapp> BusinessWhatsapp { get; set; }

        public DbSet<Customer> Customer { get; set; }
        */
    }
}
