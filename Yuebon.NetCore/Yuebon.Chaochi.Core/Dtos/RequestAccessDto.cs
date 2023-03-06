using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Core.Dtos
{

    [Serializable]
    public class RequestAccessDto
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string BirthDay { get; set; }
        public string Cell { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Rep { get; set; }
    }
}
