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
    public class DoiTuongController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý đối tượng độc giả
        // GET: /DoiTuong/
        public ActionResult Index()
        {
            return View(db.DOI_TUONG.ToList());
        }

        //Hàm này dùng để truy cập đến trang tạo thêm đối tượng độc giả
        // GET: /DoiTuong/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm đối tượng độc giả
        // POST: /DoiTuong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_DOI_TUONG,TEN_DOI_TUONG")] DOI_TUONG doi_tuong)
        {
            if (ModelState.IsValid)
            {
                db.DOI_TUONG.Add(doi_tuong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doi_tuong);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin độc giả
        // GET: /DoiTuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOI_TUONG doi_tuong = db.DOI_TUONG.Find(id);
            if (doi_tuong == null)
            {
                return HttpNotFound();
            }
            return View(doi_tuong);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin độc giả
        // POST: /DoiTuong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_DOI_TUONG,TEN_DOI_TUONG")] DOI_TUONG doi_tuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doi_tuong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doi_tuong);
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
