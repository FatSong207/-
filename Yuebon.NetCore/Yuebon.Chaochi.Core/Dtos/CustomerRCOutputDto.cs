using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class CustomerRCOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱(中文)
        /// </summary>
        [MaxLength(40)]
        public string RCName { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱(英文)
        /// </summary>
        [MaxLength(100)]
        public string RCEngName { get; set; }
        /// <summary>
        /// 設置或獲取負責人
        /// </summary>
        [MaxLength(40)]
        public string RCRep { get; set; }
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        [MaxLength(16)]
        public string RCID { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_1
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取--統一編號_2
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_3
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_3 { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_4
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_4 { get; set; }
        /// <summary>
        /// 設置或獲取-統一編號_5
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_5 { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_6
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_6 { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_7
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_7 { get; set; }
        /// <summary>
        /// 設置或獲取統一編號_8
        /// </summary>
        [MaxLength(2)]
        public string RCID_1_8 { get; set; }
        /// <summary>
        /// 設置或獲取電話(完整)
        /// </summary>
        [MaxLength(100)]
        public string RCTel { get; set; }
        /// <summary>
        /// 設置或獲取電話(區號)
        /// </summary>
        [MaxLength(100)]
        public string RCTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取電話(號碼)
        /// </summary>
        [MaxLength(100)]
        public string RCTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取手機
        /// </summary>
        [MaxLength(100)]
        public string RCCell { get; set; }
        /// <summary>
        /// 設置或獲取信箱
        /// </summary>
        [MaxLength(100)]
        public string RCMail { get; set; }
        /// <summary>
        /// 設置或獲取登記地址
        /// </summary>
        [MaxLength(100)]
        public string RCAdd { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取營業地址同登記地址
        /// </summary>
        [MaxLength(100)]
        public string RCAddSame { get; set; }
        /// <summary>
        /// 設置或獲取通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RCAddC { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取稱謂 一定是本人
        /// </summary>
        [MaxLength(100)]
        public string RCFRelation { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_姓名
        /// </summary>
        [MaxLength(100)]
        public string RCECName { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_身分證字號
        /// </summary>
        [MaxLength(100)]
        public string RCECID { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_男
        /// </summary>
        [MaxLength(10)]
        public string RCECGender_1 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)性別_女
        /// </summary>
        [MaxLength(10)]
        public string RCECGender_2 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_生日
        /// </summary>
        [MaxLength(100)]
        public string RCECBirthday { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_關係
        /// </summary>
        [MaxLength(100)]
        public string RCECRelation { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(手機)
        /// </summary>
        [MaxLength(100)]
        public string RCECCell { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(區號)
        /// </summary>
        [MaxLength(100)]
        public string RCECTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_連絡電話(號碼)
        /// </summary>
        [MaxLength(100)]
        public string RCECTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址同戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCECAddSame { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(親屬)_通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCECAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_姓名
        /// </summary>
        [MaxLength(100)]
        public string RCECFName { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_身分證字號
        /// </summary>
        [MaxLength(100)]
        public string RCECFID { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_男
        /// </summary>
        [MaxLength(100)]
        public string RCECFGender_1 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_性別_女
        /// </summary>
        [MaxLength(100)]
        public string RCECFGender_2 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_生日
        /// </summary>
        [MaxLength(100)]
        public string RCECFBirthday { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_連絡電話
        /// </summary>
        [MaxLength(100)]
        public string RCECFCell { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_關係
        /// </summary>
        [MaxLength(100)]
        public string RCECFRelation { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCECFAdd { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址同戶籍地址
        /// </summary>
        [MaxLength(10)]
        public string RCECFAddSame { get; set; }
        /// <summary>
        /// 設置或獲取緊急聯絡人(朋友)_通訊地址
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string RCECFAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_姓名
        /// </summary>
        [MaxLength(100)]
        public string RCAGName { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_身分證字號
        /// </summary>
        [MaxLength(100)]
        public string RCAGID { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人性別_男
        /// </summary>
        [MaxLength(10)]
        public string RCAGGender_1 { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人性別_女
        /// </summary>
        [MaxLength(10)]
        public string RCAGGender_2 { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_生日
        /// </summary>
        [MaxLength(100)]
        public string RCAGBirthday { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址同戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddSame { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCAGAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取簽約代理人_聯絡電話(手機)
        /// </summary>
        [MaxLength(100)]
        public string RCAGCell { get; set; }
        /// <summary>
        /// 設置或獲取--簽約代理人_聯絡電話(區號)
        /// </summary>
        [MaxLength(100)]
        public string RCAGTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取-簽約代理人_聯絡電話(號碼)
        /// </summary>
        [MaxLength(100)]
        public string RCAGTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取保證人_姓名
        /// </summary>
        [MaxLength(100)]
        public string RCGName { get; set; }
        /// <summary>
        /// 設置或獲取保證人_身分證字號
        /// </summary>
        [MaxLength(100)]
        public string RCGID { get; set; }
        /// <summary>
        /// 設置或獲取保證人性別_男
        /// </summary>
        [MaxLength(10)]
        public string RCGGender_1 { get; set; }
        /// <summary>
        /// 設置或獲取保證人性別_女
        /// </summary>
        [MaxLength(10)]
        public string RCGGender_2 { get; set; }
        /// <summary>
        /// 設置或獲取保證人_生日
        /// </summary>
        [MaxLength(100)]
        public string RCGBirthday { get; set; }
        /// <summary>
        /// 設置或獲取保證人_戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取保證人_通訊地址同戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RCGAddSame { get; set; }
        /// <summary>
        /// 設置或獲取保證人_通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string RCGCAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取保證人_聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string RCGCell { get; set; }

        /// <summary>
        /// 保證人_關係
        /// </summary>
        public string RCGRelation { get; set; }        public RCFOutputDto  RCFOutputDto { get; set; }        public string CreatorUserName { get; set; }
    }
}
