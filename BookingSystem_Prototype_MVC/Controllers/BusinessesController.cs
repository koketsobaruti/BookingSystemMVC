using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models;
using BookingSystem_Prototype_MVC.Models.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Controllers
{
    public class BusinessesController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        //model definition
        Business business;
        BusinessLogic businessLogic = new BusinessLogic();


        //create an instance of db context for dependency injection
        public BusinessesController(ApplicationDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// GET FOR VIEING THE BUSINESS DETAILS
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewBusiness()
        {
            Business obj = new Business();
            try
            {
                //fetch the value passed from other action
                var ID = "ca99791";
                    //TempData["busId"].ToString();
                //System.Diagnostics.Debug.WriteLine("BUSINESS ID ADDSOCIALS CONTROLLER:" + ID);

                if (ID is null)
                {
                    return NotFound();
                }

                obj = _db.Business.Find(ID);
                if (obj is null)
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ADDSOCIALS CONTROLLER, ViewBusiness():" + ex.Message);
            }
                
            //get the list of businesses registered
            //IEnumerable<Business> objList = _db.Business;

            //return the output
            return View(obj);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public IActionResult AddBusiness()
        {
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// POST METHOD USED TO ADD A NEW BUSINESS TO THE DATABASE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        //a built in mechanism in forms that applies an antiforgerytoken, verifying that the post
        //request has not been tampered with (security)
        [ValidateAntiForgeryToken]
        //recieve the object to add to the database
        public async Task<IActionResult> AddBusiness(Business obj)
        {
            var busID = "";
            //check whether there is input. Return N/A if there is none
            var whatsappInput = CheckInput(obj.WhatsappLink);
            var facebookInput = CheckInput(obj.FacebookLink);
            var instagramInput = CheckInput(obj.InstagramLink);
            var websiteInput = CheckInput(obj.WebsiteLink);
             
            //check if all the validations applied are successful
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;

                //get the name of the file without the extention(png,pdf)
                string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);

                //get the extention of the image uploaded
                string extension = Path.GetExtension(obj.ImageFile.FileName);

                //generate a unique image name using the name of the file+year-month.seconds-milliseconds + extention of
                //the file
                //assign the new unique name to the filename object
                obj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                //generate a path using the path of the wwwRootPath folder, the directory of the file named image
                //and the name of the file being uploaded
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);


                //generate a random business ID using the businesslogic class
                busID = businessLogic.GenerateID(obj.Name);
                System.Diagnostics.Debug.WriteLine("BUSINESS ID BUSINESS CONTROLLER:" + busID);
                //create a new Business object containing the ID generated
                business = new Business()
                {
                    ID = busID,
                    Name = obj.Name,
                    Email = obj.Email,
                    Address = obj.Address,
                    City = obj.City,
                    Country = obj.Country,
                    Phone = obj.Phone,
                    Telephone = obj.Telephone,
                    WebsiteLink = websiteInput,
                    InstagramLink = instagramInput,
                    WhatsappLink = whatsappInput,
                    FacebookLink = facebookInput,
                    ImageName = obj.ImageName,
                    Password = obj.Password
                };

                //copy the uploaded file into the root path
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await obj.ImageFile.CopyToAsync(fileStream);
                }

                //pass the object 
                _db.Business.Add(business);
                //add the object to the database
                _db.SaveChanges();
                //return RedirectToAction("Socials", "AddSocials");
            }

            //pass businessID as the parameter to the socials controller
            TempData["busId"] = busID;
            //ViewBag.ID = busID;
            //ViewBag.BusID = busID;

            //this redirects to the specified controller and view
            //return RedirectToAction("ViewBusiness", business);
            return RedirectToAction("ViewBusiness");
            //return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public string CheckInput(string obj)
        {
            var returnVal = "";

            if (obj is null)
            {
                returnVal = "N/A";
            }
            else
            {
                returnVal = obj;
            }

            return returnVal;
        }
    /*--------------------------------------------------------------------------------------00oo END OF FILE oo00-------------------------------------------------------------------------------------------------*/
   
    }
}
