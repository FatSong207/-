using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{

    /// <summary>
    /// 用戶搜索條件
    /// </summary>
    public class SearchExternalformModel : SearchInputDto<Externalform>
    {
        /// <summary>
        /// 表單代號
        /// </summary>
        public string FormId
        {
            get; set;
        }
        /// <summary>
        /// 表單名稱
        /// </summary>
        public string FormName
        {
            get; set;
        }

        /// <summary>
        /// 自訂標籤
        /// </summary>
        public virtual string CustTag { get; set; }

        /// <summary>
        /// 註冊或添加時間 開始
        /// </summary>
        public string CreatorTime1
        {
            get; set;
        }
        /// <summary>
        /// 註冊或添加時間 結束
        /// </summary>
        public string CreatorTime2
        {
            get; set;
        }

        /// <summary>
        /// Level
        /// </summary>
        public string selectedCategoryOptions
        {
            get; set;
        }

    }
}
