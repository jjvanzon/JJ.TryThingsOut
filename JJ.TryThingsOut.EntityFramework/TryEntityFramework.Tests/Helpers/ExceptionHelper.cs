using System;
using System.Linq;
using System.Text;

namespace TryEntityFramework.Tests.Helpers
{
	public static class ExceptionHelper
	{
		public static string FormatExceptionWithInnerExceptions(Exception ex, bool includeStackTrace)
		{
			var sb = new StringBuilder();
			sb.AppendLine(FormatException(ex, includeStackTrace));

			while (ex.InnerException != null)
			{
				sb.AppendLine("Inner exception:");
				sb.AppendLine(FormatException(ex.InnerException, includeStackTrace));
				ex = ex.InnerException;
			}

			return sb.ToString();
		}

		public static string FormatException(Exception ex, bool includeStackTrace)
		{
			if (ex == null) throw new ArgumentNullException(nameof(ex));

			string message = ex.Message;
			if (includeStackTrace)
			{
				message += Environment.NewLine + ex.StackTrace;
			}
			return message;
		}

        public static bool HasExceptionOrInnerExceptionsOfType<T>(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            bool any = exception.SelfAndAncestors(x => x.InnerException).OfType<T>().Any();
            return any;
        }
	}
}
