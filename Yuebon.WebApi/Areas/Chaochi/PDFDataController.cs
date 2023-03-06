using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Net;
using Yuebon.Commons.Options;
using Yuebon.Security.Application;
using Yuebon.Chaochi.Dtos;
using Yuebon.Security.IServices;
using Yuebon.Chaochi.Models;
using System.Collections;
using Newtonsoft.Json;
using System.Dynamic;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using PdfSharp.Pdf.AcroForms;
//using UglyToad.PdfPig;
//using UglyToad.PdfPig.AcroForms;
//using UglyToad.PdfPig.AcroForms.Fields;
//using UglyToad.PdfPig.Tokens;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// PDF資料交換控制器
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/[controller]")]
    [LogController]
    public class PDFDataController : ApiController
    {
        /// <summary>
        /// 構造函數注入服務
        /// </summary>
        public PDFDataController()
        {
        }
    }

    //https://stackoverflow.com/questions/68184782/how-to-merge-two-objects-overriding-null-values
    /// <summary>
    /// 
    /// </summary>
    public static class Merger
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <returns></returns>
        public static T DictCloneAndMerge<T>(T baseObject, Dictionary<string, object> overrideObject) where T : new()
        {
            var t = typeof(T);
            var publicProperties = t.GetProperties();
            var publicPropertiesJson = typeof(ExpandoObject).GetProperties();

            var output = new T();

            foreach (var propInfo in publicProperties) {

                var baseValue = propInfo.GetValue(baseObject);
                var overrideValue = baseValue;
                foreach (var d in overrideObject) {
                    //Console.WriteLine($"{d.Key} is {d.Value}");
                    if (propInfo.GetGetMethod().Name == ("get_" + d.Key)) {
                        overrideValue = d.Value;
                    }
                }
                if (baseValue == overrideValue) {
                    propInfo.SetValue(output, baseValue);
                } else {
                    if (overrideValue is Boolean) {
                        propInfo.SetValue(output, (Boolean)overrideValue ? "/Yes" : "/Off");

                    } else {
                        propInfo.SetValue(output, overrideValue);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <returns></returns>
        public static T JsonCloneAndMerge<T>(T baseObject, ExpandoObject overrideObject) where T : new()
        {
            var t = typeof(T);
            var publicProperties = t.GetProperties();
            var publicPropertiesJson = typeof(ExpandoObject).GetProperties();

            var output = new T();

            foreach (var propInfo in publicProperties) {
                var baseValue = propInfo.GetValue(baseObject);
                var overrideValue = baseValue;
                foreach (var d in overrideObject) {
                    //Console.WriteLine($"{d.Key} is {d.Value}");
                    if (propInfo.GetGetMethod().Name == ("get_" + d.Key)) {
                        overrideValue = d.Value;
                    }
                }
                if (baseValue == overrideValue) {
                    propInfo.SetValue(output, baseValue);
                } else {
                    propInfo.SetValue(output, overrideValue);
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <returns></returns>
        public static T CloneToBase<T>(T baseObject, T overrideObject) where T : new()
        {
            var t = typeof(T);
            var publicProperties = t.GetProperties();

            var output = new T();

            foreach (var propInfo in publicProperties) {
                var overrideValue = propInfo.GetValue(overrideObject);
                var defaultValue = propInfo.GetValue(baseObject);
                //var defaultValue = !propInfo.PropertyType.IsValueType
                //    ? null
                //    : Activator.CreateInstance(propInfo.PropertyType);
                //if (overrideValue == defaultValue)
                //if (overrideValue == null || (overrideValue is string && string.IsNullOrWhiteSpace((string)overrideValue)))
                //{
                //    //propInfo.SetValue(output, propInfo.GetValue(baseObject));
                //    propInfo.SetValue(output, defaultValue);
                //}
                //else 
                if (overrideValue is ICollection) {
                    if (((ICollection)overrideValue).Count == 0) {
                        propInfo.SetValue(output, defaultValue);
                    } else {
                        propInfo.SetValue(output, overrideValue);
                    }
                } else {
                    propInfo.SetValue(output, overrideValue);
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <returns></returns>
        public static T CloneAndMerge<T>(T baseObject, T overrideObject) where T : new()
        {
            var t = typeof(T);
            var publicProperties = t.GetProperties();

            var output = new T();

            foreach (var propInfo in publicProperties) {
                var overrideValue = propInfo.GetValue(overrideObject);
                var defaultValue = propInfo.GetValue(baseObject);
                //var defaultValue = !propInfo.PropertyType.IsValueType
                //    ? null
                //    : Activator.CreateInstance(propInfo.PropertyType);
                //if (overrideValue == defaultValue)
                if (overrideValue == null || (overrideValue is string && string.IsNullOrWhiteSpace((string)overrideValue))) {
                    //propInfo.SetValue(output, propInfo.GetValue(baseObject));
                    propInfo.SetValue(output, defaultValue);
                } else if (overrideValue is ICollection) {
                    if (((ICollection)overrideValue).Count == 0) {
                        propInfo.SetValue(output, defaultValue);
                    } else {
                        propInfo.SetValue(output, overrideValue);
                    }
                } else {
                    propInfo.SetValue(output, overrideValue);
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <returns></returns>
        public static T CloneAndDiff<T>(T baseObject, T overrideObject) where T : new()
        {
            var t = typeof(T);
            var publicProperties = t.GetProperties();

            var output = new T();

            foreach (var propInfo in publicProperties) {
                var overrideValue = propInfo.GetValue(overrideObject);
                var defaultValue = propInfo.GetValue(baseObject);
                //var defaultValue = !propInfo.PropertyType.IsValueType
                //    ? null
                //    : Activator.CreateInstance(propInfo.PropertyType);
                if (overrideValue == defaultValue)
                //if (defaultValue == null || (defaultValue is string && string.IsNullOrWhiteSpace((string)defaultValue)))
                {
                    //propInfo.SetValue(output, propInfo.GetValue(baseObject));
                    //propInfo.SetValue(output, overrideValue);
                } else {
                    propInfo.SetValue(output, defaultValue);
                }
            }

            return output;
        }
    }
}