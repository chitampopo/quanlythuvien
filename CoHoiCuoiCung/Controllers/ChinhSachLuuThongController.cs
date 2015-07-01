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
    public class ChinhSachLuuThongController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang hiển thị các chính sách lưu thông trong thư viện
        // GET: /ChinhSachLuuThong/
        public ActionResult Index()
        {
            return View(db.CHINH_SACH_LUU_THONG.ToList());
        }

        ///Hàm này dùng để truy cập đến trang tạo chính sách lưu thông
        // GET: /ChinhSachLuuThong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ChinhSachLuuThong/Create
        // Hàm này dùng để xử lý thêm chính sách lưu thông
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_CHINH_SACH,SO_SACH_MUON_TOI_DA,SO_NGAY_MUON,SO_LAN_DUOC_GIA_HAN,SO_NGAY_GIA_HAN")] CHINH_SACH_LUU_THONG chinh_sach_luu_thong)
        {
            if (ModelState.IsValid)
            {
                db.CHINH_SACH_LUU_THONG.Add(chinh_sach_luu_thong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chinh_sach_luu_thong);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa chính sách lưu thông
        // GET: /ChinhSachLuuThong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHINH_SACH_LUU_THONG chinh_sach_luu_thong = db.CHINH_SACH_LUU_THONG.Find(id);
            if (chinh_sach_luu_thong == null)
            {
                return HttpNotFound();
            }
            return View(chinh_sach_luu_thong);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin chính sách lưu thông
        // POST: /ChinhSachLuuThong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MA_CHINH_SACH,SO_SACH_MUON_TOI_DA,SO_NGAY_MUON,SO_LAN_DUOC_GIA_HAN,SO_NGAY_GIA_HAN")] CHINH_SACH_LUU_THONG chinh_sach_luu_thong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chinh_sach_luu_thong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chinh_sach_luu_thong);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Hàm này được sử dụng bởi độc giả
        //Hiển thỉ các thông tin lưu thống sách trong thư viện cho độc giả biết
        public ActionResult Xem()
        {
            CHINH_SACH_LUU_THONG chinhSach = db.CHINH_SACH_LUU_THONG.ToList().Last();
            ViewData["chinhSach"] = chinhSach;
            return View();
        }
    }
}
