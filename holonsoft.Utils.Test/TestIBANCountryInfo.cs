using holonsoft.Utils.Dto;
using Xunit;

namespace holonsoft.Utils.Test
{
	public class TestIBANCountryInfo
	{
		[Fact]
		public void TestStaticDataOk()
		{
			Assert.Equal(1400 + 16, IBANCountryInfo.IBANCountryList["EG"].CountryCodeChecksumValue);
			Assert.Equal(1300 + 14, IBANCountryInfo.IBANCountryList["DE"].CountryCodeChecksumValue);

			// check for copy&paste errors :-)
			foreach (var x in IBANCountryInfo.IBANCountryList)
			{
				Assert.Equal(x.Key, x.Value.CountryCode);
				Assert.True(x.Value.IBANLength < x.Value.IBANReadableFormat.Length);
			}
		}
	}
}