using System;
using System.Collections.Generic;
using System.Linq;

namespace holonsoft.Utils
{
	public static class LinqHelper
	{
		public static IEnumerable<TSource> DistinctBy<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> propertySelector)
			 => source.GroupBy(propertySelector).Select(x => x.First());

	}
}
