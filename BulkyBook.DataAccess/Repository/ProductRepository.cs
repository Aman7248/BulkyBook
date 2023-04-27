using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkyBookWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            //this way even if we update all the properties
            //_db.Products.Update(product);

            var productFromDb = _db.Products.Find(product.id);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.ListPrice=product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.Price50=product.Price50;
                productFromDb.Price100=product.Price100;
                productFromDb.Author = product.Author;
                productFromDb.ISBN= product.ISBN;
                productFromDb.CoverTypeId = product.CoverTypeId;
                if (product.ImageUrl != null)
                {
                    productFromDb.ImageUrl= product.ImageUrl;
                }
            }
        }
    }
}
