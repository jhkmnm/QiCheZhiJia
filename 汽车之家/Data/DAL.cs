using Dos.ORM;
using System;
using System.Collections.Generic;
using Model;
using System.Linq;

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

    public List<Area> GetArea()
    {
        return DB.Context.From<Area>()
            .Where(w => w.IsChecked)
            .ToList();
    }

    public int UpdateAreaChecked(List<Area> area)
    {
        return DB.Context.Update(area);
    }

    public bool CityIsChecked(string city, string city2)
    {
        var v = DB.Context.From<Area>().Where(w => (w.City == city || w.City == city2));
        return v.Count() > 0;
    }
    #endregion

    #region Nicks
    /// <summary>
    /// 删除原有人员，写入新人员
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int AddNicks(List<Nicks> data)
    {
        DB.Context.DeleteAll<Nicks>();

        return DB.Context.Insert<Nicks>(data);
    }

    public List<Nicks> GetNicks()
    {
        return DB.Context.From<Nicks>().ToList();
    }

    /// <summary>
    /// 修改人员的选中状态
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int UpdateNickChecked(string id)
    {
        var model = DB.Context.From<Nicks>().Where(w => w.Id == id).First();
        model.Check = !model.Check;
        return DB.Context.Update<Nicks>(model);
    }

    /// <summary>
    /// 获取当前选中并且Send=0的人员
    /// </summary>
    /// <returns></returns>
    public Nicks GetNick()
    {
        var v = DB.Context.From<Nicks>().Where(w => w.Check && w.Send == 0);
        if(v.Count() == 0)
        {
            var listModel = DB.Context.From<Nicks>().ToList();
            listModel.ForEach(f => f.Send = 0);
            DB.Context.Update<Nicks>(listModel);
        }
        return DB.Context.From<Nicks>().Where(w => w.Check && w.Send == 0).First();        
    }

    public int UpdateSendCount(string Id)
    {
        var uptModel = DB.Context.From<Nicks>().Where(d => d.Id == Id).First();

        if (uptModel != null)
        {
            uptModel.Send += 1;
            return DB.Context.Update<Nicks>(uptModel);
        }

        AddSendLog(Id, 1);

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

        UpdateSendCount(nickId);
        return 0;
    }

    public bool IsHaveOrder(int id)
    {
        var v = DB.Context.From<Orders>().Where(w => w.Id == id);
        return v.Count() > 0;
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
    public int AddNews(string title, string content, string url)
    {
        return DB.Context.Insert(new News { Title = title, Content = content, SendContent = url});
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

    #region OrderType
    public List<OrderType> GetOrderTypes()
    {
        return DB.Context.From<OrderType>().ToList();
    }

    public int AddOrderTypes(List<OrderType> data)
    {
        return DB.Context.Insert<OrderType>(data);
    }

    public int UpdateOrderTypeChecked(int id)
    {
        var model = DB.Context.From<OrderType>().Where(w => w.ID == id).First();
        model.IsCheck = !model.IsCheck;
        return DB.Context.Update<OrderType>(model);
    }
    #endregion

    #region Spec
    public List<Spec> GetSpecs()
    {
        return DB.Context.From<Spec>().ToList();
    }

    public int AddSpecs(List<Spec> data)
    {
        return DB.Context.Insert<Spec>(data);
    }

    public int UpdateSpecsChecked(int id)
    {
        var model = DB.Context.From<Spec>().Where(w => w.ID == id).First();
        model.IsCheck = !model.IsCheck;
        return DB.Context.Update<Spec>(model);
    }
    #endregion
}