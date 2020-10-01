using ShopBridgeApp.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopBridgeApp.Controllers
{
    public class ItemMasterController : Controller
    {
        private ShopBridgeDBEntities db = new ShopBridgeDBEntities();

        // GET: ItemMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View(db.ItemMasters.ToList());
        }

        // GET: ItemMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // GET: ItemMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemMaster itemMaster, HttpPostedFileBase postedImage)
        {
            if (ModelState.IsValid)
            {
                string uploadpath = "~/Uploads/";
                string path = "";
                if (postedImage != null)
                {
                    path = uploadpath + itemMaster.ItemName.Replace(" ", "") + DateTime.UtcNow.ToString("ddmmyyyyhhmmss") + Path.GetExtension(postedImage.FileName);
                }

                if (itemMaster.ItemID == 0)
                {
                    itemMaster.CreatedDate = DateTime.UtcNow;
                }
                else
                {
                    itemMaster.ModifiedDate = DateTime.UtcNow;
                }

                //Image Path
                itemMaster.Image = path;

                db.ItemMasters.Add(itemMaster);
                int returnVal = db.SaveChanges();
                if (returnVal > 0)
                {
                    //Saving Image
                    if (path != "")
                    {
                        if (!Directory.Exists(Server.MapPath(uploadpath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(uploadpath));
                        }

                        postedImage.SaveAs(Server.MapPath(path));
                    }
                }
                ModelState.Clear();
                return View();
            }

            return View(itemMaster);
        }

        // GET: ItemMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemMaster itemMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemMaster);
        }

        // GET: ItemMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: ItemMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster != null)
            {
                db.ItemMasters.Remove(itemMaster);
                db.SaveChanges();
            }

            return RedirectToAction("Grid");
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
