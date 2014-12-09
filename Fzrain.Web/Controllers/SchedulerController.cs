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
    public partial class SchedulerController : Controller
    {
        private readonly IRepository<Task> taskService;


        public SchedulerController(IRepository<Task> taskService)
        {
            this.taskService = taskService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(taskService.Table.AsQueryable().ToDataSourceResult(request));
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, Task task)
        {
            if (ModelState.IsValid)
            {
             var task1=   taskService.Table.Where(t => t.Id == task.Id ).FirstOrDefault();
             taskService.Delete(task1);
            }

            return Json(new[] {task}.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Insert(task.ToEntity());
            }

            return Json(new[] {task}.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, Task task)
        {
            if (ModelState.IsValid)
            {
                var task1 = taskService.Table.Where(t => t.Id == task.Id).FirstOrDefault();
                task1.Title = task.Title;
                taskService.Update(task1);
            }

            return Json(new[] {task}.ToDataSourceResult(request, ModelState));
        }


    }
}