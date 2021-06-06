using holonsoft.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace holonsoft.Utils.Test
{
	public class TestStringExtension
	{
		[Fact]
		public void TestConvertCase()
		{
			const string h1 = "thIS iS a VERY cRAZY STring!";
			const string h2 = "AnD a CRAZY oNe, too :-)";

			var result = h1.ConvertCase(StringExtension.CaseFormat.UpperFirstLetter);
			Assert.True(result.Equals("ThIS iS a VERY cRAZY STring!"));

			result = h1.ConvertCase(StringExtension.CaseFormat.UpperAllFirstLetters);
			Assert.True(result.Equals("ThIS IS A VERY CRAZY STring!"));

			result = h2.ConvertCase(StringExtension.CaseFormat.LowerFirstLetter);
			Assert.True(result.Equals("anD a CRAZY oNe, too :-)"));

			result = h2.ConvertCase(StringExtension.CaseFormat.LowerAllFirstLetters);
			Assert.True(result.Equals("anD a cRAZY oNe, too :-)"));

			result = h1.ConvertCase(StringExtension.CaseFormat.CamelCase);
			Assert.True(result.Equals("This Is A Very Crazy String!"));

			result = h1.ConvertCase(StringExtension.CaseFormat.InvertCamelCase);
			Assert.True(result.Equals("tHIS iS a vERY cRAZY sTRING!"));

			result = h1.ConvertCase(StringExtension.CaseFormat.None);
			Assert.True(result.Equals("thIS iS a VERY cRAZY STring!"));

			result = h2.ConvertCase(StringExtension.CaseFormat.LowerAllFirstLetters, new List<string>() { "and" });
			Assert.True(result.Equals("AnD a cRAZY oNe, too :-)"));

		}

		[Fact]
		public void TestReplaceHTMLUmlauts()
		{
			var h = "äöüÄÖÜß" + Environment.NewLine;
			Assert.True(h.ReplaceHTMLUmlauts(true).Equals("&auml;&ouml;&uuml;&Auml;&Ouml;&Uuml;&szlig;<br>"));
		}


		[Fact]
		public void TestAdjustLineFeedCharacters()
		{
			var h = "This is " + '\r' + "a line" + '\n' + "separated string";
			var result = h.AdjustLineFeedCharacters();
			Assert.True(result.Equals("This is " + Environment.NewLine + "a line" + Environment.NewLine + "separated string"));
		}

		[Fact]
		public void TestIsValidGuid()
		{
			var h1 = Guid.NewGuid().ToString();
			var h2 = h1.Replace('{', ' ').Replace('}', ' ').Trim();
			var h3 = "{2298BA52-16F2-GGGG-98D7-ED0DCB31752B}";
			var h4 = "{2298BA52-16F2-aaaa-98D7-ED0DCB31752B}";
			var h5 = "{2298BA52-16F-aaa-98D7-ED0DCB31752B}";

			Assert.True(h1.IsValidGuid());
			Assert.True(h2.IsValidGuid());
			Assert.False(h3.IsValidGuid());
			Assert.True(h4.IsValidGuid());
			Assert.False(h5.IsValidGuid());
		}

		[Fact]
		public void TestIsFormalValidEmail()
		{
			var h1 = "test@noanswer";
			var h2 = "test@noanswer.com";
			var h3 = "test@noanswer.UnkownTLD";
			var h4 = "test 1@noanswer.com";
			var h5 = "test-1@noanswer.com";

			Assert.False(h1.IsFormalValidEmail());
			Assert.True(h2.IsFormalValidEmail());
			Assert.True(h3.IsFormalValidEmail());
			Assert.False(h4.IsFormalValidEmail());
			Assert.True(h5.IsFormalValidEmail());
		}


		[Fact]
		public void TestSplitIntoPieces()
		{
			var h1 = "This is a very very long text" +
							 " that should be splitted into " +
							 " several pieces";

			var result = h1.SplitIntoPieces(12);

			Assert.True(result.Count == 7);

			var builder = new StringBuilder();
			foreach (var piece in result)
			{
				builder.Append(piece);
			}
			Assert.Equal(builder.ToString(), h1);
		}


		[Fact]
		public void TestIsNumeric()
		{
			var h1 = "42";
			var h2 = "4.2";
			var h3 = "a";
			var h4 = "4,2";

			Assert.True(h1.IsNumeric(false));
			Assert.True(h1.IsNumeric(true));

			Assert.False(h2.IsNumeric(false));
			Assert.True(h2.IsNumeric(true));

			Assert.False(h3.IsNumeric(false));
			Assert.False(h3.IsNumeric(true));

			Assert.False(h4.IsNumeric(false));
			Assert.True(h4.IsNumeric(true));
		}


		[Fact]
		public void TestToHexString()
		{
			const string h = "Hello World!";

			var result = h.ToHexString(false, false);
			Assert.True("48656C6C6F20576F726C6421".ToLower() == result);

			result = h.ToHexString();
			Assert.True("0x480x650x6C0x6C0x6F0x200x570x6F0x720x6C0x640x21".ToLower() == result);

			result = h.ToHexString(true, true);
			Assert.True("0x48 0x65 0x6C 0x6C 0x6F 0x20 0x57 0x6F 0x72 0x6C 0x64 0x21".ToLower() == result);

		}

		[Fact]
		public void TestCalcMd5HashCode()
		{
			const string h = "abcdefghijklmnopqrstuvwxyz";
			var result = h.CalcMd5HashCode();

			Assert.True("C3FCD3D76192E4007DFB496CCA67E13B".ToLower() == result);

			result = h.CalcMd5HashCode(true);

			Assert.True("C3FCD3D76192E4007DFB496CCA67E13B" == result);
		}


		[Fact]
		public void TestStringLeft()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";
			const string h2 = "abcdefghij";

			var result = h1.Left(10);
			Assert.Equal(result, h2);
		}


		[Fact]
		public void TestStringLeftInvalidLength()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";

			Assert.Throws<ArgumentOutOfRangeException>(() => h1.Left(1000));
		}


		[Fact]
		public void TestStringLeftNegativeLength()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";

			Assert.Throws<ArgumentOutOfRangeException>(() => h1.Left(-1000));
		}


		[Fact]
		public void TestStringRight()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";
			const string h2 = "qrstuvwxyz";

			var result = h1.Right(10);
			Assert.Equal(result, h2);
		}

		[Fact]
		public void TestStringRightInvalidLength()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";

			Assert.Throws<ArgumentOutOfRangeException>(() => h1.Right(1000));

		}


		[Fact]
		public void TestStringRightNegativeLength()
		{
			const string h1 = "abcdefghijklmnopqrstuvwxyz";

			Assert.Throws<ArgumentOutOfRangeException>(() => h1.Right(-1000));
		}

		[Fact]
		public void TestRemoveInvalidChars()
		{
			string source = "Maxine Müller^#";
			string pattern = @"[^a-zA-ZüÜäÄöÖß ]";

			var result = source.RemoveInvalidChars(pattern);
			Assert.Equal("Maxine Müller", result);

			source = "âèé";
			pattern = @"[^\w ]";
			result = source.RemoveInvalidChars(pattern);
			Assert.Equal(source, result);


			source = "âèé?!§_#   @";
			pattern = @"[^\w ]";
			result = source.RemoveInvalidChars(pattern);
			Assert.Equal("âèé_   ", result);

		}


		[Fact]
		public void TestIsFormalValidIBAN()
		{
			var source = "DE68 2105 0170 0012 3456 78";
			Assert.True(source.IsFormalValidIBAN().Item1);

			source = "DE19323412341234123412"; // invalid iban
			Assert.False(source.IsFormalValidIBAN().Item1);
		}


		[Fact]
		public void TestFormatIBAN()
		{
			var source = "DE6821050170 0012345678";
			Assert.Equal("DE68 2105 0170 0012 3456 78", source.FormatAsPrintableIBAN());

			source = "DE12123412341234123412";// invalid iban
			Assert.Equal(null, source.FormatAsPrintableIBAN());

		}


		[Fact]
		public void TestRepeat()
		{
			var source = "a";

			var result = source.Repeat(10);

			Assert.Equal(10, result.Length);
		}


		private enum TestEnum
		{
			A,
			B,
			C
		}

		[Fact]
		public void TestGetAs()
		{

			Assert.Equal(1, "1".GetAs<int>());
			Assert.Equal(1, "1".GetAs<Int32>());
			Assert.Equal(Int64.MaxValue, "9223372036854775807".GetAs<Int64>());
			Assert.Equal(true, "true".GetAs<bool>());
			Assert.Equal(1.0m, "1.0".GetAs<decimal>());
			Assert.Equal(1.0, "1.0".GetAs<double>());
			Assert.Equal(new DateTime(2016, 1, 1), "2016-01-01".GetAs<DateTime>());
			Assert.Equal(TestEnum.A, "A".GetAs<TestEnum>());
		}


		[Fact]
		public void TestGetAsWithCultureInfo()
		{
			Assert.Equal(1, "1".GetAs<int>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(1, "1".GetAs<Int32>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(Int64.MaxValue, "9223372036854775807".GetAs<Int64>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(true, "true".GetAs<bool>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(1.0m, "1,0".GetAs<decimal>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(1.0, "1,0".GetAs<double>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(new DateTime(2016, 1, 1), "01.01.2016".GetAs<DateTime>(CultureInfo.GetCultureInfo("de-DE")));
			Assert.Equal(TestEnum.A, "A".GetAs<TestEnum>(CultureInfo.GetCultureInfo("de-DE")));
		}


		[Fact]
		public void TestGetAsDefaultValueIfNullOrEmptyString()
		{
			Assert.Equal(0, ((string) null).GetAs<int>());
			Assert.Equal(0, "".GetAs<int>());
			Assert.Equal(0, " ".GetAs<int>());
		}


		[Fact]
		public void GetAsTypeException()
		{
			Assert.Throws<FormatException>(() => "3,14159265".GetAs<int>());
		}


		[Fact]
		public void TestQuoted()
		{
			var org = "Hello World";

			Assert.Equal("'Hello World'", org.Quoted());

			org += "\"";

			Assert.Equal("\"Hello World\"\"\"", org.Quoted('"'));
		}


		[Fact]
		public void TestNormalizeUmlauts()
		{
			var h = "äöüÄÖÜß";

			Assert.Equal("aeoeueAeOeUess", h.NormalizeUmlauts());
		}


		[Fact]
		public void TestDiacretics()
		{
			var h = "äöüÄÖÜßàáâ";

			Assert.Equal("aouAOUßaaa", h.RemoveDiacritics());
		}
	}
}