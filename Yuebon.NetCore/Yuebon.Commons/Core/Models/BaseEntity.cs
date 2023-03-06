using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 定義領域對象框架實體類的基類，系統默認主鍵為Id
    /// </summary>
    /// <typeparam name="TKey">實體主鍵類型</typeparam>

    [Serializable]
    public abstract class BaseEntity<TKey> :Entity,IBaseEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseEntity()
        {
        }

        /// <summary>
        /// 獲取或設置 編號
        /// </summary>
        [DisplayName("編號")]
        [Key]
        [Column("Id")]
        public virtual TKey Id { get; set; }


        /// <summary>
        /// 判斷主鍵是否為空
        /// </summary>
        /// <returns></returns>
        public override bool KeyIsNull()
        {
            if (Id == null)
            {
                return true;
            }
            else
            {
                return string.IsNullOrEmpty(Id.ToString());
            }
        }

        /// <summary>
        /// 創建默認的主鍵值
        /// </summary>
        public override void GenerateDefaultKeyVal()
        {
           Id = GuidUtils.CreateNo().CastTo<TKey>();
        }
    }
}
