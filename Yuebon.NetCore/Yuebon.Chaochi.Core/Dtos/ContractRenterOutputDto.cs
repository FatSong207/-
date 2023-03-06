using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約-承租人輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractRenterOutputDto
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
        /// 設置或獲取承租人-身份證號或統一編號
        /// </summary>
        [MaxLength(100)]
        public string RNID { get; set; }
        /// <summary>
        /// 設置或獲取承租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string RNName { get; set; }
        /// <summary>
        /// 設置或獲取承租人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string RNTel { get; set; }
        /// <summary>
        /// 設置或獲取承租人-聯絡電話_區碼
        /// </summary>
        [MaxLength(10)]
        public string RNTel_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-聯絡電話_號碼
        /// </summary>
        [MaxLength(10)]
        public string RNTel_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-完整戶籍地址
        /// </summary>
        [MaxLength(100)]
        public string RNAdd { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_區
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_街
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_路
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_段
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_號
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-戶籍地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAdd_9 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-完整通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RNAddC { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAddC_9 { get; set; }
        /// <summary>
        /// 設置或獲取承租人金融機構
        /// </summary>
        [MaxLength(100)]
        public string RBankName { get; set; }
        /// <summary>
        /// 設置或獲取承租人戶名
        /// </summary>
        [MaxLength(100)]
        public string RAName { get; set; }
        /// <summary>
        /// 設置或獲取承租人帳號
        /// </summary>
        [MaxLength(50)]
        public string RANo { get; set; }
        /// <summary>
        /// 設置或獲取承租人手機號碼
        /// </summary>
        [MaxLength(100)]
        public string RNCell { get; set; }
        /// <summary>
        /// 設置或獲取承租人電子信箱
        /// </summary>
        [MaxLength(100)]
        public string RNMail { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人姓名
        /// </summary>
        [MaxLength(100)]
        public string RNAGName_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人身份證號
        /// </summary>
        [MaxLength(100)]
        public string RNAGID_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人手機號碼
        /// </summary>
        [MaxLength(100)]
        public string RNAGCell_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_1_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_1_1_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_1_2_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_2_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_2_1_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_2_2_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_2_3_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_2_4_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_3_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_3_1_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_3_2_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_4_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_5_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_6_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_7_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_8_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-代理人通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNAGAddC_9_A { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人姓名
        /// </summary>
        [MaxLength(100)]
        public string RNGName { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人身份證號
        /// </summary>
        [MaxLength(100)]
        public string RNGID { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人手機號碼
        /// </summary>
        [MaxLength(100)]
        public string RNGCell { get; set; }
        [MaxLength(100)]
        public string RNGAdd { get; set; }
        [MaxLength(100)]
        public string RNGAdd_1 { get; set; }
        
        [MaxLength(100)]
        public string RNGAdd_1_1 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_1_2 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_2 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_2_1 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_2_2 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_2_3 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_2_4 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_3 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_3_1 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_3_2 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_4 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_5 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_6 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_7 { get; set; }
        [MaxLength(100)]
        public string RNGAdd_8 { get; set; }
        [MaxLength(100)]        public string RNGAdd_9 { get; set; }        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_縣/市
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_縣
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_鄉/鎮/市/區
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_鄉
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_鎮
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_市
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_區
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_街/路
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_街
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_路
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_段
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_巷
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_弄
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_號
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取承租人-保證人通訊地址_樓
        /// </summary>
        [MaxLength(100)]
        public string RNGCAdd_9 { get; set; }        /// <summary>
        /// 設置或獲取創建時間
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
