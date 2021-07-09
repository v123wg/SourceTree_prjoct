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

    }
}