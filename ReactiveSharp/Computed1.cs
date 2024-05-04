using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public class Computed<T, D> : ComputedBase<T>
{

    private readonly IReadOnlyRef<D> dep;

    public Computed(Getter<T> getter, IReadOnlyRef<D> dep): base(getter)
    {
        this.dep = dep;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep.ValueChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep.ValueChanged -= Watch;
    }
}


public class WritableComputed<T, D> : WritableComputedBase<T>
{

    private readonly IReadOnlyRef<D> dep;

    public WritableComputed(Getter<T> getter, Setter<T> setter, IReadOnlyRef<D> dep) : base(getter, setter)
    {
        this.dep = dep;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep.ValueChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep.ValueChanged -= Watch;
    }
}