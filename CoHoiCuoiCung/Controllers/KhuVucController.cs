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
    public class KhuVucController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý khu vục
        // GET: /KhuVuc/
        public ActionResult Index()
        {
            return View(db.KHU_VUC.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin khu vực
        // GET: /KhuVuc/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thông tin thêm khu vực
        // POST: /KhuVuc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_KHU_VUC,TEN_KHU_VUC")] KHU_VUC khu_vuc)
        {
            if (ModelState.IsValid)
            {
                db.KHU_VUC.Add(khu_vuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khu_vuc);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin khu vực
        // GET: /KhuVuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHU_VUC khu_vuc = db.KHU_VUC.Find(id);
            if (khu_vuc == null)
            {
                return HttpNotFound();
            }
            return View(khu_vuc);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa khu vực
        // POST: /KhuVuc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_KHU_VUC,TEN_KHU_VUC")] KHU_VUC khu_vuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khu_vuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khu_vuc);
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
