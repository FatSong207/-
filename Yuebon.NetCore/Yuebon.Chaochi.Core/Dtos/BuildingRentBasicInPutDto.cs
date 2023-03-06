using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Building))]
    [Serializable]
    public class BuildingRentBasicInputDto
    {

        /// <summary>
        /// 主鍵
        /// </summary>
        public virtual string Id { get; set; }


        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }


        /// <summary>
        /// 建物座落建號(段)
        /// </summary>
        public virtual string BNo_1 { get; set; }

        /// <summary>
        /// 建物座落建號（小段）
        /// </summary>
        public virtual string BNo_2 { get; set; }

        /// <summary>
        /// 建物座落建號（建號）
        /// </summary>
        public virtual string BNo_3 { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)A
        /// </summary>
        public virtual string BPNo_1_A { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)A
        /// </summary>
        public virtual string BPNo_2_A { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)A
        /// </summary>
        public virtual string BPNo_3_A { get; set; }


        /// <summary>
        /// 屬於哪一個房東
        /// </summary>
        public virtual string BelongLandlord { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)B
        /// </summary>
        public virtual string BPNo_1_B { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)B
        /// </summary>
        public virtual string BPNo_2_B { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)B
        /// </summary>
        public virtual string BPNo_3_B { get; set; }

        ///// <summary>
        ///// 稅籍編號：
        ///// </summary>
        //public string B1TaxID { get; set; }


        /// <summary>
        /// 12/27為了B1TaxID欄位
        /// </summary>
        public Building1 building1 { get; set; }

        /// <summary>
        /// 附屬建物1
        /// </summary>
        public virtual string BAttachBuilding_1 { get; set; }
        public virtual string BAttachBuilding_2 { get; set; }
        public virtual string BAttachBuilding_3 { get; set; }
        public virtual string BAttachBuilding_4 { get; set; }
        public virtual string BAttachBuilding_5 { get; set; }

        /// <summary>
        /// 附屬建物面積1
        /// </summary>
        public virtual string BAttachBuildingArea_1 { get; set; }
        public virtual string BAttachBuildingArea_2 { get; set; }
        public virtual string BAttachBuildingArea_3 { get; set; }
        public virtual string BAttachBuildingArea_4 { get; set; }
        public virtual string BAttachBuildingArea_5 { get; set; }

        /// <summary>
        /// 附屬建物坪數1
        /// </summary>
        public virtual string BAttachBuildingPing_1 { get; set; }
        public virtual string BAttachBuildingPing_2 { get; set; }
        public virtual string BAttachBuildingPing_3 { get; set; }
        public virtual string BAttachBuildingPing_4 { get; set; }
        public virtual string BAttachBuildingPing_5 { get; set; }

        /// <summary>
        /// 共有部分1
        /// </summary>
        public virtual string BJoin_1 { get; set; }
        public virtual string BJoin_2 { get; set; }
        public virtual string BJoin_3 { get; set; }
        public virtual string BJoin_4 { get; set; }
        public virtual string BJoin_5 { get; set; }

        /// <summary>
        /// 共有部分建號1
        /// </summary>
        public virtual string BJoinPno_1 { get; set; }
        public virtual string BJoinPno_2 { get; set; }
        public virtual string BJoinPno_3 { get; set; }
        public virtual string BJoinPno_4 { get; set; }
        public virtual string BJoinPno_5 { get; set; }

        /// <summary>
        /// 共有部分面積1
        /// </summary>
        public virtual string BJoinArea_1 { get; set; }
        public virtual string BJoinArea_2 { get; set; }
        public virtual string BJoinArea_3 { get; set; }
        public virtual string BJoinArea_4 { get; set; }
        public virtual string BJoinArea_5 { get; set; }

        /// <summary>
        /// 共有部分權力範圍分子1
        /// </summary>
        public virtual string BJoinOwner1_1 { get; set; }

        /// <summary>
        /// 共有部分權力範圍分母1
        /// </summary>
        public virtual string BJoinOwner2_1 { get; set; }
        public virtual string BJoinOwner1_2 { get; set; }
        public virtual string BJoinOwner2_2 { get; set; }
        public virtual string BJoinOwner1_3 { get; set; }
        public virtual string BJoinOwner2_3 { get; set; }
        public virtual string BJoinOwner1_4 { get; set; }
        public virtual string BJoinOwner2_4 { get; set; }
        public virtual string BJoinOwner1_5 { get; set; }
        public virtual string BJoinOwner2_5 { get; set; }

        /// <summary>
        /// 共有部分權力範圍面積1
        /// </summary>
        public virtual string BJoinOwnerArea_1 { get; set; }
        public virtual string BJoinOwnerArea_2 { get; set; }
        public virtual string BJoinOwnerArea_3 { get; set; }
        public virtual string BJoinOwnerArea_4 { get; set; }
        public virtual string BJoinOwnerArea_5 { get; set; }

        /// <summary>
        /// 共有部分權力範圍坪數1
        /// </summary>
        public virtual string BJoinOwnerPing_1 { get; set; }
        public virtual string BJoinOwnerPing_2 { get; set; }
        public virtual string BJoinOwnerPing_3 { get; set; }
        public virtual string BJoinOwnerPing_4 { get; set; }
        public virtual string BJoinOwnerPing_5 { get; set; }


        /// <summary>
        /// 建築完成日期
        /// </summary>
        public virtual string BCDate { get; set; }

        /// <summary>
        /// 建築完成日期（年）
        /// </summary>
        public virtual string BCDate_Y { get; set; }

        /// <summary>
        /// 建築完成日期（月）
        /// </summary>
        public virtual string BCDate_M { get; set; }

        /// <summary>
        /// 建築完成日期（日）
        /// </summary>
        public virtual string BCDate_D { get; set; }

        /// <summary>
        /// 屋齡
        /// </summary>
        public virtual string BAge { get; set; }

        /// <summary>
        /// 公寓
        /// </summary>
        public virtual string BTApartment { get; set; }

        /// <summary>
        /// 電梯大樓
        /// </summary>
        public virtual string BTElevator { get; set; }

        /// <summary>
        /// 透天厝
        /// </summary>
        public virtual string BTHouse { get; set; }

        /// <summary>
        /// 別墅
        /// </summary>
        public virtual string BTVilla { get; set; }

        /// <summary>
        /// 華廈
        /// </summary>
        public virtual string BTMansion { get; set; }

        /// <summary>
        /// 農舍
        /// </summary>
        public virtual string BTFarmHouse { get; set; }

        /// <summary>
        /// 平房
        /// </summary>
        public virtual string BTBungalow { get; set; }

        /// <summary>
        /// 廠房
        /// </summary>
        public virtual string BTWorkshop { get; set; }

        /// <summary>
        /// 房
        /// </summary>
        public virtual string BRoom { get; set; }

        /// <summary>
        /// 房
        /// </summary>
        public virtual string BLD { get; set; }

        /// <summary>
        /// 衛
        /// </summary>
        public virtual string BBath { get; set; }

        /// <summary>
        /// 陽台
        /// </summary>
        public virtual string BBalcony { get; set; }

        /// <summary>
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }

        /// <summary>
        /// 格局_1房
        /// </summary>
        public virtual string BPatten1R { get; set; }

        /// <summary>
        /// 格局_套房
        /// </summary>
        public virtual string BPatten1S { get; set; }

        /// <summary>
        /// 格局_2房
        /// </summary>
        public virtual string BPatten2R { get; set; }

        /// <summary>
        /// 格局_3房以上
        /// </summary>
        public virtual string BPatten3Rup { get; set; }

        /// <summary>
        /// 格局_4房以上
        /// </summary>
        public virtual string BPatten4Rup { get; set; }

        /// <summary>
        /// 格局_雅房
        /// </summary>
        public virtual string BBedsit { get; set; }

        /// <summary>
        /// 格局_開放空間
        /// </summary>
        public virtual string BOpenspace { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(無)
        /// </summary>
        public virtual string BMurder_N { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(有)
        /// </summary>
        public virtual string BMurder_Y { get; set; }

        /// <summary>
        /// 於產權持有前出租人確認無_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_No_Check { get; set; }

        /// <summary>
        /// 於產權持有前出租人知道發生_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_Yes_Check { get; set; }

        /// <summary>
        /// 於產權持有前出租人不知曾經發生_本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事
        /// </summary>
        public virtual string BMurder_Yes_Dont { get; set; }

        /// <summary>
        /// 第1層
        /// </summary>
        public virtual string BFloor_1 { get; set; }

        /// <summary>
        /// 第1層面積/平方公尺
        /// </summary>
        public virtual string BArea_1 { get; set; }

        /// <summary>
        /// 第1層坪數
        /// </summary>
        public virtual string BPing_1 { get; set; }

        /// <summary>
        /// 第二層
        /// </summary>
        public virtual string BFloor_2 { get; set; }

        /// <summary>
        /// 第2層面積/平方公尺
        /// </summary>
        public virtual string BArea_2 { get; set; }

        /// <summary>
        /// 第2層坪數
        /// </summary>
        public virtual string BPing_2 { get; set; }

        /// <summary>
        /// 第3層
        /// </summary>
        public virtual string BFloor_3 { get; set; }

        /// <summary>
        /// 第3層面積/平方公尺
        /// </summary>
        public virtual string BArea_3 { get; set; }

        /// <summary>
        /// 第3層坪數
        /// </summary>
        public virtual string BPing_3 { get; set; }
        /// <summary>
        /// 實際面積/平方公尺(換算成坪數對應到BRealPING)
        /// </summary>
        public virtual string BRealArea { get; set; }

        /// <summary>
        /// 實際使用坪數
        /// </summary>
        public virtual string BRealPING { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public virtual string BUse { get; set; }
    }
}
