using FPLSP_Token.Consts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FPLSP_Token;

public static class TokenDecoding
{
    /// <summary>
    ///     Giải mã token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static ClaimsPrincipal DecodeToken(string token)
    {
        var secretKey = ConstsToken.SecretKey;
        var key = Encoding.ASCII.GetBytes(secretKey ?? "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        return principal;
    }
}