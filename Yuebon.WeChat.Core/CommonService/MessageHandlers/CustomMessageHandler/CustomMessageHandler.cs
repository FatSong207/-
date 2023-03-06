/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公眾號自定義MessageHandler


    創建標識：Senparc - 20150312

    修改標識：Senparc - 20171027
    修改描述：v14.8.3 添加OnUnknownTypeRequest()方法Demo

    修改標識：Senparc - 20191002
    修改描述：v16.9.102 提供 MessageHandler 中間件

----------------------------------------------------------------*/

//DPBMARK_FILE MP
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Entities.Request;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

#if NET45
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
#else
#endif

namespace Yuebon.WeChat.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定義MessageHandler
    /// 把MessageHandler作為基類，重寫對應請求的處理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>  /*如果不需要自定義，可以直接使用：MessageHandler<DefaultMpMessageContext> */
    {
        /*
         * 重要提示：v1.5起，MessageHandler提供了一個DefaultResponseMessage的抽象方法，
         * DefaultResponseMessage必須在子類中重寫，用于返回沒有處理過的消息類型（也可以用于默認消息，如幫助信息等）；
         * 其中所有原OnXX的抽象方法已經都改為虛方法，可以不必每個都重寫。若不重寫，默認返回DefaultResponseMessage方法中的結果。
         */


#if !DEBUG || NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
        string agentUrl = "http://localhost:12222/App/Weixin/4";
        string agentToken = "27C455F496044A87";
        string wiweihiKey = "CNadjJuWzyX5bz5Gn+/XoyqiqMa5DjXQ";
#else
        //下面的Url和Token可以用其他平臺的消息，或者到www.weiweihi.com註冊微信用戶，將自動在“微信營銷工具”下得到
        private string agentUrl = Config.SenparcWeixinSetting.AgentUrl;//這里使用了www.weiweihi.com微信自動托管平臺
        private string agentToken = Config.SenparcWeixinSetting.AgentToken;//Token
        private string wiweihiKey = Config.SenparcWeixinSetting.SenparcWechatAgentKey;//WeiweihiKey專門用于對接www.Weiweihi.com平臺，獲取方式見：http://www.weiweihi.com/ApiDocuments/Item/25#51
#endif

        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        /// <summary>
        /// 模板消息集合（Key：checkCode，Value：OpenId）
        /// 注意：這里只做測試，只適用于單服務器
        /// </summary>
        public static Dictionary<string, string> TemplateMessageCollection = new Dictionary<string, string>();

        /// <summary>
        /// 為中間件提供生成當前類的委托
        /// </summary>
        public static Func<Stream, PostModel, int, CustomMessageHandler> GenerateMessageHandler = (stream, postModel, maxRecordCount)
                        => new CustomMessageHandler(stream, postModel, maxRecordCount, false /* 是否只允許處理加密消息，以提高安全性 */);

        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEcryptMessage = false)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEcryptMessage)
        {
            //這里設置僅用于測試，實際開發可以在外部更全局的地方設置，
            //比如MessageHandler<MessageContext>.GlobalGlobalMessageContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;

            //OnlyAllowEcryptMessage = true;//是否只允許接收加密消息，默認為 false

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通過第三方開放平臺發送過來的請求
            }

            //在指定條件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容錯")
                {
                    return false;
                }
                return true;
            };
        }


        /// <summary>
        /// 處理文字請求
        /// </summary>
        /// <param name="requestMessage">請求消息</param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            //說明：實際項目中這里的邏輯可以交給Service處理具體信息，參考OnLocationRequest方法或/Service/LocationSercice.cs

            #region 書中例子 
            //if (requestMessage.Content == "你好")
            //{
            //    var responseMessage = base.CreateResponseMessage<ResponseMessageNews>();
            //    var title = "Title";
            //    var description = "Description";
            //    var picUrl = "PicUrl";
            //    var url = "Url";
            //    responseMessage.Articles.Add(new Article()
            //    {
            //        Title = title,
            //        Description = description,
            //        PicUrl = picUrl,
            //        Url = url
            //    });
            //    return responseMessage;
            //}
            //else if (requestMessage.Content == "Senparc")
            //{
            //    //相似處理邏輯
            //}
            //else
            //{
            //    //...
            //}

            #endregion

            #region 歷史方法

            //方法一（v0.1），此方法調用太過繁瑣，已過時（但仍是所有方法的核心基礎），建議使用方法二到四
            //var responseMessage =
            //    ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
            //    ResponseMessageText;

            //方法二（v0.4）
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);

            //方法三（v0.4），擴展方法，需要using Senparc.Weixin.MP.Helpers;
            //var responseMessage = RequestMessage.CreateResponseMessage<ResponseMessageText>();

            //方法四（v0.6+），僅適合在HandlerMessage內部使用，本質上是對方法三的封裝
            //注意：下面泛型ResponseMessageText即返回給客戶端的類型，可以根據自己的需要填寫ResponseMessageNews等不同類型。

            #endregion

            var defaultResponseMessage = base.CreateResponseMessage<ResponseMessageText>();

            var requestHandler = await requestMessage.StartHandler()
                //關鍵字不區分大小寫，按照順序匹配成功后將不再運行下面的邏輯
                .Keyword("約束", () =>
                {
                    defaultResponseMessage.Content =
                    @"您正在進行微信內置瀏覽器約束判斷測試。您可以：
<a href=""https://sdk.weixin.senparc.com/FilterTest/"">點擊這里</a>進行客戶端約束測試（地址：https://sdk.weixin.senparc.com/FilterTest/），如果在微信外打開將直接返回文字。
或：
<a href=""https://sdk.weixin.senparc.com/FilterTest/Redirect"">點擊這里</a>進行客戶端約束測試（地址：https://sdk.weixin.senparc.com/FilterTest/Redirect），如果在微信外打開將重定向一次URL。";
                    return defaultResponseMessage;
                }).
                //匹配任一關鍵字
                Keywords(new[] { "托管", "代理" }, () =>
                {
                    //開始用代理托管，把請求轉到其他服務器上去，然后拿回結果
                    //甚至也可以將所有請求在DefaultResponseMessage()中托管到外部。

                    var dt1 = SystemTime.Now; //計時開始

                    var agentXml = RequestDocument.ToString();

                    #region 暫時轉發到SDK線上Demo

                    agentUrl = "https://sdk.weixin.senparc.com/weixin";
                    //agentToken = WebConfigurationManager.AppSettings["WeixinToken"];//Token

                    //修改內容，防止死循環
                    var agentDoc = XDocument.Parse(agentXml);
                    agentDoc.Root.Element("Content").SetValue("代理轉發文字：" + requestMessage.Content);
                    agentDoc.Root.Element("CreateTime").SetValue(DateTimeHelper.GetUnixDateTime(SystemTime.Now));//修改時間，防止去重
                    agentDoc.Root.Element("MsgId").SetValue("123");//防止去重
                    agentXml = agentDoc.ToString();

                    #endregion

                    var responseXml = MessageAgent.RequestXml(this, Senparc.CO2NET.SenparcDI.GetServiceProvider(), agentUrl, agentToken, agentXml);
                    //獲取返回的XML
                    //上面的方法也可以使用擴展方法：this.RequestResponseMessage(this,agentUrl, agentToken, RequestDocument.ToString());

                    /* 如果有WeiweihiKey，可以直接使用下面的這個MessageAgent.RequestWeiweihiXml()方法。
                    * WeiweihiKey專門用于對接www.weiweihi.com平臺，獲取方式見：https://www.weiweihi.com/ApiDocuments/Item/25#51
                    */
                    //var responseXml = MessageAgent.RequestWeiweihiXml(weiweihiKey, RequestDocument.ToString());//獲取Weiweihi返回的XML

                    var dt2 = SystemTime.Now; //計時結束

                    //轉成實體。
                    /* 如果要寫成一行，可以直接用：
                    * responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
                    * 或
                    *
                    */
                    var msg = string.Format("\r\n\r\n代理過程總耗時：{0}毫秒", (dt2 - dt1).Milliseconds);
                    var agentResponseMessage = responseXml.CreateResponseMessage(this.MessageEntityEnlightener);
                    if (agentResponseMessage is ResponseMessageText)
                    {
                        (agentResponseMessage as ResponseMessageText).Content += msg;
                    }
                    else if (agentResponseMessage is ResponseMessageNews)
                    {
                        (agentResponseMessage as ResponseMessageNews).Articles[0].Description += msg;
                    }
                    return agentResponseMessage;//可能出現多種類型，直接在這里返回
                })
                .Keywords(new[] { "測試", "退出" }, () =>
                {
                    /*
                     * 這是一個特殊的過程，此請求通常來自于微微嗨（http://www.weiweihi.com）的“盛派網絡小助手”應用請求（https://www.weiweihi.com/User/App/Detail/1），
                     * 用于演示微微嗨應用商店的處理過程，由于微微嗨的應用內部可以單獨設置對話過期時間，所以這里通常不需要考慮對話狀態，只要做最簡單的響應。
                     */
                    if (defaultResponseMessage.Content == "測試")
                    {
                        //進入APP測試
                        defaultResponseMessage.Content = "您已經進入【盛派網絡小助手】的測試程序，請發送任意信息進行測試。發送文字【退出】退出測試對話。10分鐘內無任何交互將自動退出應用對話狀態。";
                    }
                    else
                    {
                        //退出APP測試
                        defaultResponseMessage.Content = "您已經退出【盛派網絡小助手】的測試程序。";
                    }
                    return defaultResponseMessage;
                })
                .Keyword("AsyncTest", () =>
                {
                    //異步并發測試（提供給單元測試使用）
#if NET45
                    var begin = SystemTime.Now;
                    int t1, t2, t3;
                    System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
                    System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
                    var end = SystemTime.Now;
                    var thread = System.Threading.Thread.CurrentThread;
                    defaultResponseMessage.Content = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
                            thread.ManagedThreadId,
                            HttpContext.Current != null ? HttpContext.Current.ApplicationInstance.GetHashCode() : -1,
                            begin,
                            end,
                            t2 - t1
                            );
#endif

                    return defaultResponseMessage;
                })
                .Keyword("OPEN", () =>
                {
                    var openResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageNews>();
                    openResponseMessage.Articles.Add(new Article()
                    {
                        Title = "開放平臺微信授權測試！",
                        Description = @"點擊進入Open授權頁面。

授權之后，您的微信所收到的消息將轉發到第三方（盛派網絡小助手）的服務器上，并獲得對應的回復。

測試完成后，您可以登陸公眾號后臺取消授權。",
                        Url = "https://sdk.weixin.senparc.com/OpenOAuth/JumpToMpOAuth"
                    });
                    return openResponseMessage;
                })
                .Keyword("錯誤", () =>
                {
                    var errorResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                    //因為沒有設置errorResponseMessage.Content，所以這小消息將無法正確返回。
                    return errorResponseMessage;
                })
                .Keyword("容錯", () =>
                {
                    Thread.Sleep(4900);//故意延時1.5秒，讓微信多次發送消息過來，觀察返回結果
                    var faultTolerantResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                    faultTolerantResponseMessage.Content = string.Format("測試容錯，MsgId：{0}，Ticks：{1}", requestMessage.MsgId,
                        SystemTime.Now.Ticks);
                    return faultTolerantResponseMessage;
                })
                .Keyword("TM", () =>
                {
                    var openId = requestMessage.FromUserName;
                    var checkCode = Guid.NewGuid().ToString("n").Substring(0, 3);//為了防止openId泄露造成騷擾，這里啟用驗證碼
                    TemplateMessageCollection[checkCode] = openId;
                    defaultResponseMessage.Content = string.Format(@"新的驗證碼為：{0}，請在網頁上輸入。網址：https://sdk.weixin.senparc.com/AsyncMethods", checkCode);
                    return defaultResponseMessage;
                })
                .Keyword("OPENID", () =>
                {
                    var openId = requestMessage.FromUserName;//獲取OpenId
                    var userInfo = Senparc.Weixin.MP.AdvancedAPIs.UserApi.Info(appId, openId, Language.zh_CN);

                    defaultResponseMessage.Content = string.Format(
                        "您的OpenID為：{0}\r\n昵稱：{1}\r\n性別：{2}\r\n地區（國家/省/市）：{3}/{4}/{5}\r\n關注時間：{6}\r\n關注狀態：{7}",
                        requestMessage.FromUserName, userInfo.nickname, (WeixinSex)userInfo.sex, userInfo.country, userInfo.province, userInfo.city, DateTimeHelper.GetDateTimeFromXml(userInfo.subscribe_time), userInfo.subscribe);
                    return defaultResponseMessage;
                })
                .Keyword("EX", () =>
                {
                    var ex = new WeixinException("openid:" + requestMessage.FromUserName + ":這是一條測試異常信息");//回調過程在global的ConfigWeixinTraceLog()方法中
                    defaultResponseMessage.Content = "請等待異步模板消息發送到此界面上（自動延時數秒）。\r\n當前時間：" + SystemTime.Now.ToString();
                    return defaultResponseMessage;
                })
                .Keyword("MUTE", () => //不回復任何消息
                {
                    //方案一：
                    return new SuccessResponseMessage();

                    //方案二：
                    var muteResponseMessage = base.CreateResponseMessage<ResponseMessageNoResponse>();
                    return muteResponseMessage;

                    //方案三：
                    base.TextResponseMessage = "success";
                    return null;

                    //方案四：
                    return null;//在 Action 中結合使用 return new FixWeixinBugWeixinResult(messageHandler);
                })
                .Keyword("JSSDK", () =>
                {
                    defaultResponseMessage.Content = "點擊打開：https://sdk.weixin.senparc.com/WeixinJsSdk";
                    return defaultResponseMessage;
                })


                //選擇菜單，關鍵字：101（微信服務器端最終格式：id="s:101",content="滿意"）
                .SelectMenuKeyword("101", () =>
                {
                    defaultResponseMessage.Content = $"感謝您的評價（{requestMessage.Content}）！我們會一如既往為提高企業和開發者生產力而努力！";
                    return defaultResponseMessage;
                })
                //選擇菜單，關鍵字：102（微信服務器端最終格式：id="s:102",content="一般"）
                .SelectMenuKeyword("102", () =>
                {
                    defaultResponseMessage.Content = $"感謝您的評價（{requestMessage.Content}）！希望我們的服務能讓您越來越滿意！";
                    return defaultResponseMessage;
                })
                //選擇菜單，關鍵字：103（微信服務器端最終格式：id="s:103",content="不滿意"）
                .SelectMenuKeyword("103", () =>
                {
                    defaultResponseMessage.Content = $"感謝您的評價（{requestMessage.Content}）！我們需要您的意見或建議，歡迎向我們反饋！ <a href=\"https://github.com/JeffreySu/WeiXinMPSDK/issues/new\">點擊這里</a>";
                    return defaultResponseMessage;
                })
                .SelectMenuKeywords(new[] { "110", "111" }, () =>
                {
                    defaultResponseMessage.Content = $"這里只是演示，可以同時支持多個選擇菜單";
                    return defaultResponseMessage;
                })


                //“一次訂閱消息”接口測試
                .Keyword("訂閱", () =>
                {
                    defaultResponseMessage.Content = "點擊打開：https://sdk.weixin.senparc.com/SubscribeMsg";
                    return defaultResponseMessage;
                })
                //正則表達式
                .Regex(@"^\d+#\d+$", () =>
                {
                    defaultResponseMessage.Content = string.Format("您輸入了：{0}，符合正則表達式：^\\d+#\\d+$", requestMessage.Content);
                    return defaultResponseMessage;
                })

                //當 Default 使用異步方法時，需要寫在最后一個，且 requestMessage.StartHandler() 前需要使用 await 等待異步方法執行；
                //當 Default 使用同步方法，不一定要在最后一個,并且不需要使用 await
                .Default(async () =>
                {
                    var result = new StringBuilder();
                    result.AppendFormat("您剛才發送了文字信息：{0}\r\n\r\n", requestMessage.Content);

                    var currentMessageContext = await base.GetCurrentMessageContext();
                    if (currentMessageContext.RequestMessages.Count > 1)
                    {
                        result.AppendFormat("您剛才還發送了如下消息（{0}/{1}）：\r\n", currentMessageContext.RequestMessages.Count,
                            currentMessageContext.StorageData);
                        for (int i = currentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
                        {
                            var historyMessage = currentMessageContext.RequestMessages[i];
                            result.AppendFormat("{0} 【{1}】{2}\r\n",
                                historyMessage.CreateTime.ToString("HH:mm:ss"),
                                historyMessage.MsgType.ToString(),
                                (historyMessage is RequestMessageText)
                                    ? (historyMessage as RequestMessageText).Content
                                    : "[非文字類型]"
                                );
                        }
                        result.AppendLine("\r\n");
                    }

                    result.AppendFormat("如果您在{0}分鐘內連續發送消息，記錄將被自動保留（當前設置：最多記錄{1}條）。過期后記錄將會自動清除。\r\n",
                        GlobalMessageContext.ExpireMinutes, GlobalMessageContext.MaxRecordCount);
                    result.AppendLine("\r\n");
                    result.AppendLine(
                        "您還可以發送【位置】【圖片】【語音】【視頻】等類型的信息（注意是這幾種類型，不是這幾個文字），查看不同格式的回復。\r\nSDK官方地址：https://sdk.weixin.senparc.com");

                    defaultResponseMessage.Content = result.ToString();

                    return defaultResponseMessage;
                });

            return requestHandler.GetResponseMessage() as IResponseMessageBase;
        }

        /// <summary>
        /// 處理位置請求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLocationRequestAsync(RequestMessageLocation requestMessage)
        {
            var locationService = new LocationService();
            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnShortVideoRequestAsync(RequestMessageShortVideo requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您剛才發送的是小視頻";
            return responseMessage;
        }

        /// <summary>
        /// 處理圖片請求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {
            //一隔一返回News或Image格式
            if (base.GlobalMessageContext.GetMessageContext(requestMessage).RequestMessages.Count() % 2 == 0)
            {
                var responseMessage = CreateResponseMessage<ResponseMessageNews>();

                responseMessage.Articles.Add(new Article()
                {
                    Title = "您剛才發送了圖片信息",
                    Description = "您發送的圖片將會顯示在邊上",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });
                responseMessage.Articles.Add(new Article()
                {
                    Title = "第二條",
                    Description = "第二條帶連接的內容",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });

                return responseMessage;
            }
            else
            {
                var responseMessage = CreateResponseMessage<ResponseMessageImage>();
                responseMessage.Image.MediaId = requestMessage.MediaId;
                return responseMessage;
            }
        }

        /// <summary>
        /// 處理語音請求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVoiceRequestAsync(RequestMessageVoice requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
            //上傳縮略圖
            //var accessToken = Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.image,
                                                         ServerUtility.ContentRootMapPath("~/Images/Logo.jpg"));

            //設置音樂信息
            responseMessage.Music.Title = "天籟之音";
            responseMessage.Music.Description = "播放您上傳的語音";
            responseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.ThumbMediaId = uploadResult.media_id;

            //推送一條客服消息
            try
            {
                CustomApi.SendText(appId, OpenId, "本次上傳的音頻MediaId：" + requestMessage.MediaId);

            }
            catch
            {
            }

            return responseMessage;
        }
        /// <summary>
        /// 處理視頻請求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVideoRequestAsync(RequestMessageVideo requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您發送了一條視頻信息，ID：" + requestMessage.MediaId;

            #region 上傳素材并推送到客戶端

            Task.Factory.StartNew(async () =>
            {
                //上傳素材
                var dir = ServerUtility.ContentRootMapPath("~/App_Data/TempVideo/");
                var file = await MediaApi.GetAsync(appId, requestMessage.MediaId, dir);
                var uploadResult = await MediaApi.UploadTemporaryMediaAsync(appId, UploadMediaFileType.video, file, 50000);
                await CustomApi.SendVideoAsync(appId, base.WeixinOpenId, uploadResult.media_id, "這是您剛才發送的視頻", "這是一條視頻消息");
            }).ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    WeixinTrace.Log("OnVideoRequest()儲存Video過程發生錯誤：", task.Exception.Message);

                    var msg = string.Format("上傳素材出錯：{0}\r\n{1}",
                               task.Exception.Message,
                               task.Exception.InnerException != null
                                   ? task.Exception.InnerException.Message
                                   : null);
                    await CustomApi.SendTextAsync(appId, base.WeixinOpenId, msg);
                }
            });

            #endregion

            return responseMessage;
        }


        /// <summary>
        /// 處理鏈接消息請求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLinkRequestAsync(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = string.Format(@"您發送了一條連接信息：
Title：{0}
Description:{1}
Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnFileRequestAsync(RequestMessageFile requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format(@"您發送了一個文件：
文件名：{0}
說明:{1}
大小：{2}
MD5:{3}", requestMessage.Title, requestMessage.Description, requestMessage.FileTotalLen, requestMessage.FileMd5);
            return responseMessage;
        }

        /// <summary>
        /// 處理事件請求（這個方法一般不用重寫，這里僅作為示例出現。除非需要在判斷具體Event類型以外對Event信息進行統一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = await base.OnEventRequestAsync(requestMessage);//對于Event下屬分類的重寫方法，見：CustomerMessageHandler_Events.cs
                                                                                      //TODO: 對Event信息進行統一操作
            return eventResponseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有沒有被處理的消息會默認返回這里的結果，
            * 因此，如果想把整個微信請求委托出去（例如需要使用分布式或從其他服務器獲取請求），
            * 只需要在這里統一發出委托請求，如：
            * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            * return responseMessage;
            */

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "這條消息來自DefaultResponseMessage。";
            return responseMessage;
        }


        public override async Task<IResponseMessageBase> OnUnknownTypeRequestAsync(RequestMessageUnknownType requestMessage)
        {
            /*
             * 此方法用于應急處理SDK沒有提供的消息類型，
             * 原始XML可以通過requestMessage.RequestDocument（或this.RequestDocument）獲取到。
             * 如果不重寫此方法，遇到未知的請求類型將會拋出異常（v14.8.3 之前的版本就是這么做的）
             */
            var msgType = Senparc.NeuChar.Helpers.MsgTypeHelper.GetRequestMsgTypeString(requestMessage.RequestDocument);
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "未知消息類型：" + msgType;

            WeixinTrace.SendCustomLog("未知請求消息類型", requestMessage.RequestDocument.ToString());//記錄到日志中

            return responseMessage;
        }
    }
}
