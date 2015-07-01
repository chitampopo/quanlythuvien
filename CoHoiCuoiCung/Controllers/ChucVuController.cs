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
    public class ChucVuController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này truy cập đến trang xem các chức vụ trong thư viện
        // GET: /ChucVu/
        public ActionResult Index()
        {
            return View(db.CHUC_VU.ToList());
        }

        //Hàm này dùng để truy cập đến trang tạo chức vụ
        // GET: /ChucVu/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm chức vụ
        // POST: /ChucVu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_CHUC_VU,TEN_CHUC_VU")] CHUC_VU chuc_vu)
        {
            if (ModelState.IsValid)
            {
                db.CHUC_VU.Add(chuc_vu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chuc_vu);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa chức vụ
        // GET: /ChucVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUC_VU chuc_vu = db.CHUC_VU.Find(id);
            if (chuc_vu == null)
            {
                return HttpNotFound();
            }
            return View(chuc_vu);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin chức vụ
        // POST: /ChucVu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_CHUC_VU,TEN_CHUC_VU")] CHUC_VU chuc_vu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuc_vu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuc_vu);
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
