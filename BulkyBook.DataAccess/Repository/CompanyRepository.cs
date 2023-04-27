using BulkyBook.DataAccess.Repository.IRepository;
using BulkYBook.Models;
using BulkyBookWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
    }
}
