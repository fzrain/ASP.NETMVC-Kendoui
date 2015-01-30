using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Fzrain.Core;
using Fzrain.Core.Data;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public abstract class BaseListController<T> : Controller where T : BaseEntity
    {
        private readonly IRepository<T> repository;

        protected BaseListController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "Id", SortDirection = ListSortDirection.Descending });

            return Json(repository.Table.AsQueryable().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, T entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                repository.Insert(entity);
            }

            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
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

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Destroy([DataSourceRequest] DataSourceRequest request, int id)
        {
            var tEntity = repository.GetById(id);
            if (tEntity != null)
            {
                repository.Delete(tEntity);
            }

            return Json(new[] { tEntity }.ToDataSourceResult(request, ModelState));
        }

        
    }
}