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
    public class QuyenSuDungController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý cấp quyền sử dụng menu cho 1 chức vụ nào đó
        // GET: /QuyenSuDung/
        public ActionResult Index()
        {
            var quyen_sd = db.QUYEN_SD.Include(q => q.CHUC_VU).Include(q => q.QUYEN);
            return View(quyen_sd.ToList());
        }


        //Hàm này dùng để truy cập đến trang thêm quyền sử dụng menu cho 1 chức vụ nào đó
        // GET: /QuyenSuDung/Create
        public ActionResult Create()
        {
            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU");
            ViewBag.MA_QUYEN = new SelectList(db.QUYENs, "MA_QUYEN", "TEN_QUYEN");
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin quyền sử dụng menu cho 1 chức vụ nào đó
        // POST: /QuyenSuDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_QUYEN,MA_CHUC_VU,MENU")] QUYEN_SD quyen_sd)
        {
            if (ModelState.IsValid)
            {
                db.QUYEN_SD.Add(quyen_sd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU", quyen_sd.MA_CHUC_VU);
            ViewBag.MA_QUYEN = new SelectList(db.QUYENs, "MA_QUYEN", "TEN_QUYEN", quyen_sd.MA_QUYEN);
            return View(quyen_sd);
        }

        //Hàm này dùng để truy cập đến trang xóa quyền sử dụng menu của 1 chức vụ nào đó
        // GET: /QuyenSuDung/Delete/5
        public ActionResult Delete(int maQuyen, int maCV)
        {
            if (maQuyen == null || maCV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYEN_SD quyen_sd = db.QUYEN_SD.Where(a => a.MA_QUYEN == maQuyen).Where(a => a.MA_CHUC_VU == maCV).First();
            if (quyen_sd == null)
            {
                return HttpNotFound();
            }
            return View(quyen_sd);
        }

        //Hàm này dùng để xử lý xóa quyền sử dụng menu của 1 chứ cvu5 nào đó
        // POST: /QuyenSuDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int maQuyen, int maCV)
        {
            QUYEN_SD quyen_sd = db.QUYEN_SD.Where(a => a.MA_QUYEN == maQuyen).Where(a => a.MA_CHUC_VU == maCV).First();
            db.QUYEN_SD.Remove(quyen_sd);
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
