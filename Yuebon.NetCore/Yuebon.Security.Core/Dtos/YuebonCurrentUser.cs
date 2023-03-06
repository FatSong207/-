using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Tree;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 登錄成功返回用戶信息
    /// </summary>
    [Serializable]
    public class YuebonCurrentUser
    {
        /// <summary>
        /// 授權token碼
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// appkey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 用戶ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用戶帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用戶名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 昵稱
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 頭像
        /// </summary>
        public string HeadIcon { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// 頭像
        /// </summary>
        public string MemberGradeId { get; set; }

        /// <summary>
        /// 上級推廣員
        /// </summary>
        public string ReferralUserId { get; set; }
        /// <summary>
        /// 註冊時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 組織主鍵
        /// </summary>
        public virtual string OrganizeId { get; set; }
        /// <summary>
        /// 部門主鍵
        /// </summary>
        public virtual string DeptId { get; set; }
        /// <summary>
        /// 角色編碼，多個角色，使用“,”分格
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 上級主管
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 業務的店長(登入腳色為業務時才有值)
        /// </summary>
        public string StoreManagerId { get; set; }

        /// <summary>
        /// 代辦事項數
        /// </summary>
        public int TodoListCount { get; set; }


        /// <summary>
        /// 手機號碼
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 其他對象
        /// </summary>
        public object OtherOpenObj { get; set; }

        /// <summary>
        /// 微信登錄SessionId
        /// </summary>
        public string WxSessionId { get; set; }
        /// <summary>
        /// 租戶TenantId
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 登錄IP地址
        /// </summary>
        public virtual string CurrentLoginIP { get; set; }
        /// <summary>
        /// 登錄IP地址
        /// </summary>
        public virtual string IPAddressName { get; set; }


        /// <summary>
        /// 當前訪問的系統Id
        /// </summary>
        public string ActiveSystemId { get; set; }
        /// <summary>
        /// 當前訪問的系統名稱
        /// </summary>
        public string ActiveSystem { get; set; }
        /// <summary>
        /// 當前訪問的系統Url
        /// </summary>
        public string ActiveSystemUrl { get; set; }

        /// <summary>
        /// 可以訪問子系統
        /// </summary>
        public List<SystemTypeOutputDto> SubSystemList { get; set; }


        /// <summary>
        /// 授權訪問菜單
        /// </summary>
        public List<MenuOutputDto> MenusList { get; set; }
        /// <summary>
        /// 授權訪問菜單
        /// </summary>
        public List<VueRouterModel> MenusRouter { get; set; }
        /// <summary>
        /// 授權使用功能
        /// </summary>
        public List<string> Modules { get; set; }
        /// <summary>
        /// 用戶設置的軟件主題
        /// </summary>
        public string UserTheme { get; set; }
    }
}