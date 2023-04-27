using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkYBook.Models.ViewModels;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Products.GetAll(includeProperties: "Category,CoverType");
            return View(products);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new() { 
                Count = 1,
                ProductId = productId,
                Product =_unitOfWork.Products.GetFirstOrDefault(u=>u.id== productId, includeProperties:"Category,CoverType"),
            };
            return View(cartObj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            var shoppingCartFromDb = _unitOfWork.ShoppingCarts.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId
                );

            if (shoppingCartFromDb == null)
            {
                _unitOfWork.ShoppingCarts.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCarts.IncreamentCount(shoppingCartFromDb, shoppingCart.Count);
            }
            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}