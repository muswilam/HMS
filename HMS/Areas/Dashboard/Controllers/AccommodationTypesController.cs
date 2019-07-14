using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.Dashboard.ViewModels;
using HMS.Services;
using HMS.Entities;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccommodationTypesController : Controller
    {
        AccommodationTypesService ATServices = new AccommodationTypesService();
        // GET: Dashboard/AccommodationTypes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AccommodationTypesListingModel model = new AccommodationTypesListingModel();

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();
            
            return PartialView("_Listing",model);
        }

        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccommodationTypesActionModel model = new AccommodationTypesActionModel();

            if (id.HasValue) //edit record
            {
                var accommodation = ATServices.GetAccommodationTypeById(id.Value); //note , value becuz it's nullable

                model.Id = accommodation.Id;
                model.Name = accommodation.Name;
                model.Description = accommodation.Description;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationTypesActionModel formModel)
        {
            JsonResult json = new JsonResult();
            bool result = false;

            if(formModel.Id > 0) //edit record
            {
                var accommodationType = ATServices.GetAccommodationTypeById(formModel.Id);

                accommodationType.Name = formModel.Name;
                accommodationType.Description = formModel.Description;

                result = ATServices.UpdateAccommodationType(accommodationType);
            }
            else //create a new record
            {
                AccommodationType accommodationType = new AccommodationType();

                accommodationType.Name = formModel.Name;
                accommodationType.Description = formModel.Description;

                result = ATServices.SaveAccommodationType(accommodationType);
            }
            
            if(result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accommodation Type." };
            }

            return json;
        }

        //Delete Accommodation Type
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccommodationTypesActionModel model = new AccommodationTypesActionModel();

            var accommodation = ATServices.GetAccommodationTypeById(id);

            model.Id = accommodation.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationTypesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            var accommodationType = ATServices.GetAccommodationTypeById(formModel.Id);

            bool result = ATServices.DeleteAccommodationType(accommodationType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to delete this Accommodation Type." };
            }

            return json;
        }
    }
}