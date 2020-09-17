using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab5_GalynaMatsygin.Models;

namespace Lab5_GalynaMatsygin.Controllers
{
    public class SalesController : Controller
    {
        private PetContext db = new PetContext();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Pet);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoldPet sold = db.SoldPets.Find(id);
            if (sold == null)
            {
                return HttpNotFound();
            }
            return View(sold);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.PetId = new SelectList(db.Pets, "Id", "Id");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,PetId,DateofSale")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                SoldPet sold = new SoldPet();

                Pet pet = db.Pets.Find(sale.PetId);
                if(pet!=null)
                {
                    sold.Id = pet.Id;
                    sold.CategoryId = pet.CategoryId;
                    sold.Gender = pet.Gender;
                    sold.Birthdate = pet.Birthdate;
                    sold.Price = pet.Price;


                    db.SoldPets.Add(sold);
                    db.Pets.Remove(pet);
                    
                }
               
               
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sale.CustomerId);
            ViewBag.PetId = new SelectList(db.Pets, "Id", "Id", sale.PetId);
            
            return View(sale);
        }

        // GET: Sales/Edit/5
        
 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
