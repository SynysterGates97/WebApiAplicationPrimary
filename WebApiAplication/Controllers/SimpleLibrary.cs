using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAplication.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WebApiAplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleLibrary : ControllerBase
    {
        public AppDataBaseContext _ctx;

        public SimpleLibrary(AppDataBaseContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        public void Post2([FromBody] string name)
        {
            Dweller newUser = new Dweller();
            newUser.HP = 100;
            newUser.Name = name;

            _ctx.Dwellers.Add(newUser);
            _ctx.SaveChanges();
        }

        //[HttpPost]
        //public void Post()
        //{
        //    Dweller newUser = new Dweller();
        //    newUser.HP = 100;
        //    newUser.Name = "STD";

        //    _ctx.Dwellers.Add(newUser);
        //    _ctx.SaveChanges();
        //}


        [HttpGet]
        public string Get()
        {
            Dweller newUser = new Dweller();
            newUser.HP = 100;
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
