using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class capacitacionesappsfitController : Controller
    {
        // GET: capacitacionesappsfit
        public ActionResult Index()
        {
            return View();
        }

        // GET: capacitacionesappsfit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: capacitacionesappsfit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: capacitacionesappsfit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: capacitacionesappsfit/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: capacitacionesappsfit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: capacitacionesappsfit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: capacitacionesappsfit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
