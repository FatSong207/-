using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.AspNetCore.ViewModel
{
    /// <summary>
    /// Vuex菜單模型
    /// </summary>
    [Serializable]
    public class VuexMenusTreeModel
    {
        /// <summary>
        /// 字符串，對應當前路由的路徑，總是解析為絕對路徑
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 命名視圖組件
        /// </summary>
        public string component { get; set; }
        /// <summary>
        /// 重定向地址，在面包屑中點擊會重定向去的地址
        /// </summary>
        public string redirect { get; set; }
       
        /// <summary>
        /// 設定路由的名字，一定要填寫不然使用keep-alive時會出現各種問題
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 在根路由設置權限，這樣它下面所以的子路由都繼承了這個權限
        /// </summary>
        public Meta meta { get; set; }
        /// <summary>
        /// 子菜單
        /// </summary>
        public List<VuexMenusTreeModel> children { get; set; }
    }
    /// <summary>
    /// VuexMenus路由模型
    /// </summary>
    [Serializable]
    public class VuexMenus
    {
        /// <summary>
        /// 訪問路徑
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 對應模塊
        /// </summary>
        public string component { get; set; }
        /// <summary>
        /// 重定向地址，在面包屑中點擊會重定向去的地址
        /// </summary>
        public string redirect { get; set; }
       
        /// <summary>
        /// 設定路由的名字，一定要填寫不然使用keep-alive時會出現各種問題
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }
    }
    /// <summary>
    /// 路由元信息模型
    /// </summary>
    [Serializable]
    public class Meta
    {
        
        /// <summary>
        /// 設置該路由在側邊欄和面包屑中展示的名字
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 設置該路由的圖標
        /// </summary>
        public string icon { get; set; }
    }
}
