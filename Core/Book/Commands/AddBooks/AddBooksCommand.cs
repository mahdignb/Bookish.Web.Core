using Core.Common.Interfaces;
using Domain.Entities.Bookish;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Commands.AddBooks
{
    public class AddBooksCommand : IRequest<string>
    {
        public List<AddBookModel> Model { get; set; }
    }
    public class AddBooksCommandHandler : IRequestHandler<AddBooksCommand, string>
    {
        private readonly IBookishDbContext _bookishDb;

        public AddBooksCommandHandler(IBookishDbContext bookishDb)
        {
            _bookishDb = bookishDb;
        }

        public async Task<string> Handle(AddBooksCommand request, CancellationToken cancellationToken)
        {
            var books = new List<Domain.Entities.Bookish.Book>();
            foreach (var book in request.Model)
            {
                books.Add(new Domain.Entities.Bookish.Book
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Language = book.Language,
                    Edition = book.Edition,
                    ISBN = book.ISBN,
                    PublisherId = book.PublisherId,
                    PublishTime = book.PublishTime,
                    Description = book.Description
                });
            }
            _bookishDb.Books.AddRange(books);
            var addedBooks = await _bookishDb.SaveChangesAsync(cancellationToken);
            if (addedBooks > 0)
            {
                return $"Added new {addedBooks} book";
            }
            return "Something went wrong";
        }
    }
}
