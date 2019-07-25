using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class AccommodationsController : Controller
    {
        AccommodationTypesService ATServices = new AccommodationTypesService();
        AccommodationPackagesService APServices = new AccommodationPackagesService();
        AccommodationsService AServices = new AccommodationsService();

        public ActionResult Index(int accommodationTypeId , int? accommodationPackageId)
        {
            AccommodationsViewModel model = new AccommodationsViewModel();

            model.AccommodationType = ATServices.GetAccommodationTypeById(accommodationTypeId);

            model.AccommodationPackages = APServices.GetAllAccommodationPackagesByAccommodationType(accommodationTypeId);

            model.SelectedAccommodationPackageId  = accommodationPackageId.HasValue ? accommodationPackageId.Value : model.AccommodationPackages.First().Id;

            model.Accommodations = AServices.GetAllAccommodationsByAccommodationPackage(model.SelectedAccommodationPackageId);
            return View(model);
        }
    }
}