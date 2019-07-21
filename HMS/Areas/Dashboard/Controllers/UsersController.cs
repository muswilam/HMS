using HMS.Areas.Dashboard.Common;
using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {

        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        AccommodationsService AServices = new AccommodationsService();
        AccommodationPackagesService APServices = new AccommodationPackagesService();

        // GET: Dashboard/Accommodations
        public ActionResult Index(string searchTerm, string roleId, int? page)
        {
            UsersListingModel model = new UsersListingModel();

            page = page ?? 1;
            var pageSize = 2;
            var totalRecords = GetAllUsersCount(searchTerm, roleId);

            model.Users = SearchUsers(searchTerm, roleId, page.Value, pageSize);

            model.SearchByName = searchTerm;
            model.SearchByRoleId = roleId;
            //model.Roles = APServices.GetAllAccommodationPackages();

            model.Pager = new Pager(totalRecords, page, pageSize);

            return View(model);
        }

        //get list of Users by search for name or by role
        public IEnumerable<HMSUser> SearchUsers(string searchTerm, string roleId, int page, int pageSize)
        {
            var users = UserManager.Users.Include(u => u.Roles).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
               // users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * pageSize;

            return users.OrderBy(a => a.Email).Skip(skip).Take(pageSize).ToList();
        }

        //get total number of records
        public int GetAllUsersCount(string searchTerm, string roleId)
        {
            var users = UserManager.Users.Include(u => u.Roles).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
               // accommodationsDb = accommodationsDb.Where(a => a.AccommodationPackageId == accommodationPackageId.Value);
            }

            return users.Count();
        }


    }
}