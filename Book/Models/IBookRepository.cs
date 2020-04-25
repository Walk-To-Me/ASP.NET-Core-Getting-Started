using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public interface IBookRepository
    {
        IBook GetBook(int id);
        IBook GetBook(string name);
        IEnumerable<IBook> GetAllBooks();

    }
}
