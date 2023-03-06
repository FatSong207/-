using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.Mvc
{
    /// <summary>
    /// 功能權限屬性配置
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class YuebonAuthorizeAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 功能權限
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="function">功能代碼</param>
        public YuebonAuthorizeAttribute(string function)
        {
            Function = function;
        }
    }
}
