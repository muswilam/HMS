﻿using System;
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
    public class AccommodationsController : Controller
    {
        AccommodationsService AServices = new AccommodationsService();
        AccommodationPackagesService APServices = new AccommodationPackagesService();
        DashboardService DBServices = new DashboardService();

        // GET: Dashboard/Accommodations
        public ActionResult Index(string searchTerm, int? accommodationPackageId, int? page)
        {
            AccommodationsListingModel model = new AccommodationsListingModel();

            page = page ?? 1;
            var pageSize = 10;
            var totalRecords = AServices.GetAllAccommodationsCount(searchTerm, accommodationPackageId);

            model.Accommodations = AServices.GetAccommodationsBySearchOrPackageId(searchTerm, accommodationPackageId, page.Value, pageSize);

            model.SearchByName = searchTerm;
            model.SearchByAccommodationPackageId = accommodationPackageId;
            model.AccommodationPackages = APServices.GetAllAccommodationPackages();

            model.Pager = new Pager(totalRecords, page, pageSize);

            return View(model);
        }

        // create and edit (get)
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccommodationsActionModel model = new AccommodationsActionModel();

            if (id > 0) // edit
            {
                var accommodation = AServices.GetAccommodationById(id.Value);

                model.Id = accommodation.Id;
                model.Name = accommodation.Name;
                model.Description = accommodation.Description;
                model.AccommodationPackageId = accommodation.AccommodationPackageId;
                model.AccommodationPictures = accommodation.AccommodationPictures;

            }

            model.AccommodationPackages = APServices.GetAllAccommodationPackages();

            return PartialView("_Action", model);
        }

        // create and edit (post) 
        [HttpPost]
        public JsonResult Action(AccommodationsActionModel formModel)
        {
            bool result = false;

            var picsIds = string.IsNullOrEmpty(formModel.PictureIds) ? new List<int>() : formModel.PictureIds.Split(',').Select(int.Parse).ToList();

            var pictures = DBServices.GetPicturesByIds(picsIds);

            if (formModel.Id == 0) //create
            {
                //make instance of accommodation then fill it with the new data
                Accommodation accommodation = new Accommodation();

                accommodation.Name = formModel.Name;
                accommodation.Description = formModel.Description;
                accommodation.AccommodationPackageId = formModel.AccommodationPackageId;

                accommodation.AccommodationPictures = new List<AccommodationPicture>();
                accommodation.AccommodationPictures.AddRange(pictures.Select(p => new AccommodationPicture()
                {
                    PictureId = p.Id
                }));

                result = AServices.SaveAccommodation(accommodation);
            }
            else //edit
            {
                //get accommodation from db then modified it
                var accommodation = AServices.GetAccommodationById(formModel.Id);

                accommodation.Name = formModel.Name;
                accommodation.Description = formModel.Description;
                accommodation.AccommodationPackageId = formModel.AccommodationPackageId;

                //delete existing accommodation pics to add new one after editing
                AServices.DeleteAccommodationPictures(accommodation.Id);

                accommodation.AccommodationPictures.AddRange(pictures.Select(p => new AccommodationPicture() 
                { 
                    PictureId = p.Id, AccommodationId = accommodation.Id
                }));

                result = AServices.UpdateAccommodation(accommodation);
            }
            return JsonDataResult.Result(result);
        }

        // delete (get)
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccommodationsActionModel model = new AccommodationsActionModel();

            var accommodation = AServices.GetAccommodationById(id);

            model.Id = accommodation.Id;
            model.Name = accommodation.Name;

            return PartialView("_Delete", model);
        }

        // delete (post)
        public JsonResult Delete(AccommodationsActionModel formModel)
        {
            var accommodation = AServices.GetAccommodationById(formModel.Id);

            bool result = AServices.DeleteAccommodation(accommodation);

            return JsonDataResult.Result(result);
        }
    }
}