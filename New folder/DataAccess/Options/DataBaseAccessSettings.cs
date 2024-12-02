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
        public const string SectionName = "DataBaseAccess";

        [Required(ErrorMessage = "ConnectionString is required.")]
        public string? ConnectionString { get; set; }
    }
}
