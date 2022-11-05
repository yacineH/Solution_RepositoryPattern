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
        public int Book_Id { get; set; }

        [Required, MaxLength(150)]
        public string Book_Title { get; set; }

        public int Author_Id { get; set; }

        public AuthorDto Author { get; set; }
    }
}
