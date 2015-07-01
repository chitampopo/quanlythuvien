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
    public class MuonTraSachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();
        //Hàm này dùng để truy cập đến trang quản lý mượn trả sách
        // GET: /MuonTraSach/
        public ActionResult Index()
        {
            return View();
        }

        //Khi nhấn vào mượn sách thì sẽ truy cập đến hàm này
        //Hàm này dùng để truy cập đến trang tìm độc giả
        public ActionResult TimDocGia()
        {
            return View(db.DOC_GIA.ToList());
        }

        //Hàm này dùng để truy cập đến trang mượn sách của độc giả có MaDocGia là id
        public ActionResult MuonSach(int? id)
        {
            //Tìm chính sách lưu thông mới nhất
            CHINH_SACH_LUU_THONG chinhSach = db.CHINH_SACH_LUU_THONG.ToList().Last();
            //Tìm độc giả theo id
            DOC_GIA docGia = db.DOC_GIA.Find(id);
            ViewData["tenDocGia"] = docGia.TEN_DOC_GIA;
            ViewData["sdt"] = docGia.SDT_DOC_GIA;
            ViewData["diaChi"] = docGia.DIA_CHI_DOC_GIA;
            ViewData["ngayThamGia"] = docGia.NGAY_THAM_GIA;
            ViewData["ngayHetHan"] = docGia.NGAY_HET_HAN;
            //Tạo các thông báo cho nhân viên biết về độc giả
            DateTime ngayHetHan = (DateTime)docGia.NGAY_HET_HAN;
            TimeSpan ngayConLai = ngayHetHan.Subtract(DateTime.Now);
            if (ngayConLai.Days < (chinhSach.SO_NGAY_MUON + chinhSach.SO_LAN_DUOC_GIA_HAN * chinhSach.SO_NGAY_GIA_HAN))
            {
                ViewData["thongBao1"] = "Độc giả này sắp hết hạn mượn sách";
            }
            if (ngayConLai.Days < 0)
            {
                ViewData["thongBao1"] = "Độc giả này đã hết hạn mượn sách";
            }
            ViewData["ngayConLai"] = ngayConLai.Days;
            //Đếm số lượng sách mượn của độc giả
            var listMuonTraSach = db.MUON_TRA_SACH.Where(a => a.MA_DOC_GIA == id).ToList();
            int count = 0;
            for (int i = 0; i < listMuonTraSach.Count(); i++)
            {
                for (int j = 0; j < listMuonTraSach.ElementAt(i).SACH_MUON.Count(); j++)
                {
                    if (listMuonTraSach.ElementAt(i).SACH_MUON.ElementAt(j).DA_TRA == false)
                    {
                        count++;
                    }
                }
            }
            ViewData["soSachMuon"] = count;

            int soSachCoTheMuon = (int)chinhSach.SO_SACH_MUON_TOI_DA - count;
            if (soSachCoTheMuon == 0)
            {
                ViewData["thongBao2"] = "Độc giả này đã mượn vượt số lần quy định";
            }
            else
            {
                int soSachConLai = (int)chinhSach.SO_SACH_MUON_TOI_DA - count;
                @ViewData["thongBao2"] = "Độc giả này còn có thể mượn được " + soSachConLai + " cuốn sách";
            }
            ViewBag.maDocGia = id;
            return View();
        }

        //Hàm này dùng để xử lý thông tin mượn sách của độc giả
        [HttpPost]
        public ActionResult MuonSach(FormCollection fc)
        {
            //lưu vào bảng mượn trả sách
            int maDocGia = Int32.Parse(fc["maDocGia"]);
            //Lấy chính sách lưu thông mới nhất
            List<CHINH_SACH_LUU_THONG> listChinhSach = db.CHINH_SACH_LUU_THONG.ToList();
            CHINH_SACH_LUU_THONG chinhSach = listChinhSach.Last();
            //Tạo đối tượng muonTraSach và thêm các thông tin cần thiết vào
            MUON_TRA_SACH muonTraSach = new MUON_TRA_SACH();
            muonTraSach.MA_CHINH_SACH = chinhSach.MA_CHINH_SACH;
            muonTraSach.MA_DOC_GIA = maDocGia;
            int maTaiKhoan = Int32.Parse(Session["maTaiKhoan"].ToString());
            muonTraSach.MA_NHAN_VIEN = maTaiKhoan;
            muonTraSach.NGAY_GIO_LAP = DateTime.Now;
            int soNgayMuon = (int)chinhSach.SO_NGAY_MUON;
            muonTraSach.NGAY_PHAI_TRA = DateTime.Now.AddDays(soNgayMuon);
            db.MUON_TRA_SACH.Add(muonTraSach);
            //lưu vào bảng mượn sách
            DOC_GIA docGia = db.DOC_GIA.Where(a => a.MA_DOC_GIA == maDocGia).First();
            List<CUON_SACH> listSach = db.CUON_SACH.ToList();
            for (int i = 0; i < listSach.Count; i++)
            {
                int ma = listSach.ElementAt(i).MA_SACH;
                string maSach = Convert.ToString(ma);
                string check = fc[maSach];
                //Nếu checkbox của cuốn sách nào được check thì sẽ lưu vào CSDL
                if (check == "on")
                {
                    SACH_MUON sachMuon = new SACH_MUON();
                    sachMuon.MA_SACH = ma;
                    sachMuon.MA_MUON_TRA = muonTraSach.MA_MUON_TRA;
                    sachMuon.NGAY_PHAI_TRA = DateTime.Now.AddDays((double)chinhSach.SO_NGAY_MUON);
                    sachMuon.GIA_HAN = 0;
                    sachMuon.DA_TRA = false;
                    db.SACH_MUON.Add(sachMuon);
                    //Giảm số lượng trong bảng CuonSach 1 đơn vị
                    listSach.ElementAt(i).SO_LUONG--;
                    db.Entry(listSach.ElementAt(i)).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return View("Index");
        }

        //Hàm này dùng để truy cập đến trang trả sách
        public ActionResult TraSach()
        {
            return View();
        }

        //Hàm này dùng để truy cập đến trang trả sách của độc giả có MaDocGia la id
        public ActionResult TrangTraSach(int? id = 1)
        {
            DOC_GIA docGia = db.DOC_GIA.Find(id);
            List<CHINH_SACH_LUU_THONG> listChinhSach = db.CHINH_SACH_LUU_THONG.ToList();
            CHINH_SACH_LUU_THONG chinhSach = listChinhSach.Last();
            ViewData["tenDocGia"] = docGia.TEN_DOC_GIA;
            ViewData["sdt"] = docGia.SDT_DOC_GIA;
            ViewData["diaChi"] = docGia.DIA_CHI_DOC_GIA;
            ViewData["ngayThamGia"] = docGia.NGAY_THAM_GIA;
            ViewData["ngayHetHan"] = docGia.NGAY_HET_HAN;
            ViewBag.maDocGia = id;
            ViewBag.soLanGiaHanToiDa = chinhSach.SO_LAN_DUOC_GIA_HAN;
            var listMuonTra = db.MUON_TRA_SACH.Include(s => s.SACH_MUON).Where(a => a.MA_DOC_GIA == id).ToList();
            return View(listMuonTra);
        }

        //Hàm này dùng để xử lý gia hạn sách
        public ActionResult GiaHan(int maDocgia, int maMuon, int maSach)
        {
            List<CHINH_SACH_LUU_THONG> listChinhSach = db.CHINH_SACH_LUU_THONG.ToList();
            CHINH_SACH_LUU_THONG chinhSach = listChinhSach.Last();
            ViewBag.maDocGia = maDocgia;
            ViewBag.maMuon = maMuon;
            ViewBag.maSach = maSach;
            var listSachMuon = db.SACH_MUON.Where(a => a.MA_MUON_TRA == maMuon).Where(a => a.MA_SACH == maSach).ToList();
            SACH_MUON sachMuon = listSachMuon.First();
            DateTime ngayPhaiTra = DateTime.Parse(sachMuon.NGAY_PHAI_TRA.ToString());
            sachMuon.NGAY_PHAI_TRA = ngayPhaiTra.AddDays((double)chinhSach.SO_NGAY_GIA_HAN);
            sachMuon.GIA_HAN++;
            if (sachMuon.GIA_HAN > chinhSach.SO_LAN_DUOC_GIA_HAN)
            {
                ViewBag.baoLoi = "Đã quá số lần gia hạn là " + chinhSach.SO_LAN_DUOC_GIA_HAN;
            }
            else
            {
                db.Entry(sachMuon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("TrangTraSach" + "/" + maDocgia, "MuonTraSach");
        }

        //Hàm này dùng để xử lý trả sách
        public ActionResult TraSachMuon(int maDocgia, int maMuon, int maSach)
        {
            var listSachMuon = db.SACH_MUON.Where(a => a.MA_MUON_TRA == maMuon).Where(a => a.MA_SACH == maSach).ToList();
            SACH_MUON sachMuon = listSachMuon.First();
            sachMuon.DA_TRA = true;
            sachMuon.THOI_GIAN_TRA = DateTime.Now;
            db.Entry(sachMuon).State = EntityState.Modified;
            db.SaveChanges();
            CUON_SACH cuonSach = db.CUON_SACH.Where(a => a.MA_SACH == maSach).First();
            cuonSach.SO_LUONG++;
            db.Entry(cuonSach).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TrangTraSach" + "/" + maDocgia, "MuonTraSach");
        }

        //Hàm này dùng để truy cập đến trang lập phiếu phạt độc giả trễ hạn hoặc mất sách
        public ActionResult TrangPhat(int maDocGia, int maMuon)
        {
            ViewBag.maDocgia = maDocGia;
            ViewBag.maMuon = maMuon;
            List<MUON_TRA_SACH> muonTraSach = db.MUON_TRA_SACH.Where(a => a.MA_DOC_GIA == maDocGia).Where(a => a.MA_MUON_TRA == maMuon).ToList();
            return View(muonTraSach);
        }

        //Hàm này dùng để hiển thị phiếu phạt
        [HttpPost]
        public ActionResult LapPhieuPhat(FormCollection fc)
        {
            //Lấy thông tin độc giả đang bị phạt
            int maDocGia = Int32.Parse(fc["maDocGia"]);
            DOC_GIA docgia = db.DOC_GIA.Where(a => a.MA_DOC_GIA == maDocGia).First();
            //Lấy thông tin nhân viên đang lập phiếu phạt
            int maTaiKhoan = Int32.Parse(Session["maTaiKhoan"].ToString());
            NHAN_VIEN nhanVien = db.NHAN_VIEN.Where(a => a.MA_NHAN_VIEN == maTaiKhoan).First();
            //Lấy thông tin mượn trả sách của độc giả
            int maMuon = Int32.Parse(fc["maMuon"]);
            List<MUON_TRA_SACH> muonTraSach = db.MUON_TRA_SACH.Where(a => a.MA_DOC_GIA == maDocGia).Where(a => a.MA_MUON_TRA == maMuon).ToList();
            //Tạo các danh sách rỗng để lưu dữ liệu
            //Các mảng này sẽ có cùng số lượng phần tử
            //Mỗi vị trí trong các mảng sẽ lưu thông số tương ứng
            //Ví dụ 
            //listMaSach[0] = 1   -- Mã sách 1 được lưu ở vị trí thứ 0
            //listSoNgayTre[0] = 10 -- Mã sách 1 trễ hạn 10 ngày
            //listMatSach[0] = true -- Mã sách 1 bị mất
            //listGiaSach[0] = 20000 -- Mã sách 1 có giá là 20000
            List<int> listMaSach = new List<int>();
            List<int> listSoNgayTre = new List<int>();
            List<int> listGiaSach = new List<int>();
            List<bool> listMatSach = new List<bool>();
            List<string> listTenSach = new List<string>();
            //Chạy vòng lặp lưu vào các mảng trên

            //lặp các phiếu mượn của độc giả
            foreach (var item in muonTraSach)
            {
                //Đến số sách còn đang mượn của độc giả của mỗi phiếu mượn
                int count = 0;
                for (int i = 0; i < item.SACH_MUON.Count; i++)
                {
                    if (item.SACH_MUON.ElementAt(i).DA_TRA == false)
                    {
                        count++;
                    }
                }
                //Nếu tất cả sach được mượn ở phiếu này đã được trả thì qua phiếu mượn tiếp theo
                if (count == 0)
                {
                    continue;
                }
                //Lặp các cuốn sách đang được mượn
                for (int i = 0; i < item.SACH_MUON.Count(); i++)
                {
                    //Nếu cuốn này đã được trả thì bỏ qua, chỉ kiểm tra những cuốn chưa trả
                    if (item.SACH_MUON.ElementAt(i).DA_TRA == true)
                    {
                        continue;
                    }
                    else
                    {
                        //Lấy mã sách bỏ vào mảng listMaSach
                        int maSachInt = (int)item.SACH_MUON.ElementAt(i).MA_SACH;
                        listMaSach.Add(maSachInt);
                        //Lấy số ngày trể ỏ vào mảng listSoNgayTre
                        int soTre = Int32.Parse(fc["VUOT " + maSachInt]);
                        if (soTre < 0)
                        {
                            soTre = 0;
                        }
                        listSoNgayTre.Add(soTre);
                        //Thêm các thông tin giá sách, tên sách vào các mảng
                        listGiaSach.Add((int)item.SACH_MUON.ElementAt(i).CUON_SACH.GIA_SACH);
                        listTenSach.Add(item.SACH_MUON.ElementAt(i).CUON_SACH.TEN_SACH);
                        //Nếu checkbox đánh dấu mất sách được chọn thì lưu vào listSach là true
                        //ngược lại thì lưu false
                        string maSach = Convert.ToString(maSachInt);
                        string check = fc["MAT " + maSach];
                        if (check == "on")
                        {
                            listMatSach.Add(true);
                        }
                        else
                        {
                            listMatSach.Add(false);
                        }
                    }
                }
            }
            //Lấy thông tin phạt
            LY_DO_PHAT treHan = db.LY_DO_PHAT.Where(a => a.MA_LY_DO == 1).First();
            LY_DO_PHAT matSach = db.LY_DO_PHAT.Where(a => a.MA_LY_DO == 2).First();
            //Gửi đến trang in phiếu phạt
            ViewData["listMaSach"] = listMaSach;
            ViewData["listSoNgayTre"] = listSoNgayTre;
            ViewData["listMatSach"] = listMatSach;
            ViewData["listGiaSach"] = listGiaSach;
            ViewData["listTenSach"] = listTenSach;
            ViewData["phatTreHan"] = treHan.SO_TIEN_PHAT;
            ViewData["phatMatSach"] = matSach.SO_TIEN_PHAT;
            ViewBag.tenNhanVien = nhanVien.TEN_NHAN_VIEN;
            ViewBag.tenDocGia = docgia.TEN_DOC_GIA;
            ViewBag.diaChiDocGia = docgia.DIA_CHI_DOC_GIA;
            return View();
        }

        //Hàm này dùng để xử lý các thông tin trong CSDL sau khi lập phiếu phạt
        //người độc giả đóng phạt xong thì mới thực hiện chức năng này
        [HttpPost]
        public ActionResult XuLyPhieuPhat(FormCollection fc)
        {
            //Lấy thông tin độc giả
            int maDocGia = Int32.Parse(fc["maDocGia"]);
            DOC_GIA docgia = db.DOC_GIA.Where(a => a.MA_DOC_GIA == maDocGia).First();
            //Lấy thông tin nhân viên
            int maTaiKhoan = Int32.Parse(Session["maTaiKhoan"].ToString());
            NHAN_VIEN nhanVien = db.NHAN_VIEN.Where(a => a.MA_NHAN_VIEN == maTaiKhoan).First();
            //Lấy thông tin mươn trả sách
            int maMuon = Int32.Parse(fc["maMuon"]);
            int maMuonTra = maMuon;
            List<MUON_TRA_SACH> muonTraSach = db.MUON_TRA_SACH.Where(a => a.MA_DOC_GIA == maDocGia).Where(a => a.MA_MUON_TRA == maMuon).ToList();
            //Tạo các mảng rỗng như hàm LapPhieuPhat
            List<int> listMaSach = new List<int>();
            List<int> listSoNgayTre = new List<int>();
            List<int> listGiaSach = new List<int>();
            List<bool> listMatSach = new List<bool>();
            List<string> listTenSach = new List<string>();
            foreach (var item in muonTraSach)
            {
                int count = 0;
                for (int i = 0; i < item.SACH_MUON.Count; i++)
                {
                    if (item.SACH_MUON.ElementAt(i).DA_TRA == false)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    continue;
                }
                for (int i = 0; i < item.SACH_MUON.Count(); i++)
                {
                    if (item.SACH_MUON.ElementAt(i).DA_TRA == true)
                    {
                        continue;
                    }
                    else
                    {
                        int maSachInt = (int)item.SACH_MUON.ElementAt(i).MA_SACH;
                        listMaSach.Add(maSachInt);
                        int soTre = Int32.Parse(fc["VUOT " + maSachInt]);
                        if (soTre < 0)
                        {
                            soTre = 0;
                        }
                        listSoNgayTre.Add(soTre);
                        listGiaSach.Add((int)item.SACH_MUON.ElementAt(i).CUON_SACH.GIA_SACH);
                        listTenSach.Add(item.SACH_MUON.ElementAt(i).CUON_SACH.TEN_SACH);
                        string maSach = Convert.ToString(maSachInt);
                        string check = fc["MAT " + maSach];
                        if (check == "on")
                        {
                            listMatSach.Add(true);
                        }
                        else
                        {
                            listMatSach.Add(false);
                        }
                    }
                }
            }
            LY_DO_PHAT treHan = db.LY_DO_PHAT.Where(a => a.MA_LY_DO == 1).First();
            LY_DO_PHAT matSach = db.LY_DO_PHAT.Where(a => a.MA_LY_DO == 2).First();

            //Xử lý trong CSDL
            //Lặp các cuốn sách đang được mượn và xét các trường hợp bị phát
            for (int i = 0; i < listMaSach.Count; i++)
            {
                //Trường hợp không mất sách và không trể thì bỏ qua
                if (listMatSach.ElementAt(i) == false && listSoNgayTre.ElementAt(i) == 0)
                {
                    continue;
                }

                int maSachTemp = listMaSach.ElementAt(i);
                SACH_MUON sachMuon = db.SACH_MUON.Where(a => a.MA_SACH == maSachTemp).Where(a => a.MA_MUON_TRA == maMuonTra).First();
                //trường hợp mất sách không không trễ hạn
                if (listMatSach.ElementAt(i) == true && listSoNgayTre.ElementAt(i) == 0)
                {
                    PHAT p = new PHAT();
                    p.MA_LY_DO = 2;
                    p.MA_SACH = listMaSach.ElementAt(i);
                    p.MA_MUON_TRA = maMuonTra;
                    p.SO_TIEN_BI_PHAT = matSach.SO_TIEN_PHAT * listGiaSach.ElementAt(i);
                    db.PHATs.Add(p);
                }
                    //trường hợp mất sách và trễ hạn
                else if (listMatSach.ElementAt(i) == true && listSoNgayTre.ElementAt(i) > 0)
                {
                    PHAT p = new PHAT();
                    p.MA_LY_DO = 2;
                    p.MA_SACH = listMaSach.ElementAt(i);
                    p.MA_MUON_TRA = maMuonTra;
                    p.SO_TIEN_BI_PHAT = matSach.SO_TIEN_PHAT * listGiaSach.ElementAt(i);
                    PHAT p1 = new PHAT();
                    p1.MA_LY_DO = 1;
                    p1.MA_SACH = listMaSach.ElementAt(i);
                    p1.MA_MUON_TRA = maMuonTra;
                    p1.SO_TIEN_BI_PHAT = treHan.SO_TIEN_PHAT * listSoNgayTre.ElementAt(i);
                    db.PHATs.Add(p1);
                }
                    //Không mất sách nhưng trễ hạn
                else if (listMatSach.ElementAt(i) == false && listSoNgayTre.ElementAt(i) > 0)
                {
                    PHAT p1 = new PHAT();
                    p1.MA_LY_DO = 1;
                    p1.MA_SACH = listMaSach.ElementAt(i);
                    p1.MA_MUON_TRA = maMuonTra;
                    p1.SO_TIEN_BI_PHAT = treHan.SO_TIEN_PHAT * listSoNgayTre.ElementAt(i);
                    db.PHATs.Add(p1);
                    sachMuon.THOI_GIAN_TRA = DateTime.Now;
                }
                //Thêm dữ liệu đã trả vào bảng SachMuon, nhưng không lưu ngày trả vì... mất sách
                //Cũng không tăng số lượng sách trong kho của sách đó        
                sachMuon.DA_TRA = true;
                db.Entry(sachMuon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        //Hàm này dùng để truy cập đến trang tra cứu mượn trả sách của độc giả
        public ActionResult TraCuu()
        {
            if (Session["maTaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            int maTaiKhoan = (int)Session["maTaiKhoan"];
            List<MUON_TRA_SACH> listMuonTra = db.MUON_TRA_SACH.Where(a => a.MA_DOC_GIA == maTaiKhoan).ToList();
            ViewData["listMuonTra"] = listMuonTra;
            CHINH_SACH_LUU_THONG chinhSach = db.CHINH_SACH_LUU_THONG.ToList().Last();
            ViewData["chinhSach"] = chinhSach;
            return View();
        }

        //Hàm này dùng để gia hạn sách ở trang độc giả
        public ActionResult DocGiaGiaHan(int maMuon, int maSach)
        {
            int maDocgia = (int)Session["maTaiKhoan"];
            List<CHINH_SACH_LUU_THONG> listChinhSach = db.CHINH_SACH_LUU_THONG.ToList();
            CHINH_SACH_LUU_THONG chinhSach = listChinhSach.Last();
            ViewBag.maDocGia = maDocgia;
            ViewBag.maMuon = maMuon;
            ViewBag.maSach = maSach;
            var listSachMuon = db.SACH_MUON.Where(a => a.MA_MUON_TRA == maMuon).Where(a => a.MA_SACH == maSach).ToList();
            SACH_MUON sachMuon = listSachMuon.First();
            DateTime ngayPhaiTra = DateTime.Parse(sachMuon.NGAY_PHAI_TRA.ToString());
            sachMuon.NGAY_PHAI_TRA = ngayPhaiTra.AddDays((double)chinhSach.SO_NGAY_GIA_HAN);
            sachMuon.GIA_HAN++;
            if (sachMuon.GIA_HAN > chinhSach.SO_LAN_DUOC_GIA_HAN)
            {
                ViewBag.baoLoi = "Đã quá số lần gia hạn là " + chinhSach.SO_LAN_DUOC_GIA_HAN;
            }
            else
            {
                db.Entry(sachMuon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("TraCuu");
        }

        //Hàm này dùng để truy cập đến trang quy định xử phạt
        public ActionResult QuyDinhXuPhat()
        {
            List<LY_DO_PHAT> listLyDoPhat = db.LY_DO_PHAT.ToList();
            return View(listLyDoPhat);
        }
    }
}