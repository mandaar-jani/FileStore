using FileStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FileStore.Controllers
{
    public class BookController : Controller
    {
        FileStoreContext _context = null;
        public BookController(FileStoreContext inContext)
        {
            _context = inContext;
        }
        public IActionResult Index()
        {
            var model = _context.Authors.Include(a => a.Books).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var authors = _context.Authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();
            ViewBag.Authors = authors;
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Title, AuthorId")] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}