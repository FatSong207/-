﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;

namespace Zxw.Framework.NetCore.Extensions
{
    /// <summary>
    /// 表達式樹常用擴展方法
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        ///     取的  Expression<Func<T,TProperty>> predicate 表達式對應的屬性名稱
        ///         例如：c=>c.Value.Year 側返回：Value.Year
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> predicate)
        {
            var expression = predicate.Body as MemberExpression;
            //return expression.Member.Name; //該屬性只返回最后一個屬性，因此采用下面方法返回。
            return expression.ToString().Substring(2);
        }
        public static ParameterExpression CreateLambdaParam<T>(string name)
        {
            return Expression.Parameter(typeof(T), name);
        }

        /// <summary>
        /// 創建linq表達示的body部分
        /// </summary>
        public static Expression GenerateBody<T>(this ParameterExpression param, Filter filterObj)
        {
            PropertyInfo property = typeof(T).GetProperty(filterObj.Key);

            //組裝左邊
            Expression left = Expression.Property(param, property);
            //組裝右邊
            Expression right = null;

            if (property.PropertyType == typeof(int))
            {
                right = Expression.Constant(int.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                right = Expression.Constant(DateTime.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(string))
            {
                right = Expression.Constant((filterObj.Value));
            }
            else if (property.PropertyType == typeof(decimal))
            {
                right = Expression.Constant(decimal.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(Guid))
            {
                right = Expression.Constant(Guid.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(bool))
            {
                right = Expression.Constant(filterObj.Value.Equals("1"));
            }
            else if (property.PropertyType == typeof(Guid?))
            {
                left = Expression.Property(left, "Value");
                right = Expression.Constant(Guid.Parse(filterObj.Value));
            }
            else
            {
                throw new Exception("暫不能解析該Key的類型");
            }

            //c.XXX=="XXX"
            Expression filter = Expression.Equal(left, right);
            switch (filterObj.Contrast)
            {
                case "<=":
                    filter = Expression.LessThanOrEqual(left, right);
                    break;

                case "<":
                    filter = Expression.LessThan(left, right);
                    break;

                case ">":
                    filter = Expression.GreaterThan(left, right);
                    break;

                case ">=":
                    filter = Expression.GreaterThanOrEqual(left, right);
                    break;
                case "!=":
                    filter = Expression.NotEqual(left, right);
                    break;

                case "like":
                    filter = Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] {typeof(string)}),
                        Expression.Constant(filterObj.Value));
                    break;
                case "not in":
                    var listExpression = Expression.Constant(filterObj.Value.Split(',').ToList()); //數組
                    var method = typeof(List<string>).GetMethod("Contains", new Type[] {typeof(string)}); //Contains語句
                    filter = Expression.Not(Expression.Call(listExpression, method, left));
                    break;
                case "in":
                    var lExp = Expression.Constant(filterObj.Value.Split(',').ToList()); //數組
                    var methodInfo =
                        typeof(List<string>).GetMethod("Contains", new Type[] {typeof(string)}); //Contains語句
                    filter = Expression.Call(lExp, methodInfo, left);
                    break;
            }

            return filter;
        }

        public static Expression<Func<T, bool>> GenerateTypeBody<T>(this ParameterExpression param, Filter filterObj)
        {
            return (Expression<Func<T, bool>>) (param.GenerateBody<T>(filterObj));
        }

        /// <summary>
        /// 創建完整的lambda
        /// </summary>
        public static LambdaExpression GenerateLambda(this ParameterExpression param, Expression body)
        {
            //c=>c.XXX=="XXX"
            return Expression.Lambda(body, param);
        }

        public static Expression<Func<T, bool>> GenerateTypeLambda<T>(this ParameterExpression param, Expression body)
        {
            return (Expression<Func<T, bool>>) (param.GenerateLambda(body));
        }

        public static Expression AndAlso(this Expression expression, Expression expressionRight)
        {
            return Expression.AndAlso(expression, expressionRight);
        }

        public static Expression Or(this Expression expression, Expression expressionRight)
        {
            return Expression.Or(expression, expressionRight);
        }

        public static Expression And(this Expression expression, Expression expressionRight)
        {
            return Expression.And(expression, expressionRight);
        }

        //系統已經有該函數的實現
        //public static IQueryable<T> Where<T>(this IQueryable<T> query, Expression expression)
        //{
        //    Expression expr = Expression.Call(typeof(Queryable), "Where", new[] { typeof(T) },
        //       Expression.Constant(query), expression);
        //    //生成動態查詢
        //    IQueryable<T> result = query.Provider.CreateQuery<T>(expr);
        //    return result;
        //}

        public static IQueryable<T> GenerateFilter<T>(this IQueryable<T> query, string filterjson)
        {
            if (!string.IsNullOrEmpty(filterjson))
            {
                var filters = JsonConvert.DeserializeObject<IEnumerable<Filter>>(filterjson);
                var param = CreateLambdaParam<T>("c");

                Expression result = Expression.Constant(true);
                foreach (var filter in filters)
                {
                    result = result.AndAlso(param.GenerateBody<T>(filter));
                }

                query = query.Where(param.GenerateTypeLambda<T>(result));
            }
            return query;
        }

        /// <summary>
        /// 以特定的條件運行組合兩個Expression表達式
        /// </summary>
        /// <typeparam name="T">表達式的主實體類型</typeparam>
        /// <param name="first">第一個Expression表達式</param>
        /// <param name="second">要組合的Expression表達式</param>
        /// <param name="merge">組合條件運算方式</param>
        /// <returns>組合后的表達式</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            Dictionary<ParameterExpression, ParameterExpression> map =
                first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 以 Expression.AndAlso 組合兩個Expression表達式
        /// </summary>
        /// <typeparam name="T">表達式的主實體類型</typeparam>
        /// <param name="first">第一個Expression表達式</param>
        /// <param name="second">要組合的Expression表達式</param>
        /// <returns>組合后的表達式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// 以 Expression.OrElse 組合兩個Expression表達式
        /// </summary>
        /// <typeparam name="T">表達式的主實體類型</typeparam>
        /// <param name="first">第一個Expression表達式</param>
        /// <param name="second">要組合的Expression表達式</param>
        /// <returns>組合后的表達式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// 參數重新綁定
        /// </summary>
        /// <seealso cref="System.Linq.Expressions.ExpressionVisitor" />
        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            /// <summary>
            /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
            /// </summary>
            /// <param name="map">The map.</param>
            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            /// <summary>
            /// Replaces the parameters.
            /// </summary>
            /// <param name="map">The map.</param>
            /// <param name="exp">The exp.</param>
            /// <returns></returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            /// <summary>
            /// 訪問 <see cref="T:System.Linq.Expressions.ParameterExpression" />。
            /// </summary>
            /// <param name="node">要訪問的表達式。</param>
            /// <returns>
            /// 如果修改了該表達式或任何子表達式，則為修改后的表達式；否則返回原始表達式。
            /// </returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(node, out replacement))
                {
                    node = replacement;
                }
                return base.VisitParameter(node);
            }
        }
    }

    public class Filter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Contrast { get; set; }
    }
}
