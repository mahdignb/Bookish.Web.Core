using Common.Response;
using Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Book.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest<StandardResponse<string>>
    {
        public ReturnBookModel ReturnBookModel { get; set; }
    }
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, StandardResponse<string>>
    {
        private readonly IBookishDbContext _bookishDb;
        private readonly IAccessService _accessService;
        public ReturnBookCommandHandler(IBookishDbContext bookishDb, IAccessService accessService)
        {
            _bookishDb = bookishDb;
            _accessService = accessService;
        }

        public async Task<StandardResponse<string>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accessService.GetCurrentUser();
            if (currentUser != null && currentUser.IsActive == false)
            {
                return new StandardResponse<string>
                {
                    ResponseStatus = ResponseStatus.Failed.ToString(),
                    ResponseText = "Something went wrong, please contact admin",
                    Data = ""
                };
            }
            var returnedBooks = await _bookishDb.BorrowBooks
                .Where(w => w.Id == request.ReturnBookModel.BorrowBookId)
                .FirstOrDefaultAsync(cancellationToken);
            if (returnedBooks == null)
            {
                return new StandardResponse<string>
                {
                    ResponseStatus = ResponseStatus.Failed.ToString(),
                    ResponseText = "Book is not exist",
                    Data = ""
                };
            }
            var book = await _bookishDb.Books
                .Where(w => w.BookId == returnedBooks.BookId)
                .FirstOrDefaultAsync(cancellationToken);
            book.IsAvailable = true;
            returnedBooks.IsReturned = true;
            returnedBooks.UpdatedAt = DateTime.UtcNow;
            await _bookishDb.SaveChangesAsync(cancellationToken);
            return new StandardResponse<string>
            {
                ResponseStatus = ResponseStatus.Success.ToString(),
                ResponseText = "Book returned successfully",
                Data = ""
            };
        }
    }
}
