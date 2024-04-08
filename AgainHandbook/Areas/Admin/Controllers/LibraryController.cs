using Again.DataAccess.Repository.IRepository;
using Again.DataAcess.Data;
using Again.Models.ViewModels;
using AgainHandbook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgainHandbook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LibraryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
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

            if (id == null || id == 0)
            {
                //create
                return View(libraryVM);

            }
            else
            
                //Update
                libraryVM.Library = _unitOfWork.Library.Get(u => u.Id == id);
                return View(libraryVM);

            
        }

        [HttpPost]
        public IActionResult Upsert(LibraryVM libraryVM)
        {
            if (ModelState.IsValid)
            {
                if (libraryVM.Library.Id == 0) // If ID is 0, it's a new record
                {
                    _unitOfWork.Library.Add(libraryVM.Library);
                    TempData["success"] = "Library Created successfully";
                }
                else // If ID is not 0, it's an existing record
                {
                    _unitOfWork.Library.Update(libraryVM.Library);
                    TempData["success"] = "Library Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                libraryVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(libraryVM);
            }
        }


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
