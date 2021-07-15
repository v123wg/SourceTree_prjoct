using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastaOrderfood.Account;
using PastaOrderfood.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using PastaOrderfood.App_Class;

namespace PastaOrderfood.Controllers
{
    public class ProductController : Controller
    {
        PastaOrderEntities db = new PastaOrderEntities();
        int pageSize = 5;

        // GET: Product
        //[LoginAuthorize(RoleNo = "Admin")]
        // GET: Admin/Manage
        public ActionResult ProductManageIndex(int page = 1)
        {

            int currentPage = page < 1 ? 1 : page;

            #region 種類下拉式表單

            var cG = db.Categories.OrderBy(m => m.category_id).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var item in cG)
            {
                //將資料庫資料加入選擇
                selectList.Add(new SelectListItem
                {
                    Text = item.category_name,
                    Value = item.category_id.ToString()
                });
            }

            ViewBag.SelectList = selectList;
            #endregion

            var pastas = db.Pastas.Include("Categories").OrderBy(m => m.pasta_sort).ToList();
            var result = pastas.ToPagedList(currentPage, pageSize);
            switch (PageList.SearchPastasBy)
            {

                case "name":
                    pastas = db.Pastas.Include("Categories").OrderBy(m => m.rowid).Where(m => m.pasta_name.Contains(PageList.SearchPastas)).ToList();
                    result = pastas.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "category_id":
                    pastas = db.Pastas.Include("Categories").OrderBy(m => m.rowid).Where(m => m.category_id.ToString().Contains(PageList.SearchPastas)).ToList();
                    result = pastas.ToPagedList(currentPage, pageSize);
                    return View(result);
                default:
                    return View(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductManageCreate(string name, int price, int quantity,string detail,int sort, HttpPostedFileBase ImageFile, int categories_id)
        {
            Pastas c = new Pastas();
            c.ImageFile = ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(c.ImageFile.FileName);
            string extension = Path.GetExtension(c.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            c.pasta_img = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            c.ImageFile.SaveAs(fileName);
            c.pasta_name = name;
            c.category_id = categories_id;
            c.pasta_price = price;
            c.pasta_quantity = quantity;
            c.pasta_detail = detail;
            c.pasta_sort = sort;
            db.Pastas.Add(c);
            db.SaveChanges();

            return RedirectToAction("ProductManageIndex");
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductManageDelete(int rowid)
        {
            var pasta = db.Pastas.Where(m => m.rowid == rowid).FirstOrDefault();
            db.Pastas.Remove(pasta);
            db.SaveChanges();
            return RedirectToAction("ProductManageIndex");
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductManageEdit(int rowid)
        {
            #region 接單下拉式表單

            var cG = db.Categories.OrderBy(m => m.category_id).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var item in cG)
            {
                //將資料庫資料加入選擇
                selectList.Add(new SelectListItem
                {
                    Text = item.category_name,
                    Value = item.category_id.ToString()
                });
            }

            ViewBag.SelectList = selectList;
            #endregion
            var user = db.Pastas.Where(m => m.rowid == rowid).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductManageEdit(Pastas c,int categories_id)
        {
            if (!ModelState.IsValid) return View(c);
            string fileName = Path.GetFileNameWithoutExtension(c.ImageFile.FileName);
            string extension = Path.GetExtension(c.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            c.pasta_img = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            c.ImageFile.SaveAs(fileName);
            c.category_id = categories_id;
            int rowid = c.rowid;
            var pastas = db.Pastas.Where(m => m.rowid == rowid).FirstOrDefault();
            pastas.rowid = c.rowid;
            pastas.pasta_name = c.pasta_name;
            pastas.category_id = c.category_id;
            pastas.pasta_quantity = c.pasta_quantity;
            pastas.pasta_detail = c.pasta_detail;
            pastas.pasta_price = c.pasta_price;
            pastas.pasta_img = c.pasta_img;
            pastas.ImageFile = c.ImageFile;
            db.SaveChanges();
            return RedirectToAction("ProductManageIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductManageDeleteSelected(int[] rowid)
        {
            if (rowid == null)
                return RedirectToAction("ProductManageIndex");
            int delfId;
            for (int i = 0; i < rowid.Length; i++)
            {
                delfId = rowid[i];
                var customer = db.Pastas.Where(m => m.rowid == delfId)
                    .FirstOrDefault();
                db.Pastas.Remove(customer);
            }
            db.SaveChanges();
            return RedirectToAction("ProductManageIndex");
        }


        [HttpPost]
        //[LoginAuthorize(RoleNo = "Admin")]
        public JsonResult GetProductSearchData(string SearchBy, string SearchValue)
        {
            PageList.SearchPastasBy = SearchBy;
            PageList.SearchPastas = SearchValue;
            return Json("");
        }

        public ActionResult ProductCategoriesIndex()
        {
            var categories = db.Categories.OrderBy(m => m.category_id).ToList();
            return View(categories);
        }
        public ActionResult ProductCategoriesCreate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ProductCategoriesCreate(string CategoriesName)
        {
            Categories c = new Categories();
            c.category_name = CategoriesName;
            db.Categories.Add(c);
            db.SaveChanges();

            return RedirectToAction("ProductCategoriesIndex");
        }
        public ActionResult ProductCategoriesDelete(int category_id)
        {
            var categories = db.Categories.Where(m => m.category_id == category_id).FirstOrDefault();
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("ProductCategoriesIndex");
        }
        [HttpPost]
        public ActionResult ProductCategoriesEdit(int category_id,string newCategoriesName)
        {
            Categories c = new Categories();
            c.category_name = newCategoriesName;
            var categories = db.Categories.Where(m => m.category_id == category_id).FirstOrDefault();
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoriesEdit(Categories c)
        {
            int category_id = c.category_id;
            var categories = db.Categories.Where(m => m.category_id == category_id).FirstOrDefault();
            categories.category_name = c.category_name;

            db.SaveChanges();
            return RedirectToAction("ProductCategoriesIndex");
        }



    }
}