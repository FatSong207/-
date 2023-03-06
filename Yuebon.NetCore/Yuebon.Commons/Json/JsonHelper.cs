using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Yuebon.Commons.Helpers;

namespace Yuebon.Commons.Json
{

    /// <summary>
    /// JSON序列化、反序列化擴展類。
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 對象序列化成JSON字符串。
        /// </summary>
        /// <param name="obj">序列化對象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            options.WriteIndented = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowTrailingCommas = true;
            //設置時間格式
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //設置bool獲取格式
            options.Converters.Add(new BooleanJsonConverter());
            //設置數字
            options.Converters.Add(new IntJsonConverter());
            options.PropertyNamingPolicy = new UpperFirstCaseNamingPolicy();
            options.PropertyNameCaseInsensitive = true;                     //忽略大小寫
            //JsonSerializerOptions options = new JsonSerializerOptions()
            //{
            //    WriteIndented = true,                                   //格式化json字符串
            //    AllowTrailingCommas = true,                             //可以結尾有逗號
            //    //IgnoreNullValues = true,                              //可以有空值,轉換json去除空值屬性
            //    IgnoreReadOnlyProperties = true,                        //忽略只讀屬性
            //    PropertyNameCaseInsensitive = true,                     //忽略大小寫
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            //}; 
            return JsonSerializer.Serialize(obj,options);
        }

        /// <summary>
        /// JSON字符串序列化成對象。
        /// </summary>
        /// <typeparam name="T">對象類型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            //JsonSerializerOptions options = new JsonSerializerOptions()
            //{
            //    WriteIndented = true,                                   //格式化json字符串
            //    AllowTrailingCommas = true,                             //可以結尾有逗號
            //    //IgnoreNullValues = true,                              //可以有空值,轉換json去除空值屬性
            //    IgnoreReadOnlyProperties = true,                        //忽略只讀屬性
            //    PropertyNameCaseInsensitive = true,                     //忽略大小寫
            //                                                            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase     //命名方式是默認還是CamelCase
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            //};

            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            options.WriteIndented = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowTrailingCommas = true;
            //設置時間格式
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //設置bool獲取格式
            options.Converters.Add(new BooleanJsonConverter());
            //設置數字
            options.Converters.Add(new IntJsonConverter());
            //options.PropertyNamingPolicy = new UpperFirstCaseNamingPolicy();
            options.PropertyNameCaseInsensitive = true;                     //忽略大小寫
            return json == null ? default(T) : JsonSerializer.Deserialize<T>(json,options);
        }

        /// <summary>
        /// JSON字符串序列化成集合。
        /// </summary>
        /// <typeparam name="T">集合類型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string json)
        {
            //JsonSerializerOptions options = new JsonSerializerOptions()
            //{
            //    WriteIndented = true,                                   //格式化json字符串
            //    AllowTrailingCommas = true,                             //可以結尾有逗號
            //    //IgnoreNullValues = true,                              //可以有空值,轉換json去除空值屬性
            //    IgnoreReadOnlyProperties = true,                        //忽略只讀屬性
            //    PropertyNameCaseInsensitive = true,                     //忽略大小寫
            //                                                            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase     //命名方式是默認還是CamelCase
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            //};


            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            options.WriteIndented = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowTrailingCommas = true;
            //設置時間格式
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //設置bool獲取格式
            options.Converters.Add(new BooleanJsonConverter());
            //設置數字
            options.Converters.Add(new IntJsonConverter());
            //options.PropertyNamingPolicy = new UpperFirstCaseNamingPolicy();
            options.PropertyNameCaseInsensitive = true;                     //忽略大小寫
            return json == null ? null : JsonSerializer.Deserialize<List<T>>(json,options);
        }

        /// <summary>
        /// JSON字符串序列化成DataTable。
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static DataTable ToTable(this string json)
        {
            return json == null ? null : JsonSerializer.Deserialize<DataTable>(json);
        }
    }
}
