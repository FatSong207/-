using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.WebApi.Areas.Security.Models
{
    /// <summary>
    /// 控制臺首頁顯示內容
    /// </summary>
    [Serializable]
    public class DashboardOutModel
    {
        /// <summary>
        /// 許可使用公司名稱
        /// </summary>
        public string CertificatedCompany
        {
            get;
            set;
        }
        /// <summary>
        /// 系統訪問Url
        /// </summary>
        public string WebUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 服務器名稱
        /// </summary>
        public string MachineName
        {
            get;
            set;
        }
        /// <summary>
        /// 操作系統
        /// </summary>
        public string OSName
        {
            get;
            set;
        }
        /// <summary>
        /// 正在其上運行應用的操作系統。
        /// </summary>
        public string OSDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取正在其上運行應用的 .NET 安裝的名稱。
        /// </summary>
        public string FrameworkDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取正在其上運行當前應用的平臺體系結構。
        /// </summary>
        public string OSArchitecture
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取當前正在運行的應用的進程架構。
        /// </summary>
        public string ProcessArchitecture
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取當前計算機上的處理器數。
        /// </summary>
        public int ProcessorCount
        {
            get;
            set;
        }

        /// <summary>
        /// 獲取操作系統的內存頁的字節數。
        /// </summary>
        public int SystemPageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取映射到進程上下文的物理內存量。
        /// </summary>
        public long WorkingSet
        {
            get;
            set;
        }
        /// <summary>
        /// 獲取系統啟動后經過的毫秒數。
        /// </summary>
        public int TickCount
        {
            get;
            set;
        }

        /// <summary>
        /// 運行時長
        /// </summary>
        public string RunTimeLength
        {
            get;
            set;
        }
        /// <summary>
        /// 部署目錄
        /// </summary>
        public string Directory
        {
            get;
            set;
        }
        /// <summary>
        /// 系統版本
        /// </summary>
        public string SystemVersion
        {
            get;
            set;
        }
        /// <summary>
        /// 系統版本
        /// </summary>
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 軟件廠商
        /// </summary>
        public string Manufacturer
        {
            get;
            set;
        }
        /// <summary>
        /// 網址
        /// </summary>
        public string WebSite
        {
            get;
            set;
        }
        /// <summary>
        /// 更新地址
        /// </summary>
        public string UpdateUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 服務器IP地址
        /// </summary>
        public string IPAdress
        {
            get;
            set;
        }
        /// <summary>
        /// 服務器端口
        /// </summary>
        public string Port
        {
            get;
            set;
        }

        /// <summary>
        /// 系統名稱
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 總用戶數
        /// </summary>
        public int TotalUser
        {
            get;
            set;
        }

        /// <summary>
        /// 總模塊數
        /// </summary>
        public int TotalModule
        {
            get;
            set;
        }
        /// <summary>
        /// 總上傳文件數
        /// </summary>
        public int TotalUploadFile
        {
            get;
            set;
        }

        /// <summary>
        /// 定時任務
        /// </summary>
        public int TotalTask
        {
            get;
            set;
        }
        /// <summary>
        /// 總崗位角色數
        /// </summary>
        public int TotalRole
        {
            get;
            set;
        }
    }
}
