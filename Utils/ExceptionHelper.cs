using System;
using System.Text;

namespace holonsoft.Utils
{
	public static class ExceptionHelper
	{
		public static string FlattenMessages(this Exception exception)
		{
			StringBuilder builder = new();
			while (exception != null)
			{
				builder.AppendLine(exception.Message);
				exception = exception.InnerException;
			}
			return builder.ToString();
		}

		public static string Flatten(this Exception exception)
		{
			StringBuilder builder = new();
			while (exception != null)
			{
				builder.AppendLine(exception.ToString());
				exception = exception.InnerException;
			}
			return builder.ToString();
		}
	}
}
