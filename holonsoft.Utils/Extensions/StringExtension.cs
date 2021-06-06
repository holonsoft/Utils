// unset

using holonsoft.Utils.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace holonsoft.Utils.Extensions
{
	public static class StringExtension
	{
		/// <summary>
		/// Letter case of string to use when (re-)formatting
		/// <seealso cref="ConvertCase"/>
		/// </summary>
		public enum CaseFormat
		{
			None,
			CamelCase,
			InvertCamelCase,
			UpperFirstLetter,
			UpperAllFirstLetters,
			LowerFirstLetter,
			LowerAllFirstLetters
		}

		/// <summary>
		/// Regular expression to test, whether a string is a valid GUID or not
		/// </summary>
		private static readonly Regex _regExprGuid = new(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$", RegexOptions.Compiled);

		/// <summary>
		/// Regular expression to test, whether a string is a formal valid email address
		/// </summary>
		private const string _emailRegExprStrict = @"^(([^<>()[\]\\.,;:\s@\""]+" + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" + @"[a-zA-Z]{2,}))$";


		/// <summary>
		/// Regular expression to test, whether a string is a formal(!) valid IBAN or not
		/// </summary>
		private static readonly Regex _regExprIBAN = new(@"[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}", RegexOptions.Compiled);


		/// <summary>
		/// Regular expression to test, whether a string is a formal valid email address
		/// </summary>
		private static readonly Regex _regExprEmail = new(_emailRegExprStrict, RegexOptions.Compiled);

		static readonly char[] _guidBrackets = new char[] { '{', '}' };



		/// <summary>
		/// Formats the casing of a given string
		/// </summary>
		/// <param name="self">The string to format</param>
		/// <param name="newStringFormat">The string format used to convert letter case</param>
		/// <returns>A new formatted string</returns>
		public static string ConvertCase(this string self, CaseFormat newStringFormat)
		{
			return ConvertCase(self, newStringFormat, null);
		}


		/// <summary>
		/// Formats the casing of a given string
		/// </summary>
		/// <param name="self">The string to format</param>
		/// <param name="newStringFormat">The string format used to convert letter case</param>
		/// <param name="ignoreValueList"> </param>
		/// <returns>A new formatted string</returns>
		public static string ConvertCase(this string self, CaseFormat newStringFormat, List<string> ignoreValueList)
		{
			if (string.IsNullOrEmpty(self) || newStringFormat == CaseFormat.None) return self;

			if (ignoreValueList == null)
			{
				ignoreValueList = new List<string>();
			}


			string replaceHelper;

			switch (newStringFormat)
			{
				case CaseFormat.LowerAllFirstLetters:
				case CaseFormat.UpperAllFirstLetters:
				{
					var resultBuilder = new StringBuilder();

					var stringHelperArray = self.Split(new[] { " " }, StringSplitOptions.None);

					for (var i = 0; i < stringHelperArray.Length; i++)
					{
						if (i > 0)
						{
							resultBuilder.Append(' ');
						}

						if (!ignoreValueList.Contains(stringHelperArray[i].ToLower().Trim(new[] { '(', ')' })))
						{

							var characterReplaceOffset = 0;

							if (stringHelperArray[i].StartsWith("("))
							{
								characterReplaceOffset = 1;
							}

							replaceHelper = newStringFormat == CaseFormat.LowerAllFirstLetters ?
																	stringHelperArray[i][characterReplaceOffset].ToString(CultureInfo.InvariantCulture).ToLower() :
																	stringHelperArray[i][characterReplaceOffset].ToString(CultureInfo.InvariantCulture).ToUpper();

							stringHelperArray[i] = stringHelperArray[i].Remove(characterReplaceOffset, 1);

							stringHelperArray[i] = stringHelperArray[i].Insert(characterReplaceOffset, replaceHelper);
						}

						resultBuilder.Append(stringHelperArray[i]);
					}
					return resultBuilder.ToString();
				}
				case CaseFormat.UpperFirstLetter:
				{
					replaceHelper = self[0].ToString(CultureInfo.InvariantCulture).ToUpper();
					return self.Remove(0, 1).Insert(0, replaceHelper);
				}
				case CaseFormat.LowerFirstLetter:
				{
					replaceHelper = self[0].ToString(CultureInfo.InvariantCulture).ToLower();
					return self.Remove(0, 1).Insert(0, replaceHelper);
				}
				case CaseFormat.CamelCase:
				{
					return self.ToLowerInvariant().ConvertCase(CaseFormat.UpperAllFirstLetters);
				}
				case CaseFormat.InvertCamelCase:
				{
					return self.ToUpperInvariant().ConvertCase(CaseFormat.LowerAllFirstLetters);
				}
			}

			return self;
		}


		/// <summary>
		/// Replaces umlauts in a given HTML text
		/// </summary>
		/// <param name="self">Text to transform</param>
		public static string ReplaceHTMLUmlauts(this string self, bool replaceNewlineWithBR)
		{
			var sb = new StringBuilder();

			// TODO: Replace ampersand

			foreach (var c in self)
			{
				switch (c)
				{
					case 'ä':
						sb.Append("&auml;");
						break;
					case 'ö':
						sb.Append("&ouml;");
						break;
					case 'ü':
						sb.Append("&uuml;");
						break;
					case 'Ä':
						sb.Append("&Auml;");
						break;
					case 'Ö':
						sb.Append("&Ouml;");
						break;
					case 'Ü':
						sb.Append("&Uuml;");
						break;
					case 'ß':
						sb.Append("&szlig;");
						break;
					default:
						sb.Append(c);
						break;
				}
			}

			if (replaceNewlineWithBR) return sb.ToString().Replace(Environment.NewLine, "<br>");

			return sb.ToString();
		}


		/// <summary>
		/// This replaces all diacretics (accents) within normalized form
		/// The german umlauts will be replaces within a SINGLE char
		/// This behaviour is correct for international purpose
		/// E.g. ue is the "german way" but in France ü => u
		/// </summary>
		/// <param name="self">Text to transform</param>
		/// <returns>resulting string</returns>
		public static string RemoveDiacritics(this string self)
		{
			//var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(self);
			//return Encoding.UTF8.GetString(tempBytes);

			var normalizedString = self.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
			{
				sb.Append(c);
			}

			return sb.ToString();

		}


		/// <summary>
		/// Normalizes (expands) german umlauts
		/// </summary>
		/// <param name="self">Text to transform</param>
		/// <returns>resulting string</returns>
		public static string NormalizeUmlauts(this string self)
		{
			var sb = new StringBuilder();
			foreach (var c in self)
			{
				switch (c)
				{
					case 'ä':
						sb.Append("ae");
						break;
					case 'ö':
						sb.Append("oe");
						break;
					case 'ü':
						sb.Append("ue");
						break;
					case 'Ä':
						sb.Append("Ae");
						break;
					case 'Ö':
						sb.Append("Oe");
						break;
					case 'Ü':
						sb.Append("Ue");
						break;
					case 'ß':
						sb.Append("ss");
						break;
					default:
						sb.Append(c);
						break;
				}
			}
			return sb.ToString();
		}



		/// <summary>
		/// Safely replace '\n' and '\r' characters in a given string with Environment.NewLine
		/// </summary>
		/// <param name="self">Text to transform</param>
		/// <returns>Transformed string, containing real NewLine values</returns>
		public static string AdjustLineFeedCharacters(this string self)
		{
			var result = "";
			var helperList = self.Split(new[] { '\n' });

			for (var i = 0; i < helperList.Length; i++)
			{
				result += helperList[i].Replace("\r", Environment.NewLine);

				if (i < helperList.Length - 1)
				{
					result += Environment.NewLine;
				}
			}

			return result;
		}


		/// <summary>
		/// Test whether a string contains a valid guid
		/// </summary>
		/// <param name="self">string to be inspected</param>
		/// <returns>true or false</returns>
		public static bool IsValidGuid(this string self)
		{
			try
			{
				return (_regExprGuid.IsMatch(self.Trim(_guidBrackets)));
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// Test whether a string contains a formal valid guid
		/// </summary>
		/// <param name="self">string to be inspected</param>
		/// <returns>true or false</returns>
		public static bool IsFormalValidEmail(this string self)
		{
			try
			{
				return (_regExprEmail.IsMatch(self));
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// Split a given string in n pieces
		/// </summary>
		/// <param name="self">String to be splitted</param>
		/// <param name="chunkSize">Siez of one piece</param>
		/// <returns>List of string pieces</returns>
		public static List<string> SplitIntoPieces(this string self, int chunkSize)
		{
			var result = new List<string>();

			var start = 0;
			var length = self.Length;

			while ((start + chunkSize) <= length)
			{
				result.Add(self.Substring(start, chunkSize));
				start += chunkSize;
			}

			// Is there a rest to be copied?
			if (start < length)
			{
				result.Add(self[start..]);
			}

			return result;
		}


		/// <summary>
		/// Test whether a string content is numeric
		/// </summary>
		/// <param name="self">content to be testet</param>
		/// <param name="allowDecimals">true if decimals are allowed</param>
		/// <returns>true if numeric otherwise false</returns>
		public static bool IsNumeric(this string self, bool allowDecimals)
		{
			if (allowDecimals)
			{
				return Double.TryParse(self, out _);
			}
			else
			{
				return Int32.TryParse(self, out _);
			}
		}


		/// <summary>
		/// Convert a string to a hexadecimal representation
		/// </summary>
		/// <param name="self">string to be converted</param>
		/// <returns>hex string</returns>
		public static string ToHexString(this string self)
		{
			return ToHexString(self, true, false);
		}


		/// <summary>
		/// Convert a string to a hexadecimal representation
		/// </summary>
		/// <param name="self">string to be converted</param>
		/// <param name="addBlanks">add blanks between every converted char, eg. '0x23 0x21'</param>
		/// <returns>hex string</returns>
		public static string ToHexString(this string self, bool addBlanks)
		{
			return ToHexString(self, true, addBlanks);
		}


		/// <summary>
		/// Convert a string to a hexadecimal representation
		/// </summary>
		/// <param name="self">string to be converted</param>
		/// <param name="add0X">add 0X to every string, e.g. '0x23'</param>
		/// <param name="addBlanks">add blanks between every converted char, eg. '0x23 0x21'</param>
		/// <returns>hex string</returns>
		public static string ToHexString(this string self, bool add0X, bool addBlanks)
		{
			var result = new StringBuilder();
			foreach (var c in self)
			{
				if (add0X)
				{
					result.Append("0x");
				}

				result.Append(Convert.ToString(c, 16));
				if (addBlanks)
				{
					result.Append(' ');
				}
			}

			return result.ToString().Trim();
		}


		/// <summary>
		/// Calculate MD5 for the given string
		/// </summary>
		/// <param name="self">source string</param>
		/// <returns>md5 hash</returns>
		public static string CalcMd5HashCode(this string self)
		{
			return CalcMd5HashCode(self, false);
		}


		/// <summary>
		/// Calculate MD5 for the given string
		/// </summary>
		/// <param name="self">source string</param>
		/// <param name="useUpperCase"> </param>
		/// <returns>md5 hash</returns>
		public static string CalcMd5HashCode(this string self, bool useUpperCase)
		{
			byte[] inputBytes = System.Text.Encoding.Default.GetBytes(self);

			var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			var hash = md5CryptoServiceProvider.ComputeHash(inputBytes);

			var result = new StringBuilder();

			foreach (var b in hash)
			{
				result.Append(useUpperCase ? b.ToString("X2") : b.ToString("x2"));
			}

			return result.ToString();
		}


		public static string Quoted(this string self)
		{
			return Quoted(self, '\'');
		}


		public static string Quoted(this string self, char quoteWithChar)
		{
			//return quoteWithChar + self + quoteWithChar;

			var qc = quoteWithChar.ToString();
			return String.Concat(qc, self.Replace(qc, qc + qc), qc);
		}

		public static string Left(this string self, int length)
		{
			return self.Substring(0, length);
		}


		public static string Right(this string self, int length)
		{
			return self[^length..];
		}


		public static string RemoveInvalidChars(this string self, string pattern)
		{
			return self.ReplaceInvalidChars(pattern, "");
		}


		public static string ReplaceInvalidChars(this string self, string pattern, string replacement)
		{
			try
			{
				//string pattern = @"[^a-zA-Z ]";
				self = Regex.Replace(self, pattern, replacement, RegexOptions.None, TimeSpan.FromSeconds(1.5));
			}
			catch (Exception)
			{
				self = string.Empty;
			}

			return self;
		}


		/// <summary>
		/// Repeats a string x times, so total length is count * stringlength
		/// </summary>
		/// <param name="self"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static string Repeat(this string self, int count)
		{
			return string.Concat(Enumerable.Repeat(self, count));
		}


		/// <summary>
		/// Convert string to appropriate data type
		/// </summary>
		/// <exception cref="Exception">Depends on type conversion</exception>
		/// <typeparam name="T">Destination type</typeparam>
		/// <param name="self">string itself</param>
		/// <returns>default(T) if null, empty or whitespace, otherwise converted value</returns>
		public static T GetAs<T>(this string self)
		{
			return GetAs<T>(self, CultureInfo.InvariantCulture);
		}


		/// <summary>
		/// Convert string to appropriate data type
		/// </summary>
		/// <exception cref="Exception">Depends on type conversion</exception>
		/// <typeparam name="T">Destination type</typeparam>
		/// <param name="self">string itself</param>
		/// <param name="cultureInfo">Culture info for conversion</param>
		/// <returns>default(T) if null, empty or whitespace, otherwise converted value</returns>
		public static T GetAs<T>(this string self, CultureInfo cultureInfo)
		{
			if (string.IsNullOrWhiteSpace(self))
			{
				return default;
			}

			if (typeof(T).IsEnum)
			{
				return (T) Enum.Parse(typeof(T), self);
			}

			return (T) Convert.ChangeType(self, typeof(T), cultureInfo);
		}


		/// <summary>
		/// Test whether a given string is a FORMAL valid IBAN
		/// </summary>
		/// <param name="self">string to be inspected</param>
		/// <returns>Tuple(bool, string) with check and additional info</returns>
		public static (bool IsValid, string ErrorMsg) IsFormalValidIBAN(this string self)
		{
			if (string.IsNullOrWhiteSpace(self))
			{
				return (false, "Invalid IBAN string value");
			}

			var result = self.Replace(" ", "");

			try
			{
				var isValid = _regExprIBAN.IsMatch(result);

				if (!isValid)
				{
					return (false, "Formal NOT OK - chars don't match");
				}

				var countryCode = result.Substring(0, 2);

				if (!IBANCountryInfo.IBANCountryList.ContainsKey(countryCode))
				{
					return (false, "Formal NOT OK - country not supported");
				}

				var countryInfo = IBANCountryInfo.IBANCountryList[countryCode];

				if (result.Length != countryInfo.IBANLength)
				{
					return (false, "Formal NOT OK - length doesn't match country specification");
				}

				var checkSum = result.Substring(2, 2);
				var ciphers = result.Remove(0, 4);

				var countryCodeValue = countryInfo.CountryCodeChecksumValue.ToString();

				ciphers += countryCodeValue + checkSum;


				if (!BigInteger.TryParse(ciphers, out BigInteger x))
				{
					return (false, "Formal NOT OK - transformation to number failed");
				}

				if ((x % 97) != 1)
				{
					return (false, "Formal NOT OK - checksum does not match");
				}

				return (true, "Formal OK");
			}
			catch (Exception)
			{
			}

			return (false, "Formal NOT OK - unknown error");
		}


		public static string FormatAsPrintableIBAN(this string self, bool checkValidity)
		{
			if (checkValidity)
			{
				if (!IsFormalValidIBAN(self).IsValid)
				{
					return null;
				}
			}

			var intermediateResult = FormatAsIBANWithoutSpaces(self, checkValidity);

			return intermediateResult
							.SplitIntoPieces(4)
							.Aggregate<string, string>(null, (current, s) => current + (s + " "))
							.Trim();

		}


		public static string FormatAsPrintableIBAN(this string self)
		{
			return FormatAsPrintableIBAN(self, true);
		}


		public static string FormatAsIBANWithoutSpaces(this string self)
		{
			return FormatAsIBANWithoutSpaces(self, true);
		}


		public static string FormatAsIBANWithoutSpaces(this string self, bool checkValidity)
		{
			var intermediateResult = self.Trim().RemoveInvalidChars(" ").ToUpperInvariant();

			if (checkValidity)
			{
				if (!IsFormalValidIBAN(intermediateResult).IsValid)
				{
					return null;
				}
			}

			return intermediateResult;
		}

	}
}