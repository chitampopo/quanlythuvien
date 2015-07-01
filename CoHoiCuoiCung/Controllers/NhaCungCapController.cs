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
    public class NhaCungCapController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý nhà cung cấp
        // GET: /NhaCungCap/
        public ActionResult Index()
        {
            return View(db.NHA_CUNG_CAP.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin nhà cung cấp
        // GET: /NhaCungCap/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin nhà cung cấp
        // POST: /NhaCungCap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_NCC,TEN_NCC")] NHA_CUNG_CAP nha_cung_cap)
        {
            if (ModelState.IsValid)
            {
                db.NHA_CUNG_CAP.Add(nha_cung_cap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nha_cung_cap);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin nhà cung cấp
        // GET: /NhaCungCap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_CUNG_CAP nha_cung_cap = db.NHA_CUNG_CAP.Find(id);
            if (nha_cung_cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_cung_cap);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa nhà cung cấp
        // POST: /NhaCungCap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_NCC,TEN_NCC")] NHA_CUNG_CAP nha_cung_cap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nha_cung_cap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nha_cung_cap);
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
