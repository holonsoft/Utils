using holonsoft.Utils.Extensions;
using System.Net;
using Xunit;

namespace holonsoft.Utils.Test
{
	public class TestIPAddressExtension
	{
		[Fact]
		public void TestIPAddresses()
		{
			var ip = "127.0.0.1";
			Assert.Equal(false, IPAddress.Parse(ip).IsIPv6Multicast);
			Assert.Equal(false, IPAddress.Parse(ip).IsIPv4Multicast());
			Assert.Equal(false, IPAddress.Parse(ip).IsEitherV4OrV6Multicast());

			ip = "234.0.0.1";
			Assert.Equal(false, IPAddress.Parse(ip).IsIPv6Multicast);
			Assert.Equal(true, IPAddress.Parse(ip).IsIPv4Multicast());
			Assert.Equal(true, IPAddress.Parse(ip).IsEitherV4OrV6Multicast());

			ip = "FF01:0:0:0:0:0:0:2";
			Assert.Equal(true, IPAddress.Parse(ip).IsIPv6Multicast);
			Assert.Equal(false, IPAddress.Parse(ip).IsIPv4Multicast());
			Assert.Equal(true, IPAddress.Parse(ip).IsEitherV4OrV6Multicast());

		}
	}
}