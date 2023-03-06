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
    [Table("Chaochi_BlackList")]
    [Serializable]
    public class BlackList:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取身分證字號/居留證號
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 設置或獲取生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 身分別
        /// </summary>
        public string IDentity { get; set; }
        /// <summary>
        /// 設置或獲取年齡
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 設置或獲取回報者
        /// </summary>
        public string Reporter { get; set; }
        /// <summary>
        /// 設置或獲取名單來源
        /// </summary>
        public string ReportDept { get; set; }
        /// <summary>
        /// 設置或獲取當前狀態
        /// </summary>
        public string ResultState { get; set; }
        /// <summary>
        /// 設置或獲取Bili綜合評分
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// 設置或獲取Bili備註
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取審核說明
        /// </summary>
        public string TrialNote { get; set; }

        /// <summary>
        /// WriteTenantReview此API回傳的Key，日後用來接收對方回傳的審查結果並更新
        /// </summary>
        public int B_Id { get; set; }

        /// <summary>
        /// 刑事通緝犯
        /// </summary>
        public string CriminalWanted { get; set; }

        /// <summary>
        /// 查捕中逃犯
        /// </summary>
        public string CatchingFugitives { get; set; }

        /// <summary>
        /// 查緝專刊
        /// </summary>
        public string CriminalLib { get; set; }

        /// <summary>
        /// /監護補助
        /// </summary>
        public string Domestic { get; set; }

        /// <summary>
        /// /交通罰鍰
        /// </summary>
        public string TraficFines { get; set; }

        /// <summary>
        /// 汽機車燃料稅
        /// </summary>
        public string FuelPenaltyExpireds { get; set; }

        /// <summary>
        /// 消債事件
        /// </summary>
        public string ConsumerDebt { get; set; }

        /// <summary>
        /// 租金補貼
        /// </summary>
        public string RentSubsidy { get; set; }

        /// <summary>
        /// 法源網
        /// </summary>
        public string LawBank { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
