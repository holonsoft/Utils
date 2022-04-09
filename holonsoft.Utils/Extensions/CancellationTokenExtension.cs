namespace holonsoft.Utils.Extensions;

public static class CancellationTokenExtension
{
  public static Task AsAwaitable(this CancellationToken cancellationToken)
  {
    var completionSource = new TaskCompletionSource<bool>();
    cancellationToken.Register(s => ((TaskCompletionSource<bool>) s).SetResult(true), completionSource);
    return completionSource.Task;
  }
}
