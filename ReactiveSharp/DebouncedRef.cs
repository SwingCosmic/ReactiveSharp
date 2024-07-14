using ReactiveSharp.Primitive;

namespace ReactiveSharp;

public class DebouncedRef<T> : Ref<T>
{
    public DebouncedRef(T value, TimeSpan interval) : base(value)
    {
        setter = FunctionalHelpers.Debounce((T value) =>
        {
            if (ValueHelpers.AreEqual(currentValue, value)) return;

            var oldValue = currentValue;
            currentValue = value;
            OnValueChanged(this, new(value, oldValue));
        }, interval);
    }

    private readonly Action<T> setter;

    public override T Value
    {
        get => currentValue;
        set => setter(value);
    }
}