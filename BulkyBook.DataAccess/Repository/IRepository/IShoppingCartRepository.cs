using BulkYBook.Models;
using BulkyBookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        /*void Update(Category updatedCategory);*/
        int IncreamentCount(ShoppingCart cart, int count);
        int DecrementCount(ShoppingCart cart,int count);
    }
}
