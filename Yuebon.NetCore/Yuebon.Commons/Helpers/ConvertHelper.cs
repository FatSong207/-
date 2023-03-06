using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 處理數據類型轉換，數制轉換、編碼轉換相關的類
    /// </summary>
    public sealed class ConvertHelper
    {
        #region 各進制數間轉換
        /// <summary>
        /// 實現各進制數間的轉換。ConvertBase("15",10,16)表示將十進制數15轉換為16進制的數。
        /// </summary>
        /// <param name="value">要轉換的值,即原值</param>
        /// <param name="from">原值的進制,只能是2,8,10,16四個值。</param>
        /// <param name="to">要轉換到的目標進制，只能是2,8,10,16四個值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            if (!isBaseNumber(from))
                throw new ArgumentException("參數from只能是2,8,10,16四個值。");

            if (!isBaseNumber(to))
                throw new ArgumentException("參數to只能是2,8,10,16四個值。");

            int intValue = Convert.ToInt32(value, from);  //先轉成10進制
            string result = Convert.ToString(intValue, to);  //再轉成目標進制
            if (to == 2)
            {
                int resultLength = result.Length;  //獲取二進制的長度
                switch (resultLength)
                {
                    case 7:
                        result = "0" + result;
                        break;
                    case 6:
                        result = "00" + result;
                        break;
                    case 5:
                        result = "000" + result;
                        break;
                    case 4:
                        result = "0000" + result;
                        break;
                    case 3:
                        result = "00000" + result;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 判斷是否是 2 8 10 16
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <returns></returns>
        private static bool isBaseNumber(int baseNumber)
        {
            if (baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16)
                return true;
            return false;
        }

        #endregion

        #region 使用指定字符集將string轉換成byte[]

        /// <summary>
        /// 將string轉換成byte[]
        /// </summary>
        /// <param name="text">要轉換的字符串</param>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        /// <summary>
        /// 使用指定字符集將string轉換成byte[]
        /// </summary>
        /// <param name="text">要轉換的字符串</param>
        /// <param name="encoding">字符編碼</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        #endregion

        #region 使用指定字符集將byte[]轉換成string
        
        /// <summary>
        /// 將byte[]轉換成string
        /// </summary>
        /// <param name="bytes">要轉換的字節數組</param>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 使用指定字符集將byte[]轉換成string
        /// </summary>
        /// <param name="bytes">要轉換的字節數組</param>
        /// <param name="encoding">字符編碼</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region 將byte[]轉換成int
        /// <summary>
        /// 將byte[]轉換成int
        /// </summary>
        /// <param name="data">需要轉換成整數的byte數組</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果傳入的字節數組長度小于4,則返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定義要返回的整數
            int num = 0;

            //如果傳入的字節數組長度大于4,需要進行處理
            if (data.Length >= 4)
            {
                //創建一個臨時緩沖區
                byte[] tempBuffer = new byte[4];

                //將傳入的字節數組的前4個字節復制到臨時緩沖區
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //將臨時緩沖區的值轉換成整數，并賦給num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整數
            return num;
        }
        #endregion

        #region 將數據轉換為整型

        /// <summary>
        /// 將數據轉換為整型   轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據類型</typeparam>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static int ToInt32<T>(T data, int defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToInt32(data);
            }
            catch
            {
                return defValue;
            }

        }

        /// <summary>
        /// 將數據轉換為整型   轉換失敗返回默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static int ToInt32(string data, int defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            int temp = 0;
            if (Int32.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為整型  轉換失敗返回默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static int ToInt32(object data, int defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToInt32(data);
            }
            catch
            {
                return defValue;
            }
        }

        #endregion

        #region 將數據轉換為布爾型

        /// <summary>
        /// 將數據轉換為布爾類型  轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據類型</typeparam>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static bool ToBoolean<T>(T data, bool defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為布爾類型  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static bool ToBoolean(string data, bool defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            bool temp = false;
            if (bool.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }


        /// <summary>
        /// 將數據轉換為布爾類型  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static bool ToBoolean(object data, bool defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }
        }


        #endregion

        #region 將數據轉換為單精度浮點型


        /// <summary>
        /// 將數據轉換為單精度浮點型  轉換失敗 返回默認值
        /// </summary>
        /// <typeparam name="T">數據類型</typeparam>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static float ToFloat<T>(T data, float defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToSingle(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為單精度浮點型   轉換失敗返回默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static float ToFloat(object data, float defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToSingle(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為單精度浮點型   轉換失敗返回默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static float ToFloat(string data, float defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            float temp = 0;

            if (float.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }


        #endregion

        #region 將數據轉換為雙精度浮點型


        /// <summary>
        /// 將數據轉換為雙精度浮點型   轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據的類型</typeparam>
        /// <param name="data">要轉換的數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, double defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為雙精度浮點型,并設置小數位   轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據的類型</typeparam>
        /// <param name="data">要轉換的數據</param>
        /// <param name="decimals">小數的位數</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, int decimals, double defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
        }



        /// <summary>
        /// 將數據轉換為雙精度浮點型  轉換失敗返回默認值
        /// </summary>
        /// <param name="data">要轉換的數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble(object data, double defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }

        }

        /// <summary>
        /// 將數據轉換為雙精度浮點型  轉換失敗返回默認值
        /// </summary>
        /// <param name="data">要轉換的數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble(string data, double defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            double temp = 0;

            if (double.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }

        }


        /// <summary>
        /// 將數據轉換為雙精度浮點型,并設置小數位  轉換失敗返回默認值
        /// </summary>
        /// <param name="data">要轉換的數據</param>
        /// <param name="decimals">小數的位數</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble(object data, int decimals, double defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為雙精度浮點型,并設置小數位  轉換失敗返回默認值
        /// </summary>
        /// <param name="data">要轉換的數據</param>
        /// <param name="decimals">小數的位數</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static double ToDouble(string data, int decimals, double defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            double temp = 0;

            if (double.TryParse(data, out temp))
            {
                return Math.Round(temp, decimals);
            }
            else
            {
                return defValue;
            }
        }


        #endregion

        #region 將數據轉換為指定類型
        /// <summary>
        /// 將數據轉換為指定類型
        /// </summary>
        /// <param name="data">轉換的數據</param>
        /// <param name="targetType">轉換的目標類型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }

            Type type2 = data.GetType();
            if (targetType == type2)
            {
                return data;
            }
            if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(data.ToString()))
                {
                    return null;
                }
                return new Guid(data.ToString());
            }

            if (targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, data.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(targetType, data);
                }
            }

            if (targetType.IsGenericType)
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            return Convert.ChangeType(data, targetType);
        }

        /// <summary>
        /// 將數據轉換為指定類型
        /// </summary>
        /// <typeparam name="T">轉換的目標類型</typeparam>
        /// <param name="data">轉換的數據</param>
        public static T ConvertTo<T>(object data)
        {
            if (data == null || Convert.IsDBNull(data))
                return default(T);

            object obj = ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }


        #endregion

        #region = ChangeType =
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object obj, Type conversionType)
        {
            return ChangeType(obj, conversionType, Thread.CurrentThread.CurrentCulture);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static object ChangeType(object obj, Type conversionType, IFormatProvider provider)
        {
            #region Nullable  
            Type nullableType = Nullable.GetUnderlyingType(conversionType);
            if (nullableType != null)
            {
                if (obj == null)
                {
                    return null;
                }
                return Convert.ChangeType(obj, nullableType, provider);
            }
            #endregion
            if (typeof(System.Enum).IsAssignableFrom(conversionType))
            {
                return Enum.Parse(conversionType, obj.ToString());
            }
            return Convert.ChangeType(obj, conversionType, provider);
        }
        #endregion

        #region 將數據轉換Decimal

        /// <summary>
        /// 將數據轉換為Decimal  轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據類型</typeparam>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static Decimal ToDecimal<T>(T data, Decimal defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDecimal(data);
            }
            catch
            {
                return defValue;
            }
        }


        /// <summary>
        /// 將數據轉換為Decimal  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object data, Decimal defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDecimal(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為Decimal  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(string data, Decimal defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            decimal temp = 0;

            if (decimal.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }


        #endregion

        #region 將數據轉換為DateTime

        /// <summary>
        /// 將數據轉換為DateTime  轉換失敗返回默認值
        /// </summary>
        /// <typeparam name="T">數據類型</typeparam>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static DateTime ToDateTime<T>(T data, DateTime defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDateTime(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為DateTime  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object data, DateTime defValue)
        {
            //如果為空則返回默認值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDateTime(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 將數據轉換為DateTime  轉換失敗返回 默認值
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="defValue">默認值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string data, DateTime defValue)
        {
            //如果為空則返回默認值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            DateTime temp = DateTime.Now;

            if (DateTime.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        #endregion

        #region 半角全角轉換
        /// <summary>
        /// 轉全角的函數(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格為12288，半角空格為32
        ///其他字符半角(33-126)與全角(65281-65374)的對應關系是：均相差65248
        ///</remarks>
        public static string ConvertToSBC(string input)
        {
            //半角轉全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                {
                    c[i] = (char)(c[i] + 65248);
                }
            }
            return new string(c);
        }


        /// <summary> 轉半角的函數(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格為12288，半角空格為32
        ///其他字符半角(33-126)與全角(65281-65374)的對應關系是：均相差65248
        ///</remarks>
        public static string ConvertToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

    }
}
