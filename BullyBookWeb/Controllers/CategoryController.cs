using BullyBookWeb.Data;
using BullyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BullyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _applicationDbContext.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Add(obj);
                _applicationDbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromdb = _applicationDbContext.Categories.Find(id);
           // var categoryfromdbfirst = _applicationDbContext.Categories.FirstOrDefault(c => c.Id == id);
           // var categoryfromdbsingle = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);
           if(categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }
       
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Update(obj);
                _applicationDbContext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromdb = _applicationDbContext.Categories.Find(id);
            // var categoryfromdbfirst = _applicationDbContext.Categories.FirstOrDefault(c => c.Id == id);
            // var categoryfromdbsingle = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _applicationDbContext.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
                _applicationDbContext.Categories.Remove(obj);
                _applicationDbContext.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
        }
    }
}
