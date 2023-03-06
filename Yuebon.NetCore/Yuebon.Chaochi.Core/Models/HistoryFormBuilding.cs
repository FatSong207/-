
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Core.Models
{
    /// <summary>
    /// 歷史表單
    /// </summary>
    [Table("Chaochi_HistoryFormBuilding")]
    [Serializable]
    public class HistoryFormBuilding : BaseEntity<string>
    {
        public virtual string IDNo { get; set; }
        public virtual string FormName { get; set; }
        public virtual string FileName { get; set; }
        public virtual DateTime? UploadTime { get; set; }
        public virtual string Note { get; set; }
        public virtual string CreatorUserId { get; set; }
    }
}
