using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 用戶自定義主題
    /// </summary>
    [Serializable]
    public class UserThemeInputDto
    {
        /// <summary>
        /// 主題顏色
        /// </summary>
        public string Theme { set; get; }

        /// <summary>
        /// 主題風格
        /// </summary>
        public string SideTheme { set; get; }
        /// <summary>
        /// 是否固定 Header
        /// </summary>
        public bool FixedHeader { set; get; }
        /// <summary>
        /// 是否開啟 Tags-Views
        /// </summary>
        public bool TagsView { set; get; }
        /// <summary>
        /// 是否顯示 Logo
        /// </summary>
        public bool SidebarLogo { set; get; }
    }
}
