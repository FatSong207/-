using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 用對象屬性及屬性值替換預設字符串
    /// 主要應用于模板打印，導出
    /// </summary>
    public class ObjectReplaceHtmlHelper
    {

        /// <summary>
        /// 用實體屬性替換相關的字符串，主要應用于打印和導出
        /// 方法將日期時間型屬性值截取為日期型，格式“YYYY-MM-DD”，
        /// 將布爾型屬性值調整為是或否
        /// </summary>
        /// <param name="objInfo">實體對象</param>
        /// <param name="strReplace">要替換的原字符串</param>
        /// <param name="prefix">變量前綴</param>
        /// <returns></returns>
        public static string ObjectReplaceString(object objInfo, string strReplace, string prefix = "")
        {
            string result = string.Empty;
            string nowReplace = strReplace;
            Type type = objInfo.GetType();//獲得該類的Type
            foreach (PropertyInfo pi in type.GetProperties())
            {
                string name = pi.Name;//獲得屬性的名字,后面就可以根據名字判斷來進行些自己想要的操作
                var value = pi.GetValue(objInfo, null);//用pi.GetValue獲得值
                var propertyType = value?.GetType() ?? typeof(object);//獲得屬性的類型
                string replaceOld = "$" + prefix + name;
                string newStrValue = "";
                if (value != null)
                {
                    //將日期時間型和布爾型數據進行處理，其他枚舉數據提前處理
                    if (propertyType.Name == "DateTime")//如果是時間型取日期
                    {
                        newStrValue = value.ToString().Substring(0, 10);
                    }
                    else if (propertyType.Name == "Boolean")//布爾型轉為是或否
                    {
                        bool blvalue;
                        if (bool.TryParse(value.ToString(), out blvalue))
                        {
                            newStrValue = "是";
                        }
                        else
                        {
                            newStrValue = "否";
                        }
                    }
                    else
                    {
                        newStrValue = value.ToString();
                    }
                }
                nowReplace = nowReplace.Replace(replaceOld, newStrValue);
            }
            result += nowReplace;
            return result;
        }
    }
}
