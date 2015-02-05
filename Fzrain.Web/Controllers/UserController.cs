using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Core.Domain.Permission;
using Fzrain.Core.Infrastructure;
using Fzrain.Service;
using Fzrain.Service.UserManage;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Fzrain.Data ;

namespace Fzrain.Web.Controllers
{
    public class UserController : BaseListController<User>
    {
        private readonly IUserService userService;
        public UserController(IRepository<User> repository, IUserService userService) : base(repository)
        {
            this.userService = userService;
        }

        public ActionResult Roles(int id)
        {
            ViewBag.UserId = id;
            return PartialView(userService.GetRoles());
        }

        public ActionResult SetRoles(List<int> roleIds,int userId)
        {
            return new EmptyResult();
        }
    }
}