using Solution_RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }

        IBaseRepository<Book> Books { get; }

        IBaseRepository<Genre> Genres { get; }

        int Complete();
    }
}
