using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;
public class AuthorizeService : IAuthorizeService
{
    private readonly IAuthorizeRepository _repository;
    private readonly IConfiguration _configuration;
    public AuthorizeService(IAuthorizeRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }
    public async Task<string> Login(UserLogin userLogin)
    {
        var loggedUser = await _repository.Login(userLogin);
        if (loggedUser == null)
        {
            throw new NotFoundException("User not found. Wrong email");
        }

        if (!VerifyPasswordHash(userLogin.Password, loggedUser.PasswordHash!, loggedUser.PasswordSalt!))
        {
            throw new NotFoundException("User not found.Wrong password");
        }

        var claims = new[]
        {
             new Claim("id",loggedUser.Id.ToString()),
            new Claim(ClaimTypes.Email,loggedUser.Email),
            new Claim(ClaimTypes.Role,loggedUser.Role!.Name)
        };

        var token = new JwtSecurityToken
            (
            issuer: _configuration.GetSection("Jwt").GetSection("Issuer").Value,
            audience: _configuration.GetSection("Jwt").GetSection("Audience").Value,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value)),
            SecurityAlgorithms.HmacSha256)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public async Task Register(User user, string password)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;
        user.RoleId = 3;
        await _repository.Register(user);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value);
        tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        }, out SecurityToken validatedToken);

        return (JwtSecurityToken)validatedToken;
    }

    public async Task<User> Get(int id)
    {
        var user = await _repository.Get(id);
        if (user == null)
        {
            throw new Exception("not found");
        }
        return user;
    }
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}

