using Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Commands.DeleteBook
{
    public class DeleteBooksCommand : IRequest<string>
    {
        public string BookId { get; set; }
    }
    public class DeleteBooksCommandHandle : IRequestHandler<DeleteBooksCommand, string>
    {
        private readonly IBookishDbContext _bookishDb;

        public DeleteBooksCommandHandle(IBookishDbContext bookishDb)
        {
            _bookishDb = bookishDb;
        }

        public async Task<string> Handle(DeleteBooksCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookishDb.Books
                .FirstOrDefaultAsync(f => f.BookId == request.BookId);
            if (book == null)
            {
                return "Book doesnt exists";
            }
            _bookishDb.Books.Remove(book);
            await _bookishDb.SaveChangesAsync(cancellationToken);
            return "Book deleted successfully";
        }
    }
}
