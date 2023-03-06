using NPOI.OpenXmlFormats.Dml;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractOutputDto
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
        /// 設置或獲取合約名稱
        /// </summary>
        [MaxLength(200)]
        public string CName { get; set; }
        /// <summary>
        /// 設置或獲取合約種類
        /// </summary>
        [MaxLength(10)]
        public string CType { get; set; }

        /// <summary>
        /// 設置或獲取合約分類
        /// </summary>
        [MaxLength(50)]
        public string CCate { get; set; }

        /// <summary>
        /// 設置或獲取目前歷史版本
        /// </summary>
        [MaxLength(10)]
        public string Version { get; set; }

        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 設置或獲取產生人員
        /// </summary>
        public string UploadUser { get; set; }        /// <summary>
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
        [MaxLength(10)]
        public string NeedSalesSign { get; set; }

        /// <summary>
        /// 設置或獲取需主管簽名
        /// </summary>
        [MaxLength(10)]
        public string NeedSupervisorSign { get; set; }

        /// <summary>
        /// 設置或獲取需線上審核
        /// </summary>
        [MaxLength(10)]
        public string NeedSignOnline { get; set; }
        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        [MaxLength(30)]
        public string CStatus { get; set; }
        /// <summary>
        /// 設置或獲取簽約日期
        /// </summary>
        [MaxLength(15)]
        public string ContractDate { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_年
        /// </summary>
        [MaxLength(10)]
        public string Contract_Y { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_月
        /// </summary>
        [MaxLength(10)]
        public string Contract_M { get; set; }

        /// <summary>
        /// 設置或獲取簽約日期_日
        /// </summary>
        [MaxLength(10)]        public string Contract_D { get; set; }        /// <summary>
        /// 設置或獲取社宅物件編號
        /// </summary>
        [MaxLength(100)]        public string ObjectNo { get; set; }        /// <summary>
        /// 設置或獲取兆基物件編號
        /// </summary>
        [MaxLength(100)]        public string CCObjectNo { get; set; }        /// <summary>
        /// 設置或獲取媒合編號
        /// </summary>
        [MaxLength(100)]
        public string MID { get; set; }

        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        [MaxLength(100)]
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取來源包租合約編號
        /// </summary>
        public string SCID { get; set; }        /// <summary>
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        [MaxLength(100)]
        public string LSID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string LSName { get; set; }
        /// <summary>
        /// 設置或獲取出租人-負責人(法人)
        /// </summary>
        [MaxLength(100)]
        public string LSRep { get; set; }
        /// <summary>
        /// 設置或獲取出租人-完整通訊地址
        /// </summary>
        [MaxLength(100)]
        public string LSAddC { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string LSTel { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_區碼
        /// </summary>
        
        [MaxLength(10)]
        public string LSTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string LSTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取出租人-手機
        /// </summary>
        [MaxLength(10)]
        public string LSCell { get; set; }
        /// <summary>
        /// 設置或獲取承租人-身份證號或統一編號
        /// </summary>
        [MaxLength(100)]
        public string RNID { get; set; }
        /// <summary>
        /// 設置或獲取承租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string RNName { get; set; }
        /// <summary>
        /// 設置或獲取承租人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string RNTel { get; set; }
        /// <summary>
        /// 設置或獲取承租人-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string RNTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取承租人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]        public string RNTel_2 { get; set; }        /// <summary>
        /// 設置或獲取承租人-完整通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RNAdd { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取建物完整地址
        /// </summary>
        [MaxLength(100)]
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
        public string BPNo_3_B { get; set; }        /// <summary>
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
        public string BPNo_3_C { get; set; }        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string BCar_Y { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string BCar_N { get; set; }

        /// <summary>
        /// 設置或獲取車位編號
        /// </summary>
        [MaxLength(50)]
        public string BCarNo { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string BArea { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string BTRent { get; set; }

        /// <summary>
        /// 設置或獲取租金(合約用)
        /// </summary>
        [MaxLength(100)]
        public virtual string BTCRent { get; set; }

        /// <summary>
        /// 設置或獲取租金(合約用)
        /// </summary>
        [MaxLength(100)]
        public virtual string B1TCRent { get; set; }
        /// <summary>
        /// 設置或獲取租金繳款日
        /// </summary>
        [MaxLength(20)]
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
        /// 設置或獲取出租人金融機構
        /// </summary>
        [MaxLength(100)]
        public string LBankName { get; set; }

        /// <summary>
        /// 設置或獲取出租人戶名
        /// </summary>
        [MaxLength(100)]
        public string LAName { get; set; }

        /// <summary>
        /// 設置或獲取出租人帳號
        /// </summary>
        [MaxLength(50)]
        public string LANo { get; set; }

        /// <summary>
        /// 設置或獲取承租人金融機構
        /// </summary>
        [MaxLength(100)]
        public string RBankName { get; set; }

        /// <summary>
        /// 設置或獲取承租人戶名
        /// </summary>
        [MaxLength(100)]
        public string RAName { get; set; }

        /// <summary>
        /// 設置或獲取承租人帳號
        /// </summary>
        [MaxLength(50)]
        public string RANo { get; set; }

        //public string BankName
        //{
        //    get
        //    {
        //        switch(CType) {
        //            case "1":
        //                return LBankName;
        //            case "2":
        //                return RBankName;
        //            case "3":
        //                return LBankName + ";" + RBankName;
        //            default: 
        //                return LBankName;
        //        }
        //    }
        //    set { }
        //}

        public string AName
        {
            get
            {
                switch (CType) {
                    case "1":
                        return LAName;
                    case "2":
                        return RAName;
                    case "3":
                        return LAName + ";" + RAName;
                    default:
                        return LAName;
                }
            }
            set { }
        }


        public string ANo
        {
            get
            {
                switch (CType) {
                    case "1":
                        return LANo;
                    case "2":
                        return RANo;
                    case "3":
                        return LANo + ";" + RANo;
                    default:
                        return LANo;
                }
            }
            set { }
        }
        /// <summary>
        /// 設置或獲取押金月數
        /// </summary>
        [MaxLength(20)]
        public string BDMon { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Bdeposit { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-轉帳
        /// </summary>
        [MaxLength(50)]
        public string BTransfer { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-票據
        /// </summary>
        [MaxLength(50)]
        public string BBill { get; set; }

        /// <summary>
        /// 設置或獲取租金付款方式-現金
        /// </summary>
        [MaxLength(50)]
        public string BCash { get; set; }
        /// <summary>
        /// 設置或獲取二房東-統一編號
        /// </summary>
        [MaxLength(100)]
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        [MaxLength(200)]
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取二房東-負責人姓名
        /// </summary>
        [MaxLength(200)]
        public string SLRep { get; set; }
        /// <summary>
        /// 設置或獲取二房東-許可字號/登記證字號
        /// </summary>
        [MaxLength(100)]
        public string SLLRNo { get; set; }
        /// <summary>
        /// 設置或獲取二房東-營業地址
        /// </summary>
        [MaxLength(100)]
        public string SLAdd { get; set; }
        /// <summary>
        /// 設置或獲取二房東-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string SLTel { get; set; }
        /// <summary>
        /// 設置或獲取二房東-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string SLTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]        public string SLTel_2 { get; set; }        /// <summary>
        /// 設置或獲取二房東-傳真號碼
        /// </summary>
        [MaxLength(20)]
        public string SLFax { get; set; }

        /// <summary>
        /// 設置或獲取二房東-傳真號碼_區碼
        /// </summary>
        [MaxLength(10)]
        public string SLFax_1 { get; set; }

        /// <summary>
        /// 設置或獲取二房東-傳真號碼_號碼
        /// </summary>
        [MaxLength(10)]
        public string SLFax_2 { get; set; }        /// <summary>
        /// 設置或獲取管理方-統一編號
        /// </summary>
        [MaxLength(100)]
        public string MAID { get; set; }
        /// <summary>
        /// 設置或獲取管理方-公司名稱
        /// </summary>
        [MaxLength(200)]
        public string MName { get; set; }
        /// <summary>
        /// 設置或獲取管理方-負責人姓名
        /// </summary>
        [MaxLength(200)]
        public string MRep { get; set; }
        /// <summary>
        /// 設置或獲取管理方-許可字號/登記證字號
        /// </summary>
        [MaxLength(100)]
        public string MLRNo { get; set; }
        /// <summary>
        /// 設置或獲取管理方-營業地址
        /// </summary>
        [MaxLength(100)]
        public string MAdd { get; set; }
        /// <summary>
        /// 設置或獲取管理方-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string MTel { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string MTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string MTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼
        /// </summary>
        [MaxLength(20)]
        public string MFax { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼_區碼
        /// </summary>
        [MaxLength(10)]
        public string MFax_1 { get; set; }

        /// <summary>
        /// 設置或獲取管理方-傳真號碼_號碼
        /// </summary>
        [MaxLength(10)]
        public string MFax_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string MEmail { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員
        /// </summary>
        [MaxLength(200)]
        public string SIName { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-證書字號
        /// </summary>
        [MaxLength(100)]
        public string SILRNo { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-通訊地址
        /// </summary>
        [MaxLength(100)]
        public string SIAdd { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string SITel { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string SITel_1 { get; set; }

        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string SITel_2 { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string SIEmail { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-統一編號(身分證明文件編號)
        /// </summary>
        [MaxLength(100)]
        public string AGID { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人
        /// </summary>
        [MaxLength(200)]
        public string AGName { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-證書字號
        /// </summary>
        [MaxLength(100)]
        public string AGLRNo { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-通訊地址
        /// </summary>
        [MaxLength(100)]
        public string AGAdd { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string AGTel { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string AGTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string AGTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string AGEmail { get; set; }

        /// <summary>
        /// 設置或獲取管理方-所屬業務
        /// </summary>
        [MaxLength(100)]
        public string Sales { get; set; }

        /// <summary>
        /// 設置或獲取管理方-所屬業務姓名
        /// </summary>
        [MaxLength(200)]
        public string SalesName { get; set; }

        /// <summary>
        /// 設置或獲取戶名
        /// </summary>
        [MaxLength(100)]
        public string AccountName { get; set; }
        /// <summary>
        /// 設置或獲取銀行名稱
        /// </summary>
        [MaxLength(100)]
        public string BankName { get; set; }
        /// <summary>
        /// 設置或獲取銀行代號
        /// </summary>
        [MaxLength(10)]
        public string BankNo { get; set; }
        /// <summary>
        /// 設置或獲取分行名稱
        /// </summary>
        [MaxLength(100)]
        public string BranchName { get; set; }
        /// <summary>
        /// 設置或獲取分行代號
        /// </summary>
        [MaxLength(10)]
        public string BranchNo { get; set; }
        /// <summary>
        /// 設置或獲取帳號 
        /// </summary>
        [MaxLength(25)]
        public string AccountNo { get; set; }

        /// <summary>
        /// 設置或獲取房屋租金和車位租金的合計
        /// </summary>
        [MaxLength(10)]
        public string BTRPFee { get; set; }

        /// <summary>
        /// 設置或獲取同意公證
        /// </summary>
        [MaxLength(10)]
        public string Notarization_Y { get; set; }

        /// <summary>
        /// 設置或獲取不同意公證
        /// </summary>
        [MaxLength(10)]
        public string Notarization_N { get; set; }


        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

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
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        // 出租人
        public ContractLandlord LSInfo { get; set; }

        // 承租人
        public ContractRenter RNInfo { get; set; }

        // 建物
        public ContractBuildingOutputDto BuildingInfo { get; set; }

        // BZ欄位
        public TBNoCOutputDto BZOutputDto { get; set; }        // BZ欄位2
        public TBNoC_2OutputDto BZ2OutputDto { get; set; }

        // BZ欄位3
        public TBNoC_3OutputDto BZ3OutputDto { get; set; }

        //// BZ欄位1
        //public TBNoC1OutputDto BZC1OutputDto { get; set; }        //// BZ欄位1_2
        //public TBNoC1_2OutputDto BZC1_2OutputDto { get; set; }        //// BZ欄位1_3
        //public TBNoC1_3OutputDto BZC1_3OutputDto { get; set; }        //// BZ欄位2
        //public TBNoC2OutputDto BZC2OutputDto { get; set; }

        //// BZ欄位2_2
        //public TBNoC2_2OutputDto BZC2_2OutputDto { get; set; }        //// BZ欄位2_3
        //public TBNoC2_3OutputDto BZC2_3OutputDto { get; set; }

        //// BZ欄位3
        //public TBNoC3OutputDto BZC3OutputDto { get; set; }

        //// BZ欄位3_2
        //public TBNoC3_2OutputDto BZC3_2OutputDto { get; set; }        //// BZ欄位3_3
        //public TBNoC3_3OutputDto BZC3_3OutputDto { get; set; }

        // 合約相關資料
        public ContractRelevantOutputDto ContractRelevantInfo { get; set; }

        // 附件
        public List<ContractAttachmentOutputDto> MajorAttachmentList { get; set; }

        // 附件
        public List<ContractAttachmentOutputDto> MinorAttachmentList { get; set; }

        // 歷史版本
        public List<HistoryFormContractOutputDto> ContractHistoryList { get; set; }

        public List<ContractFlowLogOutputDto> ContractFlowLogList { get; set; }

        public string SignedPDF {
            get {
                if (!string.IsNullOrWhiteSpace(SignedPDFPath)) {
                    return "已存在";
                } else {
                    return "未上傳";
                }
            }
            set { }
        }

        string notary = string.Empty;
        public string Notarization {

            get
            {
                if (!string.IsNullOrWhiteSpace(Notarization_Y)) {
                    if ("/Yes".Equals(Notarization_Y.Trim())) {
                        notary = "是";
                    }
                } else if(!string.IsNullOrWhiteSpace(Notarization_N)) {
                    if ("/Yes".Equals(Notarization_N.Trim())) {
                        notary = "否";
                    }
                } else {
                    notary = "否";
                }
                return notary;
            }
            
            set { }
        }

        // 待辦事項用
        public string TodoType = "";
    }
}
