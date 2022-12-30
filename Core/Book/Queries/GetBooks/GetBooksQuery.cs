using Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Book.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<List<Domain.Entities.Bookish.Book>>
    {

    }
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Domain.Entities.Bookish.Book>>
    {
        private readonly IBookishDbContext _bookishDb;

        public GetBooksQueryHandler(IBookishDbContext bookishDb)
        {
            _bookishDb = bookishDb;
        }

        public async Task<List<Domain.Entities.Bookish.Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookishDb.Books
                .ToListAsync(cancellationToken);
        }
    }
}
