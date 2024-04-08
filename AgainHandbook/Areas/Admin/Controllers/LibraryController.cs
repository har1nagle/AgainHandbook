using Again.DataAccess.Repository.IRepository;
using Again.DataAcess.Data;
using Again.Models.ViewModels;
using AgainHandbook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
            List<Library> objLibraryList = _unitOfWork.Library.GetAll(includeProperties:"Category").ToList();
           
            return View(objLibraryList);
        }


        //Library Create index.cshtml (UI)
        public IActionResult Upsert(int? id)
        {
            LibraryVM libraryVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            Library = new Library()
            };

            if(id == null || id == 0)
            {
                //create
                return View(libraryVM);

            } else
            {
                //Update
                libraryVM.Library = _unitOfWork.Library.Get(u=>u.Id== id);
                return View(libraryVM);

            }
        }


        //Library Create Library List
        [HttpPost]
        public IActionResult Upsert(LibraryVM libraryVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Library.Add(libraryVM.Library);
                _unitOfWork.Save();
                TempData["success"] = "Library Created successfully";
                return RedirectToAction("Index");
            } else
            {
                libraryVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            return View(libraryVM);
            }


        }
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Library libraryFromDb = _unitOfWork.Library.Get(u => u.Id == id);
        //    if (libraryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(libraryFromDb);
        //}

        //// Delete Library List
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Library? obj = _unitOfWork.Library.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Library.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Library deleted successfully";
        //    return RedirectToAction("Index");
        //}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Library> objLibraryList = _unitOfWork.Library.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objLibraryList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var libraryToBeDeleted = _unitOfWork.Library.Get(u => u.Id == id);
            if (libraryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Library.Remove(libraryToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
