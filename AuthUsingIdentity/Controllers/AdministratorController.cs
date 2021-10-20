using AuthUsingIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthUsingIdentity.Controllers
{
    public class AdministratorController : Controller
    {
        public AdministratorController()
        {

        }
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly ApplicationDbContext _context;
        public AdministratorController(ApplicationRoleManager roleManager, ApplicationUserManager userManager, ApplicationDbContext context)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            _context = context;

        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Administrator
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                list.Add(new RoleViewModel(role));
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel obj)
        {
         
            var role = new ApplicationRole() { Name = obj.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel obj)
        {
            var role = new ApplicationRole() { Id = obj.Id,Name=obj.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> EditUserInRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            ViewBag.RoleId = id;
            ViewBag.RoleName = role.Name;
            if (role==null)
            {
                ViewBag.ErrorMessage = $"Role Id{id} Could not found ";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in UserManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await UserManager.IsInRoleAsync(user.Id,role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditUserInRole(List<UserRoleViewModel> viewModel, string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id{id} Could not found ";
                return View("NotFound");
            }
            for (int i = 0; i < viewModel.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(viewModel[i].UserId);
                IdentityResult result = null;
                if (viewModel[i].IsSelected && !(await UserManager.IsInRoleAsync(user.Id,role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                }
                else if (viewModel[i].IsSelected && await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (viewModel.Count - 1))
                        continue;
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}