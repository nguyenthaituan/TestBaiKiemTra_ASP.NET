using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ONTapWEB.Models;

namespace ONTapWEB.Controllers
{
    public class ThanhViensController : Controller
    {
        private KIEMTRA_59132942Entities db = new KIEMTRA_59132942Entities();

        // GET: ThanhViens
        public ActionResult Index()
        {
            var thanhVien = db.ThanhVien.Include(t => t.Tinh);
            return View(thanhVien.ToList());
        }

        [HttpGet]
        public ActionResult TimKiem(string MaTV = "", string HoTen="", string NgaySinh="", 
            string GioiTinh="", string Email="", string DiaChi="", string MaTinh="")  
        {
            if (GioiTinh != "1" && GioiTinh != "0")
                GioiTinh = null;
            ViewBag.MaTV = MaTV;
            ViewBag.HoTen = HoTen;
            ViewBag.NgaySinh = NgaySinh;
            ViewBag.GioiTinh = GioiTinh;
            ViewBag.Email = Email;
            ViewBag.DiaChi = DiaChi;
            ViewBag.MaTinh = new SelectList(db.Tinh, "MaTinh", "TenTinh");

            var thanhviens = db.ThanhVien.SqlQuery("ThanhVien_TimKiem'" + MaTV + "','" + HoTen + "','" + NgaySinh + "','" + GioiTinh + "','" + Email + "',N'" + DiaChi + "','" + MaTinh + "'");
            if (thanhviens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(thanhviens.ToList());
        }

        // GET: ThanhViens/Details/5
        public ActionResult Details(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhVien.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // GET: ThanhViens/Create
        public ActionResult Create()
        {
            ViewBag.MaTinh = new SelectList(db.Tinh, "MaTinh", "TenTinh");
            return View();
        }

        // POST: ThanhViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTV,HoTV,TenTV,NgaySinh,GioiTinh,Email,DiaChi,MaTinh")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.ThanhVien.Add(thanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTinh = new SelectList(db.Tinh, "MaTinh", "TenTinh", thanhVien.MaTinh);
            return View(thanhVien);
        }

        // GET: ThanhViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhVien.Find(id);
            
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTinh = new SelectList(db.Tinh, "MaTinh", "TenTinh", thanhVien.MaTinh);
            return View(thanhVien);
        }

        // POST: ThanhViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTV,HoTV,TenTV,NgaySinh,GioiTinh,Email,DiaChi,MaTinh")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTinh = new SelectList(db.Tinh, "MaTinh", "TenTinh", thanhVien.MaTinh);
            return View(thanhVien);
        }

        // GET: ThanhViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhVien.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // POST: ThanhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThanhVien thanhVien = db.ThanhVien.Find(id);
            db.ThanhVien.Remove(thanhVien);
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
