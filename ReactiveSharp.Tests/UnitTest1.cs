using Newtonsoft.Json.Linq;
using ReactiveSharp.Primitive;

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
    public void TestBasic()
    {
        var obj = new VO { Name = "test", Number = 1 };

        // ��Ӧʽ��д���԰�װ��
        var wrapper = RefHelpers.MemberToRef(obj, o => o.Number);

        // ��������
        var doubled = new Computed<int, int>(() => wrapper.Value * 2, wrapper);

        // Ƕ�׼�������
        var hexString = new Computed<string, int>(() => doubled.Value.ToString("X2"), doubled);

        // ����Ƕ�׼������ԣ�ʹ�ò�ָ���������͵İ汾
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
    
    [Test]
    public void TestObservable()
    {
        var obj = new VO { Name = "Observable", Number = 1 };

        var wrapper = RefHelpers.MemberToRef(obj, o => o.Name);
        var computed = new Computed<string, string>(() => "The value is changed to " + wrapper.Value, wrapper);

        var called = 0;
        computed.Subscribe(value =>
        {
            called++;
        });

        wrapper.Value = "newValue";
        Assert.That(called, Is.EqualTo(1));
        Assert.That(computed.Value, Is.EqualTo("The value is changed to newValue"));

        // Dispose��Ӧ�ô�������
        computed.Dispose();
        wrapper.Value = "disposed";
        Assert.That(called, Is.EqualTo(1));
        Assert.That(computed.Value, Is.EqualTo("The value is changed to newValue"));

    }


}
