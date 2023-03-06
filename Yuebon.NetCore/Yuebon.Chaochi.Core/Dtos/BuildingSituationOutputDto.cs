using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class BuildingSituationOutputDto
    {

        /// <summary>
        /// 主鍵
        /// </summary>
        public virtual string Id { get; set; }

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
        /// 實際坪數(在此DTO上是為了計算BMFeePing)
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 建物屬性(住宅、店面、辦公室、停車位、商業登記、廣告牆面)
        /// </summary>
        public virtual string BPropoty { get; set; }

        /// <summary>
        /// 承租起
        /// </summary>
        public virtual string BRDStart { get; set; }

        /// <summary>
        /// 承租起_D
        /// </summary>
        public virtual string BRDStart_D { get; set; }

        /// <summary>
        /// 承租起_M
        /// </summary>
        public virtual string BRDStart_M { get; set; }

        /// <summary>
        /// 承租起_Y
        /// </summary>
        public virtual string BRDStart_Y { get; set; }

        /// <summary>
        /// 承租迄
        /// </summary>
        public virtual string BRDTEnd { get; set; }

        /// <summary>
        /// 承租迄_D
        /// </summary>
        public virtual string BRDTEnd_D { get; set; }

        /// <summary>
        /// 承租迄_M
        /// </summary>
        public virtual string BRDTEnd_M { get; set; }

        /// <summary>
        /// 承租迄_Y
        /// </summary>
        public virtual string BRDTEnd_Y { get; set; }

        /// <summary>
        /// 包租包管
        /// </summary>
        public virtual string BCharter { get; set; }

        /// <summary>
        /// 代租代管
        /// </summary>
        public virtual string BChangeRent { get; set; }

        /// <summary>
        /// 服務事業轉租
        /// </summary>
        public virtual string BSecondLandlord { get; set; }

        /// <summary>
        /// 服務事業協助出租
        /// </summary>
        public virtual string BManagement { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(無)
        /// </summary>
        public virtual string BConStature_N { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(有)
        /// </summary>
        public virtual string BConStature_Y { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(無)
        /// </summary>
        public virtual string BFireSaIn_N { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(有)
        /// </summary>
        public virtual string BFireSaIn_Y { get; set; }

        /// <summary>
        /// 管理費(無)
        /// </summary>
        public virtual string BMFee_N { get; set; }

        /// <summary>
        /// 管理費(有)
        /// </summary>
        public virtual string BMFee_Y { get; set; }

        /// <summary>
        /// 管理費元/月
        /// </summary>
        public virtual string BMFee { get; set; }

        /// <summary>
        /// 有無車位(有)
        /// </summary>
        public virtual string BCar_Y { get; set; }

        /// <summary>
        /// 有無車位(無)
        /// </summary>
        public virtual string BCar_N { get; set; }

        /// <summary>
        /// 車位租金
        /// </summary>
        public virtual string BParkFee { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTRent { get; set; }

        /// <summary>
        /// 租金支付_轉帳
        /// </summary>
        public virtual string BTransfer { get; set; }

        /// <summary>
        /// 租金支付_票據
        /// </summary>
        public virtual string BBill { get; set; }

        /// <summary>
        /// 租金支付_現金
        /// </summary>
        public virtual string BCash { get; set; }

        /// <summary>
        /// 租金支付_信用卡
        /// </summary>
        public virtual string BCreditCard { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }

        /// <summary>
        /// 押金月數
        /// </summary>
        public virtual string BDMon { get; set; }

        /// <summary>
        /// 炊煮_可
        /// </summary>
        public virtual string BCook_Y { get; set; }

        /// <summary>
        /// 炊煮_否
        /// </summary>
        public virtual string BCook_N { get; set; }

        /// <summary>
        /// 寵物_可
        /// </summary>
        public virtual string BPet_Y { get; set; }

        /// <summary>
        /// 寵物_否
        /// </summary>
        public virtual string BPet_N { get; set; }

        /// <summary>
        /// 神明桌_可
        /// </summary>
        public virtual string BGodT_Y { get; set; }

        /// <summary>
        /// 神明桌_否
        /// </summary>
        public virtual string BGodT_N { get; set; }

        /// <summary>
        /// 電梯_有
        /// </summary>
        public virtual string Belevator_Y { get; set; }

        /// <summary>
        /// 電梯_無
        /// </summary>
        public virtual string Belevator_N { get; set; }
        
        /// <summary>
        /// 租賃管理相關事務_有
        /// </summary>
        public virtual string BSuper_Y { get; set; }

        /// <summary>
        /// 租賃管理相關事務_無
        /// </summary>
        public virtual string BSuper_N { get; set; }

        /// <summary>
        /// 合約租金
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 合約租金_每坪金額
        /// </summary>
        public virtual string BTCRentPing { get; set; }

        /// <summary>
        /// 管理費_每坪金額
        /// </summary>
        public virtual string BMFeePing { get; set; }

        #region 廣告功能新增

        /// <summary>
        /// 建物狀態
        /// </summary>
        public virtual string BState { get; set; }

        #endregion 廣告功能新增
    }
}
