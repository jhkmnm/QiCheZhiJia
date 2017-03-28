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

        string QC_Price_JobName = "汽车之家报价";
        string YC_Price_JobName = "易车网报价";

        //打码登录
        const string key = "234c5af7071248958ea8ffc54bc0318c";
        const string username = "122552967";
        const string password = "18923463698";
        static int AutomaticCount = 20;

        /*
         * 登录后,判断用户类型,如果是试用,抢单判断试用时间,新闻、报价判断已经报价过的次数
         * 如果是付费，判断付费模式(抢单、新闻、报价),在有效期内，购买的服务都可以使用
         */

        public FormMain(QiCheZhiJia qiche, YiChe yiche)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.qiche = qiche;
            this.yiche = yiche;
            th = new Thread(Tool.aideTimer.Run);
            th.Start();
            if(Tool.site == Aide.Site.Qiche)
            {
                tabControl1.SelectedTab = tabPage1;                
            }
            else
            {
                tabControl1.SelectedTab = tabPage2;
            }
            dgvOrder.AutoGenerateColumns = false;
            rowMergeView1.AutoGenerateColumns = false;
            tabControl2.TabPages.Remove(tabPage8);
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        private void AddLoginJob()
        {
            var job = dal.GetJob("汽车自动登录");
            if(job == null)
            {
                job = new Job { JobType = 2, Time = "00:00:00", StartTime = "00:00:00", EndTime = "00:00:00", Space = 24 * 60 * 60 * 1000, JobName = "汽车自动登录" };
                dal.AddJob(job);
            }
            Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = AutomaticLogin });
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
        private void AutomaticLogin(string jobName)
        {
            AutomaticCount--;
            if (AutomaticCount < 0) return;
            var validateCodeImg = qiche.ValidateCode();
            var validatecode = DaMa(validateCodeImg);
            var result = qiche.Login(Tool.userInfo_qc.UserName, Tool.userInfo_qc.PassWord, validatecode);
            if (!result.Result)
            {
                if (result.Message.Contains("验证码输入有误"))
                {
                    AutomaticLogin(jobName);
                }
            }
            else
            {
                DateTime dtnow = DateTime.Now;
                bool isOver = (dtnow - Tool.userInfo_qc.DueTime.Value).TotalSeconds >= 0;
                AutomaticCount = 20;
                CheckSendOrder(Tool.userInfo_qc, isOver);
                CheckSendPrice(Tool.userInfo_qc, isOver);
                CheckSendNews(Tool.userInfo_qc, isOver);
                if(string.IsNullOrWhiteSpace(lblQD_QC.Text))
                    btnSendOrder_Click(new object(), new EventArgs());
            }
        }

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
                DateTime dtnow = DateTime.Now;
                bool isOver = (dtnow - user.DueTime.Value).TotalSeconds >= 0;
                if(user.SiteName == "汽车之家")
                {
                    lblCode.Text = user.Id.ToString();
                    lblEnd.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
                    lblUserName.Text = user.UserName;
                    lblUserType.Text = user.UserType == 0 ? "试用" : "付费";
                    if(user.UserType == 1)
                        AddLoginJob();
                }
                else if (user.SiteName == "易车网")
                {
                    label21.Text = user.Id.ToString();
                    label18.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
                    label19.Text = user.UserName;
                    label22.Text = user.UserType == 0 ? "试用" : "付费";
                }
                CheckSendOrder(user, isOver);
                CheckSendPrice(user, isOver);
                CheckSendNews(user, isOver);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 抢单判断
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isOver"></param>
        private void CheckSendOrder(Service.User user, bool isOver)
        {
            bool canOrder = true;
            string message = user.DueTime.ToString();
            if (user.UserType == 0 || !user.SendOrder)
            {
                if (isOver)
                {
                    canOrder = false;
                    message = "非常抱歉，今天抢单体验时间已到";
                }
            }
            else if (user.SendOrder && isOver)
            {
                canOrder = false;
                message = "非常抱歉，付费使用时间已到!";
            }
            if (canOrder)
            {
                LoadOrder(user.SiteName);
            }
            else
            {
                if (user.SiteName == "汽车之家")
                {
                    lblQD_QC.Text = message;
                    btnSendOrder.Enabled = btnStop.Enabled = false;
                    if (th_qc != null)
                        th_qc.Abort();
                }
                else
                {
                    lblQD_YC.Text = message;
                    btnStart_YC.Enabled = btnStop_YC.Enabled = btnOrderYC.Enabled = false;
                    if (th_yc != null)
                        th_yc.Abort();
                }
            }
        }

        /// <summary>
        /// 报价判断
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isOver"></param>
        private void CheckSendPrice(Service.User user, bool isOver)
        {
            bool canQuery = true;
            string message = user.QueryNum.ToString();
            if (user.UserType == 0 || !user.Query)
            {
                if (user.QueryNum <= 0)
                {
                    canQuery = false;
                    message = "非常抱歉，今天报价次数已使用完";
                }
            }
            else if (user.Query && isOver)
            {
                canQuery = false;
                message = "非常抱歉，付费使用时间已到!";
            }
            if (canQuery)
            {
                if (Tool.site == Aide.Site.Qiche && user.SiteName == "汽车之家")
                    lbl_QC_QueryNum.Text = user.Query ? "按到期时间计算" : user.QueryNum.ToString();
                else if (Tool.site == Aide.Site.Yiche && user.SiteName == "易车网")
                    label16.Text = user.Query ? "按到期时间计算" : user.QueryNum.ToString();
                LoadJob_Query();
            }
            else
            {
                if (Tool.site == Aide.Site.Qiche && user.SiteName == "汽车之家")
                {
                    lbl_QC_QueryNum.Text = message;
                    jct_QC_Query.Enabled = false;
                    Tool.aideTimer.Dequeue(QC_Price_JobName);
                }
                else if (Tool.site == Aide.Site.Yiche && user.SiteName == "易车网")
                {
                    label16.Text = message;
                    jct_YC_Query.Enabled = false;
                    Tool.aideTimer.Dequeue(YC_Price_JobName);
                }
            }
        }

        /// <summary>
        /// 资讯判断
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isOver"></param>
        private void CheckSendNews(Service.User user, bool isOver)
        {
            bool canNews = true;
            string message = user.NewsNum.ToString();
            if (user.UserType == 0 || !user.News)
            {
                if (user.NewsNum <= 0)
                {
                    canNews = false;
                    message = "非常抱歉，今天发布资讯次数已使用完";
                }
            }
            else if (user.News && isOver)
            {
                canNews = false;
                message = "非常抱歉，付费使用时间已到!";
            }
            if (canNews)
            {
                if (user.SiteName == "汽车之家")
                    lbl_QC_NewsNum.Text = user.News ? "按到期时间计算" : user.NewsNum.ToString();
                else
                    label14.Text = user.Query ? "按到期时间计算" : user.NewsNum.ToString();
                LoadJob_News();
            }
            else
            {
                if (Tool.site == Aide.Site.Qiche && user.SiteName == "汽车之家")
                {
                    lbl_QC_NewsNum.Text = message;
                    colSitting.ReadOnly = true;
                    if(newListDTPBindingSource.DataSource != null)
                    {
                        var data = (List<NewListDTP>)newListDTPBindingSource.DataSource;
                        data.ForEach(item => {
                            item.Message = "付费时间已到";
                            item.Del = "";
                            Tool.aideTimer.Dequeue(item.NewsId);
                        });
                        rowMergeView1.Refresh();
                    }
                }
                else if (Tool.site == Aide.Site.Yiche && user.SiteName == "易车网")
                {
                    label14.Text = message;
                    colYC_Sitting.ReadOnly = true;
                    if (newListDTPBindingSource1.DataSource != null)
                    {
                        var data = (List<NewListDTP>)newListDTPBindingSource1.DataSource;
                        data.ForEach(item =>
                        {
                            item.Message = "付费时间已到";
                            item.Del = "";
                            Tool.aideTimer.Dequeue(item.NewsId);
                        });
                        rowMergeView2.Refresh();
                    }
                }
            }
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
        private void LoadOrder(string siteName)
        {
            if (siteName == "汽车之家")
            {
                if (th_qc == null)
                    LoadOrder_QC();
            }
            else
            {
                if(th_yc == null)
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
                    lbxSendOrder.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + vr.Message);
                    lbxSendOrder.TopIndex = lbxSendOrder.Items.Count - 1;
                }
                if(vr.Ref)
                {
                    bool isOver = (DateTime.Now - Tool.userInfo_qc.DueTime.Value).TotalSeconds >= 0;
                    CheckSendOrder(Tool.userInfo_qc, isOver);
                }
            }));
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            btnSendOrder.Enabled = false;
            btnCondition.Enabled = false;
            btnRef.Enabled = false;
            SendOrder_QC();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool check = chkAll.Checked;
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                row.Cells[colSelected.Name].Value = chkAll.Checked;
            }
            dal.UpdateNickChecked(check);
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
                btnCondition.Enabled = true;
                btnRef.Enabled = true;
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
            var htmlDoc = yiche.GoToOrder();
            if(ordertype.Count == 0)
            {
                List<TextValue> type = new List<TextValue>();
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
            btnOrderYC.Enabled = false;
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
                    lbxSendOrder_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + vr.Message);
                lbxSendOrder_YC.TopIndex = lbxSendOrder_YC.Items.Count - 1;
                if (vr.Ref)
                {
                    bool isOver = (DateTime.Now - Tool.userInfo_yc.DueTime.Value).TotalSeconds >= 0;
                    CheckSendOrder(Tool.userInfo_yc, isOver);
                }
            }));
        }

        private void btnStop_YC_Click(object sender, EventArgs e)
        {
            if (th_yc != null)
            {
                th_yc.Abort();
                btnStart_YC.Enabled = true;
                btnOrderYC.Enabled = true;
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
                jobname = QC_Price_JobName;
                action = SavePrice_QC;
                jc = jct_QC_Query;
                jc.SetJobEvent -= jct_QC_Query_SetJobEvent;
                jc.SetJobEvent += jct_QC_Query_SetJobEvent;
            }
            else
            {
                jobname = YC_Price_JobName;
                action = SavePrice_YC;
                jc = jct_YC_Query;
                jc.SetJobEvent -= jct_YC_Query_SetJobEvent;
                jc.SetJobEvent += jct_YC_Query_SetJobEvent;
            }
            var job = dal.GetJob(jobname);
            if (job != null)
            {
                Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = action });
                jc.SetJob(job);
            }
        }

        #region 汽车之家
        private void SavePrice_QC(string jobName)
        {
            try
            {
                var result = qiche.SavePrice();
                Invoke(new Action(() =>
                {
                    lbxQuer.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + result.Message);
                    lbxQuer.TopIndex = lbxQuer.Items.Count - 1;
                }));
                Job job = dal.GetJob(jobName);
                if (job.JobType == 1 || !string.IsNullOrWhiteSpace(job.Time))
                {
                    Invoke(new Action(() => jct_QC_Query.lblState.Text = job.Message()));
                }
                dal.AddJobLog(new JobLog { JobName = jobName, Time = DateTime.Now.ToString("yyyy-MM-dd") });
                if (result.Result)
                {
                    Tool.service.UpdateLastQuoteTime(Tool.userInfo_qc.Id);
                    Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "报价", JobTime = DateTime.Now });
                    Tool.userInfo_qc.QueryNum--;
                    bool isOver = (DateTime.Now - Tool.userInfo_qc.DueTime.Value).TotalSeconds >= 0;
                    Invoke(new Action(() => CheckSendPrice(Tool.userInfo_qc, isOver)));
                }
            }
            catch(Exception ex)
            {
                Invoke(new Action(() =>
                {
                    lbxQuer.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":自动登录失败，请重新登录");
                    lbxQuer.TopIndex = lbxQuer.Items.Count - 1;
                }));
                Tool.aideTimer.Dequeue(jobName);
            }
        }

        void jct_QC_Query_SetJobEvent(Job job, bool add)
        {
            if(add)
            {
                job.JobName = QC_Price_JobName;
                dal.AddJob(job);
            }
            Tool.aideTimer.Enqueue(new AideJobs { Job = job, JobAction = SavePrice_QC });
        }
        #endregion

        #region 易车网
        private void SavePrice_YC(string jobName)
        {
            var result = yiche.SavePrice();
            Invoke(new Action(() =>
                {
                    lbxQuer_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + result.Message);
                    lbxQuer_YC.TopIndex = lbxQuer_YC.Items.Count - 1;
                }));
            dal.AddJobLog(new JobLog { JobName = jobName, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            Job job = dal.GetJob(jobName);
            if (job.JobType == 1 || !string.IsNullOrWhiteSpace(job.Time))
            {
                Invoke(new Action(() => jct_YC_Query.lblState.Text = job.Message()));
            }
            if (result.Result)
            {
                Tool.service.UpdateLastQuoteTime(Tool.userInfo_yc.Id);
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "报价", JobTime = DateTime.Now });
                Tool.userInfo_yc.QueryNum--;
                bool isOver = (DateTime.Now - Tool.userInfo_yc.DueTime.Value).TotalSeconds >= 0;
                Invoke(new Action(() => CheckSendPrice(Tool.userInfo_yc, isOver)));
            }
        }

        private void jct_YC_Query_SetJobEvent(Job job, bool add)
        {
            if (add)
            {
                job.JobName = YC_Price_JobName;
                dal.AddJob(job);
            }            
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
                LoadNews_YC();
            }
        }

        #region 汽车之家
        private void SaveNews_QC(string newsID)
        {
            var selected = ((List<NewListDTP>)newListDTPBindingSource.DataSource).Where(w => w.NewsId == newsID).FirstOrDefault();
            if (selected != null)
            {
                try
                {
                    var result = qiche.PostNews(selected);
                    Invoke(new Action(() =>
                    {
                        lblNews.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + result);
                        lblNews.TopIndex = lblNews.Items.Count - 1;
                    }));                    
                    dal.AddJobLog(new JobLog { JobName = newsID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
                    selected.Message = "已执行";
                    selected.Del = "";
                    Invoke(new Action(() => rowMergeView1.Refresh()));
                    if (result.Contains("发布成功"))
                    {
                        Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "资讯", JobTime = DateTime.Now });
                        Tool.service.UpdateLastNewsTime(Tool.userInfo_qc.Id);
                        Tool.userInfo_qc.NewsNum--;
                        bool isOver = (DateTime.Now - Tool.userInfo_qc.DueTime.Value).TotalSeconds >= 0;
                        Invoke(new Action(() => CheckSendNews(Tool.userInfo_qc, isOver)));
                    }
                }
                catch(Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        lblNews.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":自动登录失败，请重新登录");
                        lblNews.TopIndex = lblNews.Items.Count - 1;
                    }));
                    Tool.aideTimer.Dequeue(newsID);
                }
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
                    i.Message = job.Message();
                    i.Del = i.Message.Contains("已") ? "" : "删除计划";
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
                    ((NewListDTP)newListDTPBindingSource.Current).Del = "删除计划";
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
        private void LoadNews_YC()
        {
            var list = dal.GetNewsList();
            List<NewListDTP> newdtp = new List<NewListDTP>();
            foreach (var i in list)
            {
                var dtp = new NewListDTP { NewsId = i.ID.ToString(), Title = i.Title, DelNews = i.Content };
                var job = dal.GetJob(dtp.NewsId);
                if (job != null)
                {
                    dtp.Message = job.Message();
                    dtp.Del = dtp.Message.Contains("已") ? "" : "删除计划";
                }
                newdtp.Add(dtp);
            }
            newListDTPBindingSource1.DataSource = newdtp;
            rowMergeView2.Refresh();
        }

        private void SaveNews_YC(string newsID)
        {
            var selected = ((List<NewListDTP>)newListDTPBindingSource1.DataSource).Where(w => w.NewsId == newsID).FirstOrDefault();
            if (selected != null)
            {
                var result = yiche.PostNews(selected);
                Invoke(new Action(() =>
                {
                    listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":" + result);
                    listBox3.TopIndex = lblNews.Items.Count - 1;
                }));
                dal.AddJobLog(new JobLog { JobName = newsID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
                selected.Message = "已执行";
                selected.Del = "";
                Invoke(new Action(() => rowMergeView2.Refresh()));
                if (result.Contains("发布成功"))
                {
                    Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "资讯", JobTime = DateTime.Now });
                    Tool.service.UpdateLastNewsTime(Tool.userInfo_yc.Id);
                    Tool.userInfo_yc.NewsNum--;
                    bool isOver = (DateTime.Now - Tool.userInfo_yc.DueTime.Value).TotalSeconds >= 0;
                    Invoke(new Action(() => CheckSendNews(Tool.userInfo_yc, isOver)));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form_YC(yiche);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadNews_YC();
            }
        }

        private void btn_YC_Del_Click(object sender, EventArgs e)
        {
            dal.DelJob(YC_Price_JobName);
            Tool.aideTimer.Dequeue(YC_Price_JobName);
            jct_YC_Query.InitControl();
        }

        private void btn_QC_Del_Click(object sender, EventArgs e)
        {
            dal.DelJob(QC_Price_JobName);
            Tool.aideTimer.Dequeue(QC_Price_JobName);
            jct_QC_Query.InitControl();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var str = qiche.GotoLoginPage();
            if (string.IsNullOrWhiteSpace(str))
            {
                var validateCodeImg = qiche.ValidateCode();
                var validatecode = DaMa(validateCodeImg);
                var result = qiche.Login(Tool.userInfo_qc.UserName, Tool.userInfo_qc.PassWord, validatecode);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    List<TextValue> pro = new List<TextValue>();
        //    pro.AddRange(new TextValue[] {
        //        new TextValue { Text="安徽", Value="1" },
        //        new TextValue { Text="北京", Value="2" },
        //        new TextValue {Text="福建", Value="3"},
        //        new TextValue {Text ="甘肃", Value="4" },
        //        new TextValue {Text="广东", Value="5"},
        //        new TextValue {Text="广西", Value="6"},
        //        new TextValue {Text="贵州", Value="7"},
        //        new TextValue {Text="海南", Value="8"},
        //    new TextValue {Text="河北", Value="9"},
        //    new TextValue {Text="河南", Value="10"},
        //    new TextValue {Text="黑龙江", Value="11"},
        //    new TextValue {Text="湖北", Value="12"},
        //    new TextValue {Text="湖南", Value="13"},
        //    new TextValue {Text="吉林", Value="14"},
        //    new TextValue {Text="江苏", Value="15"},
        //    new TextValue {Text="江西", Value="16"},
        //    new TextValue {Text="辽宁", Value="17"},
        //    new TextValue {Text="内蒙古", Value="18"},
        //    new TextValue {Text="宁夏", Value="19"},
        //    new TextValue {Text="青海", Value="20"},
        //    new TextValue {Text="山东", Value="21"},
        //    new TextValue {Text="山西", Value="22"},
        //    new TextValue {Text="陕西", Value="23"},
        //    new TextValue {Text="上海", Value="24"},
        //    new TextValue {Text="四川", Value="25"},
        //    new TextValue {Text="天津", Value="26"},
        //    new TextValue {Text="西藏", Value="27"},
        //    new TextValue {Text="新疆", Value="28"},
        //    new TextValue {Text="云南", Value="29"},
        //    new TextValue {Text="浙江", Value="30"},
        //    new TextValue {Text="重庆", Value="31"},
        //    new TextValue {Text="日喀则", Value="2703" } });

        //    //pro.ForEach(i => dal.AddPro(i.Text, i.Value));

        //    comboBox1.DataSource = pro;
        //    comboBox1.DisplayMember = "Text";
        //    comboBox1.ValueMember = "Value";
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    var v = yiche.LoadCityByPro("100005907", comboBox1.SelectedValue.ToString());
        //    var city = JsonConvert.DeserializeObject<List<TextValue>>(v.DocumentNode.OuterHtml);
        //    city.ForEach(i => dal.AddCity(comboBox1.Text, comboBox1.SelectedValue.ToString(), i.Text, i.Value));
        //}

        private void btnLoadNews_Click(object sender, EventArgs e)
        {
            LoadNews_YC();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            var current = (NewListDTP)newListDTPBindingSource1.Current;
            var newsid = current.NewsId;
            if (e.ColumnIndex == colYC_Sitting.Index)
            {
                var form = new FormNewsJob(newsid);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    current.Message = form.Job.Message();
                    current.Del = "删除计划";
                    Tool.aideTimer.Enqueue(new AideJobs { Job = form.Job, JobAction = SaveNews_YC });
                }
            }
            else if (e.ColumnIndex == colYC_Del.Index)
            {
                if (MessageBox.Show("确定删除发布计划?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    dal.DelJob(newsid);
                    Tool.aideTimer.Dequeue(newsid);
                    current.Message = "";
                    current.Del = "";
                }
            }
            else if(e.ColumnIndex == colYC_Title.Index)
            {
                var form = new Form_YC(yiche, Convert.ToInt32(current.NewsId));
                if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadNews_YC();
                }
            }
            else if(e.ColumnIndex == colDelNews.Index)
            {
                if (MessageBox.Show("确定删除草稿?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    dal.DelJob(newsid);
                    Tool.aideTimer.Dequeue(newsid);
                    dal.DelNews(Convert.ToInt32(newsid));
                    OperateIniFile.DelIniFile(newsid);
                    //((List<NewListDTP>)newListDTPBindingSource1.DataSource).Remove(current);
                    rowMergeView2.Rows.RemoveAt(e.RowIndex);
                }
            }
            rowMergeView2.Refresh();
        }

        private void rowMergeView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头

            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex == -1 && (e.ColumnIndex == colYC_Sitting.Index || e.ColumnIndex == colYC_Del.Index))
            {
                if (e.ColumnIndex == colYC_Sitting.Index)
                {
                    top = e.CellBounds.Top;
                    left = e.CellBounds.Left;
                    height = e.CellBounds.Height;
                    width1 = e.CellBounds.Width;
                }

                int width2 = colYC_Del.Width;

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
        DAL dal = new DAL();
        bool isbusy = false;

        public void Enqueue(AideJobs aj)
        {
            isbusy = true;
            var item = jobList.Find(w => w.Job.JobName == aj.Job.JobName);
            if (item != null)
                Dequeue(aj.Job.JobName);
            jobList.Add(aj);
            isbusy = false;
        }

        public void Dequeue(string jobname)
        {
            isbusy = true;
            var item = jobList.Find(w => w.Job.JobName == jobname);
            if(item != null)
                jobList.Remove(item);
            isbusy = false;
        }

        public void Run()
        {
            while (true)
            {
                if (!isbusy)
                {
                    if (jobList.Count > 0)
                    {
                        for (int i = 0; i < jobList.Count; i++)
                        {
                            try
                            {
                                if (jobList[i].JobAction != null)
                                {
                                    if (ExecJob(jobList[i].Job, jobList[i].JobAction) && jobList[i].Job.JobType == 1)
                                    {
                                        jobList.Remove(jobList[i]);
                                        i--;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                Thread.Sleep(1000 * 5);
            }
        }

        private bool CheckJob(Job job)
        {
            DateTime dtnow = DateTime.Now;
            if (job.JobType == 1)
            {
                DateTime dt = Convert.ToDateTime(job.JobDate + " " + job.Time);
                if ((dtnow - dt).TotalSeconds >= 0 && (dtnow - dt).TotalSeconds <= 5 && string.IsNullOrWhiteSpace(job.ExecTime))
                {
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(job.Time))
                {
                    DateTime dt = Convert.ToDateTime(job.Time);
                    if ((dtnow - dt).TotalSeconds >= 0 && (dtnow - dt).TotalSeconds <= 5 && (string.IsNullOrWhiteSpace(job.ExecTime) || Convert.ToDateTime(job.ExecTime).Date != dtnow.Date))
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
        private bool ExecJob(Job job, Action<string> action)
        {
            if(CheckJob(job))
            {
                job.ExecTime = DateTime.Now.ToString();
                dal.UpJobExecTime(job.JobName);
                action(job.JobName);
                return true;
            }
            return false;
        }
    }

    public class AideJobs
    {
        public Job Job { get; set; }
        public Action<string> JobAction { get; set; }        
    }
}