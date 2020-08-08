using DevPlatform.Domain.Api;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;
using System.Security.Claims;

namespace DevPlatform.Core.Security
{
    public interface ITokenService
    {
        TokenUserResponse GenerateToken(AppUserDto appUserDto);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
