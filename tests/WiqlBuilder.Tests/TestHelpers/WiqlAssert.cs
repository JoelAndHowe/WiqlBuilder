using FluentAssertions;
using WiqlBuilder.Builders;

namespace WiqlBuilder.Tests.TestHelpers;

internal static class WiqlAssert
{
    public static void RendersTo(WiqlQueryBuilder builder, string expected)
    {
        var actual = Normalize(builder.Build());
        var normalizedExpected = Normalize(expected);

        actual.Should().Be(normalizedExpected);
    }

    private static string Normalize(string value)
        => value.Replace("\r\n", "\n").Trim();
}