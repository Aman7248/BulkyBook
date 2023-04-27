using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private IUnitOfWork _unitOfWork; 

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categoryList=_unitOfWork.Category.GetAll();
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.Displayorder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var categoryFromDb = _appDbContext.Categories.Find(id);
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {
            if (updatedCategory.Name == updatedCategory.Displayorder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(updatedCategory);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }
        public IActionResult Delete(int? id)
        {
            //var categoryFromDb = _appDbContext.Categories.Find(id);
            var categoryFromDb =_unitOfWork.Category.GetFirstOrDefault(u=>u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var categoryToBeDeleted = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryToBeDeleted == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categoryToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
