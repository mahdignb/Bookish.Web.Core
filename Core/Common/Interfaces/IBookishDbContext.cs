using Domain.Entities.Account;
using Domain.Entities.Bookish;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Interfaces
{
    public interface IBookishDbContext
    {
        public DbSet<Domain.Entities.Bookish.Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Account> Users { get; set; }
    }
}
