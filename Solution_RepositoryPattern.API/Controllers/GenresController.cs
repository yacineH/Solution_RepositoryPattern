using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Genre> _genresReposiroty;

        public GenresController(IMapper mapper, IBaseRepository<Genre> genresReposiroty)
        {
            _mapper = mapper;
            _genresReposiroty = genresReposiroty;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _genresReposiroty.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(result.OrderBy(g => g.Name)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            await _genresReposiroty.AddAsync(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] GenreDto dto)
        {
            var genre = await _genresReposiroty.SingleAsync(g=>g.Id == id);

            if (genre == null)
                return NotFound($"pas de genre avec cet Id : {id}");

            genre.Name = dto.Name;

            return Ok(_genresReposiroty.Update(genre));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre =await _genresReposiroty.SingleAsync(g => g.Id == id);

            if(genre == null)
                return NotFound($"pas de genre avec cet Id : {id}");

            return Ok(_genresReposiroty.Delete(genre));

        }
    }
}
