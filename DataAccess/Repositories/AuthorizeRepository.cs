using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;
public class AuthorizeRepository : IAuthorizeRepository
{
    private readonly AnimalShelterContext _context;

    public AuthorizeRepository(AnimalShelterContext context)
    {
        _context = context;
    }

    public async Task<User> Get(int id)
    {
        var user = await _context.User!.Include(x => x.Role).FirstOrDefaultAsync(u => u.Id == id);

        return user!;
    }

    public async Task<User> Login(UserLogin userLogin)
    {
        var user = await _context.User!.Include(x => x.Role).FirstOrDefaultAsync(u => u.Email.Equals(userLogin.Email.ToLower()));

        return user!;
    }

    public async Task Register(User user)
    {
        _context.Attach(user);
        await _context.SaveChangesAsync();
    }
}
