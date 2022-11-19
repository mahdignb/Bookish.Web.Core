using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return "Hello world";
        }
    }
}
