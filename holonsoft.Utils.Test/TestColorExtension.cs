// unset

using holonsoft.Utils.Extensions;
using System;
using System.Drawing;
using Xunit;

namespace holonsoft.Utils.Test
{
	public class TestColorExtension
	{
		[Fact]
		public void TestColorExt()
		{
			var h = Color.Red.ToStringWithDelimiter(',');

			var c = ColorExtension.FromStringWithDelimiters(h, ',');

			Assert.Equal(Color.Red.ToArgb(), c.ToArgb());
		}


		[Fact]
		public void TestColorBrightnessChange01()
		{
			Assert.Throws<ArgumentException>(() => Color.Red.ChangeBrightness(-3));
		}


		[Fact]
		public void TestColorBrightnessChange02()
		{
			var newColor = Color.Red.ChangeBrightness(-0.3f);

			Assert.Equal(Color.FromArgb(255, 178, 0, 0), newColor);

		}



		[Fact]
		public void TestColorBrightnessChange03()
		{
			var newColor = Color.White.ChangeBrightness(-1f);
			Assert.Equal(Color.Black.ToArgb(), newColor.ToArgb());

			newColor = Color.White.ChangeBrightness(1f);
			Assert.Equal(Color.White.ToArgb(), newColor.ToArgb());

			newColor = Color.Black.ChangeBrightness(-1f);
			Assert.Equal(Color.Black.ToArgb(), newColor.ToArgb());

			newColor = Color.Black.ChangeBrightness(1f);
			Assert.Equal(Color.White.ToArgb(), newColor.ToArgb());

			newColor = Color.Black.ChangeBrightness(0);
			Assert.Equal(Color.Black.ToArgb(), newColor.ToArgb());
		}
	}
}