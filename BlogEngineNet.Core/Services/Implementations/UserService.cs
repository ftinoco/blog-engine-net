using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations;

public class UserService : IUserService
{
    public readonly IApplicationDbContext _context;
    private readonly List<User> _users = new()
    {
        new User { UserId = 1, FirstName = "Test", LastName = "User", Role = Domain.Role.Editor, Username = "test", Password = "test" }
    };
    public UserService(IApplicationDbContext context)
    {
        _context = context;
    }

    public UserInfoModel Authenticate(LoginModel model)
    {
        User user = _users.SingleOrDefault(u =>
                                u.Username == model.Username &&
                                u.Password == model.Password);
        if (user == null) return null;
         
        return new UserInfoModel(user);
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.UserId == id);
    } 
}