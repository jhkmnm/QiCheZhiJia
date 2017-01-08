using System.Collections.Generic;
using System.Web.Services;
using Model;

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
            var result = dal.GetUser(user.UserName, user.Company, user.SiteName);
            if (result == null)
            {
                user.UserType = 0;
                dal.AddUser(user);
                userResult.Data = dal.GetUser(user.UserName, user.Company, user.SiteName);
                dal.UpdateLoginLogByLogin(userResult.Data.Id);
            }
            else
            {
                dal.UpdateLoginLogByLogin(result.Id);

                if (result.UserType == 0)
                {
                    if (!dal.CheckTasteTime(result.Id))
                    {
                        userResult.Result = false;
                        userResult.Message = "非常抱歉，该用户今天体验时间已到，暂时无法登陆。详询QQ：278815541。";
                    }
                    else
                    {
                        userResult.Data = result;
                    }
                }
                else
                {
                    if (!dal.CheckDueTime(user.UserName, user.Company, user.SiteName))
                    {
                        userResult.Result = false;
                        userResult.Message = "非常抱歉，该用户使用时间已到，暂时无法登陆。详询QQ：278815541。";
                    }
                    else
                    {
                        userResult.Data = result;
                    }
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
    public int UpdateUserType(int Id)
    {
        return dal.UpdateUserType(Id);
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
    public bool CheckTasteTime(int userId)
    {
        return dal.CheckTasteTime(userId);
    }
}
