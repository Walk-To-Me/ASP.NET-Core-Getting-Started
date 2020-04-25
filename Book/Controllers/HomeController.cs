using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
using Book.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRespository;

        public HomeController(IBookRepository bookRespository)
        {
            _bookRespository = bookRespository;
        }

        public IActionResult Index()
        {
            var model = _bookRespository.GetAllBooks();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                IBook = _bookRespository.GetBook(id ?? 1001),
                PageTitle = "书籍详细信息",
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(IBook book)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Details", new { id = _bookRespository.GetBook(book.BookName).Id } );
            return View();
        }
    }
}