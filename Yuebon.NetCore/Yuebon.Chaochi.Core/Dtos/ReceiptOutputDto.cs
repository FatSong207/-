using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class ReceiptOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取領收據編號
        /// </summary>
        [MaxLength(100)]
        public string ReceiptId { get; set; }
        /// <summary>
        /// 設置或獲取領收據類別
        /// </summary>
        [MaxLength(100)]
        public string RCate { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期
        /// </summary>
        [MaxLength(10)]
        public string RDate { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_年
        /// </summary>
        [MaxLength(10)]
        public string RDate_Y { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_月
        /// </summary>
        [MaxLength(10)]
        public string RDate_M { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_日
        /// </summary>
        [MaxLength(10)]
        public string RDate_D { get; set; }
        /// <summary>
        /// 設置或獲取收款方
        /// </summary>
        [MaxLength(100)]
        public string RPayee { get; set; }
        /// <summary>
        /// 設置或獲取收款方名字/名稱
        /// </summary>
        [MaxLength(200)]
        public string RPayeeName { get; set; }
        /// <summary>
        /// 設置或獲取兆基收款單位
        /// </summary>
        [MaxLength(200)]
        public string RPayeeUnit { get; set; }
        /// <summary>
        /// 設置或獲取收款方身份證字號
        /// </summary>
        [MaxLength(100)]
        public string RPayeeID { get; set; }
        /// <summary>
        /// 設置或獲取收款方統一編號
        /// </summary>
        [MaxLength(100)]
        public string RPayeeTaxID { get; set; }
        /// <summary>
        /// 設置或獲取付款方
        /// </summary>
        [MaxLength(100)]
        public string RPayer { get; set; }
        /// <summary>
        /// 設置或獲取付款方名字/名稱
        /// </summary>
        [MaxLength(200)]
        public string RPayerName { get; set; }
        /// <summary>
        /// 設置或獲取兆基付款單位
        /// </summary>
        [MaxLength(200)]
        public string RPayerUnit { get; set; }
        /// <summary>
        /// 設置或獲取付款方身份證字號
        /// </summary>
        [MaxLength(100)]
        public string RPayerID { get; set; }
        /// <summary>
        /// 設置或獲取付款方統一編號
        /// </summary>
        [MaxLength(100)]
        public string RPayerTaxID { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址
        /// </summary>
        [MaxLength(100)]
        public string RAdd { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_縣市
        /// </summary>
        [MaxLength(100)]
        public string RAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_市
        /// </summary>
        [MaxLength(100)]        public string RAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_鄉鎮市區
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_市
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_區
        /// </summary>
        [MaxLength(100)]        public string RAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_街路
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_街
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_路
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_段
        /// </summary>
        [MaxLength(100)]
        public string RAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_號
        /// </summary>
        [MaxLength(100)]
        public string RAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_之
        /// </summary>
        [MaxLength(100)]
        public string RAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取領收據狀態
        /// </summary>
        [MaxLength(20)]
        public string RStatus { get; set; }
        /// <summary>
        /// 設置或獲取收費項目總金額
        /// </summary>
        public string RTotalCost { get; set; }
        /// <summary>
        /// 設置或獲取收費方式
        /// </summary>
        [MaxLength(60)]
        public string RPaymentMethod { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        [MaxLength(16)]
        public string RMemo { get; set; }
        /// <summary>
        /// 設置或獲取領收據簽名檔路徑
        /// </summary>
        [MaxLength(1000)]
        public string RSignatureImgPath { get; set; }

        /// <summary>
        /// 設置或獲取用印PDF路徑
        /// </summary>
        public string RSignedPDFPath { get; set; }

        /// <summary>
        /// 設置或獲取未用印PDF路徑
        /// </summary>
        public string RUnsignPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取領收據簽名日期
        /// </summary>
        [MaxLength(10)]
        public string RSignDate { get; set; }        /// <summary>
        /// 設置或獲取重複收訂
        /// </summary>
        [MaxLength(20)]
        public string ROverBooking { get; set; }
        /// <summary>
        /// 設置或獲取業務人員
        /// </summary>
        [MaxLength(100)]
        public string RSales { get; set; }
        /// <summary>
        /// 設置或獲取帳務人員
        /// </summary>
        [MaxLength(100)]
        public string RAccounting { get; set; }
        /// <summary>
        /// 設置或獲取領收據PDF路徑
        /// </summary>
        [MaxLength(1000)]
        public string RPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金起日
        /// </summary>
        [MaxLength(10)]
        public string PIRentStartDate { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金迄日
        /// </summary>
        [MaxLength(10)]
        public string PIRentEndDate { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金
        /// </summary>
        public string PIRent { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIRentMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-押金收費日
        /// </summary>
        [MaxLength(10)]
        public string PIDepositDate { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-押金實收款
        /// </summary>
        public string PIDepositActual { get; set; }        /// <summary>
        /// 設置或獲取收費項目-押金欠款
        /// </summary>
        public string PIDepositOwe { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-押金
        /// </summary>
        public string PIDeposit { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-押金收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIDepositMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-大樓管理費月份
        /// </summary>
        [MaxLength(10)]
        public string PIManagementFeeMonth { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-大樓管理費
        /// </summary>
        public string PIManagementFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-大樓管理費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIManagementFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-仲介服務費
        /// </summary>
        public string PIServiceFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-仲介服務費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIServiceFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(10)]
        public string PIConsultantFeeMonth { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-顧問費
        /// </summary>
        public string PIConsultantFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-顧問費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIConsultantFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用項目
        /// </summary>
        [MaxLength(-1)]
        public string PIEquipmentNote { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用
        /// </summary>
        public string PIEquipmentCost { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIEquipmentCostMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-電費
        /// </summary>
        public string PIElectricityBill { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-電費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIElectricityBillMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-水雜費
        /// </summary>
        public string PIWaterBill { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-水雜費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIWaterBillMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-瓦斯費
        /// </summary>
        public string PIGasFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-瓦斯費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIGasFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-垃圾費
        /// </summary>
        public string PITrashFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-垃圾費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PITrashFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-清潔費
        /// </summary>
        public string PICleaningFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-清潔費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PICleaningFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-行政處理費
        /// </summary>
        public string PIAdministrativeFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-行政處理費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIAdministrativeFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-管理服務費收費方式
        /// </summary>
        public string PIManagementServiceFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-管理服務費收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIManagementServiceFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用項目
        /// </summary>
        [MaxLength(-1)]
        public string PIMiscellaneousFeeNote { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-其他費用
        /// </summary>
        public string PIMiscellaneousFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-其他費用收費方式
        /// </summary>
        [MaxLength(40)]
        public string PIMiscellaneousFeeMethod { get; set; }
        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 設置或獲取創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取刪除用戶
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }

    }
}
