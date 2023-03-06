using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 房客-自然人搜索條件
    /// </summary>
    public class SearchContractModel : SearchInputDto<Contract>
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
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        public string LSID { get; set; }

        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        public string LSName { get; set; }

        /// <summary>
        /// 設置或獲取承租人-身份證號或統一編號
        /// </summary>
        public string RNID { get; set; }

        /// <summary>
        /// 設置或獲取承租人-姓名/法人名稱
        /// </summary>
        public string RNName { get; set; }

        /// <summary>
        /// 設置或獲取建物完整地址
        /// </summary>
        public string BAdd { get; set; }
    }
}
