using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.ViewModels;
using HMS.Services;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        AccommodationTypesService ATServices = new AccommodationTypesService();
        AccommodationPackagesService APServices = new AccommodationPackagesService();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();
            model.AccommodationPackages = APServices.GetAllAccommodationPackages();

            return View(model);
        }
    }
}