using FluentAssertions;
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
			IPAddress.Parse(ip).IsIPv6Multicast.Should().BeFalse();
			IPAddress.Parse(ip).IsIPv4Multicast().Should().BeFalse();
			IPAddress.Parse(ip).IsEitherV4OrV6Multicast().Should().BeFalse();

			ip = "234.0.0.1";
			IPAddress.Parse(ip).IsIPv6Multicast.Should().BeFalse();
			IPAddress.Parse(ip).IsIPv4Multicast().Should().BeTrue();
			IPAddress.Parse(ip).IsEitherV4OrV6Multicast().Should().BeTrue();

			ip = "FF01:0:0:0:0:0:0:2";
			IPAddress.Parse(ip).IsIPv6Multicast.Should().BeTrue();
			IPAddress.Parse(ip).IsIPv4Multicast().Should().BeFalse();
			IPAddress.Parse(ip).IsEitherV4OrV6Multicast().Should().BeTrue();
		}
	}
}