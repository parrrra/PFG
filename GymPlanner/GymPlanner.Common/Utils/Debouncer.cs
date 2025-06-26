namespace GymPlanner.Common.Utils;

public class Debouncer
{
    private readonly object _lock = new object();
    private CancellationTokenSource? _cts;

    public async Task DebounceAsync(Func<Task> action, int delay = 400)
    {
        lock (_lock)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        try
        {
            await Task.Delay(delay, _cts.Token);
            await action();
        }
        catch (OperationCanceledException)
        {
        }
    }
}