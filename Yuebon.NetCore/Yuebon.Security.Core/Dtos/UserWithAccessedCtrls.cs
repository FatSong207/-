using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 包括用戶及用戶可訪問的機構/資源/模塊/角色
    /// </summary>
    [Serializable]
    public class UserWithAccessedCtrls
    {

        public UserLoginDto User { get; set; }
        /// <summary>
        /// 用戶可以訪問到的模塊（包括所屬角色與自己的所有模塊）
        /// </summary>
        public List<MenuOutputDto> Modules { get; set; }

        /// <summary>
        /// 用戶可以訪問的資源
        /// </summary>
        public List<RoleData> RoleDatas { get; set; }

        /// <summary>
        ///  用戶所屬機構
        /// </summary>
        public List<Organize> Organizes { get; set; }

        /// <summary>
        /// 用戶所屬角色
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
