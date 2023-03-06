using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.WxOpen.Containers;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Messages.Application;

namespace Yuebon.WebApi.Areas.Msg
{
    /// <summary>
    /// 消息推送接口
    /// </summary>
    [Route("api/Msg/[controller]")]
    [ApiController]
    public class MessagerController : ApiController
    {
        /// <summary>
        /// 撥打電話推送消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tell">接受者電話號碼</param>
        /// <param name="tell2">推送內容電話號碼</param>
        /// <returns></returns>
        [HttpGet("MakePhoneCallNotice")]
        [YuebonAuthorize("")]
        [NoPermissionRequired]
        public IActionResult SendMakePhoneCallNotice(string userId, string tell, string tell2)
        {
            CommonResult result = new CommonResult();
            try
            {
                tell = tell.Replace(" ", "");
                string message = "{\"telphone\":\"" + tell2 + "\"}";
                result = Messenger.SendMakePhoneCallNotice(userId, tell, message, "");
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("更新用戶電話號碼 UpdatePhone", ex);
            }
            return ToJsonContent(result);
        }
    }
}