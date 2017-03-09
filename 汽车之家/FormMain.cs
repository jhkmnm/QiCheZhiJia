using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Model;
using System.Collections.Generic;
using System.Threading;
using Dos.Common;
using System.Linq;
using System.Diagnostics;
using System.Drawing;

namespace Aide
{
    public partial class FormMain : Form
    {
        QiCheZhiJia qiche;
        YiChe yiche;
        DAL dal = new DAL();
        Thread th_qc;
        Thread th_yc;
        Thread th;
        int top, left, height, width1;

        Job job_qc_quote;
        Job job_qc_news;
        Job job_yc_quote;
        Job job_yc_news;

        const string key = "087a9b3cdec0c2597df237916ebfff9a";
        const string username = "";
        const string password = "";

        /*
         * 登录后,判断用户类型,如果是试用,抢单判断试用时间,新闻、报价判断已经报价过的次数
         * 如果是付费，判断付费模式(抢单、新闻、报价),在有效期内，购买的服务都可以使用
         */

        public FormMain(QiCheZhiJia qiche, YiChe yiche)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.qiche = qiche;
            this.yiche = yiche;
            if(Tool.site == Aide.Site.Qiche)
            {
                tabControl1.SelectedTab = tabPage1;
            }
            else
            {
                tabControl1.SelectedTab = tabPage2;
            }

            tabControl2.TabPages.Remove(tabPage8);
            dgvOrder.AutoGenerateColumns = false;
            th = new Thread(Tool.aideTimer.Run);
            th.Start();
        }

        #region 窗体事件
        private void FormLogin_Load(object sender, EventArgs e)
        {
            InitUser();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                Tool.site = Aide.Site.Qiche;
                linkLabel2.Text = "打开汽车之家后台";
            }
            else
            {
                Tool.site = Aide.Site.Yiche;
                linkLabel2.Text = "打开易车网后台";
            }
            InitUser();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Tool.userInfo_qc != null)
            {
                Tool.service.UpdateLoginLogByLogOut(Tool.userInfo_qc.Id);
            }

            if (Tool.userInfo_yc != null)
            {
                Tool.service.UpdateLoginLogByLogOut(Tool.userInfo_yc.Id);
            }

            if (th_qc != null) th_qc.Abort();
            if (th_yc != null) th_yc.Abort();
            if (th != null) th.Abort();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://shop113012593.taobao.com/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "";
            if (Tool.site == Aide.Site.Qiche)
            {
                url = "http://ics.autohome.com.cn/passport/account/login";
            }
            else
            {
                url = "http://dealer.easypass.cn/LoginPage/DefaultLogin.aspx";
            }
            Process.Start(url);
        }
        #endregion

        #region 登录
        private void InitUser()
        {
            if (Tool.site == Aide.Site.Qiche)
            {
                if (Tool.userInfo_qc == null)
                {
                    var form = new FormLogin(Tool.site);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        if (Tool.userInfo_yc != null)
                            tabControl1.SelectedTab = tabPage2;
                        else
                            Close();
                        return;
                    }
                    else
                        qiche = form.qiche;
                }
                LoadUser(Tool.userInfo_qc);
            }
            else
            {
                if (Tool.userInfo_yc == null)
                {
                    var form = new FormLogin(Tool.site);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        if (Tool.userInfo_qc != null)
                            tabControl1.SelectedTab = tabPage1;
                        else
                            Close();
                        return;
                    }
                    else
                        yiche = form.yiche;
                }
                LoadUser(Tool.userInfo_yc);
            }
        }

        private void LoadUser(Service.User user)
        {
            try
            {
                if(user.SiteName == "汽车之家")
                {
                    lblCode.Text = user.Id.ToString();
                    lblEnd.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
                    lblUserName.Text = user.UserName;
                    lblUserType.Text = user.UserType == 0 ? "试用" : "付费";
                }
                else if (user.SiteName == "易车网")
                {
                    label21.Text = user.Id.ToString();
                    label18.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
                    label19.Text = user.UserName;
                    label22.Text = user.UserType == 0 ? "试用" : "付费";
                }

                #region 抢单判断
                bool canOrder = true;
                if (user.UserType == 0 || !user.SendOrder)
                {
                    if (!Tool.service.CheckTasteTime(user.Id))
                    {
                        canOrder = false;
                        if (Tool.site == Aide.Site.Qiche)
                        {
                            lblQD_QC.Text = "非常抱歉，今天抢单体验时间已到";
                            btnSendOrder.Enabled = btnStop.Enabled = false;
                            if (th_qc != null)
                                th_qc.Abort();
                        }
                        else
                        {
                            lblQD_YC.Text = "非常抱歉，今天抢单体验时间已到";
                            btnStart_YC.Enabled = btnStop_YC.Enabled = false;
                            if (th_yc != null)
                                th_yc.Abort();
                        }
                    }
                }
                if(canOrder)                
                {
                    if (Tool.site == Aide.Site.Qiche)
                    {
                        if (th_qc == null)
                            LoadOrder();
                    }
                    else
                    {
                        if (th_yc == null)
                            LoadOrder();
                    }
                }
                #endregion

                #region 报价判断
                bool canQuery = true;
                if (user.UserType == 0 || !user.Query)
                {
                    if (user.QueryNum <= 0)
                    {
                        canQuery = false;
                        if (Tool.site == Aide.Site.Qiche && user.SiteName == "汽车之家")
                        {
                            lbl_QC_QueryNum.Text = "非常抱歉，今天报价次数已使用完";
                            jct_QC_Query.Enabled = false;
                        }
                        else if(Tool.site == Aide.Site.Yiche && user.SiteName == "易车网")
                        {
                            lbl_QC_QueryNum.Text = "非常抱歉，今天报价次数已使用完";
                            jct_YC_Query.Enabled = false;
                        }
                    }
                }
                if (canQuery)
                {
                    if (Tool.site == Aide.Site.Qiche)
                        lbl_QC_QueryNum.Text = user.Query ? "按到期时间计算" : user.QueryNum.ToString();
                    else
                        label16.Text = user.Query ? "按到期时间计算" : user.QueryNum.ToString();
                    LoadJob_Query();
                }
                #endregion

                #region 资讯判断
                bool canNews = true;
                if (user.UserType == 0 || !user.News)
                {
                    if (user.NewsNum <= 0)
                    {
                        canNews = false;
                        if (Tool.site == Aide.Site.Qiche && user.SiteName == "汽车之家")
                        {
                            lbl_QC_NewsNum.Text = "非常抱歉，今天发布资讯次数已使用完";
                        }
                        else if (Tool.site == Aide.Site.Yiche && user.SiteName == "易车网")
                        {
                            lbl_QC_NewsNum.Text = "非常抱歉，今天发布资讯次数已使用完";
                            jct_YC_News.Enabled = false;
                        }
                    }                    
                }
                if (canNews)
                {
                    if (Tool.site == Aide.Site.Qiche)
                        lbl_QC_NewsNum.Text = user.News ? "按到期时间计算" : user.NewsNum.ToString();
                    else
                        label14.Text = user.Query ? "按到期时间计算" : user.NewsNum.ToString();
                    LoadJob_News();
                }
                #endregion
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message + ex.StackTrace);
            }
        }

        private void CheckSendOrder()
        {

        }

        private string DaMa(byte[] image)
        {
            StringBuilder VCodeText = new StringBuilder(100);
            int ret = Dama2.D2Buf(
                key, //softawre key (software id)
                username,    //user name
                password,     //password
                image,         //图片数据，图片数据不可大于4M
                (uint)image.Length, //图片数据长度
                60,         //超时时间，单位为秒，更换为实际需要的超时时间
                101,        //验证码类型ID，参见 http://wiki.dama2.com/index.php?n=ApiDoc.GetSoftIDandKEY
                VCodeText); //成功时返回验证码文本（答案）
            return ret > 0 ? VCodeText.ToString() : "";
        }
        #endregion

        #region 抢单
        private void LoadOrder()
        {
            if (Tool.site == Aide.Site.Qiche)
            {
                LoadOrder_QC();
            }
            else
            {
                LoadOrder_YC();
            }
        }

        #region 汽车之家
        private void btnCondition_Click(object sender, EventArgs e)
        {
            var form = new FormOrderCondition(Tool.site);
            form.ShowDialog();
        }

        private void LoadOrder_QC()
        {
            List<OrderType> ordertypeList = dal.GetOrderTypes(Tool.site.ToString());
            List<Spec> specList = new List<Spec>();

            if (ordertypeList.Count == 0)
            {
                var doc = qiche.LoadOrder();
                var series = doc.DocumentNode.SelectNodes("//*[@id=\"sel_series\"]");
                var ordertype = doc.DocumentNode.SelectNodes("//*[@id=\"sel_orderType\"]");

                foreach (HtmlNode node in series[0].ChildNodes)
                {
                    if (node.Name == "option" && !node.NextSibling.OuterHtml.Contains("全部"))
                    {
                        specList.Add(new Spec
                        {
                            SPecName = node.NextSibling.OuterHtml.Replace("&nbsp;", ""),
                            ID = Convert.ToInt32(node.GetAttributeValue("value", "0")),
                            IsCheck = true
                        });
                    }
                }
                dal.AddSpecs(specList);

                foreach (HtmlNode node in ordertype[0].ChildNodes)
                {
                    if (node.Name == "option" && !node.NextSibling.OuterHtml.Contains("全部"))
                    {
                        ordertypeList.Add(new OrderType
                        {
                            Site = Tool.site.ToString(),
                            TypeName = node.NextSibling.OuterHtml,
                            ID = Convert.ToInt32(node.GetAttributeValue("value", "0")),
                            IsCheck = true
                        });
                    }
                }
                dal.AddOrderTypes(ordertypeList);
            }

            var nicks = dal.GetNicks();
            if(nicks.Count == 0)
            {
                nicks = qiche.GetNicks();
                dal.AddNicks(nicks);
            }
                
            dgvOrder.DataSource = nicks;
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            var nicks = qiche.GetNicks();
            dal.AddNicks(nicks);
            dgvOrder.DataSource = nicks;
        }

        private void SendOrder_QC()
        {
            qiche.SendOrderEvent -= qiche_SendOrderEvent;
            qiche.SendOrderEvent += qiche_SendOrderEvent;
            th_qc = new Thread(qiche.SendOrder);
            th_qc.Start();
        }

        void qiche_SendOrderEvent(ViewResult vr)
        {
            this.Invoke(new Action(() =>
            {
                if (vr.Result)
                {
                    lbxSendOrder.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + vr.Message);
                    lbxSendOrder.TopIndex = lbxSendOrder.Items.Count - 1;
                }

                LoadUser(Tool.userInfo_qc);
            }));
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            btnSendOrder.Enabled = false;
            SendOrder_QC();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                row.Cells[colSelected.Name].Value = chkAll.Checked;
                dal.UpdateNickChecked(row.Cells[colSaleID.Name].Value.ToString());
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if(e.ColumnIndex == colSelected.Index)
            {
                dal.UpdateNickChecked(dgvOrder.Rows[e.RowIndex].Cells[colSaleID.Name].Value.ToString());
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(th_qc != null)
            {
                th_qc.Abort();
                btnSendOrder.Enabled = true;
            }            
        }
        #endregion

        #region 易车网
        private void btnOrderYC_Click(object sender, EventArgs e)
        {
            var form = new FormOrderCondition(Tool.site);
            form.ShowDialog();
        }

        private void LoadOrder_YC()
        {
            var ordertype = dal.GetOrderTypes(Tool.site.ToString());
            if(ordertype.Count == 0)
            {
                List<TextValue> type = new List<TextValue>();

                var htmlDoc = yiche.GoToOrder();
                var script = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"Head1\"]/script[7]/text()").InnerText.Replace(" ", "").Replace("\r", "").Split('\n');
                foreach (string str in script)
                {
                    if (str.StartsWith("SetOrderType:"))
                    {
                        type = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetOrderType:", ""));
                    }
                }

                type.ForEach(f => {
                    if (!f.Text.Contains("全部"))
                        ordertype.Add(new OrderType { Site = Tool.site.ToString(), TypeName = f.Text, ID = Convert.ToInt32(f.Value), IsCheck = true });  
                });
                dal.AddOrderTypes(ordertype);
            }
        }

        private void btnStart_YC_Click(object sender, EventArgs e)
        {
            btnStart_YC.Enabled = false;
            SendOrder_YC();
        }

        private void SendOrder_YC()
        {
            yiche.SendOrderEvent += Yiche_SendOrderEvent;
            th_yc = new Thread(yiche.SendOrder);
            th_yc.Start();
        }

        private void Yiche_SendOrderEvent(ViewResult vr)
        {
            Invoke(new Action(() =>
            {
                if (vr.Result)
                    lbxSendOrder_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + vr.Message);
                lbxSendOrder_YC.TopIndex = lbxSendOrder_YC.Items.Count - 1;

                LoadUser(Tool.userInfo_yc);
            }));
        }

        private void btnStop_YC_Click(object sender, EventArgs e)
        {
            if (th_yc != null)
            {
                th_yc.Abort();
                btnStart_YC.Enabled = true;
            }
        }
        #endregion

        #endregion

        #region 报价

        private void LoadJob_Query()
        {
            string jobname = "";
            Action<string> action = null;
            JobControl jc = null;
            if (Tool.site == Aide.Site.Qiche)
            {
                jobname = "汽车之家报价";
                action = SavePrice_QC;
                jc = jct_QC_Query;
                jc.SetJobEvent += jct_QC_Query_SetJobEvent;
            }
            else
            {
                jobname = "易车网报价";
                action = SavePrice_YC;
                jc = jct_YC_Query;
                jc.SetJobEvent += jct_YC_Query_SetJobEvent;
            }
            var job = dal.GetJob(jobname);
            if (job != null)
            {
                Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = action });
                jc.SetJob(job);
            }
        }

        private void ExecJob(Job job, Action action)
        {
            /*
             * 计划类型为1，在指定时间执行一次
             * 计划类型为2，
             * 如果有指定执行时间，设置定时器间隔时间为30秒，到指定时间执行，并将间隔时间设置为24小时
             * 如果没有指定执行时间，设置定时器间隔时间，在指定范围内执行
             */

            DateTime dtnow = DateTime.Now;
            if (job.JobType == 1)
            {
                DateTime dt = Convert.ToDateTime(job.JobDate + " " + job.Time);
                if ((dtnow - dt).Minutes >= 0 && (dtnow - dt).Minutes <= 1)
                {
                    action();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(job.Time))
                {
                    DateTime dt = Convert.ToDateTime(job.Time);
                    if ((dtnow - dt).Minutes >= 0 && (dtnow - dt).Minutes <= 1)
                    {
                        action();
                    }
                }
                else
                {
                    if ((dtnow - Convert.ToDateTime(job.StartTime)).Minutes >= 0 && (dtnow - Convert.ToDateTime(job.EndTime)).Minutes <= 0)
                    {
                        action();
                    }
                }
            }
        }

        #region 汽车之家
        private void SavePrice_QC(string jobName)
        {
            var result = qiche.SavePrice();
            Invoke(new Action(() =>
            {
                lbxQuer.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);
                lbxQuer.TopIndex = lbxQuer.Items.Count - 1;
            }));
            Job job = dal.GetJob(jobName);
            if(job.JobType == 1 || !string.IsNullOrWhiteSpace(job.Time))
            {
                Invoke(new Action(() => jct_QC_Query.lblState.Text = ""));
            }
            dal.AddJobLog(new JobLog { JobName = jobName, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            if (result.Result)
            {
                Tool.service.UpdateLastQuoteTime(Tool.userInfo_qc.Id);
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "报价", JobTime = DateTime.Now });
                Tool.userInfo_qc.QueryNum--;
                if(Tool.userInfo_qc.QueryNum <= 0)
                {
                    Tool.aideTimer.Dequeue("汽车之家报价");
                    Invoke(new Action(() => lbl_QC_QueryNum.Text = "非常抱歉，今天的报价次数已用完"));
                }
                else
                {
                    Invoke(new Action(() => lbl_QC_QueryNum.Text = Tool.userInfo_qc.QueryNum.ToString()));
                }
            }
        }

        void jct_QC_Query_SetJobEvent(Job job)
        {
            job.JobName = "汽车之家报价";
            dal.AddJob(job);
            Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = SavePrice_QC });
        }
        #endregion

        #region 易车网
        private void SavePrice_YC(string jobName)
        {
            var result = yiche.SavePrice();
            Invoke(new Action(() =>
                {
                    lbxQuer_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);
                    lbxQuer_YC.TopIndex = lbxQuer_YC.Items.Count - 1;
                }));
            dal.AddJobLog(new JobLog { JobName = jobName, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            Job job = dal.GetJob(jobName);
            if (job.JobType == 1 || !string.IsNullOrWhiteSpace(job.Time))
            {
                Invoke(new Action(() => jct_YC_Query.lblState.Text = ""));
            }
            if (result.Result)
            {
                Tool.service.UpdateLastQuoteTime(Tool.userInfo_yc.Id);
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "报价", JobTime = DateTime.Now });
                Tool.userInfo_yc.QueryNum--;
                if (Tool.userInfo_yc.QueryNum <= 0)
                {
                    Tool.aideTimer.Dequeue("易车网报价");
                    Invoke(new Action(() => label16.Text = "非常抱歉，今天的报价次数已用完"));
                }
                else
                {
                    Invoke(new Action(() => label16.Text = Tool.userInfo_yc.QueryNum.ToString()));
                }
            }
        }

        private void jct_YC_Query_SetJobEvent(Job job)
        {
            job.JobName = "易车网报价";
            dal.AddJob(job);
            Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = SavePrice_YC });
        }
        #endregion

        #endregion

        #region 资讯
        private void LoadJob_News()
        {
            if (Tool.site == Aide.Site.Qiche)
            {
                LoadNews();
            }
            else
            {
                job_yc_news = dal.GetJob("易车网新闻");
                jct_YC_News.SetJobEvent += jct_YC_News_SetJobEvent;
                jct_YC_News.SetJob(job_yc_news);
            }
        }

        #region 汽车之家
        private void SaveNews_QC(string newsID)
        {
            var selected = ((List<NewListDTP>)newListDTPBindingSource.DataSource).Where(w => w.NewsId == newsID).FirstOrDefault();
            if (selected != null)
            {
                var result = qiche.PostNews(selected);
                Invoke(new Action(() => {
                    lblNews.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result);
                    lblNews.TopIndex = lblNews.Items.Count - 1;
                }));
                var job = dal.GetJob(newsID);
                job.ExecTime = DateTime.Now.ToString();
                dal.AddJob(job);
                dal.AddJobLog(new JobLog { JobName = newsID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
                if (result.Contains("发布成功"))
                {
                    Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "资讯", JobTime = DateTime.Now });
                    Tool.userInfo_qc.NewsNum--;
                    if(Tool.userInfo_qc.NewsNum <= 0)
                    {
                        Invoke(new Action(() => lbl_QC_NewsNum.Text = "非常抱歉，今天发布资讯次数已使用完"));
                    }
                    else
                    {
                        Invoke(new Action(() => lbl_QC_NewsNum.Text = Tool.userInfo_qc.NewsNum.ToString()));
                    }
                }
                selected.Message = "已执行";
                selected.Del = "";
            }
        }

        private void btnQC_LoadNews_Click(object sender, EventArgs e)
        {
            LoadNews();
        }

        private void LoadNews()
        {
            var list = qiche.GetNewsDraft();
            foreach(var i in list)
            {
                var job = dal.GetJob(i.NewsId);
                if (job != null)
                {
                    i.Message = "将于：" + job.JobDate + " " + job.Time + "执行";
                    i.Del = "删除";
                }
            }
            newListDTPBindingSource.DataSource = list;
            rowMergeView1.Refresh();
        }

        private void rowMergeView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头
            
            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex == -1 && (e.ColumnIndex == colSitting.Index || e.ColumnIndex == colDel.Index))
            {
                if (e.ColumnIndex == colSitting.Index)
                {
                    top = e.CellBounds.Top;
                    left = e.CellBounds.Left;
                    height = e.CellBounds.Height;
                    width1 = e.CellBounds.Width;
                }

                int width2 = colDel.Width;

                Rectangle rect = new Rectangle(left, top, width1 + width2, e.CellBounds.Height);
                using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    //抹去原来的cell背景
                    e.Graphics.FillRectangle(backColorBrush, rect);
                }
                using (Pen pen = new Pen(Color.White))
                {
                    e.Graphics.DrawLine(pen, left + 1, top + 1, left + width1 + width2 - 1, top + 1);
                }
                using (Pen gridLinePen = new Pen(dgv.GridColor))
                {
                    e.Graphics.DrawLine(gridLinePen, left, top, left + width1 + width2, top);
                    e.Graphics.DrawLine(gridLinePen, left, top + height - 1, left + width1 + width2, top + height - 1);
                    e.Graphics.DrawLine(gridLinePen, left, top, left, top + height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 - 1, top, left + width1 + width2 - 1, top + height);

                    //计算绘制字符串的位置
                    string columnValue = "操作";
                    SizeF sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    float lstr = (width1 + width2 - sf.Width) / 2;
                    float rstr = (height / 2 - sf.Height);
                    //画出文本框
                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + lstr,
                                                     top + rstr + 10,
                                                     StringFormat.GenericDefault);
                    }
                }
                e.Handled = true;
            }
            #endregion
        }

        private void rowMergeView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            var newsid = ((NewListDTP)newListDTPBindingSource.Current).NewsId;
            if(e.ColumnIndex == colSitting.Index)
            {
                var form = new FormNewsJob(newsid);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ((NewListDTP)newListDTPBindingSource.Current).Message = "将于：" + form.Job.JobDate + " " + form.Job.Time +"执行";
                    ((NewListDTP)newListDTPBindingSource.Current).Del = "删除";
                    Tool.aideTimer.Enqueue(new AideJobs { Job = form.Job, JobAction = SaveNews_QC });
                }
            }
            else if(e.ColumnIndex == colDel.Index)
            {
                if(MessageBox.Show("确定删除发布计划?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    dal.DelJob(newsid);
                    Tool.aideTimer.Dequeue(newsid);
                    ((NewListDTP)newListDTPBindingSource.Current).Message = "";
                    ((NewListDTP)newListDTPBindingSource.Current).Del = "";
                }
            }
            rowMergeView1.Refresh();
        }
        #endregion

        #region 易车
        void jct_YC_News_SetJobEvent(Job job)
        {
            job_yc_news = job;
            job_yc_news.JobName = "易车网新闻";
            dal.AddJob(job_yc_news);
            tm_yc_news.Enabled = true;
        }

        private void SaveNews_YC()
        {
            if(dataGridView1.Rows.Count == 0)
            {
                listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":目前没有可以发布的新闻，请添加");
                listBox3.TopIndex = listBox3.Items.Count - 1;
                return;
            }

            foreach(News news in newsBindingSource.DataSource as List<News>)
            {
                var result = yiche.PostNews(news.ID);
                if(result == "发布成功")
                {
                    listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + news.Title + " 发布成功");                    
                    dal.AddJobLog(new JobLog { JobName = job_yc_news.ID.ToString(), Time = DateTime.Now.ToString("yyyy-MM-dd") });
                    Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "资讯", JobTime = DateTime.Now });
                    Tool.userInfo_yc.NewsNum--;
                }
                else
                {
                    listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + news.Title + " 发布失败," + result);
                }
                listBox3.TopIndex = listBox3.Items.Count - 1;
            }
        }

        private void tm_yc_news_Tick(object sender, EventArgs e)
        {
            tm_yc_news.Enabled = false;
            ExecJob(job_yc_news, SaveNews_YC);
            if(job_yc_news.JobType != 1)
                tm_yc_news.Interval = job_yc_news.Space.Value;
            LoadUser(Tool.userInfo_yc);
            tm_yc_news.Enabled = job_yc_news.JobType != 1 && jct_YC_News.Enabled;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form_YC(yiche);
            if (form.ShowDialog() == DialogResult.OK)
            {
                newsBindingSource.DataSource = dal.GetNewsList();
                dataGridView1.Refresh();
            }
        }

        private void btnLoadNews_Click(object sender, EventArgs e)
        {
            newsBindingSource.DataSource = dal.GetNewsList();
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == titleDataGridViewTextBoxColumn.Index)
            {
                //var form = new Form_YC(yiche, ((News)newsBindingSource.Current).ID);
                //if (form.ShowDialog() == DialogResult.OK)
                //{
                //    newsBindingSource.DataSource = dal.GetNewsList();
                //    dataGridView1.Refresh();
                //}
            }
            else
            {
                if(MessageBox.Show("确认删除这条新闻吗?", "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dal.DelNews(((News)newsBindingSource.Current).ID);
                    newsBindingSource.DataSource = dal.GetNewsList();
                    dataGridView1.Refresh();
                }                
            }
        }
        #endregion        

        #endregion        
    }

    public class TextValue
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class AideTimer
    {
        List<AideJobs> jobList = new List<AideJobs>();        

        public void Enqueue(AideJobs aj)
        {
            var item = jobList.Find(w => w.Job.JobName == aj.Job.JobName);
            if (item != null)
            {
                aj.Job.ExecTime = item.Job.ExecTime;
                item = aj;
            }
            else
                jobList.Add(aj);
        }

        public void Dequeue(string jobname)
        {
            var item = jobList.Find(w => w.Job.JobName == jobname);
            if(item != null)
                jobList.Remove(item);
        }

        public void Run()
        {
            while (true)
            {
                if (jobList.Count > 0)
                {
                    for (int i = 0; i < jobList.Count; i++)
                    {
                        if (jobList[i].JobAction != null)
                            ExecJob(jobList[i].Job, jobList[i].JobAction);
                    }
                }
                Thread.Sleep(1000 * 10);
            }
        }

        private bool CheckJob(Job job)
        {
            DateTime dtnow = DateTime.Now;
            if (job.JobType == 1)
            {
                DateTime dt = Convert.ToDateTime(job.JobDate + " " + job.Time);
                if ((dtnow - dt).TotalSeconds >= 0 && (dtnow - dt).TotalSeconds <= 10 && string.IsNullOrWhiteSpace(job.ExecTime))
                {
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(job.Time))
                {
                    DateTime dt = Convert.ToDateTime(job.Time);
                    if ((dtnow - dt).TotalSeconds >= 0 && (dtnow - dt).TotalSeconds <= 10 && string.IsNullOrWhiteSpace(job.ExecTime))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((dtnow - Convert.ToDateTime(job.StartTime)).TotalSeconds >= 0 && (dtnow - Convert.ToDateTime(job.EndTime)).TotalSeconds <= 0)
                    {
                        if(string.IsNullOrWhiteSpace(job.ExecTime) || (dtnow - Convert.ToDateTime(job.ExecTime)).TotalMilliseconds >= job.Space)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断作业并执行方法,方法有一个参数
        /// </summary>
        /// <param name="job"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private void ExecJob(Job job, Action<string> action)
        {
            if(CheckJob(job))
            {
                job.ExecTime = DateTime.Now.ToString();
                action(job.JobName);
            }
        }
    }

    public class AideJobs
    {
        public Job Job { get; set; }
        public Action<string> JobAction { get; set; }        
    }
}