using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class QRCoderHelper
    {
        /// <summary>
        /// 產生QRCode
        /// </summary>
        public static void GenQRCode(string data, string savePath)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(35);

            string outputFileName = savePath;
            using (MemoryStream memory = new MemoryStream()) {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite)) {
                    qrCodeImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
