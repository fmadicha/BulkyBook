using BulkyBookDataAccess;

using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using BulkyBook.Utility;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= SD.Role_Admin)]
    public class CategoryController : Controller
    {
        //no need to create object tell appdbcontext to sent obj coz of DInjection
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork =unitOfWork;
        }

        public IActionResult Index()
        { // goes to db to retrieve categories and convert them to list
          IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cross site request forgery attack, inject a key in the form
        public IActionResult Create(Category obj)
        {if(obj.Name== obj.Display.ToString()){
                ModelState.AddModelError("CustomError", "The Display order can not match exactly the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj); //add to db
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        { 
            if(id==null || id== 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u=> u.Id ==id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.Display.ToString())
            {
                ModelState.AddModelError("name", "The Display order can not match exactly the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj); //add to db
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
          
        }

    }
}
