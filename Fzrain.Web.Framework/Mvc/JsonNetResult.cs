using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Fzrain.Web.Framework.Mvc
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                //这句是解决问题的关键,也就是json.net官方给出的解决配置选项.                 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;
            var scriptSerializer = JsonSerializer.Create(Settings);
            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, Data);
                response.Write(sw.ToString());
            }
        }
    }
}
