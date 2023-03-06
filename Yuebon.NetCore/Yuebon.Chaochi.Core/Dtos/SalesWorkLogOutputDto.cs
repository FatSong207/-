using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 業務工作日誌輸出對象模型
    /// </summary>
    [Serializable]
    public class SalesWorkLogOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取隸屬店
        /// </summary>
        [MaxLength(100)]
        public string BelongDept { get; set; }

        /// <summary>
        /// 設置或獲取隸屬店名
        /// </summary>
        [MaxLength(100)]
        public string BelongDeptName { get; set; }        /// <summary>
        /// 設置或獲取業務姓名
        /// </summary>
        [MaxLength(100)]
        public string SalesName { get; set; }
        /// <summary>
        /// 設置或獲取業務帳號
        /// </summary>
        [MaxLength(100)]
        public string SalesAccount { get; set; }
        /// <summary>
        /// 設置或獲取日誌日期
        /// </summary>
        [MaxLength(100)]
        public string LogDate { get; set; }
        /// <summary>
        /// 設置或獲取審核主管
        /// </summary>
        [MaxLength(100)]
        public string AuditSupervisor { get; set; }

        /// <summary>
        /// 設置或獲取審核主管姓名
        /// </summary>
        [MaxLength(100)]
        public string AuditSupervisorName { get; set; }        /// <summary>
        /// 設置或獲取審核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// 設置或獲取店長備註
        /// </summary>
        [MaxLength(1000)]
        public string StoreManagerNote { get; set; }
        /// <summary>
        /// 設置或獲取聯繫開發
        /// </summary>
        public int? ContactDevelope { get; set; }
        /// <summary>
        /// 設置或獲取招租來電
        /// </summary>
        public int? CallForRent { get; set; }
        /// <summary>
        /// 設置或獲取房東場勘
        /// </summary>
        public int? LandlordSurvey { get; set; }
        /// <summary>
        /// 設置或獲取房客帶看
        /// </summary>
        public int? RenterSurvey { get; set; }
        /// <summary>
        /// 設置或獲取房東委託
        /// </summary>
        public int? LandlordDelegate { get; set; }
        /// <summary>
        /// 設置或獲取房客收訂
        /// </summary>
        public int? RenterDeposit { get; set; }
        /// <summary>
        /// 設置或獲取維修派工
        /// </summary>
        public int? RepairDispatch { get; set; }
        /// <summary>
        /// 設置或獲取入住點交
        /// </summary>
        public int? CheckIn { get; set; }
        /// <summary>
        /// 設置或獲取催收聯繫
        /// </summary>
        public int? CollectionContact { get; set; }
        /// <summary>
        /// 設置或獲取退租點退
        /// </summary>
        public int? LeaseCheck { get; set; }
        /// <summary>
        /// 設置或獲取清潔維護
        /// </summary>
        public int? Clean { get; set; }
        /// <summary>
        /// 設置或獲取水電抄表
        /// </summary>
        public int? Hydropwer { get; set; }
        /// <summary>
        /// 設置或獲取招租庫存_社會宅
        /// </summary>
        public int? Storage_SH { get; set; }
        /// <summary>
        /// 設置或獲取招租庫存_一般宅
        /// </summary>
        public int? Storage_NH { get; set; }
        /// <summary>
        /// 設置或獲取上一次_房東場勘
        /// </summary>
        public int? Last_LandlordSurvey { get; set; }
        /// <summary>
        /// 設置或獲取上一次_房東委託
        /// </summary>
        public int? Last_LandlordDelegate { get; set; }
        /// <summary>
        /// 設置或獲取上一次_房客帶看
        /// </summary>
        public int? Last_RenterSurvey { get; set; }
        /// <summary>
        /// 設置或獲取上一次_房客收訂
        /// </summary>
        public int? Last_RenterDeposit { get; set; }
        /// <summary>
        /// 設置或獲取上一次_維修派工
        /// </summary>
        public int? Last_RepairDispatch { get; set; }
        /// <summary>
        /// 設置或獲取上一次_入住點交
        /// </summary>
        public int? Last_CheckIn { get; set; }
        /// <summary>
        /// 設置或獲取上一次_催收聯繫
        /// </summary>
        public int? Last_CollectionContact { get; set; }
        /// <summary>
        /// 設置或獲取上一次_退租點退
        /// </summary>
        public int? Last_LeaseCheck { get; set; }
        /// <summary>
        /// 設置或獲取上一次_清潔維護
        /// </summary>
        public int? Last_Clean { get; set; }
        /// <summary>
        /// 設置或獲取上一次_水電抄表
        /// </summary>
        public int? Last_Hydropwer { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_業績額
        /// </summary>
        [MaxLength(100)]
        public string SH_Performance { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_新成交
        /// </summary>
        [MaxLength(100)]
        public string SH_New { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_再媒合
        /// </summary>
        [MaxLength(100)]
        public string SH_Match { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_續約
        /// </summary>
        [MaxLength(100)]
        public string SH_Continue { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_沒定
        /// </summary>
        [MaxLength(100)]
        public string SH_Nothing { get; set; }
        /// <summary>
        /// 設置或獲取社會宅_轉期
        /// </summary>
        [MaxLength(100)]
        public string SH_Transfer { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_業績額
        /// </summary>
        [MaxLength(100)]
        public string NH_Performance { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_新成交
        /// </summary>
        [MaxLength(100)]
        public string NH_New { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_再媒合
        /// </summary>
        [MaxLength(100)]
        public string NH_Match { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_續約
        /// </summary>
        [MaxLength(100)]
        public string NH_Continue { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_沒定
        /// </summary>
        [MaxLength(100)]
        public string NH_Nothing { get; set; }
        /// <summary>
        /// 設置或獲取一般宅_轉期
        /// </summary>
        [MaxLength(100)]
        public string NH_Transfer { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_業績額
        /// </summary>
        [MaxLength(100)]
        public string ST_Performance { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_新成交
        /// </summary>
        [MaxLength(100)]
        public string ST_New { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_再媒合
        /// </summary>
        [MaxLength(100)]
        public string ST_Match { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_續約
        /// </summary>
        [MaxLength(100)]
        public string ST_Continue { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_沒定
        /// </summary>
        [MaxLength(100)]
        public string ST_Nothing { get; set; }
        /// <summary>
        /// 設置或獲取商辦/買賣_轉期
        /// </summary>
        [MaxLength(100)]
        public string ST_Transfer { get; set; }
        /// <summary>
        /// 設置或獲取行程回報
        /// </summary>
        [MaxLength(1000)]
        public string ScheduleReport { get; set; }
        /// <summary>
        /// 設置或獲取代辦工作事項
        /// </summary>
        [MaxLength(1000)]
        public string TodoNote { get; set; }


        /// <summary>
        /// 狀態
        /// </summary>
        public string States { get; set; }
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
        public string CreatorUserDeptId { get; set; }
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
        /// 
        /// </summary>        public List<SalesWorkLogDetails> SalesWorkLogDetails { get; set; }

        /// <summary>
        /// 成交列表
        /// </summary>        public List<Deal> Deals { get; set; }
    }
}
