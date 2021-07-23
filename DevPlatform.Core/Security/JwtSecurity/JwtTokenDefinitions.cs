using DevPlatform.Core.Configuration.Configs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DevPlatform.Core.Security.JwtSecurity
{
    public class JwtTokenDefinitions
    {
        #region Methods
        public static void LoadFromConfiguration(JwtConfig jwtConfig)
        {
            Audience = jwtConfig.Audience;
            Issuer = jwtConfig.Issuer;
            TokenExpirationTime = TimeSpan.FromMinutes(jwtConfig.TokenExpirationTime);
            ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey;
            ValidateLifetime = jwtConfig.ValidateLifetime;
            ClockSkew = TimeSpan.FromMinutes(jwtConfig.ClockSkew);
            Key = jwtConfig.Key;
        }

        #endregion

        #region Properties

        public static string Key;
        public static SecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        public static SigningCredentials SigningCredentials => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        public static TimeSpan TokenExpirationTime;
        public static TimeSpan RefreshTokenExpirationTime { get; set; } = TimeSpan.FromDays(1);
        public static TimeSpan ClockSkew;
        public static string Issuer;
        public static string Audience;
        public static bool ValidateIssuerSigningKey;
        public static bool ValidateLifetime;
        #endregion
    }
}
