using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 多物件管理搜索條件
    /// </summary>
    public class SearchMOModel : SearchInputDto<MO>
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

        /// <summary>
        /// 物件完整地址
        /// </summary>
        public string BAdd
        {
            get; set;
        }

        /// <summary>
        /// 物件屬性
        /// </summary>
        public string BPropoty
        {
            get; set;
        }
    }
}
