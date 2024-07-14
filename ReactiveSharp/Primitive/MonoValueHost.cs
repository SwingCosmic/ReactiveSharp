using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveSharp.Primitive;

public abstract class MonoValueHost<T> : 
    IValueChangeSource, 
    INotifyPropertyChanged, 
    IEquatable<MonoValueHost<T>>
{

    protected T currentValue;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    public MonoValueHost(T initialValue) 
    { 
        currentValue = initialValue;
    }


    protected void OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        ValueChanged?.Invoke(sender, e);// 这个事件同时会触发observable
        PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
    }

    public bool Equals(MonoValueHost<T>? other)
    {
        if (other is null)
            return false;
        if (other.currentValue is null && currentValue is null)
            return true;
        else if (other.currentValue is null || currentValue is null)
            return false;
        return currentValue.Equals(other.currentValue);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MonoValueHost<T>);
    }

    public override int GetHashCode()
    {
        return currentValue?.GetHashCode() ?? 0;
    }

}
