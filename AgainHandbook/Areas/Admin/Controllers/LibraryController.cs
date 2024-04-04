using Again.DataAccess.Repository.IRepository;
using Again.DataAcess.Data;
using AgainHandbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgainHandbook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LibraryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //Constructor (ctor)
        public LibraryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Library> objLibraryList = _unitOfWork.Library.GetAll().ToList();
            return View(objLibraryList);
        }


        //Create index.cshtml (UI)
        public IActionResult Create()
        {
            return View();
        }

        // Create Library List
        [HttpPost]
        public IActionResult Create(Library obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Library.Add(obj);
                _unitOfWork.Save();
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
            Library libraryFromDb = _unitOfWork.Library.Get(u => u.Id == id);
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
                _unitOfWork.Library.Update(obj);
                _unitOfWork.Save();
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
            Library libraryFromDb = _unitOfWork.Library.Get(u => u.Id == id);
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
            Library? obj = _unitOfWork.Library.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Library.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Library deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
