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
    public class DanhMucController : Controller
    {
        public QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();
        //Hàm này gọi các số lượng các danh mục để hiển thị vào trang danh mục
        // GET: /DanhMuc/
        public ActionResult Index()
        {
            ViewBag.soLuongChinhSach = db.CHINH_SACH_LUU_THONG.ToList().Count();
            ViewBag.soLuongKhuVuc = db.KHU_VUC.Count();
            ViewBag.soLuongKeSach = db.KE_SACH.Count();
            ViewBag.soLuongLoaiSach = db.LOAI_SACH.Count();
            ViewBag.soLuongDauSach = db.DAU_SACH.Count();
            ViewBag.soLuongTacGia = db.TAC_GIA.Count();
            ViewBag.soLuongSach = db.CUON_SACH.Count();
            ViewBag.soLuongDoiTuong = db.DOI_TUONG.Count();
            ViewBag.soLuongChucVu = db.CHUC_VU.Count();
            ViewBag.soLuongNXB = db.NHA_XUAT_BAN.Count();
            ViewBag.soLuongNgonNgu = db.NGON_NGU.Count();
            ViewBag.soLuongNCC = db.NHA_CUNG_CAP.Count();
            ViewBag.soLuongQuyen = db.QUYENs.Count();
            return View();
        }


    }
}