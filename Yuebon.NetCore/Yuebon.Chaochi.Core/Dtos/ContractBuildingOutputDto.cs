using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約-建物輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractBuildingOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        [MaxLength(100)]
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取建物完整地址
        /// </summary>
        [MaxLength(2000)]
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_縣/市
        /// </summary>
        [MaxLength(20)]
        public string BAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_縣
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_街
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_段
        /// </summary>
        [MaxLength(100)]
        public string BAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_巷
        /// </summary>
        [MaxLength(100)]
        public string BAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_弄
        /// </summary>
        [MaxLength(100)]
        public string BAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_號
        /// </summary>
        [MaxLength(100)]
        public string BAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取建物地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落建號-地段
        /// </summary>
        [MaxLength(100)]
        public string BNo_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落建號-小段
        /// </summary>
        [MaxLength(100)]
        public string BNo_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落建號-地號
        /// </summary>
        [MaxLength(100)]
        public string BNo_3 { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地段 A組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_1_A { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-小段 A組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_2_A { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地號 A組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_3_A { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地段 B組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_1_B { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-小段 B組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_2_B { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地號 B組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_3_B { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地段 C組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_1_C { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-小段 C組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_2_C { get; set; }
        /// <summary>
        /// 設置或獲取建物坐落地號-地號 C組
        /// </summary>
        [MaxLength(100)]
        public string BPNo_3_C { get; set; }
        /// <summary>
        /// 設置或獲取有車位
        /// </summary>
        [MaxLength(10)]
        public string BCar_Y { get; set; }
        /// <summary>
        /// 設置或獲取無車位
        /// </summary>
        [MaxLength(10)]
        public string BCar_N { get; set; }
        /// <summary>
        /// 設置或獲取車位編號
        /// </summary>
        [MaxLength(50)]
        public string BCarNo { get; set; }
        /// <summary>
        /// 設置或獲取房屋面積
        /// </summary>
        [MaxLength(100)]
        public string BArea { get; set; }

        /// <summary>
        /// 設置或獲取房屋實際面積
        /// </summary>
        [MaxLength(100)]
        public string BRealArea { get; set; }
        /// <summary>
        /// 設置或獲取租金
        /// </summary>
        [MaxLength(100)]
        public string BTRent { get; set; }
        /// <summary>
        /// 設置或獲取租金(合約用)
        /// </summary>
        [MaxLength(100)]
        public string BTCRent { get; set; }
        /// <summary>
        /// 設置或獲取租金繳款日
        /// </summary>
        [MaxLength(50)]
        public string BTRent_Day { get; set; }
        /// <summary>
        /// 設置或獲取租賃起日_完整日期
        /// </summary>
        [MaxLength(10)]
        public string BRDStart { get; set; }
        /// <summary>
        /// 設置或獲取租賃起日_年
        /// </summary>
        [MaxLength(10)]
        public string BRDStart_Y { get; set; }
        /// <summary>
        /// 設置或獲取租賃起日_月
        /// </summary>
        [MaxLength(10)]
        public string BRDStart_M { get; set; }
        /// <summary>
        /// 設置或獲取租賃起日_日
        /// </summary>
        [MaxLength(10)]
        public string BRDStart_D { get; set; }
        /// <summary>
        /// 設置或獲取租賃迄日_完整日期
        /// </summary>
        [MaxLength(10)]
        public string BRDTEnd { get; set; }
        /// <summary>
        /// 設置或獲取租賃迄日_年
        /// </summary>
        [MaxLength(10)]
        public string BRDTEnd_Y { get; set; }
        /// <summary>
        /// 設置或獲取租賃迄日_月
        /// </summary>
        [MaxLength(10)]
        public string BRDTEnd_M { get; set; }
        /// <summary>
        /// 設置或獲取租賃迄日_日
        /// </summary>
        [MaxLength(10)]
        public string BRDTEnd_D { get; set; }
        /// <summary>
        /// 設置或獲取車位租金
        /// </summary>
        [MaxLength(50)]
        public string BParkFee { get; set; }
        /// <summary>
        /// 設置或獲取租金付款方式-轉帳
        /// </summary>
        [MaxLength(100)]
        public string BTransfer { get; set; }
        /// <summary>
        /// 設置或獲取租金付款方式-票據
        /// </summary>
        [MaxLength(100)]
        public string BBill { get; set; }
        /// <summary>
        /// 設置或獲取租金付款方式-現金
        /// </summary>
        [MaxLength(100)]
        public string BCash { get; set; }
        /// <summary>
        /// 設置或獲取押金月數
        /// </summary>
        [MaxLength(50)]
        public string BDMon { get; set; }
        /// <summary>
        /// 設置或獲取押金
        /// </summary>
        [MaxLength(100)]
        public string Bdeposit { get; set; }
        /// <summary>
        /// 設置或獲取樓層1
        /// </summary>
        [MaxLength(100)]
        public string BFloor_1 { get; set; }
        /// <summary>
        /// 設置或獲取樓層2
        /// </summary>
        [MaxLength(100)]
        public string BFloor_2 { get; set; }
        /// <summary>
        /// 設置或獲取樓層3
        /// </summary>
        [MaxLength(100)]
        public string BFloor_3 { get; set; }
        /// <summary>
        /// 設置或獲取樓層面積(平方公尺)1
        /// </summary>
        [MaxLength(100)]
        public string BArea_1 { get; set; }
        /// <summary>
        /// 設置或獲取樓層面積(平方公尺)2
        /// </summary>
        [MaxLength(100)]
        public string BArea_2 { get; set; }
        /// <summary>
        /// 設置或獲取樓層面積(平方公尺)3
        /// </summary>
        [MaxLength(100)]
        public string BArea_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string BUse { get; set; }
        /// <summary>
        /// 設置或獲取房/廳/衛:房
        /// </summary>
        [MaxLength(50)]
        public string BRoom { get; set; }
        /// <summary>
        /// 設置或獲取房/廳/衛:廳
        /// </summary>
        [MaxLength(50)]
        public string BLD { get; set; }
        /// <summary>
        /// 設置或獲取房/廳/衛:衛
        /// </summary>
        [MaxLength(50)]
        public string BBath { get; set; }
        /// <summary>
        /// 設置或獲取平面試停車位
        /// </summary>
        [MaxLength(100)]
        public string BRampFlatParking { get; set; }
        /// <summary>
        /// 設置或獲取機械式停車位
        /// </summary>
        [MaxLength(100)]
        public string BMechMechParking { get; set; }
        /// <summary>
        /// 設置或獲取車位第幾層
        /// </summary>
        [MaxLength(50)]
        public string BCarFloor { get; set; }
        /// <summary>
        /// 設置或獲取無漏水
        /// </summary>
        [MaxLength(100)]
        public string B1WaterLeakage_N { get; set; }
        /// <summary>
        /// 設置或獲取有漏水
        /// </summary>
        [MaxLength(100)]
        public string B1WaterLeakage_Y { get; set; }
        /// <summary>
        /// 設置或獲取漏處
        /// </summary>
        [MaxLength(100)]
        public string B1WaterEX { get; set; }
        /// <summary>
        /// 設置或獲取無定期辦理消防安檢
        /// </summary>
        [MaxLength(100)]
        public string BFireSaIn_N { get; set; }
        /// <summary>
        /// 設置或獲取有定期辦理消防安檢
        /// </summary>
        [MaxLength(100)]
        public string BFireSaIn_Y { get; set; }
        /// <summary>
        /// 設置或獲取無公寓大廈規約
        /// </summary>
        [MaxLength(100)]
        public string BConStature_N { get; set; }
        /// <summary>
        /// 設置或獲取有公寓大廈規約
        /// </summary>
        [MaxLength(100)]
        public string BConStature_Y { get; set; }
        /// <summary>
        /// 設置或獲取有管委會
        /// </summary>
        [MaxLength(50)]
        public string BSuper_Y { get; set; }
        /// <summary>
        /// 設置或獲取無管委會
        /// </summary>
        [MaxLength(50)]
        public string BSuper_N { get; set; }
        /// <summary>
        /// 設置或獲取產權持有兇殺非自然死亡:無發生上列事項
        /// </summary>
        [MaxLength(100)]
        public string BMurder_N { get; set; }
        /// <summary>
        /// 設置或獲取產權持有兇殺非自然死亡:有發生上列事項
        /// </summary>
        [MaxLength(100)]
        public string BMurder_Y { get; set; }
        /// <summary>
        /// 設置或獲取產權持有兇殺非自然死亡:出租人知道無上列事項
        /// </summary>
        [MaxLength(100)]
        public string BMurder_No_Check { get; set; }
        /// <summary>
        /// 設置或獲取產權持有兇殺非自然死亡:出租人知道有上列事項
        /// </summary>
        [MaxLength(100)]
        public string BMurder_Yes_Check { get; set; }
        /// <summary>
        /// 設置或獲取產權持有兇殺非自然死亡:出租人不知道是否發生
        /// </summary>
        [MaxLength(100)]
        public string BMurder_Yes_Dont { get; set; }
        /// <summary>
        /// 設置或獲取管理費
        /// </summary>
        [MaxLength(50)]
        public string BMFee { get; set; }        /// <summary>
        /// 設置或獲取無管理費
        /// </summary>
        [MaxLength(100)]
        public string BMFee_N { get; set; }

        /// <summary>
        /// 設置或獲取有管理費
        /// </summary>
        [MaxLength(100)]        public string BMFee_Y { get; set; }        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_A { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_A_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_A_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_B { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_B_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_B_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_C { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_C_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_C_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_D { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_D_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_D_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_E { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_E_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string B1ExtentOfOwner_E_2 { get; set; }        /// <summary>
        /// 設置或獲取建物用途
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }
        private string payment = "";        public string PaymentMethod
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(BTransfer)) {
                    if ("/Yes".Equals(BTransfer.Trim())) {
                        payment = "轉帳繳付";
                    }
                }
                if (!string.IsNullOrWhiteSpace(BBill)) {
                    if ("/Yes".Equals(BBill.Trim())) {
                        payment += payment.Length > 0 ? ",票據繳付" : "票據繳付";
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCash)) {
                    if ("/Yes".Equals(BCash.Trim())) {
                        payment += payment.Length > 0 ? ",現金繳付" : "現金繳付";
                    }
                }

                return payment;
            }
            set { }
        }
    }
}
