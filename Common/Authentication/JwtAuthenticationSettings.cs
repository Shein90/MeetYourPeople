namespace Common.Authentication;

/// <summary>
/// Provides options for jwt validation.
/// </summary>
public sealed class JwtAuthenticationSettings
{
    /// <summary>
    /// Name of section in appsettings.json.
    /// </summary>
    public const string SectionName = "JwtAuthentication";

    /// <summary>
    /// If need validate token lifetime. 
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    /// Provides audiences set in array.
    /// </summary>
    public string[] Audiences => Audience?.Split(',') ?? Array.Empty<string>();

    /// <summary>
    /// Provides issuer.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Provides audiences.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Provides signing key.
    /// </summary>
    public string? SigningKey { get; set; }

    /// <summary>
    /// Provides encryption key.
    /// </summary>
    public string? EncryptionKey { get; set; }

    /// <summary>
    /// Provides default params for token validation.
    /// </summary>
    /// <returns></returns>
    public TokenValidationParameters GetDefaultTokenValidationParameters()
    {
        var result = new TokenValidationParameters
        {
            ValidIssuer = Issuer,
            ValidAudiences = Audiences,
            ValidateIssuer = !string.IsNullOrEmpty(Issuer),
            ValidateAudience = Audiences.Length > 0,
            ValidateLifetime = ValidateLifetime,
            CryptoProviderFactory = CryptoProviderFactory.Default,
        };

        result.CryptoProviderFactory.CacheSignatureProviders = false;

        if (!string.IsNullOrEmpty(EncryptionKey))
        {
            result.TokenDecryptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(EncryptionKey));
        }
        if (!string.IsNullOrEmpty(SigningKey))
        {
            result.IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(SigningKey));
            result.ValidateIssuerSigningKey = true;
        }

        return result;
    }
}