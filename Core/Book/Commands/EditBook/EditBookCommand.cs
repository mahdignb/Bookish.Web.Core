using Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Commands.EditBook
{
    public class EditBookCommand : IRequest<string>
    {
        public string BookId { get; set; }
        public EditBookModel Model { get; set; }
    }
    public class EditBookCommandHandler : IRequestHandler<EditBookCommand, string>
    {
        private readonly IBookishDbContext _bookishDb;

        public EditBookCommandHandler(IBookishDbContext bookishDb)
        {
            _bookishDb = bookishDb;
        }

        public async Task<string> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookishDb.Books
                .FirstOrDefaultAsync(f => f.BookId == request.BookId);
            if (book == null)
            {
                return "Book doesnt exist";
            }
            book.Title = request.Model.Title;
            book.ISBN = request.Model.ISBN;
            book.PublisherId = request.Model.PublisherId;
            book.Description = request.Model.Description;
            book.Language = request.Model.Language;
            book.Edition = request.Model.Edition;

            await _bookishDb.SaveChangesAsync(cancellationToken);

            return "Book edited successfully";
        }
    }
}
