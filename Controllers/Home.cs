using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Base
{
    [AllowAnonymous]
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