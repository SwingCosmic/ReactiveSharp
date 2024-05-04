using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public class Ref<T>(T value) : MonoValueHost<T>(value), IRef<T>, IReadOnlyRef<T>
{

    public virtual T Value
    {
        get => currentValue;
        set
        {
            if (ValueHelpers.AreEqual(currentValue, value)) return;

            var oldValue = currentValue;
            currentValue = value;
            OnValueChanged(this, new(value, oldValue));
        }
    }


}
