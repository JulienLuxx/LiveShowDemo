using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveShow.Core.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LiveShow.Core.Filter
{
    /// <summary>
    /// 过滤具备SwaggerIgnore特性的api
    /// </summary>
    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var ignoreApis = context.ApiDescriptions.Where(x => x.ActionAttributes().Any(y => y is SwaggerIgnoreAttribute));
            if (ignoreApis != null)
            {
                foreach (var ignoreApi in ignoreApis)
                {
                    swaggerDoc.Paths.Remove("/" + ignoreApi.RelativePath);
                }
            }
        }
    }
}
