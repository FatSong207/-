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
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(RemittanceR))]
    [Serializable]
    public class RemittanceRInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CustomerRId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAID { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankNo { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchNo { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RANo { get; set; }

    }
}
