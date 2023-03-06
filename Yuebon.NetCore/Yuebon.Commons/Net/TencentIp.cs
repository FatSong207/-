using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Net.TencentIp
{
    /// <summary>
    /// 通過終端設備IP地址獲取其當前所在地理位置，精確到市級，常用于顯示當地城市天氣預報、初始化用戶城市等非精確定位場景。
    /// 響應結果
    /// </summary>
    public class TencentIpResult
    {
        /// <summary>
        /// 狀態狀態碼，
        /// 0為正常,
        /// 310請求參數信息有誤，
        /// 311Key格式錯誤,
        /// 306請求有護持信息請檢查字符串,
        /// 110請求來源未被授權
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 對status的描述
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// IP定位結果
        /// </summary>
        public IpResult result { get; set; }

    }
    /// <summary>
    /// IP定位結果
    /// </summary>
    public class IpResult
    {
        /// <summary>
        /// 用于定位的IP地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 定位坐標
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 定位行政區劃信息
        /// </summary>
        public Adinfo ad_info { get; set; }

    }
    /// <summary>
    /// 定位坐標
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 緯度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public decimal lng { get; set; }

    }
    /// <summary>
    /// 定位行政區劃信息
    /// </summary>
    public class Adinfo
    {
        /// <summary>
        /// 國家
        /// </summary>
        public string nation { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 區
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 行政區劃代碼
        /// </summary>
        public int adcode { get; set; }

    }
}
