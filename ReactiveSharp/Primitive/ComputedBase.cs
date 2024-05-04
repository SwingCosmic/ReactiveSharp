using System.ComponentModel;
using ReactiveSharp;

namespace ReactiveSharp.Primitive;

public abstract class ComputedBase<T> : MonoValueHost<T>, IReadOnlyRef<T>, IDisposable
{
    public readonly Getter<T> Getter;

    public ComputedBase(Getter<T> getter) : base(getter())
    {
        Getter = getter;
    }

    protected virtual void AddDependencies()
    {

    }
    protected virtual void RemoveDependencies()
    {

    }

    public T Value => currentValue;

    public void Watch(object? sender, ValueChangedEventArgs e)
    {
        var value = Getter();
        if (ValueHelpers.AreEqual(currentValue, value)) return;
        currentValue = value;
        OnValueChanged(sender, e);
    }



    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器

            RemoveDependencies();
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~MonoValueHost()
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

    public IDisposable Subscribe(IObserver<T> observer)
    {
        throw new NotImplementedException();
    }
}


public abstract class WritableComputedBase<T> : MonoValueHost<T>, IRef<T>, IDisposable
{
    public readonly Getter<T> Getter;
    public readonly Setter<T> Setter;

    public WritableComputedBase(Getter<T> getter, Setter<T> setter) : base(getter())
    {
        Getter = getter;
        Setter = setter;
    }

    protected virtual void AddDependencies()
    {

    }
    protected virtual void RemoveDependencies()
    {

    }

    public T Value
    {
        get => currentValue;
        set 
        {
            if (ValueHelpers.AreEqual(currentValue, value)) return;

            var oldValue = currentValue;
            currentValue = value;
            Setter(value);
            OnValueChanged(this, new(value, oldValue));
        }
    }

    public void Watch(object? sender, ValueChangedEventArgs e)
    {
        var value = Getter();
        if (ValueHelpers.AreEqual(currentValue, value)) return;
        var oldValue = currentValue;
        currentValue = value;
        OnValueChanged(this, new(value, oldValue));
    }



    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器

            RemoveDependencies();
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~MonoValueHost()
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