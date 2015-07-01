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
    public class SachController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        //Hàm này dùng để truy cập đến trang quản lý thông tin sách
        // GET: /Sach/
        public ActionResult Index()
        {
            var cuon_sach = db.CUON_SACH.Include(c => c.DAU_SACH);
            return View(cuon_sach.ToList());
        }

        //Hàm này dùng để truy cập đến trang xem chi tiết thông tin của 1 cuốn sách
        // GET: /Sach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUON_SACH cuon_sach = db.CUON_SACH.Find(id);
            if (cuon_sach == null)
            {
                return HttpNotFound();
            }
            return View(cuon_sach);
        }

        //Hàm này dùng để truy cập đến trang thêm thông tín cuốn sách
        // GET: /Sach/Create
        public ActionResult Create()
        {
            ViewBag.MA_DAU_SACH = new SelectList(db.DAU_SACH, "MA_DAU_SACH", "TEN_DAU_SACH");
            return View();
        }

        //Hàm này dùng để xử lý thêm thông tín 1 cuốn sách
        // POST: /Sach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_SACH,MA_DAU_SACH,GIA_SACH,TEN_SACH,NAM_XUAT_BAN,MA_VACH,TAI_BAN,SO_TRANG,SO_LUONG,GHI_CHU_SACH")] CUON_SACH cuon_sach)
        {
            if (ModelState.IsValid)
            {
                db.CUON_SACH.Add(cuon_sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_DAU_SACH = new SelectList(db.DAU_SACH, "MA_DAU_SACH", "TEN_DAU_SACH", cuon_sach.MA_DAU_SACH);
            return View(cuon_sach);
        }

        //Hàm này dùng để truy cập đến trang chỉnh sửa thông tin của 1 cuốn sách
        // GET: /Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUON_SACH cuon_sach = db.CUON_SACH.Find(id);
            if (cuon_sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DAU_SACH = new SelectList(db.DAU_SACH, "MA_DAU_SACH", "TEN_DAU_SACH", cuon_sach.MA_DAU_SACH);
            return View(cuon_sach);
        }

        //Hàm này dùng để xử lý chỉnh sửa thông tin của 1 cuốn sách
        // POST: /Sach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_SACH,MA_DAU_SACH,NAM_XUAT_BAN,MA_VACH,TAI_BAN,SO_LUONG,GIA_SACH,TEN_SACH,SO_TRANG,GHI_CHU_SACH")] CUON_SACH cuon_sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuon_sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DAU_SACH = new SelectList(db.DAU_SACH, "MA_DAU_SACH", "TEN_DAU_SACH", cuon_sach.MA_DAU_SACH);
            return View(cuon_sach);
        }

        //Hàm này dùng để truy cập đến trang xóa 1 cuốn sách
        // GET: /Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUON_SACH cuon_sach = db.CUON_SACH.Find(id);
            if (cuon_sach == null)
            {
                return HttpNotFound();
            }
            return View(cuon_sach);
        }

        //Hàm này dùng để thực hiện xoa 1 cuốn sách
        // POST: /Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUON_SACH cuon_sach = db.CUON_SACH.Find(id);
            db.CUON_SACH.Remove(cuon_sach);
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

        //Hàm này dùng để hiển thị PartialView lietkeSach
        //Được sửa dụng ở view MuonSach ở MuonTraSachController
        public PartialViewResult LietKeSach()
        {
            var cuon_sach = db.CUON_SACH.Include(c => c.DAU_SACH);
            return PartialView(cuon_sach.ToList());
        }

        //Hàm này dùng để hiển thị mã vạch được sinh ra từ mã sách
        public PartialViewResult LietKeMaVach()
        {
            var cuon_sach = db.CUON_SACH.Include(c => c.DAU_SACH);
            return PartialView(cuon_sach.ToList());
        }
        public ActionResult MaVach()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MaVach(FormCollection fc)
        {
            var listSach = db.CUON_SACH.ToList();
            for (int i = 0; i < listSach.Count; i++)
            {
                int ma = listSach.ElementAt(i).MA_SACH;
                string maSach = Convert.ToString(ma);
                string check = fc[maSach];
                if (check == "on")
                {
                    int soLuong = Int32.Parse(fc["SL "+maSach]);
                    listSach.ElementAt(i).SO_LUONG = soLuong;
                }
                else
                {
                    listSach.ElementAt(i).SO_LUONG = 0;
                }
            }
            return View("InMaVach", listSach);
        }
    }
}
