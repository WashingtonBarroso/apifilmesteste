using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace wiz.filmes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlerErrorController : ControllerBase
    {

        [Route("/error-api")]
        [HttpGet]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(title: context.Error.Message);
        
        }

    }
}