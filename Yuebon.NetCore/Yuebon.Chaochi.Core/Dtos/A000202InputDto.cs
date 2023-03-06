using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 社會住宅委託書
    /// </summary>
    [Serializable]
    public class A000202InputDto
    {

        #region LS欄位

        ///// <summary>
        ///// 姓名
        ///// </summary>
        //public virtual string LSName { get; set; }

        /// <summary>
        /// 身分證字號/統一編號
        /// </summary>
        public virtual string LSID { get; set; }

        /// <summary>
        /// 電話_區號
        /// </summary>
        public virtual string LSTel_1 { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public virtual string LSTel_2 { get; set; }

        #endregion LS欄位

        #region LN

        public string LNCell { get; set; }

        #endregion LN

        #region RO欄位

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROAdd { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROTel { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AGName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string AGLRNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ROUserName { get; set; }

        #endregion

        #region 建物

        public virtual string BAdd { get; set; }
        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }
        public virtual string BBalcony { get; set; }
        public virtual string BTCRent { get; set; }
        public virtual string Bdeposit { get; set; }
        public virtual string BCook_Y { get; set; }
        public virtual string BCook_N { get; set; }
        public virtual string BPet_Y { get; set; }
        public virtual string BPet_N { get; set; }
        public virtual string BGodT_Y { get; set; }
        public virtual string BGodT_N { get; set; }
        public virtual string BOpen { get; set; }


        #endregion

        #region BZ欄位

        /// <summary>
        /// BZ001
        /// </summary>
        public virtual string BZ001 { get; set; }

        /// <summary>
        /// BZ002
        /// </summary>
        public virtual string BZ002 { get; set; }

        /// <summary>
        /// BZ003
        /// </summary>
        public virtual string BZ003 { get; set; }

        /// <summary>
        /// BZ004
        /// </summary>
        public virtual string BZ004 { get; set; }

        /// <summary>
        /// BZ005
        /// </summary>
        public virtual string BZ005 { get; set; }

        /// <summary>
        /// BZ006
        /// </summary>
        public virtual string BZ006 { get; set; }

        /// <summary>
        /// BZ007
        /// </summary>
        public virtual string BZ007 { get; set; }

        /// <summary>
        /// BZ008
        /// </summary>
        public virtual string BZ008 { get; set; }

        /// <summary>
        /// BZ009
        /// </summary>
        public virtual string BZ009 { get; set; }

        /// <summary>
        /// BZ010
        /// </summary>
        public virtual string BZ010 { get; set; }

        /// <summary>
        /// BZ011
        /// </summary>
        public virtual string BZ011 { get; set; }

        /// <summary>
        /// BZ012
        /// </summary>
        public virtual string BZ012 { get; set; }

        /// <summary>
        /// BZ013
        /// </summary>
        public virtual string BZ013 { get; set; }

        /// <summary>
        /// BZ014
        /// </summary>
        public virtual string BZ014 { get; set; }

        /// <summary>
        /// BZ015
        /// </summary>
        public virtual string BZ015 { get; set; }

        /// <summary>
        /// BZ016
        /// </summary>
        public virtual string BZ016 { get; set; }

        /// <summary>
        /// BZ017
        /// </summary>
        public virtual string BZ017 { get; set; }

        /// <summary>
        /// BZ018
        /// </summary>
        public virtual string BZ018 { get; set; }

        /// <summary>
        /// BZ019
        /// </summary>
        public virtual string BZ019 { get; set; }

        /// <summary>
        /// BZ020
        /// </summary>
        public virtual string BZ020 { get; set; }


        #endregion BZ欄位

        #region 簽名

        public virtual string SignatureBase64_1 { get; set; }

        #endregion 簽名

        /// <summary>
        /// 是否為產生PDF按鈕
        /// </summary>
        public virtual string IsGenPDF { get; set; }
    }
}
