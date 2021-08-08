using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastaOrderfood.Account;
using PastaOrderfood.Models;

namespace goshopping.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        PastaOrderEntities db = new PastaOrderEntities();
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Index()
        {
            var order = db.Order.Where(m=>m.order_status == "接單中").ToList();
            ViewBag.status = order.Count();
            var food = db.Pastas.OrderBy(m=>m.rowid).ToList();
            ViewBag.food = food.Count();
            return View();
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Home_config()
        {
            var carousel = db.Home_Carousel.OrderBy(m => m.rowid).ToList();
            return View(carousel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Home_configCreate(string context1, string context2, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageFile_bg)
        {
            Home_Carousel hc = new Home_Carousel();
            
            hc.ImageFile = ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(hc.ImageFile.FileName);
            string extension = Path.GetExtension(hc.ImageFile.FileName);
            fileName = fileName + "img"+hc.rowid + extension;
            hc.img = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            hc.ImageFile.SaveAs(fileName);

            hc.ImageFile_bg = ImageFile_bg;
            string fileName_bg = Path.GetFileNameWithoutExtension(hc.ImageFile_bg.FileName);
            string extension_bg = Path.GetExtension(hc.ImageFile_bg.FileName);
            fileName_bg = fileName_bg + "imgbg_" + hc.rowid + extension_bg;
            hc.img_bg = "/Image/" + fileName_bg;
            fileName_bg = Path.Combine(Server.MapPath("~/Image/"), fileName_bg);
            hc.ImageFile_bg.SaveAs(fileName_bg);

            hc.ImageFile_bg = ImageFile_bg;
            hc.context_computer = context1;
            hc.context_phone = context2;

            db.Home_Carousel.Add(hc);
            db.SaveChanges();

            return RedirectToAction("Home_config");

        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Home_configDelete(int id)
        {
            var hc = db.Home_Carousel.Where(m => m.rowid == id).FirstOrDefault();
            db.Home_Carousel.Remove(hc);
            db.SaveChanges();
            return RedirectToAction("Home_config");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Home_configEdit(int id,string context1,string context2, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageFile_bg)
        {
            var hc = db.Home_Carousel.Where(m => m.rowid == id).FirstOrDefault();
            hc.ImageFile = ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(hc.ImageFile.FileName);
            string extension = Path.GetExtension(hc.ImageFile.FileName);
            fileName = fileName + "img" + hc.rowid + extension;
            hc.img = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            hc.ImageFile.SaveAs(fileName);

            hc.ImageFile_bg = ImageFile_bg;
            string fileName_bg = Path.GetFileNameWithoutExtension(hc.ImageFile_bg.FileName);
            string extension_bg = Path.GetExtension(hc.ImageFile_bg.FileName);
            fileName_bg = fileName_bg + "imgbg_" + hc.rowid + extension_bg;
            hc.img_bg = "/Image/" + fileName_bg;
            fileName_bg = Path.Combine(Server.MapPath("~/Image/"), fileName_bg);
            hc.ImageFile_bg.SaveAs(fileName_bg);

            hc.ImageFile_bg = ImageFile_bg;
            hc.context_computer = context1;
            hc.context_phone = context2;

            db.SaveChanges();

            return RedirectToAction("Home_config");
        }


        public ActionResult TestAdmin() {

            return View();
        
        
        }
    
    }
}