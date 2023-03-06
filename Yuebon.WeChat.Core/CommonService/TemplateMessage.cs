using Senparc.CO2NET.Cache;
using Yuebon.WeChat.CommonService.TemplateMessage.WxOpen;
using Senparc.Weixin.TenPay.V3;
using Senparc.Weixin.WxOpen.Containers;
using System;
using System.Threading.Tasks;

namespace Yuebon.WeChat.CommonService
{
    public class TemplateMessageService
    {
        public async Task<SessionBag> RunTemplateTestAsync(string wxOpenAppId, string sessionId, string formId)
        {
            var sessionBag = await SessionContainer.GetSessionAsync(sessionId);
            var openId = sessionBag != null ? sessionBag.OpenId : "用戶未正確登陸";

            string title = null;
            decimal price = 1;//單位：分，實際使用過程中，通過數據庫獲取訂單并讀取
            string productName = null;
            string orderNumber = null;

            if (formId.StartsWith("prepay_id="))
            {
                formId = formId.Replace("prepay_id=", "");
                title = "這是來自小程序支付的模板消息（僅測試接收，數據不一定真實）";

                var cacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                var unifiedorderRequestData = await cacheStrategy.GetAsync<TenPayV3UnifiedorderRequestData>($"WxOpenUnifiedorderRequestData-{openId}");//獲取訂單請求信息緩存
                var unifedorderResult = await cacheStrategy.GetAsync<UnifiedorderResult>($"WxOpenUnifiedorderResultData-{openId}");//獲取訂單信息緩存

                if (unifedorderResult != null && formId == unifedorderResult.prepay_id)
                {
                    price = unifiedorderRequestData.TotalFee;
                    productName = unifiedorderRequestData.Body + "/緩存獲取 prepay_id 成功";
                    orderNumber = unifiedorderRequestData.OutTradeNo;
                }
                else
                {
                    productName = "緩存獲取 prepay_id 失敗";
                    orderNumber = "1234567890";
                }
                productName += " | 注意：這條消息是從小程序發起的！僅作為UI上支付成功的演示！不能確定支付真實成功！ | prepay_id：" + unifedorderResult.prepay_id;
            }
            else
            {
                title = "在線購買（僅測試小程序接收模板消息，數據不一定真實）";
                productName = "商品名稱-模板消息測試";
                orderNumber = "9876543210";
            }

            var data = new WxOpenTemplateMessage_PaySuccessNotice(title, SystemTime.Now, productName, orderNumber, price,
                            "400-031-8816", "https://sdk.senparc.weixin.com");

            //await Senparc.Weixin.WxOpen.AdvancedAPIs
            //     .Template.TemplateApi
            //     .SendTemplateMessageAsync(
            //         wxOpenAppId, openId, data.TemplateId, data, formId, "pages/index/index", "圖書", "#fff00");

            return sessionBag;

        }

        [Obsolete("建議使用 RunTemplateTestAsync 方法")]
        public SessionBag RunTemplateTest(string wxOpenAppId, string sessionId, string formId)
        {
            var sessionBag = RunTemplateTestAsync(wxOpenAppId, sessionId, formId).ConfigureAwait(false).GetAwaiter().GetResult();
            return sessionBag;
        }
    }
}
