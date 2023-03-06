
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
    [Table("Chaochi_SecurityFormC")]
    [Serializable]
    public class SecurityFormC: BaseEntity<string>
    {
        public string BuildingId { get; set; }
        public string seq { get; set; }
        public string title  { get; set; }
        public string fileName { get; set; }
        public DateTime uploadTime { get; set; }
    }
}
