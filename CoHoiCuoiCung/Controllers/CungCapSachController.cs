using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoHoiCuoiCung.Models;
using CoHoiCuoiCung.Controllers;

namespace CoHoiCuoiCung.Controllers
{
    public class CungCapSachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang cung cấp sách
        // GET: /CungCapSach/
        public ActionResult Index()
        {
            var cung_cap_sach = db.CUNG_CAP_SACH.Include(c => c.CUON_SACH).Include(c => c.NHA_CUNG_CAP);
            return View(cung_cap_sach.ToList());
        }

        //Hàm này truy cập đến trang tạo mới thông tin cung cấp sách
        // GET: /CungCapSach/Create
        public ActionResult Create()
        {
            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH");
            ViewBag.MA_NCC = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC");
            return View();
        }

        //Hàm này dùng để xử lý thông tin cung cấp sách
        // POST: /CungCapSach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MA_NCC,MA_SACH,THOI_GIAN,SOLUONG")] CUNG_CAP_SACH cung_cap_sach)
        {
            if (ModelState.IsValid)
            {
                //Lưu thông tin cung cấp sách vào bảng CungCapSach
                db.CUNG_CAP_SACH.Add(cung_cap_sach);
                //Tăng số lượng sách trong bảng Cuon_Sach
                CUON_SACH cuonSach = db.CUON_SACH.Where(a => a.MA_SACH == cung_cap_sach.MA_SACH).First();
                cuonSach.SO_LUONG = cuonSach.SO_LUONG + cung_cap_sach.SOLUONG;
                db.Entry(cuonSach).State = EntityState.Modified;
                //Lưu các thay đổi
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_SACH = new SelectList(db.CUON_SACH, "MA_SACH", "TEN_SACH", cung_cap_sach.MA_SACH);
            ViewBag.MA_NCC = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC", cung_cap_sach.MA_NCC);

            return View(cung_cap_sach);
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
