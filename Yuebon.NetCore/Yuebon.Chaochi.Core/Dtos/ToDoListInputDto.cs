using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 待辦事項輸入對象模型
    /// </summary>
    [AutoMap(typeof(ToDoList))]
    [Serializable]
    public class ToDoListInputDto : IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取功能類型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取功能模組ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 設置或獲取功能模組資料
        /// </summary>
        public string TypeData { get; set; }
        /// <summary>
        /// 設置或獲取待辧事項名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取用戶ID
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 設置或獲取審核意見
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 設置或獲取同意或退回
        /// </summary>
        public string Action { get; set; }

        public string UserId { get; set; }

        public ContractInputDto contractInputDto { get; set; }

        public BlackListInputDto BlackListInputDto { get; set; }

    }
}
