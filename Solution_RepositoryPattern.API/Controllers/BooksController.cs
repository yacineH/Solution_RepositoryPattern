using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution_RepositoryPattern.Core.Constants;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.Core.Models;
using System.Collections;
using System.Collections.Generic;
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
        private readonly IBaseRepository<Book> _booksRepository;

        public BooksController(IBaseRepository<Book> booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            var book = await _booksRepository.GetByIdAsync(1);
            var bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = _mapper.Map<IEnumerable<BookDto>>(await _booksRepository.GetAllAsync());
            return Ok(result);
        }


        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName()
        {
            var book = await _booksRepository.FindAsync(b => b.Title == "Book 1", new[] { "Author" });
            var bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }


        [HttpGet("GetAllWithAuthors")]
        public async Task<IActionResult> GetAllWithAuthors()
        {
            var result = _mapper.Map<IEnumerable<BookDto>>(await _booksRepository.FindAllAsync(b => b.Title.Contains("Book 1"), new[] { "Author" }));

            return Ok(result);
        }



        [HttpGet("GetOrdered")]
        public async Task<IActionResult> GetOrdered()
        {
            var result = _mapper.Map<IEnumerable<BookDto>>(await _booksRepository.FindAllAsync(b => b.Title.Contains("Book"), null, null, b => b.Id, OrderBy.Descending));

            return Ok(result);
        }


        [HttpPost("AddOne")]
        public async Task<IActionResult> AddOne()
        {
            var bookDto = new BookDto { Book_Title = "Test 3", Author_Id = 1 };
            var book = _mapper.Map<Book>(bookDto);

            return Ok(await _booksRepository.AddAsync(book));
        }

    }
}
