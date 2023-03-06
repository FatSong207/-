using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class CustomerLSBuildingOutputDto
    {

        #region Property Members

        /// <summary>
        /// (自然人)身分證字號/(法人)統一編號
        /// </summary>
        public virtual string LSID { get; set; }
        /// <summary>
        /// (自然人)姓名/(法人)法人名稱
        /// </summary>
        public virtual string LSName { get; set; }

        /// <summary>
        /// 物件編號
        /// </summary>
        public virtual string BID { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        #endregion

    }
}
