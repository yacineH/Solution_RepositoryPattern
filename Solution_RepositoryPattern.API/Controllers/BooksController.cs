using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution_RepositoryPattern.Core.Constants;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.Core.Models;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Solution_RepositoryPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _booksRepository;

        public BooksController(IBaseRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;   
        }

        [HttpGet]
        public ActionResult GetById()
        {
            //je recupere un objet domain et puis il faut le converir en DTO
            //id en dure on imagine qu'on le recupere du client
            var book = _booksRepository.GetById(1);
            var author = book.Author;
            var authorDto = new AuthorDto { Author_Id = author.Id, Nom = author.Name };

            var bookDto = new BookDto
            {
                Author = authorDto,
                Author_Id = book.AuthorId,
                Book_Id = book.Id,
                Book_Title = book.Title,
            };
            return Ok(bookDto);
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_booksRepository.GetAll().Select(book => new BookDto
            {
                Author = new AuthorDto { Author_Id = book.Author.Id, Nom = book.Author.Name },
                Author_Id = book.AuthorId,
                Book_Id = book.Id,
                Book_Title = book.Title,
            })) ;
        }


        [HttpGet("GetByName")]
        public ActionResult GetByName()
        {
            var book = _booksRepository.Find(b => b.Title == "Book 1", new[] { "Author" });
            var bookDto = new BookDto
            {
                Author = new AuthorDto { Author_Id = book.Author.Id, Nom = book.Author.Name },
                Author_Id = book.AuthorId,
                Book_Id = book.Id,
                Book_Title = book.Title,
            };
            return Ok(bookDto);
        }

        [HttpGet("GetAllWithAuthors")]
        public ActionResult GetAllWithAuthors()
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains("Book 1"), new[] { "Author" })
                .Select(book=> new BookDto
                 {
                     Author = new AuthorDto { Author_Id = book.Author.Id, Nom = book.Author.Name },
                    Author_Id = book.AuthorId,
                     Book_Id = book.Id,
                     Book_Title = book.Title,

                 }));
        }


        [HttpGet("GetOrdered")]
        public ActionResult GetOrdered()
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains("Book"), null, null, b => b.Id, OrderBy.Descending)
                      .Select(book => new BookDto 
                      { 
                          Author = new AuthorDto { Author_Id = book.Author.Id, Nom = book.Author.Name },
                          Author_Id = book.AuthorId, 
                          Book_Id = book.Id, 
                          Book_Title = book.Title 
                      }));
        }

        [HttpPost("AddOne")]
        public ActionResult AddOne()
        {
            //on imagine qu'on recupere une dto object depuis le client (BookBto)
            //et puis je le converti en class domain (Book)
            var bookDto = new BookDto { Book_Title = "Test 3", Author_Id = 1 };
            var book = new Book { Title = bookDto.Book_Title, AuthorId = bookDto.Author_Id };
            return Ok(_booksRepository.Add(book));
        }

    }
}
