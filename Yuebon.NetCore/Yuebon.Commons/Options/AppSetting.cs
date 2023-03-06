using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Yuebon.Commons.Options
{
    /// <summary>
    /// 應用設置實體類
    /// </summary>
    [Serializable]
    public class AppSetting
    {
        #region 系統基本信息
        /// <summary>
        /// 系統名稱
        /// </summary>
        [XmlElement("SoftName")]
        public string SoftName { get; set; }
        /// <summary>
        /// 系統簡介
        /// </summary>
        [XmlElement("SoftSummary")]
        public string SoftSummary { get; set; }
        /// <summary>
        /// 訪問域名
        /// </summary>
        [XmlElement("WebUrl")]
        public string WebUrl { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        [XmlElement("SysLogo")]
        public string SysLogo { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [XmlElement("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("Address")]
        public string Address { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [XmlElement("Telphone")]
        public string Telphone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [XmlElement("Email")]
        public string Email { get; set; }
        /// <summary>
        /// ICP備案號
        /// </summary>
        [XmlElement("ICPCode")]
        public string ICPCode { get; set; }
        /// <summary>
        /// 公安備案號
        /// </summary>
        [XmlElement("PublicSecurityCode")]
        public string PublicSecurityCode { get; set; }
        /// <summary>
        /// 分享標題
        /// </summary>
        [XmlElement("ShareTitle")]
        public string ShareTitle { get; set; }
        /// <summary>
        /// 微信公眾號分享圖片
        /// </summary>
        [XmlElement("ShareWeChatImage")]
        public string ShareWeChatImage { get; set; }
        /// <summary>
        /// 微信小程序分享圖片
        /// </summary>
        [XmlElement("ShareWxAppletImage")]
        public string ShareWxAppletImage { get; set; }

        /// <summary>
        /// 微信推廣二維碼背景圖片
        /// </summary>
        [XmlElement("ShareBackgroundImage")]
        public string ShareBackgroundImage { get; set; }
        #endregion


        #region 功能權限設置
        /// <summary>
        /// URL重寫開關
        /// </summary>
        [XmlElement("Staticstatus")]
        public string Staticstatus { get; set; }
        /// <summary>
        /// 靜態URL后綴
        /// </summary>
        [XmlElement("Staticextension")]
        public string Staticextension { get; set; }
        /// <summary>
        /// 開啟會員功能
        /// </summary>
        [XmlElement("Memberstatus")]
        public string Memberstatus { get; set; }
        /// <summary>
        /// 是否開啟網站
        /// </summary>
        [XmlElement("Webstatus")]
        public string Webstatus { get; set; }
        /// <summary>
        /// 網站關閉原因
        /// </summary>
        [XmlElement("Webclosereason")]
        public string Webclosereason { get; set; }
        /// <summary>
        /// 網站統計代碼
        /// </summary>
        [XmlElement("Webcountcode")]
        public string Webcountcode { get; set; }
        #endregion


        #region 短信平臺設置
        /// <summary>
        /// 短信AP地址
        /// </summary>
        [XmlElement("Smsapiurl")]
        public string Smsapiurl { get; set; }
        /// <summary>
        /// 平臺登錄賬戶或Appkey
        /// </summary>
        [XmlElement("Smsusername")]
        public string Smsusername { get; set; }
        /// <summary>
        /// 平臺通訊密鑰或Appsecret
        /// </summary>
        [XmlElement("Smspassword")]
        public string Smspassword { get; set; }
        /// <summary>
        /// 短信簽名
        /// </summary>
        [XmlElement("SmsSignName")]
        public string SmsSignName { get; set; }
        #endregion


        #region 郵件發送設置
        /// <summary>
        /// SMTP服務器
        /// </summary>
        [XmlElement("Emailsmtp")]
        public string Emailsmtp { get; set; }
        /// <summary>
        /// SSL加密連接
        /// </summary>
        [XmlElement("Emailssl")]
        public string Emailssl { get; set; }
        /// <summary>
        /// SMTP端口
        /// </summary>
        [XmlElement("Emailport")]
        public string Emailport { get; set; }
        /// <summary>
        /// 發件人地址
        /// </summary>
        [XmlElement("Emailfrom")]
        public string Emailfrom { get; set; }
        /// <summary>
        /// 郵箱帳號
        /// </summary>
        [XmlElement("Emailusername")]
        public string Emailusername { get; set; }
        /// <summary>
        /// 郵箱密碼
        /// </summary>
        [XmlElement("Emailpassword")]
        public string Emailpassword { get; set; }
        /// <summary>
        /// 發件人昵稱
        /// </summary>
        [XmlElement("Emailnickname")]
        public string Emailnickname { get; set; }
        #endregion


        #region 文件服務器
        /// <summary>
        /// 文件服務器
        /// </summary>
        [XmlElement("Fileserver")]
        public string Fileserver { get; set; }
        /// <summary>
        /// 本地文件存儲物理物理路徑
        /// </summary>
        [XmlElement("LocalPath")]
        public string LocalPath { get; set; }
        /// <summary>
        /// 阿里云KeyId
        /// </summary>
        [XmlElement("Osssecretid")]
        public string Osssecretid { get; set; }
        /// <summary>
        /// 阿里云SecretKey
        /// </summary>
        [XmlElement("Osssecretkey")]
        public string Osssecretkey { get; set; }
        /// <summary>
        /// 阿里云Bucket
        /// </summary>
        [XmlElement("Ossbucket")]
        public string Ossbucket { get; set; }
        /// <summary>
        /// 阿里云EndPoint
        /// </summary>
        [XmlElement("Ossendpoint")]
        public string Ossendpoint { get; set; }
        /// <summary>
        /// 阿里云綁定域名
        /// </summary>
        [XmlElement("Ossdomain")]
        public string Ossdomain { get; set; }
        #endregion


        #region 文件上傳設置
        /// <summary>
        /// 文件上傳目錄
        /// </summary>
        [XmlElement("Filepath")]
        public string Filepath { get; set; }
        /// <summary>
        /// 文件保存方式
        /// </summary>
        [XmlElement("Filesave")]
        public string Filesave { get; set; }
        /// <summary>
        /// 編輯器圖片
        /// </summary>
        [XmlElement("Fileremote")]
        public string Fileremote { get; set; }
        /// <summary>
        /// 文件上傳類型
        /// </summary>
        [XmlElement("Fileextension")]
        public string Fileextension { get; set; }
        /// <summary>
        /// 視頻上傳類型
        /// </summary>
        [XmlElement("Videoextension")]
        public string Videoextension { get; set; }
        /// <summary>
        /// 附件上傳大小
        /// </summary>
        [XmlElement("Attachsize")]
        public string Attachsize { get; set; }
        /// <summary>
        /// 視頻上傳大小
        /// </summary>
        [XmlElement("Videosize")]
        public string Videosize { get; set; }
        /// <summary>
        /// 圖片上傳大小
        /// </summary>
        [XmlElement("Imgsize")]
        public string Imgsize { get; set; }
        /// <summary>
        /// 圖片最大尺寸 高度
        /// </summary>
        [XmlElement("Imgmaxheight")]
        public string Imgmaxheight { get; set; }
        /// <summary>
        /// 圖片最大尺寸 寬度
        /// </summary>
        [XmlElement("Imgmaxwidth")]
        public string Imgmaxwidth { get; set; }
        /// <summary>
        /// 縮略圖生成尺寸 高度
        /// </summary>
        [XmlElement("Thumbnailheight")]
        public string Thumbnailheight { get; set; }
        /// <summary>
        /// 縮略圖生成尺寸 寬度
        /// </summary>
        [XmlElement("Thumbnailwidth")]
        public string Thumbnailwidth { get; set; }
        /// <summary>
        /// 縮略圖生成方式
        /// </summary>
        [XmlElement("Thumbnailmode")]
        public string Thumbnailmode { get; set; }
        /// <summary>
        /// 圖片水印類型
        /// </summary>
        [XmlElement("Watermarktype")]
        public string Watermarktype { get; set; }
        /// <summary>
        /// 圖片水印位置
        /// </summary>
        [XmlElement("Watermarkposition")]
        public string Watermarkposition { get; set; }
        /// <summary>
        /// 圖片生成質量
        /// </summary>
        [XmlElement("Watermarkimgquality")]
        public string Watermarkimgquality { get; set; }
        /// <summary>
        /// 圖片水印文件
        /// </summary>
        [XmlElement("Watermarkpic")]
        public string Watermarkpic { get; set; }
        /// <summary>
        /// 水印透明度
        /// </summary>
        [XmlElement("Watermarktransparency")]
        public string Watermarktransparency { get; set; }
        /// <summary>
        /// 水印文字
        /// </summary>
        [XmlElement("Watermarktext")]
        public string Watermarktext { get; set; }
        /// <summary>
        /// 文字字體格式
        /// </summary>
        [XmlElement("Watermarkfont")]
        public string Watermarkfont { get; set; }
        /// <summary>
        /// 文字字體大小
        /// </summary>
        [XmlElement("Watermarkfontsize")]
        public string Watermarkfontsize { get; set; }
        /// <summary>
        /// Chaochi文件存儲物理物理路徑
        /// </summary>
        [XmlElement("ChaochiFilepath")]
        public string ChaochiFilepath { get; set; }
        /// <summary>
        /// Chaochi更新合約狀態排程式的執行者ID
        /// </summary>
        [XmlElement("ChaochiContractStatusUpdateUserId")]
        public string ChaochiContractStatusUpdateUserId { get; set; }
        /// <summary>
        /// Chaochi更新建物廣告天數排程式的執行者ID
        /// </summary>
        [XmlElement("ChaochiBuildingAdvertisementDaysUpdateUserId")]
        public string ChaochiBuildingAdvertisementDaysUpdateUserId { get; set; }
        /// <summary>
        /// Chaochi店長角色代號
        /// </summary>
        [XmlElement("ChaochiStoreManagerAlias")]
        public string ChaochiStoreManagerAlias { get; set; }
        /// <summary>
        /// Chaochi業務角色代號
        /// </summary>
        [XmlElement("ChaochiSalesAlias")]
        public string ChaochiSalesAlias { get; set; }        
        /// <summary>
        /// Chaochi組織同步API URL_部門
        /// </summary>
        [XmlElement("ChaochiAPIURLDept")]
        public string ChaochiAPIURLDept { get; set; }
        /// <summary>
        /// Chaochi組織同步API URL_人員
        /// </summary>
        [XmlElement("ChaochiAPIURLUser")]
        public string ChaochiAPIURLUser { get; set; }
        /// <summary>
        /// Chaochi組織同步API Auth ID
        /// </summary>
        [XmlElement("ChaochiAPIAuthID")]
        public string ChaochiAPIAuthID { get; set; }
        /// <summary>
        /// Chaochi組織同步API Auth Code
        /// </summary>
        [XmlElement("ChaochiAPIAuthCode")]
        public string ChaochiAPIAuthCode { get; set; }

        #endregion
    }


    /// <summary>
    /// 系統配置  為了cache 方便設置的
    /// </summary>
    [Serializable]
    public class SysSetting : AppSetting
    {
    }
}
