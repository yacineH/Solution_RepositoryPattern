using AutoMapper;
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
        private readonly IMapper _mapper;
        public AuthorsController(IMapper mapper,IBaseRepository<Author> authorsRepository)
        {
            _mapper = mapper;
            _authorsRepository = authorsRepository;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            var author = await _authorsRepository.GetByIdAsync(1);
            var authorDto = _mapper.Map<AuthorDto>(author);  
            return Ok(authorDto);
        }
    }
}
