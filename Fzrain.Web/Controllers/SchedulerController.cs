using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;
using Fzrain.Core.Domain.Scheduler;
using Fzrain.Service.UserManage;
using Fzrain.Web.Models.Scheduler;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public partial  class SchedulerController 
    {    
     public SchedulerController(IRepository<Scheduler> repository, string  sign)
         : base(repository, false)
        {
              
        }
    }
}