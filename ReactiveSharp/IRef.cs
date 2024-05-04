using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public interface IReadOnlyRef<out T> : IValueChangeSource
{
    /// <summary>
    /// 获取引用的值
    /// </summary>
    public T Value { get; }

}



public interface IRef<T> : IReadOnlyRef<T>
{
    /// <summary>
    /// 获取或设置引用的值
    /// </summary>
    public new T Value { get; set; }

}

public delegate T Getter<out T>();
public delegate void Setter<in T>(T value);
