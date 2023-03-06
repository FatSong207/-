using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Email
{
    /// <summary>
    /// 郵件內容實體
    /// </summary>
    public class MailBodyEntity
    {
        ///// <summary>
        ///// 郵件文本內容
        ///// </summary>
        //public string MailTextBody { get; set; }

        /// <summary>
        /// 郵件內容類型
        /// </summary>
        public TextFormat MailBodyType { get; set; } = TextFormat.Html;

        /// <summary>
        /// 郵件附件集合
        /// </summary>
        public List<MailFile> MailFiles { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> Recipients { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public List<string> Cc { get; set; }

        /// <summary>
        /// 密送
        /// </summary>
        public List<string> Bcc { get; set; }

        /// <summary>
        /// 發件人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 發件人地址
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// 郵件主題
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 郵件內容
        /// </summary>
        public string Body { get; set; }
    }


    public class MailFile
    {
        /// <summary>
        /// 郵件附件文件類型 例如：圖片 MailFileType="image"
        /// </summary>
        public string MailFileType { get; set; }

        /// <summary>
        /// 郵件附件文件子類型 例如：圖片 MailFileSubType="png"
        /// </summary>
        public string MailFileSubType { get; set; }

        /// <summary>
        /// 郵件附件文件路徑  例如：圖片 MailFilePath=@"C:\Files\123.png"
        /// </summary>
        public string MailFilePath { get; set; }
    }

    /// <summary>
    /// 郵件服務器基礎信息
    /// </summary>
    public class MailServerInformation
    {
        /// <summary>
        /// SMTP服務器支持SASL機制類型
        /// </summary>
        public bool Authentication { get; set; }

        /// <summary>
        /// SMTP服務器對消息的大小
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// SMTP服務器支持傳遞狀態通知
        /// </summary>
        public bool Dsn { get; set; }

        /// <summary>
        /// SMTP服務器支持Content-Transfer-Encoding
        /// </summary>
        public bool EightBitMime { get; set; }

        /// <summary>
        /// SMTP服務器支持Content-Transfer-Encoding
        /// </summary>
        public bool BinaryMime { get; set; }

        /// <summary>
        /// SMTP服務器在消息頭中支持UTF-8
        /// </summary>
        public string UTF8 { get; set; }
    }

    /// <summary>
    /// 郵件發送結果
    /// </summary>
    public class SendResultEntity
    {
        /// <summary>
        /// 結果信息
        /// </summary>
        public string ResultInformation { get; set; } = "發送成功！";

        /// <summary>
        /// 結果狀態
        /// </summary>
        public bool ResultStatus { get; set; } = true;
    }

    /// <summary>
    /// 郵件發送服務器配置
    /// </summary>
    public class SendServerConfigurationEntity
    {
        /// <summary>
        /// 郵箱SMTP服務器地址
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// 郵箱SMTP服務器端口
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// 是否啟用IsSsl
        /// </summary>
        public bool IsSsl { get; set; }

        /// <summary>
        /// 郵件編碼
        /// </summary>
        public string MailEncoding { get; set; }

        /// <summary>
        /// 郵箱帳號
        /// </summary>
        public string SenderAccount { get; set; }

        /// <summary>
        /// 郵箱密碼
        /// </summary>
        public string SenderPassword { get; set; }

    }
}
