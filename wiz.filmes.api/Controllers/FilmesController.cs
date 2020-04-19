using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using wiz.filmes.api.ViewModels;
using wiz.filmes.api.Services;

namespace wiz.filmes.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {

        private readonly IFilmeService _filmeService;

        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        /// <summary>
        ///  API para recuperar os filmes a serem lançados 
        /// </summary>
        /// <param name="nPage"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int nPage)
        {

            var result = _filmeService.ObterFilmes(nPage).Result;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
    }
}
