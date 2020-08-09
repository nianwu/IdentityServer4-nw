using System.Threading;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace utils.Mongo
{
    public class MongoQueue<TDocument, TProjection> : IEnumerable<TProjection>
    {
        public string Cursor { get; set; } = ObjectId.Empty.ToString();

        // public bool Deceding { get; set; } = false;

        public int Limit { get; set; } = 100;

        public Expression<Func<TDocument, bool>> Condition { get; }
        public Expression<Func<TDocument, object>> SortBy { get; }
        public Expression<Func<TDocument, string>> IdSelector { get; }

        public Expression<Func<TDocument, TProjection>> Projection { get; }
        public Expression<Func<TProjection, string>> ProjectionIdSelector { get; }

        public Action<string> GetCursor { private get; set; }
        public Func<string> SetCursor { private get; set; }

        private IMongoCollection<TDocument> _collection;
        private CancellationToken _cancellationToken;

        public MongoQueue(
            IMongoCollection<TDocument> collection
            , Expression<Func<TDocument, string>> idSelector
            , Expression<Func<TDocument, bool>> condition
            , Expression<Func<TDocument, object>> sortBy
            , Expression<Func<TDocument, TProjection>> projection
            , Expression<Func<TProjection, string>> projectionIdSelector
            , CancellationToken cancellationToken = default
        )
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (idSelector == null)
            {
                throw new ArgumentNullException(nameof(idSelector));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (sortBy == null)
            {
                throw new ArgumentNullException(nameof(sortBy));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (projectionIdSelector == null)
            {
                throw new ArgumentNullException(nameof(projectionIdSelector));
            }

            Condition = condition;
            SortBy = sortBy;
            IdSelector = idSelector;
            Projection = projection;
            ProjectionIdSelector = projectionIdSelector;
            _collection = collection;
            _cancellationToken = cancellationToken;
        }

        public IEnumerator<TProjection> GetEnumerator()
        {
            while (true)
            {
                var dataList = _collection.Find(
                    _collection.Filter().And(
                        _collection.Filter().Gt(IdSelector, Cursor)
                        , _collection.Filter().Where(Condition)
                    )
                )
                .SortAscending(SortBy)
                .Project(Projection)
                .Limit(Limit)
                .ToList(_cancellationToken);

                var lastOrDefault = dataList.LastOrDefault();

                if (lastOrDefault == null)
                {
                    break;
                }

                var newCursor = ProjectionIdSelector.Compile()(lastOrDefault);

                if (!ObjectId.TryParse(newCursor, out _))
                {
                    break;
                }

                if (!dataList.Any())
                {
                    break;
                }

                Cursor = newCursor;

                foreach (var item in dataList)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}