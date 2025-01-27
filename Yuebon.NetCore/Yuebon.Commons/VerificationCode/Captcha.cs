﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.VerificationCode
{
    /// <summary>
    /// 驗證碼實現
    /// </summary>
    public class Captcha 
    {
        private const string Letters = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
        /// <summary>
        /// 生成驗證碼圖片
        /// </summary>
        /// <param name="captchaCode">驗證碼</param>
        /// <param name="width">圖形寬度，默認為驗證碼長度x25</param>
        /// <param name="height">圖形高度，默認30px</param>
        /// <returns></returns>
        public async Task<CaptchaResult> GenerateCaptchaImageAsync(string captchaCode, int width = 0, int height = 30)
        {
            //驗證碼顏色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //驗證碼字體集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

            //定義圖像的大小，生成圖像的實例
            var image = new Bitmap(width == 0 ? captchaCode.Length * 25 : width, height);

            var g = Graphics.FromImage(image);

            //背景設為白色
            g.Clear(Color.White);

            var random = new Random();
            // 畫圖片的背景噪音線
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //驗證碼繪制在g中
            for (var i = 0; i < captchaCode.Length; i++)
            {
                //隨機顏色索引值
                var cindex = random.Next(c.Length);

                //隨機字體索引值
                var findex = random.Next(fonts.Length);

                //字體
                var f = new Font(fonts[findex], 15, FontStyle.Bold);

                //顏色  
                Brush b = new SolidBrush(c[cindex]);

                var ii = 4;
                if ((i + 1) % 2 == 0)
                    ii = 2;

                //繪制一個驗證字符  
                g.DrawString(captchaCode.Substring(i, 1), f, b, 17 + (i * 17), ii);
            }
            //畫圖片的前景噪音點
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(image.Width);
                var x1 = random.Next(image.Width);
                var y = random.Next(image.Height);
                var y1 = random.Next(image.Height);
                if (i % 11 == 0)
                    g.DrawLine(new Pen(Color.Silver), x, y, x1, y1);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //畫圖片的邊框線
            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);

            g.Dispose();
            image.Dispose();

            return await Task.FromResult(new CaptchaResult
            {
                CaptchaCode = captchaCode,
                CaptchaMemoryStream = ms,
                Timestamp = DateTime.Now
            });
        }
        /// <summary>
        /// 生成隨機驗證碼字符
        /// </summary>
        /// <param name="codeLength">驗證碼位數</param>
        /// <returns></returns>
        public async Task<string> GenerateRandomCaptchaAsync(int codeLength = 4)
        {
            var array = Letters.Split(new[] { ',' });

            var random = new Random();

            var temp = -1;

            var captcheCode = string.Empty;

            for (int i = 0; i < codeLength; i++)
            {
                if (temp != -1)
                    random = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));

                var index = random.Next(array.Length);

                if (temp != -1 && temp == index)
                    return await GenerateRandomCaptchaAsync(codeLength);

                temp = index;

                captcheCode += array[index];
            }

            return await Task.FromResult(captcheCode);
        }
    }
}
