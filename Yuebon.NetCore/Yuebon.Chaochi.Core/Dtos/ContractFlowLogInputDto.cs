using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約審核記錄輸入對象模型
    /// </summary>
    [AutoMap(typeof(ContractFlowLog))]
    [Serializable]
    public class ContractFlowLogInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取審核單號
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        public string CStatus { get; set; }
        /// <summary>
        /// 設置或獲取送審者
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 設置或獲取審核者
        /// </summary>
        public string Auditor { get; set; }
        /// <summary>
        /// 設置或獲取關卡動作(同意/退回/修正)
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 設置或獲取審核恴見
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 設置或獲取送出審核時間
        /// </summary>
        public DateTime? ApplyTime { get; set; }

    }
}
