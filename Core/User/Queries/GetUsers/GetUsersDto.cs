using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.User.Queries.GetUsers
{
    public class GetUsersDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfBorrowedBooks { get; set; }
    }
}
