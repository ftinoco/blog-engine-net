using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations;

public class UserService : IUserService
{
    public readonly IApplicationDbContext _context; 
    public UserService(IApplicationDbContext context)
    {
        _context = context;
    }

    public UserInfoModel Authenticate(LoginModel model)
    {
        User user = _context.Users.SingleOrDefault(u =>
                                u.Username == model.Username &&
                                u.Password == model.Password);
        if (user == null) return null;
         
        return new UserInfoModel(user);
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == id);
    } 
}