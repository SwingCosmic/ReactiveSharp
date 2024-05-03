using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public class Ref<T>(T value) : MonoValueHost<T>(value), IRef<T>, IReadOnlyRef<T>
{

    public T Value
    {
        get => currentValue;
        set
        {
            if (ValueHelpers.AreEqual(currentValue, value)) return;

            currentValue = value;
            OnPropertyChanged(this, new(nameof(Value)));
        }
    }


}
