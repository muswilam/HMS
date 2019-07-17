using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Services;
using HMS.Areas.Dashboard.ViewModels;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccommodationsController : Controller
    {
        AccommodationsService AServices = new AccommodationsService();

        // GET: Dashboard/Accommodations
        public ActionResult Index(string searchTerm)
        {
            AccommodationsListingModel model = new AccommodationsListingModel();

            model.Accommodations = AServices.GetAccommodationsBySearch(searchTerm);

            model.SearchByName = searchTerm;

            return View(model);
        }
    }
}