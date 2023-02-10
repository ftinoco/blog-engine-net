using BlogEngineNet.API.Utils;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
/*using BlogEngineNet.API.Utils.Filters;*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlogEngineNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IUserService _userService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IUserService userService, 
            IOptions<AppSettings> appSettings,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _userService = userService;
            _appSettings = appSettings;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginModel model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            response.Token = Helper.GenerateToken(response, _appSettings);
            return Ok(response);
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("test");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize]
        [HttpGet("GetAllWeather")]
        public IEnumerable<WeatherForecast> GetAll()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}