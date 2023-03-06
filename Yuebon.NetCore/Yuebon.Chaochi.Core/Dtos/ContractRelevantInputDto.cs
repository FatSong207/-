using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約相關資料輸入對象模型
    /// </summary>
    [AutoMap(typeof(ContractRelevant))]
    [Serializable]
    public class ContractRelevantInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取承租人租金補助
        /// </summary>
        public string RNRentSUBAFee { get; set; }
        /// <summary>
        /// 設置或獲取建物合約租金
        /// </summary>
        public string B1TaxID { get; set; }

        /// <summary>
        /// 設置或獲取承租人身份資格
        /// </summary>
        public string RNQualify { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:一般戶
        /// </summary>
        public string RNQualify1C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第一類弱勢戶
        /// </summary>
        public string RNQualify2C { get; set; }
        /// <summary>
        /// 設置或獲取承租人身份資格:第二類弱勢戶
        /// </summary>
        public string RNQualify3C { get; set; }

    }
}
