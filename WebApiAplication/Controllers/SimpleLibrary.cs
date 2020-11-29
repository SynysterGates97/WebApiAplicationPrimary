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
            _ctx.SavedChanges += _ctx_SavedChanges;
        }

        public DateTime LastDbUpdateTime { get; set; }

        private void _ctx_SavedChanges(object sender, Microsoft.EntityFrameworkCore.SavedChangesEventArgs e)
        {
            LastDbUpdateTime = DateTime.Now;
        }

        [HttpGet()]
        public string GetLibraryHeader()
        {
            string headString = $"В библиотеке на данный момент " +
                $"{_ctx.LibraryRecords.Count()} книг, " +
                $"дата последнего обновления {LastDbUpdateTime.ToString()}";
            return headString;
        }


        [HttpPost]
        public void AddNewBook([FromBody] string bookName)
        {
            LibraryRecord newRecord = new LibraryRecord();
            newRecord.BookName = bookName;
                        
            _ctx.LibraryRecords.Add(newRecord);
            _ctx.SaveChanges();
        }


        [HttpGet("books")]
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
