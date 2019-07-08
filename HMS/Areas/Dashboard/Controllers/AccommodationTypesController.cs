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
        public ActionResult Action()
        {
            AccommodationTypesActionModel model = new AccommodationTypesActionModel();

            return PartialView("_Action",model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationTypesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            AccommodationType accommodationType = new AccommodationType();

            accommodationType.Name = formModel.Name;
            accommodationType.Description = formModel.Description;

            var result = ATServices.SaveAccommodationTypes(accommodationType);

            if(result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to add Accommodation Type." };
            }

            return json;
        }
    }
}