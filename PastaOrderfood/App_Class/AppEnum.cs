using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// 枚舉類型類別

public static class AppEnum
{

    // 使用者角色枚舉類型

    public enum enUserRole
    {

        // 會員角色
        Member = 0,
        // 管理者角色
        Admin = 1,
        // 員工 (尚未使用)
        Employee = 2,
        // 訪客
        Guest = 3
    }
}


