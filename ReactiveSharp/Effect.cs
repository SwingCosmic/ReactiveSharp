

using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public class Effect(Action stop) : IDisposable
{
    private bool disposedValue;


    #region 同步

    public static IDisposable Run<T>(IReadOnlyRef<T> dep, Action action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep.ValueChanged += handler;

        void Stop()
        {
            dep.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable Run<T1, T2>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, Action action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable Run<T1, T2, T3>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, IReadOnlyRef<T3> dep3, Action action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;
        dep3.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
            dep3.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable Run<T1, T2, T3, T4>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, IReadOnlyRef<T3> dep3, IReadOnlyRef<T4> dep4, Action action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;
        dep3.ValueChanged += handler;
        dep4.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
            dep3.ValueChanged -= handler;
            dep4.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable Run(IEnumerable<IReadOnlyRef<object>> deps, Action action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        foreach (var dep in deps)
        {
            dep.ValueChanged += handler;
        }

        void Stop()
        {
            foreach (var dep in deps)
            {
                dep.ValueChanged -= handler;
            }
        }

        return new Effect(Stop);
    }
    #endregion

    #region 异步


    public static IDisposable RunAsync<T>(IReadOnlyRef<T> dep, Func<Task> action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep.ValueChanged += handler;

        void Stop()
        {
            dep.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable RunAsync<T1, T2>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, Func<Task> action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable RunAsync<T1, T2, T3>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, IReadOnlyRef<T3> dep3, Func<Task> action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;
        dep3.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
            dep3.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable RunAsync<T1, T2, T3, T4>(IReadOnlyRef<T1> dep1, IReadOnlyRef<T2> dep2, IReadOnlyRef<T3> dep3, IReadOnlyRef<T4> dep4, Func<Task> action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        dep1.ValueChanged += handler;
        dep2.ValueChanged += handler;
        dep3.ValueChanged += handler;
        dep4.ValueChanged += handler;

        void Stop()
        {
            dep1.ValueChanged -= handler;
            dep2.ValueChanged -= handler;
            dep3.ValueChanged -= handler;
            dep4.ValueChanged -= handler;
        }

        return new Effect(Stop);
    }

    public static IDisposable RunAsync(IEnumerable<IReadOnlyRef<object>> deps, Func<Task> action)
    {
        EventHandler<ValueChangedEventArgs> handler = (_, _) => action();
        foreach (var dep in deps)
        {
            dep.ValueChanged += handler;
        }

        void Stop()
        {
            foreach (var dep in deps)
            {
                dep.ValueChanged -= handler;
            }
        }

        return new Effect(Stop);
    }

    #endregion


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            stop();
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~Effect()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }


}
