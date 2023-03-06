using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Core.Models
{
    /// <summary>
    /// 簽名圖檔，數據實體對象
    /// </summary>
    [Table("SignatureImg")]
    [Serializable]
    public class SignatureImg : BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public SignatureImg()
        {

        }

        #region Property Members
        /// <summary>
        /// 表單編號
        /// </summary>
        public virtual string FormId { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }
        public virtual string SignatureImgPath_1 { get; set; }
        public virtual string SignatureImgPath_2 { get; set; }
        public virtual string SignatureImgPath_3 { get; set; }
        public virtual string SignatureImgPath_4 { get; set; }
        public virtual string SignatureImgPath_5 { get; set; }

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
