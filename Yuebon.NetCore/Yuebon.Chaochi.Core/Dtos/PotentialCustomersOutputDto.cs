using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 潛在客管理輸出對象模型
    /// </summary>
    [Serializable]
    public class PotentialCustomersOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取潛在客編號
        /// </summary>
        [MaxLength(100)]
        public string PID { get; set; }
        /// <summary>
        /// 設置或獲取身份
        /// </summary>
        [MaxLength(20)]
        public string Identity { get; set; }
        /// <summary>
        /// 設置或獲取自然人或法人
        /// </summary>
        [MaxLength(20)]
        public string Identity2 { get; set; }
        /// <summary>
        /// 設置或獲取客源
        /// </summary>
        [MaxLength(20)]
        public string Source { get; set; }

        /// <summary>
        /// 設置或獲取當前狀態
        /// </summary>
        [MaxLength(30)]
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取是否結案
        /// </summary>
        [MaxLength(10)]
        public string IsClosed { get; set; }
        /// <summary>
        /// 設置或獲取是否轉正
        /// </summary>
        [MaxLength(10)]
        public string IsTransfer { get; set; }
        /// <summary>
        /// 設置或獲取回報次數
        /// </summary>
        [MaxLength(10)]
        public string ReportBackCounts { get; set; }
        /// <summary>
        /// 設置或獲取指派業務
        /// </summary>
        [MaxLength(100)]
        public string Sales { get; set; }
        /// <summary>
        /// 設置或獲取隸屬區/店
        /// </summary>
        [MaxLength(200)]
        public string Area { get; set; }        /// <summary>
        /// 設置或獲取隸屬區/店_區
        /// </summary>
        [MaxLength(200)]
        public string Area_1 { get; set; }
        /// <summary>
        /// 設置或獲取隸屬區/店_店
        /// </summary>
        [MaxLength(200)]
        public string Area_2 { get; set; }
        /// <summary>
        /// 設置或獲取房屋型態
        /// </summary>
        [MaxLength(100)]
        public string HouseType { get; set; }
        /// <summary>
        /// 設置或獲取建物格局
        /// </summary>
        [MaxLength(40)]
        public string HouseInterior { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string Tel { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string Tel_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string Tel_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人/承租人-手機
        /// </summary>
        [MaxLength(15)]
        public string Cell { get; set; }
        /// <summary>
        /// 設置或獲取所屬縣市
        /// </summary>
        [MaxLength(40)]
        public string CountyCity { get; set; }
        /// <summary>
        /// 設置或獲取承租人-入住人數
        /// </summary>
        [MaxLength(10)]
        public string ResidentCount { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RAdd { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string RAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_區
        /// </summary>
        [MaxLength(100)]
        public string RAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_街
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_路
        /// </summary>
        [MaxLength(100)]
        public string RAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_段
        /// </summary>
        [MaxLength(100)]
        public string RAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_號
        /// </summary>
        [MaxLength(100)]
        public string RAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-房屋戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物完整地址
        /// </summary>
        [MaxLength(100)]
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_縣/市
        /// </summary>
        [MaxLength(20)]
        public string BAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_縣
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_街
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_段
        /// </summary>
        [MaxLength(100)]
        public string BAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_巷
        /// </summary>
        [MaxLength(100)]
        public string BAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_弄
        /// </summary>
        [MaxLength(100)]
        public string BAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_號
        /// </summary>
        [MaxLength(100)]
        public string BAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-建物地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物屬性
        /// </summary>
        [MaxLength(100)]
        public string BPropoty { get; set; }
        /// <summary>
        /// 設置或獲取承租人-期待租金
        /// </summary>
        [MaxLength(100)]
        public string ExpectRent { get; set; }
        /// <summary>
        /// 設置或獲取出租人-租金預算
        /// </summary>
        [MaxLength(100)]
        public string AnticipateRent { get; set; }
        /// <summary>
        /// 設置或獲取承租人-有無寵物
        /// </summary>
        [MaxLength(20)]
        public string HavePet { get; set; }
        /// <summary>
        /// 設置或獲取出租人-可否寵物
        /// </summary>
        [MaxLength(20)]
        public string AllowPet { get; set; }
        /// <summary>
        /// 設置或獲取客戶備註
        /// </summary>
        [MaxLength(4000)]
        public string Note { get; set; }
        /// <summary>
        /// 設置或獲取產生日期
        /// </summary>
        [MaxLength(15)]        public string CreateDate { get; set; }        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        [MaxLength(100)]
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
        [MaxLength(500)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取指派業務姓名
        /// </summary>
        [MaxLength(100)]
        public string SalesName { get; set; }

        // 訪談記錄
        public List<VisitingRecordOutputDto> VisitingRecordList { get; set; }
    }
}
