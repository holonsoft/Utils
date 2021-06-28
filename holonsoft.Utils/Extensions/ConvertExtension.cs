// unset

using System;
using System.Globalization;

namespace holonsoft.Utils.Extensions
{
	public class ConvertExtension
	{
		/// <summary>
		/// Convert a string to a given type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="typeOfValue"></param>
		/// <param name="value"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public static T GetValue<T>(Type typeOfValue, string value, CultureInfo culture)
		{
			if (typeOfValue.IsEnum)
			{
				return (T) Enum.Parse(typeOfValue, value);
			}

			return (T) Convert.ChangeType(value, typeOfValue, culture);
		}

		/// <summary>
		/// Convert a string to a given type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="typeOfValue"></param>
		/// <param name="value"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public static T GetValue<T>(string typeOfValue, string value, CultureInfo culture)
		{
			//return GetValue<T>(ReflectionUtils.FindTypeByNameInAnyNonDynamicAssembly(typeOfValue), value, culture);
			return GetValue<T>(ReflectionUtils.SearchType(typeOfValue), value, culture);
		}



	}
}