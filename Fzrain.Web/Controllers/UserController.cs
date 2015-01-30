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

namespace Fzrain.Web.Controllers
{
    public class UserController : BaseListController<User>
    {
        public UserController(IRepository<User> repository) : base(repository)
        {
        }
    }
}