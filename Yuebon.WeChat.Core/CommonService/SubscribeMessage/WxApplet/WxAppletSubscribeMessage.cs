
using Senparc.Weixin.Entities;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.WxOpen.AdvancedAPIs;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.IoC;

namespace Yuebon.WeChat.CommonService.SubscribeMessage.WxApplet
{
    /// <summary>
    /// 小程序訂閱消息
    /// </summary>
    public class WxAppletSubscribeMessage
    {
        private static SenparcWeixinSetting senparcWeixinSetting = App.GetService<SenparcWeixinSetting>();
        private static readonly string weixinAppId = senparcWeixinSetting.WxOpenAppId;

        /// <summary>
        /// 留言提醒，模板編號：1069
        /// </summary>
        /// <param name="toUser">接收者（用戶）的 openid</param>
        /// <param name="templateId">消息模板Id</param>
        /// <param name="text">留言內容,20個以內字符</param>
        /// <param name="date">留言時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page">點擊模板卡片后的跳轉頁面，僅限本小程序內的頁面。支持帶參數,（示例index?foo=bar）。該字段不填則模板無跳轉。</param>
        public static WxJsonResult SendCommentNotice(string toUser,string templateId,string text,string date,string page)
        {
            var  data = new TemplateMessageData();
            data["thing1"]=new TemplateMessageDataValue(text);
            data["time2"]= new TemplateMessageDataValue(date);
            var submitData = new
            {
                touser = toUser,
                template_id = templateId,
                page = page,
                data = data
            };
            return MessageApi.SendSubscribe(weixinAppId, toUser, templateId, data, page);
        }

        /// <summary>
        /// 新的評論提醒 ，模板編號：484
        /// </summary>
        /// <param name="toUser">接收者（用戶）的 openid</param>
        /// <param name="templateId">消息模板Id</param>
        /// <param name="title">文章標題,20個以內字符</param>
        /// <param name="desc">評論內容,20個以內字符</param>
        /// <param name="date">評論時間，4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="userNick">評論用戶,20個以內字符</param>
        /// <param name="page"></param>
        public static WxJsonResult SendRemarkNotice(string toUser, string templateId, string title, string desc, string date, string userNick, string page)
        {
            var data = new TemplateMessageData();
            data.Add("thing1", new TemplateMessageDataValue(title));
            data.Add("thing2", new TemplateMessageDataValue(desc));
            data.Add("time3", new TemplateMessageDataValue(date));
            data.Add("thing5", new TemplateMessageDataValue(userNick));
           return MessageApi.SendSubscribe(weixinAppId, toUser, templateId, data, page);
        }

        /// <summary>
        /// 動態點贊通知，模板編號：579
        /// </summary>
        /// <param name="toUser">接收者（用戶）的 openid</param>
        /// <param name="templateId">消息模板Id</param>
        /// <param name="name">點贊用戶,20個以內字符</param>
        /// <param name="date">點贊時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page">點擊模板卡片后的跳轉頁面，僅限本小程序內的頁面。支持帶參數,（示例index?foo=bar）。該字段不填則模板無跳轉。</param>
        public static WxJsonResult SendGoodNotice(string toUser, string templateId, string name, string date, string page)
        {
            var data = new TemplateMessageData();
            data.Add("name1", new TemplateMessageDataValue(name));
            data.Add("date2", new TemplateMessageDataValue(date));
            return MessageApi.SendSubscribe(weixinAppId, toUser, templateId, data, page);
        }

        /// <summary>
        /// 資訊早報通知，模板編號：269
        /// </summary>
        /// <param name="toUser">接收者（用戶）的 openid</param>
        /// <param name="templateId">消息模板Id</param>
        /// <param name="title">更新內容,20個以內字符</param>
        /// <param name="remark">備注,20個以內字符</param>
        /// <param name="page">點擊模板卡片后的跳轉頁面，僅限本小程序內的頁面。支持帶參數,（示例index?foo=bar）。該字段不填則模板無跳轉。</param>
        public static WxJsonResult SendNewsMorningNotice(string toUser, string templateId, string title, string remark, string page)
        {
            var data = new TemplateMessageData();
            data["thing1"] = new TemplateMessageDataValue(title);
            data["thing2"] = new TemplateMessageDataValue(remark);
            var submitData = new
            {
                touser = toUser,
                template_id = templateId,
                page = page,
                data = data
            };
            return MessageApi.SendSubscribe(weixinAppId, toUser, templateId, data, page);
        }

        /// <summary>
        /// 校驗一張圖片是否含有違法違規內容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/imgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推薦使用AppId，需要先註冊）</param>
        /// <param name="filePath">文件完整物理路徑<para>格式支持PNG、JPEG、JPG、GIF，圖片尺寸不超過 750px * 1334px</para></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        //[ApiBind(Senparc.NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.ImgSecCheck", true)]
        //public async static Task<WxJsonResult> ImgSecCheck(string accessTokenOrAppId, string filePath, int timeOut = Config.TIME_OUT)
        //{
        //    return WxOpenApiHandlerWapper.TryCommonApi(async accessToken =>
        //    {
        //        string urlFormat = Config.ApiMpHost + "/wxa/img_sec_check?access_token={0}";
        //        var url = urlFormat.FormatWith(accessToken);
        //        var fileDic = new Dictionary<string, string>();
        //        fileDic["media"] = filePath;
        //        return await Senparc.CO2NET.HttpUtility.Post.PostFileGetJsonAsync<WxJsonResult>(url,fileDictionary: fileDic);

        //    }, accessTokenOrAppId);
        //}
       
    }
}
