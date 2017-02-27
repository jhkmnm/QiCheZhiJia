using System;
using System.Text;
using System.Windows.Forms;
using CsharpHttpHelper;
using System.IO;
using System.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Model;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using Dos.Common;

namespace Aide
{
    public partial class FormLogin : Form
    {
        QiCheZhiJia qiche;
        YiChe yiche;
        string site;
        DAL dal = new DAL();
        Thread th_qc;
        Thread th_yc;
        string dealerid_yc = "";        

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

        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region 窗体事件
        private void FormLogin_Load(object sender, EventArgs e)
        {
            site = "汽车";

            string path = AppDomain.CurrentDomain.BaseDirectory + "js.lyt";
            if (!File.Exists(path))
            {
                MessageBox.Show("js.lyt文件缺失，建议重新解压软件解决！");
                base.Close();
            }
            qiche = new QiCheZhiJia(File.ReadAllText(path));
            yiche = new YiChe(File.ReadAllText(path));
            InitUser();
#if DEBUG
            if (site == "汽车")
            {
                txtUserName.Text = "晋江嘉华雷克萨斯";
                txtPassword.Text = "qzzs8888.";
            }
            else
            {
                txtUserName.Text = "100005907";
                txtPassword.Text = "a2343567";
            }
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewResult result = new ViewResult();
            if (site == "汽车")
                result = qiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            else
                result = yiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);

            if (!result.Result)
            {
                MessageBox.Show(result.Message);
                if(result.Message.Contains("验证码输入有误"))
                {
                    LoadValidateCode();
                }
            }
            else
            {
                panel1.Visible = false;
                if (site == "汽车")
                {
                    if (chkSavePass.Checked)
                    {
                        qiche.SavePw();
                    }
                    LoadUser(Tool.userInfo_qc);
                }
                else
                {
                    if (chkSavePass.Checked)
                    {
                        yiche.SavePw();
                    }
                    LoadUser(Tool.userInfo_yc);
                }
            }
        }

        private void btnRefImg_Click(object sender, EventArgs e)
        {
            LoadValidateCode();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                site = "汽车";
            }
            else
            {
                site = "易车";
#if DEBUG
                txtUserName.Text = "344801178@qq.com";
#endif
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
        }
        #endregion

        #region 登录
        /// <summary>
        /// 加载验证码
        /// </summary>
        private void LoadValidateCode()
        {
            if (site == "汽车")
            {
                qiche.GotoLoginPage();
                pbCode.Image = qiche.LoadValidateCode();
            }
            else
            {
                yiche.GotoLoginPage();
                pbCode.Image = yiche.LoadValidateCode();
            }
        }

        /// <summary>
        /// 加载密码
        /// </summary>
        private void LoadPw()
        {
            if (site == "汽车")
            {
                var str = qiche.LoadPw();
                if (!string.IsNullOrWhiteSpace(str[0]))
                {
                    chkSavePass.Checked = true;
                    txtUserName.Text = str[0];
                    txtPassword.Text = str[1];
                }
            }
            else
            {
                var str = yiche.LoadPw();
            }
        }

        private void InitUser()
        {
            if (site == "汽车")
            {
                if (Tool.userInfo_qc == null)
                {
                    panel1.Visible = true;
                    panel1.Location = new System.Drawing.Point(4, 25);
                    panel1.Height = this.Height - 25;
                    this.LoadPw();
                    LoadValidateCode();
                }
                else
                {
                    panel1.Visible = false;
                    LoadUser(Tool.userInfo_qc);
                }
            }
            else
            {
                if (Tool.userInfo_yc == null)
                {
                    panel1.Visible = true;
                    panel1.Location = new System.Drawing.Point(4, 25);
                    panel1.Height = this.Height - 25;
                    this.LoadPw();
                    LoadValidateCode();
                }
                else
                {
                    panel1.Visible = false;
                    LoadUser(Tool.userInfo_yc);
                }
            }
        }

        private void LoadUser(Service.User user)
        {
            try
            {
                lblCode.Text = user.Id.ToString();
                lblEnd.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
                lblUserName.Text = user.UserName;
                lblUserType.Text = user.UserType == 0 ? "试用" : "付费";
                lbl_QC_QueryNum.Text = user.Query.Value ? "按到期时间计算" : user.QueryNum.Value.ToString();
                lbl_QC_NewsNum.Text = user.News.Value ? "按到期时间计算" : user.NewsNum.Value.ToString();

                #region 抢单判断
                if (user.UserType == 0 || !user.SendOrder.Value)
                {
                    if (!Tool.service.CheckTasteTime(user.Id))
                    {
                        if (site == "汽车")
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
                    else
                    {
                        if (site == "汽车")
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
                }
                else
                {
                    if (site == "汽车")
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
                if (user.UserType == 0 || !user.Query.Value)
                {
                    if (user.QueryNum == 0)
                    {
                        if (site == "汽车")
                        {
                            lblQC_QC.Text = "非常抱歉，今天报价次数已使用完";
                            jct_QC_Query.Enabled = false;
                        }
                        else
                        {
                            lblYC_YC.Text = "非常抱歉，今天报价次数已使用完";
                            jct_YC_Query.Enabled = false;
                        }
                    }
                    else
                    {
                        LoadJob_Query();
                    }
                }
                else
                {
                    LoadJob_Query();
                }
                #endregion

                #region 资讯判断
                if (user.UserType == 0 || !user.News.Value)
                {
                    if (user.NewsNum == 0)
                    {
                        if (site == "汽车")
                        {
                            lbl_NS_QC.Text = "非常抱歉，今天发布资讯次数已使用完";
                            jct_QC_News.Enabled = false;
                        }
                        else
                        {
                            lbl_NS_YC.Text = "非常抱歉，今天发布资讯次数已使用完";
                            jct_YC_News.Enabled = false;
                        }
                    }
                    else
                    {
                        LoadJob_News();
                    }
                }
                else
                {
                    LoadJob_News();
                }
                #endregion
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message + ex.StackTrace);
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
        private void LoadOrder()
        {
            if (site == "汽车")
            {
                LoadOrder_QC();
            }
            else
            {
                LoadOrder_YC();
            }
        }

        #region 汽车之家
        private void LoadOrder_QC()
        {
            ddlProvince.DisplayMember = "Pro";
            ddlProvince.ValueMember = "ProId";

            ddlCity.DisplayMember = "City";
            ddlCity.ValueMember = "CityId";

            ddlSeries.DisplayMember = "Text";
            ddlSeries.ValueMember = "Value";

            ddlOrderType.DisplayMember = "Text";
            ddlOrderType.ValueMember = "Value";

            var province = dal.GetProvince();
            province.Insert(0, new Area { ProId = "0", Pro = "全部省份" });
            ddlProvince.DataSource = province;
            ddlProvince.SelectedIndex = 0;
            ddlProvince.SelectedIndexChanged += ddlProvince_SelectedIndexChanged;

            ddlCity.DataSource = new List<Area>() { new Area { CityId = "0", City = "全部城市" } };
            ddlCity.SelectedIndex = 0;

            var doc = qiche.LoadOrder();
            var series = doc.DocumentNode.SelectNodes("//*[@id=\"sel_series\"]");
            var ordertype = doc.DocumentNode.SelectNodes("//*[@id=\"sel_orderType\"]");

            List<TextValue> seriesList = new List<TextValue>();
            foreach (HtmlNode node in series[0].ChildNodes)
            {
                if (node.Name == "option")
                {
                    seriesList.Add(new TextValue
                    {
                        Text = node.NextSibling.OuterHtml.Replace("&nbsp;", ""),
                        Value = node.GetAttributeValue("value", "") + ":" + node.GetAttributeValue("factoryid", "")
                    });
                }
            }

            List<TextValue> ordertypeList = new List<TextValue>();
            foreach (HtmlNode node in ordertype[0].ChildNodes)
            {
                if (node.Name == "option")
                {
                    ordertypeList.Add(new TextValue
                    {
                        Text = node.NextSibling.OuterHtml,
                        Value = node.GetAttributeValue("value", "")
                    });
                }
            }

            ddlSeries.DataSource = seriesList;
            ddlOrderType.DataSource = ordertypeList;

            var nicks = qiche.GetNicks();
            dgvOrder.DataSource = nicks;
        }

        private void SendOrder_QC()
        {
            qiche.pid = ddlProvince.SelectedValue.ToString();
            qiche.cid = ddlCity.SelectedValue.ToString();
            string[] series = ddlSeries.SelectedValue.ToString().Split(':');
            qiche.sid = series[0];
            qiche.fid = series[1];
            qiche.oid = ddlOrderType.SelectedValue.ToString();
            qiche.nicks = new List<Nicks>();
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Cells[colSelected.Name].Value.ToString() == "True")
                {
                    qiche.nicks.Add(new Nicks { Nick = row.Cells[colSaleName.Name].Value.ToString(), Id = row.Cells[colSaleID.Name].Value.ToString() });
                }
            }

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
            }
        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var proid = ddlProvince.SelectedValue.ToString();
            var city = dal.GetCity(proid);
            city.Insert(0, new Area { CityId = "0", City = "全部城市" });
            ddlCity.DataSource = city;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            th_qc.Abort();
            btnSendOrder.Enabled = true;
        }
        #endregion

        #region 易车网
        private void LoadOrder_YC()
        {
            ddlPro_YC.DisplayMember = "Text";
            ddlPro_YC.ValueMember = "Value";

            ddlCity_YC.DisplayMember = "Text";
            ddlCity_YC.ValueMember = "Value";

            ddlType_YC.ValueMember = "Value";
            ddlType_YC.DisplayMember = "Text";

            List<TextValue> pro = new List<TextValue>();
            List<TextValue> type = new List<TextValue>();
            List<TextValue> city = new List<TextValue>();

            var htmlDoc = yiche.GoToOrder();
            var script = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"Head1\"]/script[7]/text()").InnerText.Replace(" ", "").Replace("\r", "").Split('\n');
            foreach (string str in script)
            {
                if (str.StartsWith("SetOrderType:"))
                {
                    type = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetOrderType:", ""));
                }
                else if (str.StartsWith("SetProvince:"))
                {
                    pro = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetProvince:", ""));
                }
                else if (str.StartsWith("SetLocation:"))
                {
                    city = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetLocation:", ""));
                }
                else if (str.Contains("DealerId"))
                {
                    dealerid_yc = str.Replace("data: { DealerId: ", "").Replace(", ProvId: provId },", "");
                }

                if (pro.Count > 0 && type.Count > 0 && city.Count > 0 && !string.IsNullOrWhiteSpace(dealerid_yc))
                    break;
            }
            ddlPro_YC.DataSource = pro;
            ddlPro_YC.SelectedIndexChanged += ddlPro_YC_SelectedIndexChanged;
            ddlType_YC.DataSource = type;
            ddlCity_YC.DataSource = city;
        }

        void ddlPro_YC_SelectedIndexChanged(object sender, EventArgs e)
        {
            var proid = ddlPro_YC.SelectedValue.ToString();
            var doc = yiche.LoadCityByPro(dealerid_yc, proid);
            var city = JsonConvert.DeserializeObject<List<TextValue>>(doc.DocumentNode.OuterHtml);
            ddlCity_YC.DataSource = city;
        }

        private void btnStart_YC_Click(object sender, EventArgs e)
        {
            btnStart_YC.Enabled = false;
            SendOrder_YC();
        }

        private void SendOrder_YC()
        {
            yiche.Type = ddlType_YC.SelectedValue.ToString();
            yiche.Pro = ddlPro_YC.SelectedValue.ToString();
            yiche.City = ddlCity_YC.SelectedValue.ToString();

            yiche.SendOrderEvent += Yiche_SendOrderEvent;
            th_yc = new Thread(yiche.SendOrder);
            th_yc.Start();
        }

        private void Yiche_SendOrderEvent(ViewResult vr)
        {
            this.Invoke(new Action(() =>
            {                
                if (vr.Result)
                    lbxSendOrder_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + vr.Message);

                LoadUser(Tool.userInfo_yc);
            }));
        }

        private void btnStop_YC_Click(object sender, EventArgs e)
        {
            th_yc.Abort();
            btnStart_YC.Enabled = true;
        }
        #endregion

        #endregion

        #region 报价

        private void LoadJob_Query()
        {
            if (site == "汽车")
            {
                job_qc_quote = dal.GetJob("汽车之家报价");
                jct_QC_Query.SetJobEvent += jct_QC_Query_SetJobEvent;
                jct_QC_Query.SetJob(job_qc_quote);                
            }
            else
            {
                job_yc_quote = dal.GetJob("易车网报价");
                jct_YC_Query.SetJobEvent += jct_YC_Query_SetJobEvent;
                jct_YC_Query.SetJob(job_yc_quote);                
            }
        }

        private void LoadJob_News()
        {
            if (site == "汽车")
            {
                job_qc_news = dal.GetJob("汽车之家新闻");
                jct_QC_News.SetJobEvent += jc_QC_News_SetJobEvent;
                jct_QC_News.SetJob(job_qc_news);                
            }
            else
            {
                job_yc_news = dal.GetJob("易车网新闻");
                jct_YC_News.SetJobEvent += jct_YC_News_SetJobEvent;
                jct_YC_News.SetJob(job_yc_news);
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
        private void tm_qc_quer_Tick(object sender, EventArgs e)
        {
            tm_qc_quer.Enabled = false;
            ExecJob(job_qc_quote, SavePrice_QC);
            tm_qc_quer.Interval = job_qc_quote.Space.Value;
            LoadUser(Tool.userInfo_qc);
            tm_qc_quer.Enabled = job_qc_quote.JobType != 1 && jct_QC_Query.Enabled;
        }

        private void SavePrice_QC()
        {
            var result = qiche.SavePrice();
            lbxQuer.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);            
            dal.AddJobLog(new JobLog { JobID = job_qc_quote.ID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            if (result.Result)
            {
                Tool.service.UpdateLastQuoteTime(Tool.userInfo_qc.Id);
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "报价", JobTime = DateTime.Now });
                Tool.userInfo_qc.QueryNum--;
            }
        }

        void jct_QC_Query_SetJobEvent(Job job)
        {
            job_qc_quote = job;
            job_qc_quote.JobName = "汽车之家报价";
            dal.AddJob(job_qc_quote);
            tm_qc_quer.Enabled = true;
        }
        #endregion

        #region 易车网
        private void tm_yc_query_Tick(object sender, EventArgs e)
        {
            tm_yc_query.Enabled = false;
            ExecJob(job_yc_quote, SavePrice_YC);
            LoadUser(Tool.userInfo_qc);            
            tm_yc_query.Enabled = job_yc_quote.JobType != 1 && jct_YC_Query.Enabled;
        }

        private void SavePrice_YC()
        {
            var result = yiche.SavePrice();
            lbxQuer_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);            
            dal.AddJobLog(new JobLog { JobID = job_yc_quote.ID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            if (result.Result)
            {
                Tool.service.UpdateLastQuoteTime(Tool.userInfo_yc.Id);
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "报价", JobTime = DateTime.Now });
                Tool.userInfo_yc.QueryNum--;
            }
        }

        private void jct_YC_Query_SetJobEvent(Job job)
        {
            job_yc_quote = job;
            job_yc_quote.JobName = "易车网报价";
            dal.AddJob(job_yc_quote);
            tm_yc_query.Enabled = true;
        }
        #endregion

        #endregion

        #region 资讯
        #region 汽车之家
        void jc_QC_News_SetJobEvent(Job job)
        {
            job_qc_news = job;
            job_qc_news.JobName = "汽车之家新闻";
            dal.AddJob(job_qc_news);
            tm_qc_news.Enabled = true;
        }
        private void SaveNews_QC()
        {
            var result = qiche.PostNews();
            lblNews.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":成功发布新闻" + result + "条");
            dal.AddJobLog(new JobLog { JobID = job_qc_news.ID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
            if (result > 0)
            {
                Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_qc.Id, JobType = "资讯", JobTime = DateTime.Now });
                Tool.userInfo_qc.NewsNum--;
            }
        }       

        private void tm_qc_news_Tick(object sender, EventArgs e)
        {
            tm_qc_news.Enabled = false;
            ExecJob(job_qc_news, SaveNews_QC);
            tm_qc_news.Interval = job_qc_news.Space.Value;
            LoadUser(Tool.userInfo_qc);
            tm_qc_news.Enabled = job_qc_news.JobType != 1 && jct_QC_News.Enabled;
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
                return;
            }

            foreach(News news in newsBindingSource.DataSource as List<News>)
            {
                var result = yiche.PostNews(news.ID);
                if(result == "发布成功")
                {
                    listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + news.Title + " 发布成功");
                    dal.AddJobLog(new JobLog { JobID = job_yc_news.ID, Time = DateTime.Now.ToString("yyyy-MM-dd") });
                    Tool.service.AddJobLog(new Service.JobLog { UserID = Tool.userInfo_yc.Id, JobType = "资讯", JobTime = DateTime.Now });
                    Tool.userInfo_yc.NewsNum--;
                }
                else
                {
                    listBox3.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + news.Title + " 发布失败," + result);
                }
            }
        }

        private void tm_yc_news_Tick(object sender, EventArgs e)
        {
            tm_yc_news.Enabled = false;
            ExecJob(job_yc_news, SaveNews_YC);
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

            }
            else
            {
                dal.DelNews(((News)newsBindingSource.Current).ID);
                newsBindingSource.DataSource = dal.GetNewsList();
                dataGridView1.Refresh();
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
}