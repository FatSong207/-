using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class BuildingEquipmentInPutDto
    {
        public string BAdd { get; set; }
        public string OtherDevices { get; set; }
        public List<BuildingEq> BuildingEqs { get; set; }
    }
}
