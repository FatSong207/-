using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 模塊功能
    /// </summary>
    [Serializable]
    public class ModuleFunctionOutputDto
    {
        /// <summary>
        /// 功能Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取  功能名稱
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取  功能標識 0-子系統 1-標識菜單/模塊，2標識按鈕功能
        /// </summary>
        public int FunctionTag { get; set; }
        /// <summary>
        /// 設置或獲取 是否禁止選擇
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 設置或獲取  功能標識 M-標識菜單，F標識功能
        /// </summary>
        public  List<MenuOutputDto> listFunction{ get; set; }
        /// <summary>
        /// 子菜單
        /// </summary>
        public List<ModuleFunctionOutputDto> Children { get; set; }
    }
}
