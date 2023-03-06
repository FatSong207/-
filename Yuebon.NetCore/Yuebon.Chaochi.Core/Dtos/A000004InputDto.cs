using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class A000004InputDto
    {
        #region 承租人

        public virtual string RCID { get; set; }

        public virtual string RCName { get; set; }

        public virtual string RCEngName { get; set; }

        public virtual string RCTel_1 { get; set; }

        public virtual string RCTel_2 { get; set; }

        public virtual string RCRep { get; set; }

        public virtual string RCCell { get; set; }

        public virtual string RCMail { get; set; }

        public virtual string RCAdd { get; set; }

        public virtual string RCAdd_1 { get; set; }

        public virtual string RCAdd_1_1 { get; set; }

        public virtual string RCAdd_1_2 { get; set; }

        public virtual string RCAdd_2 { get; set; }

        public virtual string RCAdd_2_1 { get; set; }

        public virtual string RCAdd_2_2 { get; set; }

        public virtual string RCAdd_2_3 { get; set; }

        public virtual string RCAdd_2_4 { get; set; }

        public virtual string RCAdd_3 { get; set; }

        public virtual string RCAdd_3_1 { get; set; }

        public virtual string RCAdd_3_2 { get; set; }

        public virtual string RCAdd_4 { get; set; }

        public virtual string RCAdd_5 { get; set; }

        public virtual string RCAdd_6 { get; set; }

        public virtual string RCAdd_7 { get; set; }

        public virtual string RCAdd_8 { get; set; }

        public virtual string RCAdd_9 { get; set; }

        public virtual string RCAddSame { get; set; }

        public virtual string RCAddC_1 { get; set; }

        public virtual string RCAddC_1_1 { get; set; }

        public virtual string RCAddC_1_2 { get; set; }

        public virtual string RCAddC_2 { get; set; }

        public virtual string RCAddC_2_1 { get; set; }

        public virtual string RCAddC_2_2 { get; set; }

        public virtual string RCAddC_2_3 { get; set; }

        public virtual string RCAddC_2_4 { get; set; }

        public virtual string RCAddC_3 { get; set; }

        public virtual string RCAddC_3_1 { get; set; }

        public virtual string RCAddC_3_2 { get; set; }

        public virtual string RCAddC_4 { get; set; }

        public virtual string RCAddC_5 { get; set; }

        public virtual string RCAddC_6 { get; set; }

        public virtual string RCAddC_7 { get; set; }

        public virtual string RCAddC_8 { get; set; }

        public virtual string RCAddC_9 { get; set; }

        #endregion

        #region 緊急聯絡人(親屬)

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_姓名
        /// </summary>
        public string RCECName { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_身分證字號
        /// </summary>
        public string RCECID { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_男
        /// </summary>
        public string RCECGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_女
        /// </summary>
        public string RCECGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_生日
        /// </summary>
        public string RCECBirthday { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_關係
        /// </summary>
        public string RCECRelation { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(手機)
        /// </summary>
        public string RCECCell { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(區號)
        /// </summary>
        public string RCECTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(號碼)
        /// </summary>
        public string RCECTel_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_戶籍地址
        /// </summary>
        public string RCECAdd { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址同戶籍地址
        /// </summary>
        public string RCECAddSame { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址
        /// </summary>
        public string RCECAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECAddC_9 { get; set; }


        #endregion 急聯絡人(親屬)

        #region 緊急聯絡人(朋友)

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_姓名
        /// </summary>
        public string RCECFName { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_身分證字號
        /// </summary>
        public string RCECFID { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_男
        /// </summary>
        public string RCECFGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_女
        /// </summary>
        public string RCECFGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_生日
        /// </summary>
        public string RCECFBirthday { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_連絡電話
        /// </summary>
        public string RCECFCell { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_關係
        /// </summary>
        public string RCECFRelation { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_戶籍地址
        /// </summary>
        public string RCECFAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址同戶籍地址
        /// </summary>
        public string RCECFAddSame { get; set; }

        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址
        /// </summary>
        public string RCECFAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCECFAddC_9 { get; set; }


        #endregion

        #region 簽約代理人

        /// <summary>
        /// 設置或獲取簽約代理人_姓名
        /// </summary>
        public string RCAGName { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_身分證字號
        /// </summary>
        public string RCAGID { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人性別_男
        /// </summary>
        public string RCAGGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人性別_女
        /// </summary>
        public string RCAGGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_生日
        /// </summary>
        public string RCAGBirthday { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_戶籍地址
        /// </summary>
        public string RCAGAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址同戶籍地址
        /// </summary>
        public string RCAGAddSame { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址
        /// </summary>
        public string RCAGAddC { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCAGAddC_9 { get; set; }

        /// <summary>
        /// 設置或獲取簽約代理人_聯絡電話(手機)
        /// </summary>
        public string RCAGCell { get; set; }

        /// <summary>
        /// 設置或獲取--簽約代理人_聯絡電話(區號)
        /// </summary>
        public string RCAGTel_1 { get; set; }

        /// <summary>
        /// 設置或獲取-簽約代理人_聯絡電話(號碼)
        /// </summary>
        public string RCAGTel_2 { get; set; }


        #endregion

        #region 保證人

        /// <summary>
        /// 設置或獲取保證人_姓名
        /// </summary>
        public string RCGName { get; set; }

        /// <summary>
        /// 設置或獲取保證人_身分證字號
        /// </summary>
        public string RCGID { get; set; }

        /// <summary>
        /// 設置或獲取保證人性別_男
        /// </summary>
        public string RCGGender_1 { get; set; }

        /// <summary>
        /// 設置或獲取保證人性別_女
        /// </summary>
        public string RCGGender_2 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_生日
        /// </summary>
        public string RCGBirthday { get; set; }

        /// <summary>
        /// 設置或獲取保證人_戶籍地址
        /// </summary>
        public string RCGAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_通訊地址同戶籍地址
        /// </summary>
        public string RCGAddSame { get; set; }

        /// <summary>
        /// 設置或獲取保證人_通訊地址
        /// </summary>
        public string RCGCAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_1_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_2_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3_1 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_3_2 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_4 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_5 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_6 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_7 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_8 { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RCGCAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取保證人_聯絡電話
        /// </summary>
        public string RCGCell { get; set; }

        /// <summary>
        /// 保證人_關係
        /// </summary>
        public string RCGRelation { get; set; }


        #endregion

        #region 匯款資料

        public virtual bool? RCanModify { get; set; }
        public virtual string RAName { get; set; }
        public virtual string RBankName { get; set; }
        public virtual string RBankNo { get; set; }
        public virtual string RBranchName { get; set; }
        public virtual string RBranchNo { get; set; }
        public virtual string RANo { get; set; }

        #endregion

        #region 家庭成員

        public virtual string RCFName_A { get; set; }

        public virtual string RCFID_1_A { get; set; }

        public virtual string RCFBirthday_A { get; set; }

        public virtual string RCFRelation_A { get; set; }

        public virtual string RCFCell_A { get; set; }

        public virtual string RCFName_B { get; set; }

        public virtual string RCFID_1_B { get; set; }

        public virtual string RCFBirthday_B { get; set; }

        public virtual string RCFRelation_B { get; set; }

        public virtual string RCFCell_B { get; set; }

        public virtual string RCFName_C { get; set; }

        public virtual string RCFID_1_C { get; set; }

        public virtual string RCFBirthday_C { get; set; }

        public virtual string RCFRelation_C { get; set; }

        public virtual string RCFCell_C { get; set; }

        public virtual string RCFName_D { get; set; }

        public virtual string RCFID_1_D { get; set; }

        public virtual string RCFBirthday_D { get; set; }

        public virtual string RCFRelation_D { get; set; }

        public virtual string RCFCell_D { get; set; }

        public virtual string RCFName_E { get; set; }

        public virtual string RCFID_1_E { get; set; }

        public virtual string RCFBirthday_E { get; set; }

        public virtual string RCFRelation_E { get; set; }

        public virtual string RCFCell_E { get; set; }

        public virtual string RCFName_F { get; set; }

        public virtual string RCFID_1_F { get; set; }

        public virtual string RCFBirthday_F { get; set; }

        public virtual string RCFRelation_F { get; set; }

        public virtual string RCFCell_F { get; set; }

        public virtual string RCFName_G { get; set; }

        public virtual string RCFID_1_G { get; set; }

        public virtual string RCFBirthday_G { get; set; }

        public virtual string RCFRelation_G { get; set; }

        public virtual string RCFCell_G { get; set; }

        #endregion 家庭成員

        #region BZ欄位

        /// <summary>
        /// 是否已歸檔
        /// </summary>
        public virtual string IsFiled { get; set; }

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
