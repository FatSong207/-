using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// XML文件與對象相互轉化操作
    /// </summary>
    public class XmlConverter
    {
        /// <summary>
        /// 將對象轉換為xml格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="xmlFilePath">xml文件路徑</param>
        /// <returns></returns>
        public static void Serialize<T>(T obj,string xmlFilePath)
        {
            FileStream xmlfs = null;
            try { 
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                xmlfs = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                serializer.Serialize(xmlfs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xmlfs != null)
                    xmlfs.Close();
            }
        }

        /// <summary>
        /// 將xml格式轉為對象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath">xml文件路徑</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(doc.ToString());
            T result = (T)(serializer.Deserialize(reader));
            reader.Close();
            reader.Dispose();
            return result;
        }
    }
}
