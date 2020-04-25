using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class MockBookRepository : IBookRepository
    {
        private List<IBook> _bookList;

        public MockBookRepository()
        {
            _bookList = new List<IBook>
            {
                new IBook() { Id = 1001, BookName = "西游记", AuthorName = "吴承恩", Value = 31.5M },
                new IBook() { Id = 1002, BookName = "红楼梦", AuthorName = "曹雪芹", Value = 45.8M },
                new IBook() { Id = 1003, BookName = "三国演义", AuthorName = "吴承恩", Value = 38.0M },
            };
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _bookList;
        }

        public IBook GetBook(int id)
        {
            return _bookList.FirstOrDefault(a => a.Id == id);
        }

        public IBook GetBook(string name)
        {
            return _bookList.FirstOrDefault(a => a.BookName.Equals(name));
        }
    }
}
