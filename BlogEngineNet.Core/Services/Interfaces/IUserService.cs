using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces
{
    public interface IUserService
    {
        Result<User> GetById(int id);
        UserInfoModel Authenticate(LoginModel user);
    }
}