using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 房客-自然人搜索條件
    /// </summary>
    public class SearchCustomerRNModel : SearchInputDto<CustomerRN>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId
        {
            get; set;
        }
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
    }
}
