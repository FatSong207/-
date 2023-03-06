using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約歷史版本輸入對象模型
    /// </summary>
    [AutoMap(typeof(HistoryFormContract))]
    [Serializable]
    public class HistoryFormContractInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取版號
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 設置或獲取合約名稱
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 設置或獲取產生人員
        /// </summary>
        public string UploadUser { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取定稿合約路徑
        /// </summary>
        public string UnsignPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取簽約掃描檔路徑
        /// </summary>
        public string SignedPDFPath { get; set; }

    }
}
