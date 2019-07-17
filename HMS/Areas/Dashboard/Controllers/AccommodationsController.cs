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
    public class AccommodationsController : Controller
    {
        AccommodationsService AServices = new AccommodationsService();
        AccommodationPackagesService APServices = new AccommodationPackagesService();

        // GET: Dashboard/Accommodations
        public ActionResult Index(string searchTerm)
        {
            AccommodationsListingModel model = new AccommodationsListingModel();

            model.Accommodations = AServices.GetAccommodationsBySearch(searchTerm);

            model.SearchByName = searchTerm;

            return View(model);
        }

        // create accommodation
        [HttpGet]
        public ActionResult Action()
        {
            AccommodationsActionModel model = new AccommodationsActionModel();

            model.AccommodationPackages = APServices.GetAllAccommodationPackages();

            return PartialView("_Action",model);
        }

        // post create accommodation 
        [HttpPost]
        public JsonResult Action(AccommodationsActionModel formModel)
        {
            Accommodation accommodation = new Accommodation();

            JsonResult json = new JsonResult();

            accommodation.Name = formModel.Name;
            accommodation.Description = formModel.Description;
            accommodation.AccommodationPackageId = formModel.AccommodationPackageId;

            bool result = AServices.SaveAccommodation(accommodation);

            if (result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Unable to create this accommodation" };

            return json;
        }
    }
}