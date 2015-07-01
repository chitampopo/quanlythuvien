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
    public class DocGiaController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý độc giả
        // GET: /DocGia/
        public ActionResult Index()
        {
            var doc_gia = db.DOC_GIA.Include(d => d.DOI_TUONG);
            return View(doc_gia.ToList());
        }

        //Hàm này dùng để truy cập đến trang xem thông tin chi tiết của độc giả
        // GET: /DocGia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOC_GIA doc_gia = db.DOC_GIA.Find(id);
            if (doc_gia == null)
            {
                return HttpNotFound();
            }
            return View(doc_gia);
        }

        //Hàm này dùng để truy cập đến trang tạo độc giả mới
        // GET: /DocGia/Create
        public ActionResult Create()
        {
            ViewBag.MA_DOI_TUONG = new SelectList(db.DOI_TUONG, "MA_DOI_TUONG", "TEN_DOI_TUONG");
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin độc giả
        // POST: /DocGia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_DOC_GIA,MA_DOI_TUONG,TEN_DOC_GIA,CMND,SDT_DOC_GIA,DIA_CHI_DOC_GIA,EMAIL")] DOC_GIA doc_gia, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                //Ngày tham gia là ngày hiện tại
                DateTime ngayThamGia = DateTime.Now;
                //Ngày kết thúc được khởi tạo là ngayThamGia + 1 năm
                DateTime ngayKetThuc = ngayThamGia.AddYears(1);
                doc_gia.NGAY_THAM_GIA = ngayThamGia;
                doc_gia.NGAY_HET_HAN = ngayKetThuc;
                string gioiTinh = fc["gioiTinh"];
                if (gioiTinh == "1")
                {
                    doc_gia.GIOI_TINH_DOC_GIA = true;
                }
                else
                {
                    doc_gia.GIOI_TINH_DOC_GIA = false;
                }
                db.DOC_GIA.Add(doc_gia);
                //Lưu các thông tin của độc giả
                db.SaveChanges();
                //Tìm thông tin độc giả vừa mới lưu để thêm vào bảng TaiKhoan
                DOC_GIA docGia = db.DOC_GIA.Where(a => a.MA_DOI_TUONG == doc_gia.MA_DOI_TUONG).Where(a => a.TEN_DOC_GIA == doc_gia.TEN_DOC_GIA).Where(a => a.SDT_DOC_GIA == doc_gia.SDT_DOC_GIA).First();
                TAI_KHOAN taiKhoan = new TAI_KHOAN();
                taiKhoan.TEN_TAI_KHOAN = fc["tenTaiKhoan"];
                taiKhoan.MAT_KHAU = fc["matKhau"];
                taiKhoan.MA_DOC_GIA = docGia.MA_DOC_GIA;
                db.TAI_KHOAN.Add(taiKhoan);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MA_DOI_TUONG = new SelectList(db.DOI_TUONG, "MA_DOI_TUONG", "TEN_DOI_TUONG", doc_gia.MA_DOI_TUONG);
            return View(doc_gia);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin độc giả
        // GET: /DocGia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOC_GIA doc_gia = db.DOC_GIA.Find(id);
            if (doc_gia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DOI_TUONG = new SelectList(db.DOI_TUONG, "MA_DOI_TUONG", "TEN_DOI_TUONG", doc_gia.MA_DOI_TUONG);
            return View(doc_gia);
        }

        //Hàm này dùng để xử lý các thông tin chỉnh sửa độc giả
        // POST: /DocGia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_DOC_GIA,MA_DOI_TUONG,TEN_DOC_GIA,CMND,SDT_DOC_GIA,DIA_CHI_DOC_GIA,EMAIL,NGAY_THAM_GIA,NGAY_HET_HAN")] DOC_GIA doc_gia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doc_gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DOI_TUONG = new SelectList(db.DOI_TUONG, "MA_DOI_TUONG", "TEN_DOI_TUONG", doc_gia.MA_DOI_TUONG);
            return View(doc_gia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Hàm này dùng để gọi đến PartialView liệt kê độc giả
        //Được sử dụng ở View TimDocGia ở phần MuonTraSachController
        public PartialViewResult LietKeDocGia()
        {
            return PartialView(db.DOC_GIA.ToList());
        }

        //Hàm này dùng để gọi đến PartialView
        //Được sử dụng ở View TraSach của MuonTraSachController
        public PartialViewResult LietKeDocGia1()
        {
            return PartialView(db.DOC_GIA.ToList());
        }
    }
}
