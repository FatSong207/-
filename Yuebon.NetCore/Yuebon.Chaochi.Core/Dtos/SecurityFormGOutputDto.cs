using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class SecurityFormGOutputDto
    {
        public string seq { get; set; }
        public string title { get; set; }
        public string size { get; set; }
        public string isAppear { get; set; }
        public string fileName { get; set; }
        public DateTime uploadTime { get; set; }
    }
}
