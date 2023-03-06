using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Yuebon.Commons.Log;

namespace Yuebon.Commons.Helpers
{
    /// <summary>  
    /// Guid工具類  
    /// </summary>  
    public class Utils
    {
        /// <summary>  
        /// 將地址多字串合併成單資字串地址
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string memergeAddFix(params string[] Addarry)
        {
            //PDFDataModel_NEW.LNAdd =
            //Utils.memergeAdd(
            //   PDFDataModel_NEW.LNAdd_1,
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_1_1) ? "縣" : "",
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_1_2) ? "市" : "",
            //   PDFDataModel_NEW.LNAdd_2,
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_1) ? "鄉" : "",
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_2) ? "市" : "",
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_3) ? "鎮" : "",
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_4) ? "區" : "",
            //   PDFDataModel_NEW.LNAdd_3,
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_3_1) ? "街" : "",
            //   Utils.isYes(PDFDataModel_NEW.LNAdd_3_2) ? "路" : "",
            //   PDFDataModel_NEW.LNAdd_4,
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_4) ? "" : "段",
            //   PDFDataModel_NEW.LNAdd_5,
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_5) ? "" : "巷",
            //   PDFDataModel_NEW.LNAdd_6,
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_6) ? "" : "弄",
            //   PDFDataModel_NEW.LNAdd_7,
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_7) ? "" : "號",
            //   PDFDataModel_NEW.LNAdd_8,
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_8) ? "" : "樓",
            //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_9) ? "" : "之",
            //   PDFDataModel_NEW.LNAdd_9
            //);

            if (Addarry == null)
            {
                return "";
            }
            else if (Addarry.Length != 17)
            {
                return "地址陣列不為17";
            }
            else
            {
                string[] AddarryNew = new string[] {
                          Addarry[0],                                            //   PDFDataModel_NEW.LNAdd_1,
                          Utils.isYes(Addarry[1]) ? "縣" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_1_1) ? "縣" : "",
                          Utils.isYes(Addarry[2]) ? "市" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_1_2) ? "市" : "",
                          Addarry[3],                                            //   PDFDataModel_NEW.LNAdd_2,
                          Utils.isYes(Addarry[4]) ? "鄉" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_1) ? "鄉" : "",
                          Utils.isYes(Addarry[5]) ? "市" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_2) ? "市" : "",
                          Utils.isYes(Addarry[6]) ? "鎮" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_3) ? "鎮" : "",
                          Utils.isYes(Addarry[7]) ? "區" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_2_4) ? "區" : "",
                          Addarry[8],                                            //   PDFDataModel_NEW.LNAdd_3,
                          Utils.isYes(Addarry[9]) ? "街" : "",                   //   Utils.isYes(PDFDataModel_NEW.LNAdd_3_1) ? "街" : "",
                          Utils.isYes(Addarry[10]) ? "路" : "",                  //   Utils.isYes(PDFDataModel_NEW.LNAdd_3_2) ? "路" : "",
                          Addarry[11],                                           //   PDFDataModel_NEW.LNAdd_4,
                          string.IsNullOrWhiteSpace(Addarry[11]) ? "" : "段",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_4) ? "" : "段",
                          Addarry[12],                                           //   PDFDataModel_NEW.LNAdd_5,
                          string.IsNullOrWhiteSpace(Addarry[12]) ? "" : "巷",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_5) ? "" : "巷",
                          Addarry[13],                                           //   PDFDataModel_NEW.LNAdd_6,
                          string.IsNullOrWhiteSpace(Addarry[13]) ? "" : "弄",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_6) ? "" : "弄",
                          Addarry[14],                                           //   PDFDataModel_NEW.LNAdd_7,
                          string.IsNullOrWhiteSpace(Addarry[14]) ? "" : "號",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_7) ? "" : "號",
                          Addarry[15],                                           //   PDFDataModel_NEW.LNAdd_8,
                          string.IsNullOrWhiteSpace(Addarry[15]) ? "" : "樓",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_8) ? "" : "樓",
                          //string.IsNullOrWhiteSpace(Addarry[16]) ? "" : "之",    //   string.IsNullOrWhiteSpace(PDFDataModel_NEW.LNAdd_9) ? "" : "之",
                          Addarry[16],                                           //   PDFDataModel_NEW.LNAdd_9
                };
                return String.Join("", AddarryNew);
            }

        }

        /// <summary>  
        /// 將地址多字串合併成單資字串地址(搜尋用)
        /// </summary>  
        /// <param name="Addarry">地址陣列</param>  
        /// <returns></returns>  
        public static string memergeAddSearch(params string[] Addarry)
        {
            if (Addarry == null) {
                return "";
            } else if (Addarry.Length != 12) {
                return "地址陣列不為12";
            } else {
                string[] AddarryNew = new string[] {
                          Addarry[0],
                          Utils.isYes(Addarry[1]) ? "縣" : "",
                          Utils.isYes(Addarry[2]) ? "市" : "",
                          Addarry[3],
                          Utils.isYes(Addarry[4]) ? "鄉" : "",
                          Utils.isYes(Addarry[5]) ? "市" : "",
                          Utils.isYes(Addarry[6]) ? "鎮" : "",
                          Utils.isYes(Addarry[7]) ? "區" : "",
                          Addarry[8],
                          Utils.isYes(Addarry[9]) ? "街" : "",
                          Utils.isYes(Addarry[10]) ? "路" : "",
                          Addarry[11],
                };
                return String.Join("", AddarryNew);
            }

        }

        /// <summary>  
        /// 將地址多字串合併成單資字串地址
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string memergeAdd(params string[] Addarry)
        {
            if (Addarry == null)
            {
                return "";
            }
            else
            {
                return String.Join("",Addarry);
            }
            
        }

        /// <summary>  
        /// 判斷是否是 /Yes   PDF打勾用
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static bool isYes(string YN)
        {
            if (string.IsNullOrWhiteSpace(YN))
            {
                return false;
            }
            else if (YN == "/Yes")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 地址拆解
        /// https://blog.csdn.net/lscV001/article/details/118934339
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static (string city,
            string district,
            string town,
            string lin,
            string road,
            string sec,
            string len,
            string non,
            string no,
            string floor,
            string at) ParseAddrByRegex(string address)
        {

            if (string.IsNullOrWhiteSpace(address))
            {
                address = "";
            }

            //http://jeffwebgenie.blogspot.com/2014/06/c-regex.html
            string regex = @"(?<city>\D{2}[縣市])?(?<district>\D+[鄉鎮市區])?(?<town>\D+[村里])?(?<lin>.+[鄰])?(?<road>\D+[路街大道])?(?<sec>.+[段])?(?<len>.+[巷])?(?<non>.+[弄])?(?<no>.+[號])?(?<floor>.+[樓Ff])?(?<at>[之-].+)?";
            var m = Regex.Match(address, regex, RegexOptions.IgnoreCase);

            return (m.Groups["city"].Value,
                m.Groups["district"].Value,
                m.Groups["town"].Value,
                m.Groups["lin"].Value,
                m.Groups["road"].Value,
                m.Groups["sec"].Value,
                m.Groups["len"].Value,
                m.Groups["non"].Value,
                m.Groups["no"].Value,
                m.Groups["floor"].Value,                
                m.Groups["at"].Value);
        }

        /// <summary>
        /// 地址拆解
        /// https://blog.csdn.net/lscV001/article/details/118934339
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static void ParseAddrByRegexAndSet<T>(string address, Object obj, string prefix)
        {

            if (string.IsNullOrWhiteSpace(address))
            {
                address = "";
            }

            //http://jeffwebgenie.blogspot.com/2014/06/c-regex.html
            string regex = @"(?<city>\D{2}[縣市])?(?<district>\D+[鄉鎮市區])?(?<town>\D+[村里])?(?<lin>.+[鄰])?(?<road>\D+[路街大道])?(?<sec>.+[段])?(?<len>.+[巷])?(?<non>.+[弄])?(?<no>.+[號])?(?<floor>.+[樓Ff])?(?<at>[之-].+)?";
            var m = Regex.Match(address, regex, RegexOptions.IgnoreCase);

            var t = typeof(T);
            var publicProperties = t.GetProperties();

            foreach (var propInfo in publicProperties)
            {
                if(propInfo.Name.StartsWith(prefix))
                {
                    var overrideValue = propInfo.GetValue(obj);
                    //Console.WriteLine(propInfo.Name);
                    switch (propInfo.Name.Substring(prefix.Length))
                    {
                        case "_1":
                            if (m.Groups["city"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["city"].Value.Substring(0, m.Groups["city"].Value.Length-1));
                            }
                            break;
                        case "_1_1":
                            propInfo.SetValue(obj, m.Groups["city"].Value.EndsWith("縣") ? "/Yes" : "/Off");
                            break;
                        case "_1_2":
                            propInfo.SetValue(obj, m.Groups["city"].Value.EndsWith("市") ? "/Yes" : "/Off");
                            break;
                        case "_2":
                            if (m.Groups["district"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["district"].Value.Substring(0, m.Groups["district"].Value.Length - 1));
                            }
                            break;
                        case "_2_1":
                            propInfo.SetValue(obj, m.Groups["district"].Value.EndsWith("鄉") ? "/Yes" : "/Off");
                            break;
                        case "_2_2":
                            propInfo.SetValue(obj, m.Groups["district"].Value.EndsWith("市") ? "/Yes" : "/Off");
                            break;
                        case "_2_3":
                            propInfo.SetValue(obj, m.Groups["district"].Value.EndsWith("鎮") ? "/Yes" : "/Off");
                            break;
                        case "_2_4":
                            propInfo.SetValue(obj, m.Groups["district"].Value.EndsWith("區") ? "/Yes" : "/Off");
                            break;
                        case "_3":
                            if (m.Groups["road"].Value.Length > 1 && (m.Groups["road"].Value.EndsWith("街") || m.Groups["road"].Value.EndsWith("路")))
                            {
                                propInfo.SetValue(obj, m.Groups["road"].Value.Substring(0, m.Groups["road"].Value.Length - 1));
                            }
                            else
                            {
                                propInfo.SetValue(obj, m.Groups["road"].Value);
                            }
                            break;
                        case "_3_1":
                            propInfo.SetValue(obj, m.Groups["road"].Value.EndsWith("街") ? "/Yes" : "/Off");
                            break;
                        case "_3_2":
                            propInfo.SetValue(obj, m.Groups["road"].Value.EndsWith("路") ? "/Yes" : "/Off");
                            break;
                        case "_4":
                            if (m.Groups["sec"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["sec"].Value.Substring(0, m.Groups["sec"].Value.Length - 1));
                            }
                            break;
                        case "_5":
                            if (m.Groups["len"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["len"].Value.Substring(0, m.Groups["len"].Value.Length - 1));
                            }
                            break;
                        case "_6":
                            if (m.Groups["non"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["non"].Value.Substring(0, m.Groups["non"].Value.Length - 1));
                            }
                            break;
                        case "_7":
                            if (m.Groups["no"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["no"].Value.Substring(0, m.Groups["no"].Value.Length - 1));
                            }
                            break;
                        case "_8":
                            if (m.Groups["floor"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["floor"].Value.Substring(0, m.Groups["floor"].Value.Length - 1));
                            }
                            break;
                        case "_9":
                            if (m.Groups["at"].Value.Length > 1)
                            {
                                propInfo.SetValue(obj, m.Groups["at"].Value.Substring(0, m.Groups["at"].Value.Length - 1));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //return (m.Groups["city"].Value,
            //    m.Groups["district"].Value,
            //    m.Groups["town"].Value,
            //    m.Groups["lin"].Value,
            //    m.Groups["road"].Value,
            //    m.Groups["sec"].Value,
            //    m.Groups["len"].Value,
            //    m.Groups["non"].Value,
            //    m.Groups["no"].Value,
            //    m.Groups["floor"].Value,
            //    m.Groups["at"].Value);
        }

        /// <summary>
        /// 是否為法人
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool isCompany(string id) {
        
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new Exception($"id:{id} 空白或長度不足");
            } else if(id.Length == 8) {
                return true;
            }
            else if (id.Length == 10) {
                return false;
            }
            else {
                throw new Exception($"id:{id} 長度不為統編8碼 或身分證字號10碼");
            }
        }

        /// <summary>
        /// 拆解電話號碼
        /// </summary>
        /// <param name="tel">電話號碼</param>
        /// <returns></returns>
        public static string[] getTelArray(string tel)
        {
            string[] telArray = new string[3];
            string[] result = new string[2];
            if (string.IsNullOrWhiteSpace(tel)) {
                throw new Exception($"電話號碼:{tel}不能為空白");
            } else {
                telArray = tel.Split('-');

                if (telArray != null) {
                    switch(telArray.Length) {
                        case 1:
                            result[0] = telArray[0].Substring(0, 2);
                            result[1] = telArray[0].Substring(2, 8);
                            break;
                        case 2:
                            telArray.CopyTo(result, 0);
                            break;
                        case 3:
                            result[0] = telArray[0];
                            result[1] = telArray[1] + telArray[2];
                            break;
                    }
                } else {
                    throw new Exception($"電話號碼:{tel}格式錯誤");
                }
            }

            return result;
        }

        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="uploadPath"></param>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public static Security.Dtos.UploadFileResultOuputDto AddFile(IFormFile file, string uploadPath, string FormId)
        {
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = string.IsNullOrWhiteSpace(FormId) ? file.FileName : FormId + ".pdf";

                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, uploadPath, data);

                    Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Security.Dtos.UploadFileResultOuputDto {
                        FileName = fileName,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private static void UploadFile(string fileName, string uploadPath, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(uploadPath);
            }

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
        }
    }
}
