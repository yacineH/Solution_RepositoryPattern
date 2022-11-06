using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.Core.Dtos
{
    public class GenreDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
