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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
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

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        // GET: Dashboard/Accommodations
        public ActionResult Index(string searchTerm, string roleId, int? page)
        {
            UsersListingModel model = new UsersListingModel();

            page = page ?? 1;
            var pageSize = 10;
            var totalRecords = GetAllUsersCount(searchTerm, roleId);

            model.Users = SearchUsers(searchTerm, roleId, page.Value, pageSize);

            model.SearchByName = searchTerm;
            model.SearchByRoleId = roleId;
            model.Roles = RoleManager.Roles.ToList();

            model.Pager = new Pager(totalRecords, page, pageSize);

            return View(model);
        }

        // get list of Users by search for name or by role
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

        // get total number of records
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

        // create and edit (get)
        [HttpGet]
        public async Task<ActionResult> Action(string id)
        {
            UsersActionModel model = new UsersActionModel();

            if (!string.IsNullOrEmpty(id)) // edit
            {
                var user = await UserManager.FindByIdAsync(id);

                model.Id = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
            }

            return PartialView("_Action", model);
        }

        // create and edit (post) 
        [HttpPost]
        public async Task<JsonResult> Action(UsersActionModel formModel)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (string.IsNullOrEmpty(formModel.Id)) //Create
            {
                var user = new HMSUser();

                user.FullName = formModel.FullName;
                user.Email = formModel.Email;
                user.UserName = formModel.UserName;

                user.Country = formModel.Country;
                user.City = formModel.City;
                user.Address = formModel.Address;

                result = await UserManager.CreateAsync(user);

            }
            else //edit
            {
                var user = await UserManager.FindByIdAsync(formModel.Id);

                user.FullName = formModel.FullName;
                user.Email = formModel.Email;
                user.UserName = formModel.UserName;

                user.Country = formModel.Country;
                user.City = formModel.City;
                user.Address = formModel.Address;

                result = await UserManager.UpdateAsync(user);
            }

            json.Data = new { success = result.Succeeded, message = string.Join(" , ", result.Errors) };
            return json;
        }

        // delete get
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            UsersActionModel model = new UsersActionModel();

            var user = await UserManager.FindByIdAsync(id);

            model.Id = user.Id;
            model.UserName = user.UserName;

            return PartialView("_Delete", model);
        }

        // delete (post)
        public async Task<JsonResult> Delete(UsersActionModel formModel)
        {
            JsonResult json = new JsonResult();

            if (!string.IsNullOrEmpty(formModel.Id))
            {
                var user = await UserManager.FindByIdAsync(formModel.Id);

                var result = await UserManager.DeleteAsync(user);
                json.Data = new { success = result.Succeeded, message = string.Join(" , ", result.Errors) };
            }
            else
                json.Data = new { success = false, message = "Invalid user." };

            return json;
        }

        // Get List of User Roles and List of Roles that can assign to him
        [HttpGet]
        public async Task<ActionResult> UserRoles(string id)
        {
            UserRolesModel model = new UserRolesModel();

            model.UserId = id;

            var user = await UserManager.FindByIdAsync(id);
            var userRolesId = user.Roles.Select(r => r.RoleId).ToList();

            model.UserRoles = RoleManager.Roles.Where(r => userRolesId.Contains(r.Id)).ToList();

            model.Roles = RoleManager.Roles.Where(r => !userRolesId.Contains(r.Id)).ToList();

            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string roleId , string userId , bool isDelete = false)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            var user = await UserManager.FindByIdAsync(userId);
            var role = await RoleManager.FindByIdAsync(roleId);

            if(user != null && role != null)
            {
                if(!isDelete) // assign new role
                    result = await UserManager.AddToRoleAsync(userId, role.Name);
                else // delete role
                    result = await UserManager.RemoveFromRoleAsync(userId, role.Name);

               json.Data = new { success = result.Succeeded , message = string.Join(" ,  ", result.Errors)};
            }
            else
                json.Data = new { success = false, message = "Invalid operation." };

            return json;
        }
    }
}