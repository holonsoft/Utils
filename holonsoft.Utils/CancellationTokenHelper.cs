using System.Threading;
using System.Threading.Tasks;

namespace holonsoft.Utils
{
	public static class CancellationTokenHelper
	{
		public static Task AsAwaitable(this CancellationToken cancellationToken)
		{
			var completionSource = new TaskCompletionSource<bool>();
			cancellationToken.Register(s => ((TaskCompletionSource<bool>) s).SetResult(true), completionSource);
			return completionSource.Task;
		}
	}
}
