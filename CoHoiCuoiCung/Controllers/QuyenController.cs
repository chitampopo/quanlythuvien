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
    public class QuyenController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý thông tin quyền sử dụng các menu
        // GET: /Quyen/
        public ActionResult Index()
        {
            return View(db.QUYENs.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm quyền sử dụng menu
        // GET: /Quyen/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin quyền
        // POST: /Quyen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_QUYEN,TEN_QUYEN")] QUYEN quyen)
        {
            if (ModelState.IsValid)
            {
                db.QUYENs.Add(quyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quyen);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin quyền
        // GET: /Quyen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYEN quyen = db.QUYENs.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa quyền
        // POST: /Quyen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_QUYEN,TEN_QUYEN")] QUYEN quyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quyen);
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
