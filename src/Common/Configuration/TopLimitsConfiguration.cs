namespace CodeExcercise.Common.Configuration;

public class TopLimitsConfiguration
{
    public static string SectionName { get; } = "Limits";

    public int DefaultTopValue { get; set; }
}