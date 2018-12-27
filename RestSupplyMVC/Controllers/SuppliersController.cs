using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.Repositories;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly RestSupplyDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public SuppliersController()
        {
            _context = new RestSupplyDbContext();
            _unitOfWork = new UnitOfWork(_context);
            //... you can init other repos in the UoW c-tor
        }

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(_unitOfWork.Suppliers.GetAll());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _unitOfWork.Suppliers.GetById(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //[Authorize]
        public ActionResult Create()
        {
            var viewModel = new SupplierViewModel
            {
                IngredientList = _unitOfWork.Ingredients.GetAll()
            };
            return View(viewModel);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    //IngredientsSet = new List<Ingredients>() TODO
                    Phone = viewModel.Phone
                };

                _unitOfWork.Suppliers.Add(supplier);
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _context.SuppliersSet.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(supplier).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _context.SuppliersSet.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = _context.SuppliersSet.Find(id);
            _context.SuppliersSet.Remove(supplier);
            _context.SaveChanges();
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
