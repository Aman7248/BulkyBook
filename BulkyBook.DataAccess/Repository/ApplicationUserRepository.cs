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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public ApplicationUserRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        /*public void Update(Category updatedCategory)
        {
            _appDbContext.Categories.Update(updatedCategory);
        }*/
    }
}
