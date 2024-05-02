using System.ComponentModel;

namespace ReactiveSharp;

public class Ref<T>(T value) : IRef<T>, IReadOnlyRef<T>, IEquatable<Ref<T>>
{
    protected T _value = value;

    public T Value
    {
        get => _value;
        set
        {
            if (ValueHelpers.AreEqual(_value, value)) return;

            _value = value;
            PropertyChanged?.Invoke(this, new(nameof(Value)));
        }
    }

    public Getter<T> Getter => throw new NotImplementedException();

    public Setter<T> Setter => throw new NotImplementedException();

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool Equals(Ref<T>? other)
    {
        if (other is null)
            return false;
        if (other.Value is null && Value is null)
            return true;
        else if (other.Value is null || Value is null)
            return false;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Ref<T>);
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }

}
