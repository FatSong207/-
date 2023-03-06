using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(RoleData))]
    [Serializable]
    public class RoleDataInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 類型，company-公司，dept-部門，person-個人
        /// </summary>
        public virtual string DType { get; set; }

        /// <summary>
        /// 數據數據，部門ID或個人ID
        /// </summary>
        public virtual string AuthorizeData { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Note { get; set; }


    }
}
