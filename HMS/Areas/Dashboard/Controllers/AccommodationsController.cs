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

        // create and edit (get)
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccommodationsActionModel model = new AccommodationsActionModel();

            if(id != 0) // edit
            {
                var accommodation = AServices.GetAccommodationById(id.Value);

                model.Id = accommodation.Id;
                model.Name = accommodation.Name;
                model.Description = accommodation.Description;
                model.AccommodationPackageId = accommodation.AccommodationPackageId;

            }

            model.AccommodationPackages = APServices.GetAllAccommodationPackages();

            return PartialView("_Action",model);
        }

        // create and edit (post) 
        [HttpPost]
        public JsonResult Action(AccommodationsActionModel formModel)
        {
            JsonResult json = new JsonResult();
            bool result = false;

            if(formModel.Id == 0) //create
            {
                //make instance of accommodation then fill it with the new data
                Accommodation accommodation = new Accommodation();

                accommodation.Name = formModel.Name;
                accommodation.Description = formModel.Description;
                accommodation.AccommodationPackageId = formModel.AccommodationPackageId;

                result = AServices.SaveAccommodation(accommodation);
            }
            else //edit
            {
                //get accommodation from db then modified it
                var accommodation = AServices.GetAccommodationById(formModel.Id);

                accommodation.Name = formModel.Name;
                accommodation.Description = formModel.Description;
                accommodation.AccommodationPackageId = formModel.AccommodationPackageId;

                result = AServices.UpdateAccommodation(accommodation);
            }


            if (result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Unable to create this accommodation" };

            return json;
        }
    }
}