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

    public async Task<string> VerifyCredentials(LoginUserDto credentials)
    {
        var foundUser = await _userRepo.GetOneByEmail(credentials.Email) ?? throw new Exception("Email not found");
        var isAuthenticated = PasswordService.VerifyPassword(credentials.Password, foundUser.Password, foundUser.Salt);
        if (!isAuthenticated) throw new Exception("Password is incorrect");
        return GenerateToken(foundUser);
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("integrify-assignment-secret-key-1234567890"));
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