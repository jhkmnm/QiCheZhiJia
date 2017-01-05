using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aide
{
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
            if(status >= 0)
            {
                where.And(d => d.Status == status);
            }
            if (due >= 0)
            {
                //0到期 1未到期
                where.And(d => (due == 0 && d.DueTime > DateTime.Now) || (due == 1 && d.DueTime <= DateTime.Now));
            }

            return DB.Context.From<User>()
                .Where(where)
                .ToList();
        }

        public User GetUser(string userName, string company)
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

            return DB.Context.From<User>()
                .Where(where)
                .ToFirst();
        }

        public User GetUser(int userId)
        {
            var where = new Where<User>();
            where.And(d => d.Id == userId);

            return DB.Context.From<User>()
                .Where(where)
                .ToFirst();
        }

        public int AddUser(User user)
        {
            return DB.Context.Insert(user);
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
        /// 修改用户类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int UpdateUserType(int Id)
        {
            return DB.Context.Update<User>(User._.UserType, 1, User._.Id == Id);
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
        public bool CheckDueTime(string userName, string company)
        {
            var where = new Where<User>();
            where.And(d => d.UserName == userName);
            where.And(d => d.Company == company);

            var user = DB.Context.From<User>()
                .Where(where)
                .ToFirst();

            return user.DueTime > DateTime.Now;
        }
        #endregion

        #region Dict
        public Dictionaries GetDic()
        {
            return DB.Context.From<Dictionaries>().ToFirst();
        }

        public int UpdateDic(string key, string value)
        {
            return DB.Context.Update<Dictionaries>(Dictionaries._.Value, value, Dictionaries._.Key == key);
        }
        #endregion

        #region LoginLog
        public int UpdateLoginLogByLogin(int userId)
        {
            var today = DateTime.Today.ToShortDateString();            

            var where = new Where<LoginLog>();
            where.And(d => d.UserId == userId);
            where.And(d => d.ToDay == today);

            var log = DB.Context.From<LoginLog>()
                .Where(where).ToFirst();
            
            if(log == null)
            {
                log = new LoginLog {
                    UserId = userId,
                    ToDay = today,
                    LastLoginTime = DateTime.Now                    
                };

                return DB.Context.Insert(log);
            }
            else
            {                
                log.LastLoginTime = DateTime.Now;
                return DB.Context.Update(log);
            }
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

        /// <summary>
        /// 检查体验时间        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true 还在体验期内 false 过期 </returns>
        public bool CheckTasteTime(int userId)
        {
            var today = DateTime.Today.ToShortDateString();
            var duetime = GetDic();

            var where = new Where<LoginLog>();
            where.And(d => d.UserId == userId);
            where.And(d => d.ToDay == today);

            var log = DB.Context.From<LoginLog>()
                .Where(where).ToFirst();

            return Convert.ToInt32(duetime.Value) > (log.LoginTime + (float)(DateTime.Now - log.LastLoginTime).TotalHours);
        }
        #endregion
    }
}
