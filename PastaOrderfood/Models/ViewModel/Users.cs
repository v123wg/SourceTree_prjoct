using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Windows.Input;

namespace PastaOrderfood.Models
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class Users
    {
        private class UsersMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "帳號")]
            public string mno { get; set; }
            [Display(Name = "姓名")]
            public string mname { get; set; }
            [Display(Name = "密碼")]
            public string password { get; set; }
            [Display(Name = "信箱")]
            public string email { get; set; }
            [Display(Name = "權限")]
            public string role_no { get; set; }
            [Display(Name = "生日")]
            public Nullable<System.DateTime> birthday { get; set; }
            [Display(Name = "標記")]
            public string remark { get; set; }
            [Display(Name = "驗證碼")]
            public string varify_code { get; set; }
            [Display(Name = "是否信箱驗證")]
            public Nullable<int> isvarify { get; set; }

            
        }



    }
}