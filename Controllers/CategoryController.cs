using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Remoting;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories;
            return View(objCategoryList);
        }
        // Get
        public IActionResult Create()
        {
            return View();
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                ModelState.AddModelError("name", "The Name cannot be empty.");

            if (string.IsNullOrEmpty(obj.DisplayOrder.ToString()))
                ModelState.AddModelError("displayorder", "The DisplayOrder cannot be empty.");

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");                
            }
            return View(obj);
        }

        // Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryFromDB = _context.Categories.Find(id);
            // var categoryFromDBFirst = _context.Categories.FirstOrDefault(a => a.Id == id);
            // var categoryFromDBSingle = _context.Categories.SingleOrDefault(a => a.Id == id);

            if (categoryFromDB == null)
                return NotFound();

            return View(categoryFromDB);
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                ModelState.AddModelError("name", "The Name cannot be empty.");

            if (obj.Name.Equals(obj.DisplayOrder.ToString()))
                ModelState.AddModelError("name", "The Name and Display order cannot be same!");

            if (ModelState.IsValid)
            {
                //var category = _context.Categories.Find(obj.Id);
                //category.Name = obj.Name;
                //category.DisplayOrder = obj.DisplayOrder;
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryFromDB = _context.Categories.Find(id);
            // var categoryFromDBFirst = _context.Categories.FirstOrDefault(a => a.Id == id);
            // var categoryFromDBSingle = _context.Categories.SingleOrDefault(a => a.Id == id);

            if (categoryFromDB == null)
                return NotFound();

            return View(categoryFromDB);
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
