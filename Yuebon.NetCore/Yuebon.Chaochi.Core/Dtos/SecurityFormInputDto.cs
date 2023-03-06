using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 內部表單輸入對象模型
    /// </summary>
    [AutoMap(typeof(SecurityForm))]
    [Serializable]
    public class SecurityFormInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FormId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Vno { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string TBNO { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Type { get; set; }

    }
}
