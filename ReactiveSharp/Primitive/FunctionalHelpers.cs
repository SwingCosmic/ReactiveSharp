using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;


namespace ReactiveSharp.Primitive;

public class FunctionalHelpers
{
    public static Action Debounce(Action action, TimeSpan delay)
    {
        CancellationTokenSource? cancellationTokenSource = null;
        return () =>
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            Task.Delay(delay, cancellationTokenSource.Token)
                .ContinueWith(_ => action());
        };
    }
    public static Action<T> Debounce<T>(Action<T> action, TimeSpan delay)
    {
        CancellationTokenSource? cancellationTokenSource = null;
        return parameter =>
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            Task.Delay(delay, cancellationTokenSource.Token)
                .ContinueWith(_ => action(parameter));
        };
    }

    public static Action Throttle(Action action, TimeSpan period)
    {
        var lastExecutionTime = DateTime.MinValue;
        return () =>
        {
            var now = DateTime.Now;
            if (now - lastExecutionTime >= period)
            {
                action();
                lastExecutionTime = now;
            }
        };
    }
    public static Action<T> Throttle<T>(Action<T> action, TimeSpan period)
    {
        var lastExecutionTime = DateTime.MinValue;
        return parameter =>
        {
            var now = DateTime.Now;
            if (now - lastExecutionTime >= period)
            {
                action(parameter);
                lastExecutionTime = now;
            }
        };
    }
}