using BookingSystem_Prototype_MVC.Data;
using BookingSystem_Prototype_MVC.Models.BusinessModels;
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
        public IActionResult ViewServices(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //LINQ to return a list of businessService objecjects that have the same businessId
            IEnumerable<BusinessServices> objList = _db.BusinessServices.Where(service => service.ID.Equals(id));
            ////IEnumerable<BusinessServices> objList2 = _db.BusinessServices; 
            ////businessServices = new BusinessServices();
            ViewBag.ID = id;
            return View(objList);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        [HttpPost]
        //a built in mechanism in forms that applies an antiforgerytoken, verifying that the post
        //request has not been tampered with (security)
        [ValidateAntiForgeryToken]
        public IActionResult AddService(BusinessServices obj, string id)
        {
            //string userId = TempData["userId"].ToString();
            //obj.ID = TempData["busId"].ToString();

            _db.BusinessServices.Add(obj);
            _db.SaveChanges();

            ViewBag.ID = id;

            return View();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IActionResult AddService(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        #region    EDIT && DELETE PRODUCT/SERVICE

        /// <summary>
        /// CREATE A MODAAL POP-UP FOR UPDATING THE SERVICE/PRODUCT
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            BusinessServices service = _db.BusinessServices.Where(s => s.ServiceID == id).FirstOrDefault();
            return PartialView("_EditServicePartialView", service);
        }

        [HttpPost]
        public IActionResult Edit(BusinessServices serviceUpdate)
        {
            _db.BusinessServices.Update(serviceUpdate);
            _db.SaveChanges();
            string id = serviceUpdate.ID;
            return RedirectToAction("ViewServices", new { id = id});
        }

        /// <summary>
        /// CREATE A MODAL PUP-UP FOR DELETING THE SERVICE/PRODUCT
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            BusinessServices service = _db.BusinessServices.Where(s => s.ServiceID == id).FirstOrDefault();
            return PartialView("_DeleteServicePartialView", service);
        }

        [HttpPost]
        public IActionResult Delete(BusinessServices serviceDelete)
        {
            string id = serviceDelete.ID;
            _db.BusinessServices.Remove(serviceDelete);
            _db.SaveChanges();
            return RedirectToAction("ViewServices", new { id = id });
        }
        #endregion

    }
}
