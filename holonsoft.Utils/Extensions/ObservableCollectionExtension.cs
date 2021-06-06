using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace holonsoft.Utils.Extensions
{
	public static class ObservableCollectionExtension
	{
		public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector)
		{
			(int Index, T Element)[] ordered =
				 collection
						.Select((x, i) => (Index: i, Element: x))
						.OrderBy(x => keySelector(x.Element))
						.ToArray();

			for (int i = 0; i < ordered.Length; i++)
				collection.Move(ordered[i].Index, i);
		}
	}
}
