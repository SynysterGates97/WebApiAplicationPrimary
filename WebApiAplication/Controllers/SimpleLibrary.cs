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
            var lastDbUpdateTime = DateTime.Now;


            int libCount = _ctx.LastRecordTimes.Count();
            if (libCount == 0)
            {
                _ctx.LastRecordTimes.Add(new LastRecordTime() { lastUpdateTime = DateTime.Now });
            }
            else
            {
                _ctx.LastRecordTimes.Find(1).lastUpdateTime = DateTime.Now;
            }
                

            _ctx.SavedChanges -= _ctx_SavedChanges;
            _ctx.SaveChanges();
            _ctx.SavedChanges += _ctx_SavedChanges;
        }

        [HttpGet()]
        public string GetLibraryHeader()
        {
            int libCount = _ctx.LibraryRecords.Count();

            string lastUpdateTimeString = _ctx.LastRecordTimes.Count() > 0 ?
                  _ctx.LastRecordTimes.Find(1).lastUpdateTime.ToString() : 
                 "никогда";

            string headString = $"В вашей библиотеке на данный момент " +
                $"{_ctx.LibraryRecords.Count()} книг, " +
                $"дата последнего обновления {lastUpdateTimeString}";

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

        [HttpGet("db")]
        public IEnumerable<LibraryRecord> GetAll()
        {

            return _ctx.LibraryRecords;

        }

        [HttpDelete("{bookName}")]
        public string DeleteBookByName(string bookName)
        {
            var recordsToDelete = _ctx.LibraryRecords.Where(u => u.BookName ==  bookName);

            int deletedRecordsCount = 0;
            foreach(var record in recordsToDelete)
            {
                if (record != null)
                {
                    _ctx.LibraryRecords.Remove(record);
                    deletedRecordsCount++;
                    
                }

            }         
            if(deletedRecordsCount > 0)
            {
                _ctx.SaveChanges();
                return $"Книга \"{bookName}\" была удалена из вашей библиотеки, в количестве {deletedRecordsCount}";
            }

            return $"В библиотеке нет книги с таким названием";

        }



        [HttpGet("{ip}")]
        public string Get(int ip)
        {
            
            return "Hasssa" + ip;
        }
    }
}
