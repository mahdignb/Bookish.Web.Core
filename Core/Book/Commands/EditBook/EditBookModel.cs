using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Commands.EditBook
{
    public class EditBookModel
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Edition { get; set; }
        public DateTime PublishTime { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }
    }
}
