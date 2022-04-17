using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models.BusinessModels;
using BookingSystem_Prototype_MVC.Models.BusinessModels.Logic;
using BookingSystem_Prototype_MVC.Models.ServiceModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomClassLibrary;
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
        BusinessServices businessServices;
        BusinessLogic businessLogic = new BusinessLogic();
        IdentityGenerator identityGenerator = new IdentityGenerator();


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
                //var ID = TempData["busId"].ToString();
                var ID = "un33810";
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
                System.Diagnostics.Debug.WriteLine("BUSINESS CONTROLLER, ViewBusiness():" + ex.Message);
            }
                
            //get the list of businesses registered
            //IEnumerable<Business> objList = _db.Business;

            //return the output
            return View(obj);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        
        
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /*public IActionResult AddServices(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //LINQ to return a list of businessService objecjects that have the same businessId
            IEnumerable<BusinessServices> objList = _db.BusinessServices.Where(service => service.Equals(id));
            ////IEnumerable<BusinessServices> objList2 = _db.BusinessServices; 
            ////businessServices = new BusinessServices();

            return View(objList);
        }*/
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
            //var busID = "";
            //check whether there is input. Return N/A if there is none
            obj.WhatsappLink = CheckInput(obj.WhatsappLink);
            obj.FacebookLink = CheckInput(obj.FacebookLink);
            obj.InstagramLink = CheckInput(obj.InstagramLink);
            obj.WebsiteLink = CheckInput(obj.WebsiteLink);
             
            //check if all the validations applied are successful
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var location = "Images";

                //generate a filename
                obj.ImageName = GenerateFileName(obj.ImageFile.FileName);

                string path = ReturnImagePath(wwwRootPath, location, obj.ImageName);

                //generate a random business ID using the businesslogic class
                obj.ID = identityGenerator.GenerateID(obj.Name, 10000, 99999, 0, 2);
                //busID = businessLogic.GenerateBusinessID(obj.Name);

                //create a hashed password
                obj.Password = businessLogic.GenerateHash(obj.Password);

                /*get the name of the file without the extention(png,pdf)
                ///string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);

                //get the extention of the image uploaded
                ///string extension = Path.GetExtension(obj.ImageFile.FileName);*/

                //generate a unique image name using the name of the file+year-month.seconds-milliseconds + extention of the file assign the new unique name to the filename object
                ///obj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                //generate a path using the path of the wwwRootPath folder, the directory of the file named image and the name of the file being uploaded
                ///string path = Path.Combine(wwwRootPath + "/Images/", obj.ImageName);


                //copy the uploaded file into the root path
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await obj.ImageFile.CopyToAsync(fileStream);
                }

                //pass the object 
                _db.Business.Add(obj);

                //add the object to the database
                _db.SaveChanges();
                //return RedirectToAction("Socials", "AddSocials");
            }

            //pass businessID as the parameter to the socials controller
            TempData["busId"] = obj.ID;

            //return RedirectToAction("ViewBusiness");
            return RedirectToAction("AddService");

            //ViewBag.ID = busID;
            //ViewBag.BusID = busID;

            //this redirects to the specified controller and view
            //return RedirectToAction("ViewBusiness", business);
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public string GenerateFileName(string objfileName) {

            //get the name of the file without the extention(png,pdf)
            string fileName = Path.GetFileNameWithoutExtension(objfileName);

            //get the extention of the image uploaded
            string extension = Path.GetExtension(objfileName);

            //generate a unique image name using the name of the file + year - month.seconds - milliseconds + extention of
            //the file
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            return fileName;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        
        public string ReturnImagePath(string wwwRootPath,string location, string fileName)
        {
            //generate a path using the path of the wwwRootPath folder, the directory of the file named image
            //and the name of the file being uploaded
            string path = Path.Combine(wwwRootPath + "/" + location + "/" + fileName);

            return path;
        }

        /*--------------------------------------------------------------------------------------00oo END OF FILE oo00-------------------------------------------------------------------------------------------------*/

    }
}
