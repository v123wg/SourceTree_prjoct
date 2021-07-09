using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastaOrderfood.App_Class
{
    public class CartItem
    {

        public static int CartCount
        {
            get { return (HttpContext.Current.Session["CartCount"] == null) ? 1 : (int)(HttpContext.Current.Session["CartCount"]); }
            set { HttpContext.Current.Session["CartCount"] = value; }
        }
    }
}