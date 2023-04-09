namespace CodeExcercise.Common.Configuration
{
    public class SqlConnectionConfiguration
    {
        public static string SectionName { get; } = "SqlConfiguration";

        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}