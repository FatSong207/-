﻿using System;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterIPService:IService<FilterIP, FilterIPOutputDto, string>
    {
        /// <summary>
        /// 驗證IP地址是否被拒絕
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
       bool ValidateIP(string ip);
    }
}
