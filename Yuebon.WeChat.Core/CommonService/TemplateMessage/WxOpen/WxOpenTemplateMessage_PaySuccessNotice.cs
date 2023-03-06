//DPBMARK_FILE MiniProgram
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace Yuebon.WeChat.CommonService.TemplateMessage.WxOpen
{
    public class WxOpenTemplateMessage_PaySuccessNotice : TemplateMessageBase
    {

        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; }
        public TemplateDataItem keyword4 { get; set; }
        public TemplateDataItem keyword5 { get; set; }
        public TemplateDataItem keyword6 { get; set; }

        /// <summary>
        /// “購買成功通知”模板消息數據
        /// </summary>
        /// <param name="payAddress">購買地點</param>
        /// <param name="payTime">購買時間</param>
        /// <param name="productName">物品名稱</param>
        /// <param name="orderNumber">交易單號</param>
        /// <param name="orderPrice">購買價格</param>
        /// <param name="hotLine">售后電話</param>
        /// <param name="url"></param>
        /// <param name="templateId"></param>
        public WxOpenTemplateMessage_PaySuccessNotice(
            string payAddress, DateTimeOffset payTime, string productName,
            string orderNumber, decimal orderPrice, string hotLine,
            string url,
            //根據實際的“模板ID”進行修改
            string templateId = "Ap1S3tRvsB8BXsWkiILLz93nhe7S8IgAipZDfygy9Bg")
            : base(templateId, url, "購買成功通知")
        {
            /* 
                關鍵詞
                購買地點 {{keyword1.DATA}}
                購買時間 {{keyword2.DATA}}
                物品名稱 {{keyword3.DATA}}
                交易單號 {{keyword4.DATA}}
                購買價格 {{keyword5.DATA}}
                售后電話 {{keyword6.DATA}}
                */

            keyword1 = new TemplateDataItem(payAddress);
            keyword2 = new TemplateDataItem(payTime.LocalDateTime.ToString());
            keyword3 = new TemplateDataItem(productName);
            keyword4 = new TemplateDataItem(orderNumber);
            keyword5 = new TemplateDataItem(orderPrice.ToString("C"));
            keyword6 = new TemplateDataItem(hotLine);
        }
    }

}
