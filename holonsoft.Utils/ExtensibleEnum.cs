using System.Reflection;

namespace holonsoft.Utils;

public abstract record ExtensibleEnum
{
  protected ExtensibleEnum(string name)
    => Name = name;

  public string Name { get; init; }
}

public abstract record ExtensibleEnum<TSelf, TValue> : ExtensibleEnum, IComparable, IComparable<TSelf>
  where TSelf : ExtensibleEnum<TSelf, TValue>
  where TValue : struct
{
  private static Dictionary<string, TSelf> _valuesByName;
  private static Dictionary<TValue, TSelf> _valuesByValue;

  protected ExtensibleEnum(TValue value, string name) : base(name)
    => Value = value;

  public TValue Value { get; init; }

  public int CompareTo(object obj)
  {
    if (obj == null)
    {
      return 1;
    }

    if (obj is TSelf otherObj)
    {
      return CompareTo(otherObj);
    }

    throw new ArgumentException($"Cannot compare {GetType()} to {obj.GetType()}.");
  }

  public int CompareTo(TSelf other)
  {
    if (other == null)
    {
      return 1;
    }

    return Comparer<TValue>.Default.Compare(Value, other.Value);
  }

  private static void EnsureDictionariesInitialized()
  {
    _valuesByName
      ??= ReflectionUtils.AllTypes.Values
        .Where(x => !x.IsAbstract && typeof(TSelf).IsAssignableFrom(x))
        .SelectMany(x => x.GetFields(BindingFlags.Public | BindingFlags.Static))
        .Where(x => typeof(TSelf).IsAssignableFrom(x.FieldType))
        .Select(x => (TSelf) x.GetValue(null))
        .ToDictionary(x => x.Name);

    _valuesByValue
      ??= _valuesByName
        .Values
        .ToDictionary(x => x.Value);
  }

  private static Dictionary<string, TSelf> GetValuesByNameInternal()
  {
    EnsureDictionariesInitialized();
    return _valuesByName;
  }

  private static Dictionary<TValue, TSelf> GetValuesByValueInternal()
  {
    EnsureDictionariesInitialized();
    return _valuesByValue;
  }

  public static TValue[] GetUnderlyingValues()
    => GetValuesByValueInternal().Keys.ToArray();

  public static TSelf[] GetValues()
    => GetValuesByNameInternal().Values.ToArray();

  public static string[] GetNames()
    => GetValuesByNameInternal().Keys.ToArray();

  public static bool TryParse(string name, out TSelf value)
    => GetValuesByNameInternal().TryGetValue(name, out value);

  public static TSelf Parse(string name)
  {
    if (!TryParse(name, out var value))
    {
      throw new KeyNotFoundException(name);
    }

    return value;
  }

  public static bool TryGetValueByUnderlyingValue(TValue underlyingValue, out TSelf value)
    => GetValuesByValueInternal().TryGetValue(underlyingValue, out value);

  public static TSelf GetValueByUnderlyingValue(TValue underlyingValue)
  {
    if (!TryGetValueByUnderlyingValue(underlyingValue, out var value))
    {
      throw new KeyNotFoundException(underlyingValue.ToString());
    }

    return value;
  }
}

public abstract record ExtensibleEnum<TSelf> : ExtensibleEnum<TSelf, int>
  where TSelf : ExtensibleEnum<TSelf>
{
  protected ExtensibleEnum(int value, string name) : base(value, name) { }
}
