using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.Repositories;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly RestSupplyDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public IngredientsController()
        {
            _context = new RestSupplyDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Ingredients
        public ActionResult Index()
        {
            return View(_unitOfWork.Ingredients.GetAll());
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id.Value);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var createdIngredient = new Ingredients
                {
                    Name = vm.Name,
                    Unit = vm.Unit
                };
                _unitOfWork.Ingredients.Add(createdIngredient);
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id.Value);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(ingredients).State = EntityState.Modified;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id.Value);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id);
            _unitOfWork.Ingredients.Remove(ingredients);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
