using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveSharp.Primitive;

public abstract class MonoValueHost<T> : INotifyPropertyChanged, IEquatable<MonoValueHost<T>>
{

    protected T currentValue;

    public event PropertyChangedEventHandler? PropertyChanged;

    public MonoValueHost(T initialValue) 
    { 
        currentValue = initialValue;
    }


    protected void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(sender, e);
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
