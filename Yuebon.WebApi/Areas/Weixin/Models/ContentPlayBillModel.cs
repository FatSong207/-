using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.WebApi.Areas.WeiXin.Models
{
    /// <summary>
    /// 詳情頁面生成海報傳入參數模型
    /// </summary>
    [Serializable]
    public class ContentPlayBillModel
    {

        /// <summary>
        /// 內容Id
        /// </summary>
        public string ContentId
        {
            get;
            set;
        }
        /// <summary>
        /// 內容類型art 文章、job職位等
        /// </summary>
        public string ContentType
        {
            get;
            set;
        }
        /// <summary>
        /// 內容標題
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 小程序跳轉頁面
        /// </summary>
        public string Page
        {
            get;
            set;
        }

        /// <summary>
        /// 二維碼中心圖片
        /// </summary>
        public string CenterImg
        {
            get;
            set;
        }
        /// <summary>
        /// 最大32個可見字符，只支持數字，大小寫英文以及部分特殊字符：!#$'()*+,/:;=?@-._~，其它字符請自行編碼為合法字符（因不支持%，中文無法使用 urlencode 處理，請使用其他編碼方式）
        /// </summary>
        public string Scene
        {
            get;
            set;
        }
        /// <summary>
        /// 二維碼的寬度，單位 px。最小 280px，最大 1280px，默認480px
        /// </summary>
        public int Width
        {
            get;
            set;
        }

    }
}
