using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Models;
using Yuebon.Messages.Application;

namespace Yuebon.MessagesApp.Api.Areas.Msg.Controllers
{
    /// <summary>
    /// 消息推送接口
    /// </summary>
    [Route("api/Msg/[controller]")]
    [ApiController]
    public class SendMessageController : ApiController
    {
        /// <summary>
        /// 留言提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="text">留言內容,20個以內字符</param>
        /// <param name="date">留言時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page">點擊模板卡片后的跳轉頁面，僅限本小程序內的頁面。支持帶參數,（示例index?foo=bar）。該字段不填則模板無跳轉。</param>
        [HttpGet("SendCommentNotice")]
        public IActionResult SendCommentNotice(string userId, string text, string date, string page = "")
        {
            CommonResult result = new CommonResult();
            result = CheckToken();
            if (result.ErrCode == ErrCode.successCode)
            {
                result= Messenger.SendCommentNotice(userId, text, date, page);
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 新的評論提醒
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="title">文章標題,20個以內字符</param>
        /// <param name="desc">評論內容,20個以內字符</param>
        /// <param name="date">評論時間，4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="userNickName">評論用戶,20個以內字符</param>
        /// <param name="page"></param>
        [HttpGet("SendRemarkNotice")]
        public IActionResult SendRemarkNotice(string userId, string title, string desc, string date, string userNickName, string page)
        {
            CommonResult result = new CommonResult();
            result = CheckToken();
            if (result.ErrCode == ErrCode.successCode)
            {
                result = Messenger.SendRemarkNotice(userId, title,desc, date,userNickName, page);
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 動態點贊通知
        /// </summary>
        /// <param name="userId">接收者（用戶）的 userId</param>
        /// <param name="date">點贊時間,4小時制時間格式（支持+年月日）	例如：15:01，或：2019年10月1日 15:01</param>
        /// <param name="page"></param>
        [HttpGet("SendGoodNotice")]
        public IActionResult SendGoodNotice(string userId,  string date, string page)
        {
            CommonResult result = new CommonResult();
            result = CheckToken();
            if (result.ErrCode == ErrCode.successCode)
            {
                result = Messenger.SendGoodNotice(userId, date, page);
            }
            return ToJsonContent(result);
        }
    }
}