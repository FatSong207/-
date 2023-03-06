using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約歷史版本輸出對象模型
    /// </summary>
    [Serializable]
    public class HistoryFormContractOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        [MaxLength(100)]
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取版號
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 設置或獲取合約名稱
        /// </summary>
        [MaxLength(200)]
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
        [MaxLength(100)]
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取定稿合約路徑
        /// </summary>
        [MaxLength(200)]
        public string UnsignPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取簽約掃描檔路徑
        /// </summary>
        [MaxLength(200)]
        public string SignedPDFPath { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }
        //private string uploadStatus;        public string Status {             get
            {
                if (string.IsNullOrWhiteSpace(SignedPDFPath)) {
                    return "未上傳";
                } else {
                    return "已存在";
                }
            }            set
            {
                //if(string.IsNullOrWhiteSpace(SignedPDFPath)) {
                //    uploadStatus = "未上傳";
                //} else {
                //    uploadStatus = "己存在";
                //}
            }        }
    }
}
