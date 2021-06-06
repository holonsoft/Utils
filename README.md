# Utils
Utility functions for other projects of holonsoft

## ReflectionUtils
Cached access to types. Loads all assemblies from disk so it really looks in >all< assemblies.

  public static Type FindTypeByNameInAnyAssembly(string typeName)
  public static Type FindTypeByNameInAnyNonDynamicAssembly(string typeName)
  public static Type FindTypeByNameInAnyDynamicAssembly(string typeName)


## CancellationTokenExtension

  public static Task AsAwaitable(this CancellationToken cancellationToken)

## ColorExtension

  public static Color FromStringWithDelimiters(string colorValueAsString, char delimiter)
  
  public static string ToStringWithDelimiter(this Color self, char delimiter)
  
  public static Color ChangeBrightness(this Color self, float correctionFactor)

## ConvertExtension

  public static T GetValue<T>(Type typeOfValue, string value, CultureInfo culture)
  
  public static T GetValue<T>(string typeOfValue, string value, CultureInfo culture)
  
## ExceptionExtension

  public static string Flatten(this Exception exception)
  
## IPAddressExtension

  public static bool IsIPv4Multicast(this IPAddress self)
  
  public static bool IsEitherV4OrV6Multicast(this IPAddress self)
  
  public static bool IsV4Address(this IPAddress self)

## LinqExtension
  
  public static IEnumerable<TSource> DistinctBy<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> propertySelector)
  
  public static IEnumerable<T> OrEmpty<T>(this T self)
  
## ObservableCollectionExtension
  
  public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector)
  
## StringExtension
  
  public static (bool IsValid, string ErrorMsg) IsFormalValidIBAN(this string self)
  
  public static List<string> SplitIntoPieces(this string self, int chunkSize)
  
  public static T GetAs<T>(this string self)
  
  public static T GetAs<T>(this string self, CultureInfo cultureInfo)
  
  public static bool IsFormalValidEmail(this string self)
  
  public static bool IsNumeric(this string self, bool allowDecimals)
  
  public static bool IsValidGuid(this string self)
  
  public static string AdjustLineFeedCharacters(this string self)
  
  public static string CalcMd5HashCode(this string self)
  
  public static string CalcMd5HashCode(this string self, bool useUpperCase)
  
  public static string ConvertCase(this string self, CaseFormat newStringFormat)
  
  public static string ConvertCase(this string self, CaseFormat newStringFormat, List<string> ignoreValueList)
  
  public static string FormatAsIBANWithoutSpaces(this string self)
  
  public static string FormatAsIBANWithoutSpaces(this string self, bool checkValidity)
  
  public static string FormatAsPrintableIBAN(this string self)
  
  public static string FormatAsPrintableIBAN(this string self, bool checkValidity)
  
  public static string Left(this string self, int length)
  
  public static string NormalizeUmlauts(this string self)
  
  public static string Quoted(this string self)
  
  public static string Quoted(this string self, char quoteWithChar)
  
  public static string RemoveDiacritics(this string self)
  
  public static string RemoveInvalidChars(this string self, string pattern)
  
  public static string Repeat(this string self, int count)
  
  public static string ReplaceHTMLUmlauts(this string self, bool replaceNewlineWithBR)
  
  public static string ReplaceInvalidChars(this string self, string pattern, string replacement)
  
  public static string Right(this string self, int length)
  
  public static string ToHexString(this string self)
  
  public static string ToHexString(this string self, bool add0X, bool addBlanks)
  
  public static string ToHexString(this string self, bool addBlanks)
  
  
