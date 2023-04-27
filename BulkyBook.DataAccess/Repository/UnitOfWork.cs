using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category=new CategoryRepository(_db);
            CoverType=new CoverTypeRepository(_db);
            Products=new ProductRepository(_db);
            Companies=new CompanyRepository(_db);
            ApplicationUsers=new ApplicationUserRepository(_db);
            ShoppingCarts=new ShoppingCartRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Products { get; private set; }

        public ICompanyRepository Companies { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public IShoppingCartRepository ShoppingCarts{get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
