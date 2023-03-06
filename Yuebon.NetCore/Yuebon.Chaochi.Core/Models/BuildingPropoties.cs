using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_BuildingPropoties")]
    [Serializable]
    public class BuildingPropoties:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string BuildingPropotyName { get; set; }

    }
}
