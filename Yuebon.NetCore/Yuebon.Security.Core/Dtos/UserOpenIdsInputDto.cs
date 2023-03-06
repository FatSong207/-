using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(UserOpenIds))]
    [Serializable]
    public class UserOpenIdsInputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string OpenIdType { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string OpenId { get; set; }


    }
}
