using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Helpers;

namespace Yuebon.Email
{
    /// <summary>
    /// 郵件信息
    /// </summary>
    public static class MailMessage
    {
        /// <summary>
        /// 組裝郵件文本/附件郵件信息
        /// </summary>
        /// <param name="mailBodyEntity">郵件消息實體</param>
        /// <returns></returns>
        public static MimeMessage AssemblyMailMessage(MailBodyEntity mailBodyEntity)
        {
            if (mailBodyEntity == null)
            {
                throw new ArgumentNullException(nameof(mailBodyEntity));
            }
            var message = new MimeMessage();

            //設置郵件基本信息
            SetMailBaseMessage(message, mailBodyEntity);

            var multipart = new Multipart("mixed");

            //插入文本消息
            if (!string.IsNullOrEmpty(mailBodyEntity.Body))
            {
                var alternative = new MultipartAlternative
                {
                    AssemblyMailTextMessage(mailBodyEntity.Body, mailBodyEntity.MailBodyType)
                 };
                multipart.Add(alternative);
            }
            if (mailBodyEntity.MailFiles != null)
            {
                //插入附件
                foreach (var mailFile in mailBodyEntity.MailFiles)
                {
                    if (mailFile.MailFilePath != null && File.Exists(mailFile.MailFilePath))
                    {
                        var mimePart = AssemblyMailAttachmentMessage(mailFile.MailFileType, mailFile.MailFileSubType,
                             mailFile.MailFilePath);
                        multipart.Add(mimePart);
                    }
                }
            }

            //組合郵件內容
            message.Body = multipart;
            return message;
        }

        /// <summary>
        /// 設置郵件基礎信息
        /// </summary>
        /// <param name="minMessag"></param>
        /// <param name="mailBodyEntity"></param>
        /// <returns></returns>
        public static MimeMessage SetMailBaseMessage(MimeMessage minMessag, MailBodyEntity mailBodyEntity)
        {
            if (minMessag == null)
            {
                throw new ArgumentNullException();
            }
            if (mailBodyEntity == null)
            {
                throw new ArgumentNullException();
            }

            //插入發件人
            minMessag.From.Add(new MailboxAddress(mailBodyEntity.Sender, mailBodyEntity.SenderAddress));

            //插入收件人
            if (mailBodyEntity.Recipients.Any())
            {
                foreach (var recipients in mailBodyEntity.Recipients)
                {
                    minMessag.To.Add(new MailboxAddress("mailAddress",recipients));
                }
            }

            //插入抄送人
            if (mailBodyEntity.Cc != null && mailBodyEntity.Cc.Any())
            {
                foreach (var cC in mailBodyEntity.Cc)
                {
                    minMessag.Cc.Add(new MailboxAddress("mailAddress",cC));
                }
            }

            //插入密送人
            if (mailBodyEntity.Bcc != null && mailBodyEntity.Bcc.Any())
            {
                foreach (var bcc in mailBodyEntity.Bcc)
                {
                    minMessag.Bcc.Add(new MailboxAddress("mailAddress",bcc));
                }
            }

            //插入主題
            minMessag.Subject = mailBodyEntity.Subject;
            return minMessag;
        }

        /// <summary>
        /// 組裝郵件文本信息
        /// </summary>
        /// <param name="mailBody">郵件內容</param>
        /// <param name="textPartType">郵件類型(plain,html,rtf,xml)</param>
        /// <returns></returns>
        public static TextPart AssemblyMailTextMessage(string mailBody, TextFormat textPartType)
        {
            if (string.IsNullOrEmpty(mailBody))
            {
                throw new ArgumentNullException();
            }
            //var textBody = new TextPart(textPartType)
            //{
            //    Text = mailBody,
            //};

            //處理查看源文件有亂碼問題
            var textBody = new TextPart(textPartType);
            textBody.SetText(Encoding.Default, mailBody);
            return textBody;
        }

        /// <summary>
        /// 組裝郵件附件信息
        /// </summary>
        /// <param name="fileAttachmentType">附件類型(image,application)</param>
        /// <param name="fileAttachmentSubType">附件子類型 </param>
        /// <param name="fileAttachmentPath">附件路徑</param>
        /// <returns></returns>
        public static MimePart AssemblyMailAttachmentMessage(string fileAttachmentType, string fileAttachmentSubType, string fileAttachmentPath)
        {
            if (string.IsNullOrEmpty(fileAttachmentSubType))
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(fileAttachmentType))
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(fileAttachmentPath))
            {
                throw new ArgumentNullException();
            }
            var fileName = Path.GetFileName(fileAttachmentPath);
            var attachment = new MimePart(fileAttachmentType, fileAttachmentSubType)
            {
                Content = new MimeContent(File.OpenRead(fileAttachmentPath)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                //FileName = fileName,
            };

            //qq郵箱附件文件名中文亂碼問題
            //var charset = "GB18030";
            attachment.ContentType.Parameters.Add(Encoding.Default, "name", fileName);
            attachment.ContentDisposition.Parameters.Add(Encoding.Default, "filename", fileName);

            //處理文件名過長
            foreach (var param in attachment.ContentDisposition.Parameters)
                param.EncodingMethod = ParameterEncodingMethod.Rfc2047;
            foreach (var param in attachment.ContentType.Parameters)
                param.EncodingMethod = ParameterEncodingMethod.Rfc2047;

            return attachment;
        }

        /// <summary>
        /// 創建郵件日志文件
        /// </summary>
        /// <returns></returns>
        public static string CreateMailLog()
        {
            var logPath = AppDomain.CurrentDomain.BaseDirectory + "logs\\Email\\";
            var filename=DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".txt";
            if (!logPath.EndsWith("\\"))
            {
                logPath = logPath + "\\";
            }
            logPath = logPath.ToFilePath();
            if (!Directory.Exists(logPath))//如果不存在就創建file文件夾
            {
                Directory.CreateDirectory(logPath);
            }
            logPath=logPath + filename;
            if (File.Exists(logPath)) return logPath;
            var fs = File.Create(logPath);
            fs.Close();
            return logPath;

        }
    }
}
