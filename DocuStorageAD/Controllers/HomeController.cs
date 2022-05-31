using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageAD.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("The api is running!");
        }

    }
}
