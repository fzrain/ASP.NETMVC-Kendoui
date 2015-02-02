using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Scheduler;
using Fzrain.Service.Scheduler;
using Fzrain.Web.Models.Scheduler;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public  class SchedulerController : BaseListController<Task>
    {
        public SchedulerController(IRepository<Task> repository) : base(repository)
        {
        }
    }
}