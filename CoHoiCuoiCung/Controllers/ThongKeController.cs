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
    public class ThongKeController : Controller
    {
        public QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();
        //Hàm này dùng để truy cập đến trang thống kê của hệ thống
        // GET: /ThongKe/
        public ActionResult Index()
        {
            return View();
        }

        //Hàm này dùng để truy cập đến trang thống kê sách còn và mượn của thư viện
        public ActionResult ConVaMuon()
        {
            List<CUON_SACH> listSach = db.CUON_SACH.ToList();
            List<SACH_MUON> listSachMuon = db.SACH_MUON.ToList();
            //Tạo danh sách rỗng chứa số lượng sách mượn
            List<int> ListSLMuon = new List<int>();
            int soLuongSach = 0;
            int soLuongSachMuon = 0;
            //tổng lượng sách trong kho
            for (int i = 0; i < listSach.Count; i++)
            {
                soLuongSach += (int)listSach.ElementAt(i).SO_LUONG;
            }
            //tổng lương sách đang được mượn
            for (int i = 0; i < listSachMuon.Count; i++)
            {
                if (listSachMuon.ElementAt(i).DA_TRA == false)
                {
                    soLuongSachMuon++;
                }
            }
            //Đếm số lượng sách đang mượn của từng mã sách
            for (int i = 0; i < listSach.Count; i++)
            {
                ListSLMuon.Add(0);
            }
            for (int i = 0; i < listSach.Count; i++)
            {
                for (int j = 0; j < listSachMuon.Count; j++)
                {
                    if (listSach.ElementAt(i).MA_SACH == listSachMuon.ElementAt(j).MA_SACH && listSachMuon.ElementAt(j).DA_TRA == false)
                    {
                        ListSLMuon[i] += 1;
                    }
                }
            }
            ViewBag.soLuongSach = soLuongSach;
            ViewBag.soLuongSachMuon = soLuongSachMuon;
            ViewData["listSach"] = listSach;
            ViewData["listSLMuon"] = ListSLMuon;
            return View();
        }

        //Hàm này dùng để truy cập đến trang thống kê từ ngày đến ngày có bao nhiêu cuốn mượn, trả
        public ActionResult TuNgayDenNgay()
        {
            return View();
        }

        //Hàm này dùng để xử lý thống kê từ ngày đến ngày
        [HttpPost]
        public ActionResult ThongKeTuNgayDenNgay(FormCollection fc)
        {
            string tuNgay = fc["tuNgay"];
            string denNgay = fc["denNgay"];
            DateTime tuNgayDT = DateTime.Parse(tuNgay);
            DateTime denNgayDT = DateTime.Parse(denNgay);
            List<CUON_SACH> listSach = db.CUON_SACH.ToList();
            List<int> soLuongMuon = new List<int>();
            List<int> soLuongTra = new List<int>();
            for (int i = 0; i < listSach.Count; i++)
            {
                soLuongMuon.Add(0);
                soLuongTra.Add(0);
            }
            List<MUON_TRA_SACH> listSachMuon = db.MUON_TRA_SACH.Where(a => a.NGAY_GIO_LAP >= tuNgayDT).Where(a => a.NGAY_GIO_LAP <= denNgayDT).ToList();
            //Đếm số lượng sách đang được mượn của từng mã sách
            for (int i = 0; i < listSachMuon.Count; i++)
            {
                for (int j = 0; j < listSachMuon.ElementAt(i).SACH_MUON.Count(); j++)
                {
                    for (int z = 0; z < listSach.Count; z++)
                    {
                        if (listSachMuon.ElementAt(i).SACH_MUON.ElementAt(j).MA_SACH == listSach.ElementAt(z).MA_SACH)
                        {
                            soLuongMuon[z]++;
                        }
                    }
                }
            }
            //Đếm số lượng sách trả của từng mã sách
            for (int i = 0; i < listSachMuon.Count; i++)
            {
                for (int j = 0; j < listSachMuon.ElementAt(i).SACH_MUON.Count(); j++)
                {
                    for (int z = 0; z < listSach.Count; z++)
                    {
                        if (listSachMuon.ElementAt(i).SACH_MUON.ElementAt(j).MA_SACH == listSach.ElementAt(z).MA_SACH && listSachMuon.ElementAt(i).SACH_MUON.ElementAt(j).DA_TRA==true)
                        {
                            soLuongTra[z]++;
                        }
                    }
                }
            }
            ViewData["listSachMuon"] = listSachMuon;
            ViewData["listSach"] = listSach;
            ViewData["tuNgay"] = tuNgayDT;
            ViewData["denNgay"] = denNgayDT;
            ViewData["soLuongMuon"] = soLuongMuon;
            ViewData["soLuongTra"] = soLuongTra;
            return View();
        }

        //Hàm này dùng để truy cập đến trang thống kê sách theo năm xuất bản
        public ActionResult TheoNamXuatBan()
        {
            return View();
        }

        //Hàm này dùng để xử lý thống kê sách theo năm xuất bản
        [HttpPost]
        public ActionResult ThongKeTheoNam(FormCollection fc)
        {
            ViewBag.thongBao = "";
            int tuNam = Int32.Parse(fc["tuNam"]);
            int denNam = tuNam;
            string strDenNam = fc["denNam"] as string;
            if (strDenNam != "")
            {
                denNam = Int32.Parse(fc["denNam"]);
            }
            ViewBag.tuNam = tuNam;
            ViewBag.denNam = denNam;
            List<CUON_SACH> listSach = db.CUON_SACH.Where(a => a.NAM_XUAT_BAN >= tuNam).Where(a => a.NAM_XUAT_BAN <= denNam).ToList();
            if (listSach.Count == 0)
            {
                ViewBag.thongBao = "Không có cuốn nào";
            }
            return View(listSach);
        }

        //Hàm này dùng để truy cập đén trang thống kê sách theo số trang
        public ActionResult TheoSoTrang()
        {
            return View();
        }

        //Hàm này dùng để xử lý thống kê theo số trang
        [HttpPost]
        public ActionResult ThongKeTheoSoTrang(FormCollection fc)
        {
            ViewBag.thongBao = "";
            int tuTrang = Int32.Parse(fc["tuTrang"]);
            int denTrang = tuTrang;
            string strDenNam = fc["denTrang"] as string;
            if (strDenNam != "")
            {
                denTrang = Int32.Parse(fc["denTrang"]);
            }
            ViewBag.tuTrang = tuTrang;
            ViewBag.denTrang = denTrang;
            List<CUON_SACH> listSach = db.CUON_SACH.Where(a => a.SO_TRANG >= tuTrang).Where(a => a.SO_TRANG <= denTrang).ToList();
            if (listSach.Count == 0)
            {
                ViewBag.thongBao = "Không có cuốn nào";
            }
            return View(listSach);
        }

        //Hàm này dùng để truy cập đến trang thống kê theo nhà cung cấp
        public ActionResult TheoNhaCungCap()
        {
            List<NHA_CUNG_CAP> listNhaCungCap = db.NHA_CUNG_CAP.ToList();
            return View(listNhaCungCap);
        }

        //Hàm này dùng để xử lý thống kê theo nhà cung cấp
        [HttpPost]
        public ActionResult ThongKeTheoNhaCungCap(FormCollection fc)
        {
            ViewBag.thongBao = "";
            int maNCC = Int32.Parse(fc["maNCC"]);
            List<CUNG_CAP_SACH> listCungCapSach = db.CUNG_CAP_SACH.Where(a => a.MA_NCC == maNCC).ToList();
            NHA_CUNG_CAP ncc = db.NHA_CUNG_CAP.Where(a => a.MA_NCC == maNCC).First();
            if (listCungCapSach.Count == 0)
            {
                ViewBag.thongBao = "Không có cuốn nào";
            }
            ViewBag.tenNCC = ncc.TEN_NCC;
            return View(listCungCapSach);
        }

        //Hàm này dùng để truy cập đến trang thống kê độc giả
        public ActionResult DocGia()
        {
            List<DOC_GIA> listDocGia = db.DOC_GIA.ToList();
            return View(listDocGia);
        }

        //Hàm này dùng để truy cập đến trang xem trạng thái của 1 quyển sách
        public ActionResult XemTrangThai()
        {
            List<CUON_SACH> listSach = db.CUON_SACH.ToList();
            return View(listSach);
        }

        //Hàm này dùng để xử lý thống kê trạng thái của 1 cuốn sách
        [HttpPost]
        public ActionResult XemTrangThaiSach(FormCollection fc)
        {
            int maSach = Int32.Parse(fc["maSach"]);
            CUON_SACH sach = db.CUON_SACH.Where(a => a.MA_SACH == maSach).First();
            ViewData["sach"] = sach;
            int soSachMuon = db.SACH_MUON.Where(a => a.MA_SACH == maSach).Where(a => a.DA_TRA == false).ToList().Count;
            ViewData["soSachMuon"] = soSachMuon;
            return View();
        }
    }
}
