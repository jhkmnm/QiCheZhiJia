using Dos.ORM;
using System;
using System.Collections.Generic;
using Model;

public class DAL
{
    #region Area
    public List<Area> GetProvince()
    {
        return DB.Context.From<Area>()
            .Select(Area._.ProId, Area._.Pro)
            .Distinct()
            .ToList();
    }

    public List<Area> GetCity(string proId)
    {
        var where = new Where<Area>();
        where.And(w => w.ProId == proId);

        return DB.Context.From<Area>()
            .Where(where)
            .ToList();
    }
    #endregion

    #region Nicks
    public int AddNicks(Nicks data)
    {
        var nick = DB.Context.From<Nicks>()
            .Where(w => w.Id == data.Id)
            .ToFirst();

        if(nick == null)
            return DB.Context.Insert(data);
        return 0;
    }

    public int UpdateSendCount(string Id)
    {
        var uptModel = DB.Context.From<Nicks>().Where(d => d.Id == Id).First();

        if (uptModel != null)
        {
            uptModel.Send += 1;
            return DB.Context.Update<Nicks>(uptModel);
        }
        return 0;
    }
    #endregion

    #region Orders
    public int AddOrders(Orders order)
    {
        var o = DB.Context.From<Orders>()
            .Where(w => w.Id == order.Id)
            .ToFirst();

        if(o == null)
        {
            DB.Context.Insert(order);
        }
        return 0;
    }

    public int UpdateOrderSend(int orderId, string nickId)
    {
        var uptModel = DB.Context.From<Orders>().Where(d => d.Id == orderId).First();
        if (uptModel != null)
        {
            uptModel.SendTo = nickId;
            uptModel.SendTime = DateTime.Now;
            return DB.Context.Update(uptModel);
        }
        return 0;
    }
    #endregion

    #region SendLog
    public int AddSendLog(string nickId, int count)
    {
        var sendtime = DateTime.Now.ToString("yyyy-MM-dd");
        var uptModel = DB.Context.From<SendLog>().Where(d => d.NickID == nickId && d.SendTime == sendtime).First();
        if (uptModel != null)
        {
            uptModel.OrderCount += count;
            return DB.Context.Update<SendLog>(uptModel);
        }
        else
        {
            return DB.Context.Insert<SendLog>(new SendLog { NickID = nickId, OrderCount = count, SendTime = sendtime });
        }
    }

    public List<SendLog> GetTodaySendLog()
    {
        var sendtime = DateTime.Now.ToString("yyyy-MM-dd");
        return DB.Context.From<SendLog>()
            .Where(w => w.SendTime == sendtime)
            .ToList();
    }
    #endregion

    #region Job
    public int AddJob(Job job)
    {
        var uptModel = DB.Context.From<Job>().Where(a => a.JobName == job.JobName).ToFirst();
        if(uptModel == null)
        {
            return DB.Context.Insert(job);
        }
        else
        {
            uptModel.Time = job.Time;
            return DB.Context.Update(uptModel);
        }
    }

    public Job GetJob(string jobName)
    {
        return DB.Context.From<Job>().Where(a => a.JobName == jobName).ToFirst();
    }
    #endregion

    #region JobLog
    public int AddJobLog(JobLog log)
    {
        return DB.Context.Insert(log);
    }

    public JobLog GetJobLog(string jobName)
    {
        return DB.Context.From<JobLog>()
            .Select(JobLog._.All, Job._.JobName)
            .InnerJoin<Job>((a, b) => a.JobID == b.ID)
            .Where<Job>((a, b) => b.JobName == jobName && a.Time.PadLeft(10) == DateTime.Today.ToString("yyyy-MM-dd"))
            .ToFirst();
    }
    #endregion

    #region News
    public int AddNews(string title, string content)
    {
        return DB.Context.Insert(new News { Title = title, Content = content});
    }

    public List<News> GetNewsList()
    {
        return DB.Context.From<News>().ToList();
    }

    public News GetNews(int id)
    {
        return DB.Context.From<News>()
            .Where(w => w.ID == id)
            .ToFirstDefault();            
    }

    public int DelNews(int id)
    {
        return DB.Context.Delete<News>(s => s.ID == id);
    }
    #endregion
}