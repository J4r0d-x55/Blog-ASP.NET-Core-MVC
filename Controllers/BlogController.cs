using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using BlogMVC.Models;
using System.Linq;

namespace BlogMVC.Controllers
{
    
    public class BlogController : Controller
    {
        static List<BlogEntry> Posts = new List<BlogEntry>();

        public IActionResult Index()
        {
            return View("Index", Posts);
        }
        public IActionResult CreatorPage(Guid id)
        {
            if (id != Guid.Empty)
            {
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == id);
                return View(model: existingEntry);
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreatorPage(BlogEntry entry)
        {
            if(entry.Id == Guid.Empty)
            {
                // New article

                BlogEntry newEntry = new BlogEntry();
                newEntry.Content = entry.Content;
                newEntry.Id = Guid.NewGuid();
                Posts.Add(newEntry);
            }
            else
            {
                // Existing article

                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == entry.Id);
                existingEntry.Content = entry.Content;
            }
           
            return RedirectToAction("Index");
        }
    }
}
