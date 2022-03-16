using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Controllers
{
    public class SocialsController : Controller
    {
        private readonly ApplicationDBContext _db;
        BusinessSocials socials;
        Business bus;
        //create an instance of db context for dependency injection
        public SocialsController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult AddSocials()
        {
            return View();
        }

        [HttpPost]
        //a built in mechanism in forms that applies an antiforgerytoken, verifying that the post
        //request has not been tampered with (security)
        [ValidateAntiForgeryToken]
        public IActionResult AddSocials(BusinessSocials obj)
        {
            //get the businessID passed by the Business controller
            var busID = TempData["busId"].ToString();
            System.Diagnostics.Debug.WriteLine("BUSINESS ID ADDSOCIALS CONTROLLER:" + busID);

            var facebook = obj.FacebookLink;
            var instagram = obj.InstagramLink;
            var website = obj.WebsiteLink;
            var whatsapp = obj.WhatsappLink;

            if ((String.IsNullOrEmpty(facebook)) && (String.IsNullOrEmpty(instagram))
                && (String.IsNullOrEmpty(website)) && (String.IsNullOrEmpty(whatsapp.ToString())))
            {
                return RedirectToAction("AddSocials", "Socials");
            }

            socials= new BusinessSocials(){
                ID = busID,
                FacebookLink = obj.FacebookLink,
                InstagramLink = obj.InstagramLink,
                WebsiteLink = obj.WebsiteLink,
                WhatsappLink = obj.WhatsappLink
            };

            return View();
        }
    }
}
