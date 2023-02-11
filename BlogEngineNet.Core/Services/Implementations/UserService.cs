using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context; 
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

    public Result<User> GetById(int id)
    { 
        var result = new Result<User>();
        try
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == id);
            result.Value = user;
            if (user is null)
            {
                result.Message = "User record not found";
                result.ResultType = ResultType.INFO;
            }
            else
            {
                result.Value = user;
                result.Message = "Record obtained successfully";
                result.ResultType = ResultType.SUCCESS;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            // the exception should be register in log
        }
        return result;
    } 
}