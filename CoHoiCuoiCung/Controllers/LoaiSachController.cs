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
    public class LoaiSachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý loại sách
        // GET: /LoaiSach/
        public ActionResult Index()
        {
            return View(db.LOAI_SACH.ToList());
        }

        ////Hàm này dùng để truy cập đến trang thêm thông tin loại sách
        // GET: /LoaiSach/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin loại sách
        // POST: /LoaiSach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_LOAI_SACH,TEN_LOAI_SACH")] LOAI_SACH loai_sach)
        {
            if (ModelState.IsValid)
            {
                db.LOAI_SACH.Add(loai_sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loai_sach);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin loại sách
        // GET: /LoaiSach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI_SACH loai_sach = db.LOAI_SACH.Find(id);
            if (loai_sach == null)
            {
                return HttpNotFound();
            }
            return View(loai_sach);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin loại sách
        // POST: /LoaiSach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_LOAI_SACH,TEN_LOAI_SACH")] LOAI_SACH loai_sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loai_sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai_sach);
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
