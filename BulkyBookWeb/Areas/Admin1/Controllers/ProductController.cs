using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BulkyBookWeb.Areas.Admin1.Controllers
{
    [Area("Admin1")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index() 
        {
            IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }


        //get
        public IActionResult Upsert(int? id)
        {
            ProductVm productVm = new()
            {
                Product = new(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(
                    i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
                    i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            /*IEnumerable<SelectListItem> CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
                    u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString(),
               }
               );
            if (id == null || id == 0)
            {
                ViewData["CoverTypeList"] = CoverTypeList;
                return View(product);
            }*/

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategotyList = CategotyList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVm);
            }
            else
            {   
                //update
            }
                         
            return View();
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVm obj,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_UnitOfWork.CoverType.Update(obj);
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
