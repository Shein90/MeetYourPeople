namespace Common.Configuration;

/// <summary>
/// Provides extensions for specific configuration settings. 
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Adds options after validation.
    /// </summary>
    public static void AddOptionWithValidation<TOption>(this IServiceCollection serviceCollection,
        IConfiguration configuration) where TOption : class =>
            serviceCollection.AddOptions<TOption>().Bind(configuration).ValidateDataAnnotations();
}