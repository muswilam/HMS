using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Services;
using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.ViewModels;
using HMS.Areas.Dashboard.Common;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccommodationPackagesController : Controller
    {
        AccommodationPackagesService APServices = new AccommodationPackagesService();
        AccommodationTypesService ATServices = new AccommodationTypesService();
        DashboardService DBServices = new DashboardService();

        // GET: Dashboard/AccommodationPackages
        public ActionResult Index(string searchTerm , int? accommodationTypeId , int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            AccommodationPackagesListingModel model = new AccommodationPackagesListingModel();

            model.SearchByName = searchTerm;
            model.SearchByAccommodationTypeId = accommodationTypeId;

            model.AccommodationPackages = APServices.GetAccommodationPackagesBySearch(searchTerm , accommodationTypeId , page.Value , recordSize);

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();

            var totalRecords = APServices.GetAccommodationPackagesCount(searchTerm,accommodationTypeId);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        // Create and Edite (Get view) 
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            if(id.HasValue) // edit
            {
                var apFromDB = APServices.GetAccommodationPackageById(id.Value);
                model.Id = apFromDB.Id;
                model.Name = apFromDB.Name;
                model.NoOfRoom = apFromDB.NoOfRoom;
                model.FeePerNight = apFromDB.FeePerNight;
                model.AccommodationTypeId = apFromDB.AccommodationTypeId;
            }

            model.AccommodationTypes = ATServices.GetAllAccommodationTypes();

            return PartialView("_Action", model);
        }

        // Create and Edite (Post view)
        [HttpPost]
        public JsonResult Action(AccommodationPackagesActionModel formModel)
        {
            bool result = false;

            if (formModel.Id == 0) //create
            {
                AccommodationPackage ap = new AccommodationPackage();

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = formModel.AccommodationTypeId;

                //model.pictureIds = "22,23,24" => ["22","23","24"] => [22,23,24]
                var picsIds = formModel.PictureIds.Split(',').Select(int.Parse).ToList();

                var pictures = DBServices.GetPicturesByIds(picsIds);

                ap.AccommodationPackagePictures = new List<AccommodationPackagePicture>();
                ap.AccommodationPackagePictures.AddRange(pictures.Select(p => new AccommodationPackagePicture()
                {
                    PictureId = p.Id
                }));

                result = APServices.AddAccommodationPackage(ap);
            }
            else
            {
                var ap = APServices.GetAccommodationPackageById(formModel.Id);

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = formModel.AccommodationTypeId;

                result = APServices.UpdateAccommodationPackage(ap);
            }

            return JsonDataResult.Result(result);
        }

        // Delete 
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            var apFromDB = APServices.GetAccommodationPackageById(id);

            model.Id = apFromDB.Id;
            model.Name = apFromDB.Name;

            return PartialView("_Delete",model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationPackagesActionModel formModel)
        {
            var apFromDb = APServices.GetAccommodationPackageById(formModel.Id);

            bool result = APServices.DeleteAccommodationPackage(apFromDb);

            return JsonDataResult.Result(result);
        }
    }
}