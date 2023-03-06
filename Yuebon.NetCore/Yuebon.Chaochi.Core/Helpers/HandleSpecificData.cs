using System.Linq;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Helpers;
using System;

namespace Yuebon.Chaochi.Core.Helpers
{
    public class HandleSpecificData
    {

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位
        /// </summary>
        /// <param name="SourceCustomerRN"></param>
        /// <param name="customerrn"></param>
        /// <returns></returns>
        public static CustomerRN HandleSpecificDataRN<T>(CustomerRN SourceCustomerRN, T inputDto) where T : class
        {
            var t = typeof(T);
            var props = t.GetProperties();
            var customerrn = new CustomerRN();
            var crnProps = typeof(CustomerRN).GetProperties();
            foreach (var prop in props) {
                var propInfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                if (propInfo is not null) {
                    //var propinfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                    propInfo.SetValue(customerrn, prop.GetValue(inputDto));
                }
            }

            #region 同住者
            //同住者A
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_A)) {
                var splitChar = customerrn.RNFID_1_A.ToCharArray();
                SourceCustomerRN.RNFID_1_A_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_A_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_A_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_A_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_A_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_A_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_A_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_A_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_A_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_A_10 = splitChar[9].ToString();
            }
            //同住者B
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_B)) {
                var splitChar = customerrn.RNFID_1_B.ToCharArray();
                SourceCustomerRN.RNFID_1_B_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_B_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_B_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_B_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_B_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_B_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_B_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_B_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_B_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_B_10 = splitChar[9].ToString();
            }
            //同住者C
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_C)) {
                var splitChar = customerrn.RNFID_1_C.ToCharArray();
                SourceCustomerRN.RNFID_1_C_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_C_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_C_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_C_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_C_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_C_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_C_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_C_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_C_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_C_10 = splitChar[9].ToString();
            }
            //同住者D
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_D)) {
                var splitChar = customerrn.RNFID_1_D.ToCharArray();
                SourceCustomerRN.RNFID_1_D_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_D_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_D_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_D_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_D_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_D_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_D_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_D_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_D_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_D_10 = splitChar[9].ToString();
            }
            //同住者E
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_E)) {
                var splitChar = customerrn.RNFID_1_E.ToCharArray();
                SourceCustomerRN.RNFID_1_E_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_E_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_E_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_E_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_E_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_E_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_E_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_E_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_E_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_E_10 = splitChar[9].ToString();
            }
            //同住者F
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_F)) {
                var splitChar = customerrn.RNFID_1_F.ToCharArray();
                SourceCustomerRN.RNFID_1_F_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_F_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_F_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_F_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_F_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_F_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_F_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_F_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_F_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_F_10 = splitChar[9].ToString();
            }
            //同住者G
            if (!string.IsNullOrEmpty(customerrn.RNFID_1_G)) {
                var splitChar = customerrn.RNFID_1_G.ToCharArray();
                SourceCustomerRN.RNFID_1_G_1 = splitChar[0].ToString();
                SourceCustomerRN.RNFID_1_G_2 = splitChar[1].ToString();
                SourceCustomerRN.RNFID_1_G_3 = splitChar[2].ToString();
                SourceCustomerRN.RNFID_1_G_4 = splitChar[3].ToString();
                SourceCustomerRN.RNFID_1_G_5 = splitChar[4].ToString();
                SourceCustomerRN.RNFID_1_G_6 = splitChar[5].ToString();
                SourceCustomerRN.RNFID_1_G_7 = splitChar[6].ToString();
                SourceCustomerRN.RNFID_1_G_8 = splitChar[7].ToString();
                SourceCustomerRN.RNFID_1_G_9 = splitChar[8].ToString();
                SourceCustomerRN.RNFID_1_G_10 = splitChar[9].ToString();
            }
            #endregion 同住者

            #region 承租人

            //生日
            if (!string.IsNullOrEmpty(customerrn.RNBirthday)) {
                SourceCustomerRN.RNBirthday_Y = customerrn.RNBirthday.Split("-")[0];
                SourceCustomerRN.RNBirthday_M = customerrn.RNBirthday.Split("-")[1];
                SourceCustomerRN.RNBirthday_D = customerrn.RNBirthday.Split("-")[2];
            }
            //電話
            SourceCustomerRN.RNTel = customerrn.RNTel_1 + "-" + customerrn.RNTel_2;
            SourceCustomerRN.RNTelN = customerrn.RNTelN_1 + "-" + customerrn.RNTelN_2;
            ////手機
            //if (!string.IsNullOrEmpty(customerrn.RNCell)) {
            //    if (customerrn.RNCell.Length >= 4 && !customerrn.RNCell.Contains("-")) {
            //        SourceCustomerRN.RNCell = customerrn.RNCell.Insert(4, "-");
            //    }
            //}
            //身分證字號
            if (!string.IsNullOrEmpty(customerrn.RNID)) {
                var idSplit = customerrn.RNID.ToCharArray();
                SourceCustomerRN.RNID_1_1 = idSplit[0].ToString();
                SourceCustomerRN.RNID_1_2 = idSplit[1].ToString();
                SourceCustomerRN.RNID_1_3 = idSplit[2].ToString();
                SourceCustomerRN.RNID_1_4 = idSplit[3].ToString();
                SourceCustomerRN.RNID_1_5 = idSplit[4].ToString();
                SourceCustomerRN.RNID_1_6 = idSplit[5].ToString();
                SourceCustomerRN.RNID_1_7 = idSplit[6].ToString();
                SourceCustomerRN.RNID_1_8 = idSplit[7].ToString();
                SourceCustomerRN.RNID_1_9 = idSplit[8].ToString();
                SourceCustomerRN.RNID_1_10 = idSplit[9].ToString();
            }

            //戶口名簿號
            if (!string.IsNullOrEmpty(customerrn.RNFAccount)) {
                var split = customerrn.RNFAccount.ToCharArray();
                SourceCustomerRN.RNMAccount_1 = split[0].ToString();
                SourceCustomerRN.RNMAccount_2 = split[1].ToString();
                SourceCustomerRN.RNMAccount_3 = split[2].ToString();
                SourceCustomerRN.RNMAccount_4 = split[3].ToString();
                SourceCustomerRN.RNMAccount_5 = split[4].ToString();
                SourceCustomerRN.RNMAccount_6 = split[5].ToString();
                SourceCustomerRN.RNMAccount_7 = split[6].ToString();
                SourceCustomerRN.RNMAccount_8 = split[7].ToString();
            }

            ////配偶戶口名簿號
            //if (!string.IsNullOrEmpty(customerrn.RNMMAccount)) {
            //    var split = customerrn.RNMMAccount.ToCharArray();
            //    SourceCustomerRN.RNMMAccount_1 = split[0].ToString();
            //    SourceCustomerRN.RNMMAccount_2 = split[1].ToString();
            //    SourceCustomerRN.RNMMAccount_3 = split[2].ToString();
            //    SourceCustomerRN.RNMMAccount_4 = split[3].ToString();
            //    SourceCustomerRN.RNMMAccount_5 = split[4].ToString();
            //    SourceCustomerRN.RNMMAccount_6 = split[5].ToString();
            //    SourceCustomerRN.RNMMAccount_7 = split[6].ToString();
            //    SourceCustomerRN.RNMMAccount_8 = split[7].ToString();
            //}

            #endregion 承租人

            #region 地址

            //承租人戶籍地址
            SourceCustomerRN.RNAdd = Utils.memergeAddFix(
                customerrn.RNAdd_1,
                customerrn.RNAdd_1_1,
                customerrn.RNAdd_1_2,
                customerrn.RNAdd_2,
                customerrn.RNAdd_2_1,
                customerrn.RNAdd_2_2,
                customerrn.RNAdd_2_3,
                customerrn.RNAdd_2_4,
                customerrn.RNAdd_3,
                customerrn.RNAdd_3_1,
                customerrn.RNAdd_3_2,
                customerrn.RNAdd_4,
                customerrn.RNAdd_5,
                customerrn.RNAdd_6,
                customerrn.RNAdd_7,
                customerrn.RNAdd_8,
                customerrn.RNAdd_9);
            //承租人通訊地址
            if (customerrn.RNAddSame == "/Yes") {
                SourceCustomerRN.RNAddC_1 = customerrn.RNAdd_1;
                SourceCustomerRN.RNAddC_1_1 = customerrn.RNAdd_1_1;
                SourceCustomerRN.RNAddC_1_2 = customerrn.RNAdd_1_2;
                SourceCustomerRN.RNAddC_2 = customerrn.RNAdd_2;
                SourceCustomerRN.RNAddC_2_1 = customerrn.RNAdd_2_1;
                SourceCustomerRN.RNAddC_2_2 = customerrn.RNAdd_2_2;
                SourceCustomerRN.RNAddC_2_3 = customerrn.RNAdd_2_3;
                SourceCustomerRN.RNAddC_2_4 = customerrn.RNAdd_2_4;
                SourceCustomerRN.RNAddC_3 = customerrn.RNAdd_3;
                SourceCustomerRN.RNAddC_3_1 = customerrn.RNAdd_3_1;
                SourceCustomerRN.RNAddC_3_2 = customerrn.RNAdd_3_2;
                SourceCustomerRN.RNAddC_4 = customerrn.RNAdd_4;
                SourceCustomerRN.RNAddC_5 = customerrn.RNAdd_5;
                SourceCustomerRN.RNAddC_6 = customerrn.RNAdd_6;
                SourceCustomerRN.RNAddC_7 = customerrn.RNAdd_7;
                SourceCustomerRN.RNAddC_8 = customerrn.RNAdd_8;
                SourceCustomerRN.RNAddC_9 = customerrn.RNAdd_9;
                SourceCustomerRN.RNAddC = SourceCustomerRN.RNAdd;
            } else {
                SourceCustomerRN.RNAddC = Utils.memergeAddFix(
                        customerrn.RNAddC_1,
                        customerrn.RNAddC_1_1,
                        customerrn.RNAddC_1_2,
                        customerrn.RNAddC_2,
                        customerrn.RNAddC_2_1,
                        customerrn.RNAddC_2_2,
                        customerrn.RNAddC_2_3,
                        customerrn.RNAddC_2_4,
                        customerrn.RNAddC_3,
                        customerrn.RNAddC_3_1,
                        customerrn.RNAddC_3_2,
                        customerrn.RNAddC_4,
                        customerrn.RNAddC_5,
                        customerrn.RNAddC_6,
                        customerrn.RNAddC_7,
                        customerrn.RNAddC_8,
                        customerrn.RNAddC_9);
            }

            //緊急聯絡人戶籍地址
            SourceCustomerRN.RNECAdd = Utils.memergeAddFix(
                customerrn.RNECAdd_1,
                customerrn.RNECAdd_1_1,
                customerrn.RNECAdd_1_2,
                customerrn.RNECAdd_2,
                customerrn.RNECAdd_2_1,
                customerrn.RNECAdd_2_2,
                customerrn.RNECAdd_2_3,
                customerrn.RNECAdd_2_4,
                customerrn.RNECAdd_3,
                customerrn.RNECAdd_3_1,
                customerrn.RNECAdd_3_2,
                customerrn.RNECAdd_4,
                customerrn.RNECAdd_5,
                customerrn.RNECAdd_6,
                customerrn.RNECAdd_7,
                customerrn.RNECAdd_8,
                customerrn.RNECAdd_9);
            //緊急聯絡人通訊地址
            if (customerrn.RNECAddSame == "/Yes") {
                SourceCustomerRN.RNECAddC_1 = customerrn.RNECAdd_1;
                SourceCustomerRN.RNECAddC_1_1 = customerrn.RNECAdd_1_1;
                SourceCustomerRN.RNECAddC_1_2 = customerrn.RNECAdd_1_2;
                SourceCustomerRN.RNECAddC_2 = customerrn.RNECAdd_2;
                SourceCustomerRN.RNECAddC_2_1 = customerrn.RNECAdd_2_1;
                SourceCustomerRN.RNECAddC_2_2 = customerrn.RNECAdd_2_2;
                SourceCustomerRN.RNECAddC_2_3 = customerrn.RNECAdd_2_3;
                SourceCustomerRN.RNECAddC_2_4 = customerrn.RNECAdd_2_4;
                SourceCustomerRN.RNECAddC_3 = customerrn.RNECAdd_3;
                SourceCustomerRN.RNECAddC_3_1 = customerrn.RNECAdd_3_1;
                SourceCustomerRN.RNECAddC_3_2 = customerrn.RNECAdd_3_2;
                SourceCustomerRN.RNECAddC_4 = customerrn.RNECAdd_4;
                SourceCustomerRN.RNECAddC_5 = customerrn.RNECAdd_5;
                SourceCustomerRN.RNECAddC_6 = customerrn.RNECAdd_6;
                SourceCustomerRN.RNECAddC_7 = customerrn.RNECAdd_7;
                SourceCustomerRN.RNECAddC_8 = customerrn.RNECAdd_8;
                SourceCustomerRN.RNECAddC_9 = customerrn.RNECAdd_9;
                SourceCustomerRN.RNECAddC = SourceCustomerRN.RNECAdd;
            } else {
                SourceCustomerRN.RNECAddC = Utils.memergeAddFix(
                        customerrn.RNECAddC_1,
                        customerrn.RNECAddC_1_1,
                        customerrn.RNECAddC_1_2,
                        customerrn.RNECAddC_2,
                        customerrn.RNECAddC_2_1,
                        customerrn.RNECAddC_2_2,
                        customerrn.RNECAddC_2_3,
                        customerrn.RNECAddC_2_4,
                        customerrn.RNECAddC_3,
                        customerrn.RNECAddC_3_1,
                        customerrn.RNECAddC_3_2,
                        customerrn.RNECAddC_4,
                        customerrn.RNECAddC_5,
                        customerrn.RNECAddC_6,
                        customerrn.RNECAddC_7,
                        customerrn.RNECAddC_8,
                        customerrn.RNECAddC_9);
            }

            //代理人戶籍地址
            SourceCustomerRN.RNAGAdd = Utils.memergeAddFix(
                customerrn.RNAGAdd_1_A,
                customerrn.RNAGAdd_1_1_A,
                customerrn.RNAGAdd_1_2_A,
                customerrn.RNAGAdd_2_A,
                customerrn.RNAGAdd_2_1_A,
                customerrn.RNAGAdd_2_2_A,
                customerrn.RNAGAdd_2_3_A,
                customerrn.RNAGAdd_2_4_A,
                customerrn.RNAGAdd_3_A,
                customerrn.RNAGAdd_3_1_A,
                customerrn.RNAGAdd_3_2_A,
                customerrn.RNAGAdd_4_A,
                customerrn.RNAGAdd_5_A,
                customerrn.RNAGAdd_6_A,
                customerrn.RNAGAdd_7_A,
                customerrn.RNAGAdd_8_A,
                customerrn.RNAGAdd_9_A);
            //代理人通訊地址
            if (customerrn.RNAGAddSame == "/Yes") {
                SourceCustomerRN.RNAGAddC_1_A = customerrn.RNAGAdd_1_A;
                SourceCustomerRN.RNAGAddC_1_1_A = customerrn.RNAGAdd_1_1_A;
                SourceCustomerRN.RNAGAddC_1_2_A = customerrn.RNAGAdd_1_2_A;
                SourceCustomerRN.RNAGAddC_2_A = customerrn.RNAGAdd_2_A;
                SourceCustomerRN.RNAGAddC_2_1_A = customerrn.RNAGAdd_2_1_A;
                SourceCustomerRN.RNAGAddC_2_2_A = customerrn.RNAGAdd_2_2_A;
                SourceCustomerRN.RNAGAddC_2_3_A = customerrn.RNAGAdd_2_3_A;
                SourceCustomerRN.RNAGAddC_2_4_A = customerrn.RNAGAdd_2_4_A;
                SourceCustomerRN.RNAGAddC_3_A = customerrn.RNAGAdd_3_A;
                SourceCustomerRN.RNAGAddC_3_1_A = customerrn.RNAGAdd_3_1_A;
                SourceCustomerRN.RNAGAddC_3_2_A = customerrn.RNAGAdd_3_2_A;
                SourceCustomerRN.RNAGAddC_4_A = customerrn.RNAGAdd_4_A;
                SourceCustomerRN.RNAGAddC_5_A = customerrn.RNAGAdd_5_A;
                SourceCustomerRN.RNAGAddC_6_A = customerrn.RNAGAdd_6_A;
                SourceCustomerRN.RNAGAddC_7_A = customerrn.RNAGAdd_7_A;
                SourceCustomerRN.RNAGAddC_8_A = customerrn.RNAGAdd_8_A;
                SourceCustomerRN.RNAGAddC_9_A = customerrn.RNAGAdd_9_A;
                SourceCustomerRN.RNAGAddC = SourceCustomerRN.RNAGAdd;
            } else {
                SourceCustomerRN.RNAGAddC = Utils.memergeAddFix(
                        customerrn.RNAGAddC_1_A,
                        customerrn.RNAGAddC_1_1_A,
                        customerrn.RNAGAddC_1_2_A,
                        customerrn.RNAGAddC_2_A,
                        customerrn.RNAGAddC_2_1_A,
                        customerrn.RNAGAddC_2_2_A,
                        customerrn.RNAGAddC_2_3_A,
                        customerrn.RNAGAddC_2_4_A,
                        customerrn.RNAGAddC_3_A,
                        customerrn.RNAGAddC_3_1_A,
                        customerrn.RNAGAddC_3_2_A,
                        customerrn.RNAGAddC_4_A,
                        customerrn.RNAGAddC_5_A,
                        customerrn.RNAGAddC_6_A,
                        customerrn.RNAGAddC_7_A,
                        customerrn.RNAGAddC_8_A,
                        customerrn.RNAGAddC_9_A);
            }

            //保證人戶籍地址
            SourceCustomerRN.RNGAdd = Utils.memergeAddFix(
                customerrn.RNGAdd_1,
                customerrn.RNGAdd_1_1,
                customerrn.RNGAdd_1_2,
                customerrn.RNGAdd_2,
                customerrn.RNGAdd_2_1,
                customerrn.RNGAdd_2_2,
                customerrn.RNGAdd_2_3,
                customerrn.RNGAdd_2_4,
                customerrn.RNGAdd_3,
                customerrn.RNGAdd_3_1,
                customerrn.RNGAdd_3_2,
                customerrn.RNGAdd_4,
                customerrn.RNGAdd_5,
                customerrn.RNGAdd_6,
                customerrn.RNGAdd_7,
                customerrn.RNGAdd_8,
                customerrn.RNGAdd_9);
            //保證人通訊地址
            if (customerrn.RNGAddSame == "/Yes") {
                SourceCustomerRN.RNGCAdd_1 = customerrn.RNGAdd_1;
                SourceCustomerRN.RNGCAdd_1_1 = customerrn.RNGAdd_1_1;
                SourceCustomerRN.RNGCAdd_1_2 = customerrn.RNGAdd_1_2;
                SourceCustomerRN.RNGCAdd_2 = customerrn.RNGAdd_2;
                SourceCustomerRN.RNGCAdd_2_1 = customerrn.RNGAdd_2_1;
                SourceCustomerRN.RNGCAdd_2_2 = customerrn.RNGAdd_2_2;
                SourceCustomerRN.RNGCAdd_2_3 = customerrn.RNGAdd_2_3;
                SourceCustomerRN.RNGCAdd_2_4 = customerrn.RNGAdd_2_4;
                SourceCustomerRN.RNGCAdd_3 = customerrn.RNGAdd_3;
                SourceCustomerRN.RNGCAdd_3_1 = customerrn.RNGAdd_3_1;
                SourceCustomerRN.RNGCAdd_3_2 = customerrn.RNGAdd_3_2;
                SourceCustomerRN.RNGCAdd_4 = customerrn.RNGAdd_4;
                SourceCustomerRN.RNGCAdd_5 = customerrn.RNGAdd_5;
                SourceCustomerRN.RNGCAdd_6 = customerrn.RNGAdd_6;
                SourceCustomerRN.RNGCAdd_7 = customerrn.RNGAdd_7;
                SourceCustomerRN.RNGCAdd_8 = customerrn.RNGAdd_8;
                SourceCustomerRN.RNGCAdd_9 = customerrn.RNGAdd_9;
                SourceCustomerRN.RNGCAdd = SourceCustomerRN.RNGAdd;
            } else {
                SourceCustomerRN.RNGCAdd = Utils.memergeAddFix(
                        customerrn.RNGCAdd_1,
                        customerrn.RNGCAdd_1_1,
                        customerrn.RNGCAdd_1_2,
                        customerrn.RNGCAdd_2,
                        customerrn.RNGCAdd_2_1,
                        customerrn.RNGCAdd_2_2,
                        customerrn.RNGCAdd_2_3,
                        customerrn.RNGCAdd_2_4,
                        customerrn.RNGCAdd_3,
                        customerrn.RNGCAdd_3_1,
                        customerrn.RNGCAdd_3_2,
                        customerrn.RNGCAdd_4,
                        customerrn.RNGCAdd_5,
                        customerrn.RNGCAdd_6,
                        customerrn.RNGCAdd_7,
                        customerrn.RNGCAdd_8,
                        customerrn.RNGCAdd_9);
            }

            #endregion 地址

            return SourceCustomerRN;
        }

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位
        /// </summary>
        /// <param name="SourceCustomerRC"></param>
        /// <param name="customerrn"></param>
        /// <returns></returns>
        public static CustomerRC HandleSpecificDataRC<T>(CustomerRC SourceCustomerRC, T inputDto) where T : class
        {
            var t = typeof(T);
            var props = t.GetProperties();
            var customerrc = new CustomerRC();
            var crcProps = typeof(CustomerRC).GetProperties();
            foreach (var prop in props) {
                var propInfo = crcProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                if (propInfo is not null) {
                    //var propinfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                    propInfo.SetValue(customerrc, prop.GetValue(inputDto));
                }
            }

            #region 同住者
            //同住者A
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_A)) {
                var splitChar = customerrc.RCFID_1_A.ToCharArray();
                SourceCustomerRC.RCFID_1_A_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_A_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_A_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_A_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_A_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_A_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_A_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_A_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_A_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_A_10 = splitChar[9].ToString();
            }
            //同住者B
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_B)) {
                var splitChar = customerrc.RCFID_1_B.ToCharArray();
                SourceCustomerRC.RCFID_1_B_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_B_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_B_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_B_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_B_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_B_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_B_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_B_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_B_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_B_10 = splitChar[9].ToString();
            }
            //同住者C
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_C)) {
                var splitChar = customerrc.RCFID_1_C.ToCharArray();
                SourceCustomerRC.RCFID_1_C_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_C_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_C_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_C_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_C_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_C_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_C_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_C_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_C_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_C_10 = splitChar[9].ToString();
            }
            //同住者D
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_D)) {
                var splitChar = customerrc.RCFID_1_D.ToCharArray();
                SourceCustomerRC.RCFID_1_D_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_D_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_D_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_D_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_D_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_D_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_D_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_D_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_D_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_D_10 = splitChar[9].ToString();
            }
            //同住者E
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_E)) {
                var splitChar = customerrc.RCFID_1_E.ToCharArray();
                SourceCustomerRC.RCFID_1_E_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_E_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_E_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_E_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_E_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_E_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_E_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_E_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_E_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_E_10 = splitChar[9].ToString();
            }
            //同住者F
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_F)) {
                var splitChar = customerrc.RCFID_1_F.ToCharArray();
                SourceCustomerRC.RCFID_1_F_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_F_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_F_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_F_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_F_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_F_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_F_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_F_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_F_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_F_10 = splitChar[9].ToString();
            }
            //同住者G
            if (!string.IsNullOrEmpty(customerrc.RCFID_1_G)) {
                var splitChar = customerrc.RCFID_1_G.ToCharArray();
                SourceCustomerRC.RCFID_1_G_1 = splitChar[0].ToString();
                SourceCustomerRC.RCFID_1_G_2 = splitChar[1].ToString();
                SourceCustomerRC.RCFID_1_G_3 = splitChar[2].ToString();
                SourceCustomerRC.RCFID_1_G_4 = splitChar[3].ToString();
                SourceCustomerRC.RCFID_1_G_5 = splitChar[4].ToString();
                SourceCustomerRC.RCFID_1_G_6 = splitChar[5].ToString();
                SourceCustomerRC.RCFID_1_G_7 = splitChar[6].ToString();
                SourceCustomerRC.RCFID_1_G_8 = splitChar[7].ToString();
                SourceCustomerRC.RCFID_1_G_9 = splitChar[8].ToString();
                SourceCustomerRC.RCFID_1_G_10 = splitChar[9].ToString();
            }
            #endregion 同住者

            #region 承租人
            //電話
            SourceCustomerRC.RCTel = customerrc.RCTel_1 + "-" + customerrc.RCTel_2;
            //手機
            //if (!string.IsNullOrEmpty(customerrc.RCCell)) {
            //    if (customerrc.RCCell.Length >= 4 && !customerrc.RCCell.Contains("-")) {
            //        SourceCustomerRC.RCCell = customerrc.RCCell.Insert(4, "-");
            //    }
            //}
            //身分證字號
            if (!string.IsNullOrEmpty(customerrc.RCID)) {
                var idSplit = customerrc.RCID.ToCharArray();
                SourceCustomerRC.RCID_1_1 = idSplit[0].ToString();
                SourceCustomerRC.RCID_1_2 = idSplit[1].ToString();
                SourceCustomerRC.RCID_1_3 = idSplit[2].ToString();
                SourceCustomerRC.RCID_1_4 = idSplit[3].ToString();
                SourceCustomerRC.RCID_1_5 = idSplit[4].ToString();
                SourceCustomerRC.RCID_1_6 = idSplit[5].ToString();
                SourceCustomerRC.RCID_1_7 = idSplit[6].ToString();
                SourceCustomerRC.RCID_1_8 = idSplit[7].ToString();
            }

            #endregion 承租人

            #region 地址

            //承租人戶籍地址
            SourceCustomerRC.RCAdd = Utils.memergeAddFix(
                customerrc.RCAdd_1,
                customerrc.RCAdd_1_1,
                customerrc.RCAdd_1_2,
                customerrc.RCAdd_2,
                customerrc.RCAdd_2_1,
                customerrc.RCAdd_2_2,
                customerrc.RCAdd_2_3,
                customerrc.RCAdd_2_4,
                customerrc.RCAdd_3,
                customerrc.RCAdd_3_1,
                customerrc.RCAdd_3_2,
                customerrc.RCAdd_4,
                customerrc.RCAdd_5,
                customerrc.RCAdd_6,
                customerrc.RCAdd_7,
                customerrc.RCAdd_8,
                customerrc.RCAdd_9);
            //承租人通訊地址
            if (customerrc.RCAddSame == "/Yes") {
                SourceCustomerRC.RCAddC_1 = customerrc.RCAdd_1;
                SourceCustomerRC.RCAddC_1_1 = customerrc.RCAdd_1_1;
                SourceCustomerRC.RCAddC_1_2 = customerrc.RCAdd_1_2;
                SourceCustomerRC.RCAddC_2 = customerrc.RCAdd_2;
                SourceCustomerRC.RCAddC_2_1 = customerrc.RCAdd_2_1;
                SourceCustomerRC.RCAddC_2_2 = customerrc.RCAdd_2_2;
                SourceCustomerRC.RCAddC_2_3 = customerrc.RCAdd_2_3;
                SourceCustomerRC.RCAddC_2_4 = customerrc.RCAdd_2_4;
                SourceCustomerRC.RCAddC_3 = customerrc.RCAdd_3;
                SourceCustomerRC.RCAddC_3_1 = customerrc.RCAdd_3_1;
                SourceCustomerRC.RCAddC_3_2 = customerrc.RCAdd_3_2;
                SourceCustomerRC.RCAddC_4 = customerrc.RCAdd_4;
                SourceCustomerRC.RCAddC_5 = customerrc.RCAdd_5;
                SourceCustomerRC.RCAddC_6 = customerrc.RCAdd_6;
                SourceCustomerRC.RCAddC_7 = customerrc.RCAdd_7;
                SourceCustomerRC.RCAddC_8 = customerrc.RCAdd_8;
                SourceCustomerRC.RCAddC_9 = customerrc.RCAdd_9;
                SourceCustomerRC.RCAddC = SourceCustomerRC.RCAdd;
            } else {
                SourceCustomerRC.RCAddC = Utils.memergeAddFix(
                        customerrc.RCAddC_1,
                        customerrc.RCAddC_1_1,
                        customerrc.RCAddC_1_2,
                        customerrc.RCAddC_2,
                        customerrc.RCAddC_2_1,
                        customerrc.RCAddC_2_2,
                        customerrc.RCAddC_2_3,
                        customerrc.RCAddC_2_4,
                        customerrc.RCAddC_3,
                        customerrc.RCAddC_3_1,
                        customerrc.RCAddC_3_2,
                        customerrc.RCAddC_4,
                        customerrc.RCAddC_5,
                        customerrc.RCAddC_6,
                        customerrc.RCAddC_7,
                        customerrc.RCAddC_8,
                        customerrc.RCAddC_9);
            }

            //緊急聯絡人戶籍地址
            SourceCustomerRC.RCECAdd = Utils.memergeAddFix(
                customerrc.RCECAdd_1,
                customerrc.RCECAdd_1_1,
                customerrc.RCECAdd_1_2,
                customerrc.RCECAdd_2,
                customerrc.RCECAdd_2_1,
                customerrc.RCECAdd_2_2,
                customerrc.RCECAdd_2_3,
                customerrc.RCECAdd_2_4,
                customerrc.RCECAdd_3,
                customerrc.RCECAdd_3_1,
                customerrc.RCECAdd_3_2,
                customerrc.RCECAdd_4,
                customerrc.RCECAdd_5,
                customerrc.RCECAdd_6,
                customerrc.RCECAdd_7,
                customerrc.RCECAdd_8,
                customerrc.RCECAdd_9);
            //緊急聯絡人通訊地址
            if (customerrc.RCECAddSame == "/Yes") {
                SourceCustomerRC.RCECAddC_1 = customerrc.RCECAdd_1;
                SourceCustomerRC.RCECAddC_1_1 = customerrc.RCECAdd_1_1;
                SourceCustomerRC.RCECAddC_1_2 = customerrc.RCECAdd_1_2;
                SourceCustomerRC.RCECAddC_2 = customerrc.RCECAdd_2;
                SourceCustomerRC.RCECAddC_2_1 = customerrc.RCECAdd_2_1;
                SourceCustomerRC.RCECAddC_2_2 = customerrc.RCECAdd_2_2;
                SourceCustomerRC.RCECAddC_2_3 = customerrc.RCECAdd_2_3;
                SourceCustomerRC.RCECAddC_2_4 = customerrc.RCECAdd_2_4;
                SourceCustomerRC.RCECAddC_3 = customerrc.RCECAdd_3;
                SourceCustomerRC.RCECAddC_3_1 = customerrc.RCECAdd_3_1;
                SourceCustomerRC.RCECAddC_3_2 = customerrc.RCECAdd_3_2;
                SourceCustomerRC.RCECAddC_4 = customerrc.RCECAdd_4;
                SourceCustomerRC.RCECAddC_5 = customerrc.RCECAdd_5;
                SourceCustomerRC.RCECAddC_6 = customerrc.RCECAdd_6;
                SourceCustomerRC.RCECAddC_7 = customerrc.RCECAdd_7;
                SourceCustomerRC.RCECAddC_8 = customerrc.RCECAdd_8;
                SourceCustomerRC.RCECAddC_9 = customerrc.RCECAdd_9;
                SourceCustomerRC.RCECAddC = SourceCustomerRC.RCECAdd;
            } else {
                SourceCustomerRC.RCECAddC = Utils.memergeAddFix(
                        customerrc.RCECAddC_1,
                        customerrc.RCECAddC_1_1,
                        customerrc.RCECAddC_1_2,
                        customerrc.RCECAddC_2,
                        customerrc.RCECAddC_2_1,
                        customerrc.RCECAddC_2_2,
                        customerrc.RCECAddC_2_3,
                        customerrc.RCECAddC_2_4,
                        customerrc.RCECAddC_3,
                        customerrc.RCECAddC_3_1,
                        customerrc.RCECAddC_3_2,
                        customerrc.RCECAddC_4,
                        customerrc.RCECAddC_5,
                        customerrc.RCECAddC_6,
                        customerrc.RCECAddC_7,
                        customerrc.RCECAddC_8,
                        customerrc.RCECAddC_9);
            }

            //緊急聯絡人(朋友)戶籍地址
            SourceCustomerRC.RCECFAdd = Utils.memergeAddFix(
                customerrc.RCECFAdd_1,
                customerrc.RCECFAdd_1_1,
                customerrc.RCECFAdd_1_2,
                customerrc.RCECFAdd_2,
                customerrc.RCECFAdd_2_1,
                customerrc.RCECFAdd_2_2,
                customerrc.RCECFAdd_2_3,
                customerrc.RCECFAdd_2_4,
                customerrc.RCECFAdd_3,
                customerrc.RCECFAdd_3_1,
                customerrc.RCECFAdd_3_2,
                customerrc.RCECFAdd_4,
                customerrc.RCECFAdd_5,
                customerrc.RCECFAdd_6,
                customerrc.RCECFAdd_7,
                customerrc.RCECFAdd_8,
                customerrc.RCECFAdd_9);
            //緊急聯絡人通訊地址
            if (customerrc.RCECFAddSame == "/Yes") {
                SourceCustomerRC.RCECFAddC_1 = customerrc.RCECFAdd_1;
                SourceCustomerRC.RCECFAddC_1_1 = customerrc.RCECFAdd_1_1;
                SourceCustomerRC.RCECFAddC_1_2 = customerrc.RCECFAdd_1_2;
                SourceCustomerRC.RCECFAddC_2 = customerrc.RCECFAdd_2;
                SourceCustomerRC.RCECFAddC_2_1 = customerrc.RCECFAdd_2_1;
                SourceCustomerRC.RCECFAddC_2_2 = customerrc.RCECFAdd_2_2;
                SourceCustomerRC.RCECFAddC_2_3 = customerrc.RCECFAdd_2_3;
                SourceCustomerRC.RCECFAddC_2_4 = customerrc.RCECFAdd_2_4;
                SourceCustomerRC.RCECFAddC_3 = customerrc.RCECFAdd_3;
                SourceCustomerRC.RCECFAddC_3_1 = customerrc.RCECFAdd_3_1;
                SourceCustomerRC.RCECFAddC_3_2 = customerrc.RCECFAdd_3_2;
                SourceCustomerRC.RCECFAddC_4 = customerrc.RCECFAdd_4;
                SourceCustomerRC.RCECFAddC_5 = customerrc.RCECFAdd_5;
                SourceCustomerRC.RCECFAddC_6 = customerrc.RCECFAdd_6;
                SourceCustomerRC.RCECFAddC_7 = customerrc.RCECFAdd_7;
                SourceCustomerRC.RCECFAddC_8 = customerrc.RCECFAdd_8;
                SourceCustomerRC.RCECFAddC_9 = customerrc.RCECFAdd_9;
                SourceCustomerRC.RCECFAddC = SourceCustomerRC.RCECFAdd;
            } else {
                SourceCustomerRC.RCECFAddC = Utils.memergeAddFix(
                        customerrc.RCECFAddC_1,
                        customerrc.RCECFAddC_1_1,
                        customerrc.RCECFAddC_1_2,
                        customerrc.RCECFAddC_2,
                        customerrc.RCECFAddC_2_1,
                        customerrc.RCECFAddC_2_2,
                        customerrc.RCECFAddC_2_3,
                        customerrc.RCECFAddC_2_4,
                        customerrc.RCECFAddC_3,
                        customerrc.RCECFAddC_3_1,
                        customerrc.RCECFAddC_3_2,
                        customerrc.RCECFAddC_4,
                        customerrc.RCECFAddC_5,
                        customerrc.RCECFAddC_6,
                        customerrc.RCECFAddC_7,
                        customerrc.RCECFAddC_8,
                        customerrc.RCECFAddC_9);
            }

            //代理人戶籍地址
            SourceCustomerRC.RCAGAdd = Utils.memergeAddFix(
                customerrc.RCAGAdd_1,
                customerrc.RCAGAdd_1_1,
                customerrc.RCAGAdd_1_2,
                customerrc.RCAGAdd_2,
                customerrc.RCAGAdd_2_1,
                customerrc.RCAGAdd_2_2,
                customerrc.RCAGAdd_2_3,
                customerrc.RCAGAdd_2_4,
                customerrc.RCAGAdd_3,
                customerrc.RCAGAdd_3_1,
                customerrc.RCAGAdd_3_2,
                customerrc.RCAGAdd_4,
                customerrc.RCAGAdd_5,
                customerrc.RCAGAdd_6,
                customerrc.RCAGAdd_7,
                customerrc.RCAGAdd_8,
                customerrc.RCAGAdd_9);
            //代理人通訊地址
            if (customerrc.RCAGAddSame == "/Yes") {
                SourceCustomerRC.RCAGAddC_1 = customerrc.RCAGAdd_1;
                SourceCustomerRC.RCAGAddC_1_1 = customerrc.RCAGAdd_1_1;
                SourceCustomerRC.RCAGAddC_1_2 = customerrc.RCAGAdd_1_2;
                SourceCustomerRC.RCAGAddC_2 = customerrc.RCAGAdd_2;
                SourceCustomerRC.RCAGAddC_2_1 = customerrc.RCAGAdd_2_1;
                SourceCustomerRC.RCAGAddC_2_2 = customerrc.RCAGAdd_2_2;
                SourceCustomerRC.RCAGAddC_2_3 = customerrc.RCAGAdd_2_3;
                SourceCustomerRC.RCAGAddC_2_4 = customerrc.RCAGAdd_2_4;
                SourceCustomerRC.RCAGAddC_3 = customerrc.RCAGAdd_3;
                SourceCustomerRC.RCAGAddC_3_1 = customerrc.RCAGAdd_3_1;
                SourceCustomerRC.RCAGAddC_3_2 = customerrc.RCAGAdd_3_2;
                SourceCustomerRC.RCAGAddC_4 = customerrc.RCAGAdd_4;
                SourceCustomerRC.RCAGAddC_5 = customerrc.RCAGAdd_5;
                SourceCustomerRC.RCAGAddC_6 = customerrc.RCAGAdd_6;
                SourceCustomerRC.RCAGAddC_7 = customerrc.RCAGAdd_7;
                SourceCustomerRC.RCAGAddC_8 = customerrc.RCAGAdd_8;
                SourceCustomerRC.RCAGAddC_9 = customerrc.RCAGAdd_9;
                SourceCustomerRC.RCAGAddC = SourceCustomerRC.RCAGAdd;
            } else {
                SourceCustomerRC.RCAGAddC = Utils.memergeAddFix(
                        customerrc.RCAGAddC_1,
                        customerrc.RCAGAddC_1_1,
                        customerrc.RCAGAddC_1_2,
                        customerrc.RCAGAddC_2,
                        customerrc.RCAGAddC_2_1,
                        customerrc.RCAGAddC_2_2,
                        customerrc.RCAGAddC_2_3,
                        customerrc.RCAGAddC_2_4,
                        customerrc.RCAGAddC_3,
                        customerrc.RCAGAddC_3_1,
                        customerrc.RCAGAddC_3_2,
                        customerrc.RCAGAddC_4,
                        customerrc.RCAGAddC_5,
                        customerrc.RCAGAddC_6,
                        customerrc.RCAGAddC_7,
                        customerrc.RCAGAddC_8,
                        customerrc.RCAGAddC_9);
            }

            //保證人戶籍地址
            SourceCustomerRC.RCGAdd = Utils.memergeAddFix(
                customerrc.RCGAdd_1,
                customerrc.RCGAdd_1_1,
                customerrc.RCGAdd_1_2,
                customerrc.RCGAdd_2,
                customerrc.RCGAdd_2_1,
                customerrc.RCGAdd_2_2,
                customerrc.RCGAdd_2_3,
                customerrc.RCGAdd_2_4,
                customerrc.RCGAdd_3,
                customerrc.RCGAdd_3_1,
                customerrc.RCGAdd_3_2,
                customerrc.RCGAdd_4,
                customerrc.RCGAdd_5,
                customerrc.RCGAdd_6,
                customerrc.RCGAdd_7,
                customerrc.RCGAdd_8,
                customerrc.RCGAdd_9);
            //保證人通訊地址
            if (customerrc.RCGAddSame == "/Yes") {
                SourceCustomerRC.RCGCAdd_1 = customerrc.RCGAdd_1;
                SourceCustomerRC.RCGCAdd_1_1 = customerrc.RCGAdd_1_1;
                SourceCustomerRC.RCGCAdd_1_2 = customerrc.RCGAdd_1_2;
                SourceCustomerRC.RCGCAdd_2 = customerrc.RCGAdd_2;
                SourceCustomerRC.RCGCAdd_2_1 = customerrc.RCGAdd_2_1;
                SourceCustomerRC.RCGCAdd_2_2 = customerrc.RCGAdd_2_2;
                SourceCustomerRC.RCGCAdd_2_3 = customerrc.RCGAdd_2_3;
                SourceCustomerRC.RCGCAdd_2_4 = customerrc.RCGAdd_2_4;
                SourceCustomerRC.RCGCAdd_3 = customerrc.RCGAdd_3;
                SourceCustomerRC.RCGCAdd_3_1 = customerrc.RCGAdd_3_1;
                SourceCustomerRC.RCGCAdd_3_2 = customerrc.RCGAdd_3_2;
                SourceCustomerRC.RCGCAdd_4 = customerrc.RCGAdd_4;
                SourceCustomerRC.RCGCAdd_5 = customerrc.RCGAdd_5;
                SourceCustomerRC.RCGCAdd_6 = customerrc.RCGAdd_6;
                SourceCustomerRC.RCGCAdd_7 = customerrc.RCGAdd_7;
                SourceCustomerRC.RCGCAdd_8 = customerrc.RCGAdd_8;
                SourceCustomerRC.RCGCAdd_9 = customerrc.RCGAdd_9;
                SourceCustomerRC.RCGCAdd = SourceCustomerRC.RCGAdd;
            } else {
                SourceCustomerRC.RCGCAdd = Utils.memergeAddFix(
                        customerrc.RCGCAdd_1,
                        customerrc.RCGCAdd_1_1,
                        customerrc.RCGCAdd_1_2,
                        customerrc.RCGCAdd_2,
                        customerrc.RCGCAdd_2_1,
                        customerrc.RCGCAdd_2_2,
                        customerrc.RCGCAdd_2_3,
                        customerrc.RCGCAdd_2_4,
                        customerrc.RCGCAdd_3,
                        customerrc.RCGCAdd_3_1,
                        customerrc.RCGCAdd_3_2,
                        customerrc.RCGCAdd_4,
                        customerrc.RCGCAdd_5,
                        customerrc.RCGCAdd_6,
                        customerrc.RCGCAdd_7,
                        customerrc.RCGCAdd_8,
                        customerrc.RCGCAdd_9);
            }

            #endregion 地址

            return SourceCustomerRC;
        }

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位
        /// </summary>
        /// <returns></returns>
        public static CustomerLN HandleSpecificDataLN<T>(CustomerLN SourceCustomerLN, T inputDto) where T : class
        {
            var t = typeof(T);
            var props = t.GetProperties();
            var customerln = new CustomerLN();
            var clnProps = typeof(CustomerLN).GetProperties();
            foreach (var prop in props) {
                var propInfo = clnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                if (propInfo is not null) {
                    //var propinfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                    propInfo.SetValue(customerln, prop.GetValue(inputDto));
                }
            }

            #region LS欄位

            #region 戶籍地址

            var hasLSAddProp = t.GetProperty("LSAdd_1");
            if (hasLSAddProp is not null) {
                var lsAdd_1 = hasLSAddProp.GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsAdd_1)) {
                    SourceCustomerLN.LNAdd_1 = lsAdd_1;
                    customerln.LNAdd_1 = lsAdd_1;
                    var lsAdd_1_1 = t.GetProperty("LSAdd_1_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_1_2 = t.GetProperty("LSAdd_1_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2 = t.GetProperty("LSAdd_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_1 = t.GetProperty("LSAdd_2_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_2 = t.GetProperty("LSAdd_2_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_3 = t.GetProperty("LSAdd_2_3")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_4 = t.GetProperty("LSAdd_2_4")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3 = t.GetProperty("LSAdd_3")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3_1 = t.GetProperty("LSAdd_3_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3_2 = t.GetProperty("LSAdd_3_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_4 = t.GetProperty("LSAdd_4")?.GetValue(inputDto)?.ToString();
                    var lsAdd_5 = t.GetProperty("LSAdd_5")?.GetValue(inputDto)?.ToString();
                    var lsAdd_6 = t.GetProperty("LSAdd_6")?.GetValue(inputDto)?.ToString();
                    var lsAdd_7 = t.GetProperty("LSAdd_7")?.GetValue(inputDto)?.ToString();
                    var lsAdd_8 = t.GetProperty("LSAdd_8")?.GetValue(inputDto)?.ToString();
                    var lsAdd_9 = t.GetProperty("LSAdd_9")?.GetValue(inputDto)?.ToString();

                    if (!string.IsNullOrEmpty(lsAdd_1_1)) {
                        SourceCustomerLN.LNAdd_1_1 = lsAdd_1_1;
                        customerln.LNAdd_1_1 = lsAdd_1_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_1_2)) {
                        SourceCustomerLN.LNAdd_1_2 = lsAdd_1_2;
                        customerln.LNAdd_1_2 = lsAdd_1_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2)) {
                        SourceCustomerLN.LNAdd_2 = lsAdd_2;
                        customerln.LNAdd_2 = lsAdd_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_1)) {
                        SourceCustomerLN.LNAdd_2_1 = lsAdd_2_1;
                        customerln.LNAdd_2_1 = lsAdd_2_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_2)) {
                        SourceCustomerLN.LNAdd_2_2 = lsAdd_2_2;
                        customerln.LNAdd_2_2 = lsAdd_2_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_3)) {
                        SourceCustomerLN.LNAdd_2_3 = lsAdd_2_3;
                        customerln.LNAdd_2_3 = lsAdd_2_3;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_4)) {
                        SourceCustomerLN.LNAdd_2_4 = lsAdd_2_4;
                        customerln.LNAdd_2_4 = lsAdd_2_4;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3)) {
                        SourceCustomerLN.LNAdd_3 = lsAdd_3;
                        customerln.LNAdd_3 = lsAdd_3;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3_1)) {
                        SourceCustomerLN.LNAdd_3_1 = lsAdd_3_1;
                        customerln.LNAdd_3_1 = lsAdd_3_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3_2)) {
                        SourceCustomerLN.LNAdd_3_2 = lsAdd_3_2;
                        customerln.LNAdd_3_2 = lsAdd_3_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_4)) {
                        SourceCustomerLN.LNAdd_4 = lsAdd_4;
                        customerln.LNAdd_4 = lsAdd_4;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_5)) {
                        SourceCustomerLN.LNAdd_5 = lsAdd_5;
                        customerln.LNAdd_5 = lsAdd_5;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_6)) {
                        SourceCustomerLN.LNAdd_6 = lsAdd_6;
                        customerln.LNAdd_6 = lsAdd_6;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_7)) {
                        SourceCustomerLN.LNAdd_7 = lsAdd_7;
                        customerln.LNAdd_7 = lsAdd_7;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_8)) {
                        SourceCustomerLN.LNAdd_8 = lsAdd_8;
                        customerln.LNAdd_8 = lsAdd_8;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_9)) {
                        SourceCustomerLN.LNAdd_9 = lsAdd_9;
                        customerln.LNAdd_9 = lsAdd_9;
                    }
                }
            }
            #endregion 戶籍地址

            #region 電話

            var haslsTelProp = t.GetProperty("LSTel_1");
            if (haslsTelProp is not null) {
                var lsTel_1 = t.GetProperty("LSTel_1").GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsTel_1))
                    SourceCustomerLN.LNTel_1 = lsTel_1;
                var lsTel_2 = t.GetProperty("LSTel_2").GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsTel_2))
                    SourceCustomerLN.LNTel_2 = lsTel_2;
            }

            #endregion 電話

            #region 身分證字號

            var haslsidProp = t.GetProperty("LSID");
            if (haslsidProp is not null) {
                var lsid = t.GetProperty("LSID").GetValue(inputDto)?.ToString();
                customerln.LNID = lsid;
            }

            #endregion 身分證字號

            #endregion LS欄位

            #region 出租人

            //生日
            if (!string.IsNullOrEmpty(customerln.LNBirthday)) {
                SourceCustomerLN.LNBirthday_Y = customerln.LNBirthday.Split("-")[0];
                SourceCustomerLN.LNBirthday_M = customerln.LNBirthday.Split("-")[1];
                SourceCustomerLN.LNBirthday_D = customerln.LNBirthday.Split("-")[2];
            }

            //電話
            SourceCustomerLN.LNTel = customerln.LNTel_1 + "-" + customerln.LNTel_2;
            SourceCustomerLN.LNTelN = customerln.LNTelN_1 + "-" + customerln.LNTelN_2;
            SourceCustomerLN.LNAGTel_A = customerln.LNAGTel_1_A + "-" + customerln.LNAGTel_2_A;
            SourceCustomerLN.LNAGTel_B = customerln.LNAGTel_1_B + "-" + customerln.LNAGTel_2_B;

            ////手機
            //if (!string.IsNullOrEmpty(customerln.LNCell)) {
            //    if (customerln.LNCell.Length >= 4 && !customerln.LNCell.Contains("-")) {
            //        SourceCustomerLN.LNCell = customerln.LNCell.Insert(4, "-");
            //    }
            //}

            //身分證字號
            if (!string.IsNullOrEmpty(customerln.LNID)) {
                var idSplit = customerln.LNID.ToCharArray();
                SourceCustomerLN.LNID_1_1 = idSplit[0].ToString();
                SourceCustomerLN.LNID_1_2 = idSplit[1].ToString();
                SourceCustomerLN.LNID_1_3 = idSplit[2].ToString();
                SourceCustomerLN.LNID_1_4 = idSplit[3].ToString();
                SourceCustomerLN.LNID_1_5 = idSplit[4].ToString();
                SourceCustomerLN.LNID_1_6 = idSplit[5].ToString();
                SourceCustomerLN.LNID_1_7 = idSplit[6].ToString();
                SourceCustomerLN.LNID_1_8 = idSplit[7].ToString();
                SourceCustomerLN.LNID_1_9 = idSplit[8].ToString();
                SourceCustomerLN.LNID_1_10 = idSplit[9].ToString();
            }

            #endregion

            #region 地址

            //出租人戶籍地址
            SourceCustomerLN.LNAdd = Utils.memergeAddFix(
                customerln.LNAdd_1,
                customerln.LNAdd_1_1,
                customerln.LNAdd_1_2,
                customerln.LNAdd_2,
                customerln.LNAdd_2_1,
                customerln.LNAdd_2_2,
                customerln.LNAdd_2_3,
                customerln.LNAdd_2_4,
                customerln.LNAdd_3,
                customerln.LNAdd_3_1,
                customerln.LNAdd_3_2,
                customerln.LNAdd_4,
                customerln.LNAdd_5,
                customerln.LNAdd_6,
                customerln.LNAdd_7,
                customerln.LNAdd_8,
                customerln.LNAdd_9);
            //出租人通訊地址
            if (customerln.LNAddSame == "/Yes") {
                SourceCustomerLN.LNAddC_1 = customerln.LNAdd_1;
                SourceCustomerLN.LNAddC_1_1 = customerln.LNAdd_1_1;
                SourceCustomerLN.LNAddC_1_2 = customerln.LNAdd_1_2;
                SourceCustomerLN.LNAddC_2 = customerln.LNAdd_2;
                SourceCustomerLN.LNAddC_2_1 = customerln.LNAdd_2_1;
                SourceCustomerLN.LNAddC_2_2 = customerln.LNAdd_2_2;
                SourceCustomerLN.LNAddC_2_3 = customerln.LNAdd_2_3;
                SourceCustomerLN.LNAddC_2_4 = customerln.LNAdd_2_4;
                SourceCustomerLN.LNAddC_3 = customerln.LNAdd_3;
                SourceCustomerLN.LNAddC_3_1 = customerln.LNAdd_3_1;
                SourceCustomerLN.LNAddC_3_2 = customerln.LNAdd_3_2;
                SourceCustomerLN.LNAddC_4 = customerln.LNAdd_4;
                SourceCustomerLN.LNAddC_5 = customerln.LNAdd_5;
                SourceCustomerLN.LNAddC_6 = customerln.LNAdd_6;
                SourceCustomerLN.LNAddC_7 = customerln.LNAdd_7;
                SourceCustomerLN.LNAddC_8 = customerln.LNAdd_8;
                SourceCustomerLN.LNAddC_9 = customerln.LNAdd_9;
                SourceCustomerLN.LNAddC = SourceCustomerLN.LNAdd;
            } else {
                SourceCustomerLN.LNAddC = Utils.memergeAddFix(
                        customerln.LNAddC_1,
                        customerln.LNAddC_1_1,
                        customerln.LNAddC_1_2,
                        customerln.LNAddC_2,
                        customerln.LNAddC_2_1,
                        customerln.LNAddC_2_2,
                        customerln.LNAddC_2_3,
                        customerln.LNAddC_2_4,
                        customerln.LNAddC_3,
                        customerln.LNAddC_3_1,
                        customerln.LNAddC_3_2,
                        customerln.LNAddC_4,
                        customerln.LNAddC_5,
                        customerln.LNAddC_6,
                        customerln.LNAddC_7,
                        customerln.LNAddC_8,
                        customerln.LNAddC_9);
            }
            //代理人戶籍地址
            SourceCustomerLN.LNAGAdd_A = Utils.memergeAddFix(
                customerln.LNAGAdd_1_A,
                customerln.LNAGAdd_1_1_A,
                customerln.LNAGAdd_1_2_A,
                customerln.LNAGAdd_2_A,
                customerln.LNAGAdd_2_1_A,
                customerln.LNAGAdd_2_2_A,
                customerln.LNAGAdd_2_3_A,
                customerln.LNAGAdd_2_4_A,
                customerln.LNAGAdd_3_A,
                customerln.LNAGAdd_3_1_A,
                customerln.LNAGAdd_3_2_A,
                customerln.LNAGAdd_4_A,
                customerln.LNAGAdd_5_A,
                customerln.LNAGAdd_6_A,
                customerln.LNAGAdd_7_A,
                customerln.LNAGAdd_8_A,
                customerln.LNAGAdd_9_A);
            //代理人通訊地址
            if (customerln.LNAGAddSame == "/Yes") {
                SourceCustomerLN.LNAGAddC_1_A = customerln.LNAGAdd_1_A;
                SourceCustomerLN.LNAGAddC_1_1_A = customerln.LNAGAdd_1_1_A;
                SourceCustomerLN.LNAGAddC_1_2_A = customerln.LNAGAdd_1_2_A;
                SourceCustomerLN.LNAGAddC_2_A = customerln.LNAGAdd_2_A;
                SourceCustomerLN.LNAGAddC_2_1_A = customerln.LNAGAdd_2_1_A;
                SourceCustomerLN.LNAGAddC_2_2_A = customerln.LNAGAdd_2_2_A;
                SourceCustomerLN.LNAGAddC_2_3_A = customerln.LNAGAdd_2_3_A;
                SourceCustomerLN.LNAGAddC_2_4_A = customerln.LNAGAdd_2_4_A;
                SourceCustomerLN.LNAGAddC_3_A = customerln.LNAGAdd_3_A;
                SourceCustomerLN.LNAGAddC_3_1_A = customerln.LNAGAdd_3_1_A;
                SourceCustomerLN.LNAGAddC_3_2_A = customerln.LNAGAdd_3_2_A;
                SourceCustomerLN.LNAGAddC_4_A = customerln.LNAGAdd_4_A;
                SourceCustomerLN.LNAGAddC_5_A = customerln.LNAGAdd_5_A;
                SourceCustomerLN.LNAGAddC_6_A = customerln.LNAGAdd_6_A;
                SourceCustomerLN.LNAGAddC_7_A = customerln.LNAGAdd_7_A;
                SourceCustomerLN.LNAGAddC_8_A = customerln.LNAGAdd_8_A;
                SourceCustomerLN.LNAGAddC_9_A = customerln.LNAGAdd_9_A;
                SourceCustomerLN.LNAGAddC_A = SourceCustomerLN.LNAGAdd_A;
            } else {
                SourceCustomerLN.LNAGAddC_A = Utils.memergeAddFix(
                        customerln.LNAGAddC_1_A,
                        customerln.LNAGAddC_1_1_A,
                        customerln.LNAGAddC_1_2_A,
                        customerln.LNAGAddC_2_A,
                        customerln.LNAGAddC_2_1_A,
                        customerln.LNAGAddC_2_2_A,
                        customerln.LNAGAddC_2_3_A,
                        customerln.LNAGAddC_2_4_A,
                        customerln.LNAGAddC_3_A,
                        customerln.LNAGAddC_3_1_A,
                        customerln.LNAGAddC_3_2_A,
                        customerln.LNAGAddC_4_A,
                        customerln.LNAGAddC_5_A,
                        customerln.LNAGAddC_6_A,
                        customerln.LNAGAddC_7_A,
                        customerln.LNAGAddC_8_A,
                        customerln.LNAGAddC_9_A);
            }
            //SourceCustomerLN.LNAGAdd_B = Utils.memergeAddFix(
            //                customerln.LNAGAdd_1_B,
            //                customerln.LNAGAdd_1_1_B,
            //                customerln.LNAGAdd_1_2_B,
            //                customerln.LNAGAdd_2_B,
            //                customerln.LNAGAdd_2_1_B,
            //                customerln.LNAGAdd_2_2_B,
            //                customerln.LNAGAdd_2_3_B,
            //                customerln.LNAGAdd_2_4_B,
            //                customerln.LNAGAdd_3_B,
            //                customerln.LNAGAdd_3_1_B,
            //                customerln.LNAGAdd_3_2_B,
            //                customerln.LNAGAdd_4_B,
            //                customerln.LNAGAdd_5_B,
            //                customerln.LNAGAdd_6_B,
            //                customerln.LNAGAdd_7_B,
            //                customerln.LNAGAdd_8_B,
            //                customerln.LNAGAdd_9_B);

            #endregion

            return SourceCustomerLN;
        }

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位
        /// </summary>
        /// <returns></returns>
        public static CustomerLC HandleSpecificDataLC<T>(CustomerLC SourceCustomerLC, T inputDto) where T : class
        {
            var t = typeof(T);
            var props = t.GetProperties();
            var customerlc = new CustomerLC();
            var clnProps = typeof(CustomerLC).GetProperties();
            foreach (var prop in props) {
                var propInfo = clnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                if (propInfo is not null) {
                    //var propinfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                    propInfo.SetValue(customerlc, prop.GetValue(inputDto));
                }
            }

            #region LS欄位

            #region 戶籍地址

            var hasLSAddProp = t.GetProperty("LSAdd_1");
            if (hasLSAddProp is not null) {
                var lsAdd_1 = hasLSAddProp.GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsAdd_1)) {
                    SourceCustomerLC.LCAdd_1 = lsAdd_1;
                    customerlc.LCAdd_1 = lsAdd_1;
                    var lsAdd_1_1 = t.GetProperty("LSAdd_1_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_1_2 = t.GetProperty("LSAdd_1_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2 = t.GetProperty("LSAdd_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_1 = t.GetProperty("LSAdd_2_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_2 = t.GetProperty("LSAdd_2_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_3 = t.GetProperty("LSAdd_2_3")?.GetValue(inputDto)?.ToString();
                    var lsAdd_2_4 = t.GetProperty("LSAdd_2_4")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3 = t.GetProperty("LSAdd_3")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3_1 = t.GetProperty("LSAdd_3_1")?.GetValue(inputDto)?.ToString();
                    var lsAdd_3_2 = t.GetProperty("LSAdd_3_2")?.GetValue(inputDto)?.ToString();
                    var lsAdd_4 = t.GetProperty("LSAdd_4")?.GetValue(inputDto)?.ToString();
                    var lsAdd_5 = t.GetProperty("LSAdd_5")?.GetValue(inputDto)?.ToString();
                    var lsAdd_6 = t.GetProperty("LSAdd_6")?.GetValue(inputDto)?.ToString();
                    var lsAdd_7 = t.GetProperty("LSAdd_7")?.GetValue(inputDto)?.ToString();
                    var lsAdd_8 = t.GetProperty("LSAdd_8")?.GetValue(inputDto)?.ToString();
                    var lsAdd_9 = t.GetProperty("LSAdd_9")?.GetValue(inputDto)?.ToString();

                    if (!string.IsNullOrEmpty(lsAdd_1_1)) {
                        SourceCustomerLC.LCAdd_1_1 = lsAdd_1_1;
                        customerlc.LCAdd_1_1 = lsAdd_1_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_1_2)) {
                        SourceCustomerLC.LCAdd_1_2 = lsAdd_1_2;
                        customerlc.LCAdd_1_2 = lsAdd_1_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2)) {
                        SourceCustomerLC.LCAdd_2 = lsAdd_2;
                        customerlc.LCAdd_2 = lsAdd_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_1)) {
                        SourceCustomerLC.LCAdd_2_1 = lsAdd_2_1;
                        customerlc.LCAdd_2_1 = lsAdd_2_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_2)) {
                        SourceCustomerLC.LCAdd_2_2 = lsAdd_2_2;
                        customerlc.LCAdd_2_2 = lsAdd_2_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_3)) {
                        SourceCustomerLC.LCAdd_2_3 = lsAdd_2_3;
                        customerlc.LCAdd_2_3 = lsAdd_2_3;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_2_4)) {
                        SourceCustomerLC.LCAdd_2_4 = lsAdd_2_4;
                        customerlc.LCAdd_2_4 = lsAdd_2_4;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3)) {
                        SourceCustomerLC.LCAdd_3 = lsAdd_3;
                        customerlc.LCAdd_3 = lsAdd_3;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3_1)) {
                        SourceCustomerLC.LCAdd_3_1 = lsAdd_3_1;
                        customerlc.LCAdd_3_1 = lsAdd_3_1;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_3_2)) {
                        SourceCustomerLC.LCAdd_3_2 = lsAdd_3_2;
                        customerlc.LCAdd_3_2 = lsAdd_3_2;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_4)) {
                        SourceCustomerLC.LCAdd_4 = lsAdd_4;
                        customerlc.LCAdd_4 = lsAdd_4;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_5)) {
                        SourceCustomerLC.LCAdd_5 = lsAdd_5;
                        customerlc.LCAdd_5 = lsAdd_5;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_6)) {
                        SourceCustomerLC.LCAdd_6 = lsAdd_6;
                        customerlc.LCAdd_6 = lsAdd_6;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_7)) {
                        SourceCustomerLC.LCAdd_7 = lsAdd_7;
                        customerlc.LCAdd_7 = lsAdd_7;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_8)) {
                        SourceCustomerLC.LCAdd_8 = lsAdd_8;
                        customerlc.LCAdd_8 = lsAdd_8;
                    }
                    if (!string.IsNullOrEmpty(lsAdd_9)) {
                        SourceCustomerLC.LCAdd_9 = lsAdd_9;
                        customerlc.LCAdd_9 = lsAdd_9;
                    }
                }
            }
            #endregion 戶籍地址

            #region 電話

            var haslsTelProp = t.GetProperty("LSTel_1");
            if (haslsTelProp is not null) {
                var lsTel_1 = t.GetProperty("LSTel_1").GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsTel_1))
                    SourceCustomerLC.LCTel_1 = lsTel_1;
                var lsTel_2 = t.GetProperty("LSTel_2").GetValue(inputDto)?.ToString();
                if (!string.IsNullOrEmpty(lsTel_2))
                    SourceCustomerLC.LCTel_2 = lsTel_2;
            }

            #endregion 電話

            #region 身分證字號

            var haslsidProp = t.GetProperty("LSID");
            if (haslsidProp is not null) {
                var lsid = t.GetProperty("LSID").GetValue(inputDto)?.ToString();
                customerlc.LCID = lsid;
            }

            #endregion 身分證字號

            #endregion LS欄位

            #region 出租人

            #endregion

            #region 地址

            SourceCustomerLC.LCAdd = Utils.memergeAddFix(
                customerlc.LCAdd_1, 
                customerlc.LCAdd_1_1, 
                customerlc.LCAdd_1_2,
                customerlc.LCAdd_2,
                customerlc.LCAdd_2_1,
                customerlc.LCAdd_2_2,
                customerlc.LCAdd_2_3,
                customerlc.LCAdd_2_4,
                customerlc.LCAdd_3,
                customerlc.LCAdd_3_1,
                customerlc.LCAdd_3_2,
                customerlc.LCAdd_4,
                customerlc.LCAdd_5,
                customerlc.LCAdd_6,
                customerlc.LCAdd_7,
                customerlc.LCAdd_8,
                customerlc.LCAdd_9);
            if (customerlc.LCAddSame == "/Yes") {
                SourceCustomerLC.LCAddC_1 = customerlc.LCAdd_1;
                SourceCustomerLC.LCAddC_1_1 = customerlc.LCAdd_1_1;
                SourceCustomerLC.LCAddC_1_2 = customerlc.LCAdd_1_2;
                SourceCustomerLC.LCAddC_2 = customerlc.LCAdd_2;
                SourceCustomerLC.LCAddC_2_1 = customerlc.LCAdd_2_1;
                SourceCustomerLC.LCAddC_2_2 = customerlc.LCAdd_2_2;
                SourceCustomerLC.LCAddC_2_3 = customerlc.LCAdd_2_3;
                SourceCustomerLC.LCAddC_2_4 = customerlc.LCAdd_2_4;
                SourceCustomerLC.LCAddC_3 = customerlc.LCAdd_3;
                SourceCustomerLC.LCAddC_3_1 = customerlc.LCAdd_3_1;
                SourceCustomerLC.LCAddC_3_2 = customerlc.LCAdd_3_2;
                SourceCustomerLC.LCAddC_4 = customerlc.LCAdd_4;
                SourceCustomerLC.LCAddC_5 = customerlc.LCAdd_5;
                SourceCustomerLC.LCAddC_6 = customerlc.LCAdd_6;
                SourceCustomerLC.LCAddC_7 = customerlc.LCAdd_7;
                SourceCustomerLC.LCAddC_8 = customerlc.LCAdd_8;
                SourceCustomerLC.LCAddC_9 = customerlc.LCAdd_9;
                SourceCustomerLC.LCAddC = SourceCustomerLC.LCAdd;
            } else {
                SourceCustomerLC.LCAddC = Utils.memergeAddFix(
                        customerlc.LCAddC_1,
                        customerlc.LCAddC_1_1,
                        customerlc.LCAddC_1_2,
                        customerlc.LCAddC_2,
                        customerlc.LCAddC_2_1,
                        customerlc.LCAddC_2_2,
                        customerlc.LCAddC_2_3,
                        customerlc.LCAddC_2_4,
                        customerlc.LCAddC_3,
                        customerlc.LCAddC_3_1,
                        customerlc.LCAddC_3_2,
                        customerlc.LCAddC_4,
                        customerlc.LCAddC_5,
                        customerlc.LCAddC_6,
                        customerlc.LCAddC_7,
                        customerlc.LCAddC_8,
                        customerlc.LCAddC_9);
            }
            SourceCustomerLC.LCAGAdd_A = Utils.memergeAddFix(
                customerlc.LCAGAdd_1_A,
                customerlc.LCAGAdd_1_1_A,
                customerlc.LCAGAdd_1_2_A,
                customerlc.LCAGAdd_2_A,
                customerlc.LCAGAdd_2_1_A,
                customerlc.LCAGAdd_2_2_A,
                customerlc.LCAGAdd_2_3_A,
                customerlc.LCAGAdd_2_4_A,
                customerlc.LCAGAdd_3_A,
                customerlc.LCAGAdd_3_1_A,
                customerlc.LCAGAdd_3_2_A,
                customerlc.LCAGAdd_4_A,
                customerlc.LCAGAdd_5_A,
                customerlc.LCAGAdd_6_A,
                customerlc.LCAGAdd_7_A,
                customerlc.LCAGAdd_8_A,
                customerlc.LCAGAdd_9_A);
            SourceCustomerLC.LCAGAdd_B = Utils.memergeAddFix(
                customerlc.LCAGAdd_1_B,
                customerlc.LCAGAdd_1_1_B,
                customerlc.LCAGAdd_1_2_B,
                customerlc.LCAGAdd_2_B,
                customerlc.LCAGAdd_2_1_B,
                customerlc.LCAGAdd_2_2_B,
                customerlc.LCAGAdd_2_3_B,
                customerlc.LCAGAdd_2_4_B,
                customerlc.LCAGAdd_3_B,
                customerlc.LCAGAdd_3_1_B,
                customerlc.LCAGAdd_3_2_B,
                customerlc.LCAGAdd_4_B,
                customerlc.LCAGAdd_5_B,
                customerlc.LCAGAdd_6_B,
                customerlc.LCAGAdd_7_B,
                customerlc.LCAGAdd_8_B,
                customerlc.LCAGAdd_9_B);


            #endregion

            //電話
            SourceCustomerLC.LCTel = customerlc.LCTel_1 + "-" + customerlc.LCTel_2;
            SourceCustomerLC.LCAGTel_A = customerlc.LCAGTel_1_A + "-" + customerlc.LCAGTel_2_A;
            SourceCustomerLC.LCAGTel_B = customerlc.LCAGTel_1_B + "-" + customerlc.LCAGTel_2_B;
            //統一編號
            if (!string.IsNullOrEmpty(customerlc.LCID)) {
                var idSplit = customerlc.LCID.ToCharArray();
                SourceCustomerLC.LCID_1_1 = idSplit[0].ToString();
                SourceCustomerLC.LCID_1_2 = idSplit[1].ToString();
                SourceCustomerLC.LCID_1_3 = idSplit[2].ToString();
                SourceCustomerLC.LCID_1_4 = idSplit[3].ToString();
                SourceCustomerLC.LCID_1_5 = idSplit[4].ToString();
                SourceCustomerLC.LCID_1_6 = idSplit[5].ToString();
                SourceCustomerLC.LCID_1_7 = idSplit[6].ToString();
                SourceCustomerLC.LCID_1_8 = idSplit[7].ToString();
            }
            return SourceCustomerLC;
        }

        /// <summary>
        /// 新增或修改時合併或分割特殊欄位
        /// </summary>
        /// <param name="SourceCustomerRN"></param>
        /// <param name="customerrn"></param>
        /// <returns></returns>
        public static Building HandleSpecificDataBuilding<T>(Building SourceBuilding, T inputDto) where T : class
        {
            var t = typeof(T);
            var props = t.GetProperties();
            var building = new Building();
            var bProps = typeof(Building).GetProperties();
            foreach (var prop in props) {
                var propInfo = bProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                if (propInfo is not null) {
                    //var propinfo = crnProps.Where(p => p.Name == prop.Name).FirstOrDefault();
                    propInfo.SetValue(building, prop.GetValue(inputDto));
                }
            }

            #region 建物基本資料
            //換算屋齡
            //if (!string.IsNullOrEmpty(building.BCDate)) {
            //    var bcDate = Convert.ToDateTime(building.BCDate).AddYears(1911);
            //    if (bcDate < DateTime.Now) {
            //        var nowyear = DateTime.Now.Year;
            //        SourceBuilding.BAge = (DateTime.Now.Year - bcDate.Year).ToString(); 
            //    }
            //}

            //SourceBuilding.BAdd = Utils.memergeAddFix(
            //    building.BAdd_1,
            //    building.BAdd_1_1,
            //    building.BAdd_1_2,
            //    building.BAdd_2,
            //    building.BAdd_2_1,
            //    building.BAdd_2_2,
            //    building.BAdd_2_3,
            //    building.BAdd_2_4,
            //    building.BAdd_3,
            //    building.BAdd_3_1,
            //    building.BAdd_3_2,
            //    building.BAdd_4,
            //    building.BAdd_5,
            //    building.BAdd_6,
            //    building.BAdd_7,
            //    building.BAdd_8,
            //    building.BAdd_9
            //    );

            #endregion



            return SourceBuilding;
        }
    }
}
