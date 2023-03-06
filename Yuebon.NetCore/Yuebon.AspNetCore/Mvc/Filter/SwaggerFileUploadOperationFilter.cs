using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.Mvc.Filter
{
    /// <summary>
    /// Swagger 上傳文件過濾器
    /// </summary>
    public class SwaggerFileUploadOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 應用過濾器
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //判斷上傳檔案的型別，只有上傳的型別是IFormCollection的才進行重寫。
            if (context.ApiDescription.ActionDescriptor.Parameters.Any(w => w.ParameterType == typeof(IFormCollection)))
            {
                Dictionary<string, OpenApiSchema> schema = new Dictionary<string, OpenApiSchema>();
                schema["fileName"] = new OpenApiSchema { Description = "Select file", Type = "string", Format = "binary" };
                Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType>();
                content["multipart/form-data"] = new OpenApiMediaType { Schema = new OpenApiSchema { Type = "object", Properties = schema } };
                operation.RequestBody = new OpenApiRequestBody() { Content = content };
            }
        }
    }
}
