using ReactiveSharp.Primitive;

namespace ReactiveSharp;

public class LazyRef<T>(Func<T> factory) : MonoValueHost<T?>(null), IReadOnlyRef<T> where T : class
{
    private readonly Func<T> factory = factory;

    public T Value
    {
        get
        {
            if (currentValue is null)
            {
                Interlocked.CompareExchange(ref currentValue, factory(), null);
                OnValueChanged(this, new(currentValue, null));
            }
            return currentValue;
        }
    }
}
