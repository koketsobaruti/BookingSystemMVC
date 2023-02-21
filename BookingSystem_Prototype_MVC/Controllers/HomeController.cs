using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models;
using BookingSystem_Prototype_MVC.Models.BusinessModels;
using BookingSystem_Prototype_MVC.Models.BusinessModels.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        BusinessLogic businessLogic = new BusinessLogic();

        //create an instance of db context for dependency injection
        public HomeController(ApplicationDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //a built in mechanism in forms that applies an antiforgerytoken, verifying that the post
        //request has not been tampered with (security)
        [ValidateAntiForgeryToken]
        //recieve the object to add to the database
        public IActionResult Index(Business business)
        {
            //check if all the validations applied are successful
            
                Business obj = new Business();
                string pass = business.Password;
                string userEmail = business.Email;
            //obj = _db.Business.Find("hairshop@gmail.com");
            try
            {
                obj = _db.Business.Single(a => a.Email.Equals(userEmail));
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("HOMECONTROLLER:LOGIN ERROR MESSAGE" + ex.ToString());
            }
                
                if (obj is null)
                {
                    return NotFound();
                }
                else
                {
                    string hashPass = businessLogic.GenerateHash(pass);
                
                    if (obj.Password.Equals(hashPass))
                    {
                        System.Diagnostics.Debug.WriteLine("HOMECONTROLLER: USER VALID" );
                        //get the user ID and send it to the next page
                        string userId = obj.ID;
                        TempData["userId"] = userId;
                        return RedirectToAction("ViewBusiness", "Businesses");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("HOMECONTROLLER: USER NOT VALID");
                        return NotFound();
                    }
                }
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
