# Utils
Utility functions for other projects of holonsoft

## Assembly extensions

public static Type SearchType(string typeToSearch)

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
  
  
  
