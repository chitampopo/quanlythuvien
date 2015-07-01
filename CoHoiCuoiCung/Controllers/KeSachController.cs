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
    public class KeSachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quảng lý kệ sách
        // GET: /KeSach/
        public ActionResult Index()
        {
            var ke_sach = db.KE_SACH.Include(k => k.KHU_VUC);
            return View(ke_sach.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm kệ sách
        // GET: /KeSach/Create
        public ActionResult Create()
        {
            ViewBag.MA_KHU_VUC = new SelectList(db.KHU_VUC, "MA_KHU_VUC", "TEN_KHU_VUC");
            return View();
        }

        //Hàm này dùng để xử lý thông tin thêm kệ sách
        // POST: /KeSach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_KE_SACH,MA_KHU_VUC,TEN_KE_SACH")] KE_SACH ke_sach)
        {
            if (ModelState.IsValid)
            {
                db.KE_SACH.Add(ke_sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_KHU_VUC = new SelectList(db.KHU_VUC, "MA_KHU_VUC", "TEN_KHU_VUC", ke_sach.MA_KHU_VUC);
            return View(ke_sach);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin kệ sách
        // GET: /KeSach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KE_SACH ke_sach = db.KE_SACH.Find(id);
            if (ke_sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KHU_VUC = new SelectList(db.KHU_VUC, "MA_KHU_VUC", "TEN_KHU_VUC", ke_sach.MA_KHU_VUC);
            return View(ke_sach);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin kệ sách
        // POST: /KeSach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_KE_SACH,MA_KHU_VUC,TEN_KE_SACH")] KE_SACH ke_sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ke_sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_KHU_VUC = new SelectList(db.KHU_VUC, "MA_KHU_VUC", "TEN_KHU_VUC", ke_sach.MA_KHU_VUC);
            return View(ke_sach);
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
