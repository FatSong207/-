using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約-出租人輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractLandlordOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        [MaxLength(100)]
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        [MaxLength(100)]
        public string LSID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string LSName { get; set; }
        /// <summary>
        /// 設置或獲取出租人-負責人(法人)
        /// </summary>
        [MaxLength(100)]
        public string LSRep { get; set; }
        /// <summary>
        /// 設置或獲取戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string LSAdd { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_縣
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_區
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_街
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_路
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_段
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_巷
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_弄
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_號
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-完整通訊地址
        /// </summary>
        [MaxLength(100)]
        public string LSAddC { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string LSTel { get; set; }
        /// <summary>
        /// 設置或獲取出租人-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string LSTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string LSTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取出租人-手機
        /// </summary>
        [MaxLength(15)]
        public string LSCell { get; set; }
        /// <summary>
        /// 設置或獲取出租人-電子郵箱
        /// </summary>
        [MaxLength(255)]
        public string LSMail { get; set; }
        /// <summary>
        /// 設置或獲取出租人金融機構
        /// </summary>
        [MaxLength(100)]
        public string LBankName { get; set; }
        /// <summary>
        /// 設置或獲取出租人戶名
        /// </summary>
        [MaxLength(100)]
        public string LAName { get; set; }
        /// <summary>
        /// 設置或獲取出租人帳號
        /// </summary>
        [MaxLength(50)]
        public string LANo { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人ID
        /// </summary>
        [MaxLength(100)]
        public string LSAGID_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人姓名
        /// </summary>
        [MaxLength(100)]
        public string LSAGName_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_1_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_縣
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_1_1_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_1_2_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_2_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_2_1_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_2_2_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_市
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_2_3_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_區
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_2_4_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_3_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_街
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_3_1_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_路
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_3_2_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_4_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_巷
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_5_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_弄
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_6_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_號
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_7_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_8_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人地址_樓
        /// </summary>
        [MaxLength(100)]
        public string LSAGAddC_9_A { get; set; }
        /// <summary>
        /// 設置或獲取出租人-代理人電話
        /// </summary>
        [MaxLength(100)]
        public string LSAGCell_A { get; set; }
        /// <summary>
        /// 設置或獲取代理人姓名
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}
