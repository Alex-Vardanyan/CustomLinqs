using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomLinq
{
    static class MyLinqExt
    {
        public static IEnumerable<TResult> SelectExt<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource,TResult> selector)
        {
            var result = new List<TResult>();
            foreach(TSource item in source)
            {
                result.Add(selector(item));
            }
            return result;
        }

        public static IEnumerable<TSource> WhereExt<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> predicate)
        {
            var result = new List<TSource>();
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static IEnumerable<IGrouping<TKey,TSource>> GroupByExt<TSource,TKey>(this IEnumerable<TSource> source, Func<TSource,TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<TSource>>();
            foreach (TSource item in source)
            {
                var key = keySelector(item);
                if (!result.ContainsKey(key))
                {
                    result.Add(key, new List<TSource> { item});
                }
                else
                {
                    result[key].Add(item);
                }
            }

            foreach (var item in result)
            {
                yield return new Grouping<TKey, TSource>(item.Key, item.Value);
            }            

        }

        class Grouping<TKey, TSource> : IGrouping<TKey, TSource>
        {

            private TKey _key;
            private IEnumerable<TSource> _elements;
            public TKey Key { get { return _key; }}

            public Grouping(TKey tkey,IEnumerable<TSource> elements)
            {
                _key = tkey;
                _elements = elements;
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                return _elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static List<TSource> ToListExt<TSource>(this IEnumerable<TSource> source)
        {
            var result = new List<TSource>();
            foreach (var item in source)
            {
                result.Add(item);
            }
            return result;
        }

        public static IOrderedEnumerable<TSource> OrderByExt<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var result = new SortedList<TKey, TSource>();
            foreach(TSource item in source)
            {
                result.Add(keySelector(item), item);
                Console.WriteLine(keySelector(item));
            }

            return new OrderedEnum<TKey, TSource>(result.Values);
        }

        public static IOrderedEnumerable<TSource> OrderByDescExt<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var result = new SortedList<TKey, TSource>();
            foreach (TSource item in source)
            {
                result.Add(keySelector(item), item);
                Console.WriteLine(keySelector(item));
            }

            var descResult = new List<TSource>();
            for (int i = 0; i < result.Count; i++)
            {
                descResult.Add(result.Values[result.Count - i -1]);
            }
            return new OrderedEnum<TKey, TSource>(descResult);
        }


        class OrderedEnum<TKey,TSource> : IOrderedEnumerable<TSource>
        {
            private IEnumerable<TSource> _list;
            public OrderedEnum(IEnumerable<TSource> source)
            {
                    _list = source;
            }

            public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


        public static Dictionary<TKey, TSource> ToDictionaryExt<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource,TKey> keySelector)
        {
            var result = new Dictionary<TKey, TSource>();
            foreach (TSource item in source)
            {
                var key = keySelector(item);
                result.Add(key, item);
            }
            return result;
        }
    }
}
