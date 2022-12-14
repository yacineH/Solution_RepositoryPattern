using Microsoft.AspNetCore.Http;
using Solution_RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.Core.Dtos
{
    public class BookDto
    {
        [Required, MaxLength(150)]
        public string Book_Title { get; set; }

        public int Author_Id { get; set; }

        public double? Price { get; set; }

        public IFormFile Poster { get; set; }

        public byte GenreId { get; set; }
        //solution possible mais je vais utiliser automapper
        //public bool? LongTitle => this.Book_Title.Length > 3;
        public bool? IsFree { get; set; } = false;
    }
}
