using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 實體基類
    /// </summary>
    public abstract class Entity:IEntity
    {
        /// <summary>
        /// 判斷主鍵是否為空
        /// </summary>
        /// <returns></returns>
        public abstract bool KeyIsNull();

        /// <summary>
        /// 創建默認的主鍵值
        /// </summary>
        public abstract void GenerateDefaultKeyVal();

        /// <summary>
        /// 構造函數
        /// </summary>
        public Entity()
        {
            if (KeyIsNull())
            {
                GenerateDefaultKeyVal();
            }
        }
    }
}
