using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAplication.Models
{
    public class LibraryRecord
    {
        public int Id { get; set; }

        public DateTime lastUpdateTime { get; set; }

        public string Author { get; set; }

        public string BookName { get; set; }

        public string YearOfIssue { get; set; }

        public string  Publisher { get; set; }
    }
}
