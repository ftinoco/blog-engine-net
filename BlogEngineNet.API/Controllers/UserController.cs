using BlogEngineNet.API.Utils;
using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Models.Security;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlogEngineNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    { 
        private readonly IUserService _userService;
        private readonly IOptions<AppSettings> _appSettings; 

        public UserController(
            IUserService userService, 
            IOptions<AppSettings> appSettings )
        { 
            _userService = userService;
            _appSettings = appSettings;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(LoginModel model)
        {
            var result = _userService.Authenticate(model);

            if (result.ResultType == ResultType.ERROR)
                return BadRequest(result.Message);
            result.Value.Token = Helper.GenerateToken(result.Value, _appSettings);
            return Ok(result.Value);
        } 
    }
}