using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Json;
using Yuebon.Security.Dtos;

namespace Yuebon.AspNetCore.Common
{
    /// <summary>
    /// 權限控制
    /// </summary>
    public static class Permission
    {

        /// <summary>
        /// 判斷當前用戶是否擁有某功能點的權限
        /// </summary>
        /// <param name="functionCode">功能編碼code</param>
        /// <param name="userId">用戶id</param>
        /// <returns></returns>
        public static bool HasFunction(string functionCode, string userId)
        {            
            bool hasFunction = false;
            if (!string.IsNullOrEmpty(userId)) { 
                if (string.IsNullOrEmpty(functionCode))
                {
                    hasFunction = true;
                }
                else
                {
                    List<MenuOutputDto> listFunction =new YuebonCacheHelper().Get("User_Function_" + userId).ToJson().ToList<MenuOutputDto>();
                    if (listFunction != null && listFunction.Count(t => t.EnCode == functionCode) > 0)
                    {
                        hasFunction = true;
                    }
                }
            }
            return hasFunction;
        }
        /// <summary>
        /// 判斷是否為系統管理員或超級管理員
        /// </summary>
        /// <returns>true:系統管理員或超級管理員,false:不是系統管理員或超級管理員</returns>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static bool IsAdmin(YuebonCurrentUser currentUser)
        {
            bool blnIsAdmin = false;
            if (currentUser != null)
            {
                if(currentUser.Account == "admin"|| currentUser.Role.Contains("administrators",StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return blnIsAdmin;
        }
    }
}
