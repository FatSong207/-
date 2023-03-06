using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_CustomerRC")]
    [Serializable]
    public class CustomerRC:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 公司名稱(中文)
        /// </summary>
        public string RCName { get; set; }
        /// <summary>
        /// 公司名稱(英文)
        /// </summary>
        public string RCEngName { get; set; }
        /// <summary>
        /// 負責人
        /// </summary>
        public string RCRep { get; set; }
        /// <summary>
        /// 統一編號
        /// </summary>
        public string RCID { get; set; }
        /// <summary>
        /// 統一編號_1
        /// </summary>
        public string RCID_1_1 { get; set; }
        /// <summary>
        /// --統一編號_2
        /// </summary>
        public string RCID_1_2 { get; set; }
        /// <summary>
        /// 統一編號_3
        /// </summary>
        public string RCID_1_3 { get; set; }
        /// <summary>
        /// 統一編號_4
        /// </summary>
        public string RCID_1_4 { get; set; }
        /// <summary>
        /// -統一編號_5
        /// </summary>
        public string RCID_1_5 { get; set; }
        /// <summary>
        /// 統一編號_6
        /// </summary>
        public string RCID_1_6 { get; set; }
        /// <summary>
        /// 統一編號_7
        /// </summary>
        public string RCID_1_7 { get; set; }
        /// <summary>
        /// 統一編號_8
        /// </summary>
        public string RCID_1_8 { get; set; }
        /// <summary>
        /// 電話(完整)
        /// </summary>
        public string RCTel { get; set; }
        /// <summary>
        /// 電話(區號)
        /// </summary>
        public string RCTel_1 { get; set; }
        /// <summary>
        /// 電話(號碼)
        /// </summary>
        public string RCTel_2 { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string RCCell { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string RCMail { get; set; }
        /// <summary>
        /// 登記地址
        /// </summary>
        public string RCAdd { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAdd_9 { get; set; }
        /// <summary>
        /// 營業地址同登記地址
        /// </summary>
        public string RCAddSame { get; set; }
        /// <summary>
        /// 通訊地址
        /// </summary>
        public string RCAddC { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAddC_9 { get; set; }
        /// <summary>
        /// 稱謂 一定是本人
        /// </summary>
        public string RCFRelation { get; set; }
        /// <summary>
        /// 同住者A_姓名
        /// </summary>
        public string RCFName_A { get; set; }
        /// <summary>
        /// 同住者A_稱謂
        /// </summary>
        public string RCFRelation_A { get; set; }
        /// <summary>
        /// 同住者A_連絡電話
        /// </summary>
        public string RCFCell_A { get; set; }
        /// <summary>
        /// 同住者B_姓名
        /// </summary>
        public string RCFName_B { get; set; }
        /// <summary>
        /// 同住者B_稱謂
        /// </summary>
        public string RCFRelation_B { get; set; }
        /// <summary>
        /// 同住者B_連絡電話
        /// </summary>
        public string RCFCell_B { get; set; }
        /// <summary>
        /// 同住者C_姓名
        /// </summary>
        public string RCFName_C { get; set; }
        /// <summary>
        /// 同住者C_稱謂
        /// </summary>
        public string RCFRelation_C { get; set; }
        /// <summary>
        /// 同住者C_連絡電話
        /// </summary>
        public string RCFCell_C { get; set; }
        /// <summary>
        /// 同住者A_身分證字號
        /// </summary>
        public string RCFID_1_A { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_A_10 { get; set; }
        /// <summary>
        /// 同住者A_生日
        /// </summary>
        public string RCFBirthday_A { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_A { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_A { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_A { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string LastModifyUserId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string DeleteUserId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_B_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_B { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_B { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_B { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_B { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_C_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_C { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_C { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_C { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_C { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFName_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_D_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_D { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFName_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_E_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_E { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFName_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_F_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_F { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFName_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFRelation_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFCell_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFID_1_G_10 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_Y_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_M_G { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCFBirthday_D_G { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_姓名
        /// </summary>
        public string RCECName { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_身分證字號
        /// </summary>
        public string RCECID { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)性別_男
        /// </summary>
        public string RCECGender_1 { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)性別_女
        /// </summary>
        public string RCECGender_2 { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_生日
        /// </summary>
        public string RCECBirthday { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_關係
        /// </summary>
        public string RCECRelation { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(手機)
        /// </summary>
        public string RCECCell { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(區號)
        /// </summary>
        public string RCECTel_1 { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_連絡電話(號碼)
        /// </summary>
        public string RCECTel_2 { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_戶籍地址
        /// </summary>
        public string RCECAdd { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_通訊地址同戶籍地址
        /// </summary>
        public string RCECAddSame { get; set; }
        /// <summary>
        /// 緊急聯絡人(親屬)_通訊地址
        /// </summary>
        public string RCECAddC { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAdd_9 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECAddC_9 { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_姓名
        /// </summary>
        public string RCECFName { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_身分證字號
        /// </summary>
        public string RCECFID { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_性別_男
        /// </summary>
        public string RCECFGender_1 { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_性別_女
        /// </summary>
        public string RCECFGender_2 { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_生日
        /// </summary>
        public string RCECFBirthday { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_連絡電話
        /// </summary>
        public string RCECFCell { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_關係
        /// </summary>
        public string RCECFRelation { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_戶籍地址
        /// </summary>
        public string RCECFAdd { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAdd_9 { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_通訊地址同戶籍地址
        /// </summary>
        public string RCECFAddSame { get; set; }
        /// <summary>
        /// 緊急聯絡人(朋友)_通訊地址
        /// </summary>
        public string RCECFAddC { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCECFAddC_9 { get; set; }
        /// <summary>
        /// 簽約代理人_姓名
        /// </summary>
        public string RCAGName { get; set; }
        /// <summary>
        /// 簽約代理人_身分證字號
        /// </summary>
        public string RCAGID { get; set; }
        /// <summary>
        /// 簽約代理人性別_男
        /// </summary>
        public string RCAGGender_1 { get; set; }
        /// <summary>
        /// 簽約代理人性別_女
        /// </summary>
        public string RCAGGender_2 { get; set; }
        /// <summary>
        /// 簽約代理人_生日
        /// </summary>
        public string RCAGBirthday { get; set; }
        /// <summary>
        /// 簽約代理人_戶籍地址
        /// </summary>
        public string RCAGAdd { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAdd_9 { get; set; }
        /// <summary>
        /// 簽約代理人_通訊地址同戶籍地址
        /// </summary>
        public string RCAGAddSame { get; set; }
        /// <summary>
        /// 簽約代理人_通訊地址
        /// </summary>
        public string RCAGAddC { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCAGAddC_9 { get; set; }
        /// <summary>
        /// 簽約代理人_聯絡電話(手機)
        /// </summary>
        public string RCAGCell { get; set; }
        /// <summary>
        /// --簽約代理人_聯絡電話(區號)
        /// </summary>
        public string RCAGTel_1 { get; set; }
        /// <summary>
        /// -簽約代理人_聯絡電話(號碼)
        /// </summary>
        public string RCAGTel_2 { get; set; }
        /// <summary>
        /// 保證人_姓名
        /// </summary>
        public string RCGName { get; set; }
        /// <summary>
        /// 保證人_身分證字號
        /// </summary>
        public string RCGID { get; set; }
        /// <summary>
        /// 保證人性別_男
        /// </summary>
        public string RCGGender_1 { get; set; }
        /// <summary>
        /// 保證人性別_女
        /// </summary>
        public string RCGGender_2 { get; set; }
        /// <summary>
        /// 保證人_生日
        /// </summary>
        public string RCGBirthday { get; set; }
        /// <summary>
        /// 保證人_戶籍地址
        /// </summary>
        public string RCGAdd { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGAdd_9 { get; set; }
        /// <summary>
        /// 保證人_通訊地址同戶籍地址
        /// </summary>
        public string RCGAddSame { get; set; }
        /// <summary>
        /// 保證人_通訊地址
        /// </summary>
        public string RCGCAdd { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_1_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_2_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3_1 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_3_2 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_4 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_5 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_6 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_7 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_8 { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string RCGCAdd_9 { get; set; }
        /// <summary>
        /// 保證人_聯絡電話
        /// </summary>
        public string RCGCell { get; set; }

        /// <summary>
        /// 保證人_關係
        /// </summary>
        public string RCGRelation { get; set; }

    }
}
