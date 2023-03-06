using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class A000001InputDto
    {
        #region 出租人

        public virtual string LNName { get; set; }
        public virtual string LNID { get; set; }
        public virtual string LNGender1 { get; set; }
        public virtual string LNGender2 { get; set; }
        public virtual string LNBirthday { get; set; }
        public virtual string LNTel_1 { get; set; }
        public virtual string LNTel_2 { get; set; }
        public virtual string LNCell { get; set; }
        public virtual string LNMail { get; set; }
        public virtual string LNLine { get; set; }
        public virtual string LNAdd { get; set; }
        public virtual string LNAddC { get; set; }
        /// <summary>
        /// 戶籍地址_縣/市
        /// </summary>
        public virtual string LNAdd_1 { get; set; }

        /// <summary>
        /// 戶籍地址_縣
        /// </summary>
        public virtual string LNAdd_1_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string LNAdd_1_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉/鎮/市/區
        /// </summary>
        public virtual string LNAdd_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉
        /// </summary>
        public virtual string LNAdd_2_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string LNAdd_2_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鎮
        /// </summary>
        public virtual string LNAdd_2_3 { get; set; }

        /// <summary>
        /// 戶籍地址_區
        /// </summary>
        public virtual string LNAdd_2_4 { get; set; }

        /// <summary>
        /// 戶籍地址_街/路
        /// </summary>
        public virtual string LNAdd_3 { get; set; }

        /// <summary>
        /// 戶籍地址_街
        /// </summary>
        public virtual string LNAdd_3_1 { get; set; }

        /// <summary>
        /// 戶籍地址_路
        /// </summary>
        public virtual string LNAdd_3_2 { get; set; }

        /// <summary>
        /// 戶籍地址_段
        /// </summary>
        public virtual string LNAdd_4 { get; set; }

        /// <summary>
        /// 戶籍地址_巷
        /// </summary>
        public virtual string LNAdd_5 { get; set; }

        /// <summary>
        /// 戶籍地址_弄
        /// </summary>
        public virtual string LNAdd_6 { get; set; }

        /// <summary>
        /// 戶籍地址_號
        /// </summary>
        public virtual string LNAdd_7 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string LNAdd_8 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string LNAdd_9 { get; set; }
        /// <summary>
        /// 地址_同戶籍地址
        /// </summary>
        public virtual string LNAddSame { get; set; }

        /// <summary>
        /// 通訊地址_縣/市
        /// </summary>
        public virtual string LNAddC_1 { get; set; }

        /// <summary>
        /// 通訊地址_縣
        /// </summary>
        public virtual string LNAddC_1_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string LNAddC_1_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉/鎮/市/區
        /// </summary>
        public virtual string LNAddC_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉
        /// </summary>
        public virtual string LNAddC_2_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string LNAddC_2_2 { get; set; }

        /// <summary>
        /// 通訊地址_鎮
        /// </summary>
        public virtual string LNAddC_2_3 { get; set; }

        /// <summary>
        /// 通訊地址_區
        /// </summary>
        public virtual string LNAddC_2_4 { get; set; }

        /// <summary>
        /// 通訊地址_街/路
        /// </summary>
        public virtual string LNAddC_3 { get; set; }

        /// <summary>
        /// 通訊地址_街
        /// </summary>
        public virtual string LNAddC_3_1 { get; set; }

        /// <summary>
        /// 通訊地址_路
        /// </summary>
        public virtual string LNAddC_3_2 { get; set; }

        /// <summary>
        /// 通訊地址_段
        /// </summary>
        public virtual string LNAddC_4 { get; set; }

        /// <summary>
        /// 通訊地址_巷
        /// </summary>
        public virtual string LNAddC_5 { get; set; }

        /// <summary>
        /// 通訊地址_弄
        /// </summary>
        public virtual string LNAddC_6 { get; set; }

        /// <summary>
        /// 通訊地址_號
        /// </summary>
        public virtual string LNAddC_7 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string LNAddC_8 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string LNAddC_9 { get; set; }


        /// <summary>
        /// 公司
        /// </summary>
        public string LNCompany { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string LNJobtitle { get; set; }

        #endregion 出租人

        #region 代理人

        public virtual string LNAGName_A { get; set; }
        public virtual string LNAGID_A { get; set; }
        public virtual string LNAGCell_A { get; set; }
        public virtual string LNAGAdd_A { get; set; }
        public virtual string LNAGAdd_1_A { get; set; }
        public virtual string LNAGAdd_1_1_A { get; set; }
        public virtual string LNAGAdd_1_2_A { get; set; }
        public virtual string LNAGAdd_2_A { get; set; }
        public virtual string LNAGAdd_2_1_A { get; set; }
        public virtual string LNAGAdd_2_2_A { get; set; }
        public virtual string LNAGAdd_2_3_A { get; set; }
        public virtual string LNAGAdd_2_4_A { get; set; }
        public virtual string LNAGAdd_3_A { get; set; }
        public virtual string LNAGAdd_3_1_A { get; set; }
        public virtual string LNAGAdd_3_2_A { get; set; }
        public virtual string LNAGAdd_4_A { get; set; }
        public virtual string LNAGAdd_5_A { get; set; }
        public virtual string LNAGAdd_6_A { get; set; }
        public virtual string LNAGAdd_7_A { get; set; }
        public virtual string LNAGAdd_8_A { get; set; }
        public virtual string LNAGAdd_9_A { get; set; }
        public virtual string LNAGAddSame { get; set; }
        public virtual string LNAGAddC_A { get; set; }
        public virtual string LNAGAddC_1_A { get; set; }
        public virtual string LNAGAddC_1_1_A { get; set; }
        public virtual string LNAGAddC_1_2_A { get; set; }
        public virtual string LNAGAddC_2_A { get; set; }
        public virtual string LNAGAddC_2_1_A { get; set; }
        public virtual string LNAGAddC_2_2_A { get; set; }
        public virtual string LNAGAddC_2_3_A { get; set; }
        public virtual string LNAGAddC_2_4_A { get; set; }
        public virtual string LNAGAddC_3_A { get; set; }
        public virtual string LNAGAddC_3_1_A { get; set; }
        public virtual string LNAGAddC_3_2_A { get; set; }
        public virtual string LNAGAddC_4_A { get; set; }
        public virtual string LNAGAddC_5_A { get; set; }
        public virtual string LNAGAddC_6_A { get; set; }
        public virtual string LNAGAddC_7_A { get; set; }
        public virtual string LNAGAddC_8_A { get; set; }
        public virtual string LNAGAddC_9_A { get; set; }


        #endregion

        #region 建物
        public virtual string BAdd { get; set; }
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
        public virtual string BTHouse { get; set; }
        public virtual string BTApartment { get; set; }

        /// <summary>
        /// 華廈
        /// </summary>
        public virtual string BTMansion { get; set; }

        /// <summary>
        /// 電梯大樓
        /// </summary>
        public virtual string BTElevator { get; set; }

        /// <summary>
        /// 農舍
        /// </summary>
        public virtual string BTFarmHouse { get; set; }

        /// <summary>
        /// 廠房
        /// </summary>
        public virtual string BTWorkshop { get; set; }

        /// <summary>
        /// 建築完成日期
        /// </summary>
        public virtual string BCDate { get; set; }

        /// <summary>
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }
        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }

        /// <summary>
        /// 第1層
        /// </summary>
        public virtual string BFloor_1 { get; set; }

        /// <summary>
        /// 第1層面積/平方公尺
        /// </summary>
        public virtual string BArea_1 { get; set; }

        /// <summary>
        /// 第二層
        /// </summary>
        public virtual string BFloor_2 { get; set; }

        /// <summary>
        /// 第2層面積/平方公尺
        /// </summary>
        public virtual string BArea_2 { get; set; }

        /// <summary>
        /// 第3層
        /// </summary>
        public virtual string BFloor_3 { get; set; }

        /// <summary>
        /// 第3層面積/平方公尺
        /// </summary>
        public virtual string BArea_3 { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public virtual string BUse { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }
        public virtual string BMFee_Y { get; set; }
        public virtual string BMFee_N { get; set; }

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
        /// 有無車位(有)
        /// </summary>
        public virtual string BCar_Y { get; set; }

        /// <summary>
        /// 有無車位(無)
        /// </summary>
        public virtual string BCar_N { get; set; }

        /// <summary>
        /// 坡道平面
        /// </summary>
        public virtual string BRampFlatParking { get; set; }

        /// <summary>
        /// 坡道機械
        /// </summary>
        public virtual string BRampMechParking { get; set; }

        /// <summary>
        /// 機械平面
        /// </summary>
        public virtual string BMechFlatParking { get; set; }

        /// <summary>
        /// 機械機械
        /// </summary>
        public virtual string BMechMechParking { get; set; }
        public virtual string BCarFloor { get; set; }
        public virtual string BCarNo { get; set; }

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

        #region Building1

        /// <summary>
        /// 無漏水情形
        /// </summary>
        public virtual string B1WaterLeakage_N { get; set; }

        /// <summary>
        /// 有漏水情形
        /// </summary>
        public virtual string B1WaterLeakage_Y { get; set; }

        /// <summary>
        /// 滲漏水處
        /// </summary>
        public virtual string B1WaterEX { get; set; }

        #endregion

        #endregion

        #region 匯款資料

        public virtual bool? LCanModify { get; set; }
        public virtual string LAName { get; set; }
        public virtual string LBankName { get; set; }
        public virtual string LBankNo { get; set; }
        public virtual string LBranchName { get; set; }
        public virtual string LBranchNo { get; set; }
        public virtual string LANo { get; set; }

        #endregion

        #region BZ欄位

        /// <summary>
        /// BZ001
        /// </summary>
        public virtual string BZ001 { get; set; }

        /// <summary>
        /// BZ002
        /// </summary>
        public virtual string BZ002 { get; set; }

        /// <summary>
        /// BZ003
        /// </summary>
        public virtual string BZ003 { get; set; }

        /// <summary>
        /// BZ004
        /// </summary>
        public virtual string BZ004 { get; set; }

        /// <summary>
        /// BZ005
        /// </summary>
        public virtual string BZ005 { get; set; }

        /// <summary>
        /// BZ006
        /// </summary>
        public virtual string BZ006 { get; set; }

        /// <summary>
        /// BZ007
        /// </summary>
        public virtual string BZ007 { get; set; }

        /// <summary>
        /// BZ008
        /// </summary>
        public virtual string BZ008 { get; set; }

        /// <summary>
        /// BZ009
        /// </summary>
        public virtual string BZ009 { get; set; }

        /// <summary>
        /// BZ010
        /// </summary>
        public virtual string BZ010 { get; set; }

        /// <summary>
        /// BZ011
        /// </summary>
        public virtual string BZ011 { get; set; }

        /// <summary>
        /// BZ012
        /// </summary>
        public virtual string BZ012 { get; set; }

        /// <summary>
        /// BZ013
        /// </summary>
        public virtual string BZ013 { get; set; }

        /// <summary>
        /// BZ014
        /// </summary>
        public virtual string BZ014 { get; set; }

        /// <summary>
        /// BZ015
        /// </summary>
        public virtual string BZ015 { get; set; }

        /// <summary>
        /// BZ016
        /// </summary>
        public virtual string BZ016 { get; set; }

        /// <summary>
        /// BZ017
        /// </summary>
        public virtual string BZ017 { get; set; }

        /// <summary>
        /// BZ018
        /// </summary>
        public virtual string BZ018 { get; set; }

        /// <summary>
        /// BZ019
        /// </summary>
        public virtual string BZ019 { get; set; }

        /// <summary>
        /// BZ020
        /// </summary>
        public virtual string BZ020 { get; set; }

        /// <summary>
        /// BZ021
        /// </summary>
        public virtual string BZ021 { get; set; }

        /// <summary>
        /// BZ022
        /// </summary>
        public virtual string BZ022 { get; set; }

        /// <summary>
        /// BZ023
        /// </summary>
        public virtual string BZ023 { get; set; }

        /// <summary>
        /// BZ024
        /// </summary>
        public virtual string BZ024 { get; set; }

        /// <summary>
        /// BZ025
        /// </summary>
        public virtual string BZ025 { get; set; }

        /// <summary>
        /// BZ026
        /// </summary>
        public virtual string BZ026 { get; set; }

        /// <summary>
        /// BZ027
        /// </summary>
        public virtual string BZ027 { get; set; }

        /// <summary>
        /// BZ028
        /// </summary>
        public virtual string BZ028 { get; set; }

        /// <summary>
        /// BZ029
        /// </summary>
        public virtual string BZ029 { get; set; }

        /// <summary>
        /// BZ030
        /// </summary>
        public virtual string BZ030 { get; set; }

        /// <summary>
        /// BZ031
        /// </summary>
        public virtual string BZ031 { get; set; }

        /// <summary>
        /// BZ032
        /// </summary>
        public virtual string BZ032 { get; set; }

        /// <summary>
        /// BZ033
        /// </summary>
        public virtual string BZ033 { get; set; }

        /// <summary>
        /// BZ034
        /// </summary>
        public virtual string BZ034 { get; set; }

        /// <summary>
        /// BZ035
        /// </summary>
        public virtual string BZ035 { get; set; }

        /// <summary>
        /// BZ036
        /// </summary>
        public virtual string BZ036 { get; set; }

        /// <summary>
        /// BZ037
        /// </summary>
        public virtual string BZ037 { get; set; }

        /// <summary>
        /// BZ038
        /// </summary>
        public virtual string BZ038 { get; set; }

        /// <summary>
        /// BZ039
        /// </summary>
        public virtual string BZ039 { get; set; }

        /// <summary>
        /// BZ040
        /// </summary>
        public virtual string BZ040 { get; set; }

        /// <summary>
        /// BZ041
        /// </summary>
        public virtual string BZ041 { get; set; }

        /// <summary>
        /// BZ042
        /// </summary>
        public virtual string BZ042 { get; set; }

        /// <summary>
        /// BZ043
        /// </summary>
        public virtual string BZ043 { get; set; }

        /// <summary>
        /// BZ044
        /// </summary>
        public virtual string BZ044 { get; set; }

        /// <summary>
        /// BZ045
        /// </summary>
        public virtual string BZ045 { get; set; }

        /// <summary>
        /// BZ046
        /// </summary>
        public virtual string BZ046 { get; set; }

        /// <summary>
        /// BZ047
        /// </summary>
        public virtual string BZ047 { get; set; }

        /// <summary>
        /// BZ048
        /// </summary>
        public virtual string BZ048 { get; set; }

        /// <summary>
        /// BZ049
        /// </summary>
        public virtual string BZ049 { get; set; }

        /// <summary>
        /// BZ050
        /// </summary>
        public virtual string BZ050 { get; set; }

        /// <summary>
        /// BZ051
        /// </summary>
        public virtual string BZ051 { get; set; }

        /// <summary>
        /// BZ052
        /// </summary>
        public virtual string BZ052 { get; set; }

        /// <summary>
        /// BZ053
        /// </summary>
        public virtual string BZ053 { get; set; }

        /// <summary>
        /// BZ054
        /// </summary>
        public virtual string BZ054 { get; set; }

        /// <summary>
        /// BZ055
        /// </summary>
        public virtual string BZ055 { get; set; }

        /// <summary>
        /// BZ056
        /// </summary>
        public virtual string BZ056 { get; set; }

        /// <summary>
        /// BZ057
        /// </summary>
        public virtual string BZ057 { get; set; }

        /// <summary>
        /// BZ058
        /// </summary>
        public virtual string BZ058 { get; set; }

        /// <summary>
        /// BZ059
        /// </summary>
        public virtual string BZ059 { get; set; }

        /// <summary>
        /// BZ060
        /// </summary>
        public virtual string BZ060 { get; set; }

        /// <summary>
        /// BZ061
        /// </summary>
        public virtual string BZ061 { get; set; }

        /// <summary>
        /// BZ062
        /// </summary>
        public virtual string BZ062 { get; set; }

        /// <summary>
        /// BZ063
        /// </summary>
        public virtual string BZ063 { get; set; }

        /// <summary>
        /// BZ064
        /// </summary>
        public virtual string BZ064 { get; set; }

        /// <summary>
        /// BZ065
        /// </summary>
        public virtual string BZ065 { get; set; }

        /// <summary>
        /// BZ066
        /// </summary>
        public virtual string BZ066 { get; set; }

        /// <summary>
        /// BZ067
        /// </summary>
        public virtual string BZ067 { get; set; }

        /// <summary>
        /// BZ068
        /// </summary>
        public virtual string BZ068 { get; set; }

        /// <summary>
        /// BZ069
        /// </summary>
        public virtual string BZ069 { get; set; }

        /// <summary>
        /// BZ070
        /// </summary>
        public virtual string BZ070 { get; set; }

        /// <summary>
        /// BZ071
        /// </summary>
        public virtual string BZ071 { get; set; }

        /// <summary>
        /// BZ072
        /// </summary>
        public virtual string BZ072 { get; set; }

        /// <summary>
        /// BZ073
        /// </summary>
        public virtual string BZ073 { get; set; }

        /// <summary>
        /// BZ074
        /// </summary>
        public virtual string BZ074 { get; set; }

        /// <summary>
        /// BZ075
        /// </summary>
        public virtual string BZ075 { get; set; }

        /// <summary>
        /// BZ076
        /// </summary>
        public virtual string BZ076 { get; set; }

        /// <summary>
        /// BZ077
        /// </summary>
        public virtual string BZ077 { get; set; }

        /// <summary>
        /// BZ078
        /// </summary>
        public virtual string BZ078 { get; set; }

        /// <summary>
        /// BZ079
        /// </summary>
        public virtual string BZ079 { get; set; }

        /// <summary>
        /// BZ080
        /// </summary>
        public virtual string BZ080 { get; set; }

        /// <summary>
        /// BZ081
        /// </summary>
        public virtual string BZ081 { get; set; }

        /// <summary>
        /// BZ082
        /// </summary>
        public virtual string BZ082 { get; set; }

        /// <summary>
        /// BZ083
        /// </summary>
        public virtual string BZ083 { get; set; }

        /// <summary>
        /// BZ084
        /// </summary>
        public virtual string BZ084 { get; set; }

        /// <summary>
        /// BZ085
        /// </summary>
        public virtual string BZ085 { get; set; }

        /// <summary>
        /// BZ086
        /// </summary>
        public virtual string BZ086 { get; set; }

        /// <summary>
        /// BZ087
        /// </summary>
        public virtual string BZ087 { get; set; }

        /// <summary>
        /// BZ088
        /// </summary>
        public virtual string BZ088 { get; set; }

        /// <summary>
        /// BZ089
        /// </summary>
        public virtual string BZ089 { get; set; }

        /// <summary>
        /// BZ090
        /// </summary>
        public virtual string BZ090 { get; set; }

        /// <summary>
        /// BZ091
        /// </summary>
        public virtual string BZ091 { get; set; }

        /// <summary>
        /// BZ092
        /// </summary>
        public virtual string BZ092 { get; set; }

        /// <summary>
        /// BZ093
        /// </summary>
        public virtual string BZ093 { get; set; }

        /// <summary>
        /// BZ094
        /// </summary>
        public virtual string BZ094 { get; set; }

        /// <summary>
        /// BZ095
        /// </summary>
        public virtual string BZ095 { get; set; }

        /// <summary>
        /// BZ096
        /// </summary>
        public virtual string BZ096 { get; set; }

        /// <summary>
        /// BZ097
        /// </summary>
        public virtual string BZ097 { get; set; }

        /// <summary>
        /// BZ098
        /// </summary>
        public virtual string BZ098 { get; set; }

        /// <summary>
        /// BZ099
        /// </summary>
        public virtual string BZ099 { get; set; }

        /// <summary>
        /// BZ100
        /// </summary>
        public virtual string BZ100 { get; set; }

        /// <summary>
        /// BZ101
        /// </summary>
        public virtual string BZ101 { get; set; }

        /// <summary>
        /// BZ102
        /// </summary>
        public virtual string BZ102 { get; set; }

        /// <summary>
        /// BZ103
        /// </summary>
        public virtual string BZ103 { get; set; }



        #endregion BZ欄位

        #region 簽名

        public virtual string SignatureBase64_1 { get; set; }

        #endregion 簽名

        /// <summary>
        /// 是否為產生PDF按鈕
        /// </summary>
        public virtual string IsGenPDF { get; set; }
    }
}
