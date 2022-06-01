using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorage.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("The api v1 is running!");
        }

    }
}
