using System.Net;

namespace holonsoft.Utils.Extensions;
public static class IPAddressExtension
{
  public static bool IsIPv4Multicast(this IPAddress self)
  {
    var addrbytes = self.GetAddressBytes();

    if (addrbytes.Length > 4)
    {
      return false;
    }

    var b = (int)addrbytes[0];

    return ((b >= 224) && (b <= 239));
  }


  public static bool IsEitherV4OrV6Multicast(this IPAddress self) => self.IsIPv6Multicast || self.IsIPv4Multicast();

  public static bool IsV4Address(this IPAddress self) => self.GetAddressBytes().Length == 4;
}
