using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{

    /// <summary>
    /// 用戶搜索條件
    /// </summary>
    public class SearchExternalformPDFModel : SearchInputDto<Externalform>
    {
        /// <summary>
        /// 表單代號
        /// </summary>
        public string FormId
        {
            get; set;
        }

        /// <summary>
        /// TBNO 存放BZ欄位判斷
        /// </summary>
        public virtual string TBNO { get; set; }

        /// <summary>
        /// 指定匯款資訊(必須要==true且LRemittanceId或RRemittanceId才會帶入)
        /// </summary>
        public virtual bool? RemittanceCheck { get; set; }

        /// <summary>
        /// L匯款資料
        /// </summary>
        public virtual string LRemittanceId { get; set; }

        /// <summary>
        /// R匯款資料
        /// </summary>
        public virtual string RRemittanceId { get; set; }

        /// <summary>
        /// (自然人)身分證字號
        /// </summary>
        public string LNID
        {
            get; set;
        }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public string LCID
        {
            get; set;
        }

        /// <summary>
        /// (自然人)身分證字號
        /// </summary>
        public string RNID
        {
            get; set;
        }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public string RCID
        {
            get; set;
        }

        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public string BAdd
        {
            get; set;
        }

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
        /// 以下列指定資訊，取代此表單欄位內容
        /// </summary>
        public virtual bool? ReplaceROFieldCheck { get; set; }

        /// <summary>
        /// 租屋公司統一編號
        /// </summary>
        public virtual string ROID { get; set; }

        /// <summary>
        /// 租屋公司名稱
        /// </summary>
        public virtual string ROName { get; set; }

        /// <summary>
        /// 租屋公司負責人
        /// </summary>
        public virtual string RORep { get; set; }

        /// <summary>
        /// 租屋公司地址
        /// </summary>
        public virtual string ROAdd { get; set; }

        /// <summary>
        /// 租屋公司電話  
        /// </summary>
        public virtual string ROTel { get; set; }

        /// <summary>
        /// 租屋公司傳真
        /// </summary>
        public virtual string ROFax { get; set; }

        /// <summary>
        /// 租屋公司營業登記證
        /// </summary>
        public virtual string ROLRNo { get; set; }

        /// <summary>
        /// 事業服務人員(當前USER)
        /// </summary>
        public virtual string ROUserName { get; set; }

        /// <summary>
        /// 住宅管理人員
        /// </summary>
        public virtual string RHMName { get; set; }

        /// <summary>
        /// 住宅管理員證號
        /// </summary>
        public virtual string RHMRNo { get; set; }

        /// <summary>
        /// 經紀人 
        /// </summary>
        public string AGName { get; set; }

        /// <summary>
        /// 經紀人證號 
        /// </summary>
        public string AGLRNo { get; set; }


        /// <summary>
        /// 經紀人電話 
        /// </summary>
        public string AGTel { get; set; }


        /// <summary>
        /// 表單代號
        /// </summary>
        public string testName
        {
            get; set;
        }
        /// <summary>
        /// 表單名稱
        /// </summary>
        public string FormName
        {
            get; set;
        }
        /// <summary>
        /// 公會縣市
        /// </summary>
        public string GuildCountyCity{get; set;}
        /// <summary>
        /// 期數
        /// </summary>
        public string Period{ get; set;}
        /// <summary>
        /// 類別
        /// </summary>
        public string Type
        {
            get; set;
        }
        /// <summary>
        /// 註冊或添加時間 開始
        /// </summary>
        public string CreatorTime1
        {
            get; set;
        }
        /// <summary>
        /// 註冊或添加時間 結束
        /// </summary>
        public string CreatorTime2
        {
            get; set;
        }

    }
}
