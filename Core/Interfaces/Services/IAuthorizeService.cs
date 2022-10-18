using Core.Entities;
using Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Interfaces.Services;
public interface IAuthorizeService
{
    Task<string> Login(UserLogin userLogin);
    Task Register(User user, string password);
    Task<User> Get(int id);
    JwtSecurityToken Verify(string jwt);
}
