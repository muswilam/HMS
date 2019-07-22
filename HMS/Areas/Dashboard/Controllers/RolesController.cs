using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using HMS.Entities;
using System.Data.Entity;
using HMS.Areas.Dashboard.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using HMS.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace HMS.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

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
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManger;
        }

        public ActionResult Index(string searchTerm, int? page) 
        {
            RolesListingViewModel model = new RolesListingViewModel();

            page = page ?? 1;
            var pageSize = 10;
            var totalRecords = GetAllRolesCount(searchTerm);

            model.Roles = SearchRoles(searchTerm, page.Value, pageSize);

            model.SearchByName = searchTerm;

            model.Pager = new Pager(totalRecords, page, pageSize);

            return View(model);
        }

        //get list of users by search for name or by role
        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page , int pagesize)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * pagesize;

            return roles.OrderBy(a => a.Name).Skip(skip).Take(pagesize).ToList();
        }

        //get total number of records
        public int GetAllRolesCount(string searchTerm)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return roles.Count();
        }

        // create and edit (get)
        [HttpGet]
        public async Task<ActionResult> Action(string id)
        {
            RolesActionModel model = new RolesActionModel();

            if (!string.IsNullOrEmpty(id)) // edit
            {
                var role = await RoleManager.FindByIdAsync(id);

                model.Id = role.Id;
                model.Name = role.Name; 
            }

            return PartialView("_Action", model);
        }

        // create and edit (post) 
        [HttpPost]
        public async Task<JsonResult> Action(RolesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (string.IsNullOrEmpty(formModel.Id)) //Create
            {
                var role = new IdentityRole();

                role.Name = formModel.Name;

                result = await RoleManager.CreateAsync(role);

            }
            else //edit
            {
                var role = await RoleManager.FindByIdAsync(formModel.Id);

                role.Name = formModel.Name;

                result = await RoleManager.UpdateAsync(role);
            }

            json.Data = new { success = result.Succeeded, message = string.Join(" , ", result.Errors) };
            return json;
        }

        // delete get
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            RolesActionModel model = new RolesActionModel();

            var role = await RoleManager.FindByIdAsync(id);

            model.Id = role.Id;
            model.Name = role.Name;

            return PartialView("_Delete", model);
        }

        // delete (post)
        public async Task<JsonResult> Delete(RolesActionModel formModel)
        {
            JsonResult json = new JsonResult();

            if (!string.IsNullOrEmpty(formModel.Id))
            {
                var role = await RoleManager.FindByIdAsync(formModel.Id);

                var result = await RoleManager.DeleteAsync(role);
                json.Data = new { success = result.Succeeded, message = string.Join(" , ", result.Errors) };
            }
            else
                json.Data = new { success = false, message = "Invalid user." };

            return json;
        }
    }
}