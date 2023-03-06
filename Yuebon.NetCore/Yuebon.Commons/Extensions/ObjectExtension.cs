/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Yuebon.Commons.Extensions
{
    /// <summary>
    /// 對象自定義擴展類
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <typeparam name="T">轉換的元素類型。</typeparam>
        /// <param name="list">集合。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>數據集。</returns>
        public static DataSet ToDataSet<T>(this IEnumerable<T> list, bool generic = true)
        {
            return ListToDataSet(list, generic);
        }

        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <param name="list">集合。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>數據集。</returns>
        public static DataSet ToDataSet(this IEnumerable list, bool generic = true)
        {
            return ListToDataSet(list, generic);
        }

        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <typeparam name="T">轉換的元素類型。</typeparam>
        /// <param name="list">集合。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>數據集。</returns>
        public static DataSet ToDataSet<T>(this IEnumerable list, bool generic = true)
        {
            return ListToDataSet(list, typeof(T), generic);
        }

        /// <summary>
        /// 將實例轉換為集合數據集。
        /// </summary>
        /// <typeparam name="T">實例類型。</typeparam>
        /// <param name="o">實例。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>數據集。</returns>
        public static DataSet ToListSet<T>(this T o, bool generic = true)
        {
            if (o is IEnumerable)
            {
                return ListToDataSet(o as IEnumerable, generic);
            }
            else
            {
                return ListToDataSet(new T[] { o }, generic);
            }
        }

        /// <summary>
        /// 將可序列化實例轉換為XmlDocument。
        /// </summary>
        /// <typeparam name="T">實例類型。</typeparam>
        /// <param name="o">實例。</param>
        /// <returns>XmlDocument。</returns>
        public static XmlDocument ToXmlDocument<T>(this T o)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.InnerXml = o.ToListSet().GetXml();
            return xmlDocument;
        }


        public static object ChangeType(this object convertibleValue, Type type)
        {
            if (null == convertibleValue) return null;

            try
            {
                if (type == typeof(Guid) || type == typeof(Guid?))
                {
                    string value = convertibleValue.ToString();
                    if (value == "") return null;
                    return Guid.Parse(value);
                }

                if (!type.IsGenericType) return Convert.ChangeType(convertibleValue, type);
                if (type.ToString() == "System.Nullable`1[System.Boolean]" || type.ToString() == "System.Boolean")
                {
                    if (convertibleValue.ToString() == "0")
                        return false;
                    return true;
                }
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(type));
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 獲取對象里指定成員名稱
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="properties"> 格式 Expression<Func<entityt, object>> exp = x => new { x.字段1, x.字段2 };或x=>x.Name</param>
        /// <returns></returns>
        public static string[] GetExpressionProperty<TEntity>(this Expression<Func<TEntity, object>> properties)
        {
            if (properties == null)
                return new string[] { };
            if (properties.Body is NewExpression)
                return ((NewExpression)properties.Body).Members.Select(x => x.Name).ToArray();
            if (properties.Body is MemberExpression)
                return new string[] { ((MemberExpression)properties.Body).Member.Name };
            if (properties.Body is UnaryExpression)
                return new string[] { ((properties.Body as UnaryExpression).Operand as MemberExpression).Member.Name };
            throw new Exception("未實現的表達式");
        }
        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <param name="list">集合。</param>
        /// <param name="t">轉換的元素類型。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>轉換后的數據集。</returns>
        private static DataSet ListToDataSet(IEnumerable list, Type t, bool generic)
        {
            DataSet ds = new DataSet("Data");
            if (t == null)
            {
                if (list != null)
                {
                    foreach (var i in list)
                    {
                        if (i == null)
                        {
                            continue;
                        }
                        t = i.GetType();
                        break;
                    }
                }
                if (t == null)
                {
                    return ds;
                }
            }
            ds.Tables.Add(t.Name);
            //如果集合中元素為DataSet擴展涉及到的基本類型時，進行特殊轉換。
            if (t.IsValueType || t == typeof(string))
            {
                ds.Tables[0].TableName = "Info";
                ds.Tables[0].Columns.Add(t.Name);
                if (list != null)
                {
                    foreach (var i in list)
                    {
                        DataRow addRow = ds.Tables[0].NewRow();
                        addRow[t.Name] = i;
                        ds.Tables[0].Rows.Add(addRow);
                    }
                }
                return ds;
            }
            //處理模型的字段和屬性。
            var fields = t.GetFields();
            var properties = t.GetProperties();
            foreach (var j in fields)
            {
                if (!ds.Tables[0].Columns.Contains(j.Name))
                {
                    if (generic)
                    {
                        ds.Tables[0].Columns.Add(j.Name, j.FieldType);
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add(j.Name);
                    }
                }
            }
            foreach (var j in properties)
            {
                if (!ds.Tables[0].Columns.Contains(j.Name))
                {
                    if (generic)
                    {
                        ds.Tables[0].Columns.Add(j.Name, j.PropertyType);
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add(j.Name);
                    }
                }
            }
            if (list == null)
            {
                return ds;
            }
            //讀取list中元素的值。
            foreach (var i in list)
            {
                if (i == null)
                {
                    continue;
                }
                DataRow addRow = ds.Tables[0].NewRow();
                foreach (var j in fields)
                {
                    MemberExpression field = Expression.Field(Expression.Constant(i), j.Name);
                    LambdaExpression lambda = Expression.Lambda(field, new ParameterExpression[] { });
                    Delegate func = lambda.Compile();
                    object value = func.DynamicInvoke();
                    addRow[j.Name] = value;
                }
                foreach (var j in properties)
                {
                    MemberExpression property = Expression.Property(Expression.Constant(i), j);
                    LambdaExpression lambda = Expression.Lambda(property, new ParameterExpression[] { });
                    Delegate func = lambda.Compile();
                    object value = func.DynamicInvoke();
                    addRow[j.Name] = value;
                }
                ds.Tables[0].Rows.Add(addRow);
            }
            return ds;
        }

        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <typeparam name="T">轉換的元素類型。</typeparam>
        /// <param name="list">集合。</param>
        /// <param name="generic">是否生成泛型數據集。</param>
        /// <returns>數據集。</returns>
        private static DataSet ListToDataSet<T>(IEnumerable<T> list, bool generic)
        {
            return ListToDataSet(list, typeof(T), generic);
        }

        /// <summary>
        /// 將集合轉換為數據集。
        /// </summary>
        /// <param name="list">集合。</param>
        /// <param name="generic">是否轉換為字符串形式。</param>
        /// <returns>轉換后的數據集。</returns>
        private static DataSet ListToDataSet(IEnumerable list, bool generic)
        {
            return ListToDataSet(list, null, generic);
        }

        /// <summary>
        /// 獲取DataSet第一表，第一行，第一列的值。
        /// </summary>
        /// <param name="ds">DataSet數據集。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataSet ds)
        {
            if (
                ds == null
                || ds.Tables.Count == 0
                )
            {
                return string.Empty;
            }
            else
            {
                return ds.Tables[0].GetData();
            }
        }

        /// <summary>
        /// 獲取DataTable第一行，第一列的值。
        /// </summary>
        /// <param name="dt">DataTable數據集表。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataTable dt)
        {
            if (
                dt.Columns.Count == 0
                || dt.Rows.Count == 0
                )
            {
                return string.Empty;
            }
            else
            {
                return dt.Rows[0][0];
            }
        }

        /// <summary>
        /// 獲取DataSet第一個匹配columnName的值。
        /// </summary>
        /// <param name="ds">數據集。</param>
        /// <param name="columnName">列名。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataSet ds, string columnName)
        {
            if (
                ds == null
                || ds.Tables.Count == 0
                )
            {
                return string.Empty;
            }
            foreach (DataTable dt in ds.Tables)
            {
                object o = dt.GetData(columnName);
                if (!string.IsNullOrEmpty(o.ToString()))
                {
                    return o;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 獲取DataTable第一個匹配columnName的值。
        /// </summary>
        /// <param name="dt">數據表。</param>
        /// <param name="columnName">列名。</param>
        /// <returns>值。</returns>
        public static object GetData(this DataTable dt, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return GetData(dt);
            }
            if (
                dt.Columns.Count == 0
                || dt.Columns.IndexOf(columnName) == -1
                || dt.Rows.Count == 0
                )
            {
                return string.Empty;
            }
            return dt.Rows[0][columnName];
        }

        /// <summary>
        /// 將object轉換為string類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>string。</returns>
        public static string ToString(this object o, string t)
        {
            string info = string.Empty;
            if (o == null)
            {
                info = t;
            }
            else
            {
                info = o.ToString();
            }
            return info;
        }

        /// <summary>
        /// 將DateTime?轉換為string類型信息。
        /// </summary>
        /// <param name="o">DateTime?。</param>
        /// <param name="format">標準或自定義日期和時間格式的字符串。</param>
        /// <param name="t">默認值。</param>
        /// <returns>string。</returns>
        public static string ToString(this DateTime? o, string format, string t)
        {
            string info = string.Empty;
            if (o == null)
            {
                info = t;
            }
            else
            {
                info = o.Value.ToString(format);
            }
            return info;
        }

        /// <summary>
        /// 將TimeSpan?轉換為string類型信息。
        /// </summary>
        /// <param name="o">TimeSpan?。</param>
        /// <param name="format">標準或自定義時間格式的字符串。</param>
        /// <param name="t">默認值。</param>
        /// <returns>string。</returns>
        public static string ToString(this TimeSpan? o, string format, string t)
        {
            string info = string.Empty;
            if (o == null)
            {
                info = t;
            }
            else
            {
                info = o.Value.ToString(format);
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為截取后的string類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="startIndex">此實例中子字符串的起始字符位置（從零開始）。</param>
        /// <param name="length">子字符串中的字符數。</param>
        /// <param name="suffix">后綴。如果沒有截取則不添加。</param>
        /// <returns>截取后的string類型信息。</returns>
        public static string ToSubString(this object o, int startIndex, int length, string suffix = null)
        {
            string inputString = o.ToString(string.Empty);
            startIndex = Math.Max(startIndex, 0);
            startIndex = Math.Min(startIndex, (inputString.Length - 1));
            length = Math.Max(length, 1);
            if (startIndex + length > inputString.Length)
            {
                length = inputString.Length - startIndex;
            }
            if (inputString.Length == startIndex + length)
            {
                return inputString;
            }
            else
            {
                return inputString.Substring(startIndex, length) + suffix;
            }
        }

        /// <summary>
        /// 將object轉換為byte類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>byte。</returns>
        public static byte ToByte(this object o, byte t = default(byte))
        {
            byte info;
            if (!byte.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object ToObject(this byte[] source)
        {
            using (var memStream = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                memStream.Write(source, 0, source.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = bf.Deserialize(memStream);
                return obj;
            }
        }

        /// <summary>
        /// 將object轉換為char類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>char。</returns>
        public static char ToChar(this object o, char t = default(char))
        {
            char info;
            if (!char.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為int類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>int。</returns>
        public static int ToInt(this object o, int t = default(int))
        {
            int info;
            if (!int.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為double類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>double。</returns>
        public static double ToDouble(this object o, double t = default(double))
        {
            double info;
            if (!double.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為decimal類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>decimal。</returns>
        public static decimal ToDecimal(this object o, decimal t = default(decimal))
        {
            decimal info;
            if (!decimal.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為float類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>float。</returns>
        public static float ToFloat(this object o, float t = default(float))
        {
            float info;
            if (!float.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為long類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>long。</returns>
        public static long ToLong(this object o, long t = default(long))
        {
            long info;
            if (!long.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為bool類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>bool。</returns>
        public static bool ToBool(this object o, bool t = default(bool))
        {
            bool info;
            if (!bool.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為sbyte類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>sbyte。</returns>
        public static sbyte ToSbyte(this object o, sbyte t = default(sbyte))
        {
            sbyte info;
            if (!sbyte.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為short類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>short。</returns>
        public static short ToShort(this object o, short t = default(short))
        {
            short info;
            if (!short.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為ushort類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>ushort。</returns>
        public static ushort ToUShort(this object o, ushort t = default(ushort))
        {
            ushort info;
            if (!ushort.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為ulong類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>ulong。</returns>
        public static ulong ToULong(this object o, ulong t = default(ulong))
        {
            ulong info;
            if (!ulong.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為Enum[T]類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>Enum[T]。</returns>
        public static T ToEnum<T>(this object o, T t = default(T))
            where T : struct
        {
            T info;
            if (!Enum.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為DateTime類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>DateTime。</returns>
        public static DateTime ToDateTime(this object o, DateTime t = default(DateTime))
        {
            if (t == default(DateTime))
            {
                t = new DateTime(1753, 1, 1);
            }
            DateTime info;
            //"yyyy-MM-dd HH:mm:ss.fff"
            
            //if (!DateTime.TryParse(o.ToString("yyyy-MM-dd HH:mm:ss.fff"), out info))
            if (!DateTime.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為TimeSpan類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>TimeSpan。</returns>
        public static TimeSpan ToTimeSpan(this object o, TimeSpan t = default(TimeSpan))
        {
            if (t == default(TimeSpan))
            {
                t = new TimeSpan(0, 0, 0);
            }
            TimeSpan info;
            if (!TimeSpan.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為Guid類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>Guid。</returns>
        public static Guid ToGuid(this object o, Guid t = default(Guid))
        {
            Guid info;
            if (!Guid.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            return info;
        }

        private static Regex BoolRegex = new Regex("(?<info>(true|false))", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取bool類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>bool。</returns>
        public static bool? GetBool(this object o)
        {
            bool info;
            if (!bool.TryParse(BoolRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        private static Regex IntRegex = new Regex("(?<info>-?\\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取int類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>int。</returns>
        public static int? GetInt(this object o)
        {
            int info;
            if (!int.TryParse(IntRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        private static Regex DecimalRegex = new Regex("(?<info>-?\\d+(\\.\\d+)?)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取decimal類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>decimal。</returns>
        public static decimal? GetDecimal(this object o)
        {
            decimal info;
            if (!decimal.TryParse(DecimalRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 從object中獲取double類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>double。</returns>
        public static double? GetDouble(this object o)
        {
            double info;
            if (!double.TryParse(DecimalRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 從object中獲取正數信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>decimal。</returns>
        public static decimal? GetPositiveNumber(this object o)
        {
            decimal info;
            if (!decimal.TryParse(DecimalRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return Math.Abs(info);
        }

        private static Regex DateTimeRegex = new Regex("(?<info>(((\\d+)[/年-](0?[13578]|1[02])[/月-](3[01]|[12]\\d|0?\\d)[日]?)|((\\d+)[/年-](0?[469]|11)[/月-](30|[12]\\d|0?\\d)[日]?)|((\\d+)[/年-]0?2[/月-](2[0-8]|1\\d|0?\\d)[日]?))(\\s((2[0-3]|[0-1]\\d)):[0-5]\\d:[0-5]\\d)?)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取DateTime?類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>DateTime?。</returns>
        public static DateTime? GetDateTime(this object o)
        {
            DateTime info;
            if (!DateTime.TryParse(DateTimeRegex.Match(o.ToString(string.Empty)).Groups["info"].Value.Replace("年", "-").Replace("月", "-").Replace("/", "-").Replace("日", ""), out info))
            {
                return null;
            }
            return info;
        }

        private static Regex TimeSpanRegex = new Regex("(?<info>-?(\\d+\\.(([0-1]\\d)|(2[0-3])):[0-5]\\d:[0-5]\\d)|((([0-1]\\d)|(2[0-3])):[0-5]\\d:[0-5]\\d)|(\\d+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取TimeSpan?類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>TimeSpan?。</returns>
        public static TimeSpan? GetTimeSpan(this object o)
        {
            TimeSpan info;
            if (!TimeSpan.TryParse(TimeSpanRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        private static Regex GuidRegex = new Regex("(?<info>\\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\\}{0,1})", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 從object中獲取Guid?類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <returns>Guid?。</returns>
        public static Guid? GetGuid(this object o)
        {
            Guid info;
            if (!Guid.TryParse(GuidRegex.Match(o.ToString(string.Empty)).Groups["info"].Value, out info))
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 將object轉換為SqlServer中的DateTime?類型信息。
        /// </summary>
        /// <param name="o">object。</param>
        /// <param name="t">默認值。</param>
        /// <returns>DateTime?。</returns>
        public static DateTime? GetSqlDateTime(this object o, DateTime t = default(DateTime))
        {
            DateTime info;
            if (!DateTime.TryParse(o.ToString(string.Empty), out info))
            {
                info = t;
            }
            if (info < new DateTime(1753, 1, 1) || info > new DateTime(9999, 12, 31))
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 讀取XElement節點的文本內容。
        /// </summary>
        /// <param name="xElement">XElement節點。</param>
        /// <param name="t">默認值。</param>
        /// <returns>文本內容。</returns>
        public static string Value(this XElement xElement, string t = default(string))
        {
            if (xElement == null)
            {
                return t;
            }
            else
            {
                return xElement.Value;
            }
        }

        /// <summary>
        /// 獲取與指定鍵相關的值。
        /// </summary>
        /// <typeparam name="TKey">鍵類型。</typeparam>
        /// <typeparam name="TValue">值類型。</typeparam>
        /// <param name="dictionary">表示鍵/值對象的泛型集合。</param>
        /// <param name="key">鍵。</param>
        /// <param name="t">默認值。</param>
        /// <returns>值。</returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue t = default(TValue))
        {
            TValue value = default(TValue);
            if (dictionary == null || key == null)
            {
                return t;
            }
            if (!dictionary.TryGetValue(key, out value))
            {
                value = t;
            }
            return value;
        }

        /// <summary>
        /// 獲取與指定鍵相關或者第一個的值。
        /// </summary>
        /// <typeparam name="TKey">鍵類型。</typeparam>
        /// <typeparam name="TValue">值類型。</typeparam>
        /// <param name="dictionary">表示鍵/值對象的泛型集合。</param>
        /// <param name="key">鍵。</param>
        /// <param name="t">默認值。</param>
        /// <returns>值。</returns>
        public static TValue GetFirstOrDefaultValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue t = default(TValue))
        {
            TValue value = default(TValue);
            if (dictionary == null || key == null)
            {
                return t;
            }
            if (!dictionary.TryGetValue(key, out value))
            {
                if (dictionary.Count() == 0)
                {
                    value = t;
                }
                else
                {
                    value = dictionary.FirstOrDefault().Value;
                }
            }
            return value;
        }

        /// <summary>
        /// 獲取具有指定 System.Xml.Linq.XName 的第一個（按文檔順序）子元素。
        /// </summary>
        /// <param name="xContainer">XContainer。</param>
        /// <param name="xName">要匹配的 System.Xml.Linq.XName。</param>
        /// <param name="t">是否返回同名默認值。</param>
        /// <returns>與指定 System.Xml.Linq.XName 匹配的 System.Xml.Linq.XElement，或者為 null。</returns>
        public static XElement Element(this XContainer xContainer, XName xName, bool t)
        {
            XElement info;
            if (xContainer == null)
            {
                info = null;
            }
            else
            {
                info = xContainer.Element(xName);
            }
            if (t && info == null)
            {
                info = new XElement(xName);
            }
            return info;
        }

        /// <summary>
        /// 按文檔順序返回此元素或文檔的子元素集合。
        /// </summary>
        /// <param name="xContainer">XContainer。</param>
        /// <param name="t">是否返回非空默認值。</param>
        /// <returns>System.Xml.Linq.XElement 的按文檔順序包含此System.Xml.Linq.XContainer 的子元素，或者非空默認值。</returns>
        public static IEnumerable<XElement> Elements(this XContainer xContainer, bool t)
        {
            IEnumerable<XElement> info;
            if (xContainer == null)
            {
                info = null;
            }
            else
            {
                info = xContainer.Elements();
            }
            if (t && info == null)
            {
                info = new List<XElement>();
            }
            return info;
        }

        /// <summary>
        /// 按文檔順序返回此元素或文檔的經過篩選的子元素集合。集合中只包括具有匹配 System.Xml.Linq.XName 的元素。
        /// </summary>
        /// <param name="xContainer">XContainer。</param>
        /// <param name="xName">要匹配的 System.Xml.Linq.XName。</param>
        /// <param name="t">是否返回非空默認值。</param>
        /// <returns>System.Xml.Linq.XElement 的按文檔順序包含具有匹配System.Xml.Linq.XName 的 System.Xml.Linq.XContainer 的子級，或者非空默認值。</returns>
        public static IEnumerable<XElement> Elements(this XContainer xContainer, XName xName, bool t)
        {
            IEnumerable<XElement> info;
            if (xContainer == null)
            {
                info = null;
            }
            else
            {
                info = xContainer.Elements(xName);
            }
            if (t && info == null)
            {
                info = new List<XElement>();
            }
            return info;
        }

        /// <summary>
        /// 刪除html標簽。
        /// </summary>
        /// <param name="html">輸入的字符串。</param>
        /// <returns>沒有html標簽的字符串。</returns>
        public static string RemoveHTMLTags(this string html)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace((html ?? string.Empty).Replace("&nbsp;", " ").Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ").Replace("\t", " "), "<\\/?[^>]+>", "\r\n"), "(\r\n)+", "\r\n"), "(\\s)+", " ").Trim();
        }

        /// <summary>
        /// 字符串轉換為文件名。
        /// </summary>
        /// <param name="s">字符串。</param>
        /// <returns>文件名。</returns>
        public static string ToFileName(this string s)
        {
            return Regex.Replace(s.ToString(string.Empty), @"[\\/:*?<>|]", "_").Replace("\t", " ").Replace("\r\n", " ").Replace("\"", " ");
        }

        /// <summary>
        /// 獲取星期一的日期。
        /// </summary>
        /// <param name="dateTime">日期。</param>
        /// <returns>星期一的日期。</returns>
        public static DateTime? GetMonday(this DateTime dateTime)
        {
            return dateTime.AddDays(-1 * (int)dateTime.AddDays(-1).DayOfWeek).ToString("yyyy-MM-dd").GetDateTime();
        }

        /// <summary>
        /// 獲取默認非空字符串。
        /// </summary>
        /// <param name="s">首選默認非空字符串。</param>
        /// <param name="args">依次非空字符串可選項。</param>
        /// <returns>默認非空字符串。若無可選項則返回string.Empty。</returns>
        public static string DefaultStringIfEmpty(this string s, params string[] args)
        {
            if (string.IsNullOrEmpty(s))
            {
                foreach (string i in args)
                {
                    if (!string.IsNullOrEmpty(i) && !string.IsNullOrEmpty(i.Trim()))
                    {
                        return i;
                    }
                }
            }
            return (s ?? string.Empty);
        }

        /// <summary>
        /// 對 URL 字符串進行編碼。
        /// </summary>
        /// <param name="s">要編碼的文本。</param>
        /// <param name="regex">匹配要編碼的文本。</param>
        /// <param name="encoding">指定編碼方案的 System.Text.Encoding 對象。</param>
        /// <returns>一個已編碼的字符串。</returns>
        public static string ToUrlEncodeString(this string s, Regex regex = default(Regex), Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            if (regex == null)
            {
                return HttpUtility.UrlEncode(s, encoding);
            }
            List<string> l = new List<string>();
            foreach (char i in s)
            {
                string t = i.ToString();
                l.Add(regex.IsMatch(t) ? HttpUtility.UrlEncode(t, encoding) : t);
            }
            return string.Join(string.Empty, l);
        }

        /// <summary>
        /// 對 URL 字符串進行編碼。
        /// </summary>
        /// <param name="s">要編碼的文本。</param>
        /// <param name="regex">匹配要編碼的文本。</param>
        /// <param name="encoding">指定編碼方案的 System.Text.Encoding 對象。</param>
        /// <returns>一個已編碼的字符串。</returns>
        public static string ToUrlEncodeString(this string s, string regex, Encoding encoding = null)
        {
            return ToUrlEncodeString(s, new Regex(regex), encoding);
        }

        /// <summary>
        /// 將日期轉換為UNIX時間戳字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUnixTimeStamp(this DateTime date)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            string timeStamp = date.Subtract(startTime).Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }

        public static string Join(this IEnumerable<object> source, string separator)
        {
            return string.Join(separator, source);
        }
        private static Regex MobileRegex = new Regex("^1[3|4|5|7|8][0-9]\\d{4,8}$");
        private static Regex EmailRegex = new Regex("^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\\.[a-zA-Z0-9_-]{2,3}){1,2})$");

        /// <summary>
        /// 判斷當前字符串是否是移動電話號碼
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsMobile(this string mobile)
        {
            return MobileRegex.IsMatch(mobile);
        }

        /// <summary>
        /// 判斷當前字符串是否為郵箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(this string email)
        {
            return EmailRegex.IsMatch(email);
        }
        /// <summary>
        /// 把對象類型轉換為指定類型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType.IsNullableType())
            {
                conversionType = conversionType.GetUnNullableType();
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 把對象類型轉化為指定類型
        /// </summary>
        /// <typeparam name="T"> 動態類型 </typeparam>
        /// <param name="value"> 要轉化的源對象 </param>
        /// <returns> 轉化后的指定類型的對象，轉化失敗引發異常。 </returns>
        public static T CastTo<T>(this object value)
        {
            if (value == null && default(T) == null)
            {
                return default(T);
            }
            if (value.GetType() == typeof(T))
            {
                return (T)value;
            }
            object result = CastTo(value, typeof(T));
            return (T)result;
        }

        /// <summary>
        /// 把對象類型轉化為指定類型，轉化失敗時返回指定的默認值
        /// </summary>
        /// <typeparam name="T"> 動態類型 </typeparam>
        /// <param name="value"> 要轉化的源對象 </param>
        /// <param name="defaultValue"> 轉化失敗返回的指定默認值 </param>
        /// <returns> 轉化后的指定類型對象，轉化失敗時返回指定的默認值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 將對象保存為csv
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="csvFullName">文件名稱</param>
        /// <param name="separator">分隔符，默認逗號</param>
        public static void SaveToCsv<T>(this IEnumerable<T> source, string csvFullName, string separator = ",")
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(separator))
                separator = ",";
            var csv = string.Join(separator, source);
            using (var sw = new StreamWriter(csvFullName, false))
            {
                sw.Write(csv);
                sw.Close();
            }
        }

        public static bool IsImplement(this Type entityType, Type interfaceType)
        {
            return /*entityType.IsClass && !entityType.IsAbstract &&*/ entityType.GetTypeInfo().GetInterfaces().Any(t =>
                t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == interfaceType);
        }
        /// <summary>
        /// 將對象轉為DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            DataTable dtReturn = new DataTable();


            if (source == null) return dtReturn;
            // column names 
            PropertyInfo[] oProps = null;

            foreach (var rec in source)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (var pi in oProps)
                    {
                        var colType = pi.PropertyType;

                        if (colType.IsNullableType())
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        if (colType == typeof(Boolean))
                        {
                            colType = typeof(int);
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                var dr = dtReturn.NewRow();

                foreach (var pi in oProps)
                {
                    var value = pi.GetValue(rec, null) ?? DBNull.Value;
                    if (value is bool)
                    {
                        dr[pi.Name] = (bool)value ? 1 : 0;
                    }
                    else
                    {
                        dr[pi.Name] = value;
                    }
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }

    /// <summary>
    /// 結果。
    /// </summary>
    /// <typeparam name="T">結果返回值類型。</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 標記。
        /// </summary>
        public Flag Flag { get; set; }

        /// <summary>
        /// 返回值。
        /// </summary>
        public T Return { get; set; }

        /// <summary>
        /// 消息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 異常。
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 時間。
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 整型數據。
        /// </summary>
        public int Int { get; set; }

        /// <summary>
        /// 浮點數據。
        /// </summary>
        public decimal Decimal { get; set; }

        /// <summary>
        /// 布爾數據。
        /// </summary>
        public bool Bool { get; set; }

        /// <summary>
        /// 對象。
        /// </summary>
        public object Object { get; set; }
    }

    /// <summary>
    /// 標記。
    /// </summary>
    public enum Flag
    {
        /// <summary>
        /// 默認。
        /// </summary>
        Default,

        /// <summary>
        /// 真。
        /// </summary>
        True,

        /// <summary>
        /// 假。
        /// </summary>
        False
    }
}