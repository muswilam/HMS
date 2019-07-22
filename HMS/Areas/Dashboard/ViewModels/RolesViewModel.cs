using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using HMS.ViewModels;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class RolesListingViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string SearchByName { get; set; }
        public Pager Pager { get; set; }
    }

    public class RolesActionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}