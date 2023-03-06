using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 角色授權輸入模型
    /// </summary>
    [Serializable]
    public class RoleAuthorizeDataInputDto
    {
        /// <summary>
        /// 設置角色功能
        /// </summary>
        public string[] RoleFunctios { get; set; }

        /// <summary>
        /// 設置角色可訪問系統
        /// </summary>
        public string[] RoleSystem { get; set; }
        /// <summary>
        /// 設置角色可訪問數據
        /// </summary>
        public string[] RoleData { get; set; }

        /// <summary>
        /// 設置角色Id
        /// </summary>
        public string RoleId { get; set; }
    }
}
