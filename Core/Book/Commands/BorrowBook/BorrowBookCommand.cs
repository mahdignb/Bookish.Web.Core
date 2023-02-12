using Common.Response;
using Core.Common.Interfaces;
using Domain.Entities.Bookish;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Book.Commands.BorrowBook
{
    public class BorrowBookCommand : IRequest<StandardResponse<string>>
    {
        public List<BorrowBookCommandModel> BorrowBookCommandModel { get; set; }
    }
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, StandardResponse<string>>
    {
        private readonly IBookishDbContext _bookishDb;
        private readonly IAccessService _accessService;
        public BorrowBookCommandHandler(IBookishDbContext bookishDb, IAccessService accessService)
        {
            _bookishDb = bookishDb;
            _accessService = accessService;
        }

        public async Task<StandardResponse<string>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
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
            var isBookAvailable = !await _bookishDb.BorrowBooks
                .AnyAsync(a => request.BorrowBookCommandModel.Select(s => s.BookId).Contains(a.BookId), cancellationToken);
            if (isBookAvailable == false)
            {
                return new StandardResponse<string>
                {
                    ResponseStatus = ResponseStatus.Failed.ToString(),
                    ResponseText = "You cant borrow this book because it is already borrowed",
                    Data = ""
                };
            }
            var borrowedBooks = new List<BorrowBookModel>();
            foreach (var item in request.BorrowBookCommandModel)
            {
                borrowedBooks.Add(new BorrowBookModel
                {
                    Id = Guid.NewGuid().ToString(),
                    BookId = item.BookId,
                    UserId = item.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
                var book = await _bookishDb.Books
                    .Where(w => w.BookId == item.BookId)
                    .FirstOrDefaultAsync(cancellationToken);
                book.IsAvailable = false;
            }
            _bookishDb.BorrowBooks.AddRange(borrowedBooks);
            await _bookishDb.SaveChangesAsync(cancellationToken);
            return new StandardResponse<string>
            {
                ResponseStatus = ResponseStatus.Success.ToString(),
                ResponseText = "Operation completed successfully"
            };
        }
    }
}
