using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Fzrain.Core;
using Fzrain.Core.Data;
using Fzrain.Web.Framework.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public abstract class BaseListController<T> : Controller where T : BaseEntity
    {
        private  readonly IRepository<T> repository;
        private readonly bool useCommonPage;
        protected BaseListController(IRepository<T> repository,bool useCommonPage=true)
        {           
           this.repository = repository;
           this.useCommonPage = useCommonPage;
        }

        public virtual ActionResult Index()
        {
            ViewBag.ControllerName = typeof(T).Name;
            var props = typeof(T).GetProperties().Where(p => !p.GetMethod.IsVirtual);
            List<string> list = new List<string>();
            foreach (PropertyInfo prop in props )
            {
              DisplayNameAttribute attr=  prop.GetCustomAttribute<DisplayNameAttribute>();
                string name = attr!=null ? attr.DisplayName : prop.Name;
             
                list.Add(prop.Name+";"+name);
            }
            ViewBag.Columns = list;
            return View(useCommonPage ? "../Common/Index" : "");
        }

        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "Id", SortDirection = ListSortDirection.Descending });

            return Json(repository.Table.AsQueryable().ToDataSourceResult(request));
        }

        [HttpPost]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, T entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                repository.Insert(entity);
            }

            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public virtual ActionResult Update([DataSourceRequest] DataSourceRequest request, T entity)
        {

            if (entity != null && ModelState.IsValid)
            {
                var t = repository.GetById(entity.Id);
                var props = typeof(T).GetProperties().Where(p=>!p.GetMethod.IsVirtual);
                foreach (var prop in props)
                {
                    object value = Convert.ChangeType(prop.GetValue(entity), typeof(T).GetProperty(prop.Name).PropertyType);
                    prop.SetValue(t, value);
                }
                repository.Update(t);
            }

            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public virtual ActionResult Destroy([DataSourceRequest] DataSourceRequest request, int id)
        {
            var tEntity = repository.GetById(id);
            if (tEntity != null)
            {
                repository.Delete(tEntity);
            }

            return Json(new[] { tEntity }.ToDataSourceResult(request, ModelState));
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}