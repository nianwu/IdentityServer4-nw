using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace utils.Mongo
{
    public static class IMongoCollectionExtend
    {
        /// <summary>
        /// 快捷构建器
        /// </summary>
        public static FilterDefinitionBuilder<T> Filter<T>(this IMongoCollection<T> e) => Builders<T>.Filter;

        /// <summary>
        /// 快捷构建器
        /// </summary>
        public static SortDefinitionBuilder<T> Sort<T>(this IMongoCollection<T> e) => Builders<T>.Sort;

        /// <summary>
        /// 快捷构建器
        /// </summary>
        public static UpdateDefinitionBuilder<T> Update<T>(this IMongoCollection<T> e) => Builders<T>.Update;

        /// <summary>
        /// 快捷构建器
        /// </summary>
        public static ProjectionDefinitionBuilder<T> Projection<T>(this IMongoCollection<T> e) => Builders<T>.Projection;

        /// <summary>
        /// 获取mongo队列
        /// </summary>
        /// <param name="e"></param>
        /// <param name="idSelector"></param>
        /// <param name="condition"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <returns></returns>
        public static MongoQueue<TDocument, TProjection> GetMongoQueue<TDocument, TProjection>(
            this IMongoCollection<TDocument> e
            , Expression<Func<TDocument, string>> idSelector
            , Expression<Func<TDocument, bool>> condition
            , Expression<Func<TDocument, object>> sortBy
            , Expression<Func<TDocument, TProjection>> projection
            , Expression<Func<TProjection, string>> projectionIdSelector
            , CancellationToken cancellationToken
        )
        {
            return new MongoQueue<TDocument, TProjection>(e, idSelector, condition, sortBy, projection, projectionIdSelector, cancellationToken);
        }
    }

    public static class IFindFluentExtend
    {
        public static IFindFluent<TDocument, TProjection> SortAscending<TDocument, TProjection>(this IFindFluent<TDocument, TProjection> e, Expression<Func<TDocument, object>> field)
        {
            return e.Sort(Builders<TDocument>.Sort.Ascending(field));
        }

        public static IFindFluent<TDocument, TProjection> SortDescending<TDocument, TProjection>(this IFindFluent<TDocument, TProjection> e, Expression<Func<TDocument, object>> field)
        {
            return e.Sort(Builders<TDocument>.Sort.Descending(field));
        }
    }
}