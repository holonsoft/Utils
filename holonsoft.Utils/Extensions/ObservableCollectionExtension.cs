using System.Collections.ObjectModel;

namespace holonsoft.Utils.Extensions;
public static class ObservableCollectionExtension
{
  public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector)
  {
    var ordered =
       collection
          .Select((x, i) => (Index: i, Element: x))
          .OrderBy(x => keySelector(x.Element))
          .ToArray();

    for (var i = 0; i < ordered.Length; i++)
    {
      collection.Move(ordered[i].Index, i);
    }
  }
}
