using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class SearchWebFormPDF
    {
        /// <summary>
        /// 表單代號
        /// </summary>
        public string FormId { get; set; }

        ///// <summary>
        ///// 指定匯款資訊(必須要==true且LRemittanceId或RRemittanceId才會帶入)
        ///// </summary>
        //public virtual bool? RemittanceCheck { get; set; }

        ///// <summary>
        ///// L匯款資料
        ///// </summary>
        //public virtual string LRemittanceId { get; set; }

        ///// <summary>
        ///// R匯款資料
        ///// </summary>
        //public virtual string RRemittanceId { get; set; }

        /// <summary>
        /// (自然人)身分證字號
        /// </summary>
        public string LNID { get; set; }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public string LCID { get; set; }

        /// <summary>
        /// (自然人)身分證字號
        /// </summary>
        public string RNID { get; set; }

        /// <summary>
        /// (法人)統一編號
        /// </summary>
        public string RCID { get; set; }


        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public string BAdd { get; set; }
    }
}
