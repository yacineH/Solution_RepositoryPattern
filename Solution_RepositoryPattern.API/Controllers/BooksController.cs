using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Solution_RepositoryPattern.API.Utils;
using Solution_RepositoryPattern.Core.Constants;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            Log.Information("Exe : GetAllAsync");
            var result = await _unitOfWork.Books.GetAllAsync(new[] {"Genre","Author"} );
            return Ok(result);
        }


        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromForm]BookDto dto)
        {
            if (!RestrictionFile.Extensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("seulement les fichiers avec extention : .jpg,.png sont autorisés");

            if (dto.Poster.Length > RestrictionFile.MaximumSize)
                return BadRequest("le fichier ne doit pas depasser 1MB");

            var isValidAuthor = await _unitOfWork.Authors.SingleAsync(a=>a.Id == dto.Author_Id);
            
            if (isValidAuthor == null)
                return BadRequest($"AuthorId n'existe pas en base");

            var isValidGenre = _unitOfWork.Genres.SingleAsync(g => g.Id == dto.GenreId);

            if (isValidGenre == null)
                return BadRequest($"GenreId n'existe pas en base");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var book = new Book
            {
                GenreId = dto.GenreId,
                Title = dto.Book_Title,
                Poster = dataStream.ToArray(),
                AuthorId = dto.Author_Id,
                Price = dto.Price,
            };
            
            await _unitOfWork.Books.AddAsync(book);

            return Ok(book);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.FindAsync(b=>b.Id == id, new[] {"Author","Genre"});

            if (book == null)
                return NotFound($"le book avec id :{id} n'existe pas");

            return Ok(book);
        }


        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            Log.Information("Exe : GetByGenreIdAsync");
            var result = await _unitOfWork.Books.GetAllAsync(b=>b.GenreId == genreId,new[] { "Genre", "Author" });
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var book = await _unitOfWork.Books.FindAsync(b=>b.Id == id);

            if (book == null)
                return NotFound($"book avec id {id} n'existe pas en base");

            return Ok(_unitOfWork.Books.Delete(book));
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm]BookDto dto)
        {
            var book = await _unitOfWork.Books.FindAsync(b => b.Id == dto.GenreId);

            if (book == null)
                return NotFound($"book avec id {id} n'existe pas en base");

            var isValidGenre = _unitOfWork.Genres.SingleAsync(g => g.Id == dto.GenreId);

            if (isValidGenre == null)
                return BadRequest($"GenreId n'existe pas en base");
            
            if (dto.Poster != null) 
            {
                if (!RestrictionFile.Extensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("seulement les fichiers avec extention : .jpg,.png sont autorisés");

                if (dto.Poster.Length > RestrictionFile.MaximumSize)
                    return BadRequest("le fichier ne doit pas depasser 1MB");

                var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                book.Poster = dataStream.ToArray();
            }

            book.Title = dto.Book_Title;
            book.GenreId = dto.GenreId;
            book.AuthorId = dto.Author_Id;
            book.Price = dto.Price;

            return Ok(_unitOfWork.Books.Update(book));

        }



        #region avant-modif
        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync()
        {
            var book = await _unitOfWork.Books.FindAsync(b => b.Title == "Book 1", new[] { "Author" });
            var bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }


        [HttpGet("GetAllWithAuthorsAsync")]
        public async Task<IActionResult> GetAllWithAuthorsAsync()
        {
            var result = _mapper.Map<IEnumerable<BookDto>>(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("Book 1"), new[] { "Author" }));

            return Ok(result);
        }



        [HttpGet("GetOrderedAsync")]
        public async Task<IActionResult> GetOrderedAsync()
        {
            var result = _mapper.Map<IEnumerable<BookDto>>(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("Book"), null, null, b => b.Id, OrderBy.Descending));

            return Ok(result);
        }


        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync()
        {
            var bookDto = new BookDto { Book_Title = "Test 3", Author_Id = 1 };
            var book = _mapper.Map<Book>(bookDto);

            return Ok(await _unitOfWork.Books.AddAsync(book));
        }
        #endregion
    }
}
