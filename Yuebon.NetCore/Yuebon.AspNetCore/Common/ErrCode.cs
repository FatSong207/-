using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.AspNetCore.Models
{
    /// <summary>
    /// 錯誤代碼描述
    /// </summary>
    public static class ErrCode
    {

        /// <summary>
        /// 請求成功
        /// </summary>
        public static string err0 = "請求成功";

        /// <summary>
        /// 請求成功代碼0
        /// </summary>
        public static string successCode = "0";

        /// <summary>
        /// 請求失敗
        /// </summary>
        public static string err1 = "請求失敗";

        /// <summary>
        /// 請求失敗代碼1
        /// </summary>
        public static string failCode = "1";

        /// <summary>
        /// 獲取access_token時AppID或AppSecret錯誤。請開發者認真比對appid和AppSecret的正確性，或查看是否正在為恰當的應用調用接口
        /// </summary>
        public static string err40001 = "獲取access_token時AppID或AppSecret錯誤。請開發者認真比對appid和AppSecret的正確性，或查看是否正在為恰當的應用調用接口";

        /// <summary>
        /// 調用接口的服務器URL地址不正確，請聯系供應商進行設置
        /// </summary>
        public static string err40002 = "調用接口的服務器URL地址不正確，請聯繫供應商進行授權 ";

        /// <summary>
        /// 請確保grant_type字段值為client_credential
        /// </summary>
        public static string err40003 = "請確保grant_type字段值為client_credential";

        /// <summary>
        /// 不合法的憑證類型
        /// </summary>
        public static string err40004 = "不合法的憑證類型";

        /// <summary>
        /// 用戶令牌accesstoken超時失效
        /// </summary>
        public static string err40005 = "用戶令牌accesstoken超時失效";

        /// <summary>
        /// 您未被授權使用該功能，請重新登錄試試或聯系管理員進行處理
        /// </summary>
        public static string err40006 = "您未被授權使用該功能，請重新登錄試試或聯系系統管理員進行處理";

        /// <summary>
        /// 傳遞參數出現錯誤
        /// </summary>
        public static string err40007 = "傳遞參數出現錯誤";

        /// <summary>
        /// 用戶未登錄或超時
        /// </summary>
        public static string err40008 = "用戶未登錄或超時";
        /// <summary>
        /// 程序異常
        /// </summary>
        public static string err40110 = "程序異常";

        /// <summary>
        /// 新增數據失敗
        /// </summary>
        public static string err43001 = "新增數據失敗";

        /// <summary>
        /// 更新數據失敗
        /// </summary>
        public static string err43002 = "更新數據失敗";

        /// <summary>
        /// 物理刪除數據失敗
        /// </summary>
        public static string err43003 = "刪除數據失敗";

        /// <summary>
        /// 該用戶不存在
        /// </summary>
        public static string err50001 = "該用戶不存在";

        /// <summary>
        /// 該用戶已存在
        /// </summary>
        public static string err50002 = "用戶已存在，請登錄或重新註冊！";

        /// <summary>
        /// 會員註冊失敗
        /// </summary>
        public static string err50003 = "會員註冊失敗";

        /// <summary>
        /// 查詢數據不存在
        /// </summary>
        public static string err60001 = "查詢數據不存在";

        /// <summary>
        /// 簽名檔以歸檔就無法進入此web表單
        /// </summary>
        public static string err41005 = "檔案已歸檔";
    }
}
