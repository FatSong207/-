using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Yuebon.WeChat.CommonService.Download;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP;
using Senparc.Weixin;

namespace Yuebon.WeChat.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定義MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        private string GetWelcomeInfo()
        {
            //獲取Senparc.Weixin.MP.dll版本信息
#if NET45
            var filePath = ServerUtility.ContentRootMapPath("~/bin/Senparc.Weixin.MP.dll");//發布路徑
#else
            //var filePath = ServerUtility.ContentRootMapPath("~/bin/Release/netcoreapp2.2/Senparc.Weixin.MP.dll");//本地測試路徑
            var filePath = ServerUtility.ContentRootMapPath("~/Senparc.Weixin.MP.dll");//發布路徑
#endif
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

            string version = fileVersionInfo == null
                ? "-"
                : string.Format("{0}.{1}.{2}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart);

            return string.Format(
@"歡迎關注【Senparc.Weixin 微信公眾平臺SDK】，當前運行版本：v{0}。
您可以發送【文字】【位置】【圖片】【語音】【文件】等不同類型的信息，查看不同格式的回復。
您也可以直接點擊菜單查看各種類型的回復。
還可以點擊菜單體驗微信支付。
SDK官方地址：https://weixin.senparc.com
SDK Demo：https://sdk.weixin.senparc.com
源代碼及Demo下載地址：https://github.com/JeffreySu/WeiXinMPSDK
Nuget地址：https://www.nuget.org/packages/Senparc.Weixin.MP
QQ群：289181996
===============
更多：
1、JSSDK測試：https://sdk.weixin.senparc.com/WeixinJSSDK
2、開放平臺測試（建議PC上打開）：https://sdk.weixin.senparc.com/OpenOAuth/JumpToMpOAuth
3、回復關鍵字：
【open】   進入第三方開放平臺（Senparc.Weixin.Open）測試
【tm】     測試異步模板消息
【openid】 獲取OpenId等用戶信息
【約束】   測試微信瀏覽器約束
【AsyncTest】 異步并發測試
【錯誤】    體驗發生錯誤無法返回正確信息
【容錯】    體驗去重容錯
【ex】      體驗錯誤日志推送提醒
【mute】     不返回任何消息，也無出錯信息
【jssdk】    測試JSSDK圖文轉發接口
格式：【數字#數字】，如2010#0102，調用正則表達式匹配
【訂閱】     測試“一次性訂閱消息”接口
",
                version);
        }

        public string GetDownloadInfo(CodeRecord codeRecord)
        {
            return string.Format(@"您已通過二維碼驗證，瀏覽器即將開始下載 Senparc.Weixin SDK 幫助文檔。
當前選擇的版本：v{0}（{1}）
我們期待您的意見和建議，客服熱線：400-031-8816。
感謝您對盛派網絡的支持！
© {2} Senparc", codeRecord.Version, codeRecord.IsWebVersion ? "網頁版" : ".chm文檔版", SystemTime.Now.Year);
        }

        public override IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            // 預處理文字或事件類型請求。
            // 這個請求是一個比較特殊的請求，通常用于統一處理來自文字或菜單按鈕的同一個執行邏輯，
            // 會在執行OnTextRequest或OnEventRequest之前觸發，具有以下一些特征：
            // 1、如果返回null，則繼續執行OnTextRequest或OnEventRequest
            // 2、如果返回不為null，則終止執行OnTextRequest或OnEventRequest，返回最終ResponseMessage
            // 3、如果是事件，則會將RequestMessageEvent自動轉為RequestMessageText類型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey

            if (requestMessage.Content == "OneClick")
            {
                var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                strongResponseMessage.Content = "您點擊了底部按鈕。\r\n為了測試微信軟件換行bug的應對措施，這里做了一個——\r\n換行";
                return strongResponseMessage;
            }
            return null;//返回null，則繼續執行OnTextRequest或OnEventRequest
        }

        /// <summary>
        /// 點擊事件
        /// </summary>
        /// <param name="requestMessage">請求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜單點擊，需要跟創建菜單時的Key匹配

            switch (requestMessage.EventKey)
            {
                case "OneClick":
                    {
                        //這個過程實際已經在OnTextOrEventRequest中命中“OneClick”關鍵字，并完成回復，這里不會執行到。
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了底部按鈕。\r\n為了測試微信軟件換行bug的應對措施，這里做了一個——\r\n換行";
                    }
                    break;
                case "SubClickRoot_Text":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了子菜單按鈕。";
                    }
                    break;
                case "SubClickRoot_News":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "您點擊了子菜單圖文按鈕",
                            Description = "您點擊了子菜單圖文按鈕，這是一條圖文信息。這個區域是Description內容\r\n可以使用\\r\\n進行換行。",
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg",
                            Url = "https://sdk.weixin.senparc.com"
                        });

                        //隨機添加一條圖文，或只輸出一條圖文信息
                        if (SystemTime.Now.Second % 2 == 0)
                        {
                            strongResponseMessage.Articles.Add(new Article()
                            {
                                Title = "這是隨機產生的第二條圖文信息，用于測試多條圖文的樣式",
                                Description = "這是隨機產生的第二條圖文信息，用于測試多條圖文的樣式",
                                PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg",
                                Url = "https://sdk.weixin.senparc.com"
                            });
                        }
                    }
                    break;
                case "SubClickRoot_Music":
                    {
                        //上傳縮略圖
                        var filePath = "~/wwwroot/Images/Logo.thumb.jpg";
                        var uploadResult = MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.thumb,
                                                                    ServerUtility.ContentRootMapPath(filePath));
                        //PS：縮略圖官方沒有特別提示文件大小限制，實際測試哪怕114K也會返回文件過大的錯誤，因此盡量控制在小一點（當前圖片39K）

                        //設置音樂信息
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageMusic>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Music.Title = "天籟之音";
                        strongResponseMessage.Music.Description = "真的是天籟之音";
                        strongResponseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.ThumbMediaId = uploadResult.thumb_media_id;
                    }
                    break;
                case "SubClickRoot_Image":
                    {
                        var filePath = "~/wwwroot/Images/Logo.jpg";
                        var uploadResult = MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.image,
                                                                     ServerUtility.ContentRootMapPath(filePath));
                        //設置圖片信息
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageImage>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Image.MediaId = uploadResult.media_id;
                    }
                    break;
                case "SendMenu"://菜單消息
                    {
                        //注意：
                        //1、此接口可以在任意地方調用（包括后臺線程），此處演示為通過
                        //2、一下"s:"前綴只是 Senparc.Weixin 的內部約定，可以使用 OnTextRequest事件中的 requestHandler.SelectMenuKeyword() 方法自動匹配到后綴（如101）

                        var menuContentList = new List<SendMenuContent>(){
                            new SendMenuContent("101","滿意"),
                            new SendMenuContent("102","一般"),
                            new SendMenuContent("103","不滿意")
                        };
                        //使用異步接口
                        CustomApi.SendMenuAsync(appId, OpenId, "請對 Senparc.Weixin SDK 給出您的評價", menuContentList, "感謝您的參與！");

                        reponseMessage = new ResponseMessageNoResponse();//不返回任何消息
                    }
                    break;
                case "SubClickRoot_Agent"://代理消息
                    {
                        //獲取返回的XML
                        var dt1 = SystemTime.Now;
                        reponseMessage = MessageAgent.RequestResponseMessage(this, Senparc.CO2NET.SenparcDI.GetServiceProvider(), agentUrl, agentToken, RequestDocument.ToString());
                        //上面的方法也可以使用擴展方法：this.RequestResponseMessage(this,agentUrl, agentToken, RequestDocument.ToString());

                        var dt2 = SystemTime.Now;

                        if (reponseMessage is ResponseMessageNews)
                        {
                            (reponseMessage as ResponseMessageNews)
                                .Articles[0]
                                .Description += string.Format("\r\n\r\n代理過程總耗時：{0}毫秒", (dt2 - dt1).Milliseconds);
                        }
                    }
                    break;
                case "Member"://托管代理會員信息
                    {
                        //原始方法為：MessageAgent.RequestXml(this,agentUrl, agentToken, RequestDocument.ToString());//獲取返回的XML
                        reponseMessage = this.RequestResponseMessage(Senparc.CO2NET.SenparcDI.GetServiceProvider(), agentUrl, agentToken, RequestDocument.ToString());
                    }
                    break;
                case "OAuth"://OAuth授權測試
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();

                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0測試",
                            Description = "選擇下面兩種不同的方式進行測試，區別在于授權成功后，最后停留的頁面。",
                            //Url = "https://sdk.weixin.senparc.com/oauth2",
                            //PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0測試（不帶returnUrl），測試環境可使用",
                            Description = "OAuth2.0測試（不帶returnUrl）",
                            Url = "https://sdk.weixin.senparc.com/oauth2",
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        var returnUrl = "/OAuth2/TestReturnUrl";
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0測試（帶returnUrl），生產環境強烈推薦使用",
                            Description = "OAuth2.0測試（帶returnUrl）",
                            Url = "https://sdk.weixin.senparc.com/oauth2?returnUrl=" + returnUrl.UrlEncode(),
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        reponseMessage = strongResponseMessage;

                    }
                    break;
                case "Description":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = GetWelcomeInfo();
                        reponseMessage = strongResponseMessage;
                    }
                    break;
                case "SubClickRoot_PicPhotoOrAlbum":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了【微信拍照】按鈕。系統將會彈出拍照或者相冊發圖。";
                    }
                    break;
                case "SubClickRoot_ScancodePush":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了【微信掃碼】按鈕。";
                    }
                    break;
                case "ConditionalMenu_Male":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了個性化菜單按鈕，您的微信性別設置為：男。";
                    }
                    break;
                case "ConditionalMenu_Femle":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您點擊了個性化菜單按鈕，您的微信性別設置為：女。";
                    }
                    break;
                case "GetNewMediaId"://獲取新的MediaId
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        try
                        {
                            var result = MediaApi.UploadForeverMedia(appId, ServerUtility.ContentRootMapPath("~/Images/logo.jpg"));
                            strongResponseMessage.Content = result.media_id;
                        }
                        catch (Exception e)
                        {
                            strongResponseMessage.Content = "發生錯誤：" + e.Message;
                            WeixinTrace.SendCustomLog("調用UploadForeverMedia()接口發生異常", e.Message);
                        }
                    }
                    break;
                default:
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您點擊了按鈕，EventKey：" + requestMessage.EventKey;
                        reponseMessage = strongResponseMessage;
                    }
                    break;
            }

            return reponseMessage;
        }

        /// <summary>
        /// 進入事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "您剛才發送了ENTER事件請求。";
            return responseMessage;
        }

        /// <summary>
        /// 位置事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            //這里是微信客戶端（通過微信服務器）自動發送過來的位置信息
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "這里寫什么都無所謂，比如：上帝愛你！";
            return responseMessage;//這里也可以返回null（需要注意寫日志時候null的問題）
        }

        /// <summary>
        /// 通過二維碼掃描關注掃描事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            //通過掃描關注
            var responseMessage = CreateResponseMessage<ResponseMessageText>();

            //下載文檔
            if (!string.IsNullOrEmpty(requestMessage.EventKey))
            {
                var sceneId = long.Parse(requestMessage.EventKey.Replace("qrscene_", ""));
                //var configHelper = new ConfigHelper(new HttpContextWrapper(HttpContext.Current));
                var codeRecord =
                    ConfigHelper.CodeCollection.Values.FirstOrDefault(z => z.QrCodeTicket != null && z.QrCodeId == sceneId);


                if (codeRecord != null)
                {
                    //確認可以下載
                    codeRecord.AllowDownload = true;
                    responseMessage.Content = GetDownloadInfo(codeRecord);
                }
            }

            responseMessage.Content = responseMessage.Content ?? string.Format("通過掃描二維碼進入，場景值：{0}", requestMessage.EventKey);

            return responseMessage;
        }

        /// <summary>
        /// 打開網頁事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            //說明：這條消息只作為接收，下面的responseMessage到達不了客戶端，類似OnEvent_UnsubscribeRequest
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您點擊了view按鈕，將打開網頁：" + requestMessage.EventKey;
            return responseMessage;
        }

        /// <summary>
        /// 群發完成事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_MassSendJobFinishRequest(RequestMessageEvent_MassSendJobFinish requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "接收到了群發完成的信息。";
            return responseMessage;
        }

        /// <summary>
        /// 訂閱（關注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = GetWelcomeInfo();
            if (!string.IsNullOrEmpty(requestMessage.EventKey))
            {
                responseMessage.Content += "\r\n============\r\n場景值：" + requestMessage.EventKey;
            }

            //推送消息
            //下載文檔
            if (requestMessage.EventKey.StartsWith("qrscene_"))
            {
                var sceneId = long.Parse(requestMessage.EventKey.Replace("qrscene_", ""));
                //var configHelper = new ConfigHelper(new HttpContextWrapper(HttpContext.Current));
                var codeRecord =
                    ConfigHelper.CodeCollection.Values.FirstOrDefault(z => z.QrCodeTicket != null && z.QrCodeId == sceneId);

                if (codeRecord != null)
                {
                    if (codeRecord.AllowDownload)
                    {
                        Task.Factory.StartNew(() => CustomApi.SendTextAsync(null, OpenId, "下載已經開始，如需下載其他版本，請刷新頁面后重新掃一掃。"));
                    }
                    else
                    {
                        //確認可以下載
                        codeRecord.AllowDownload = true;
                        Task.Factory.StartNew(() =>CustomApi.SendTextAsync(null, OpenId, GetDownloadInfo(codeRecord)));
                    }
                }
            }


            return responseMessage;
        }

        /// <summary>
        /// 退訂
        /// 實際上用戶無法收到非訂閱帳號的消息，所以這里可以隨便寫。
        /// unsubscribe事件的意義在于及時刪除網站應用中已經記錄的OpenID綁定，消除冗余數據。并且關注用戶流失的情況。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "有空再來";
            return responseMessage;
        }

        /// <summary>
        /// 事件之掃碼推事件(scancode_push)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之掃碼推事件";
            return responseMessage;
        }

        /// <summary>
        /// 事件之掃碼推事件且彈出“消息接收中”提示框(scancode_waitmsg)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之掃碼推事件且彈出“消息接收中”提示框";
            return responseMessage;
        }

        /// <summary>
        /// 事件之彈出拍照或者相冊發圖（pic_photo_or_album）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之彈出拍照或者相冊發圖";
            return responseMessage;
        }

        /// <summary>
        /// 事件之彈出系統拍照發圖(pic_sysphoto)
        /// 實際測試時發現微信并沒有推送RequestMessageEvent_Pic_Sysphoto消息，只能接收到用戶在微信中發送的圖片消息。
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之彈出系統拍照發圖";
            return responseMessage;
        }

        /// <summary>
        /// 事件之彈出微信相冊發圖器(pic_weixin)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之彈出微信相冊發圖器";
            return responseMessage;
        }

        /// <summary>
        /// 事件之彈出地理位置選擇器（location_select）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之彈出地理位置選擇器";
            return responseMessage;
        }

        /// <summary>
        /// 事件之發送模板消息返回結果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_TemplateSendJobFinishRequest(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            switch (requestMessage.Status)
            {
                case "success":
                    //發送成功

                    break;
                case "failed:user block":
                    //送達由于用戶拒收（用戶設置拒絕接收公眾號消息）而失敗
                    break;
                case "failed: system failed":
                    //送達由于其他原因失敗
                    break;
                default:
                    throw new WeixinException("未知模板消息狀態：" + requestMessage.Status);
            }

            //注意：此方法內不能再發送模板消息，否則會造成無限循環！

            try
            {
                var msg = @"已向您發送模板消息
狀態：{0}
MsgId：{1}
（這是一條來自MessageHandler的客服消息）".FormatWith(requestMessage.Status, requestMessage.MsgID);
                CustomApi.SendText(appId, OpenId, msg);//發送客服消息
            }
            catch (Exception e)
            {
                Senparc.Weixin.WeixinTrace.SendCustomLog("模板消息發送失敗", e.ToString());
            }


            //無需回復文字內容
            //return requestMessage
            //    .CreateResponseMessage<ResponseMessageNoResponse>();
            return null;
        }

        #region 微信認證事件推送

        public override IResponseMessageBase OnEvent_QualificationVerifySuccessRequest(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            //以下方法可以強制定義返回的字符串值
            //TextResponseMessage = "your content";
            //return null;

            return new SuccessResponseMessage();//返回"success"字符串
        }

        #endregion
    }
}
