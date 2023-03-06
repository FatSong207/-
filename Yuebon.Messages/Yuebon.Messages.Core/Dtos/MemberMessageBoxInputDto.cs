using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(MemberMessageBox))]
    [Serializable]
    public class MemberMessageBoxInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取消息內容Id
        /// </summary>
        public long? ContentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string MsgContent { get; set; }

        /// <summary>
        /// 設置或獲取發送者
        /// </summary>
        public string Sernder { get; set; }

        /// <summary>
        /// 設置或獲取接受者
        /// </summary>
        public string Accepter { get; set; }

        /// <summary>
        /// 設置或獲取是否已讀
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? ReadDate { get; set; }


    }
}
