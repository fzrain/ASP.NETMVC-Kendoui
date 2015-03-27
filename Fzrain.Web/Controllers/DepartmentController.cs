using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Web.Controllers
{
    public partial class DepartmentController
    {
        // GET: Department
     

        public ActionResult GetDptTreeData(int? id)
        {
            Department d1 = new Department { Id = 1, DepartmentName = "开发部" };
            Department d2 = new Department { Id = 2, DepartmentName = "实施部" };
            Department d3 = new Department { Id = 3, DepartmentName = "开发1部" };
            d3.ParentDpt = d1;
            d1.ChildrenDpt = new List<Department>() { d3 };
            List<Department> departments = new List<Department>
            {
               d1,d2,d3
            };

            var data = from d in departments
                       where id.HasValue ? d.ParentDpt != null && d.ParentDpt.Id == id : d.ParentDpt == null
                       select new
                   {
                       id = d.Id,
                       Name = d.DepartmentName,
                       hasChildren = d.ChildrenDpt != null && d.ChildrenDpt.Any(),
                       image = d.ChildrenDpt != null && d.ChildrenDpt.Any() ? "folder" : "file"
                   };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}