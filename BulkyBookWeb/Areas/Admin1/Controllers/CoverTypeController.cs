using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;




namespace BulkyBookWeb.Areas.Admin1.Controllers
{
    [Area("Admin1")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CoverTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index() 
        {
            IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }


        //get
        public IActionResult Create()
        {
            return View();
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(obj);
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
            
            var CoverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            
            if (CoverTypeFromDb == null)
            {
                return null;
            }
            return View(CoverTypeFromDb);
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Update(obj);
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
            var CoverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.CoverType.Remove(obj);
            _UnitOfWork.Save();
            return RedirectToAction("Index");

            return View(obj);
        }
    }
}
