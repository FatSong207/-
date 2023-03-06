using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Models;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Building))]
    [Serializable]
    public class BuildingInputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 建物屬性(住宅、店面、辦公室、停車位、商業登記、廣告牆面)
        /// </summary>
        public virtual string BPropoty { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣市)
        /// </summary>
        public virtual string BAdd_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(縣)
        /// </summary>
        public virtual string BAdd_1_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_1_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉鎮市區)
        /// </summary>
        public virtual string BAdd_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鄉)
        /// </summary>
        public virtual string BAdd_2_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(市)
        /// </summary>
        public virtual string BAdd_2_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(鎮)
        /// </summary>
        public virtual string BAdd_2_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(區)
        /// </summary>
        public virtual string BAdd_2_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街路)
        /// </summary>
        public virtual string BAdd_3 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(街)
        /// </summary>
        public virtual string BAdd_3_1 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址(路)
        /// </summary>
        public virtual string BAdd_3_2 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址4(段)
        /// </summary>
        public virtual string BAdd_4 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址5(巷)
        /// </summary>
        public virtual string BAdd_5 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址6(弄)
        /// </summary>
        public virtual string BAdd_6 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址7(號)
        /// </summary>
        public virtual string BAdd_7 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址8(樓)
        /// </summary>
        public virtual string BAdd_8 { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址9(之)
        /// </summary>
        public virtual string BAdd_9 { get; set; }

        /// <summary>
        /// 建物座落建號(段)
        /// </summary>
        public virtual string BNo_1 { get; set; }

        /// <summary>
        /// 建物座落建號（小段）
        /// </summary>
        public virtual string BNo_2 { get; set; }

        /// <summary>
        /// 建物座落建號（建號）
        /// </summary>
        public virtual string BNo_3 { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)A
        /// </summary>
        public virtual string BPNo_1_A { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)A
        /// </summary>
        public virtual string BPNo_2_A { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)A
        /// </summary>
        public virtual string BPNo_3_A { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)B
        /// </summary>
        public virtual string BPNo_1_B { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)B
        /// </summary>
        public virtual string BPNo_2_B { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)B
        /// </summary>
        public virtual string BPNo_3_B { get; set; }

        /// <summary>
        /// 建物坐落地號(地段)C
        /// </summary>
        public virtual string BPNo_1_C { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)C
        /// </summary>
        public virtual string BPNo_2_C { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)C
        /// </summary>
        public virtual string BPNo_3_C { get; set; }

        /// <summary>
        /// 建築完成日期
        /// </summary>
        public virtual string BCDate { get; set; }

        /// <summary>
        /// 建築完成日期（年）
        /// </summary>
        public virtual string BCDate_Y { get; set; }

        /// <summary>
        /// 建築完成日期（月）
        /// </summary>
        public virtual string BCDate_M { get; set; }

        /// <summary>
        /// 建築完成日期（日）
        /// </summary>
        public virtual string BCDate_D { get; set; }

        /// <summary>
        /// 樓層(層數)
        /// </summary>
        public virtual string BLevel { get; set; }

        /// <summary>
        /// 樓層(層次）
        /// </summary>
        public virtual string BFloor { get; set; }

        /// <summary>
        /// 謄本面積(平方公尺)1
        /// </summary>
        public virtual string BArea { get; set; }

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }

        /// <summary>
        /// 代租租金1年/月
        /// </summary>
        public virtual string BTRent { get; set; }

        /// <summary>
        /// 代租租金2包租代管
        /// </summary>
        public virtual string BR80C { get; set; }

        /// <summary>
        /// 代租租金3年/月
        /// </summary>
        public virtual string BR80V { get; set; }

        /// <summary>
        /// 代租租金4代租代管
        /// </summary>
        public virtual string BR90C { get; set; }

        /// <summary>
        /// 代租租金5年/月
        /// </summary>
        public virtual string BR90V { get; set; }

        /// <summary>
        /// 押金1
        /// </summary>
        public virtual string BDepositC { get; set; }

        /// <summary>
        /// 押金2
        /// </summary>
        public virtual string Bdeposit { get; set; }

        public virtual string BelongLID { get; set; }


        #region 台北公會三期-社會住宅轉租契約書
        /// <summary>
        /// 建物坐落地號(地段)
        /// </summary>
        public virtual string BPNo_1 { get; set; }

        /// <summary>
        /// 建物坐落地號(小段)
        /// </summary>
        public virtual string BPNo_2 { get; set; }

        /// <summary>
        /// 建物坐落地號(地號)
        /// </summary>
        public virtual string BPNo_3 { get; set; }

        /// <summary>
        /// 有無車位(有)
        /// </summary>
        public virtual string BCar_Y { get; set; }

        /// <summary>
        /// 有無車位(無)
        /// </summary>
        public virtual string BCar_N { get; set; }

        /// <summary>
        /// 車位編號
        /// </summary>
        public virtual string BCarNo { get; set; }

        /// <summary>
        /// 押金月數
        /// </summary>
        public virtual string BDMon { get; set; }
        #endregion

        ///// <summary>
        ///// 創建日期
        ///// </summary>
        //public virtual DateTime? CreatorTime { get; set; }

        ///// <summary>
        ///// 創建用戶主鍵
        ///// </summary>
        //public virtual string CreatorUserId { get; set; }

        ///// <summary>
        ///// 最後修改時間
        ///// </summary>
        //public virtual DateTime? LastModifyTime { get; set; }

        ///// <summary>
        ///// 最後修改用戶
        ///// </summary>
        //public virtual string LastModifyUserId { get; set; }

        ///// <summary>
        ///// 刪除時間
        ///// </summary>
        //public virtual DateTime? DeleteTime { get; set; }

        ///// <summary>
        ///// 刪除用戶
        ///// </summary>
        //[MaxLength(50)]
        //public virtual string DeleteUserId { get; set; }

        ///// <summary>
        ///// 產權資料
        ///// </summary>
        public virtual List<BuildingBelonging> BuildingBelongings { get; set; }

        /// <summary>
        /// 其他所屬權人
        /// </summary>
        public virtual Building1 Building1 { get; set; }


        public string BState { get; set; }

        public string[] ids { get; set; }

        /// <summary>
        /// 設置或獲取分租物件單編號
        /// </summary>
        public string MOID { get; set; }
    }
}
