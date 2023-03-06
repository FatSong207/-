using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約輸入對象模型
    /// </summary>
    [AutoMap(typeof(Contract))]
    [Serializable]
    public class ContractInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }

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
        /// 設置或獲取合約狀態
        /// </summary>
        public string CStatus { get; set; }

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
        /// 設置或獲取出租人-負責人(法人)
        /// </summary>
        public string LSRep { get; set; }

        /// <summary>
        /// 設置或獲取出租人-完整通訊地址
        /// </summary>
        public string LSAddC { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣/市
        /// </summary>
        public string LSAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣
        /// </summary>
        public string LSAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        public string LSAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        public string LSAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉
        /// </summary>
        public string LSAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_鎮
        /// </summary>
        public string LSAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        public string LSAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_區
        /// </summary>
        public string LSAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_街/路
        /// </summary>
        public string LSAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_街
        /// </summary>
        public string LSAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_路
        /// </summary>
        public string LSAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_段
        /// </summary>
        public string LSAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_巷
        /// </summary>
        public string LSAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_弄
        /// </summary>
        public string LSAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_號
        /// </summary>
        public string LSAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        public string LSAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        public string LSAddC_9 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話
        /// </summary>
        public string LSTel { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_區碼
        /// </summary>
        public string LSTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_號碼
        /// </summary>
        public string LSTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-手機
        /// </summary>
        public string LSCell { get; set; }

        /// <summary>
        /// 設置或獲取承租人-聯絡電話
        /// </summary>
        public string RNTel { get; set; }

        /// <summary>
        /// 設置或獲取承租人-聯絡電話_區碼
        /// </summary>
        public string RNTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-聯絡電話_號碼
        /// </summary>
        public string RNTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-完整通訊地址
        /// </summary>
        public string RNAdd { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣/市
        /// </summary>
        public string RNAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣
        /// </summary>
        public string RNAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        public string RNAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        public string RNAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉
        /// </summary>
        public string RNAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_鎮
        /// </summary>
        public string RNAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        public string RNAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_區
        /// </summary>
        public string RNAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_街/路
        /// </summary>
        public string RNAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_街
        /// </summary>
        public string RNAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_路
        /// </summary>
        public string RNAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_段
        /// </summary>
        public string RNAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_巷
        /// </summary>
        public string RNAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_弄
        /// </summary>
        public string RNAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_號
        /// </summary>
        public string RNAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        public string RNAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        public string RNAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_縣/市
        /// </summary>
        public string BAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_縣
        /// </summary>
        public string BAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_市
        /// </summary>
        public string BAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_鄉/鎮/市/區
        /// </summary>
        public string BAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_鄉
        /// </summary>
        public string BAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_鎮
        /// </summary>
        public string BAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_市
        /// </summary>
        public string BAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_區
        /// </summary>
        public string BAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_街/路
        /// </summary>
        public string BAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_街
        /// </summary>
        public string BAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_路
        /// </summary>
        public string BAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_段
        /// </summary>
        public string BAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_巷
        /// </summary>
        public string BAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_弄
        /// </summary>
        public string BAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_號
        /// </summary>
        public string BAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_樓
        /// </summary>
        public string BAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取建物地址_樓
        /// </summary>
        public string BAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落建號-地段
        /// </summary>
        public string BNo_1 { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落建號-小段
        /// </summary>
        public string BNo_2 { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落建號-地號
        /// </summary>
        public string BNo_3 { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地段 A組
        /// </summary>
        public string BPNo_1_A { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-小段 A組
        /// </summary>
        public string BPNo_2_A { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地號 A組
        /// </summary>
        public string BPNo_3_A { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地段 B組
        /// </summary>
        public string BPNo_1_B { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-小段 B組
        /// </summary>
        public string BPNo_2_B { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地號 B組
        /// </summary>
        public string BPNo_3_B { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地段 C組
        /// </summary>
        public string BPNo_1_C { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-小段 C組
        /// </summary>
        public string BPNo_2_C { get; set; }

        /// <summary>
        /// 設置或獲取建物坐落地號-地號 C組
        /// </summary>
        public string BPNo_3_C { get; set; }

        /// <summary>
        /// 設置或獲取有車位
        /// </summary>
        public string BCar_Y { get; set; }

        /// <summary>
        /// 設置或獲取無車位
        /// </summary>
        public string BCar_N { get; set; }

        /// <summary>
        /// 設置或獲取車位編號
        /// </summary>
        public string BCarNo { get; set; }

        /// <summary>
        /// 設置或獲取房屋面積
        /// </summary>
        public string BArea { get; set; }

        /// <summary>
        /// 設置或獲取租金
        /// </summary>
        public string BTRent { get; set; }

        /// <summary>
        /// 設置或獲取租金(合約用)
        /// </summary>
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 設置或獲取租金繳款日
        /// </summary>
        public string BTRent_Day { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_完整日期
        /// </summary>
        public string BRDStart { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_年
        /// </summary>
        public string BRDStart_Y { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_月
        /// </summary>
        public string BRDStart_M { get; set; }

        /// <summary>
        /// 設置或獲取租賃起日_日
        /// </summary>
        public string BRDStart_D { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_完整日期
        /// </summary>
        public string BRDTEnd { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_年
        /// </summary>
        public string BRDTEnd_Y { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_月
        /// </summary>
        public string BRDTEnd_M { get; set; }

        /// <summary>
        /// 設置或獲取租賃迄日_日
        /// </summary>
        public string BRDTEnd_D { get; set; }

        /// <summary>
        /// 設置或獲取車位租金
        /// </summary>
        public string BParkFee { get; set; }

        /// <summary>
        /// 設置或獲取出租人金融機構
        /// </summary>
        public string LBankName { get; set; }

        /// <summary>
        /// 設置或獲取出租人戶名
        /// </summary>
        public string LAName { get; set; }

        /// <summary>
        /// 設置或獲取出租人帳號
        /// </summary>
        public string LANo { get; set; }

        /// <summary>
        /// 設置或獲取承租人金融機構
        /// </summary>
        public string RBankName { get; set; }

        /// <summary>
        /// 設置或獲取承租人戶名
        /// </summary>
        public string RAName { get; set; }

        /// <summary>
        /// 設置或獲取承租人帳號
        /// </summary>
        public string RANo { get; set; }

        /// <summary>
        /// 設置或獲取押金月數
        /// </summary>
        public string BDMon { get; set; }

        /// <summary>
        /// 設置或獲取押金
        /// </summary>
        public string Bdeposit { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-轉帳
        /// </summary>
        public string BTransfer { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-票據
        /// </summary>
        public string BBill { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-現金
        /// </summary>
        public string BCash { get; set; }

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
        /// 設置或獲取合約帳號對象
        /// </summary>
        public string RemittanceTarget { get; set; }

        /// <summary>
        /// 設置或獲取社宅/一般宅
        /// </summary>
        public string RemittanceType { get; set; }

        /// <summary>
        /// 設置或獲取出租人戶名
        /// </summary>
        public string AccountNameL { get; set; }

        /// <summary>
        /// 設置或獲取承租人戶名
        /// </summary>
        public string AccountNameR { get; set; }

        /// <summary>
        /// 設置或獲取公司戶名
        /// </summary>
        public string AccountNameC { get; set; }

        /// <summary>
        /// 設置或獲取使用單位
        /// </summary>
        public string UseCounty { get; set; }

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


        public string Auditor { get; set; }

        public string Action { get; set; }

        public string Comment { get; set; }

        public string AID { get; set; }

        public string AttachmentFormName { get; set; }

        // 附件
        public List<ContractAttachmentOutputDto> attachmentList { get; set; }

        // 歷史版本
        public List<HistoryFormContractOutputDto> historyList { get; set; }
    }
}
