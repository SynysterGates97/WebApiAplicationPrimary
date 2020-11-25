using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAplication.Models;

namespace WebApiAplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        public AppDataBaseContext _ctx;

        public WeatherForecastController(AppDataBaseContext ctx)
        {
            _ctx = ctx;
        }


        private readonly ILogger<WeatherForecastController> _logger;

        [HttpPost]
        public string Post([FromBody] string value)
        {
            Dweller newUser = new Dweller();
            newUser.HP = 100;
            newUser.Name = "Одинокий путник";

            _ctx.Dwellers.Add(newUser);
            _ctx.SaveChanges();
            return "Не унывайте, пацаны";
        }

        [HttpGet]
        public string Get()
        {
            Dweller newUser = new Dweller();
            //newUser.HP = 100;
            newUser.Name = "Одинокий путник";

            _ctx.Dwellers.Add(newUser);
            _ctx.SaveChanges();
            return "Не унывайте, пацаны";
        }

        [HttpGet("{ip}")]
        public string Get(int ip)
        {
            
            return "Hasssa" + ip;
        }
    }
}
