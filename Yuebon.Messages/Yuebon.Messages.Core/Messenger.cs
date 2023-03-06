using System.Threading.Tasks;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Commons.Models;
using Yuebon.Messages.IServices;
using Yuebon.Messages.Models;
using Yuebon.Security.Application;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Yuebon.WeChat.CommonService.SubscribeMessage.WxApplet;
using Senparc.Weixin.Entities;
using Yuebon.SMS.AliYun;
using Yuebon.Commons.Core.App;

namespace Yuebon.Messages.Application
{
    public static class Messenger
    {
        static IMessageTemplatesService messageTemplatesService = App.GetService<IMessageTemplatesService>();
        static IMemberMessageBoxService memberMessageBoxService = App.GetService<IMemberMessageBoxService>();
        static IMemberSubscribeMsgService memberSubscribeMsgService = App.GetService<IMemberSubscribeMsgService>();
        static IUserService userService = App.GetService<IUserService>();

        /// <summary>
        /// 留言提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="text">留言內容,20個以內字符</param>
        /// <param name="date">留言時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page">點擊模板卡片后的跳轉頁面，僅限本小程序內的頁面。支持帶參數,（示例index?foo=bar）。該字段不填則模板無跳轉。</param>
        public static CommonResult SendCommentNotice(string userId, string text,string date,string page="")
        {
            CommonResult result = new CommonResult();
            User user = new UserApp().GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(text))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("CommentNotice");
                if (!string.IsNullOrEmpty(template.InnerMessageSubject) && !string.IsNullOrEmpty(template.InnerMessageBody) && template.SendInnerMessage)
                {
                    MemberMessageBox memberMessageBox = new MemberMessageBox();
                    memberMessageBox.Id = GuidUtils.CreateNo();
                    memberMessageBox.IsRead = false;
                    memberMessageBox.Sernder = "系統消息";
                    memberMessageBox.Accepter = userId;
                    memberMessageBox.MsgContent = string.Format(template.InnerMessageBody, text, date);
                    memberMessageBoxService.Insert(memberMessageBox);
                }
                if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet)
                {
                    MemberSubscribeMsg memberSubscribeMsg= memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id,userId, "WxApplet");
                    if(memberSubscribeMsg.SubscribeStatus == "accept") { 
                        UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                        if (userOpenIds != null )
                        {
                            WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendCommentNotice(userOpenIds.OpenId, template.WxAppletSubscribeTemplateId, text, date, page);
                            result.ErrCode = wxJsonResult.errcode.ToString();
                            result.ErrMsg = wxJsonResult.errmsg;
                        }
                    }
                    else
                    {
                        result.Success = true;
                        result.ErrCode = "用戶拒絕訂閱";
                    }
                }
                else
                {
                    result.Success = true;
                    result.ErrCode = "用戶未訂閱";
                }

            }
            return result;
        }
        /// <summary>
        /// 新的評論提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="title">文章標題,20個以內字符</param>
        /// <param name="desc">評論內容,20個以內字符</param>
        /// <param name="date">評論時間，4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="remarkUserId">評論者（用戶）的 userId</param>
        /// <param name="page"></param>
        public static CommonResult SendRemarkNotice(string userId, string title, string desc, string date,string remarkUserId, string page)
        {
            CommonResult result = new CommonResult();
            UserApp userApp = new UserApp();
            User user = userApp.GetUserById(userId);
            User remarkUser = userApp.GetUserById(remarkUserId);

            if (user != null && !string.IsNullOrEmpty(title))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("RemarkNotice");
                if (!string.IsNullOrEmpty(template.InnerMessageSubject) && !string.IsNullOrEmpty(template.InnerMessageBody) && template.SendInnerMessage)
                {
                    MemberMessageBox memberMessageBox = new MemberMessageBox();
                    memberMessageBox.Id = GuidUtils.CreateNo();
                    memberMessageBox.IsRead = false;
                    memberMessageBox.Sernder = "系統消息";
                    memberMessageBox.Accepter = userId;
                    memberMessageBox.MsgContent = string.Format(template.InnerMessageBody, user.NickName,date, title);
                    memberMessageBoxService.Insert(memberMessageBox);
                }
                if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet )
                {
                    MemberSubscribeMsg memberSubscribeMsg = memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id, userId, "WxApplet");
                    if (memberSubscribeMsg.SubscribeStatus == "accept")
                    {
                        UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                        if (userOpenIds != null)
                        {
                            WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendRemarkNotice(userOpenIds.OpenId, template.WxAppletSubscribeTemplateId, title, desc, date, remarkUser.NickName, page);
                            result.ErrCode = wxJsonResult.errcode.ToString();
                            result.ErrMsg = wxJsonResult.errmsg;
                        }
                    }
                }
                else
                {
                    result.Success = true;
                    result.ErrCode = "用戶拒絕或未訂閱";
                }
            }
            return result;
        }

        /// <summary>
        /// 動態點贊通知
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="date">點贊時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page"></param>
        public static CommonResult SendGoodNotice(string userId, string date, string page)
        {
            CommonResult result = new CommonResult();
            User user = new UserApp().GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(date))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("RemarkNotice");

                if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet)
                {
                    MemberSubscribeMsg memberSubscribeMsg = memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id, userId, "WxApplet");
                    if (memberSubscribeMsg.SubscribeStatus == "accept")
                    {
                        UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                        if (userOpenIds != null)
                        {
                            WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendGoodNotice(userOpenIds.OpenId, template.WxAppletTemplateId, user.NickName, date, page);
                            result.ErrCode = wxJsonResult.errcode.ToString();
                            result.ErrMsg = wxJsonResult.errmsg;
                        }
                    }
                }
                else
                {
                    result.Success = true;
                    result.ErrCode = "用戶拒絕或未訂閱";
                }
            }
            return result;
        }
        /// <summary>
        /// 資訊早報提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="title">更新內容,20個以內字符</param>
        /// <param name="remark">備注,20個以內字符</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static CommonResult SendNewsMorningNotice(string userId, string title, string remark, string page = "")
        {
            CommonResult result = new CommonResult();
            User user = new UserApp().GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(title))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("NewsMorningNotice");
                if (!string.IsNullOrEmpty(template.InnerMessageSubject) && !string.IsNullOrEmpty(template.InnerMessageBody) && template.SendInnerMessage)
                {
                    MemberMessageBox memberMessageBox = new MemberMessageBox();
                    memberMessageBox.Id = GuidUtils.CreateNo();
                    memberMessageBox.IsRead = false;
                    memberMessageBox.Sernder = "資訊早報";
                    memberMessageBox.Accepter = userId;
                    memberMessageBox.MsgContent = string.Format(template.InnerMessageBody, title, remark);
                    memberMessageBoxService.Insert(memberMessageBox);
                }

                if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet)
                {
                    MemberSubscribeMsg memberSubscribeMsg = memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id, userId, "WxApplet");
                    if (memberSubscribeMsg!=null)
                    {
                        if (memberSubscribeMsg.SubscribeStatus == "accept")
                        {
                            UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                            if (userOpenIds != null)
                            {
                                WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendNewsMorningNotice(userOpenIds.OpenId, template.WxAppletSubscribeTemplateId, title, remark, page);
                                if (wxJsonResult.errcode.ToString() == "請求成功" || wxJsonResult.errcode.ToString() == "用戶拒絕接受消息")
                                {
                                    result.ErrCode = "0";
                                    result.ErrMsg = wxJsonResult.errcode.ToString();
                                }
                                else
                                {
                                    result.ErrCode = "0";
                                    result.ErrMsg = wxJsonResult.errcode.ToString() + wxJsonResult.ToJson();
                                }
                            }
                        }
                        else
                        {
                            result.ErrCode = "0";
                            result.ErrMsg = "用戶拒絕";
                        }
                    }
                    else
                    {
                        result.ErrCode = "0";
                        result.ErrMsg = "用戶未訂閱";
                    }
                }
                else
                {
                    result.ErrCode = "0";
                    result.ErrMsg = "用戶拒絕";
                }

            }
            return result;
        }


        /// <summary>
        /// 閱讀瀏覽提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="phone">接收者（用戶）的電話號碼</param>
        /// <param name="title">更新內容,20個以內字符</param>
        /// <param name="remark">備注,20個以內字符</param>
        /// <param name="smsMessage">短信消息,可選:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static CommonResult SendReadNotice(string userId, string phone, string title, string remark,string smsMessage, string page = "")
        {
            CommonResult result = new CommonResult();
            User user = new UserApp().GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(title))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("ReadNotice");

                if (!string.IsNullOrEmpty(template.InnerMessageSubject) && !string.IsNullOrEmpty(template.InnerMessageBody) && template.SendInnerMessage)
                {
                    MemberMessageBox memberMessageBox = new MemberMessageBox();
                    memberMessageBox.Id = GuidUtils.CreateNo();
                    memberMessageBox.IsRead = false;
                    memberMessageBox.Sernder = "瀏覽閱讀";
                    memberMessageBox.Accepter = userId;
                    memberMessageBox.MsgContent = string.Format(template.InnerMessageBody, title, remark);
                    memberMessageBoxService.Insert(memberMessageBox);
                }

                #region 發送小程序模板消息
                if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet)
                {
                    MemberSubscribeMsg memberSubscribeMsg = memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id, userId, "WxApplet");
                    if (memberSubscribeMsg != null)
                    {
                        if (memberSubscribeMsg.SubscribeStatus == "accept")
                        {
                            UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                            if (userOpenIds != null)
                            {
                                WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendNewsMorningNotice(userOpenIds.OpenId, template.WxAppletSubscribeTemplateId, title, remark, page);
                                if (wxJsonResult.errcode.ToString() == "請求成功" || wxJsonResult.errcode.ToString() == "用戶拒絕接受消息")
                                {
                                    result.ErrCode = "0";
                                    result.ErrMsg = wxJsonResult.errcode.ToString();
                                }
                                else
                                {
                                    result.ErrCode = "0";
                                    result.ErrMsg = wxJsonResult.errcode.ToString() + wxJsonResult.ToJson();
                                }
                            }
                        }
                        else
                        {
                            result.ErrCode = "0";
                            result.ErrMsg = "用戶拒絕";
                        }
                    }
                    else
                    {
                        result.ErrCode = "0";
                        result.ErrMsg = "用戶未訂閱";
                    }
                }
                else
                {
                    result.ErrCode = "0";
                    result.ErrMsg = "用戶拒絕";
                }
                #endregion

                #region 發送SMS短信
                if (!string.IsNullOrEmpty(template.SMSTemplateCode) && template.SendSMS)
                {
                    AliYunSMS aliYunSMS = new AliYunSMS();
                    string outmsg = string.Empty;
                    bool sendRs = aliYunSMS.Send(phone, template.SMSTemplateCode, smsMessage, out outmsg);
                    if (sendRs)
                    {
                        result.ErrCode = "0";
                        result.Success = true;
                        result.ErrMsg = "短信發送成功";
                    }
                }
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 撥打電話通知
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="phone">接收者（用戶）的電話號碼</param>
        /// <param name="message">可選:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="page"></param>
        public static CommonResult SendMakePhoneCallNotice(string userId, string phone, string message, string page)
        {
            CommonResult result = new CommonResult();
            User user = new UserApp().GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(phone))
            {
                MessageTemplates template = messageTemplatesService.GetByMessageType("MakePhoneCallNotice");
                if (template != null)
                {
                    #region 發送微信小程序模板消息
                    if (!string.IsNullOrEmpty(template.WxAppletSubscribeTemplateId) && template.UseInWxApplet)
                    {
                        MemberSubscribeMsg memberSubscribeMsg = memberSubscribeMsgService.GetByMessageTemplateIdAndUser(template.Id, userId, "WxApplet");
                        if (memberSubscribeMsg.SubscribeStatus == "accept")
                        {
                            UserOpenIds userOpenIds = new UserApp().GetUserOpenIdById(userId, "yuebon.openid.wxapplet");
                            if (userOpenIds != null)
                            {
                                WxJsonResult wxJsonResult = WxAppletSubscribeMessage.SendGoodNotice(userOpenIds.OpenId, template.WxAppletTemplateId, user.NickName, phone, page);
                                result.ErrCode = wxJsonResult.errcode.ToString();
                                result.ErrMsg = wxJsonResult.errmsg;
                            }
                        }
                    }
                    else
                    {
                        result.ErrCode = "0";
                        result.Success = true;
                        result.ErrCode = "用戶拒絕或未訂閱";
                    }
                    #endregion

                    #region 發送SMS短信
                    if (!string.IsNullOrEmpty(template.SMSTemplateCode) && template.SendSMS)
                    {
                        AliYunSMS aliYunSMS = new AliYunSMS();
                        string outmsg = string.Empty;
                        bool sendRs = aliYunSMS.Send(phone, template.SMSTemplateCode, message, out outmsg);
                        if (sendRs)
                        {
                            result.ErrCode = "0";
                            result.Success = true;
                            result.ErrMsg = "短信發送成功";
                        }
                    }
                    #endregion
                }
            }
            return result;
        }
    }
}
