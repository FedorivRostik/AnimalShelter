using Core.Entities;
using Core.Models;

namespace Core.Interfaces.Repositories;
public interface IAuthorizeRepository
{
    Task<User> Login(UserLogin userLogin);
    Task Register(User user);
    Task<User> Get(int id);
}
