﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.CO2NET.Utilities;
using Yuebon.WeChat.CommonService.Utilities;
using Microsoft.AspNetCore.Http;

namespace Yuebon.WeChat.CommonService.Download
{
    public class ConfigHelper
    {
        //Key：guid，Value：<QrCodeId,Version>
        public static Dictionary<string, CodeRecord> CodeCollection = new Dictionary<string, CodeRecord>(StringComparer.OrdinalIgnoreCase);
        public static object Lock = new object();


        public ConfigHelper()
        {
        }

        private string GetDatabaseFilePath()
        {
            return ServerUtility.ContentRootMapPath("~/App_Data/Document/Config.xml");
        }

        private XDocument GetXDocument()
        {
            var doc = XDocument.Load(GetDatabaseFilePath());
            return doc;
        }

        /// <summary>
        /// 獲取配置文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Config GetConfig()
        {
            var doc = GetXDocument();
            var config = new Config()
            {
                QrCodeId = int.Parse(doc.Root.Element("QrCodeId").Value),
                DownloadCount = int.Parse(doc.Root.Element("DownloadCount").Value),
                Versions = doc.Root.Element("Versions").Elements("Version").Select(z => z.Value).ToList(),
                WebVersions = doc.Root.Element("WebVersions").Elements("Version").Select(z => z.Value).ToList()
            };
            return config;
        }

        /// <summary>
        /// 獲取一個二維碼場景標示（自增，唯一）
        /// </summary>
        /// <returns></returns>
        public int GetQrCodeId()
        {
            lock (Lock)
            {
                var config = GetConfig();
                config.QrCodeId++;
                Save(config);
                return config.QrCodeId;
            }
        }

        public void Save(Config config)
        {
            var doc = GetXDocument();
            doc.Root.Element("QrCodeId").Value = config.QrCodeId.ToString();
            doc.Root.Element("DownloadCount").Value = config.DownloadCount.ToString();
            doc.Root.Element("Versions").Elements().Remove();
            foreach (var version in config.Versions)
            {
                doc.Root.Element("Versions").Add(new XElement("Version", version));
            }
#if NET45
            doc.Save(GetDatabaseFilePath());
#else
            using (FileStream fs = new FileStream(GetDatabaseFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                doc.Save(fs);
            }
#endif
        }

        public string Download(string version, bool isWebVersion)
        {
            lock (Lock)
            {
                var config = GetConfig();
                config.DownloadCount++;
                Save(config);
            }

            //打包下載文件
            //FileStream fs = new FileStream(_context.ServerUtility.ContentRootMapPath(string.Format("~/App_Data/Document/Files/Senparc.Weixin-v{0}.rar", version)), FileMode.Open);
            //return fs;

            var filePath = ServerUtility.ContentRootMapPath(string.Format("~/App_Data/Document/Files/Senparc.Weixin{0}-v{1}.rar", isWebVersion ? "-Web" : "", version));
            if (!File.Exists(filePath))
            {
                //使用.zip文件
                filePath = filePath.Replace(".rar", ".zip");
            }
            return filePath;
        }
    }
}
