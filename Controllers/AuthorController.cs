using FileStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FileStore.Controllers
{
    public class AuthorController : Controller
    {
        FileStoreContext _context = null;
        public AuthorController(FileStoreContext inContext)
        {
            _context = inContext;
        }
        public IActionResult Index()
        {
            var model = _context.Authors.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("FirstName, LastName")] Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}