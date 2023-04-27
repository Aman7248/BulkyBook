using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private IUnitOfWork _unitOfWork; 

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categoryList=_unitOfWork.CoverType.GetAll();
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var categoryFromDb = _appDbContext.Categories.Find(id);
            var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }
        [HttpPost]
        public IActionResult Edit(CoverType updatedCoverType)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(updatedCoverType);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");
            }
            return View(updatedCoverType);
        }
        public IActionResult Delete(int? id)
        {
            //var categoryFromDb = _appDbContext.Categories.Find(id);
            var coverTypeFromDb =_unitOfWork.Category.GetFirstOrDefault(u=>u.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var coverTypeToBeDeleted = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeToBeDeleted == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(coverTypeToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
