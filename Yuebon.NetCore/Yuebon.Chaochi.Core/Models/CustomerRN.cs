
using System;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 客戶表_房客_自然人，數據實體對象
    /// </summary>
    [Table("Chaochi_CustomerRN")]
    [Serializable]
    public class CustomerRN : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public CustomerRN()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string RNName { get; set; }

        /// <summary>
        /// 身分證號/居留證號
        /// </summary>
        public virtual string RNID { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_1
        /// </summary>
        public virtual string RNID_1_1 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_2
        /// </summary>
        public virtual string RNID_1_2 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_3
        /// </summary>
        public virtual string RNID_1_3 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_4
        /// </summary>
        public virtual string RNID_1_4 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_5
        /// </summary>
        public virtual string RNID_1_5 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_6
        /// </summary>
        public virtual string RNID_1_6 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_7
        /// </summary>
        public virtual string RNID_1_7 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_8
        /// </summary>
        public virtual string RNID_1_8 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_9
        /// </summary>
        public virtual string RNID_1_9 { get; set; }

        /// <summary>
        /// 身分證號/居留證號(拆開)_10
        /// </summary>
        public virtual string RNID_1_10 { get; set; }

        /// <summary>
        /// LINEID
        /// </summary>
        public virtual string RNLine { get; set; }

        /// <summary>
        /// 承租地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 性別_男
        /// </summary>
        public virtual string RNGender1 { get; set; }

        /// <summary>
        /// 性別_女
        /// </summary>
        public virtual string RNGender2 { get; set; }

        /// <summary>
        /// 生日_年/月/日
        /// </summary>
        public virtual string RNBirthday { get; set; }

        /// <summary>
        /// 生日_年
        /// </summary>
        public virtual string RNBirthday_Y { get; set; }

        /// <summary>
        /// 生日_月
        /// </summary>
        public virtual string RNBirthday_M { get; set; }

        /// <summary>
        /// 生日_日
        /// </summary>
        public virtual string RNBirthday_D { get; set; }

        /// <summary>
        /// 戶口名簿戶號
        /// </summary>
        public virtual string RNFAccount { get; set; }

        /// <summary>
        /// 完整電話號碼_日
        /// </summary>
        public virtual string RNTel { get; set; }

        /// <summary>
        /// 電話區碼_日
        /// </summary>
        public virtual string RNTel_1 { get; set; }

        /// <summary>
        /// 電話號碼_日
        /// </summary>
        public virtual string RNTel_2 { get; set; }

        /// <summary>
        /// 完整電話號碼_夜
        /// </summary>
        public virtual string RNTelN { get; set; }

        /// <summary>
        /// 電話區碼_夜
        /// </summary>
        public virtual string RNTelN_1 { get; set; }

        /// <summary>
        /// 電話號碼_夜
        /// </summary>
        public virtual string RNTelN_2 { get; set; }

        /// <summary>
        /// 行動電話號碼
        /// </summary>
        public virtual string RNCell { get; set; }

        /// <summary>
        /// 電子信箱
        /// </summary>
        public virtual string RNMail { get; set; }

        /// <summary>
        /// 完整戶籍地址
        /// </summary>
        public virtual string RNAdd { get; set; }

        /// <summary>
        /// 戶籍地址_縣/市
        /// </summary>
        public virtual string RNAdd_1 { get; set; }

        /// <summary>
        /// 戶籍地址_縣
        /// </summary>
        public virtual string RNAdd_1_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string RNAdd_1_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉/鎮/市/區
        /// </summary>
        public virtual string RNAdd_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鄉
        /// </summary>
        public virtual string RNAdd_2_1 { get; set; }

        /// <summary>
        /// 戶籍地址_市
        /// </summary>
        public virtual string RNAdd_2_2 { get; set; }

        /// <summary>
        /// 戶籍地址_鎮
        /// </summary>
        public virtual string RNAdd_2_3 { get; set; }

        /// <summary>
        /// 戶籍地址_區
        /// </summary>
        public virtual string RNAdd_2_4 { get; set; }

        /// <summary>
        /// 戶籍地址_街/路
        /// </summary>
        public virtual string RNAdd_3 { get; set; }

        /// <summary>
        /// 戶籍地址_街
        /// </summary>
        public virtual string RNAdd_3_1 { get; set; }

        /// <summary>
        /// 戶籍地址_路
        /// </summary>
        public virtual string RNAdd_3_2 { get; set; }

        /// <summary>
        /// 戶籍地址_段
        /// </summary>
        public virtual string RNAdd_4 { get; set; }

        /// <summary>
        /// 戶籍地址_巷
        /// </summary>
        public virtual string RNAdd_5 { get; set; }

        /// <summary>
        /// 戶籍地址_弄
        /// </summary>
        public virtual string RNAdd_6 { get; set; }

        /// <summary>
        /// 戶籍地址_號
        /// </summary>
        public virtual string RNAdd_7 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string RNAdd_8 { get; set; }

        /// <summary>
        /// 戶籍地址_樓
        /// </summary>
        public virtual string RNAdd_9 { get; set; }

        /// <summary>
        /// 地址_同戶籍地址
        /// </summary>
        public virtual string RNAddSame { get; set; }

        /// <summary>
        /// 完整通訊地址
        /// </summary>
        public virtual string RNAddC { get; set; }

        /// <summary>
        /// 通訊地址_縣/市
        /// </summary>
        public virtual string RNAddC_1 { get; set; }

        /// <summary>
        /// 通訊地址_縣
        /// </summary>
        public virtual string RNAddC_1_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string RNAddC_1_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉/鎮/市/區
        /// </summary>
        public virtual string RNAddC_2 { get; set; }

        /// <summary>
        /// 通訊地址_鄉
        /// </summary>
        public virtual string RNAddC_2_1 { get; set; }

        /// <summary>
        /// 通訊地址_市
        /// </summary>
        public virtual string RNAddC_2_2 { get; set; }

        /// <summary>
        /// 通訊地址_鎮
        /// </summary>
        public virtual string RNAddC_2_3 { get; set; }

        /// <summary>
        /// 通訊地址_區
        /// </summary>
        public virtual string RNAddC_2_4 { get; set; }

        /// <summary>
        /// 通訊地址_街/路
        /// </summary>
        public virtual string RNAddC_3 { get; set; }

        /// <summary>
        /// 通訊地址_街
        /// </summary>
        public virtual string RNAddC_3_1 { get; set; }

        /// <summary>
        /// 通訊地址_路
        /// </summary>
        public virtual string RNAddC_3_2 { get; set; }

        /// <summary>
        /// 通訊地址_段
        /// </summary>
        public virtual string RNAddC_4 { get; set; }

        /// <summary>
        /// 通訊地址_巷
        /// </summary>
        public virtual string RNAddC_5 { get; set; }

        /// <summary>
        /// 通訊地址_弄
        /// </summary>
        public virtual string RNAddC_6 { get; set; }

        /// <summary>
        /// 通訊地址_號
        /// </summary>
        public virtual string RNAddC_7 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string RNAddC_8 { get; set; }

        /// <summary>
        /// 通訊地址_樓
        /// </summary>
        public virtual string RNAddC_9 { get; set; }

        /// <summary>
        /// 一般戶
        /// </summary>
        public virtual string RNQualify1C { get; set; }

        /// <summary>
        /// 第一類弱勢戶
        /// </summary>
        public virtual string RNQualify2C { get; set; }

        /// <summary>
        /// 第二類弱勢戶
        /// </summary>
        public virtual string RNQualify3C { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第一碼
        /// </summary>
        public virtual string RNMAccount_1 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第二碼
        /// </summary>
        public virtual string RNMAccount_2 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第三碼
        /// </summary>
        public virtual string RNMAccount_3 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第四碼
        /// </summary>
        public virtual string RNMAccount_4 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第五碼
        /// </summary>
        public virtual string RNMAccount_5 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第六碼
        /// </summary>
        public virtual string RNMAccount_6 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第七碼
        /// </summary>
        public virtual string RNMAccount_7 { get; set; }

        /// <summary>
        /// 申請人戶口名簿戶號_第八碼
        /// </summary>
        public virtual string RNMAccount_8 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號
        /// </summary>
        public virtual string RNMMAccount { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第一碼
        /// </summary>
        public virtual string RNMMAccount_1 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第二碼
        /// </summary>
        public virtual string RNMMAccount_2 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第三碼
        /// </summary>
        public virtual string RNMMAccount_3 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第四碼
        /// </summary>
        public virtual string RNMMAccount_4 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第五碼
        /// </summary>
        public virtual string RNMMAccount_5 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第六碼
        /// </summary>
        public virtual string RNMMAccount_6 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第七碼
        /// </summary>
        public virtual string RNMMAccount_7 { get; set; }

        /// <summary>
        /// 配偶戶口名簿戶號_第八碼
        /// </summary>
        public virtual string RNMMAccount_8 { get; set; }

        /// <summary>
        /// 子女人數
        /// </summary>
        public virtual string RNChild { get; set; }

        /// <summary>
        /// 家庭成員1_姓名
        /// </summary>
        public virtual string RNFName_A { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_A { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_A_1 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_A_2 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_A_3 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_A_4 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_A_5 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_A_6 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_A_7 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_A_8 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_A_9 { get; set; }

        /// <summary>
        /// 家庭成員1_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_A_10 { get; set; }

        /// <summary>
        /// 家庭成員1_生日
        /// </summary>
        public virtual string RNFBirthday_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_A { get; set; }

        /// <summary>
        /// 家庭成員1_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_A { get; set; }

        /// <summary>
        /// 家庭成員1_稱謂
        /// </summary>
        public virtual string RNFTitle_A { get; set; }

        /// <summary>
        /// 家庭成員1_年所得
        /// </summary>
        public virtual string RNFIncome_A { get; set; }

        /// <summary>
        /// 家庭成員2_姓名
        /// </summary>
        public virtual string RNFName_B { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_B { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_B_1 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_B_2 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_B_3 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_B_4 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_B_5 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_B_6 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_B_7 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_B_8 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_B_9 { get; set; }

        /// <summary>
        /// 家庭成員2_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_B_10 { get; set; }

        /// <summary>
        /// 家庭成員2_生日
        /// </summary>
        public virtual string RNFBirthday_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_B { get; set; }

        /// <summary>
        /// 家庭成員2_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_B { get; set; }

        /// <summary>
        /// 家庭成員2_稱謂
        /// </summary>
        public virtual string RNFTitle_B { get; set; }

        /// <summary>
        /// 家庭成員2_年所得
        /// </summary>
        public virtual string RNFIncome_B { get; set; }

        /// <summary>
        /// 家庭成員3_姓名
        /// </summary>
        public virtual string RNFName_C { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號
        /// </summary>
        public virtual string RNFID_1_C { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_C_1 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_C_2 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_C_3 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_C_4 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_C_5 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_C_6 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_C_7 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_C_8 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_C_9 { get; set; }

        /// <summary>
        /// 家庭成員3_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_C_10 { get; set; }

        /// <summary>
        /// 家庭成員3_生日
        /// </summary>
        public virtual string RNFBirthday_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_C { get; set; }

        /// <summary>
        /// 家庭成員3_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_C { get; set; }

        /// <summary>
        /// 家庭成員3_稱謂
        /// </summary>
        public virtual string RNFTitle_C { get; set; }

        /// <summary>
        /// 家庭成員3_年所得
        /// </summary>
        public virtual string RNFIncome_C { get; set; }

        /// <summary>
        /// 家戶年所得
        /// </summary>
        public virtual string RNFTSalary { get; set; }

        /// <summary>
        /// 家庭成員月收入
        /// </summary>
        public virtual string RNFMSalary { get; set; }

        /// <summary>
        /// 緊急聯絡人姓名
        /// </summary>
        public virtual string RNECName { get; set; }

        /// <summary>
        /// 與緊急聯絡人關係
        /// </summary>
        public virtual string RNECRelation { get; set; }

        /// <summary>
        /// 緊急聯絡人手機
        /// </summary>
        public virtual string RNECCell { get; set; }

        /// <summary>
        /// 緊急聯絡人電話
        /// </summary>
        public virtual string RNECTel_1 { get; set; }
        public virtual string RNECTel_2 { get; set; }

        /// <summary>
        /// 緊急聯絡人戶籍地址
        /// </summary>
        public virtual string RNECAdd { get; set; }
        public virtual string RNECAdd_1 { get; set; }
        public virtual string RNECAdd_1_1 { get; set; }
        public virtual string RNECAdd_1_2 { get; set; }
        public virtual string RNECAdd_2 { get; set; }
        public virtual string RNECAdd_2_1 { get; set; }
        public virtual string RNECAdd_2_2 { get; set; }
        public virtual string RNECAdd_2_3 { get; set; }
        public virtual string RNECAdd_2_4 { get; set; }
        public virtual string RNECAdd_3 { get; set; }
        public virtual string RNECAdd_3_1 { get; set; }
        public virtual string RNECAdd_3_2 { get; set; }
        public virtual string RNECAdd_4 { get; set; }
        public virtual string RNECAdd_5 { get; set; }
        public virtual string RNECAdd_6 { get; set; }
        public virtual string RNECAdd_7 { get; set; }
        public virtual string RNECAdd_8 { get; set; }
        public virtual string RNECAdd_9 { get; set; }

        public virtual string RNECAddSame { get; set; }
        /// <summary>
        /// 緊急聯絡人通訊地址
        /// </summary>
        public virtual string RNECAddC { get; set; }
        public virtual string RNECAddC_1 { get; set; }
        public virtual string RNECAddC_1_1 { get; set; }
        public virtual string RNECAddC_1_2 { get; set; }
        public virtual string RNECAddC_2 { get; set; }
        public virtual string RNECAddC_2_1 { get; set; }
        public virtual string RNECAddC_2_2 { get; set; }
        public virtual string RNECAddC_2_3 { get; set; }
        public virtual string RNECAddC_2_4 { get; set; }
        public virtual string RNECAddC_3 { get; set; }
        public virtual string RNECAddC_3_1 { get; set; }
        public virtual string RNECAddC_3_2 { get; set; }
        public virtual string RNECAddC_4 { get; set; }
        public virtual string RNECAddC_5 { get; set; }
        public virtual string RNECAddC_6 { get; set; }
        public virtual string RNECAddC_7 { get; set; }
        public virtual string RNECAddC_8 { get; set; }
        public virtual string RNECAddC_9 { get; set; }

        /// <summary>
        /// 代理人姓名
        /// </summary>
        public virtual string RNAGName_A { get; set; }

        /// <summary>
        /// 代理人身份證字號/居留證號
        /// </summary>
        public virtual string RNAGID_A { get; set; }

        /// <summary>
        /// 代理人生日
        /// </summary>
        public virtual string RNAGBirthday { get; set; }

        /// <summary>
        /// 代理人地址
        /// </summary>
        public virtual string RNAGAdd { get; set; }
        public virtual string RNAGAdd_1_A { get; set; }
        public virtual string RNAGAdd_1_1_A { get; set; }
        public virtual string RNAGAdd_1_2_A { get; set; }
        public virtual string RNAGAdd_2_A { get; set; }
        public virtual string RNAGAdd_2_1_A { get; set; }
        public virtual string RNAGAdd_2_2_A { get; set; }
        public virtual string RNAGAdd_2_3_A { get; set; }
        public virtual string RNAGAdd_2_4_A { get; set; }
        public virtual string RNAGAdd_3_A { get; set; }
        public virtual string RNAGAdd_3_1_A { get; set; }
        public virtual string RNAGAdd_3_2_A { get; set; }
        public virtual string RNAGAdd_4_A { get; set; }
        public virtual string RNAGAdd_5_A { get; set; }
        public virtual string RNAGAdd_6_A { get; set; }
        public virtual string RNAGAdd_7_A { get; set; }
        public virtual string RNAGAdd_8_A { get; set; }
        public virtual string RNAGAdd_9_A { get; set; }
        public virtual string RNAGAddSame { get; set; }
        public virtual string RNAGAddC { get; set; }
        public virtual string RNAGAddC_1_A { get; set; }
        public virtual string RNAGAddC_1_1_A { get; set; }
        public virtual string RNAGAddC_1_2_A { get; set; }
        public virtual string RNAGAddC_2_A { get; set; }
        public virtual string RNAGAddC_2_1_A { get; set; }
        public virtual string RNAGAddC_2_2_A { get; set; }
        public virtual string RNAGAddC_2_3_A { get; set; }
        public virtual string RNAGAddC_2_4_A { get; set; }
        public virtual string RNAGAddC_3_A { get; set; }
        public virtual string RNAGAddC_3_1_A { get; set; }
        public virtual string RNAGAddC_3_2_A { get; set; }
        public virtual string RNAGAddC_4_A { get; set; }
        public virtual string RNAGAddC_5_A { get; set; }
        public virtual string RNAGAddC_6_A { get; set; }
        public virtual string RNAGAddC_7_A { get; set; }
        public virtual string RNAGAddC_8_A { get; set; }
        public virtual string RNAGAddC_9_A { get; set; }

        /// <summary>
        /// 代理人手機
        /// </summary>
        public virtual string RNAGCell_A { get; set; }

        /// <summary>
        /// 代理人電話
        /// </summary>
        public virtual string RNAGTel_1_A { get; set; }
        public virtual string RNAGTel_2_A { get; set; }


        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        public string CreatorUserOrgId { get; set; }
        public string CreatorUserDeptId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除標志
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteUserId { get; set; }

        #region B030401

        /// <summary>
        /// 設籍於該市居民，有居住事實，或有就學就業需求者
        /// </summary>
        public virtual string RNQualify1C_1 { get; set; }

        /// <summary>
        /// 現任職務之最高職務列等在警正四階以下或相當職務列等之基層警察及消防人員
        /// </summary>
        public virtual string RNQualify1C_2 { get; set; }

        /// <summary>
        /// 申請人或家庭成員為身心障礙者或 65 歲以上老人，申請換居並將自RNQualify2C_1
        /// 有住宅出租。(需同時檢附該申請人之表單 1 出租人出租住宅申請書影本)
        /// </summary>
        public virtual string RNQualify1C_3 { get; set; }

        /// <summary>
        /// 六十五歲以上之老人
        /// </summary>
        public virtual string RNQualify2C_1 { get; set; }

        /// <summary>
        /// 原住民
        /// </summary>
        public virtual string RNQualify2C_2 { get; set; }

        /// <summary>
        /// 育有未成年(20 歲以下)子女三人以上
        /// </summary>
        public virtual string RNQualify2C_3 { get; set; }

        /// <summary>
        /// 特殊境遇家庭
        /// </summary>
        public virtual string RNQualify2C_4 { get; set; }

        /// <summary>
        /// □於安置教養機構或寄養家庭結束安置無法返家，未滿二十五歲。
        /// </summary>
        public virtual string RNQualify2C_5 { get; set; }

        /// <summary>
        /// □受家庭暴力或性侵害之受害者及其子女。
        /// </summary>
        public virtual string RNQualify2C_6 { get; set; }

        /// <summary>
        /// □身心障礙者。
        /// </summary>
        public virtual string RNQualify2C_7 { get; set; }

        /// <summary>
        /// □感染人類免疫缺乏病毒者或罹患後天免疫缺乏症候群者。
        /// </summary>
        public virtual string RNQualify2C_8 { get; set; }

        /// <summary>
        /// □災民。
        /// </summary>
        public virtual string RNQualify2C_9 { get; set; }

        /// <summary>
        /// □遊民。
        /// </summary>
        public virtual string RNQualify2C_10 { get; set; }

        /// <summary>
        /// □因懷孕或生育而遭遇困境之未成年
        /// </summary>
        public virtual string RNQualify2C_11 { get; set; }


        /// <summary>
        /// □低收入戶。
        /// </summary>
        public virtual string RNQualify3C_1 { get; set; }

        /// <summary>
        /// □中低收入戶。RNFName_C
        /// </summary>
        public virtual string RNQualify3C_2 { get; set; }

        #endregion B030401

        #region B030601

        /// <summary>
        /// 家庭成員4_姓名
        /// </summary>
        public virtual string RNFName_D { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_D_1 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_D_2 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_D_3 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_D_4 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_D_5 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_D_6 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_D_7 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_D_8 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_D_9 { get; set; }

        /// <summary>
        /// 家庭成員4_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_D_10 { get; set; }

        /// <summary>
        /// 家庭成員4_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_D { get; set; }

        /// <summary>
        /// 家庭成員4_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_D { get; set; }

        /// <summary>
        /// 家庭成員4_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_D { get; set; }

        /// <summary>
        /// 家庭成員5_姓名
        /// </summary>
        public virtual string RNFName_E { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_E_1 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_E_2 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_E_3 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_E_4 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_E_5 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_E_6 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_E_7 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_E_8 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_E_9 { get; set; }

        /// <summary>
        /// 家庭成員5_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_E_10 { get; set; }

        /// <summary>
        /// 家庭成員5_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_E { get; set; }

        /// <summary>
        /// 家庭成員5_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_E { get; set; }

        /// <summary>
        /// 家庭成員5_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_E { get; set; }


        /// <summary>
        /// 家庭成員6_姓名
        /// </summary>
        public virtual string RNFName_F { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_F_1 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_F_2 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_F_3 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_F_4 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_F_5 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_F_6 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_F_7 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_F_8 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_F_9 { get; set; }

        /// <summary>
        /// 家庭成員6_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_F_10 { get; set; }

        /// <summary>
        /// 家庭成員6_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_F { get; set; }

        /// <summary>
        /// 家庭成員6_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_F { get; set; }

        /// <summary>
        /// 家庭成員6_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_F { get; set; }

        /// <summary>
        /// 家庭成員7_姓名
        /// </summary>
        public virtual string RNFName_G { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第一碼
        /// </summary>
        public virtual string RNFID_1_G_1 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第二碼
        /// </summary>
        public virtual string RNFID_1_G_2 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第三碼
        /// </summary>
        public virtual string RNFID_1_G_3 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第四碼
        /// </summary>
        public virtual string RNFID_1_G_4 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第五碼
        /// </summary>
        public virtual string RNFID_1_G_5 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第六碼
        /// </summary>
        public virtual string RNFID_1_G_6 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第七碼
        /// </summary>
        public virtual string RNFID_1_G_7 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第八碼
        /// </summary>
        public virtual string RNFID_1_G_8 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第九碼
        /// </summary>
        public virtual string RNFID_1_G_9 { get; set; }

        /// <summary>
        /// 家庭成員7_身份證號/居留證號_第十碼
        /// </summary>
        public virtual string RNFID_1_G_10 { get; set; }

        /// <summary>
        /// 家庭成員7_生日_年
        /// </summary>
        public virtual string RNFBirthday_Y_G { get; set; }

        /// <summary>
        /// 家庭成員7_生日_月
        /// </summary>
        public virtual string RNFBirthday_M_G { get; set; }

        /// <summary>
        /// 家庭成員7_生日_日
        /// </summary>
        public virtual string RNFBirthday_D_G { get; set; }

        ///// <summary>
        ///// 家庭成員8_姓名
        ///// </summary>
        //public virtual string RNFName_H { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第一碼
        ///// </summary>
        //public virtual string RNFID_1_H_1 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第二碼
        ///// </summary>
        //public virtual string RNFID_1_H_2 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第三碼
        ///// </summary>
        //public virtual string RNFID_1_H_3 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第四碼
        ///// </summary>
        //public virtual string RNFID_1_H_4 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第五碼
        ///// </summary>
        //public virtual string RNFID_1_H_5 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第六碼
        ///// </summary>
        //public virtual string RNFID_1_H_6 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第七碼
        ///// </summary>
        //public virtual string RNFID_1_H_7 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第八碼
        ///// </summary>
        //public virtual string RNFID_1_H_8 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第九碼
        ///// </summary>
        //public virtual string RNFID_1_H_9 { get; set; }

        ///// <summary>
        ///// 家庭成員8_身份證號/居留證號_第十碼
        ///// </summary>
        //public virtual string RNFID_1_H_10 { get; set; }

        ///// <summary>
        ///// 家庭成員8_生日_年
        ///// </summary>
        //public virtual string RNFBirthday_Y_H { get; set; }

        ///// <summary>
        ///// 家庭成員8_生日_月
        ///// </summary>
        //public virtual string RNFBirthday_M_H { get; set; }

        ///// <summary>
        ///// 家庭成員8_生日_日
        ///// </summary>
        //public virtual string RNFBirthday_D_H { get; set; }

        #endregion B030601

        #region B030701

        /// <summary>
        /// 租金補助
        /// </summary>
        public virtual string RNRentSUB { get; set; }

        /// <summary>
        /// 租金補助 申請金額 新台幣
        /// </summary>
        public virtual string RNRentSUBAFee { get; set; }

        /// <summary>
        /// 簽 約 租 金  □無共住戶，簽約租金為新臺幣
        /// </summary>
        public virtual string RNRentSUBNoFee { get; set; }

        /// <summary>
        /// 簽 約 租 金  有共住戶，申請人負擔之租金為新臺幣
        /// </summary>
        public virtual string RNRentSUBYesFee { get; set; }

        /// <summary>
        /// 公證費 申請金額 新台幣
        /// </summary>
        public virtual string RNSFAFee { get; set; }

        /// <summary>
        /// 公證費
        /// </summary>
        public virtual string RNSFee { get; set; }

        /// <summary>
        /// 公證費 實際支付金額 新臺幣
        /// </summary>
        public virtual string RNSFRFee { get; set; }
        #endregion B030701

        #region 0726MM新增保證人欄位

        public virtual string RNGName { get; set; }
        public virtual string RNGID { get; set; }
        public virtual string RNGAdd { get; set; }
        public virtual string RNGAdd_1 { get; set; }
        public virtual string RNGAdd_1_1 { get; set; }
        public virtual string RNGAdd_1_2 { get; set; }
        public virtual string RNGAdd_2 { get; set; }
        public virtual string RNGAdd_2_1 { get; set; }
        public virtual string RNGAdd_2_2 { get; set; }
        public virtual string RNGAdd_2_3 { get; set; }
        public virtual string RNGAdd_2_4 { get; set; }
        public virtual string RNGAdd_3 { get; set; }
        public virtual string RNGAdd_3_1 { get; set; }
        public virtual string RNGAdd_3_2 { get; set; }
        public virtual string RNGAdd_4 { get; set; }
        public virtual string RNGAdd_5 { get; set; }
        public virtual string RNGAdd_6 { get; set; }
        public virtual string RNGAdd_7 { get; set; }
        public virtual string RNGAdd_8 { get; set; }
        public virtual string RNGAdd_9 { get; set; }
        public virtual string RNGAddSame { get; set; }
        public virtual string RNGCAdd { get; set; }
        public virtual string RNGCAdd_1 { get; set; }
        public virtual string RNGCAdd_1_1 { get; set; }
        public virtual string RNGCAdd_1_2 { get; set; }
        public virtual string RNGCAdd_2 { get; set; }
        public virtual string RNGCAdd_2_1 { get; set; }
        public virtual string RNGCAdd_2_2 { get; set; }
        public virtual string RNGCAdd_2_3 { get; set; }
        public virtual string RNGCAdd_2_4 { get; set; }
        public virtual string RNGCAdd_3 { get; set; }
        public virtual string RNGCAdd_3_1 { get; set; }
        public virtual string RNGCAdd_3_2 { get; set; }
        public virtual string RNGCAdd_4 { get; set; }
        public virtual string RNGCAdd_5 { get; set; }
        public virtual string RNGCAdd_6 { get; set; }
        public virtual string RNGCAdd_7 { get; set; }
        public virtual string RNGCAdd_8 { get; set; }
        public virtual string RNGCAdd_9 { get; set; }
        public virtual string RNGCell { get; set; }
        public virtual string RNGMail { get; set; }

        #endregion 0726MM新增保證人欄位

        #region C02030201

        public virtual string RNFID_1_D { get; set; }
        public virtual string RNFBirthday_D { get; set; }
        public virtual string RNFID_1_E { get; set; }
        public virtual string RNFBirthday_E { get; set; }
        public virtual string RNFID_1_F { get; set; }
        public virtual string RNFBirthday_F { get; set; }
        public virtual string RNFID_1_G { get; set; }
        public virtual string RNFBirthday_G { get; set; }
        #endregion C02030201

        #region 8/17 MM 欄位修改0817.docx

        /// <summary>
        /// 緊急連絡人身分證字號
        /// </summary>
        public virtual string RNECID { get; set; }

        /// <summary>
        /// 緊急連絡人生日
        /// </summary>
        public virtual string RNECBirthday { get; set; }

        /// <summary>
        /// 保證人生日
        /// </summary>
        public virtual string RNGBirthday { get; set; }

        /// <summary>
        /// 未滿18歲之未成年人
        /// </summary>
        public virtual string RNQualify3C_12 { get; set; }

        /// <summary>
        /// 申請人稱謂(值一定為"本人")
        /// </summary>
        public virtual string RNFRelation { get; set; }

        /// <summary>
        /// 同住人A稱謂
        /// </summary>
        public virtual string RNFRelation_A { get; set; }

        /// <summary>
        /// 同住人A手機
        /// </summary>
        public virtual string RNFCell_A { get; set; }

        /// <summary>
        /// 同住人B稱謂
        /// </summary>
        public virtual string RNFRelation_B { get; set; }

        /// <summary>
        /// 同住人B手機
        /// </summary>
        public virtual string RNFCell_B { get; set; }

        /// <summary>
        /// 同住人C稱謂
        /// </summary>
        public virtual string RNFRelation_C { get; set; }

        /// <summary>
        /// 同住人C手機
        /// </summary>
        public virtual string RNFCell_C { get; set; }

        /// <summary>
        /// 同住人D稱謂
        /// </summary>
        public virtual string RNFRelation_D { get; set; }

        /// <summary>
        /// 同住人D手機
        /// </summary>
        public virtual string RNFCell_D { get; set; }

        /// <summary>
        /// 同住人E稱謂
        /// </summary>
        public virtual string RNFRelation_E { get; set; }

        /// <summary>
        /// 同住人E手機
        /// </summary>
        public virtual string RNFCell_E { get; set; }

        /// <summary>
        /// 同住人F稱謂
        /// </summary>
        public virtual string RNFRelation_F { get; set; }

        /// <summary>
        /// 同住人F手機
        /// </summary>
        public virtual string RNFCell_F { get; set; }

        /// <summary>
        /// 同住人G稱謂
        /// </summary>
        public virtual string RNFRelation_G { get; set; }

        /// <summary>
        /// 同住人G手機
        /// </summary>
        public virtual string RNFCell_G { get; set; }

        #endregion 8/17 MM 欄位修改0817.docx

        #endregion

    }
}