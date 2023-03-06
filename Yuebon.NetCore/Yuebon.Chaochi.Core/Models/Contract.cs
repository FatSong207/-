using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 合約數據實體對象
    /// </summary>
    [Table("Chaochi_Contract")]
    [Serializable]
    public class Contract:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 設置或獲取合約名稱
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 設置或獲取合約種類
        /// </summary>
        public string CType { get; set; }

        /// <summary>
        /// 設置或獲取合約分類
        /// </summary>
        public string CCate { get; set; }

        /// <summary>
        /// 設置或獲取目前歷史版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 設置或獲取產生人員
        /// </summary>
        public string UploadUser { get; set; }

        /// <summary>
        /// 設置或獲取備註
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 設置或獲取定稿合約路徑
        /// </summary>
        public string UnsignPDFPath { get; set; }

        /// <summary>
        /// 設置或獲取簽約掃描檔路徑
        /// </summary>
        public string SignedPDFPath { get; set; }

        /// <summary>
        /// 設置或獲取需業務簽名
        /// </summary>
        public string NeedSalesSign { get; set; }

        /// <summary>
        /// 設置或獲取需主管簽名
        /// </summary>
        public string NeedSupervisorSign { get; set; }

        /// <summary>
        /// 設置或獲取需線上審核
        /// </summary>
        public string NeedSignOnline { get; set; }

        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        public string CStatus { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期
        /// </summary>
        public string ContractDate { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_年
        /// </summary>
        public string Contract_Y { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_月
        /// </summary>
        public string Contract_M { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_日
        /// </summary>
        public string Contract_D { get; set; }

        /// <summary>
        /// 設置或獲取社宅物件編號
        /// </summary>
        public string ObjectNo { get; set; }

        /// <summary>
        /// 設置或獲取兆基物件編號
        /// </summary>
        public string CCObjectNo { get; set; }

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

        /// <summary>
        /// 設置或獲取媒合編號
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        public string MOID { get; set; }

        /// <summary>
        /// 設置或獲取來源包租合約編號
        /// </summary>
        public string SCID { get; set; }

        /// <summary>
        /// 設置或獲取二房東-統一編號
        /// </summary>
        public string SLID { get; set; }

        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        public string SLName { get; set; }

        /// <summary>
        /// 設置或獲取二房東-負責人姓名
        /// </summary>
        public string SLRep { get; set; }

        /// <summary>
        /// 設置或獲取二房東-許可字號/登記證字號
        /// </summary>
        public string SLLRNo { get; set; }

        /// <summary>
        /// 設置或獲取二房東-營業地址
        /// </summary>
        public string SLAdd { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話
        /// </summary>
        public string SLTel { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話_區碼
        /// </summary>
        public string SLTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話_號碼
        /// </summary>
        public string SLTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-傳真號碼
        /// </summary>
        public string SLFax { get; set; }

        /// <summary>
        /// 設置或獲取二房東-傳真號碼_區碼
        /// </summary>
        public string SLFax_1 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-傳真號碼_號碼
        /// </summary>
        public string SLFax_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-統一編號
        /// </summary>
        public string MAID { get; set; }

        /// <summary>
        /// 設置或獲取管理方-公司名稱
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 設置或獲取管理方-負責人姓名
        /// </summary>
        public string MRep { get; set; }

        /// <summary>
        /// 設置或獲取管理方-許可字號/登記證字號
        /// </summary>
        public string MLRNo { get; set; }

        /// <summary>
        /// 設置或獲取管理方-營業地址
        /// </summary>
        public string MAdd { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話
        /// </summary>
        public string MTel { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_區碼
        /// </summary>
        public string MTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_號碼
        /// </summary>
        public string MTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼
        /// </summary>
        public string MFax { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼_區碼
        /// </summary>
        public string MFax_1 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼_號碼
        /// </summary>
        public string MFax_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-電子郵件信箱
        /// </summary>
        public string MEmail { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員
        /// </summary>
        public string SIName { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-證書字號
        /// </summary>
        public string SILRNo { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-通訊地址
        /// </summary>
        public string SIAdd { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話
        /// </summary>
        public string SITel { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_區碼
        /// </summary>
        public string SITel_1 { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_號碼
        /// </summary>
        public string SITel_2 { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-電子郵件信箱
        /// </summary>
        public string SIEmail { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-統一編號(身分證明文件編號)
        /// </summary>
        public string AGID { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人
        /// </summary>
        public string AGName { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-證書字號
        /// </summary>
        public string AGLRNo { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-通訊地址
        /// </summary>
        public string AGAdd { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話
        /// </summary>
        public string AGTel { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_區碼
        /// </summary>
        public string AGTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_號碼
        /// </summary>
        public string AGTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取不動產經紀人-電子郵件信箱
        /// </summary>
        public string AGEmail { get; set; }

        /// <summary>
        /// 設置或獲取管理方-所屬業務
        /// </summary>
        public string Sales { get; set; }

        /// <summary>
        /// 設置或獲取管理方-所屬業務姓名
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_完整日期
        /// </summary>
        public string BRDStart { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_完整日期
        /// </summary>
        public string BRDTEnd { get; set; }

        /// <summary>
        /// 設置或獲取戶名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 設置或獲取銀行名稱
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 設置或獲取銀行代號
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 設置或獲取分行名稱
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// 設置或獲取分行代號
        /// </summary>
        public string BranchNo { get; set; }

        /// <summary>
        /// 設置或獲取帳號 
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 設置或獲取房屋租金和車位租金的合計
        /// </summary>
        public string BTRPFee { get; set; }

        /// <summary>
        /// 設置或獲取同意公證
        /// </summary>
        public string Notarization_Y { get; set; }

        /// <summary>
        /// 設置或獲取不同意公證
        /// </summary>
        public string Notarization_N { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>        
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 創建的人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }

        /// <summary>
        /// 創建的人所屬部門ID
        /// </summary>
        public string CreatorUserDeptId { get; set; }

        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        public string LastModifyUserId { get; set; }
    }
}
