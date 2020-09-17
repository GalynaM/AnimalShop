using Lab5_GalynaMatsygin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab5_GalynaMatsygin.Controllers
{
    public class SoldController : Controller
    {
        private PetContext db = new PetContext();
        // GET: Sold
        public ActionResult Index()
        {
            return View(db.SoldPets.ToList());
        }
    }
}