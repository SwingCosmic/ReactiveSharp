using ReactiveSharp.Primitive;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReactiveSharp;

public class Computed<T> : ComputedBase<T>
{
    private IReadOnlyRef<object>[] deps;

    public Computed(Getter<T> getter, params IReadOnlyRef<object>[] deps): base(getter)
    {
        this.deps = deps;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        foreach (var dep in deps)
        {
            dep.PropertyChanged += Watch;
        }
    }

    protected override void RemoveDependencies()
    {
        foreach (var dep in deps)
        {
            dep.PropertyChanged -= Watch;
        }
    }


}


public class WritableComputed<T> : WritableComputedBase<T>
{

    private IReadOnlyRef<object>[] deps;

    public WritableComputed(Getter<T> getter, Setter<T> setter, params IReadOnlyRef<object>[] deps) : base(getter, setter)
    {
        this.deps = deps;
        AddDependencies();
    }

    protected override void AddDependencies()
    {
        foreach (var dep in deps)
        {
            dep.PropertyChanged += Watch;
        }
    }

    protected override void RemoveDependencies()
    {
        foreach (var dep in deps)
        {
            dep.PropertyChanged -= Watch;
        }
    }
}