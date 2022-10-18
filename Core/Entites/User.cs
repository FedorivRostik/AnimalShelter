namespace Core.Entities;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
