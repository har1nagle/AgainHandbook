using Again.DataAcess.Data;
using AgainHandbook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AgainHandbook.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _db;
        //Constructor (ctor)
        public LibraryController(ApplicationDbContext db)
        {
            _db = db;  
        }

        public IActionResult Index()
        {
            List<Library> objLibraryList = _db.Libraries.ToList();
          

            return View(objLibraryList);
        }


        //Create index.cshtml (UI)
        public IActionResult Create()
        {
            //For Category dropdown
            IEnumerable<SelectListItem> CategoryList = _db.Categories.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()

            });
            //ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;


            return View();
        }

        // Create Library List
        [HttpPost]
        public IActionResult Create(Library obj) 
        {
            if (ModelState.IsValid)
            {
                _db.Libraries.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Library Created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        //Edit Library List
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            { 
                return NotFound();
            }
            Library libraryFromDb = _db.Libraries.Find(id);
            if (libraryFromDb == null)
            {
                return NotFound();
            }
            return View(libraryFromDb);
        }

        // Update Library List
        [HttpPost]
        public IActionResult Edit(Library obj)
        {
            if (ModelState.IsValid)
            {
                _db.Libraries.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Library updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        //Delete Library List
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Library libraryFromDb = _db.Libraries.Find(id);
            if (libraryFromDb == null)
            {
                return NotFound();
            }
            return View(libraryFromDb);
        }

        // Delete Library List
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Library? obj = _db.Libraries.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Libraries.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Library deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
