using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 二房東輸入對象模型
    /// </summary>
    [AutoMap(typeof(SecondLandlord))]
    [Serializable]
    public class SecondLandlordInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        public string SLRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        public string SLLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        public string SLAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        public string SLTel { get; set; }

    }
}
