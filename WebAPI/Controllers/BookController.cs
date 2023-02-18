using Common.Response;
using Core.Book.Commands.AddBooks;
using Core.Book.Commands.BorrowBook;
using Core.Book.Commands.DeleteBook;
using Core.Book.Commands.EditBook;
using Core.Book.Commands.ReturnBook;
using Core.Book.Queries.GetBooks;
using Core.Book.Queries.GetBorrowedBooks;
using Domain.Entities.Bookish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ApiControllerBase
    {
        [HttpPost]
        [Route("GetBooks")]
        public async Task<List<Book>> GetBooks([FromBody] GetBooksQuery query)
        {
            return await Mediator.Send(query);
        }
        [HttpPost]
        [Route("GetBorrowedBooks")]
        public async Task<StandardResponse<List<BorrowBookModel>>> GetBorrowedBooks([FromBody] GetBorrowedBooksQuery query)
        {
            return await Mediator.Send(query);
        }
        [HttpPost]
        [Route("AddBooks")]
        public async Task<string> AddBooks([FromBody] AddBooksCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost]
        [Route("EditBook")]
        public async Task<string> EditBook([FromBody] EditBookCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<string> DeleteBook([FromBody] DeleteBooksCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost]
        [Route("BorrowBook")]
        public async Task<StandardResponse<string>> BorrowBook([FromBody] BorrowBookCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost]
        [Route("ReturnBook")]
        public async Task<StandardResponse<string>> ReturnBook([FromBody] ReturnBookCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
