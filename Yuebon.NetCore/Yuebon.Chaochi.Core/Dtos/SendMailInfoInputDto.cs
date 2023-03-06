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
    [AutoMap(typeof(SendMailInfo))]
    [Serializable]
    public class SendMailInfoInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 參考key值
        /// </summary>
        public virtual string RefKey { get; set; }
        /// <summary>
        /// 設置或獲取信件主旨
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 設置或獲取收件者
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 設置或獲取內文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 設置或獲取附件
        /// </summary>
        public string Attachments { get; set; }
        /// <summary>
        /// 設置或獲取是否啟用
        /// </summary>
        public bool? Enable { get; set; }
        /// <summary>
        /// 設置或獲取預計寄送日期
        /// </summary>
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 設置或獲取完成寄送日期
        /// </summary>
        public DateTime? SendedTime { get; set; }

    }
}
