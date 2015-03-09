using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Web.Framework.Theme
{
    public  class ThemeSetting
    {
        /// <summary>
        /// 得到当前主题
        /// </summary>
        public static string CurrentTheme
        {
  
            get
            {
                var cookie = System.Web.HttpContext.Current.Request.Cookies["theme"];
                return cookie != null && !string.IsNullOrWhiteSpace(cookie.Value) ? cookie.Value : "default";
            }
        }

        public static string CurrentDimensions
        {
            get
            {
                var cookie = System.Web.HttpContext.Current.Request.Cookies["Dimensions"];
                return cookie != null && !string.IsNullOrWhiteSpace(cookie.Value) ? cookie.Value : "common-bootstrap";
            }
        }

    }
}
