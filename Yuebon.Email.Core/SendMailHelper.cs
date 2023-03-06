using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Json;
using Yuebon.Commons.Options;

namespace Yuebon.Email
{
    /// <summary>
    /// 發送郵件
    /// </summary>
    public static class SendMailHelper
    {

        /// <summary>
        /// 發送郵件
        /// </summary>
        /// <param name="mailBodyEntity">郵件基礎信息</param>
        public static SendResultEntity SendMail(MailBodyEntity mailBodyEntity)
        {
            var sendResultEntity = new SendResultEntity();
            if (mailBodyEntity == null)
            {
                throw new ArgumentNullException();
            }

            SendServerConfigurationEntity sendServerConfiguration = new SendServerConfigurationEntity();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            AppSetting appSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (appSetting != null)
            {
                sendServerConfiguration.SmtpHost = DEncrypt.Decrypt(appSetting.Emailsmtp);
                sendServerConfiguration.SenderAccount = appSetting.Emailusername;
                sendServerConfiguration.SenderPassword = DEncrypt.Decrypt(appSetting.Emailpassword);
                sendServerConfiguration.SmtpPort = appSetting.Emailport.ToInt();
                sendServerConfiguration.IsSsl = appSetting.Emailssl.ToBool();
                sendServerConfiguration.MailEncoding ="utf-8";

                mailBodyEntity.Sender = appSetting.Emailnickname;
                mailBodyEntity.SenderAddress = appSetting.Emailusername;

            }
            else
            {
                SysSetting sysSetting = XmlConverter.Deserialize<SysSetting>("xmlconfig/sys.config");
                if (sysSetting != null) {
                    //yuebonCacheHelper.Add("SysSetting", sysSetting);

                    sendServerConfiguration.SmtpHost = DEncrypt.Decrypt(sysSetting.Emailsmtp);
                    sendServerConfiguration.SenderAccount = sysSetting.Emailusername;
                    sendServerConfiguration.SenderPassword = DEncrypt.Decrypt(sysSetting.Emailpassword);
                    sendServerConfiguration.SmtpPort = sysSetting.Emailport.ToInt();
                    sendServerConfiguration.IsSsl = sysSetting.Emailssl.ToBool();
                    sendServerConfiguration.MailEncoding = "utf-8";

                    mailBodyEntity.Sender = sysSetting.Emailnickname;
                    mailBodyEntity.SenderAddress = sysSetting.Emailusername;
                } else {
                    sendResultEntity.ResultInformation = "郵件服務器未配置";
                    sendResultEntity.ResultStatus = false;
                    throw new ArgumentNullException();
                }
            }
            sendResultEntity= SendMail(mailBodyEntity, sendServerConfiguration);
            return sendResultEntity;
        }
        /// <summary>
        /// 發送郵件
        /// </summary>
        /// <param name="mailBodyEntity">郵件基礎信息</param>
        /// <param name="sendServerConfiguration">發件人基礎信息</param>
        public static SendResultEntity SendMail(MailBodyEntity mailBodyEntity,
            SendServerConfigurationEntity sendServerConfiguration)
        {

            var sendResultEntity = new SendResultEntity();
            if (mailBodyEntity == null)
            {
                throw new ArgumentNullException();
            }

            if (sendServerConfiguration == null)
            {
                sendResultEntity.ResultInformation = "郵件服務器未配置";
                sendResultEntity.ResultStatus = false;
                throw new ArgumentNullException();
            }

            using (var client = new SmtpClient(new ProtocolLogger(MailMessage.CreateMailLog())))
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                Connection(mailBodyEntity, sendServerConfiguration, client, sendResultEntity);

                if (sendResultEntity.ResultStatus == false)
                {
                    return sendResultEntity;
                }

                SmtpClientBaseMessage(client);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                Authenticate(mailBodyEntity, sendServerConfiguration, client, sendResultEntity);

                if (sendResultEntity.ResultStatus == false)
                {
                    return sendResultEntity;
                }

                Send(mailBodyEntity, sendServerConfiguration, client, sendResultEntity);

                if (sendResultEntity.ResultStatus == false)
                {
                    return sendResultEntity;
                }
                client.Disconnect(true);
            }
            return sendResultEntity;
        }


        /// <summary>
        /// 連接服務器
        /// </summary>
        /// <param name="mailBodyEntity">郵件內容</param>
        /// <param name="sendServerConfiguration">發送配置</param>
        /// <param name="client">客戶端對象</param>
        /// <param name="sendResultEntity">發送結果</param>
        public static void Connection(MailBodyEntity mailBodyEntity, SendServerConfigurationEntity sendServerConfiguration,
            SmtpClient client, SendResultEntity sendResultEntity)
        {
            try
            {
                client.Connect(sendServerConfiguration.SmtpHost, sendServerConfiguration.SmtpPort, sendServerConfiguration.IsSsl);
            }
            catch (SmtpCommandException ex)
            {
                sendResultEntity.ResultInformation = $"嘗試連接時出錯:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
            catch (SmtpProtocolException ex)
            {
                sendResultEntity.ResultInformation = $"嘗試連接時的協議錯誤:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
            catch (Exception ex)
            {
                sendResultEntity.ResultInformation = $"服務器連接錯誤:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
        }

        /// <summary>
        /// 賬戶認證
        /// </summary>
        /// <param name="mailBodyEntity">郵件內容</param>
        /// <param name="sendServerConfiguration">發送配置</param>
        /// <param name="client">客戶端對象</param>
        /// <param name="sendResultEntity">發送結果</param>
        public static void Authenticate(MailBodyEntity mailBodyEntity, SendServerConfigurationEntity sendServerConfiguration,
            SmtpClient client, SendResultEntity sendResultEntity)
        {
            try
            {
                client.Authenticate(sendServerConfiguration.SenderAccount, sendServerConfiguration.SenderPassword);
            }
            catch (AuthenticationException ex)
            {
                sendResultEntity.ResultInformation = $"無效的用戶名或密碼:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
            catch (SmtpCommandException ex)
            {
                sendResultEntity.ResultInformation = $"嘗試驗證錯誤:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
            catch (SmtpProtocolException ex)
            {
                sendResultEntity.ResultInformation = $"嘗試驗證時的協議錯誤:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
            catch (Exception ex)
            {
                sendResultEntity.ResultInformation = $"賬戶認證錯誤:{0}" + ex.Message;
                sendResultEntity.ResultStatus = false;
            }
        }

        /// <summary>
        /// 發送郵件
        /// </summary>
        /// <param name="mailBodyEntity">郵件內容</param>
        /// <param name="sendServerConfiguration">發送配置</param>
        /// <param name="client">客戶端對象</param>
        /// <param name="sendResultEntity">發送結果</param>
        public static void Send(MailBodyEntity mailBodyEntity, SendServerConfigurationEntity sendServerConfiguration,
            SmtpClient client, SendResultEntity sendResultEntity)
        {
            try
            {
                client.Send(MailMessage.AssemblyMailMessage(mailBodyEntity));
            }
            catch (SmtpCommandException ex)
            {
                switch (ex.ErrorCode)
                {
                    case SmtpErrorCode.RecipientNotAccepted:
                        sendResultEntity.ResultInformation = $"收件人未被接受:{ex.Message}";
                        break;
                    case SmtpErrorCode.SenderNotAccepted:
                        sendResultEntity.ResultInformation = $"發件人未被接受:{ex.Message}";
                        break;
                    case SmtpErrorCode.MessageNotAccepted:
                        sendResultEntity.ResultInformation = $"消息未被接受:{ex.Message}";
                        break;
                }
                sendResultEntity.ResultStatus = false;
            }
            catch (SmtpProtocolException ex)
            {
                sendResultEntity.ResultInformation = $"發送消息時的協議錯誤:{ex.Message}";
                sendResultEntity.ResultStatus = false;
            }
            catch (Exception ex)
            {
                sendResultEntity.ResultInformation = $"郵件接收失敗:{ex.Message}";
                sendResultEntity.ResultStatus = false;
            }
        }

        /// <summary>
        /// 獲取SMTP基礎信息
        /// </summary>
        /// <param name="client">客戶端對象</param>
        /// <returns></returns>
        public static MailServerInformation SmtpClientBaseMessage(SmtpClient client)
        {
            var mailServerInformation = new MailServerInformation
            {
                Authentication = client.Capabilities.HasFlag(SmtpCapabilities.Authentication),
                BinaryMime = client.Capabilities.HasFlag(SmtpCapabilities.BinaryMime),
                Dsn = client.Capabilities.HasFlag(SmtpCapabilities.Dsn),
                EightBitMime = client.Capabilities.HasFlag(SmtpCapabilities.EightBitMime),
                Size = client.MaxSize
            };

            return mailServerInformation;
        }
    }
}
