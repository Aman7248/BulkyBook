using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkYBook.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View(_unitOfWork.Companies.GetAll());
        }
        //add+Update
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Company company = new();


            if (id == null || id == 0)
            {
                //create
                
                return View(company);
            }
            else
            {
                //update\
                company=_unitOfWork.Companies.GetFirstOrDefault(u=>u.Id==id);
                return View(company);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Company company, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                
                if (company.Id == 0)
                {
                    _unitOfWork.Companies.Add(company);
                    TempData["success"] = "Company created successfully";
                    
                }
                else
                {
                    _unitOfWork.Companies.Update(company);
                    TempData["success"] = "Company updated successfully";
                    
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");


            }
            return View(company);
        }
        

        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Companies.GetAll();
            return Json(new {data=companyList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var company = _unitOfWork.Companies.GetFirstOrDefault(u => u.Id == id);
            if(company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
            
            _unitOfWork.Companies.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
