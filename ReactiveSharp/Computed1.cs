﻿using System.ComponentModel;

namespace ReactiveSharp;

public class Computed<T, D> : IReadOnlyRef<T>, IDisposable
{
    private readonly Func<T> getter;
    private readonly IReadOnlyRef<D> dep;

    public Computed(Func<T> getter, IReadOnlyRef<D> dep)
    {
        this.getter = getter;
        this.dep = dep;
        _value = getter();

        dep.PropertyChanged += Watch;
    }

    protected T _value;
    private bool disposedValue;

    public T Value => _value;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(sender, e);
    }

    public void Watch(object? sender, PropertyChangedEventArgs e)
    {
        var value = getter();
        if (ValueHelpers.AreEqual(_value, value)) return;
        _value = value;
        PropertyChanged?.Invoke(sender, e);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null

            dep.PropertyChanged -= Watch;

            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~Computed()
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


public class WritableComputed<T, D> : Computed<T, D>, IRef<T>
{

    private readonly Action<T> setter;

    public WritableComputed(Func<T> getter, Action<T> setter, IReadOnlyRef<D> dep) : base(getter, dep)
    {
        this.setter = setter;
    }

    public new T Value
    {
        get => _value;
        set
        {
            if (ValueHelpers.AreEqual(_value, value)) return;

            _value = value;
            setter(value);
            OnPropertyChanged(this, new(nameof(Value)));
        }
    }
}