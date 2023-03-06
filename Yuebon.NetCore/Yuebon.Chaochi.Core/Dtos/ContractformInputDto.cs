using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Contractform))]
    [Serializable]
    public class ContractformInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取表單編號
        /// </summary>
        public string FormId { get; set; }
        /// <summary>
        /// 設置或獲取表單版本
        /// </summary>
        public string Vno { get; set; }
        /// <summary>
        /// 設置或獲取表單名稱
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取合約分類Id
        /// </summary>
        public string CateId { get; set; }        /// <summary>
        /// 設置或獲取合約分類
        /// </summary>
        public string Level { get; set; }

    }
}
