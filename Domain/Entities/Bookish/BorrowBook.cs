using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bookish
{
    public class BorrowBookModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
