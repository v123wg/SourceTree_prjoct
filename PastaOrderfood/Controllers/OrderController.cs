using PagedList;
using PastaOrderfood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PastaOrderfood.Account;
using PastaOrderfood.App_Class;

namespace PastaOrderfood.Controllers
{
    public class OrderController : Controller
    {
        PastaOrderEntities db = new PastaOrderEntities();
        int pageSize = 5;
        // GET: Order
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderIndex(int page = 1)
        {

            int currentPage = page < 1 ? 1 : page;
            var order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).ToList();
            var result = order.ToPagedList(currentPage, pageSize);
            switch (PageList.SearchOrderBy)
            {

                case "id":
                    order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).Where(m => m.order_id.ToString().Contains(PageList.SearchOrder)).ToList();
                    result = order.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "name":
                    order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).Where(m => m.order_name.Contains(PageList.SearchOrder)).ToList();
                    result = order.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "phone":
                    order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).Where(m => m.order_phone.Contains(PageList.SearchOrder)).ToList();
                    result = order.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "email":
                    order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).Where(m => m.order_email.Contains(PageList.SearchOrder)).ToList();
                    result = order.ToPagedList(currentPage, pageSize);
                    return View(result);
                case "isLogin":
                    order = db.Order.Include("OrderDetail").OrderBy(m => m.order_id).Where(m => m.isLogin.ToString().Contains(PageList.SearchOrder)).ToList();
                    result = order.ToPagedList(currentPage, pageSize);
                    return View(result);
                default:
                    return View(result);
            }


        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderCreate()
        {
            return View();
        }
        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderCreate(Order o)
        {
            if (!ModelState.IsValid) return View(o);
            db.Order.Add(o);
            db.SaveChanges();

            return RedirectToAction("OrderIndex");
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderDelete(int id)
        {
            var order = db.Order.Include("OrderDetail").Where(m => m.order_id == id).FirstOrDefault();
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("OrderIndex");
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderEdit(int id)
        {
            var order = db.Order.Where(m => m.order_id == id).FirstOrDefault();
            return View(order);
        }
        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderEdit(Order o)
        {
            int order_id = o.order_id;
            var order = db.Order.Where(m => m.order_id == order_id).FirstOrDefault();
            order.order_id = o.order_id;
            order.order_name = o.order_name;
            db.SaveChanges();
            return RedirectToAction("OrderIndex");
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult OrderDetailIndex(int id)
        {
            int count = 0;
            ViewBag.temp = "";
            List<Cart> C = new List<Cart>();
            string UN = UserAccount.UserName;
            var UOrderDetail = db.OrderDetail.Include("Order").Where(m => m.orderid == id).ToList();
            foreach (var item in UOrderDetail)
            {
                var i = db.Pastas.Where(m => m.rowid == item.itemId).ToList();
                C.Add(new Cart()
                {
                    pasta_name = i[count].pasta_name,
                    quantity = (int)item.quantity,
                    unitprice = (int)i[count].pasta_price,
                    total = (int)item.Order.order_total
                });

            }

            ViewBag.temp = C;
            return View(UOrderDetail);
        }
        [LoginAuthorize(RoleNo = "Admin,Member")]
        public ActionResult UOrderIndex(int page = 1)
        {
            string UN= UserAccount.UserName;
            int currentPage = page < 1 ? 1 : page;
            var UOrder = db.Order.Include("OrderDetail").Where(m => m.order_name == UN && m.isLogin == 1).ToList();
            var result = UOrder.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        [LoginAuthorize(RoleNo = "Admin,Member")]
        public ActionResult UOrderDetailIndex(int id )
        {
            int count = 0;
            ViewBag.temp = "";
            List <Cart>  C = new List<Cart>();
            string UN = UserAccount.UserName;
            var UOrderDetail = db.OrderDetail.Include("Order").Where(m => m.orderid == id).ToList();
            foreach (var item in UOrderDetail)
            {
                var i = db.Pastas.Where(m => m.rowid == item.itemId).ToList();
                C.Add(new Cart()
                {
                    pasta_name = i[count].pasta_name,
                    quantity = (int)item.quantity,
                    unitprice =(int)i[count].pasta_price,
                    total = (int)item.Order.order_total
                }) ;
            }

            ViewBag.temp = C;
            return View(UOrderDetail);
        }



        public JsonResult GetOrderSearchData(string SearchBy, string SearchValue) {
            PageList.SearchOrderBy = SearchBy;
            PageList.SearchOrder = SearchValue;
            return Json("");
        }

    }
}