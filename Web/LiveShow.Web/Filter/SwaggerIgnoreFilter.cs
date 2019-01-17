using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Web.Filter
{
    /// <summary>
    /// 过滤具备SwaggerIgnore特性的api
    /// </summary>
    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
