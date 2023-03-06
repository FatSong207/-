using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.Mvc
{
    /// <summary> 
    /// 隱藏接口，不生成到swagger文檔展示 
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public partial class HiddenApiAttribute : Attribute { }
    /// <summary>
    /// 隱藏接口，不生成到swagger文檔展示 
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        /// <summary>
        /// 實現Apply方法
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="documentFilterContext"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext documentFilterContext)
        {
            foreach (ApiDescription apiDescription in documentFilterContext.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
                {
                    if (Enumerable.OfType<HiddenApiAttribute>(methodInfo.GetCustomAttributes<HiddenApiAttribute>()).Any())
                    {
                        var key = "/" + apiDescription.RelativePath.TrimEnd('/');
                        if (key.Contains("?"))
                        {
                            int idx = key.IndexOf("?", StringComparison.Ordinal);
                            key = key.Substring(0, idx);
                        }
                        swaggerDoc.Paths.Remove(key);
                    }
                }
            }
        }
    }
}
