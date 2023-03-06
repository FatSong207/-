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
    public class MessageTemplatesOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string MessageType { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool SendEmail { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool SendSMS { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool SendInnerMessage { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool SendWeixin { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(150)]
        public string WeixinTemplateId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string TagDescription { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string EmailSubject { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(1073741823)]
        public string EmailBody { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string InnerMessageSubject { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(1073741823)]
        public string InnerMessageBody { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string SMSBody { get; set; }
        /// <summary>
        /// 設置或獲取微信模板編號，如果為空則表示不支持微信消息提醒
        /// </summary>
        [MaxLength(50)]
        public string WeiXinTemplateNo { get; set; }
        /// <summary>
        /// 設置或獲取微信模板中對應的名稱
        /// </summary>
        [MaxLength(100)]
        public string WeiXinName { get; set; }
        /// <summary>
        /// 設置或獲取是否用于微信小程序
        /// </summary>
        public bool? UseInWxApplet { get; set; }
        /// <summary>
        /// 設置或獲取小程序模板ID
        /// </summary>
        [MaxLength(50)]
        public string WxAppletTemplateId { get; set; }
        /// <summary>
        /// 設置或獲取小程序模板編號
        /// </summary>
        [MaxLength(50)]
        public string AppletTemplateNo { get; set; }
        /// <summary>
        /// 設置或獲取小程序模板名稱
        /// </summary>
        [MaxLength(100)]
        public string AppletTemplateName { get; set; }
        /// <summary>
        /// 設置或獲取訂閱消息模板ID
        /// </summary>
        [MaxLength(50)]
        public string WxAppletSubscribeTemplateId { get; set; }
        /// <summary>
        /// 設置或獲取模板編號
        /// </summary>
        [MaxLength(50)]
        public string WxAppletSubscribeTemplateNo { get; set; }
        /// <summary>
        /// 設置或獲取標題
        /// </summary>
        [MaxLength(50)]
        public string WxAppletSubscribeTemplateName { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string SMSTemplateCode { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(-1)]
        public string SMSTemplateContent { get; set; }
        /// <summary>
        /// 設置或獲取O2O小程序模板ID 
        /// </summary>
        [MaxLength(50)]
        public string WxO2OAppletTemplateId { get; set; }
        /// <summary>
        /// 設置或獲取是否在O2O小程序中使用
        /// </summary>
        public bool? UseInO2OApplet { get; set; }

    }
}
