using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin1.Controllers
{
    [Area("Admin1")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _UnitOfWork.Category.GetAll();
            return View(objCategoryList);
        }


        //get
        public IActionResult Create()
        {
            return View();
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "the Display Order can't match the name.");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CategoryFromDb = _db.Categories.Find(id);
            var CategoryFromDb = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            /*var CategoryFromDb= _db.Categories.FirstOrDefault(c => c.Id == id);*/
            if (CategoryFromDb == null)
            {
                return null;
            }
            return View(CategoryFromDb);
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "the Display Order can't match the name.");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
            var CategoryFromDb = _UnitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _UnitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
            return RedirectToAction("Index");

            return View(obj);
        }
    }
}
