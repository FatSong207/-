using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons;
using Yuebon.Commons.Models;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.IServices;
using System.Security.Cryptography.X509Certificates;

namespace Yuebon.Chaochi.Core.Helpers
{
    public class SignUpHelper
    {
        public static async Task<CommonResult> EncryptURLAsync(string questTopicId, string agentId)
        {
            var result = new CommonResult();
            using HttpClient client = new();
            string url = "";
            try {
                client.DefaultRequestHeaders.Accept.Clear();
                var form = new Dictionary<string, string> {
                    {"QuestTopicId",questTopicId },
                    {"AgentId",agentId },
                };

                var o = JsonConvert.SerializeObject(form);
                var domain = Configs.GetSection("signUpUrl").Value;
                var result2 = await client.PostAsync($"{domain}/SignUpUg/Question/EncryptKey", new StringContent(o, Encoding.UTF8, "application/json"));
                string resultContent = await result2.Content.ReadAsStringAsync();
                var su = resultContent.ToObject<SignUpResult>();
                url = $"{domain}/SignUpUg/Question?k={su.data.k}";
                //QRCoderHelper.GenQRCode(url, @$"{path}\EventSatisfactionQRCode\{info.EventId}.png");

                if (!su.Success) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "生成問卷時發生錯誤";
                    return result;
                }

            } catch (HttpRequestException ex) {
                Log4NetHelper.Error(ex.ToString());
                result.ErrCode = "43001";
                result.ErrMsg = "生成問卷時發生錯誤";
                return result;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            result.ResData = url;
            return result;
        }

        public static async Task<CommonResult> GenSignUp(string questTopicId, string subject, string remarkTop, string remarkEnd, string questFile)
        {
            var result = new CommonResult();
            //Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            //Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            //var path = $"{sysSetting.ChaochiFilepath}";
            var url = "";

            using HttpClient client = new();

            try {
                client.DefaultRequestHeaders.Accept.Clear();
                var form = new Dictionary<string, string> {
                            {"QuestTopicId",questTopicId },
                            {"Subject",subject },
                            {"RemarkTop",remarkTop },
                            {"RemarkEnd",remarkEnd },
                            {"QuestFile",questFile },
                        };

                var o = JsonConvert.SerializeObject(form);

                var domain = Configs.GetSection("signUpUrl").Value;
                var result2 = await client.PostAsync($"{domain}/SignUpUg/Question/Import", new StringContent(o, Encoding.UTF8, "application/json"));
                string resultContent = await result2.Content.ReadAsStringAsync();
                var su = resultContent.ToObject<SignUpResult>();
                url = $"{domain}/SignUpUg/Question?k={su.data.k}";
                //QRCoderHelper.GenQRCode(url, @$"{path}\EventSatisfactionQRCode\{info.EventId}.png");

                if (!su.Success) {
                    result.ErrCode = "43001";
                    result.ErrMsg = "生成問卷時發生錯誤";
                    return result;
                }

            } catch (HttpRequestException ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "43001";
                result.ErrMsg = "生成問卷時發生錯誤";
                return result;
            }
            result.Success = true;
            result.ErrCode = ErrCode.successCode;
            result.ResData = url;
            return result;
        }
    }

    public class SignUpResult
    {
        public string RawBytes { get; set; }
        public bool Success { get; set; }
        public int ReturnType { get; set; }
        public Data data { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
    }

    public class Data
    {
        public int QuestTopicSN { get; set; }
        public string k { get; set; }
    }
}
