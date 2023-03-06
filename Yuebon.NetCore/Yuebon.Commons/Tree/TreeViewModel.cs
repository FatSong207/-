
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Extensions;

namespace Yuebon.Commons.Tree
{
    /// <summary>
    /// 樹形視圖模型
    /// </summary>
    public class TreeViewModel
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public TreeViewModel()
        {
            this.nodes = new List<TreeViewModel>();
        }
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="nodeId">j節點Id</param>
        /// <param name="pId">父節點Id</param>
        public TreeViewModel(string nodeId, string pId)
        {
            this.nodeId = nodeId;
            this.pid = pId;
            this.nodes = new List<TreeViewModel>();
        }
        /**
         * 生成一個節點
         * @param nodeId
         * @param pId
         * @param text
         * @param icon
         * @param href
         */
        public TreeViewModel(string nodeId, string pId, string text, string icon, string href)
        {
            this.nodeId = nodeId;
            this.pid = pId;
            this.text = text;
            this.icon = icon;
            this.href = href;
            this.nodes = new List<TreeViewModel>();
        }
        /// <summary>
        /// 樹的節點Id，區別于數據庫中保存的數據Id
        /// </summary>
        public string nodeId { get; set; }
        /// <summary>
        /// 樹的父節點Id
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 節點名稱
        /// </summary>
        public string text { get; set; }  
        /// <summary>
        /// 節點圖標
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 點擊節點觸發的鏈接
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 子節點
        /// </summary>
        public List<TreeViewModel> nodes { get; set; }
        /// <summary>
        /// 節點標簽
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        /// 節點狀態
        /// </summary>
        public TreeViewSateModel state { get; set; }

    }
    /// <summary>
    /// 樹形視圖節點選擇狀態
    /// </summary>
    public class TreeViewSateModel
    {
        /// <summary>
        /// 選中
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 顯示或隱藏
        /// </summary>
        public bool? disabled { get; set; }
        /// <summary>
        /// 展開
        /// </summary>
        public bool? expanded { get; set; }
        /// <summary>
        /// 選中
        /// </summary>
        public bool? selected { get; set; }
    }
}
