using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoHoiCuoiCung.Controllers
{
    public class MuonSachController : Controller
    {
        //
        // GET: /MuonSach/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MuonSach/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MuonSach/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MuonSach/Create
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

        //
        // GET: /MuonSach/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MuonSach/Edit/5
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

        //
        // GET: /MuonSach/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MuonSach/Delete/5
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
