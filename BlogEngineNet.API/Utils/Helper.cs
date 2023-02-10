using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogEngineNet.API.Utils;

public  class Helper
{
    //public static string GenerateToken(UserInfoModel user, IOptions<AppSettings> appSettings)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
    //        Expires = DateTime.UtcNow.AddDays(7),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return tokenHandler.WriteToken(token);
    //}

    public static string GenerateToken(UserInfoModel user, IOptions<AppSettings> appSettings)
    {
        var issuer = appSettings.Value.Jwt.Issuer;
        var audience = appSettings.Value.Jwt.Audience;
        var key = Encoding.ASCII.GetBytes(appSettings.Value.Jwt.Key);

        SigningCredentials credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha512Signature
            );
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
             }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor); 
        return tokenHandler.WriteToken(token);
    }
}