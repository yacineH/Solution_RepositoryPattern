using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.Core.Dtos
{
    public class AuthorDto
    {
        public int Author_Id { get; set; }

        [Required, MaxLength(150)]
        public string Nom { get; set; }
    }
}
