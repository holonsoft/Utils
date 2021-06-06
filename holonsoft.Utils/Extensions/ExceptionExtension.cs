using System;
using System.Text;

namespace holonsoft.Utils.Extensions
{
	public static class ExceptionExtension
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
