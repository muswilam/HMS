﻿using System;
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
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationPackagesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            bool result = false;

            if (formModel.Id == 0) //create
            {
                AccommodationPackage ap = new AccommodationPackage();

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = 13;

                result = APServices.AddAccommodationPackage(ap);
            }
            else
            {
                var ap = APServices.GetAccommodationPackageById(formModel.Id);

                ap.Name = formModel.Name;
                ap.NoOfRoom = formModel.NoOfRoom;
                ap.FeePerNight = formModel.FeePerNight;
                ap.AccommodationTypeId = 13;

                result = APServices.UpdateAccommodationPackage(ap);
            }
          
            if(result)
            {
                json.Data = new { Success = true }; 
            }else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on accommodation package" };
            }

            return json;
        }
    }
}