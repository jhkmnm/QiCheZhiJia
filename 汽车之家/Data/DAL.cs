﻿using Dos.ORM;
using System;
using System.Collections.Generic;
using Model;
using System.Linq;

public class DAL
{
    #region Area
    public List<Area> GetArea(string site)
    {
        return DB.Context.From<Area>()
            .Where(w => w.Site == site)
            .ToList();
    }

    public List<Area> GetAreaBySiteChecked(string site)
    {
        return DB.Context.From<Area>()
            .Where(w => w.Site == site && w.IsChecked == true)
            .ToList();
    }

    public int UpdateAreaChecked(List<Area> area)
    {
        return DB.Context.Update(area);
    }

    public List<Area> GetProvince()
    {
        return DB.Context.From<Area>()
            .Select(Area._.ProId, Area._.Pro)
            .Where(w => w.Site == "Yiche")
            .Distinct()
            .ToList();
    }
    public int AddPro(string pro, string proid)
    {
        var p = DB.Context.From<Area>()
            .Where(w => w.Pro == pro && w.Site == "Yiche").First();
        if(p == null)
        {
            
        }
        else
        {            
            DB.Context.Update<Area>(Area._.ProId, proid, Area._.Site == "Yiche" && Area._.Pro == pro);
        }
        return 0;
    }

    public int AddCity(string pro, string proid, string city, string cityid)
    {
        return DB.Context.Insert(new Area { City = city, CityId = cityid, IsChecked = true, Pro = pro, ProId = proid, Site = "Yiche" });
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
        return DB.Context.Update(model);
    }

    public int UpdateNickChecked(bool check)
    {
        var listModel = DB.Context.From<Nicks>().ToList();
        foreach (var entity in listModel)
        {
            entity.Check = check;
        }
        return DB.Context.Update(listModel);
    }

    /// <summary>
    /// 获取当前选中并且Send=0的人员
    /// </summary>
    /// <returns></returns>
    public Nicks GetNick()
    {
        var list = GetNicks();
        if(!list.Any(w => w.Check && w.Send == 0))
        {
            list.ForEach(f => f.Send = 0);
            DB.Context.Update(list);
        }
        return list.Where(w => w.Check && w.Send == 0).First();
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
            DB.Context.Update(uptModel);
        }

        UpdateSendCount(nickId);
        return 0;
    }

    public bool IsHaveOrder(int id)
    {
        var v = DB.Context.From<Orders>().Where(w => w.Id == id);
        return v.Count() > 0;
    }

    public List<Orders> GetOrderList()
    {
        return DB.Context.From<Orders>().Select(s => s.Id).ToList();
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
        DelJob(job.JobName);
        return DB.Context.Insert(job);
    }

    public int UpJobExecTime(string jobName)
    {
        var uptModel = DB.Context.From<Job>().Where(a => a.JobName == jobName).ToFirst();
        if (uptModel != null)
        {
            uptModel.ExecTime = DateTime.Now.ToString();
            return DB.Context.Update(uptModel);
        }
        return 0;
    }

    public Job GetJob(string jobName)
    {
        return DB.Context.From<Job>().Where(a => a.JobName == jobName).ToFirst();
    }

    public int DelJob(string jobName)
    {
        return DB.Context.Delete<Job>(w => w.JobName == jobName);
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
            .InnerJoin<Job>((a, b) => a.JobName == b.JobName)
            .Where<Job>((a, b) => b.JobName == jobName && a.Time.PadLeft(10) == DateTime.Today.ToString("yyyy-MM-dd"))
            .ToFirst();
    }
    #endregion

    #region News
    public int AddNews(int newsid, string title, string content, string url)
    {
        var item = DB.Context.From<News>().Where(w => w.ID == newsid).First();
        if(item != null)
        {
            item.Title = title;
            item.Content = content;
            item.SendContent = url;
            DB.Context.Update(item);
            return newsid;
        }
        else
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
    public List<OrderType> GetOrderTypes(string site)
    {
        return DB.Context.From<OrderType>()
            .Where(w => w.Site == site)
            .ToList();
    }

    public int AddOrderTypes(List<OrderType> data)
    {
        return DB.Context.Insert(data);
    }

    public int UpdateOrderTypeChecked(List<OrderType> order)
    {
        return DB.Context.Update(order);
    }
    #endregion

    #region Spec
    public List<Spec> GetSpecs()
    {
        return DB.Context.From<Spec>().ToList();
    }

    public int AddSpecs(List<Spec> data)
    {
        return DB.Context.Insert(data);
    }

    public int UpdateSpecsChecked(List<Spec> spec)
    {
        return DB.Context.Update(spec);
        //var model = DB.Context.From<Spec>().Where(w => w.ID == id).First();
        //model.IsCheck = !model.IsCheck;
        //return DB.Context.Update(model);
    }
    #endregion
}