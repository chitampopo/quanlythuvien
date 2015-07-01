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
    public class TacGiaController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý thông tin tác giả
        // GET: /TacGia/
        public ActionResult Index()
        {
            return View(db.TAC_GIA.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin tác giả
        // GET: /TacGia/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin tác giả
        // POST: /TacGia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_TAC_GIA,TEN_TAC_GIA")] TAC_GIA tac_gia)
        {
            if (ModelState.IsValid)
            {
                db.TAC_GIA.Add(tac_gia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tac_gia);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin tác giả
        // GET: /TacGia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAC_GIA tac_gia = db.TAC_GIA.Find(id);
            if (tac_gia == null)
            {
                return HttpNotFound();
            }
            return View(tac_gia);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin tác giả
        // POST: /TacGia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_TAC_GIA,TEN_TAC_GIA")] TAC_GIA tac_gia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tac_gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tac_gia);
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
