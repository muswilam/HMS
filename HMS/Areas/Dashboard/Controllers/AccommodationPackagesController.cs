using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Services;
using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccommodationPackagesController : Controller
    {
        AccommodationPackagesService APServices = new AccommodationPackagesService();
        AccommodationTypesService ATServices = new AccommodationTypesService();

        // GET: Dashboard/AccommodationPackages
        public ActionResult Index(string searchTerm , int? accommodationTypeId)
        {
            AccommodationPackagesListingModel model = new AccommodationPackagesListingModel();

            model.SearchByName = searchTerm;
            model.SearchByAccommodationTypeId = accommodationTypeId;

            model.AccommodationPackages = APServices.GetAccommodationPackagesBySearch(searchTerm , accommodationTypeId);

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();

            return View(model);
        }

        // Create and Edite (Get view) 
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            if(id.HasValue) // edit
            {
                var apFromDB = APServices.GetAccommodationPackageById(id.Value);
                model.Id = apFromDB.Id;
                model.Name = apFromDB.Name;
                model.NoOfRoom = apFromDB.NoOfRoom;
                model.FeePerNight = apFromDB.FeePerNight;
                model.AccommodationTypeId = apFromDB.AccommodationTypeId;
            }

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();

            return PartialView("_Action", model);
        }

        // Create and Edite (Post view)
        [HttpPost]
        public JsonResult Action(AccommodationPackagesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            bool result = false;

            if (formModel.Id == 0) //create
            {
                AccommodationPackage ap = new AccommodationPackage();

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = formModel.AccommodationTypeId;

                result = APServices.AddAccommodationPackage(ap);
            }
            else
            {
                var ap = APServices.GetAccommodationPackageById(formModel.Id);

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = formModel.AccommodationTypeId;

                result = APServices.UpdateAccommodationPackage(ap);
            }
          
            if(result)
                json.Data = new { success = true }; 
            else
                json.Data = new { success = false, message = "Unable to perform action on accommodation package" };

            return json;
        }

        // Delete 
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            var apFromDB = APServices.GetAccommodationPackageById(id);

            model.Id = apFromDB.Id;
            model.Name = apFromDB.Name;

            return PartialView("_Delete",model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationPackagesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            var apFromDb = APServices.GetAccommodationPackageById(formModel.Id);

            bool result = APServices.DeleteAccommodationPackage(apFromDb);

            if(result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Unable to delete this accommodation package" };

            return json;
        }
    }
}