using HMS.Entities;
using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class UsersListingModel
    {
        public IEnumerable<HMSUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public Pager Pager { get; set; }

        public string SearchByName { get; set; }
        public string SearchByRoleId { get; set; }

    }

    public class UsersActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //note, identity framework made id string
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}