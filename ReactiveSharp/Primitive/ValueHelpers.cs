using System.Runtime.CompilerServices;

namespace ReactiveSharp.Primitive;

internal static class ValueHelpers
{
    public static bool AreEqual<T>(T? a, T? b)
    {
        if (a is null && b is null)
            return true;
        else if (a is null || b is null)
            return false;
        return a.Equals(b);
    }
}