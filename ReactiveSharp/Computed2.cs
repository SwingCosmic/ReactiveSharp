using ReactiveSharp.Primitive;

namespace ReactiveSharp;

public class Computed<T, D1, D2> : ComputedBase<T>
{

    private readonly IReadOnlyRef<D1> dep1;
    private readonly IReadOnlyRef<D2> dep2;

    public Computed(Getter<T> getter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2) : base(getter)
    {
        this.dep1 = dep1;
        this.dep2 = dep2;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep1.ValueChanged += Watch;
        dep2.ValueChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep1.ValueChanged -= Watch;
        dep2.ValueChanged -= Watch;
    }

}


public class WritableComputed<T, D1, D2> : WritableComputedBase<T>
{

    private readonly IReadOnlyRef<D1> dep1;
    private readonly IReadOnlyRef<D2> dep2;

    public WritableComputed(Getter<T> getter, Setter<T> setter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2) : base(getter, setter)
    {
        this.dep1 = dep1;
        this.dep2 = dep2;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep1.ValueChanged += Watch;
        dep2.ValueChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep1.ValueChanged -= Watch;
        dep2.ValueChanged -= Watch;
    }

}