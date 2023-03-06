using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Dtos
{
    public class SearchBuildingAdvertisementModel : SearchInputDto<BuildingAdvertisement>
    {
        /// <summary>
        /// 建物類型
        /// </summary>
        public string BPropoty { get; set; }

        /// <summary>
        /// 建物狀態
        /// </summary>
        public string BState { get; set; }

        /// <summary>
        /// 最大坪數
        /// </summary>
        public decimal BRealPINGMax { get; set; }

        /// <summary>
        /// 最小坪數
        /// </summary>
        public decimal BRealPINGMin { get; set; }

        /// <summary>
        /// 最高租金
        /// </summary>
        public int BTRentMax { get; set; }

        /// <summary>
        /// 最高租金
        /// </summary>
        public int BTRentMin { get; set; }

        /// <summary>
        /// 格局
        /// </summary>
        public string BPatten { get; set; }

        /// <summary>
        /// 電梯
        /// </summary>
        public string BElevator { get; set; }
    }
}
