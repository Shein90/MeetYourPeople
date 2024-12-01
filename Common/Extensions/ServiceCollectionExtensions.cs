
using Common.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Common.Extensions;

/// <summary>
/// Provides different extension for service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    ///// <summary>
    ///// Adds jwt bearer authentication to service collection.
    ///// </summary>
    //public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection serviceCollection, JwtAuthenticationSettings jwtAuthSettings)
    //{
    //    serviceCollection
    //        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
    //        {
    //            option.TokenValidationParameters = jwtAuthSettings?.GetDefaultTokenValidationParameters()!;
    //            option.Events = new JwtBearerEvents
    //            {
    //                OnTokenValidated = context =>
    //                {
    //                    var claim = context.Principal?.FindFirst(ClaimsService.BillingIdType);
    //                    if (claim == null || claim.Value.IsNullOrWhiteSpace())
    //                    {
    //                        context.Fail("It's old token");
    //                    }
    //                    return Task.CompletedTask;
    //                },
    //                OnMessageReceived = context =>
    //                {
    //                    try
    //                    {
    //                        var jwtToken = GzippedJwtUtils.DecodeFromRequest(context.Request);
    //                        context.Token = jwtToken;
    //                    }
    //                    catch (Exception exception)
    //                    {
    //                        context.Fail(exception);
    //                        context.Token = null;
    //                    }

    //                    return Task.CompletedTask;
    //                }
    //            };
    //        });

    //    return serviceCollection;
    //}

    ///// <summary>
    ///// Adds all necessary components for supervisor authorization.
    ///// </summary>
    //public static IServiceCollection AddSupervisorAuthorization(this IServiceCollection serviceCollection)
    //{
    //    var provider = serviceCollection.BuildServiceProvider();
    //    var settings = provider.GetService<IOptions<JwtAuthenticationSettings>>();

    //    serviceCollection
    //     .AddSingleton<IAuthorizationHandler, SupervisorStatusHendler>()
    //     .AddAuthorization(options =>
    //     {
    //         options.AddPolicy(AuthPolicyNames.SupervisorPolicy,
    //            builder => builder.Requirements.Add(new SupervisorStatusRequirement()));
    //     })
    //     .AddMightyCallClaims()
    //     .AddAllowAllCors()
    //     .AddJwtBearerAuthentication(settings!.Value);

    //    return serviceCollection;
    //}
}