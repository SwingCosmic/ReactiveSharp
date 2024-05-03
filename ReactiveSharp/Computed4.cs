using ReactiveSharp.Primitive;
using System.ComponentModel;

namespace ReactiveSharp;

public class Computed<T, D1, D2, D3, D4> : ComputedBase<T>
{

    private readonly IReadOnlyRef<D1> dep1;
    private readonly IReadOnlyRef<D2> dep2;
    private readonly IReadOnlyRef<D3> dep3;
    private readonly IReadOnlyRef<D4> dep4;

    public Computed(Getter<T> getter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2, IReadOnlyRef<D3> dep3, IReadOnlyRef<D4> dep4) : base(getter)
    {
        this.dep1 = dep1;
        this.dep2 = dep2;
        this.dep3 = dep3;
        this.dep4 = dep4;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep1.PropertyChanged += Watch;
        dep2.PropertyChanged += Watch;
        dep3.PropertyChanged += Watch;
        dep4.PropertyChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep1.PropertyChanged -= Watch;
        dep2.PropertyChanged -= Watch;
        dep3.PropertyChanged -= Watch;
        dep4.PropertyChanged -= Watch;
    }

}



public class WritableComputed<T, D1, D2, D3, D4> : WritableComputedBase<T>
{

    private readonly IReadOnlyRef<D1> dep1;
    private readonly IReadOnlyRef<D2> dep2;
    private readonly IReadOnlyRef<D3> dep3;
    private readonly IReadOnlyRef<D4> dep4;

    public WritableComputed(Getter<T> getter, Setter<T> setter, IReadOnlyRef<D1> dep1, IReadOnlyRef<D2> dep2, IReadOnlyRef<D3> dep3, IReadOnlyRef<D4> dep4) : base(getter, setter)
    {
        this.dep1 = dep1;
        this.dep2 = dep2;
        this.dep3 = dep3;
        this.dep4 = dep4;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        dep1.PropertyChanged += Watch;
        dep2.PropertyChanged += Watch;
        dep3.PropertyChanged += Watch;
        dep4.PropertyChanged += Watch;
    }

    protected override void RemoveDependencies()
    {
        dep1.PropertyChanged -= Watch;
        dep2.PropertyChanged -= Watch;
        dep3.PropertyChanged -= Watch;
        dep4.PropertyChanged -= Watch;
    }

}