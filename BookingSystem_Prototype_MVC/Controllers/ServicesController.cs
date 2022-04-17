using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem_Prototype_MVC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDBContext _db;

        //model definition
        BusinessServices businessServices;


        //create an instance of db context for dependency injection
        public ServicesController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult ViewServices()
        {
            string id = "te41527";
            //TempData["busId"].ToString();
            if (id == null)
            {
                return NotFound();
            }
            //LINQ to return a list of businessService objecjects that have the same businessId
            IEnumerable<BusinessServices> objList = _db.BusinessServices.Where(service => service.ID.Equals(id));
            ////IEnumerable<BusinessServices> objList2 = _db.BusinessServices; 
            ////businessServices = new BusinessServices();

            return View(objList);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        [HttpPost]
        //a built in mechanism in forms that applies an antiforgerytoken, verifying that the post
        //request has not been tampered with (security)
        [ValidateAntiForgeryToken]
        public IActionResult AddService(BusinessServices obj)
        {
            obj.ID = TempData["busId"].ToString();
            if (ModelState.IsValid)
            {
                _db.BusinessServices.Add(obj);
                _db.SaveChanges();
            }

            TempData["busId"] = obj.ID;

            return RedirectToAction("ViewServices");
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IActionResult AddService()
        {
            return View();
        }
    }
}
