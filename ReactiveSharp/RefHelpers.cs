

using ReactiveSharp.Primitive;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

namespace ReactiveSharp;

public static class RefHelpers
{
    public static IReadOnlyRef<V> MemberToComputed<T, V>(T obj, Getter<V> getter)
    {
        var selfRef = new Ref<T>(obj);
        return new Computed<V, T>(getter, selfRef);
    }

#if NET7_0_OR_GREATER
    [RequiresDynamicCode("此方法使用了Expression.Compile，无法在AOT环境下调用")]
#endif
    public static IRef<V> MemberToRef<T, V>(T obj, Expression<Func<T, V>> fieldOrProperty)
    {
        var selfRef = new Ref<T>(obj);

        var p = fieldOrProperty.Body as MemberExpression ?? throw new InvalidOperationException();
        if (p.Member.MemberType is not (MemberTypes.Property or MemberTypes.Field))
            throw new InvalidOperationException();
        

        var getter = fieldOrProperty.Compile();

        var target = Expression.Parameter(typeof(T), "target");
        var value = Expression.Parameter(typeof(V), "value");
        var setter = Expression.Lambda<Action<T, V>>(
                Expression.Assign(
                    Expression.MakeMemberAccess(
                        target,
                        p.Member
                    ),
                    value
                ),
                target,
                value
            ).Compile();
        return new WritableComputed<V, T>(() => getter(obj), v => setter(obj, v), selfRef);
    }

    public static IRef<V> MemberToRef<T, V>(T obj, Getter<V> memberGetter, Setter<V> memberSetter)
    {
        var selfRef = new Ref<T>(obj);
        return new WritableComputed<V, T>(memberGetter, memberSetter, selfRef);
    }

    public static IReadOnlyRef<object?> AsAnyRef<T>(this IReadOnlyRef<T> self)
    {
        return new Computed<object?, T>(() => self.Value, self);
    }    


    public static IObservable<IValueChangeInfo> AsObservable(this IValueChangeSource obj)
    {
        return Observable.FromEvent<EventHandler<ValueChangedEventArgs>, ValueChangedEventArgs>(
            h => (s, e) => h(e),
            h => obj.ValueChanged += h,
            h => obj.ValueChanged -= h 
        ); 
    }
}
