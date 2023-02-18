using Common.Response;
using Core.Common.Interfaces;
using Domain.Entities.Bookish;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Book.Queries.GetBorrowedBooks
{
    public class GetBorrowedBooksQuery:IRequest<StandardResponse<List<BorrowBookModel>>>
    {

    }
    public class GetBorrowedBooksQueryHanler : IRequestHandler<GetBorrowedBooksQuery, StandardResponse<List<BorrowBookModel>>>
    {
        private readonly IBookishDbContext _bookishDb;

        public GetBorrowedBooksQueryHanler(IBookishDbContext bookishDb)
        {
            _bookishDb = bookishDb;
        }

        public async Task<StandardResponse<List<BorrowBookModel>>> Handle(GetBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            var borrowedBooks = await _bookishDb.BorrowBooks
                .Where(w=>w.IsReturned == false)
                .ToListAsync(cancellationToken);
            foreach (var book in borrowedBooks)
            {
                var userName = await _bookishDb.Users
                    .Where(w => w.Id == book.UserId)
                    .Select(w => w.UserName)
                    .FirstOrDefaultAsync(cancellationToken);
                var bookName = await _bookishDb.Books
                    .Where(w => w.BookId == book.BookId)
                    .Select(s=>s.Title)
                    .FirstOrDefaultAsync(cancellationToken);
                book.UserId = userName;
                book.BookId = bookName;
            }
            return new StandardResponse<List<BorrowBookModel>>()
            {
                Data =borrowedBooks,
                ResponseStatus=ResponseStatus.Success.ToString(),
                ResponseText = "Success"
            };
        }
    }
}
