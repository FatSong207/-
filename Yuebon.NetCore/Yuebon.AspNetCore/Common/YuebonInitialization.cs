﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Helpers;
using Yuebon.Security.Models;

namespace Yuebon.AspNetCore.Common
{
    /// <summary>
    /// 系統初始化內容
    /// </summary>
    public abstract class YuebonInitialization
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual  void Initial()
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SysSetting sysSetting = XmlConverter.Deserialize<SysSetting>("xmlconfig/sys.config");
            if (sysSetting != null)
            {
                yuebonCacheHelper.Add("SysSetting", sysSetting);
            }
       }
    }
}
