using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Models.Security;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context; 
    public UserService(IApplicationDbContext context)
    {
        _context = context;
    }

    public Result<UserInfoModel> Authenticate(LoginModel model)
    {
        Result<UserInfoModel> result = new();
        try
        {
            User user = _context.Users.SingleOrDefault(u =>
                                    u.Username == model.Username &&
                                    u.Password == model.Password);
            if (user is null)
            {
                result.Message = "Username or password is incorrect";
                result.ResultType = ResultType.ERROR;
            }
            else
            {
                result.Value = new UserInfoModel(user); 
                result.Message = "Logged in";
                result.ResultType = ResultType.SUCCESS;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
        }

        return result;
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