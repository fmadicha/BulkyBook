using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;  
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {   //Only inside the Repository is where we are supposed to use _db outside repository we should never use the it
            //_db.Products.Update(obj);   updates all properties even if not updated
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if(objFromDb!=null)  //then update properties
            {
                objFromDb.Title=obj.Title;
                objFromDb.ISBN =obj.ISBN;
                objFromDb.Price =obj.Price;
                objFromDb.Price50 =obj.Price50;
                objFromDb.Price100 =obj.Price100;
                objFromDb.ListPrice=obj.ListPrice;
                objFromDb.Description =obj.Description;
                objFromDb.Author=obj.Author;
                objFromDb.CoverTypeId=obj.CoverTypeId;
                objFromDb.Description=obj.Description;
                objFromDb.CategoryId=obj.CategoryId;
                if(obj.ImageUrl!=null)      //means u want to explicitly want to upload the url
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
