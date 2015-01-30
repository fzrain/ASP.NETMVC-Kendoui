using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Configuration;
using Fzrain.Core.Domain.Permission;
using Fzrain.Service.Configuration;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public class SettingController : BaseListController<Setting>
    {
        public SettingController(IRepository<Setting> repository) : base(repository)
        {
        }
    }
}