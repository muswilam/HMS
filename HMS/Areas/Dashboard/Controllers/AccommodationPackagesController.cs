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
        // GET: Dashboard/AccommodationPackages
        public ActionResult Index(string searchTerm)
        {
            AccommodationPackagesListingModel model = new AccommodationPackagesListingModel();

            model.SearchTerm = searchTerm;

            model.AccommodationPackages = APServices.GetAccommodationPackagesBySearch(searchTerm);

            return View(model);
        }

        // Action control
        [HttpGet]
        public ActionResult Action()
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();
            return PartialView("_Action",model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationPackagesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            AccommodationPackage ap = new AccommodationPackage();

            ap.Name = formModel.Name;
            ap.NoOfRoom = formModel.NoOfRoom;
            ap.FeePerNight = formModel.FeePerNight;
            ap.AccommodationTypeId = 13;

            bool result = APServices.AddAccommodationPackage(ap);

            if(result)
            {
                json.Data = new { Success = true }; 
            }else
            {
                json.Data = new { Success = false, Message = "Unable to create accommodation package" };
            }

            return json;
        }
    }
}