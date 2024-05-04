# ReactiveSharp

像前端一样在.NET中简单地使用响应式数据。

* 实现`IObservable<T>`，因此可以与`Rx.NET`配合使用
* 实现了`INotifyPropertyChanged`等接口，可以与`WPF`等传统数据绑定一起工作

## 重要的类型

* IReadOnlyRef&lt;T&gt;
表示一个只读的响应式值的引用

* IRef&lt;T&gt;
表示一个可写入的响应式值的引用

以上类型可以通过创建响应式对象的实例，或者通过`RefHelpers`的工具方法生成

* RefHelpers
提供了大量工具方法用于封装现有对象为响应式对象，例如将类的一个属性封装为独立的`IRef<T>`，使其可以用于创建计算属性


## 响应式对象

### Ref&lt;T&gt;

可读写的响应式对象引用，实现了`IRef<T>`

相当于`vue`的`ShallowRef`，或者`@preact/signals`的`Signal`，对所引用的对象具有浅层响应式

### Computed&lt;T&gt;

只读的计算属性引用，仅当依赖变化时才会重新计算，实现了`IReadOnlyRef<T>`。
计算属性可以依赖任何实现了`IReadOnlyRef<T>`接口的类型（包括其他计算属性）

相当于`vue`的`Computed`，但是语法上更接近`react`的`useMemo`

```csharp
var a = new Ref<int>(1);
var b = new Ref<int>(1);

var c = new Computed<int, int, int>(() => 
{
  return a.Value + b.Value;
}, a, b);

a.Value = 2;

Console.WriteLine(c.Value); // 3

```

### Effect
提供一些方法监听依赖的变更并执行副作用，类似`vue`的`watch`。
支持同步和异步的版本