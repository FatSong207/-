using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Yuebon.Commons.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public delegate void PageInfoChanged(PagerInfo info);

    /// <summary>
    /// 分頁實體
    /// </summary>
    [Serializable]
    [DataContract]
    public class PagerInfo
    {
        /// <summary>
        /// 頁面選擇事件
        /// </summary>
        public event PageInfoChanged OnPageInfoChanged;
        /// <summary>
        /// 當前頁碼
        /// </summary>
        private int currenetPageIndex;
        /// <summary>
        /// 每頁顯示的記錄
        /// </summary>
        private int pageSize;
        /// <summary>
        /// 記錄總數
        /// </summary>
        private int recordCount;

        #region 屬性變量

        /// <summary>
        /// 獲取或設置當前頁碼
        /// </summary>
        [XmlElement(ElementName = "CurrenetPageIndex")]
        [DataMember]
        public int CurrenetPageIndex
        {
            get { return currenetPageIndex; }
            set
            {
                currenetPageIndex = value;

                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        /// <summary>
        /// 獲取或設置每頁顯示的記錄
        /// </summary>
        [XmlElement(ElementName = "PageSize")]
        [DataMember]
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        /// <summary>
        /// 獲取或設置記錄總數
        /// </summary>
        [XmlElement(ElementName = "RecordCount")]
        [DataMember]
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        #endregion
    }
}
