using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 領收據數據實體對象
    /// </summary>
    [Table("Chaochi_Receipt")]
    [Serializable]
    public class Receipt:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取領收據編號
        /// </summary>
        public string ReceiptId { get; set; }
        /// <summary>
        /// 設置或獲取領收據類別
        /// </summary>
        public string RCate { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期
        /// </summary>
        public string RDate { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_年
        /// </summary>
        public string RDate_Y { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_月
        /// </summary>
        public string RDate_M { get; set; }
        /// <summary>
        /// 設置或獲取領收據日期_日
        /// </summary>
        public string RDate_D { get; set; }
        /// <summary>
        /// 設置或獲取收款方
        /// </summary>
        public string RPayee { get; set; }
        /// <summary>
        /// 設置或獲取收款方名字/名稱
        /// </summary>
        public string RPayeeName { get; set; }
        /// <summary>
        /// 設置或獲取兆基收款單位
        /// </summary>
        public string RPayeeUnit { get; set; }
        /// <summary>
        /// 設置或獲取收款方身份證字號
        /// </summary>
        public string RPayeeID { get; set; }
        /// <summary>
        /// 設置或獲取收款方統一編號
        /// </summary>
        public string RPayeeTaxID { get; set; }

        /// <summary>
        /// 設置或獲取收款方電話
        /// </summary>
        public string RPayeeTel { get; set; }
        /// <summary>
        /// 設置或獲取付款方
        /// </summary>
        public string RPayer { get; set; }
        /// <summary>
        /// 設置或獲取付款方名字/名稱
        /// </summary>
        public string RPayerName { get; set; }
        /// <summary>
        /// 設置或獲取兆基付款單位
        /// </summary>
        public string RPayerUnit { get; set; }
        /// <summary>
        /// 設置或獲取付款方身份證字號
        /// </summary>
        public string RPayerID { get; set; }
        /// <summary>
        /// 設置或獲取付款方統一編號
        /// </summary>
        public string RPayerTaxID { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址
        /// </summary>
        public string RAdd { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_縣市
        /// </summary>
        public string RAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_縣
        /// </summary>
        public string RAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_市
        /// </summary>
        public string RAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_鄉鎮市區
        /// </summary>
        public string RAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_鄉
        /// </summary>
        public string RAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_鎮
        /// </summary>
        public string RAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_市
        /// </summary>
        public string RAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_區
        /// </summary>
        public string RAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_街路
        /// </summary>
        public string RAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_街
        /// </summary>
        public string RAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物門牌地址_路
        /// </summary>
        public string RAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_段
        /// </summary>
        public string RAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_巷
        /// </summary>
        public string RAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_弄
        /// </summary>
        public string RAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_號
        /// </summary>
        public string RAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_樓
        /// </summary>
        public string RAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取建物門牌地址_之
        /// </summary>
        public string RAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取領收據狀態
        /// </summary>
        public string RStatus { get; set; }
        /// <summary>
        /// 設置或獲取收費項目總金額
        /// </summary>
        public string RTotalCost { get; set; }
        /// <summary>
        /// 設置或獲取收費方式
        /// </summary>
        public string RPaymentMethod { get; set; }
        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string RMemo { get; set; }
        /// <summary>
        /// 設置或獲取領收據簽名檔路徑
        /// </summary>
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
        public string RSignDate { get; set; }        /// <summary>
        /// 設置或獲取重複收訂
        /// </summary>
        public string ROverBooking { get; set; }
        /// <summary>
        /// 設置或獲取業務人員
        /// </summary>
        public string RSales { get; set; }
        /// <summary>
        /// 設置或獲取帳務人員
        /// </summary>
        public string RAccounting { get; set; }
        /// <summary>
        /// 設置或獲取領收據PDF路徑
        /// </summary>
        public string RPDFPath { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金起日
        /// </summary>
        public string PIRentStartDate { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金起日_年
        /// </summary>
        public string PIRentStartDate_Y { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金起日_月
        /// </summary>
        public string PIRentStartDate_M { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金起日_日
        /// </summary>
        public string PIRentStartDate_D { get; set; }        /// <summary>
        /// 設置或獲取收費項目-租金迄日
        /// </summary>
        public string PIRentEndDate { get; set; }

        /// <summary>
        /// 設置或獲取收費項目-租金迄日_年
        /// </summary>
        public string PIRentEndDate_Y { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金迄日_月
        /// </summary>
        public string PIRentEndDate_M { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金迄日_日
        /// </summary>
        public string PIRentEndDate_D { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-租金
        /// </summary>
        public string PIRent { get; set; }

        /// <summary>
        /// 設置或獲取收費項目-租金月數
        /// </summary>
        public string PIRentMonth { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-押金收費日
        /// </summary>
        public string PIDepositDate { get; set; }

        /// <summary>
        /// 設置或獲取收費項目-押金收費日_年
        /// </summary>
        public string PIDepositDate_Y { get; set; }

                /// <summary>
        /// 設置或獲取收費項目-押金收費日_月
        /// </summary>
        public string PIDepositDate_M { get; set; }

        /// <summary>
        /// 設置或獲取收費項目-押金收費日_日
        /// </summary>
        public string PIDepositDate_D { get; set; }
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
        /// 設置或獲取收費項目-大樓管理費月份
        /// </summary>
        public string PIManagementFeeMonth { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-大樓管理費
        /// </summary>
        public string PIManagementFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-仲介服務費
        /// </summary>
        public string PIServiceFee { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string PIConsultantFeeMonth { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-顧問費
        /// </summary>
        public string PIConsultantFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用項目
        /// </summary>
        public string PIEquipmentNote { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用
        /// </summary>
        public string PIEquipmentCost { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-電費
        /// </summary>
        public string PIElectricityBill { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-水雜費
        /// </summary>
        public string PIWaterBill { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-瓦斯費
        /// </summary>
        public string PIGasFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-垃圾費
        /// </summary>
        public string PITrashFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-清潔費
        /// </summary>
        public string PICleaningFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-行政處理費
        /// </summary>
        public string PIAdministrativeFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-管理服務費收費方式
        /// </summary>
        public string PIManagementServiceFee { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-設備費用項目
        /// </summary>
        public string PIMiscellaneousFeeNote { get; set; }
        /// <summary>
        /// 設置或獲取收費項目-其他費用
        /// </summary>
        public string PIMiscellaneousFee { get; set; }
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
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 創建的人所屬組織ID
        /// </summary>
        public virtual string CreatorUserOrgId { get; set; }

        /// <summary>
        /// 創建的人所屬部門ID
        /// </summary>
        public virtual string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶
        /// </summary>
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取刪除用戶
        /// </summary>
        public string DeleteUserId { get; set; }

    }
}
