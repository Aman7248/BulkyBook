using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkyBookWeb;
using BulkyBookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int DecrementCount(ShoppingCart cart, int count)
        {
            cart.Count-=count;
            return cart.Count;
        }

        public int IncreamentCount(ShoppingCart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }


        /*public void Update(Category updatedCategory)
        {
            _appDbContext.Categories.Update(updatedCategory);
        }*/
    }
}
