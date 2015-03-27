using System.Collections.Generic;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;
using Fzrain.Service.UserManage;

namespace Fzrain.Web.Controllers
{
    public partial  class UserController
    {
        private readonly IUserService userService;
        public UserController(IRepository<User> repository, IUserService userService) : base(repository,false)
        {
            this.userService = userService;        
        }
     
        public ActionResult Roles(int id)
        {
            ViewBag.UserId = id;
            ViewBag.MyRoles = userService.GetRoles(id);
            return PartialView(userService.GetAllRoles());
        }

        public ActionResult SetRoles(List<int> roleIds, int userId)
        {
            userService.SetRoles(userId, roleIds);
            return Json("ok");
        }
        public ActionResult Modules(int id)
        {
            ViewBag.UserId = id;
            ViewBag.MyModules = userService.GeModules(id);
            return PartialView(userService.GetAllModules());
        }

        public ActionResult SetModules(List<int> moduleIds, int userId)
        {
            userService.SetModules(userId, moduleIds);
            return Json("ok");
        }
    }
}