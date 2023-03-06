using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Commons.Properties;

namespace Yuebon.Commons.Data
{
    /// <summary>
    /// 參數合法性檢查類
    /// </summary>
    [DebuggerStepThrough]
    public static class Check
    {
        /// <summary>
        /// 驗證指定值的斷言<paramref name="assertion"/>是否為真，如果不為真，拋出指定消息<paramref name="message"/>的指定類型<typeparamref name="TException"/>異常
        /// </summary>
        /// <typeparam name="TException">異常類型</typeparam>
        /// <param name="assertion">要驗證的斷言。</param>
        /// <param name="message">異常消息。</param>
        private static void Require<TException>(bool assertion, string message)
            where TException : Exception
        {
            if (assertion)
            {
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }

        /// <summary>
        /// 驗證指定值的斷言表達式是否為真，不為值拋出<see cref="Exception"/>異常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="assertionFunc">要驗證的斷言表達式</param>
        /// <param name="message">異常消息</param>
        public static void Required<T>(T value, Func<T, bool> assertionFunc, string message)
        {
            if (assertionFunc == null)
            {
                throw new ArgumentNullException(nameof(assertionFunc));
            }
            Require<Exception>(assertionFunc(value), message);
        }

        /// <summary>
        /// 驗證指定值的斷言表達式是否為真，不為真拋出<typeparamref name="TException"/>異常
        /// </summary>
        /// <typeparam name="T">要判斷的值的類型</typeparam>
        /// <typeparam name="TException">拋出的異常類型</typeparam>
        /// <param name="value">要判斷的值</param>
        /// <param name="assertionFunc">要驗證的斷言表達式</param>
        /// <param name="message">異常消息</param>
        public static void Required<T, TException>(T value, Func<T, bool> assertionFunc, string message) where TException : Exception
        {
            if (assertionFunc == null)
            {
                throw new ArgumentNullException("assertionFunc");
            }
            Require<TException>(assertionFunc(value), message);
        }

        /// <summary>
        /// 檢查參數不能為空引用，否則拋出<see cref="ArgumentNullException"/>異常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(T value, string paramName)
        {
            Require<ArgumentNullException>(value != null, string.Format(Resources.ParameterCheck_NotNull, paramName));
        }

        /// <summary>
        /// 檢查字符串不能為空引用或空字符串，否則拋出<see cref="ArgumentNullException"/>異常或<see cref="ArgumentException"/>異常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty(string value, string paramName)
        {
            Require<ArgumentException>(!string.IsNullOrEmpty(value), string.Format(Resources.ParameterCheck_NotNullOrEmpty_String, paramName));
        }

        /// <summary>
        /// 檢查Guid值不能為Guid.Empty，否則拋出<see cref="ArgumentException"/>異常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotEmpty(Guid value, string paramName)
        {
            Require<ArgumentException>(value != Guid.Empty, string.Format(Resources.ParameterCheck_NotEmpty_Guid, paramName));
        }

        /// <summary>
        /// 檢查集合不能為空引用或空集合，否則拋出<see cref="ArgumentNullException"/>異常或<see cref="ArgumentException"/>異常。
        /// </summary>
        /// <typeparam name="T">集合項的類型。</typeparam>
        /// <param name="list"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty<T>(IReadOnlyList<T> list, string paramName)
        {
            NotNull(list, paramName);
            Require<ArgumentException>(list.Any(), string.Format(Resources.ParameterCheck_NotNullOrEmpty_Collection, paramName));
        }

        /// <summary>
        /// 檢查集合中沒有包含值為null的項
        /// </summary>
        public static void HasNoNulls<T>(IReadOnlyList<T> list, string paramName)
        {
            NotNull(list, paramName);
            Require<ArgumentException>(list.All(m => m != null), string.Format(Resources.ParameterCheck_NotContainsNull_Collection, paramName));
        }

        /// <summary>
        /// 檢查參數必須小于[或可等于，參數<paramref name="canEqual"/>]指定值，否則拋出<see cref="ArgumentOutOfRangeException"/>異常。
        /// </summary>
        /// <typeparam name="T">參數類型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <param name="target">要比較的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LessThan<T>(T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0;
            string format = canEqual ? Resources.ParameterCheck_NotLessThanOrEqual : Resources.ParameterCheck_NotLessThan;
            Require<ArgumentOutOfRangeException>(flag, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 檢查參數必須大于[或可等于，參數<paramref name="canEqual"/>]指定值，否則拋出<see cref="ArgumentOutOfRangeException"/>異常。
        /// </summary>
        /// <typeparam name="T">參數類型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <param name="target">要比較的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void GreaterThan<T>(T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            string format = canEqual ? Resources.ParameterCheck_NotGreaterThanOrEqual : Resources.ParameterCheck_NotGreaterThan;
            Require<ArgumentOutOfRangeException>(flag, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 檢查參數必須在指定范圍之間，否則拋出<see cref="ArgumentOutOfRangeException"/>異常。
        /// </summary>
        /// <typeparam name="T">參數類型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <param name="start">比較范圍的起始值。</param>
        /// <param name="end">比較范圍的結束值。</param>
        /// <param name="startEqual">是否可等于起始值</param>
        /// <param name="endEqual">是否可等于結束值</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Between<T>(T value, string paramName, T start, T end, bool startEqual = false, bool endEqual = false)
            where T : IComparable<T>
        {
            bool flag = startEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
            string message = startEqual
                ? string.Format(Resources.ParameterCheck_Between, paramName, start, end)
                : string.Format(Resources.ParameterCheck_BetweenNotEqual, paramName, start, end, start);
            Require<ArgumentOutOfRangeException>(flag, message);

            flag = endEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0;
            message = endEqual
                ? string.Format(Resources.ParameterCheck_Between, paramName, start, end)
                : string.Format(Resources.ParameterCheck_BetweenNotEqual, paramName, start, end, end);
            Require<ArgumentOutOfRangeException>(flag, message);
        }

        /// <summary>
        /// 檢查指定路徑的文件夾必須存在，否則拋出<see cref="DirectoryNotFoundException"/>異常。
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void DirectoryExists(string directory, string paramName = null)
        {
            NotNull(directory, paramName);
            Require<DirectoryNotFoundException>(Directory.Exists(directory), string.Format(Resources.ParameterCheck_DirectoryNotExists, directory));
        }

        /// <summary>
        /// 檢查指定路徑的文件必須存在，否則拋出<see cref="FileNotFoundException"/>異常。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException">當文件路徑為null時</exception>
        /// <exception cref="FileNotFoundException">當文件路徑不存在時</exception>
        public static void FileExists(string filename, string paramName = null)
        {
            NotNull(filename, paramName);
            Require<FileNotFoundException>(File.Exists(filename), string.Format(Resources.ParameterCheck_FileNotExists, filename));
        }

        /// <summary>
        /// 檢查<see cref="IInputDto{TKey}"/>各屬性的合法性，否則拋出<see cref="ValidationException"/>異常
        /// </summary>
        public static void Validate<TKey>(IInputDto<TKey> dto, string paramName)
        {
            NotNull(dto, paramName);
            dto.Validate();
        }

        /// <summary>
        /// 檢查<see cref="IInputDto{TKey}"/>各屬性的合法性，否則拋出<see cref="ValidationException"/>異常
        /// </summary>
        public static void Validate<TInputDto, TKey>(TInputDto[] dtos, string paramName) where TInputDto : IInputDto<TKey>
        {
            NotNull(dtos, paramName);
            foreach (TInputDto dto in dtos)
            {
                dto.Validate();
            }
        }
    }
}
