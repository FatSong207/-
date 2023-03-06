using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Data;

namespace Yuebon.Commons.Extensions
{
    /// <summary>
    /// 類型<see cref="Type"/>輔助擴展方法類
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 判斷當前類型是否可由指定類型派生
        /// </summary>
        public static bool IsDeriveClassFrom<TBaseType>(this Type type, bool canAbstract = false)
        {
            return IsDeriveClassFrom(type, typeof(TBaseType), canAbstract);
        }

        /// <summary>
        /// 判斷當前類型是否可由指定類型派生
        /// </summary>
        public static bool IsDeriveClassFrom(this Type type, Type baseType, bool canAbstract = false)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(baseType, nameof(baseType));

            return type.IsClass && (!canAbstract && !type.IsAbstract) && type.IsBaseOn(baseType);
        }

        /// <summary>
        /// 判斷類型是否為Nullable類型
        /// </summary>
        /// <param name="type"> 要處理的類型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {
            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 由類型的Nullable類型返回實際類型
        /// </summary>
        /// <param name="type"> 要處理的類型對象 </param>
        /// <returns> </returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        /// <summary>
        /// 通過類型轉換器獲取Nullable類型的基礎類型
        /// </summary>
        /// <param name="type"> 要處理的類型對象 </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                NullableConverter nullableConverter = new NullableConverter(type);
                return nullableConverter.UnderlyingType;
            }
            return type;
        }

        /// <summary>
        /// 獲取類型的Description特性描述信息
        /// </summary>
        /// <param name="type">類型對象</param>
        /// <param name="inherit">是否搜索類型的繼承鏈以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在則返回類型的全名</returns>
        public static string GetDescription(this Type type, bool inherit = true)
        {
            DescriptionAttribute desc = type.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? type.FullName : desc.Description;
        }

        /// <summary>
        /// 獲取成員元數據的Description特性描述信息
        /// </summary>
        /// <param name="member">成員元數據對象</param>
        /// <param name="inherit">是否搜索成員的繼承鏈以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在則返回成員的名稱</returns>
        public static string GetDescription(this MemberInfo member, bool inherit = true)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            if (desc != null)
            {
                return desc.Description;
            }
            DisplayNameAttribute displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
            if (displayName != null)
            {
                return displayName.DisplayName;
            }
            DisplayAttribute display = member.GetAttribute<DisplayAttribute>(inherit);
            if (display != null)
            {
                return display.Name;
            }
            return member.Name;
        }

        /// <summary>
        /// 檢查指定指定類型成員中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要檢查的Attribute特性類型</typeparam>
        /// <param name="memberInfo">要檢查的類型成員</param>
        /// <param name="inherit">是否從繼承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 從類型成員獲取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性類型</typeparam>
        /// <param name="memberInfo">類型類型成員</param>
        /// <param name="inherit">是否從繼承中查找</param>
        /// <returns>存在返回第一個，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return attributes.FirstOrDefault() as T;
        }

        /// <summary>
        /// 從類型成員獲取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性類型</typeparam>
        /// <param name="memberInfo">類型類型成員</param>
        /// <param name="inherit">是否從繼承中查找</param>
        /// <returns>返回所有指定Attribute特性的數組</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        /// <summary>
        /// 判斷類型是否為集合類型
        /// </summary>
        /// <param name="type">要處理的類型</param>
        /// <returns>是返回True，不是返回False</returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// 判斷當前泛型類型是否可由指定類型的實例填充
        /// </summary>
        /// <param name="genericType">泛型類型</param>
        /// <param name="type">指定類型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericType, Type type)
        {
            genericType.CheckNotNull("genericType");
            type.CheckNotNull("type");
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("該功能只支持泛型類型的調用，非泛型類型可使用 IsAssignableFrom 方法。");
            }

            List<Type> allOthers = new List<Type> { type };
            if (genericType.IsInterface)
            {
                allOthers.AddRange(type.GetInterfaces());
            }

            foreach (var other in allOthers)
            {
                Type cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                    {
                        cur = cur.GetGenericTypeDefinition();
                    }
                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                    {
                        return true;
                    }
                    cur = cur.BaseType;
                }
            }
            return false;
        }

        /// <summary>
        /// 方法是否是異步
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        /// <summary>
        /// 返回當前類型是否是指定基類的派生類
        /// </summary>
        /// <param name="type">當前類型</param>
        /// <param name="baseType">要判斷的基類型</param>
        /// <returns></returns>
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition)
            {
                return baseType.IsGenericAssignableFrom(type);
            }
            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 返回當前類型是否是指定基類的派生類
        /// </summary>
        /// <typeparam name="TBaseType">要判斷的基類型</typeparam>
        /// <param name="type">當前類型</param>
        /// <returns></returns>
        public static bool IsBaseOn<TBaseType>(this Type type)
        {
            Type baseType = typeof(TBaseType);
            return type.IsBaseOn(baseType);
        }

        /// <summary>
        /// 獲取類型的全名，附帶所在類庫
        /// </summary>
        public static string GetFullNameWithModule(this Type type)
        {
            return $"{type.FullName},{type.Module.Name.Replace(".dll", "").Replace(".exe", "")}";
        }

        /// <summary>
        /// 獲取類型的顯示短名稱
        /// </summary>
        public static string ShortDisplayName(this Type type)
        {
            return type.DisplayName(false);
        }

        /// <summary>
        /// 獲取類型的顯示名稱
        /// </summary>
        public static string DisplayName([NotNull]this Type type, bool fullName = true)
        {
            StringBuilder sb = new StringBuilder();
            ProcessType(sb, type, fullName);
            return sb.ToString();
        }


        /// <summary>
        /// 獲取主鍵字段
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PropertyInfo GetKeyProperty(this Type entity)
        {
            return entity.GetProperties().GetKeyProperty();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static PropertyInfo GetKeyProperty(this PropertyInfo[] properties)
        {
            return properties.Where(c => c.IsKey()).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsKey(this PropertyInfo propertyInfo)
        {
            object[] keyAttributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);
            if (keyAttributes.Length > 0)
                return true;
            return false;
        }

        #region 私有方法

        private static readonly Dictionary<Type, string> _builtInTypeNames = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" },
            { typeof(void), "void" }
        };

        private static void ProcessType(StringBuilder builder, Type type, bool fullName)
        {
            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                ProcessGenericType(builder, type, genericArguments, genericArguments.Length, fullName);
            }
            else if (type.IsArray)
            {
                ProcessArrayType(builder, type, fullName);
            }
            else if (_builtInTypeNames.TryGetValue(type, out var builtInName))
            {
                builder.Append(builtInName);
            }
            else if (!type.IsGenericParameter)
            {
                builder.Append(fullName ? type.FullName : type.Name);
            }
        }

        private static void ProcessArrayType(StringBuilder builder, Type type, bool fullName)
        {
            var innerType = type;
            while (innerType.IsArray)
            {
                innerType = innerType.GetElementType();
            }

            ProcessType(builder, innerType, fullName);

            while (type.IsArray)
            {
                builder.Append('[');
                builder.Append(',', type.GetArrayRank() - 1);
                builder.Append(']');
                type = type.GetElementType();
            }
        }

        private static void ProcessGenericType(StringBuilder builder, Type type, Type[] genericArguments, int length, bool fullName)
        {
            var offset = type.IsNested ? type.DeclaringType.GetGenericArguments().Length : 0;

            if (fullName)
            {
                if (type.IsNested)
                {
                    ProcessGenericType(builder, type.DeclaringType, genericArguments, offset, fullName);
                    builder.Append('+');
                }
                else
                {
                    builder.Append(type.Namespace);
                    builder.Append('.');
                }
            }

            var genericPartIndex = type.Name.IndexOf('`');
            if (genericPartIndex <= 0)
            {
                builder.Append(type.Name);
                return;
            }

            builder.Append(type.Name, 0, genericPartIndex);
            builder.Append('<');

            for (var i = offset; i < length; i++)
            {
                ProcessType(builder, genericArguments[i], fullName);
                if (i + 1 == length)
                {
                    continue;
                }

                builder.Append(',');
                if (!genericArguments[i + 1].IsGenericParameter)
                {
                    builder.Append(' ');
                }
            }

            builder.Append('>');
        }

        #endregion
    }
}
