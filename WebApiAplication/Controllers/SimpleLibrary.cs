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
        public void AddNewBook([FromBody] string bookName)
        {
            LibraryRecord newRecord = new LibraryRecord();
            newRecord.BookName = bookName;

            _ctx.LibraryRecords.Add(newRecord);
            _ctx.SaveChanges();
        }


        [HttpGet]
        public IEnumerable<string> GetBooksInLibrary()
        {
            var books = _ctx.LibraryRecords.Select(b => b.BookName).ToArray();

            return books;

        }

        [HttpGet("{ip}")]
        public string Get(int ip)
        {
            
            return "Hasssa" + ip;
        }
    }
}
