using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        DashboardService DBServices = new DashboardService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult jsonResult = new JsonResult();
            var picsList = new List<Picture>();

            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = Server.MapPath("~/images/site/") + fileName;

                picture.SaveAs(filePath);

                var pictureDb = new Picture();
                pictureDb.URL = fileName;

                if(DBServices.SavePicture(pictureDb))
                {
                    picsList.Add(pictureDb);
                }
            }

            jsonResult.Data = picsList;
            return jsonResult;
        }
    }
}