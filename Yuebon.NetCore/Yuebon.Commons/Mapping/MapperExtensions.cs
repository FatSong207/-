using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Properties;

namespace Yuebon.Commons.Mapping
{
    /// <summary>
    /// 對象映射擴展操作
    /// </summary>
    public static class MapperExtensions
    {
        private static IMapper _mapper;

        /// <summary>
        /// 設置對象映射執行者
        /// </summary>
        /// <param name="mapper">映射執行者</param>
        public static void SetMapper(IMapper mapper)
        {
            mapper.CheckNotNull("mapper");
            _mapper = mapper;
        }

        /// <summary>
        /// 將對象映射為指定類型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目標類型</typeparam>
        /// <param name="source">源對象</param>
        /// <returns>目標類型的對象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            CheckMapper();
            return _mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 使用源類型的對象更新目標類型的對象
        /// </summary>
        /// <typeparam name="TSource">源類型</typeparam>
        /// <typeparam name="TTarget">目標類型</typeparam>
        /// <param name="source">源對象</param>
        /// <param name="target">待更新的目標對象</param>
        /// <returns>更新后的目標類型對象</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            CheckMapper();
            return _mapper.Map(source, target);
        }

        /// <summary>
        /// 將數據源映射為指定<typeparamref name="TOutputDto"/>的集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TOutputDto"></typeparam>
        /// <param name="source"></param>
        /// <param name="membersToExpand"></param>
        /// <returns></returns>
        public static IQueryable<TOutputDto> ToOutput<TEntity, TOutputDto>(this IQueryable<TEntity> source,
            params Expression<Func<TOutputDto, object>>[] membersToExpand)
        {
            CheckMapper();
            return _mapper.ProjectTo<TOutputDto>(source, membersToExpand);
        }

        /// <summary>
        /// 集合到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static List<T> MapTo<T>(this IEnumerable obj)
        {
            CheckMapper();
            return _mapper.Map<List<T>>(obj);
        }

        /// <summary>
        /// 驗證映射執行者是否為空
        /// </summary>
        private static void CheckMapper()
        {
            if (_mapper == null)
            {
                throw new NullReferenceException(Resources.Map_MapperIsNull);
            }
        }
    }
}
