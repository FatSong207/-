using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class FileInfoOutputDto
    {

        #region Property Members

        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string Id { get; set; }

        ///// <summary>
        ///// 照片資料
        ///// </summary>
        //public virtual List<FileInfo> BuildingImages { get; set;
        public virtual List<MyFileInfo> BuildingImages { get; set; }

        public virtual List<EqFileInfo> EqRepairImages { get; set; }
        #endregion

    }
}
