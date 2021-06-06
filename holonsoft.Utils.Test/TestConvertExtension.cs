// unset

using holonsoft.Utils.Extensions;
using System;
using System.Globalization;
using Xunit;

namespace holonsoft.Utils.Test
{
	public class TestConvertExtension
	{
		public enum MyDummyEnum
		{
			A,
			B,
			C
		}



		[Fact]
		public void TestConversion()
		{
			dynamic a = "123";
			dynamic result = ConvertExtension.GetValue<int>(typeof(int).ToString(), a, CultureInfo.InvariantCulture);
			Assert.Equal(123, result);

			a = "123.45";
			result = ConvertExtension.GetValue<float>(typeof(float).ToString(), a, CultureInfo.InvariantCulture);
			Assert.Equal(123.45f, result);

			result = ConvertExtension.GetValue<float>(typeof(float), a, CultureInfo.InvariantCulture);
			Assert.Equal(123.45f, result);

			result = ConvertExtension.GetValue<double>(typeof(double).ToString(), a, CultureInfo.InvariantCulture);
			Assert.Equal(123.45, result);

			a = "06.06.2019";
			result = ConvertExtension.GetValue<DateTime>(typeof(DateTime).ToString(), a, CultureInfo.InvariantCulture);
			Assert.Equal(new DateTime(2019, 6, 6).Date, result.Date);

			var b = "A";
			var result2 = ConvertExtension.GetValue<MyDummyEnum>(typeof(MyDummyEnum).ToString(), b, CultureInfo.InvariantCulture);
			Assert.Equal(MyDummyEnum.A, result2);


			a = "123,45";
			result = ConvertExtension.GetValue<float>(typeof(float).ToString(), a, new CultureInfo("de-DE"));
			Assert.Equal(123.45f, result);
		}


		[Fact]
		public void TestConversionFail()
		{
			dynamic a = "123.45";
			Assert.Throws<FormatException>(() => ConvertExtension.GetValue<int>(typeof(int).ToString(), a, CultureInfo.InvariantCulture));

			a = "123.45";
			Assert.Throws<ArgumentException>(() => ConvertExtension.GetValue<MyDummyEnum>(typeof(MyDummyEnum).ToString(), a, CultureInfo.InvariantCulture));

		}
	}
}