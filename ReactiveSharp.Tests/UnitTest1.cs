namespace ReactiveSharp.Tests;


public class VO
{
    public string Name { get; set; } = "";
    public int Number { get; set; }
}

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var obj = new VO { Name = "test", Number = 1 };

        // 响应式读写属性包装器
        var wrapper = RefHelpers.MemberToRef(obj, o => o.Number);

        // 计算属性
        var doubled = new Computed<int, int>(() => wrapper.Value * 2, wrapper);

        // 嵌套计算属性
        var hexString = new Computed<string, int>(() => doubled.Value.ToString("X2"), doubled);

        // 多重嵌套计算属性，使用不指定依赖类型的版本
        var ret = new Computed<string>(() => $"{doubled.Value}=>0x{hexString.Value}", doubled.AsAnyRef(), hexString);

        wrapper.Value = 5;

        Assert.Multiple(() =>
        {
            Assert.That(obj.Number, Is.EqualTo(5));
            Assert.That(doubled.Value, Is.EqualTo(10));
            Assert.That(hexString.Value, Is.EqualTo("0A"));
            Assert.That(ret.Value, Is.EqualTo("10=>0x0A"));
        });
    }


}
