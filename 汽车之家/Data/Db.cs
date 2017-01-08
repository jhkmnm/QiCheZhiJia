﻿using Dos.Common;
using Dos.ORM;

class DB
{
    public static readonly DbSession Context = new DbSession("AccCon");

    static DB()
    {
        Context.RegisterSqlLogger(delegate(string sql)
        {
#if DEBUG
                //在此可以记录sql日志
                //写日志会影响性能，建议开发版本记录sql以便调试，发布正式版本不要记录
                LogHelper.Debug(sql, "SQL日志");
#endif
        });
    }
}
