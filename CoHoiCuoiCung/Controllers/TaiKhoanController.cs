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
    public class TaiKhoanController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý tài khoản
        // GET: /TaiKhoan/
        public ActionResult Index()
        {
            var tai_khoan = db.TAI_KHOAN.Include(t => t.DOC_GIA).Include(t => t.NHAN_VIEN);
            return View(tai_khoan.ToList());
        }

        //Hàm này dùng để truy cập đến trang xem chi tiết tài khoản
        // GET: /TaiKhoan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tai_khoan = db.TAI_KHOAN.Find(id);
            if (tai_khoan == null)
            {
                return HttpNotFound();
            }
            return View(tai_khoan);
        }

        //Hàm này dùng để truy cập đến trang thêm thông tin tài khoản
        // GET: /TaiKhoan/Create
        public ActionResult Create()
        {
            ViewBag.MA_DOC_GIA = new SelectList(db.DOC_GIA, "MA_DOC_GIA", "TEN_DOC_GIA");
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN");
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tin tài khoản
        // POST: /TaiKhoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_TAI_KHOAN,MA_NHAN_VIEN,MA_DOC_GIA,TEN_TAI_KHOAN,MAT_KHAU")] TAI_KHOAN tai_khoan)
        {
            if (ModelState.IsValid)
            {
                db.TAI_KHOAN.Add(tai_khoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_DOC_GIA = new SelectList(db.DOC_GIA, "MA_DOC_GIA", "TEN_DOC_GIA", tai_khoan.MA_DOC_GIA);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", tai_khoan.MA_NHAN_VIEN);
            return View(tai_khoan);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin tải khoản
        // GET: /TaiKhoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tai_khoan = db.TAI_KHOAN.Find(id);
            if (tai_khoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DOC_GIA = new SelectList(db.DOC_GIA, "MA_DOC_GIA", "TEN_DOC_GIA", tai_khoan.MA_DOC_GIA);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", tai_khoan.MA_NHAN_VIEN);
            return View(tai_khoan);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin tài khoản
        // POST: /TaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_TAI_KHOAN,MA_NHAN_VIEN,MA_DOC_GIA,TEN_TAI_KHOAN,MAT_KHAU")] TAI_KHOAN tai_khoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tai_khoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DOC_GIA = new SelectList(db.DOC_GIA, "MA_DOC_GIA", "TEN_DOC_GIA", tai_khoan.MA_DOC_GIA);
            ViewBag.MA_NHAN_VIEN = new SelectList(db.NHAN_VIEN, "MA_NHAN_VIEN", "TEN_NHAN_VIEN", tai_khoan.MA_NHAN_VIEN);
            return View(tai_khoan);
        }

        // GET: /TaiKhoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tai_khoan = db.TAI_KHOAN.Find(id);
            if (tai_khoan == null)
            {
                return HttpNotFound();
            }
            return View(tai_khoan);
        }

        // POST: /TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAI_KHOAN tai_khoan = db.TAI_KHOAN.Find(id);
            db.TAI_KHOAN.Remove(tai_khoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Hàm này dùng để truy cập đến trang đăng nhập
        //Đây là URL chính
        public ActionResult DangNhap()
        {
            return View();
        }

        //Hàm này dùng để xử lý thông tin đăng nhập
        [HttpPost]
        public ActionResult DangNhap(FormCollection fc)
        {
            string tenTaiKhoan = fc["tenTaiKhoan"];
            string matKhau = fc["matKhau"];
            var kiemTra = db.TAI_KHOAN.Where(a => a.TEN_TAI_KHOAN == tenTaiKhoan).Where(a => a.MAT_KHAU == matKhau).ToList();
            var taiKhoan = new TAI_KHOAN();
            if (kiemTra.Count == 1)
            {
                taiKhoan = kiemTra.ElementAt(0);
            }
            //Nếu thông tin đăng nhập là chính xác và là nhân viên
            if (kiemTra.Count == 1 && taiKhoan.MA_DOC_GIA == null)
            {
                Session.Add("maTaiKhoan", taiKhoan.MA_NHAN_VIEN);
                Session.Add("loaiTaiKhoan", "NHANVIEN");
                NHAN_VIEN nhanVien = db.NHAN_VIEN.Where(a => a.MA_NHAN_VIEN == taiKhoan.MA_NHAN_VIEN).First();
                ViewData["tenNhanVien"] = nhanVien.TEN_NHAN_VIEN;
                ViewData["sdtNhanVien"] = nhanVien.SDT_NHAN_VIEN;
                ViewData["diaChiNhanVien"] = nhanVien.DIA_CHI_NHAN_VIEN;
                return View("TrangQuanTri");
            } // Ngược lại nếu thông tin đăng nhập là dúng và là độc giả
            else if (kiemTra.Count == 1 && taiKhoan.MA_NHAN_VIEN == null)
            {
                Session.Add("maTaiKhoan", taiKhoan.MA_DOC_GIA);
                Session.Add("loaiTaiKhoan", "DOCGIA");
                var listDocGia = db.DOC_GIA.Where(a => a.MA_DOC_GIA == taiKhoan.MA_DOC_GIA).ToList();
                DOC_GIA docGia = listDocGia.ElementAt(0);
                ViewData["maDocGia"] = docGia.MA_DOC_GIA;
                ViewData["tenDocGia"] = docGia.TEN_DOC_GIA;
                ViewData["sdtDocGia"] = docGia.SDT_DOC_GIA;
                ViewData["email"] = docGia.EMAIL;
                ViewData["diaChiDocGia"] = docGia.DIA_CHI_DOC_GIA;
                ViewData["ngayThamGia"] = docGia.NGAY_THAM_GIA;
                ViewData["ngayHetHan"] = docGia.NGAY_HET_HAN;
                ViewData["tenDoiTuong"] = docGia.DOI_TUONG.TEN_DOI_TUONG;
                return View("TrangDocGia", docGia);
            }
            else // ngược lại thông báo đăng nhập thất bại
            {
                ViewBag.thongBao = "Đăng nhập thất bại! Xin thử lại";
                return View();
            }
        }
        //Hàm này dùng để đăng xuất
        //Xóa hết các session
        public ActionResult DangXuat()
        {
            Session.Remove("maTaiKhoan");
            Session.Remove("loaiTaiKhoan");
            return RedirectToAction("DangNhap");
        }

        //Hàm này dùng để remove các menu không được cấp quyền sử dụng ở tài khoảng đó
        //đồng thời kiểm tra đăng nhập vào hệ thống hay chưa
        public PartialViewResult Menu()
        {
            int maTaiKhoan = 0;
            if (Session["maTaiKhoan"] == null)
            {
                //maTaiKhoan = 3;
                return PartialView("TraVeTrangDangNhap");
            }
            else
            {
                maTaiKhoan = Int32.Parse(Session["maTaiKhoan"].ToString());
            }
            GIU_CHUC_VU giuCV = db.GIU_CHUC_VU.Where(a => a.MA_NHAN_VIEN == maTaiKhoan).Where(a => a.NGAY_BAT_DAU < DateTime.Now).Where(a => a.NGAY_KET_THUC > DateTime.Now).First();
            string tenChucVu = giuCV.CHUC_VU.TEN_CHUC_VU;
            List<QUYEN_SD> listQSD1 = db.QUYEN_SD.Include(a => a.CHUC_VU).Where(a => a.MA_CHUC_VU == giuCV.MA_CHUC_VU).ToList();
            List<string> listQSD = new List<string>();
            for (int i = 0; i < listQSD1.Count; i++)
            {
                string temp = listQSD1.ElementAt(i).MENU;
                listQSD.Add(temp);
            }
            ViewBag.maTaiKhoan = maTaiKhoan;
            ViewBag.tenChucVu = tenChucVu;
            ViewBag.listQSD = listQSD;
            return PartialView("Menu");
        }

        //Hàm này dùng để truy cập đến trang đổi mật khẩu của nhân viên
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        //Hàm này dùng để truy cập đến trang đổi mật khẩu của độc giả
        public ActionResult DocGiaDoiMatKhau()
        {
            return View();
        }
        public PartialViewResult CheckDocGia()
        {
            int maTaiKhoan = 0;
            if (Session["maTaiKhoan"] == null)
            {
                //maTaiKhoan = 3;
                return PartialView("TraVeTrangDangNhap");
            }
            else
            {
                maTaiKhoan = Int32.Parse(Session["maTaiKhoan"].ToString());
            }
            return PartialView();
        }
        //Nhận vào mật khẩu cũ, mới và xác nhận mật khẩu mới
        //Nếu mật khẩu củ không khớp với tài khoản hiện tại hiển thị thông báo lỗi
        //Nếu mật khẩu mới và xác nhận không khớp thì báo lỗi
        //Nếu tất cả đều đúng thì đổi thành công và thực hiện chức năng đăng xuất để người dùng đăng nhập lại
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection fc)
        {
            string passCu = fc["matKhauCu"];
            string passMoi = fc["matKhauMoi"];
            string xacNhan = fc["xacNhan"];
            int maTaiKhoan = (int)Session["maTaiKhoan"];
            string loaiTaiKhoan = (string)Session["loaiTaiKhoan"];
            if (loaiTaiKhoan == "NHANVIEN")
            {
                TAI_KHOAN taiKhoan = db.TAI_KHOAN.Where(a => a.MA_NHAN_VIEN == maTaiKhoan).First();
                if (taiKhoan.MAT_KHAU != passCu)
                {
                    ViewBag.thongBao = "Mật khẩu cũ không chính xác";
                    return View();
                }
                else
                {
                    if (passMoi != xacNhan)
                    {
                        ViewBag.thongBao = "Mật khẩu mới và xác nhận mật khẩu mới không chính xác";
                        return View();
                    }
                    else
                    {
                        taiKhoan.MAT_KHAU = passMoi;
                        db.Entry(taiKhoan).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("DangXuat");
                    }
                }
            }
            else
            {
                TAI_KHOAN taiKhoan = db.TAI_KHOAN.Where(a => a.MA_DOC_GIA == maTaiKhoan).First();
                if (taiKhoan.MAT_KHAU != passCu)
                {
                    ViewBag.thongBao = "Mật khẩu cũ không chính xác";
                    return View();
                }
                else
                {
                    if (passMoi != xacNhan)
                    {
                        ViewBag.thongBao = "Mật khẩu mới và xác nhận mật khẩu mới không chính xác";
                        return View();
                    }
                    else
                    {
                        taiKhoan.MAT_KHAU = passMoi;
                        db.Entry(taiKhoan).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("DangXuat");
                    }
                }
            }
        }
    }
}
