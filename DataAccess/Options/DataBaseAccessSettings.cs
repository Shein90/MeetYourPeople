namespace DataAccess.Options
{
    /// <summary>
    /// Provides settings for connecting to database.
    /// </summary>
    public sealed class DataBaseAccessSettings
    {
        /// <summary>
        /// Name of section in appsettings.json.
        /// </summary>
        [Required(ErrorMessage = "ConnectionString is required.")]
        public const string SectionName = "DataBaseAccess";

        public string? ConnectionString { get; set; }
    }
}
