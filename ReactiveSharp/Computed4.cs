using System.ComponentModel;

namespace ReactiveSharp;

public class Computed<T, D1, D2, D3, D4> : IReadOnlyRef<T>, IDisposable
{
    private readonly Func<T> getter;
    private readonly IReadOnlyRef<D1> dep1;
    private readonly IReadOnlyRef<D2> dep2;
    private readonly IReadOnlyRef<D3> dep3;
    private readonly IReadOnlyRef<D4> dep4;

    public Computed(Func<T> getter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2, IReadOnlyRef<D3> dep3, IReadOnlyRef<D4> dep4)
    {
        this.getter = getter;
        this.dep1 = dep1;
        this.dep2 = dep2;
        this.dep3 = dep3;
        this.dep4 = dep4;
        _value = getter();

        dep1.PropertyChanged += Watch;
        dep2.PropertyChanged += Watch;
        dep3.PropertyChanged += Watch;
        dep4.PropertyChanged += Watch;
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

            dep1.PropertyChanged -= Watch;
            dep2.PropertyChanged -= Watch;
            dep3.PropertyChanged -= Watch;
            dep4.PropertyChanged -= Watch;

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




public class WritableComputed<T, D1, D2, D3, D4> : Computed<T, D1, D2, D3, D4>, IRef<T>
{

    private readonly Action<T> setter;

    public WritableComputed(Func<T> getter, Action<T> setter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2, IReadOnlyRef<D3> dep3, IReadOnlyRef<D4> dep4) : base(getter, dep1, dep2, dep3, dep4)
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