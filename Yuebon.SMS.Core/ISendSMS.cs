using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.SMS
{
    /// <summary>
    /// 短信發送接口
    /// 所有平臺短信發送都要實現這些接口方法
    /// </summary>
    public interface ISendSMS
    {
        /// <summary>
        /// 群發
        /// </summary>
        /// <param name="phoneNumbers">必填:待發送手機號。支持JSON格式的批量調用，批量上限為100個手機號碼</param>
        /// <param name="TemplateCode">必填:短信模板</param>
        /// <param name="message">必填:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="returnMsg"></param>
        /// <param name="OutId">可選:outId為提供給業務方擴展字段,最終在短信回執消息中將此值帶回給調用者</param>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool Send(string[] phoneNumbers, string TemplateCode, string message, out string returnMsg, string OutId = "", string speed = "1");
        /// <summary>
        /// 短信發送
        /// </summary>
        /// <param name="cellPhone">必填:待發送手機號。支持以逗號分隔的形式進行批量調用，批量上限為1000個手機號碼,批量調用相對于單條調用及時性稍有延遲,驗證碼類型的短信推薦使用單條調用的方式，發送國際/港澳臺消息時，接收號碼格式為00+國際區號+號碼，如“0085200000000”</param>
        /// <param name="templateCode">模板code</param>
        /// <param name="OutId">可選:outId為提供給業務方擴展字段,最終在短信回執消息中將此值帶回給調用者</param>
        /// <param name="message">可選:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="returnMsg"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool Send(string cellPhone, string templateCode, string message, out string returnMsg, string OutId = "", string speed = "0");
    }
}
