using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Security.Dtos
{
    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string vcode { get; set; }
        public string vkey { get; set; }
        public string appId { get; set; }
        public string systemCode { get; set; }
    }
}
