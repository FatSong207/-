using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Tree
{
    /// <summary>
    /// Vuex菜單模型
    /// </summary>
    [Serializable]
    public class VueRouterModel
    {
        /// <summary>
        /// 設定路由的名字，一定要填寫不然使用keep-alive時會出現各種問題
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 路由地址，對應當前路由的路徑，總是解析為絕對路徑
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 是否隱藏路由，當設置 true 的時候該路由不會再側邊欄出現
        /// </summary>
        public bool hidden { get; set; }
        /// <summary>
        /// 命名視圖組件,組件地址
        /// </summary>
        public string component{ get; set; }
        /// <summary>
        /// 重定向地址，當設置 noRedirect 的時候該路由在面包屑導航中不可被點擊
        /// </summary>
        public string redirect { get; set; }
        /// <summary>
        /// 當你一個路由下面的 children 聲明的路由大于1個時，自動會變成嵌套的模式--如組件頁面
        /// </summary>
        public bool alwaysShow { get; set; }
        /// <summary>
        /// 在根路由設置權限，這樣它下面所以的子路由都繼承了這個權限
        /// </summary>
        public Meta meta { get; set; }
        /// <summary>
        /// 子路由,子菜單
        /// </summary>
        public List<VueRouterModel> children { get; set; }
    }
    /// <summary>
    /// 路由元信息模型
    /// </summary>
    [Serializable]
    public class Meta
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="noCache"></param>
        public Meta(string title, string icon, bool noCache)
        {
            this.title = title;
            this.icon = icon;
            this.noCache = noCache;
        }

        /// <summary>
        /// 設置該路由在側邊欄和面包屑中展示的名字
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 設置該路由的圖標
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 設置為true，則不會被keep-alive緩存
        /// </summary>
        public bool noCache { get; set; }

        /// <summary>
        /// 當路由設置了該屬性，則會高亮相對應的側邊欄。
        /// 這在某些場景非常有用，比如：一個文章的列表頁路由為：/article/list
        /// 點擊文章進入文章詳情頁，這時候路由為/article/1，但你想在側邊欄高亮文章列表的路由，就可以進行如下設置
        /// activeMenu: '/article/list'
        /// </summary>
        public string activeMenu { get; set; }
    }
}
