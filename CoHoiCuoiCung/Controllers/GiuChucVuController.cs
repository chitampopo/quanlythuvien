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
    public class GiuChucVuController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này truy cập đến trang quản lý giữ chức vụ của nhân viên
        // GET: /GiuChucVu/
        public ActionResult Index()
        {
            var giu_chuc_vu = db.GIU_CHUC_VU.Include(g => g.CHUC_VU).Include(g => g.NHAN_VIEN);
            return View(giu_chuc_vu.ToList());
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin giữ chức vụ
        // GET: /GiuChucVu/Create
        public ActionResult Create()
        {
            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU");
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN");
            return View();
        }

        //Hàm này dùng để xử lý các thông tin thêm giữ chức vụ
        // POST: /GiuChucVu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_NHAN_VIEN,MA_CHUC_VU,NGAY_BAT_DAU,NGAY_KET_THUC")] GIU_CHUC_VU giu_chuc_vu)
        {
            if (ModelState.IsValid)
            {
                db.GIU_CHUC_VU.Add(giu_chuc_vu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU", giu_chuc_vu.MA_CHUC_VU);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", giu_chuc_vu.MA_NHAN_VIEN);
            return View(giu_chuc_vu);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin chức vụ
        // GET: /GiuChucVu/Edit/5
        public ActionResult Edit(int maCV, int maNV)
        {
            if (maCV == null || maNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIU_CHUC_VU giu_chuc_vu = db.GIU_CHUC_VU.Where(a => a.MA_CHUC_VU == maCV).Where(a => a.MA_NHAN_VIEN == maNV).First();
            if (giu_chuc_vu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU", giu_chuc_vu.MA_CHUC_VU);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", giu_chuc_vu.MA_NHAN_VIEN);
            return View(giu_chuc_vu);
        }

        //Hàm này dùng để xử lý các thông tin chỉnh sửa chức vụ
        // POST: /GiuChucVu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_NHAN_VIEN,MA_CHUC_VU,NGAY_BAT_DAU,NGAY_KET_THUC")] GIU_CHUC_VU giu_chuc_vu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giu_chuc_vu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_CHUC_VU = new SelectList(db.CHUC_VU, "MA_CHUC_VU", "TEN_CHUC_VU", giu_chuc_vu.MA_CHUC_VU);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", giu_chuc_vu.MA_NHAN_VIEN);
            return View(giu_chuc_vu);
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
