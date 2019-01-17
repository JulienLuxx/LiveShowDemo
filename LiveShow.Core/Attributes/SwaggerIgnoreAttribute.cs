using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Core.Attributes
{
    /// <summary>
    /// ignore some api on swagger.json
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SwaggerIgnoreAttribute: Attribute
    {
    }
}
