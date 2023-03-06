using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Core.Models
{
    [Table("OpenDataRoad")]
    [Serializable]
    public class OpenDataRoad : BaseEntity<string>
    {
        public string city { get; set; }
        public string site_id { get; set; }
        public string road { get; set; }
    }
}
