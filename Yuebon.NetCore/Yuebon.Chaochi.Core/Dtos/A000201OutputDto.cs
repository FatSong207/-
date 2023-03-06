using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 房屋租賃委託契約書
    /// </summary>
    [Serializable]
    public class A000201OutputDto
    {

        #region LS欄位

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string LSName { get; set; }

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

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_1_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_3 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_2_4 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3_1 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_3_2 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_4 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_5 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_6 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_7 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_8 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string LSAdd_9 { get; set; }

        #endregion

        #region LN

        /// <summary>
        /// 受託人的電話原本是LSTel欄位，對方要求手機號碼，但是法人沒有手機所以該欄位直接改成LNCell
        /// </summary>
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

        /// <summary>
        /// 謄本面積（坪）2
        /// </summary>
        public virtual string BPing { get; set; }
        public virtual string BRoom { get; set; }
        public virtual string BLD { get; set; }
        public virtual string BBath { get; set; }
        public virtual string BKitchen { get; set; }
        public virtual string BBalcony { get; set; }
        public virtual string BTApartment { get; set; }
        public virtual string BTElevator { get; set; }
        public virtual string BTHouse { get; set; }
        public virtual string Belevator_Y { get; set; }
        public virtual string Belevator_N { get; set; }
        public virtual string BSuper_Y { get; set; }
        public virtual string BSuper_N { get; set; }
        public virtual string BCarFloor { get; set; }
        public virtual string BCarNo { get; set; }
        public virtual string BRampFlatParking { get; set; }
        public virtual string BRampMechParking { get; set; }
        public virtual string BMechFlatParking { get; set; }
        public virtual string BMechMechParking { get; set; }
        public virtual string BTCRent { get; set; }
        public virtual string Bdeposit { get; set; }
        public virtual string BParkFee { get; set; }
        public virtual string BMFee { get; set; }
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

        /// <summary>
        /// BZ021
        /// </summary>
        public virtual string BZ021 { get; set; }

        /// <summary>
        /// BZ022
        /// </summary>
        public virtual string BZ022 { get; set; }

        /// <summary>
        /// BZ023
        /// </summary>
        public virtual string BZ023 { get; set; }

        /// <summary>
        /// BZ024
        /// </summary>
        public virtual string BZ024 { get; set; }

        /// <summary>
        /// BZ025
        /// </summary>
        public virtual string BZ025 { get; set; }

        /// <summary>
        /// BZ026
        /// </summary>
        public virtual string BZ026 { get; set; }

        /// <summary>
        /// BZ027
        /// </summary>
        public virtual string BZ027 { get; set; }

        /// <summary>
        /// BZ028
        /// </summary>
        public virtual string BZ028 { get; set; }

        /// <summary>
        /// BZ029
        /// </summary>
        public virtual string BZ029 { get; set; }

        /// <summary>
        /// BZ030
        /// </summary>
        public virtual string BZ030 { get; set; }

        /// <summary>
        /// BZ031
        /// </summary>
        public virtual string BZ031 { get; set; }

        /// <summary>
        /// BZ032
        /// </summary>
        public virtual string BZ032 { get; set; }

        /// <summary>
        /// BZ033
        /// </summary>
        public virtual string BZ033 { get; set; }

        /// <summary>
        /// BZ034
        /// </summary>
        public virtual string BZ034 { get; set; }

        /// <summary>
        /// BZ035
        /// </summary>
        public virtual string BZ035 { get; set; }

        /// <summary>
        /// BZ036
        /// </summary>
        public virtual string BZ036 { get; set; }

        /// <summary>
        /// BZ037
        /// </summary>
        public virtual string BZ037 { get; set; }

        /// <summary>
        /// BZ038
        /// </summary>
        public virtual string BZ038 { get; set; }

        /// <summary>
        /// BZ039
        /// </summary>
        public virtual string BZ039 { get; set; }

        /// <summary>
        /// BZ040
        /// </summary>
        public virtual string BZ040 { get; set; }

        /// <summary>
        /// BZ041
        /// </summary>
        public virtual string BZ041 { get; set; }

        /// <summary>
        /// BZ042
        /// </summary>
        public virtual string BZ042 { get; set; }

        /// <summary>
        /// BZ043
        /// </summary>
        public virtual string BZ043 { get; set; }

        /// <summary>
        /// BZ044
        /// </summary>
        public virtual string BZ044 { get; set; }

        /// <summary>
        /// BZ045
        /// </summary>
        public virtual string BZ045 { get; set; }

        /// <summary>
        /// BZ046
        /// </summary>
        public virtual string BZ046 { get; set; }

        /// <summary>
        /// BZ047
        /// </summary>
        public virtual string BZ047 { get; set; }

        /// <summary>
        /// BZ048
        /// </summary>
        public virtual string BZ048 { get; set; }

        /// <summary>
        /// BZ049
        /// </summary>
        public virtual string BZ049 { get; set; }

        /// <summary>
        /// BZ050
        /// </summary>
        public virtual string BZ050 { get; set; }

        /// <summary>
        /// BZ051
        /// </summary>
        public virtual string BZ051 { get; set; }

        /// <summary>
        /// BZ052
        /// </summary>
        public virtual string BZ052 { get; set; }

        /// <summary>
        /// BZ053
        /// </summary>
        public virtual string BZ053 { get; set; }

        /// <summary>
        /// BZ054
        /// </summary>
        public virtual string BZ054 { get; set; }

        /// <summary>
        /// BZ055
        /// </summary>
        public virtual string BZ055 { get; set; }

        /// <summary>
        /// BZ056
        /// </summary>
        public virtual string BZ056 { get; set; }

        /// <summary>
        /// BZ057
        /// </summary>
        public virtual string BZ057 { get; set; }

        /// <summary>
        /// BZ058
        /// </summary>
        public virtual string BZ058 { get; set; }

        /// <summary>
        /// BZ059
        /// </summary>
        public virtual string BZ059 { get; set; }

        /// <summary>
        /// BZ060
        /// </summary>
        public virtual string BZ060 { get; set; }

        /// <summary>
        /// BZ061
        /// </summary>
        public virtual string BZ061 { get; set; }

        /// <summary>
        /// BZ062
        /// </summary>
        public virtual string BZ062 { get; set; }



        #endregion BZ欄位

        #region 簽名

        public string SignatureImgPath_1 { get; set; }


        #endregion 簽名
    }
}
