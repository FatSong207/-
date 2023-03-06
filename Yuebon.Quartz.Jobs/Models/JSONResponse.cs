using System;
using System.Collections.Generic;
using NPOI.SS.Formula.Functions;

namespace Yuebon.Quartz.Models
{
    /// <summary>
    /// 帳號同步排程，API回傳JSON數據實體對象
    /// </summary>
    [Serializable]
    public class JSONResponse<T>
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public List<T> data { get; set; }
    }
}
