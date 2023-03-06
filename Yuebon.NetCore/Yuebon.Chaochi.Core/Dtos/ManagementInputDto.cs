using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 管理方輸入對象模型
    /// </summary>
    [AutoMap(typeof(Management))]
    [Serializable]
    public class ManagementInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        public string MAID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        public string MRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        public string MLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        public string MAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        public string MTel { get; set; }
        /// <summary>
        /// 設置或獲取電子郵件信箱
        /// </summary>
        public string MEmail { get; set; }

    }
}
