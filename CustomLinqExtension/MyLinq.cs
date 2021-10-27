using System;
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
                if (result.ContainsKey(keySelector(item)) == false)
                {
                    result.Add(keySelector(item), new List<TSource>());
                }
            }

            foreach (var item in source)
            {
                result[keySelector(item)].Add(item);
            }

            return result as IEnumerable<IGrouping<TKey, TSource>>;
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
                result.TryAdd(keySelector(item), item);
            }
            return result.Values as IOrderedEnumerable<TSource>;
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
