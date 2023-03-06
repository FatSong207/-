using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// byte轉換類
    /// </summary>
    public class ByteConvertHelper
    {
        /// <summary>
        /// 將對象轉換為byte數組
        /// </summary>
        /// <param name="obj">被轉換對象</param>
        /// <returns>轉換后byte數組</returns>
        public static byte[] Object2Bytes(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);
            return serializedResult;
        }

        /// <summary>
        /// 將byte數組轉換成對象
        /// </summary>
        /// <param name="buff">被轉換byte數組</param>
        /// <returns>轉換完成后的對象</returns>
        public static object Bytes2Object(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<object>(json);
        }

        /// <summary>
        /// 將byte數組轉換成對象
        /// </summary>
        /// <param name="buff">被轉換byte數組</param>
        /// <returns>轉換完成后的對象</returns>
        public static T Bytes2Object<T>(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
