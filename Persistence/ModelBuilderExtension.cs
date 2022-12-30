using Domain.Entities.Account;
using Domain.Entities.Bookish;
using Domain.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            //Roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "USER"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "SuperAdmin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "SUPERADMIN"
            });
            //Publishers
            builder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = "1",
                Name = "Manning",
                Address = "Shelter Island",
                City = "New York",
                Country = "United States"
            });
            //Author
            builder.Entity<Author>().HasData(new Author
            {
                AuthorId = "1",
                FirstName = "Robert",
                MiddleName = "C",
                LastName = "Martin",
            });
            //Books
            builder.Entity<Book>().HasData(new Book
            {
                BookId = "1",
                Title = "Clean code",
                Language = "English",
                Edition = "1",
                PublisherId = "1",
                ISBN = "1234",
                Description = "Uncle bobs clean code",
                PublishTime = DateTime.UtcNow
            });
            //BooksAuthor
            builder.Entity<BookAuthor>().HasData(new BookAuthor
            {
                AuthorId = "1",
                BookId = "1"
            });
        }
    }
}
