using FPLSP_Token.Consts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FPLSP_Token;

public static class TokenEncoding
{
    public static string GenerateToken(ClaimsIdentity claimsIdentity)
    {
        // Cấu hình các thông tin cần thiết
        string secretKey = ConstsToken.SecretKey;
        string issuer = ConstsToken.Issuer;
        string audience = ConstsToken.Audience;
        // thời gian hết hạn
        DateTime expiration = DateTime.UtcNow.AddDays(1);
        // Tạo mã thông báo truy cập
        string token = GenerateAccessToken(claimsIdentity, secretKey, issuer, audience, expiration);
        return token;
    }
    private static string GenerateAccessToken(ClaimsIdentity claimsIdentity, string secretKey, string issuer, string audience, DateTime expiration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = expiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = issuer,
            Audience = audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

}