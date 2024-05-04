using ReactiveSharp.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveSharp;

public class ThrottledRef<T> : Ref<T>
{
    public ThrottledRef(T value, TimeSpan interval) : base(value)
    {
        setter = FunctionalHelpers.Throttle((T value) =>
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
