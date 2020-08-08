using DevPlatform.Core.Security.JwtSecurity;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevPlatform.Core.Security
{
    public class TokenService : ITokenService
    {
        public TokenUserResponse GenerateToken(AppUserDto appUserDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUserDto.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUserDto.AppUserId.ToString()),
            };

            string accessToken = GenerateAccessToken(claims);
            string refreshToken = GenerateRefreshToken();
            var token = new TokenUserResponse
            {
                UserId = appUserDto.AppUserId,
                UserName = appUserDto.UserName,
                CoverPhotoUrl = appUserDto.CoverPhotoUrl,
                ProfilePhotoUrl = appUserDto.ProfilePhotoUrl,
                RegisteredDate = appUserDto.RegisteredDate,
                UserPosts = appUserDto.UserPosts,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expires = JwtTokenDefinitions.TokenExpirationTime.Ticks
            };

            return token;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var handler = new JwtSecurityTokenHandler();
            var securityTokenHanlder = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject = claimsIdentity,
                Expires = DateTime.Now.Add(JwtTokenDefinitions.TokenExpirationTime),
                NotBefore = DateTime.Now
            });
            string accessToken = handler.WriteToken(securityTokenHanlder);

            return accessToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
