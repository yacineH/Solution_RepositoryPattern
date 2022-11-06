using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.Core.Models;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _authorsRepository;

        public AuthorsController(IBaseRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        //[HttpGet]
        //public ActionResult GetById()
        //{
        //    //id est en dure on imagine qu'on le recupre depuis le client
        //    var author = _authorsRepository.GetById(1);
        //    var authorDto = new AuthorDto { Author_Id = author.Id, Nom = author.Name };
        //    return Ok(authorDto);
        //}

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById()
        {
            var author = await _authorsRepository.GetByIdAsync(1);
            var authorDto = new AuthorDto { Author_Id = author.Id, Nom = author.Name };
            return Ok(authorDto);
        }
    }
}
