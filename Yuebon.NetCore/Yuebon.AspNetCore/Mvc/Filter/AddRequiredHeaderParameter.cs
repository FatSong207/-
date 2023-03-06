using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuebon.AspNetCore.Mvc.Filter
{
    /// <summary>
    /// 向swagger添加header參數
    /// </summary>
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //https://www.codeprj.com/zh/blog/b890db1.html
            var info = context.MethodInfo;
            context.ApiDescription.TryGetMethodInfo(out info);
            Attribute attribute = info.GetCustomAttribute(typeof(NoSignRequiredAttribute));
            if (attribute != null)
            {
                return;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "sign",
                In = ParameterLocation.Header,
                Description = "是否啟用簽名",
                Required = true,
                AllowEmptyValue = false
            });
        }
    }
}
