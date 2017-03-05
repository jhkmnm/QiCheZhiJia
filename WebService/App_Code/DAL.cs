using Dos.ORM;
using System;
using System.Collections.Generic;
using Model;
using Dos.Common;
using System.Linq;

public class DAL
{
    #region User

    public List<User> GetUserList(int userType, int status, int due)
    {
        var where = new Where<User>();
        if (userType >= 0)
        {
            where.And(d => d.UserType == userType);
        }
        if (status >= 0)
        {
            where.And(d => d.Status == status);
        }
        if (due == 0)//0到期 1未到期
        {            
            where.And(d => d.DueTime > DateTime.Now);
        }
        else if (due == 1)
        {
            where.And(d => d.DueTime <= DateTime.Now);
        }

        return DB.Context.From<User>()
            .Where(where)
            .ToList();
    }

    public User GetUser(string userName, string company, string sitename)
    {
        var where = new Where<User>();
        if (!string.IsNullOrWhiteSpace(userName))
        {
            where.And(d => d.UserName == userName);
        }
        if (!string.IsNullOrWhiteSpace(company))
        {
            where.And(d => d.Company == company);
        }
        if (!string.IsNullOrWhiteSpace(sitename))
        {
            where.And(d => d.SiteName == sitename);
        }

        return GetSingleUser(where);
    }

    public User CheckUser(string userName, string company, string sitename)
    {
        var where = new Where<User>();
        if (!string.IsNullOrWhiteSpace(userName))
        {
            where.And(d => d.UserName != userName);
        }
        if (!string.IsNullOrWhiteSpace(company))
        {
            where.And(d => d.Company == company);
        }
        if (!string.IsNullOrWhiteSpace(sitename))
        {
            where.And(d => d.SiteName == sitename);
        }

        return GetSingleUser(where);
    }

    private User GetSingleUser(Where<User> where)
    {
        return DB.Context.From<User>()
            .Where(where)
            .ToFirst();
    }

    public User GetUser(int userId)
    {
        var where = new Where<User>();
        where.And(d => d.Id == userId);

        return GetSingleUser(where);
    }

    public int AddUser(User user)
    {
        try
        {
            return DB.Context.Insert(user);
        }
        catch(Exception ex)
        {
            LogHelper.Debug(ex.StackTrace + ex.Message, "日志");
            return 0;
        }
    }

    /// <summary>
    /// 更新最后分配时间
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public int UpdateLastAllotTime(int Id)
    {
        return DB.Context.Update<User>(User._.LastAllotTime, DateTime.Now, User._.Id == Id);
    }

    /// <summary>
    /// 更新最后报价时间
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public int UpdateLastQuoteTime(int Id)
    {
        return DB.Context.Update<User>(User._.LastQuoteTime, DateTime.Now, User._.Id == Id);
    }

    /// <summary>
    /// 更新最后资讯时间
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public int UpdateLastNewsTime(int Id)
    {
        return DB.Context.Update<User>(User._.LastNewsTime, DateTime.Now, User._.Id == Id);
    }

    /// <summary>
    /// 修改用户类型
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public int UpdateUserType(int Id, bool sendOrder, bool query, bool news)
    {
        Dictionary<Field, object> upvalue = new Dictionary<Field, object>();
        upvalue.Add(User._.UserType, 1);
        upvalue.Add(User._.SendOrder, sendOrder);
        upvalue.Add(User._.Query, query);
        upvalue.Add(User._.News, news);
        return DB.Context.Update<User>(upvalue, User._.Id == Id);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public int DelUser(int Id)
    {
        return DB.Context.Delete<User>(d => d.Id == Id);
    }

    /// <summary>
    /// 检查过期时间
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="company"></param>
    /// <returns>true 有效 false 过期</returns>
    public bool CheckDueTime(string userName, string company, string sitename)
    {
        var user = GetUser(userName, company, sitename);

        return user.DueTime > DateTime.Now;
    }

    /// <summary>
    /// 更新到期时间
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dueTime"></param>
    /// <returns></returns>
    public int UpdUserDueTime(int id, DateTime dueTime)
    {
        return DB.Context.Update<User>(User._.DueTime, dueTime, User._.Id == id);
    }
    #endregion

    #region Dict
    public List<Dictionaries> GetDic()
    {
        return DB.Context.From<Dictionaries>().ToList();
    }

    public int UpdateDic(string key, string value)
    {
        return DB.Context.Update<Dictionaries>(Dictionaries._.Value, value, Dictionaries._.Key == key);
    }

    public Dictionaries GetDic(string key)
    {
        var where = new Where<Dictionaries>();
        where.And(d => d.Key == key);
        return DB.Context.From<Dictionaries>()
            .Where(where)
            .ToFirst();
    }
    #endregion

    #region LoginLog
    public DateTime? UpdateLoginLogByLogin(int userId)
    {
        var today = DateTime.Today.ToShortDateString();

        var where = new Where<LoginLog>();
        where.And(d => d.UserId == userId);
        where.And(d => d.ToDay == today);

        var log = DB.Context.From<LoginLog>()
            .Where(where).ToFirst();

        var user = GetUser(userId);
        user.Status = 1;
        
        int result = 0;
        if (log == null)
        {
            var duetime = GetDic("体验时间");
            user.DueTime = DateTime.Now.AddHours(Convert.ToDouble(duetime.Value));
            log = new LoginLog
            {
                UserId = userId,
                ToDay = today,
                LastLoginTime = DateTime.Now
            };

            result = DB.Context.Insert(log);
        }
        else
        {
            log.LastLoginTime = DateTime.Now;
            result = DB.Context.Update(log);
        }
        DB.Context.Update(user);
        return user.DueTime;
    }

    public int UpdateLoginLogByLogOut(int userId)
    {
        var today = DateTime.Today.ToShortDateString();

        var where = new Where<LoginLog>();
        where.And(d => d.UserId == userId);
        where.And(d => d.ToDay == today);

        var user = GetUser(userId);
        user.Status = 0;
        DB.Context.Update(user);

        var log = DB.Context.From<LoginLog>()
            .Where(where).ToFirst();

        if(log != null)
        {
            log.LoginTime = ((log.LoginTime.HasValue ? log.LoginTime.Value : 0) + (float)(DateTime.Now - log.LastLoginTime).TotalHours);            
            return DB.Context.Update(log);
        }
        return 0;
    }

    //public int UpdateLoginLogByAss(int userId)
    //{
    //    var today = DateTime.Today.ToShortDateString();        

    //    var where = new Where<LoginLog>();
    //    where.And(d => d.UserId == userId);
    //    where.And(d => d.ToDay == today);

    //    var log = DB.Context.From<LoginLog>()
    //        .Where(where).ToFirst();

    //    log.LoginTime += (float)(DateTime.Now - log.LastLoginTime).TotalHours;
    //    return DB.Context.Update(log);
    //}
    public float GetLoginTime(int userId)
    {
        var today = DateTime.Today.ToShortDateString();
        var user = DB.Context.From<LoginLog>()
            .Where(w => w.UserId == userId && w.ToDay == today)
            .Select(s => s.LoginTime).First();
        return user.LoginTime.Value;
    }

    /// <summary>
    /// 检查体验时间        /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>true 还在体验期内 false 过期 </returns>
    public bool CheckTasteTime(int userId)
    {
        var today = DateTime.Today.ToShortDateString();
        var duetime = GetDic("体验时间");

        var where = new Where<LoginLog>();
        where.And(d => d.UserId == userId);
        where.And(d => d.ToDay == today);

        var log = DB.Context.From<LoginLog>()
            .Where(where).ToFirst();

        return Convert.ToInt32(duetime.Value) > ((log.LoginTime.HasValue ? log.LoginTime.Value : 0) + (float)(DateTime.Now - log.LastLoginTime).TotalHours);
    }

    /// <summary>
    /// 获取指定用户剩余报价和新闻次数
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public int[] GetQueryAndNewsNum(int userId)
    {        
        var queryDic = GetDic("报价次数");
        var newDic = GetDic("发布新闻次数");

        var log = GetJobLogByUser(userId);
        var queryNum = Convert.ToInt32(queryDic.Value) - log.Where(w => w.JobType == "报价" && w.JobTime.Value.Date.Equals(DateTime.Now.Date)).Count();
        var newNum = Convert.ToInt32(newDic.Value) - log.Where(w => w.JobType == "资讯" && w.JobTime.Value.Date.Equals(DateTime.Now.Date)).Count();
        return new int[] { queryNum, newNum };
    }
    #endregion

    #region JobLog
    public int AddJobLog(JobLog log)
    {
        return DB.Context.Insert(log);
    }

    public int DelJobLog(int userId)
    {
        return DB.Context.Delete<JobLog>(d => d.UserID == userId);
    }

    public List<JobLog> GetJobLogByUser(int userId)
    {
        return DB.Context.From<JobLog>()
            .Where(w => w.UserID == userId)
            .ToList();
    }
    #endregion
}