using PastaOrderfood.Account;
using PastaOrderfood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using PastaOrderfood.App_Class;

namespace PastaOrderfood.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        PastaOrderEntities db = new PastaOrderEntities();
        int pageSize = 5;

        [LoginAuthorize(RoleNo = "Admin")]
        // GET: Admin/Manage
        public ActionResult UserManageIndex(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var user = db.Users.OrderBy(m => m.rowid).ToList();
            var result = user.ToPagedList(currentPage, pageSize);
            switch (PageList.SearchMemberBy)
            {

                case "mno":
                    user = db.Users.OrderBy(m => m.rowid).Where(m => m.mno.Contains(PageList.SearchMember)).ToList();
                    result = user.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "mname":
                    user = db.Users.OrderBy(m => m.rowid).Where(m => m.mname.Contains(PageList.SearchMember)).ToList();
                    result = user.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "email":
                    user = db.Users.OrderBy(m => m.rowid).Where(m => m.email.Contains(PageList.SearchMember)).ToList();
                    result = user.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "role_no":
                    user = db.Users.OrderBy(m => m.rowid).Where(m => m.role_no.Contains(PageList.SearchMember)).ToList();
                    result = user.ToPagedList(currentPage, pageSize);
                    return View(result);
                default:
                    return View(result);
            }

        }
        //PageList.SearchText

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageCreate()
        {
            return View();
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageCreate(Users c)
        {
            if (!ModelState.IsValid) return View(c);
            db.Users.Add(c);
            db.SaveChanges();

            return RedirectToAction("UserManageIndex");
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageDelete(int rowid)
        {
            var user = db.Users.Where(m => m.rowid == rowid).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserManageIndex");
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageEdit(int rowid)
        {
            var user = db.Users.Where(m => m.rowid == rowid).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageEdit(Users c)
        {
            int rowid = c.rowid;
            var user = db.Users.Where(m => m.rowid == rowid).FirstOrDefault();
            user.mno = c.mno;
            user.mname = c.mname;
            user.password = c.password;
            user.email = c.email;
            user.role_no = c.role_no;
            user.birthday = c.birthday;
            user.remark = c.remark;
            user.isvarify = c.isvarify;
            db.SaveChanges();
            return RedirectToAction("UserManageIndex");
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult UserManageDeleteSelected(int[] rowid)
        {
            if (rowid == null)
                return RedirectToAction("UserManageIndex");
            int delfId;
            for (int i = 0; i < rowid.Length; i++)
            {
                delfId = rowid[i];
                var customer = db.Users.Where(m => m.rowid == delfId)
                    .FirstOrDefault();
                db.Users.Remove(customer);
            }
            db.SaveChanges();
            return RedirectToAction("UserManageIndex");
        }



        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public JsonResult GetSearchData(string SearchBy, string SearchValue)
        {
            PageList.SearchMember = SearchValue;
            PageList.SearchMemberBy = SearchBy;
            return Json("");
        }



    }
}