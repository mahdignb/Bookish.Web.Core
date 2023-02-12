using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Commands.BorrowBook
{
    public class BorrowBookCommandModel
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
    }
}
