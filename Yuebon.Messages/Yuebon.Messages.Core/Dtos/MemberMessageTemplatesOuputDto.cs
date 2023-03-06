using System;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;

namespace Yuebon.Messages.Dtos
{
    /// <summary>
    /// 用戶訂閱消息模板定義
    /// </summary>
    [Serializable]
    public class MemberMessageTemplatesOuputDto : IOutputDto
    {
        #region Property Members

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 消息類型
        /// </summary>
        public  string MessageType
        {
            get; set;
        }
        /// <summary>
        /// 消息名稱
        /// </summary>
        public  string Name
        {
            get; set;
        }
        /// <summary>
        /// 是否發送郵件
        /// </summary>
        public  bool SendEmail
        {
            get; set;
        }
        /// <summary>
        /// 是否發送短信
        /// </summary>
        public  bool SendSMS
        {

            get; set;
        }
        /// <summary>
        /// 是否發送站內消息
        /// </summary>
        public  bool SendInnerMessage
        {

            get; set;
        }
        /// <summary>
        /// 是否發送微信公眾號消息
        /// </summary>
        public  bool SendWeixin
        {

            get; set;
        }
        /// <summary>
        /// 微信公眾號消息模板Id
        /// </summary>
        public  string WeixinTemplateId
        {

            get; set;
        }
        /// <summary>
        /// 消息內容
        /// </summary>

        public  string TagDescription
        {

            get; set;
        }
        /// <summary>
        /// Email郵件消息主題
        /// </summary>
        public  string EmailSubject
        {

            get; set;
        }
        /// <summary>
        /// Email郵件消息內容
        /// </summary>
        public  string EmailBody
        {

            get; set;
        }
        /// <summary>
        /// 站內消息主題
        /// </summary>
        public  string InnerMessageSubject
        {

            get; set;
        }
        /// <summary>
        /// 站內消息內容
        /// </summary>
        public  string InnerMessageBody
        {

            get; set;
        }
        /// <summary>
        /// 短信內容
        /// </summary>
        public  string SMSBody
        {

            get; set;
        }

        /// <summary>
        /// 微信模板編號，如果為空則表示不支持微信消息提醒
        /// </summary>
        public  string WeiXinTemplateNo
        {

            get; set;
        }

        /// <summary>
        /// 微信模板中對應的名稱
        /// </summary>
        public  string WeiXinName
        {

            get; set;
        }

        /// <summary>
        /// 是否用于微信小程序
        /// </summary>
        public  bool UseInWxApplet
        {

            get; set;
        }

        /// <summary>
        /// 小程序模板ID
        /// </summary>
        public  string WxAppletTemplateId
        {

            get; set;
        }

        /// <summary>
        /// 小程序模板編號
        /// </summary>
        public  string AppletTemplateNo
        {

            get; set;
        }

        /// <summary>
        /// 小程序模板名稱
        /// </summary>
        public  string AppletTemplateName
        {

            get; set;
        }


        /// <summary>
        /// 小程序模板消息模板ID
        /// </summary>
        public virtual string WxAppletSubscribeTemplateId
        {
            get; set;
        }
        /// <summary>
        /// 小程序訂閱消息模板編號
        /// </summary>
        public virtual string WxAppletSubscribeTemplateNo
        {
            get; set;
        }
        /// <summary>
        /// 小程序訂閱消息模板名稱
        /// </summary>
        public virtual string WxAppletSubscribeTemplateName
        {
            get; set;
        }
        /// <summary>
        /// 短信模板代碼
        /// </summary>
        public  string SMSTemplateCode
        {

            get; set;
        }
        /// <summary>
        /// 短信模板內容
        /// </summary>
        public  string SMSTemplateContent
        {

            get; set;
        }

        /// <summary>
        /// O2O小程序模板ID 
        /// </summary>
        public  string WxO2OAppletTemplateId
        {

            get; set;
        }

        /// <summary>
        /// 是否在O2O小程序中使用
        /// </summary>
        public  bool UseInO2OApplet
        {

            get; set;
        }

        /// <summary>
        /// 消息訂閱者訂閱消息ID 
        /// </summary>
        public string MemberSubscribeMsgId
        {

            get; set;
        }
        /// <summary>
        /// 消息訂閱者訂閱消息狀態
        /// </summary>
        public string SubscribeStatus
        {

            get; set;
        }
        /// <summary>
        /// 消息訂閱者訂閱類型：SMS短信，WxApplet 微信小程序，InnerMessage站內消息 ，Email郵件通知
        /// </summary>
        public string SubscribeType
        {

            get; set;
        }
        
        #endregion
    }
}
