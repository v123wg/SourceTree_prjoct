using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastaOrderfood.App_Class
{
    public class backSetting
    {
        public static int NavOn
        {
            get { return (HttpContext.Current.Session["NavOn"] == null) ? 0 : (int)(HttpContext.Current.Session["NavOn"]); }
            set { HttpContext.Current.Session["NavOn"] = value; }
        }
    }
}