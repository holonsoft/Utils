using System;
using System.Collections.Generic;
using System.Linq;

namespace holonsoft.Utils.Extensions
{
	public static class LinqExtension
	{
#if !NET6_0_OR_GREATER
		public static IEnumerable<TSource> DistinctBy<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> propertySelector)
			 => source.GroupBy(propertySelector).Select(x => x.First());
#endif

		public static IEnumerable<T> OrEmpty<T>(this T self)
			where T : class
		{
			return (IEnumerable<T>) self ?? Enumerable.Empty<T>();
		}

	}
}
