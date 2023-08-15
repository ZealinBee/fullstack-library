using IntegrifyLibrary.Domain;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IntegrifyLibrary.Business;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;

    public AuthService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public string VerifyCredentials(LoginUserDto credentials)
    {
        var foundUser = _userRepo.GetOneByEmail(credentials.Email);
        return GenerateToken(foundUser);
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-key"));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "integrify-assignment",
            Expires = DateTime.Now.AddMinutes(10),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

}