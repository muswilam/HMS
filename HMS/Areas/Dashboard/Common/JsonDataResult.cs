using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Common
{
    public static class JsonDataResult
    {
        // json data
        public static JsonResult Result(bool resultMethod)
        {
            JsonResult json = new JsonResult();

            if (resultMethod)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "OOPS! Something went wrong" };

            return json;
        }
    }
}