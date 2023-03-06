using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MemberSubscribeMsgOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取訂閱用戶
        /// </summary>
        [MaxLength(50)]
        public string SubscribeUserId { get; set; }

        /// <summary>
        /// 設置或獲取訂閱類型：SMS短信，WxApplet 微信小程序，InnerMessage站內消息 ，Email郵件通知
        /// </summary>
        [MaxLength(50)]
        public string SubscribeType { get; set; }

        /// <summary>
        /// 設置或獲取消息模板Id主鍵
        /// </summary>
        [MaxLength(200)]
        public string MessageTemplateId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
        public string SubscribeTemplateId { get; set; }

        /// <summary>
        /// 設置或獲取訂閱狀態
        /// </summary>
        [MaxLength(50)]
        public string SubscribeStatus { get; set; }


    }
}
