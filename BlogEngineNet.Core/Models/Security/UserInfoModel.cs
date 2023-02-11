using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;

namespace BlogEngineNet.Core.Models.Security
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public UserInfoModel(User user)
        {
            Id = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role;
            Username = user.Username;
        }
    }
}