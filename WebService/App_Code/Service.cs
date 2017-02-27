using System.Collections.Generic;
using System.Web.Services;
using Model;
using System;

/// <summary>
/// Service 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    DAL dal = new DAL();

    public Service()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 用户登录
    /// 1. 判断用户是否存在
    /// 2. 不存在写入，返回
    /// 3. 存在
    ///     a. 判断是否过期，过期返回
    ///     b. 判断是否体验过期，过期返回
    ///     c. 更新登录日志
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [WebMethod]
    public UserResult UserLogin(User user)
    {
        var userResult = new UserResult();
        userResult.Message = "";
        userResult.Result = true;

        var ruser = dal.CheckUser(user.UserName, user.Company, user.SiteName);
        if (ruser != null)
        {
            userResult.Result = false;
            userResult.Message = "非常抱歉，贵公司已有用户使用该软件，暂时无法登陆。详询QQ：278815541。";
        }
        else
        {
            userResult.Data = dal.GetUser(user.UserName, user.Company, user.SiteName);
            if (userResult.Data == null)
            {
                user.UserType = 0;
                dal.AddUser(user);
                userResult.Data = dal.GetUser(user.UserName, user.Company, user.SiteName);
            }

            dal.UpdateLoginLogByLogin(userResult.Data.Id);

            if (userResult.Data.UserType == 0)
            {
                //如果是试用用户获取剩余的报价和新闻次数
                var num = dal.GetQueryAndNewsNum(userResult.Data.Id);
                userResult.Data.QueryNum = num[0];
                userResult.Data.NewsNum = num[1];
            }
            else
            {
                if (!dal.CheckDueTime(user.UserName, user.Company, user.SiteName))
                {
                    userResult.Result = false;
                    userResult.Message = "非常抱歉，该用户使用时间已到，暂时无法登陆。详询QQ：278815541。";
                }
            }
        }

        return userResult;
    }

    [WebMethod]
    public List<User> GetUserList(int userType, int status, int due)
    {
        return dal.GetUserList(userType, status, due);
    }

    //[WebMethod]
    //public User GetUser(string userName, string company)
    //{
    //    return dal.GetUser(userName, company);
    //}

    //[WebMethod]
    //public User GetUserById(int userId)
    //{
    //    return dal.GetUser(userId);
    //}

    //[WebMethod]
    //public int AddUser(User user)
    //{
    //    return dal.AddUser(user);
    //}

    [WebMethod]
    public int UpdateLastAllotTime(int Id)
    {
        return dal.UpdateLastAllotTime(Id);
    }

    [WebMethod]
    public int UpdateUserType(int Id, bool sendOrder, bool query, bool news)
    {
        return dal.UpdateUserType(Id, sendOrder, query, news);
    }

    [WebMethod]
    public int DelUser(int Id)
    {
        return dal.DelUser(Id);
    }

    //[WebMethod]
    //public bool CheckDueTime(string userName, string company)
    //{
    //    return dal.CheckDueTime(userName, company);
    //}

    [WebMethod]
    public List<Dictionaries> GetDic()
    {
        return dal.GetDic();
    }

    [WebMethod]
    public int UpdateDic(string key, string value)
    {
        return dal.UpdateDic(key, value);
    }

    [WebMethod]
    public int UpdateLoginLogByLogin(int userId)
    {
        return dal.UpdateLoginLogByLogin(userId);
    }

    [WebMethod]
    public int UpdateLoginLogByLogOut(int userId)
    {
        return dal.UpdateLoginLogByLogOut(userId);
    }

    [WebMethod]
    public bool CheckTasteTime(int userId)
    {
        return dal.CheckTasteTime(userId);
    }

    [WebMethod]
    public int UpdUserDueTime(int id, DateTime dueTime)
    {
        return dal.UpdUserDueTime(id, dueTime);
    }

    [WebMethod]
    public int UpdateLastQuoteTime(int Id)
    {
        return dal.UpdateLastQuoteTime(Id);
    }

    [WebMethod]
    public int UpdateLastNewsTime(int Id)
    {
        return dal.UpdateLastNewsTime(Id);
    }

    [WebMethod]
    public int AddJobLog(JobLog log)
    {
        return dal.AddJobLog(log);
    }

    [WebMethod]
    public int DelJobLog(int userId)
    {
        return dal.DelJobLog(userId);
    }

    [WebMethod]
    public List<JobLog> GetJobLogByUser(int userId)
    {
        return dal.GetJobLogByUser(userId);
    }
}
