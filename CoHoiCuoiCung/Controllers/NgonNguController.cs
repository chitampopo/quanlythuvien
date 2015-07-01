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
    public class NgonNguController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý ngôn ngữ
        // GET: /NgonNgu/
        public ActionResult Index()
        {
            return View(db.NGON_NGU.ToList());
        }


        //Hàm này dùng để truy cập đến trang thêm ngôn ngữ
        // GET: /NgonNgu/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý thêm ngôn ngữ
        // POST: /NgonNgu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_NGON_NGU,TEN_NGON_NGU")] NGON_NGU ngon_ngu)
        {
            if (ModelState.IsValid)
            {
                db.NGON_NGU.Add(ngon_ngu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ngon_ngu);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin ngôn ngữ
        // GET: /NgonNgu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGON_NGU ngon_ngu = db.NGON_NGU.Find(id);
            if (ngon_ngu == null)
            {
                return HttpNotFound();
            }
            return View(ngon_ngu);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa ngôn ngữ
        // POST: /NgonNgu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_NGON_NGU,TEN_NGON_NGU")] NGON_NGU ngon_ngu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ngon_ngu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ngon_ngu);
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
