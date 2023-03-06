using Senparc.CO2NET.Utilities;
using Senparc.Weixin.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yuebon.WeChat.CommonService.OpenTicket
{
    /// <summary>
    /// OpenTicket即ComponentVerifyTicket
    /// </summary>
    public class OpenTicketHelper
    {
        public static string GetOpenTicket(string componentAppId)
        {
            //實際開發過程不一定要用文件記錄，也可以用數據庫。
            var openTicketPath = ServerUtility.ContentRootMapPath("~/App_Data/OpenTicket");
            string openTicket = null;
            var filePath = Path.Combine(openTicketPath, string.Format("{0}.txt", componentAppId));
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (TextReader tr = new StreamReader(fs))
                    {
                        openTicket = tr.ReadToEnd();
                    }
                }
            }
            else
            {
                throw new WeixinException("OpenTicket不存在！");
            }

            //其他邏輯

            return openTicket;
        }
    }
}
