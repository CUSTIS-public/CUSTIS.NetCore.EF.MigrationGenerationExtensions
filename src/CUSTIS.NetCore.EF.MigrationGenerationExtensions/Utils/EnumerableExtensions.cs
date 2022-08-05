using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils
{
    /// <summary> Расширения из AppUtils </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>Преобразует список в строку, разделенную запятыми</summary>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> value)
        {
            return value.Where(v => v is not null).Cast<T>();
        }

        /// <summary> Делит исходную последовательность на 3 группы элементов в зависимости от их класса </summary>
        public static void DivideByType<TSrc, T1, T2>(
            this IEnumerable<TSrc> source,
            out IReadOnlyCollection<T1> first,
            out IReadOnlyCollection<T2> second,
            out IReadOnlyCollection<TSrc> other)
            where T1 : TSrc
            where T2 : TSrc
            where TSrc : class
        {
            var result = source.DivideByType(new[] { typeof(T1), typeof(T2) });
            first = (IReadOnlyCollection<T1>)result[0];
            second = (IReadOnlyCollection<T2>)result[1];
            other = (IReadOnlyCollection<TSrc>)result[2];
        }

        /// <summary> Делит исходную последовательность на группы элементов в зависимости от их класса </summary>
        public static IReadOnlyList<IList> DivideByType<TSrc>(
            this IEnumerable<TSrc> source, IReadOnlyCollection<Type> types)
            where TSrc : class
        {
            var typesWithSrc = types.Concat(new[] { typeof(TSrc) }).ToArray();
            var result = typesWithSrc
                .Select(t => (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t))!)
                .ToArray();

            foreach (var element in source)
            {
                for (var i = 0; i < typesWithSrc.Length; i++)
                {
                    if (typesWithSrc[i].IsInstanceOfType(element))
                    {
                        result[i].Add(element);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>Преобразует список в строку, разделенную запятыми</summary>
        public static string JoinBySeparator<T>(this IEnumerable<T> value, string separator)
        {
            return string.Join(separator, value.Select(val => val?.ToString()));
        }
    }
}