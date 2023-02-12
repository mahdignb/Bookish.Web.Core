using Common.Response;
using Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.User.Queries.GetUsers
{
    public class GetUsersQueries : IRequest<StandardResponse<List<GetUsersDto>>>
    {

    }
    public class GetUserQueriesHandler : IRequestHandler<GetUsersQueries, StandardResponse<List<GetUsersDto>>>
    {
        private readonly IBookishDbContext _bookishDb;
        private readonly IAccessService _accessService;

        public GetUserQueriesHandler(IBookishDbContext bookishDb, IAccessService accessService)
        {
            _bookishDb = bookishDb;
            _accessService = accessService;
        }

        public async Task<StandardResponse<List<GetUsersDto>>> Handle(GetUsersQueries request, CancellationToken cancellationToken)
        {
            var currentUser = _accessService.GetCurrentUser();
            if (currentUser == null)
            {
                return new StandardResponse<List<GetUsersDto>>
                {
                    ResponseStatus = ResponseStatus.Failed.ToString(),
                    ResponseText = "User not found",
                    Data = new List<GetUsersDto>()
                };
            }
            if (currentUser.IsAdmin == false && currentUser.IsActive == false)
            {
                return new StandardResponse<List<GetUsersDto>>
                {
                    ResponseStatus = ResponseStatus.Failed.ToString(),
                    ResponseText = "You dont have access to this section",
                    Data = new List<GetUsersDto>()
                };
            }
            var users = await _bookishDb.Users.ToListAsync(cancellationToken);
            var usersDto = new List<GetUsersDto>();
            foreach (var user in users)
            {
                usersDto.Add(new GetUsersDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserType = user.UserType,
                    PhoneNumber = user.PhoneNumber,
                    NumberOfBorrowedBooks = await _bookishDb.BorrowBooks
                    .CountAsync(c => c.UserId == user.Id, cancellationToken)
                });
            }
            return new StandardResponse<List<GetUsersDto>>
            {
                ResponseStatus = ResponseStatus.Success.ToString(),
                ResponseText = "Ok",
                Data = usersDto
            };
        }
    }
}
