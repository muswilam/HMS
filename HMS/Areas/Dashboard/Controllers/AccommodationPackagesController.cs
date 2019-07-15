using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Services;
using HMS.Areas.Dashboard.ViewModels;

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
    }
}