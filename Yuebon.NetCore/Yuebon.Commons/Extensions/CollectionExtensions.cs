﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuebon.Commons.Data;

namespace Yuebon.Commons.Extensions
{
    /// <summary>
    /// 集合擴展方法
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 如果條件成立，添加項
        /// </summary>
        public static void AddIf<T>(this ICollection<T> collection, T value, bool flag)
        {
            Check.NotNull(collection, nameof(collection));
            if (flag)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果條件成立，添加項
        /// </summary>
        public static void AddIf<T>(this ICollection<T> collection, T value, Func<bool> func)
        {
            Check.NotNull(collection, nameof(collection));
            if (func())
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果不存在，添加項
        /// </summary>
        public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc = null)
        {
            Check.NotNull(collection, nameof(collection));
            bool exists = existFunc == null ? collection.Contains(value) : existFunc(value);
            if (!exists)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果不為空，添加項
        /// </summary>
        public static void AddIfNotNull<T>(this ICollection<T> collection, T value) where T : class
        {
            Check.NotNull(collection, nameof(collection));
            if (value != null)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 獲取對象，不存在對使用委托添加對象
        /// </summary>
        public static T GetOrAdd<T>(this ICollection<T> collection, Func<T, bool> selector, Func<T> factory)
        {
            Check.NotNull(collection, nameof(collection));
            T item = collection.FirstOrDefault(selector);
            if (item == null)
            {
                item = factory();
                collection.Add(item);
            }

            return item;
        }
    }
}
