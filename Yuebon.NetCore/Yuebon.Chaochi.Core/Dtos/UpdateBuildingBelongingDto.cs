using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class UpdateBuildingBelongingDto
    {
        public string[] buildingIds { get; set; }

        public Chbelonging chbelonging { get; set; }
    }

    public class Chbelonging
    {
        public string destDept { get; set; }
        public string destLandlord { get; set; }
        public string destUserId { get; set; }
    }
}
