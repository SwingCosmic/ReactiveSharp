using System;
using System.Collections;
using System.Data.Common;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;


namespace ReactiveSharp;

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
        var subject = new Subject<Unit>();

        var throttledObservable = subject
            .Throttle(period)
            .Publish()
            .RefCount();

        throttledObservable.Subscribe(_ => action());

        return () => subject.OnNext(Unit.Default);
    }
    public static Action<T> Throttle<T>(Action<T> action, TimeSpan period)
    {
        var subject = new Subject<T>();

        var throttledObservable = subject
            .Throttle(period)
            .Publish()
            .RefCount();

        throttledObservable.Subscribe(action);

        return subject.OnNext;
    }
}