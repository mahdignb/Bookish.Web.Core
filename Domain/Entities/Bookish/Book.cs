using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bookish
{
    public class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Edition { get; set; }
        public string PublishTime { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
