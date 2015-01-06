using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain.Configuration;
using Fzrain.Core.Domain.Permission;
using Fzrain.Service.Configuration;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingService settingService;

        public SettingController(ISettingService settingService)
        {
            this.settingService = settingService;
        }

        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "Id", SortDirection = ListSortDirection.Descending });

            return Json(settingService.GetAll().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Setting setting)
        {
            if (setting != null && ModelState.IsValid)
            {
                settingService.AddSetting(setting);
            }

            return Json(new[] { setting }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Setting setting)
        {

            if (setting != null && ModelState.IsValid)
            {
                var s = settingService.GetById(setting.Id);
                s.Name = setting.Name;
                s.Value  = setting.Value;
                s.Describe  = setting.Describe;
               
                settingService.UpdateSetting(s);
            }

            return Json(new[] { setting }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, int id)
        {
            var setting = settingService.GetById(id);
            if (setting != null)
            {
                settingService.RemoveSetting(setting);
            }

            return Json(new[] { setting }.ToDataSourceResult(request, ModelState));
        }
    }
}