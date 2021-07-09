using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PastaOrderfood.Account
{
    public static class UserAccount
    {
        public static bool IsLogin {
            get { return (HttpContext.Current.Session["IsLogin"] == null) ? false : (bool)HttpContext.Current.Session["IsLogin"]; }
            set { HttpContext.Current.Session["IsLogin"] = value; }
        }
        public static string UserNo {
            get { return (HttpContext.Current.Session["UserNo"] == null) ? "" : HttpContext.Current.Session["UserNo"].ToString(); }
            set { HttpContext.Current.Session["UserNo"] = value; }
        }
        public static AppEnum.enUserRole RoleNo {
            get { return (HttpContext.Current.Session["RoleNo"] == null) ? AppEnum.enUserRole.Guest : (AppEnum.enUserRole)HttpContext.Current.Session["RoleNo"]; }
            set { HttpContext.Current.Session["RoleNo"] = value; }
        }
        public static string RoleName {get { return Enum.GetName(typeof(AppEnum.enUserRole), UserAccount.RoleNo); } }
        public static string UserName {
            get { return (HttpContext.Current.Session["UserName"] == null) ? "未登入" : HttpContext.Current.Session["UserName"].ToString(); }
            set { HttpContext.Current.Session["UserName"] = value; }

        }

        public static AppEnum.enUserRole GetRoleNo(string roleNo)
        {
            AppEnum.enUserRole roleUser = AppEnum.enUserRole.Guest;
            Enum.TryParse(roleNo, true, out roleUser);
            return roleUser;
        }

        public static int UserStatus
        {
            get
            {
                int int_value = 0;
                if (HttpContext.Current.Session["UserStatus"] != null)
                {
                    string str_value = HttpContext.Current.Session["UserStatus"].ToString();
                    if (!int.TryParse(str_value, out int_value)) int_value = 0;
                }
                return int_value;
            }
            set
            { HttpContext.Current.Session["UserStatus"] = value; }
        }

        public static int UserCode
        {
            get
            {
                int int_value = -1;
                if (HttpContext.Current.Session["UserCode"] != null)
                {
                    string str_value = HttpContext.Current.Session["UserCode"].ToString();
                    if (!int.TryParse(str_value, out int_value)) int_value = -1;
                }
                return int_value;
            }
            set
            { HttpContext.Current.Session["UserCode"] = value; }
        }

        public static string UserRoleNo
        {
            get { return (HttpContext.Current.Session["UserRoleNo"] == null) ? "Guest" : HttpContext.Current.Session["UserRoleNo"].ToString(); }
            set { HttpContext.Current.Session["UserRoleNo"] = value; }
        }


        public static void Login(string userNo, string userName, AppEnum.enUserRole roleNo)
        {
            UserNo = userNo;
            UserName = userName;
            RoleNo = roleNo;
            IsLogin = true;
        }

        public static void LogOut()
        {
            UserNo = "";
            UserName = "";
            RoleNo = AppEnum.enUserRole.Guest;
            IsLogin = false;
        }
        public static string GetNewVarifyCode()
        {
            return Guid.NewGuid().ToString().ToUpper(); //產生驗證碼
        }




    }
}