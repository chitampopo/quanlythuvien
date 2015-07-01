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
    public class NhaXuatBanController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý thông tin các nhà xuất bản
        // GET: /NhaXuatBan/
        public ActionResult Index()
        {
            return View(db.NHA_XUAT_BAN.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin nhà xuất bản
        // GET: /NhaXuatBan/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử  lý thông tin thêm nhà xuất bản
        // POST: /NhaXuatBan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_NXB,TEN_NXB")] NHA_XUAT_BAN nha_xuat_ban)
        {
            if (ModelState.IsValid)
            {
                db.NHA_XUAT_BAN.Add(nha_xuat_ban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nha_xuat_ban);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin nhà xuất bản
        // GET: /NhaXuatBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_XUAT_BAN nha_xuat_ban = db.NHA_XUAT_BAN.Find(id);
            if (nha_xuat_ban == null)
            {
                return HttpNotFound();
            }
            return View(nha_xuat_ban);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa nhà xuất bản
        // POST: /NhaXuatBan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_NXB,TEN_NXB")] NHA_XUAT_BAN nha_xuat_ban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nha_xuat_ban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nha_xuat_ban);
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
