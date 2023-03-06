using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class HistoryFormLNOutputDto
    {
        public virtual string Id { get; set; }
        public virtual string IDNo { get; set; }
        public virtual string FormName { get; set; }
        public virtual string FileName { get; set; }
        public virtual DateTime? UploadTime { get; set; }
        public virtual string Note { get; set; }
        public virtual string CreatorUserId { get; set; }
    }
}
