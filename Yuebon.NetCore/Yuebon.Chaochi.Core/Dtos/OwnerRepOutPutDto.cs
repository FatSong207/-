using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class OwnerRepOutPutDto
    {

        #region Property Members

        public string Name { get; set; }

        public string ID { get; set; }

        public string Tel1 { get; set; }

        public string Tel2 { get; set; }

        public string Cell { get; set; }

        public string Address { get; set; }

        #endregion

    }
}
