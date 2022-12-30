using Core.Book.Queries.GetBooks;
using Domain.Entities.Bookish;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
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
    }
}
