using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Log;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 文件處理幫助類
    /// </summary>
    public class FileHelper
    {

        /// <summary>
        /// 制作壓縮包（多個文件壓縮到一個壓縮包，支持加密、注釋）
        /// </summary>
        /// <param name="topDirectoryName">壓縮文件目錄</param>
        /// <param name="zipedFileName">壓縮包文件名</param>
        /// <param name="compresssionLevel">壓縮級別 1-9 </param>
        /// <param name="password">密碼</param>
        /// <param name="comment">注釋</param>
        /// <param name="filetype">文件類型</param>
        public static void ZipFiles(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment, string filetype)
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (compresssionLevel != 0)
                {
                    zos.SetLevel(compresssionLevel);//設置壓縮級別
                }
                if (!string.IsNullOrEmpty(password))
                {
                    zos.Password = password;//設置zip包加密密碼
                }
                if (!string.IsNullOrEmpty(comment))
                {
                    zos.SetComment(comment);//設置zip包的注釋
                }
                //循環設置目錄下所有的*.jpg文件（支持子目錄搜索）
                foreach (string file in Directory.GetFiles(topDirectoryName, filetype, SearchOption.AllDirectories))
                {
                    if (File.Exists(file))
                    {
                        FileInfo item = new FileInfo(file);
                        FileStream fs = File.OpenRead(item.FullName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        ZipEntry entry = new ZipEntry(item.Name);
                        zos.PutNextEntry(entry);
                        zos.Write(buffer, 0, buffer.Length);
                        fs.Close();
                    }
                }
                zos.Close();
            }
        }

        /// <summary>
        /// 壓縮多層目錄
        /// </summary>
        /// <param name="topDirectoryName">壓縮文件目錄</param>
        /// <param name="zipedFileName">壓縮包文件名</param>
        /// <param name="compresssionLevel">壓縮級別 1-9 </param>
        /// <param name="password">密碼</param>
        /// <param name="comment">注釋</param>
        /// <param name="filetype">文件類型</param>
        public static void ZipFileDirectory(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment, string filetype)
        {
            using (System.IO.FileStream ZipFile = File.Open(zipedFileName, FileMode.OpenOrCreate))
            {
                using (ZipOutputStream zos = new ZipOutputStream(ZipFile))
                {
                    if (compresssionLevel != 0)
                    {
                        zos.SetLevel(compresssionLevel);//設置壓縮級別
                    }
                    if (!string.IsNullOrEmpty(password))
                    {
                        zos.Password = password;//設置zip包加密密碼
                    }
                    if (!string.IsNullOrEmpty(comment))
                    {
                        zos.SetComment(comment);//設置zip包的注釋
                    }
                    ZipSetp(topDirectoryName, zos, "", filetype);
                }
            }
        }

        /// <summary>
        /// 遞歸遍歷目錄
        /// </summary>
        /// <param name="strDirectory">The directory.</param>
        /// <param name="s">The ZipOutputStream Object.</param>
        /// <param name="parentPath">The parent path.</param>
        private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath, string filetype)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory += Path.DirectorySeparatorChar;
            }

            Crc32 crc = new Crc32();

            string[] filenames = Directory.GetFileSystemEntries(strDirectory, filetype);
            foreach (string file in filenames)// 遍歷所有的文件和目錄
            {
                if (Directory.Exists(file))// 先當作目錄處理如果存在這個目錄就遞歸Copy該目錄下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\") + 1);
                    pPath += "\\";
                    ZipSetp(file, s, pPath, filetype);
                }
                else // 否則直接壓縮文件
                {
                    //打開壓縮文件
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(fileName);
                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;
                        fs.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);
                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 讀文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            path = path.ToFilePath();
            if (!File.Exists(path))
                return "";
            using (StreamReader stream = new StreamReader(path))
            {
                return stream.ReadToEnd(); // 讀取文件
            }
        }



        /// <summary>
        /// 寫文件
        /// </summary>
        /// <param name="path">文件路徑</param>
        /// <param name="fileName">文件名稱</param>
        /// <param name="content">文件內容</param>
        /// <param name="appendToLast">是否追加到最后</param>
        public static void WriteFile(string path, string fileName, string content, bool appendToLast = false)
        {
            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }
            path = path.ToFilePath();
            if (!Directory.Exists(path))//如果不存在就創建file文件夾
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = File.Open(path + fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] by = Encoding.Default.GetBytes(content);
                if (appendToLast)
                {
                    stream.Position = stream.Length;
                }
                else
                {
                    stream.SetLength(0);
                }
                stream.Write(by, 0, by.Length);
            }
        }

        #region 直接刪除指定目錄下的所有文件及文件夾(保留目錄)
        /// <summary>
        /// 刪除指定目錄下的所有文件及文件夾(保留目錄)
        /// </summary>
        /// <param name="file">文件目錄</param>
        public static void DeleteDirectory(string file)
        {
            try
            {
                //判斷文件夾是否還存在
                if (Directory.Exists(file))
                {
                    DirectoryInfo fileInfo = new DirectoryInfo(file);
                    //去除文件夾的只讀屬性
                    fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //去除文件的只讀屬性
                            File.SetAttributes(file, FileAttributes.Normal);
                            //如果有子文件刪除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循環遞歸刪除子文件夾
                            DeleteDirectory(f);
                        }
                    }
                    //刪除空文件夾
                    Directory.Delete(file);
                }

            }
            catch (Exception ex) // 異常處理
            {
                Log4NetHelper.Error("代碼生成異常", ex);
            }
        }

        #endregion

    }
}
