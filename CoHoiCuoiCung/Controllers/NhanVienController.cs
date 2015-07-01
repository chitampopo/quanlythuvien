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
    public class NhanVienController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý thông tin nhân viên
        // GET: /NhanVien/
        public ActionResult Index()
        {
            return View(db.NHAN_VIEN.ToList());
        }

        //Hàm này dùng để truy cập đến trang xem thông tin chi tiết của 1 nhân viên
        // GET: /NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nhan_vien = db.NHAN_VIEN.Find(id);
            if (nhan_vien == null)
            {
                return HttpNotFound();
            }
            TAI_KHOAN taiKhoan = db.TAI_KHOAN.Where(a => a.MA_NHAN_VIEN == id).First();
            ViewData["tenTaiKhoan"] = taiKhoan.TEN_TAI_KHOAN;
            //Mật khẩu sẽ được hiển thị bằng các dấu *
            string maHoa = "";
            for (int i = 0; i < taiKhoan.MAT_KHAU.Count(); i++)
            {
                maHoa += "*";
            }
            
            ViewData["matKhau"] = maHoa;
            return View(nhan_vien);
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin nhân viên
        // GET: /NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        //Hàm này dùng để xử lý các thông tin thêm nhân viên
        // POST: /NhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_NHAN_VIEN,TEN_NHAN_VIEN,DIA_CHI_NHAN_VIEN,SDT_NHAN_VIEN")] NHAN_VIEN nhan_vien, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                string gioiTinh = fc["gioiTinh"];
                if (gioiTinh == "1")
                {
                    nhan_vien.GIOI_TINH_NHAN_VIEN = true;
                }
                else
                {
                    nhan_vien.GIOI_TINH_NHAN_VIEN = false;
                }
                db.NHAN_VIEN.Add(nhan_vien);
                db.SaveChanges();
                //Tìm nhân viên mới thêm
                NHAN_VIEN nhanVien = db.NHAN_VIEN.Where(a => a.TEN_NHAN_VIEN == nhan_vien.TEN_NHAN_VIEN).Where(a => a.SDT_NHAN_VIEN == nhan_vien.SDT_NHAN_VIEN).First();
                //Tạo tài khoản cho nhân viên này
                TAI_KHOAN taiKhoan = new TAI_KHOAN();
                taiKhoan.MA_NHAN_VIEN = nhanVien.MA_NHAN_VIEN;
                taiKhoan.TEN_TAI_KHOAN = fc["tenTaiKhoan"];
                taiKhoan.MAT_KHAU = fc["matKhau"];
                db.TAI_KHOAN.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhan_vien);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin nhân viên
        // GET: /NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nhan_vien = db.NHAN_VIEN.Find(id);
            if (nhan_vien == null)
            {
                return HttpNotFound();
            }
            TAI_KHOAN taiKhoan = db.TAI_KHOAN.Where(a => a.MA_NHAN_VIEN == id).First();
            ViewData["tenTaiKhoan"] = taiKhoan.TEN_TAI_KHOAN;
            ViewData["matKhau"] = taiKhoan.MAT_KHAU;
            return View(nhan_vien);
        }

        //Hàm này dùng để xử lý thông tin chỉnh sửa nhân viên
        // POST: /NhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_NHAN_VIEN,TEN_NHAN_VIEN,DIA_CHI_NHAN_VIEN,SDT_NHAN_VIEN")] NHAN_VIEN nhan_vien, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhan_vien).State = EntityState.Modified;
                db.SaveChanges();
                string matKhauMoi = fc["matKhauMoi"];
                if (matKhauMoi != null && matKhauMoi.Count() > 0)
                {
                    TAI_KHOAN taiKhoan = db.TAI_KHOAN.Where(a => a.MA_NHAN_VIEN == nhan_vien.MA_NHAN_VIEN).First();
                    taiKhoan.MAT_KHAU = matKhauMoi;
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(nhan_vien);
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
