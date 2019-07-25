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

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();

            return View(model);
        }
    }
}