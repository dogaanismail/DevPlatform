﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DevPlatform.Core.Security.JwtSecurity
{
    public static class JwtConfigure
    {
        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = JwtTokenDefinitions.IssuerSigningKey,
                        ValidAudience = JwtTokenDefinitions.Audience,
                        ValidIssuer = JwtTokenDefinitions.Issuer,
                        ValidateIssuerSigningKey = JwtTokenDefinitions.ValidateIssuerSigningKey,
                        ValidateLifetime = JwtTokenDefinitions.ValidateLifetime,
                        ClockSkew = JwtTokenDefinitions.ClockSkew
                    };
                });
        }
    }
}
