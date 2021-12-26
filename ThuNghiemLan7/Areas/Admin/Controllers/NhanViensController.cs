using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThuNghiemLan7.Models;
using ThuNghiemLan7.Areas.Admin.MaHoa;
using PagedList;

namespace ThuNghiemLan7.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        private BTLDB db = new BTLDB();

        // GET: Admin/NhanViens
        public ActionResult Index(int? page)
        {
            var userSession = (UserLogin)Session[PLLogin.USER_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/LoginQLUser");
            }
            var nhanViens = db.NhanVien.Include(n => n.ChucVu);
            nhanViens = nhanViens.OrderBy(n => n.MaNhanVien);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(nhanViens.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "MaChucVu", "TenChucVu");
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhanVien,TenDangNhap,MatKhau,TenNhanVien,MaChucVu")] NhanVien nhanVien)
        {
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            try
            {
                if (ModelState.IsValid)
                {
                    db.NhanVien.Add(nhanVien);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                ViewBag.Error = "lỗi nhập dữ liệu!" + ex.Message;
                return View(nhanVien);
            }
        }

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhanVien,TenDangNhap,MatKhau,TenNhanVien,MaChucVu")] NhanVien nhanVien)
        {
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                ViewBag.Error = "lỗi nhập dữ liệu!" + ex.Message;
                return View(nhanVien);
            }
        }

        // GET: Admin/NhanViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            db.NhanVien.Remove(nhanVien);
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
    }
}
