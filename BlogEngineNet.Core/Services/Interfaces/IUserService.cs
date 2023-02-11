using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces
{
    public interface IUserService
    {
        Result<User> GetById(int id);
        Result<UserInfoModel> Authenticate(LoginModel user);
    }
}