using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.VerificationCode
{
    /// <summary>
    /// 驗證碼接口
    /// </summary>
    public interface ICaptcha
    {
        /// <summary>
        /// 生成隨機驗證碼
        /// </summary>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        Task<string> GenerateRandomCaptchaAsync(int codeLength = 4);

        /// <summary>
        /// 生成驗證碼圖片
        /// </summary>
        /// <param name="captchaCode">驗證碼</param>
        /// <param name="width">寬為0將根據驗證碼長度自動匹配合適寬度</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        Task<CaptchaResult> GenerateCaptchaImageAsync(string captchaCode, int width = 0, int height = 30);
    }
}
