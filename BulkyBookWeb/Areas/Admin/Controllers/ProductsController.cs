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
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            return View(_unitOfWork.Products.GetAll());
        }
        //add+Update
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })

            };


            if (id == null || id == 0)
            {
                //create
                /*ViewBag.categoryList = categoryList;
                ViewData["coverTypeList"] = coverTypeList;*/
                return View(productViewModel);
            }
            else
            {
                //update\
                productViewModel.Product=_unitOfWork.Products.GetFirstOrDefault(u=>u.id==id);
                return View(productViewModel);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                string webRootPath=_webHostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string fileName=Guid.NewGuid().ToString();
                    string uploads = Path.Combine(webRootPath, @"Images\Products");
                    var extension =Path.GetExtension(formFile.FileName);

                    if (productViewModel.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (FileStream fs = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        formFile.CopyTo(fs);
                    }
                    productViewModel.Product.ImageUrl = @"\Images\Products\" + fileName + extension;  
                }
                if (productViewModel.Product.id == 0)
                {
                    _unitOfWork.Products.Add(productViewModel.Product);
                    TempData["success"] = "Product created successfully";
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.Products.Update(productViewModel.Product);
                    TempData["success"] = "Product updated successfully";
                    _unitOfWork.Save();
                }
                return RedirectToAction("Index");


            }
            return View(productViewModel);
        }
        /*[HttpGet]
        public IActionResult Delete(int? id)
        {
            var product=_unitOfWork.Products.GetFirstOrDefault(u=>u.id== id);
            ProductViewModel productViewModel = new()
            {
                Product = product,
                CategoryList=
            };
            return View(product);
        }*/

        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Products.GetAll(includeProperties:"Category,CoverType");
            return Json(new {data=productList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.Products.GetFirstOrDefault(u => u.id == id);
            if(product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Products.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
