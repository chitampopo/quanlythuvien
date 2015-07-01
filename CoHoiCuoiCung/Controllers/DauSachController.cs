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
    public class DauSachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý đầu sách
        // GET: /DauSach/
        public ActionResult Index()
        {
            var dau_sach = db.DAU_SACH.Include(d => d.NGON_NGU).Include(d => d.NHA_XUAT_BAN).Include(d => d.LOAI_SACH);
            return View(dau_sach.ToList());
        }

        //Hàm này dùng để truy cập đến trang tạo đầu sách mới
        //Bao gồm cả thông tin của các tác giả của đầu sách
        // GET: /DauSach/Create
        public ActionResult Create()
        {
            ViewBag.MA_NGON_NGU = new SelectList(db.NGON_NGU, "MA_NGON_NGU", "TEN_NGON_NGU");
            ViewBag.MA_NXB = new SelectList(db.NHA_XUAT_BAN, "MA_NXB", "TEN_NXB");
            ViewBag.MA_LOAI_SACH = new SelectList(db.LOAI_SACH, "MA_LOAI_SACH", "TEN_LOAI_SACH");
            ViewBag.listTacGia = db.TAC_GIA.ToList();
            return View();
        }

        //Hàm này dùng để xử lý thông tin tạo đầu sách mới
        // POST: /DauSach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_DAU_SACH,MA_NGON_NGU,MA_NXB,MA_LOAI_SACH,TEN_DAU_SACH")] DAU_SACH dau_sach, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                //Lưu vào bảng Dau_Sach
                db.DAU_SACH.Add(dau_sach);
                //Xử lý xử vào bảng trung gian giữa bảng Dau_Sach và bảng Tac_gia
                //có quan hệ nhiều - nhiều
                List<TAC_GIA> listTacGia = db.TAC_GIA.ToList();
                for (int i = 0; i < listTacGia.Count; i++)
                {
                    //Lấy dữ liệu checkbox có name là TG.x trong đó x là mã tác giả
                    string check = fc["TG." + listTacGia.ElementAt(i).MA_TAC_GIA];
                    //Nếu được check thì sẽ lưu vào bảng trung giang
                    if (check == "on")
                    {
                        dau_sach.TAC_GIA.Add(listTacGia.ElementAt(i));
                        listTacGia.ElementAt(i).DAU_SACH.Add(dau_sach);
                    }
                }
                //Lưu các thay đổi
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_NGON_NGU = new SelectList(db.NGON_NGU, "MA_NGON_NGU", "TEN_NGON_NGU", dau_sach.MA_NGON_NGU);
            ViewBag.MA_NXB = new SelectList(db.NHA_XUAT_BAN, "MA_NXB", "TEN_NXB", dau_sach.MA_NXB);
            ViewBag.MA_LOAI_SACH = new SelectList(db.LOAI_SACH, "MA_LOAI_SACH", "TEN_LOAI_SACH", dau_sach.MA_LOAI_SACH);


            return View(dau_sach);
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
