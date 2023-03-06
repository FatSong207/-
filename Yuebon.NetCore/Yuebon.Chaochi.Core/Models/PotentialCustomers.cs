using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 潛在客管理，數據實體對象
    /// </summary>
    [Table("Chaochi_PotentialCustomers")]
    [Serializable]
    public class PotentialCustomers:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取潛在客編號
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 設置或獲取身份
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 設置或獲取自然人或法人
        /// </summary>
        public string Identity2 { get; set; }
        /// <summary>
        /// 設置或獲取客源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 設置或獲取當前狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取是否結案
        /// </summary>
        public string IsClosed { get; set; }
        /// <summary>
        /// 設置或獲取是否轉正
        /// </summary>
        public string IsTransfer { get; set; }
        /// <summary>
        /// 設置或獲取回報次數
        /// </summary>
        public string ReportBackCounts { get; set; }
        /// <summary>
        /// 設置或獲取指派業務
        /// </summary>
        public string Sales { get; set; }
        /// <summary>
        /// 設置或獲取隸屬區/店
        /// </summary>
        public string Area { get; set; }        /// <summary>
        /// 設置或獲取隸屬區/店_區
        /// </summary>
        public string Area_1 { get; set; }
        /// <summary>
        /// 設置或獲取隸屬區/店_店
        /// </summary>
        public string Area_2 { get; set; }
        /// <summary>
        /// 設置或獲取房屋型態
        /// </summary>
        public string HouseType { get; set; }
        /// <summary>
        /// 設置或獲取建物格局
        /// </summary>
        public string HouseInterior { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-姓名/法人名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話_區碼
        /// </summary>
        public string Tel_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話_號碼
        /// </summary>
        public string Tel_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-手機
        /// </summary>
        public string Cell { get; set; }
        /// <summary>
        /// 設置或獲取所屬縣市
        /// </summary>
        public string CountyCity { get; set; }
        /// <summary>
        /// 設置或獲取承租人-入住人數
        /// </summary>
        public string ResidentCount { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址
        /// </summary>
        public string RAdd { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_縣/市
        /// </summary>
        public string RAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_縣
        /// </summary>
        public string RAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_市
        /// </summary>
        public string RAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鄉/鎮/市/區
        /// </summary>
        public string RAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鄉
        /// </summary>
        public string RAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鎮
        /// </summary>
        public string RAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_市
        /// </summary>
        public string RAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_區
        /// </summary>
        public string RAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_街/路
        /// </summary>
        public string RAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_街
        /// </summary>
        public string RAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_路
        /// </summary>
        public string RAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_段
        /// </summary>
        public string RAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_巷
        /// </summary>
        public string RAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_弄
        /// </summary>
        public string RAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_號
        /// </summary>
        public string RAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_樓
        /// </summary>
        public string RAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_樓
        /// </summary>
        public string RAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物完整地址
        /// </summary>
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_縣/市
        /// </summary>
        public string BAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_縣
        /// </summary>
        public string BAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_市
        /// </summary>
        public string BAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鄉/鎮/市/區
        /// </summary>
        public string BAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鄉
        /// </summary>
        public string BAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鎮
        /// </summary>
        public string BAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_市
        /// </summary>
        public string BAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_區
        /// </summary>
        public string BAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_街/路
        /// </summary>
        public string BAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_街
        /// </summary>
        public string BAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_路
        /// </summary>
        public string BAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_段
        /// </summary>
        public string BAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_巷
        /// </summary>
        public string BAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_弄
        /// </summary>
        public string BAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_號
        /// </summary>
        public string BAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_樓
        /// </summary>
        public string BAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_樓
        /// </summary>
        public string BAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物屬性
        /// </summary>
        public string BPropoty { get; set; }
        /// <summary>
        /// 設置或獲取承租人-期待租金
        /// </summary>
        public string ExpectRent { get; set; }
        /// <summary>
        /// 設置或獲取出租人-租金預算
        /// </summary>
        public string AnticipateRent { get; set; }
        /// <summary>
        /// 設置或獲取承租人-有無寵物
        /// </summary>
        public string HavePet { get; set; }
        /// <summary>
        /// 設置或獲取出租人-可否寵物
        /// </summary>
        public string AllowPet { get; set; }
        /// <summary>
        /// 設置或獲取客戶備註
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        public string CreateDate { get; set; }        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
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
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
