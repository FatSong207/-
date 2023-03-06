using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Common
{ 
    /// <summary>
    /// SessionObject是登錄之后，給客戶端傳回的對象
    /// </summary>
    public class SessionObject
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; set; }
        /// <summary>
        /// 當前登錄的用戶的信息
        /// </summary>
        public User LogonUser { get; set; }
    }
}
