using System;
using System.Drawing;
using System.Text;

namespace holonsoft.Utils.Extensions
{
	public static class ColorExtension
	{
		public static Color FromStringWithDelimiters(string colorValueAsString, char delimiter)
		{
			var h = colorValueAsString.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

			if (h?.Length != 4)
			{
				throw new Exception("wrong string format, must have [alpha, red, green, blue] values delimited by a delimiter");
			}

			// no excplicit exception, if a non parsable value is given, we will get one :-)
			return Color.FromArgb(int.Parse(h[0]), int.Parse(h[1]), int.Parse(h[2]), int.Parse(h[3]));
		}


		public static string ToStringWithDelimiter(this Color self, char delimiter)
		{
			var sb = new StringBuilder();

			sb.Append(self.A);
			sb.Append(delimiter);
			sb.Append(self.R);
			sb.Append(delimiter);
			sb.Append(self.G);
			sb.Append(delimiter);
			sb.Append(self.B);

			return sb.ToString();
		}

		// based on https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
		/// <summary>
		/// Change brightbnes of a given color
		/// </summary>
		/// <param name="self">Color to be changed</param>
		/// <param name="correctionFactor">-1.0f to 1.0f, percentage of brightness change, values lower zero make color darker, values over zero lighter</param>
		/// <returns>new color</returns>
		public static Color ChangeBrightness(this Color self, float correctionFactor)
		{
			if (Math.Abs(correctionFactor) > 1.000)
			{
				throw new ArgumentException("CorrectionFactor must be between -1 and 1");
			}

			var red = (float) self.R;
			var green = (float) self.G;
			var blue = (float) self.B;

			if (correctionFactor < 0)
			{
				correctionFactor = 1 + correctionFactor;
				red *= correctionFactor;
				green *= correctionFactor;
				blue *= correctionFactor;
			}
			else
			{
				red = (255 - red) * correctionFactor + red;
				green = (255 - green) * correctionFactor + green;
				blue = (255 - blue) * correctionFactor + blue;
			}

			return Color.FromArgb(self.A, (int) red, (int) green, (int) blue);
		}
	}
}