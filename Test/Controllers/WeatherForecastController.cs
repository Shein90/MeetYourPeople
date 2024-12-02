using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        MypDbContext _dbContext;



        public WeatherForecastController(ILogger<WeatherForecastController> logger,
        MypDbContext mypDbContext)
        {
            _dbContext = mypDbContext;


        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task Get()
        {

            var existingUser = _dbContext.Users;
            var address = new Address
            {
                AddressText = "userDto.Address"
            };

            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();


        }
    }
}
