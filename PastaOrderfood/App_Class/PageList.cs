using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastaOrderfood.App_Class
{
    public class PageList
    {
        public static int PageNo
        {
            get { return (HttpContext.Current.Session["PageNo"] == null) ? 1 : (int)(HttpContext.Current.Session["PageNo"]); }
            set { HttpContext.Current.Session["PageNo"] = value; }
        }
        public static int PageSize
        {
            get { return (HttpContext.Current.Session["PageSize"] == null) ? 1 : (int)(HttpContext.Current.Session["PageSize"]); }
            set { HttpContext.Current.Session["PageSize"] = value; }
        }
        public static string SearchMember
        {
            get { return (HttpContext.Current.Session["SearchMember"] == null) ? "" : HttpContext.Current.Session["SearchMember"].ToString(); }
            set { HttpContext.Current.Session["SearchMember"] = value; }
        }
        public static string SearchMemberBy
        {
            get { return (HttpContext.Current.Session["SearchMemberBy"] == null) ? "" : HttpContext.Current.Session["SearchMemberBy"].ToString(); }
            set { HttpContext.Current.Session["SearchMemberBy"] = value; }
        }
        public static string SearchPastas
        {
            get { return (HttpContext.Current.Session["SearchPastas"] == null) ? "" : HttpContext.Current.Session["SearchPastas"].ToString(); }
            set { HttpContext.Current.Session["SearchPastas"] = value; }
        }
        public static string SearchPastasBy
        {
            get { return (HttpContext.Current.Session["SearchPastasBy"] == null) ? "" : HttpContext.Current.Session["SearchPastasBy"].ToString(); }
            set { HttpContext.Current.Session["SearchPastasBy"] = value; }
        }
        public static string SearchOrder
        {
            get { return (HttpContext.Current.Session["SearchOrder"] == null) ? "" : HttpContext.Current.Session["SearchOrder"].ToString(); }
            set { HttpContext.Current.Session["SearchOrder"] = value; }
        }
        public static string SearchOrderBy
        {
            get { return (HttpContext.Current.Session["SearchOrderBy"] == null) ? "" : HttpContext.Current.Session["SearchOrderBy"].ToString(); }
            set { HttpContext.Current.Session["SearchOrderBy"] = value; }
        }
    }
}