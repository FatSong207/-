
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 房屋物件，數據實體對象
    /// </summary>
    [Table("Chaochi_Building")]
    [Serializable]
    public class Building : BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public Building()
        {

        }

        #region Property Members
        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建的人(代表當前業務)
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 創建的人所屬組織ID
        /// </summary>
        public virtual string CreatorUserOrgId { get; set; }

        /// <summary>
        /// 創建的人所屬部門ID
        /// </summary>
        public virtual string CreatorUserDeptId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        ///// <summary>
        ///// 刪除時間
        ///// </summary>
        //public virtual DateTime? DeleteTime { get; set; }

        ///// <summary>
        ///// 刪除用戶
        ///// </summary>
        //[MaxLength(50)]
        //public virtual string DeleteUserId { get; set; }

        //**********下方為正是欄位  上方為測試欄位 暫時不移除***************

        //----------------
        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 建物屬性(住宅、店面、辦公室、停車位、商業登記、廣告牆面)
        /// </summary>
        public virtual string BPropoty { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣市)
        /// </summary>
        public virtual string BAdd_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣)
        /// </summary>
        public virtual string BAdd_1_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_1_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string BAdd_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉)
        /// </summary>
        public virtual string BAdd_2_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_2_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鎮)
        /// </summary>
        public virtual string BAdd_2_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(區)
        /// </summary>
        public virtual string BAdd_2_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街路)
        /// </summary>
        public virtual string BAdd_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街)
        /// </summary>
        public virtual string BAdd_3_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(路)
        /// </summary>
        public virtual string BAdd_3_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址4(段)
        /// </summary>
        public virtual string BAdd_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址5(巷)
        /// </summary>
        public virtual string BAdd_5 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址6(弄)
        /// </summary>
        public virtual string BAdd_6 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址7(號)
        /// </summary>
        public virtual string BAdd_7 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址8(樓)
        /// </summary>
        public virtual string BAdd_8 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址9(之)
        /// </summary>
        public virtual string BAdd_9 { get; set; }

        /// <summary>
        /// 屬於哪一個房東
        /// </summary>
        public virtual string BelongLID { get; set; }

        /// <summary>
        /// 屬於哪一個二房東
        /// </summary>
        public virtual string BelongSLID { get; set; }
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
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTRent { get; set; }

        /// <summary>
        /// 代租租金2包租代管
        /// </summary>
        public virtual string BR80C { get; set; }

        /// <summary>
        /// 代租租金3年/月
        /// </summary>
        public virtual string BR80V { get; set; }

        /// <summary>
        /// 代租租金4代租代管
        /// </summary>
        public virtual string BR90C { get; set; }

        /// <summary>
        /// 代租租金5年/月
        /// </summary>
        public virtual string BR90V { get; set; }

        /// <summary>
        /// 押金1
        /// </summary>
        public virtual string BDepositC { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }

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
        /// 設置或獲取分租物件單編號
        /// </summary>
        public string MOID { get; set; }

        #region
        public virtual string BAuthStart { get; set; }
        public virtual string BAuthStart_Y { get; set; }
        public virtual string BAuthStart_M { get; set; }
        public virtual string BAuthStart_D { get; set; }
        public virtual string BAuthEnd { get; set; }
        public virtual string BAuthEnd_Y { get; set; }
        public virtual string BAuthEnd_M { get; set; }
        public virtual string BAuthEnd_D { get; set; }
        public virtual string BCommunity { get; set; }
        public virtual string BpingV { get; set; }
        public virtual string BpingActualV { get; set; }
        public virtual string BpingActual { get; set; }
        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }
        public virtual string BKitchen { get; set; }
        public virtual string BBalcony { get; set; }
        public virtual string BTApartment { get; set; }
        public virtual string BTElevatorB { get; set; }
        public virtual string BTHouse { get; set; }
        public virtual string Belevator_Y { get; set; }
        public virtual string Belevator_N { get; set; }
        public virtual string BSuper_Y { get; set; }
        public virtual string BSuper_N { get; set; }
        public virtual string Bhousehold { get; set; }
        public virtual string Bddirection { get; set; }
        public virtual string BCarNo { get; set; }
        public virtual string BAboveG { get; set; }
        public virtual string BUnderG { get; set; }
        public virtual string BRampParking { get; set; }
        public virtual string BLiftParking { get; set; }
        public virtual string BCarFloor { get; set; }
        public virtual string BPParking { get; set; }
        public virtual string BMParking { get; set; }
        public virtual string BMFeeV { get; set; }
        public virtual string BCleaningFeeV { get; set; }
        public virtual string BCableTVFeeV { get; set; }
        public virtual string BInterNetFeeV { get; set; }
        public virtual string BWaterBillV { get; set; }
        public virtual string BElectricityBillV { get; set; }
        public virtual string BGasFeeV { get; set; }
        public virtual string BParkFee { get; set; }
        public virtual string BParkCFee { get; set; }
        public virtual string BMFee { get; set; }
        public virtual string BMiniLeaseP { get; set; }
        public virtual string BCook_Y { get; set; }
        public virtual string BCook_N { get; set; }
        public virtual string BPet_Y { get; set; }
        public virtual string BPet_N { get; set; }
        public virtual string BGodT_Y { get; set; }
        public virtual string BGodT_N { get; set; }
        public virtual string BOpen { get; set; }
        public virtual string BKeyFM { get; set; }
        public virtual string BKeyFR { get; set; }
        public virtual string BkeyCount { get; set; }


        #endregion

        #region B030301
        public virtual string HSI { get; set; }
        public virtual string HSIRFee { get; set; }
        public virtual string HSIAFee { get; set; }
        public virtual string SFee { get; set; } 
        public virtual string SFRFee { get; set; }
        public virtual string SFAFee { get; set; }
        public virtual string BRFFee { get; set; }
        public virtual string BRFRFee { get; set; }
        public virtual string BRFAFee { get; set; }
        #endregion

        #region 台北公會三期-社會住宅轉租契約書

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

        /// <summary>
        /// 建物坐落地號(地段)C
        /// </summary>
        public virtual string BPNo_1_C { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)C
        /// </summary>
        public virtual string BPNo_2_C { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)C
        /// </summary>
        public virtual string BPNo_3_C { get; set; }

        /// <summary>
        /// 有無車位(有)
        /// </summary>
        public virtual string BCar_Y { get; set; }

        /// <summary>
        /// 有無車位(無)
        /// </summary>
        public virtual string BCar_N { get; set; }

        /// <summary>
        /// 押金月數
        /// </summary>
        public virtual string BDMon { get; set; }
        #endregion

        #region B030201 屋況及租屋安全檢核表

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(無)
        /// </summary>
        public virtual string BConStature_N { get; set; }

        /// <summary>
        /// 公寓大廈規約或其他住戶應遵循事項(有)
        /// </summary>
        public virtual string BConStature_Y { get; set; }

        /// <summary>
        /// 火警警報器
        /// </summary>
        public virtual string BFireAL_Y { get; set; }

        /// <summary>
        /// 滅火器
        /// </summary>
        public virtual string BFireEX_Y { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(無)
        /// </summary>
        public virtual string BFireSaIn_N { get; set; }

        /// <summary>
        /// 定期辦理消防安全檢查(有)
        /// </summary>
        public virtual string BFireSaIn_Y { get; set; }

        /// <summary>
        /// 機械平面
        /// </summary>
        public virtual string BMechFlatParking { get; set; }

        /// <summary>
        /// 機械機械
        /// </summary>
        public virtual string BMechMechParking { get; set; }

        /// <summary>
        /// 管理費(無)
        /// </summary>
        public virtual string BMFee_N { get; set; }

        /// <summary>
        /// 管理費(有)
        /// </summary>
        public virtual string BMFee_Y { get; set; }

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
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(無)
        /// </summary>
        public virtual string BMurder_N { get; set; }

        /// <summary>
        /// 曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事(有)
        /// </summary>
        public virtual string BMurder_Y { get; set; }

        /// <summary>
        /// 坡道平面
        /// </summary>
        public virtual string BRampFlatParking { get; set; }

        /// <summary>
        /// 坡道機械
        /// </summary>
        public virtual string BRampMechParking { get; set; }

        /// <summary>
        /// 獨立型偵煙器
        /// </summary>
        public virtual string BSmokeDE_Y { get; set; }

        /// <summary>
        /// 電梯大樓
        /// </summary>
        public virtual string BTElevator { get; set; }

        /// <summary>
        /// 農舍
        /// </summary>
        public virtual string BTFarmHouse { get; set; }

        /// <summary>
        /// 華廈
        /// </summary>
        public virtual string BTMansion { get; set; }

        /// <summary>
        /// 別墅
        /// </summary>
        public virtual string BTVilla { get; set; }

        #endregion B030201 屋況及租屋安全檢核表

        #region B031101


        /// <summary>
        /// 租金補貼新台幣
        /// </summary>
        public virtual string BTRentGrant { get; set; }

        #endregion B031101

        #region C01030101 社會住宅包租帶罐第三期計畫_出租人出租住宅申請書

        /// <summary>
        /// 屋齡
        /// </summary>
        public virtual string BAge { get; set; }

        /// <summary>
        /// 身障及長者換居專案
        /// </summary>
        public virtual string BChangeResidence { get; set; }

        /// <summary>
        /// 包租代管及租補整合專案
        /// </summary>
        public virtual string BChangeTrack { get; set; }

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
        /// 格局_雅房
        /// </summary>
        public virtual string BBedsit { get; set; }

        /// <summary>
        /// 格局_開放空間
        /// </summary>
        public virtual string BOpenspace { get; set; }

        /// <summary>
        /// 實際使用坪數
        /// </summary>
        public virtual string BRealPING { get; set; }

        /// <summary>
        /// 房屋類型_平房
        /// </summary>
        public virtual string BTBungalow { get; set; }

        #endregion C01030101 C01030101 社會住宅包租帶罐第三期計畫_出租人出租住宅申請書

        #region C01030701 社會住宅包出代管第三期計畫住宅出租修繕費申請書

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

        #endregion C01030701 社會住宅包出代管第三期計畫住宅出租修繕費申請書

        #region 建物現況

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

        #endregion 建物現況

        #region 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 廠房
        /// </summary>
        public virtual string BTWorkshop { get; set; }

        /// <summary>
        /// 格局_4房以上
        /// </summary>
        public virtual string BPatten4Rup { get; set; }

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
        /// 用途
        /// </summary>
        public virtual string BUse { get; set; }

        /// <summary>
        /// 合約租金
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 合約租金_每坪金額
        /// </summary>
        public virtual string BTCRentPing { get; set; }

        /// <summary>
        /// 每坪金額
        /// </summary>
        public virtual string BMFeePing { get; set; }
        #endregion 8/17 MM 欄位修改0817.docx

        #region 廣告功能新增

        /// <summary>
        /// 建物狀態
        /// </summary>
        public virtual string BState { get; set; }

        #endregion 廣告功能新增

        #region 建物標示新增

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

        #endregion

        #endregion
    }
}