using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MOBuildingOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        [MaxLength(100)]
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取物件完整地址
        /// </summary>
        [MaxLength(100)]
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_縣/市
        /// </summary>
        [MaxLength(20)]
        public string BAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_縣
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_市
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_區
        /// </summary>
        [MaxLength(100)]
        public string BAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_街
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_路
        /// </summary>
        [MaxLength(100)]
        public string BAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_段
        /// </summary>
        [MaxLength(100)]
        public string BAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_巷
        /// </summary>
        [MaxLength(100)]
        public string BAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_弄
        /// </summary>
        [MaxLength(100)]
        public string BAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_號
        /// </summary>
        [MaxLength(100)]
        public string BAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_樓
        /// </summary>
        [MaxLength(100)]
        public string BAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物屬性
        /// </summary>
        [MaxLength(100)]
        public string BPropoty { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
