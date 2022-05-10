using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;  
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }
    }
}
