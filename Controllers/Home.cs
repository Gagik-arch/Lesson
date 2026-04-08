using Microsoft.AspNetCore.Mvc;

namespace Base
{

    [ApiController]
    [Route("")]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Base";
        }
    }
}