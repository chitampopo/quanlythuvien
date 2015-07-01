using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoHoiCuoiCung.Models;

namespace CoHoiCuoiCung.Controllers
{
    public class SachMuonController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: /SachMuon/
        public ActionResult Index()
        {
            var sach_muon = db.SACH_MUON.Include(s => s.CUON_SACH).Include(s => s.MUON_TRA_SACH);
            return View(sach_muon.ToList());
        }

        // GET: /SachMuon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH_MUON sach_muon = db.SACH_MUON.Find(id);
            if (sach_muon == null)
            {
                return HttpNotFound();
            }
            return View(sach_muon);
        }

        // GET: /SachMuon/Create
        public ActionResult Create()
        {
            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH");
            ViewBag.MA_MUON_TRA = new SelectList(db.MUON_TRA_SACH, "MA_MUON_TRA", "MA_MUON_TRA");
            return View();
        }

        // POST: /SachMuon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_SACH,MA_MUON_TRA,NGAY_PHAI_TRA,DA_TRA")] SACH_MUON sach_muon)
        {
            if (ModelState.IsValid)
            {
                db.SACH_MUON.Add(sach_muon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH", sach_muon.MA_SACH);
            ViewBag.MA_MUON_TRA = new SelectList(db.MUON_TRA_SACH, "MA_MUON_TRA", "MA_MUON_TRA", sach_muon.MA_MUON_TRA);
            return View(sach_muon);
        }

        // GET: /SachMuon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH_MUON sach_muon = db.SACH_MUON.Find(id);
            if (sach_muon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH", sach_muon.MA_SACH);
            ViewBag.MA_MUON_TRA = new SelectList(db.MUON_TRA_SACH, "MA_MUON_TRA", "MA_MUON_TRA", sach_muon.MA_MUON_TRA);
            return View(sach_muon);
        }

        // POST: /SachMuon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_SACH,MA_MUON_TRA,NGAY_PHAI_TRA,DA_TRA")] SACH_MUON sach_muon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach_muon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH", sach_muon.MA_SACH);
            ViewBag.MA_MUON_TRA = new SelectList(db.MUON_TRA_SACH, "MA_MUON_TRA", "MA_MUON_TRA", sach_muon.MA_MUON_TRA);
            return View(sach_muon);
        }

        // GET: /SachMuon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH_MUON sach_muon = db.SACH_MUON.Find(id);
            if (sach_muon == null)
            {
                return HttpNotFound();
            }
            return View(sach_muon);
        }

        // POST: /SachMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH_MUON sach_muon = db.SACH_MUON.Find(id);
            db.SACH_MUON.Remove(sach_muon);
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
