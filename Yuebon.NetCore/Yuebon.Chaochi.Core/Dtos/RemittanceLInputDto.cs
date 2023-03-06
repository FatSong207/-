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
    [AutoMap(typeof(RemittanceL))]
    [Serializable]
    public class RemittanceLInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CustomerLId { get; set; }

        /// <summary>
        /// 所屬人身分證字號或統編
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LAName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LAID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBankName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBankNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBranchName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBranchNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LANo { get; set; }


    }
}
