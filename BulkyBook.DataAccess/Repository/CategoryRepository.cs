using BulkyBook.DataAccess.Repository.IRepository;
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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public void Update(Category updatedCategory)
        {
            _appDbContext.Categories.Update(updatedCategory);
        }
    }
}
