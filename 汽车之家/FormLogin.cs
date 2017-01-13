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

namespace Aide
{
    public partial class FormLogin : Form
    {
        QiCheZhiJia qiche;
        YiChe yiche;
        string site;
        DAL dal = new DAL();
        Thread th_qc;
        string dealerid_yc = "";

        public FormLogin()
        {
            InitializeComponent();
        }

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
            dtpQuer.Value = new DateTime(2000, 01, 01, 0, 0, 0);
#if DEBUG
            if (site == "汽车")
            {
                txtUserName.Text = "晋江嘉华雷克萨斯";
            }
            else
            {
                txtUserName.Text = "344801178@qq.com";
            }
            txtPassword.Text = "qzzs8888.";
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewResult result = new ViewResult();
            if(site == "汽车")
                result = qiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            else
                result = yiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            
            if (!result.Result)
            {
                MessageBox.Show(result.Message);
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
                    LoadOrder_QC();
                    var job_qc = dal.GetJob("汽车之家报价");
                    if (job_qc != null)
                    {
                        var _qc = job_qc.Time.Split(':');
                        dtpQuer.Value = new DateTime(2000, 01, 01, Convert.ToInt32(_qc[0]), Convert.ToInt32(_qc[1]), Convert.ToInt32(_qc[2]));
                        lblState.Text = "已设置";
                        tm_qc_quer.Enabled = true;
                    }
                }
                else
                {
                    if (chkSavePass.Checked)
                    {
                        yiche.SavePw();
                    }
                    LoadUser(Tool.userInfo_yc);
                    LoadOrder_YC();
                    var job = dal.GetJob("易车网报价");
                    if (job != null)
                    {
                        var _qc = job.Time.Split(':');
                        dtpQuer_YC.Value = new DateTime(2000, 01, 01, Convert.ToInt32(_qc[0]), Convert.ToInt32(_qc[1]), Convert.ToInt32(_qc[2]));
                        lblState_YC.Text = "已设置";
                        tm_yc_query.Enabled = true;
                    }
                }                
            }         
        }

        private void btnRefImg_Click(object sender, EventArgs e)
        {
            LoadValidateCode();
        }        

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

        private void InitUser()
        {
            if(site == "汽车")
            {
                if(Tool.userInfo_qc == null)
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
            lblCode.Text = user.Id.ToString();
            lblEnd.Text = user.DueTime.HasValue ? user.DueTime.ToString() : "";
            lblUserName.Text = user.UserName;
            lblUserType.Text = user.UserType == 0 ? "试用" : "付费";
        }

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
            foreach(HtmlNode node in series[0].ChildNodes)
            {
                if(node.Name == "option")
                {
                    seriesList.Add(new TextValue {
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
                if(row.Cells[colSelected.Name].Value.ToString() == "True")
                {
                    qiche.nicks.Add(new Nicks { Nick = row.Cells[colSaleName.Name].Value.ToString(), Id = row.Cells[colSaleID.Name].Value.ToString() });
                }
            }

            qiche.SendOrderEvent += qiche_SendOrderEvent;
            th_qc = new Thread(qiche.SendOrder);
            th_qc.Start();
        }

        void qiche_SendOrderEvent(ViewResult vr)
        {
            this.Invoke(new Action(() => {
                if(vr.Result)
                    lbxSendOrder.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +":"+ vr.Message);
            }));
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            btnSendOrder.Enabled = false;
            SendOrder_QC();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvOrder.Rows)
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

        private void btnSetting_QC_Click(object sender, EventArgs e)
        {
            dal.AddJob(new Job { JobName = "汽车之家报价", Time = dtpQuer.Value.ToString("HH:mm:ss") });
            lblState.Text = "已设置";
            tm_qc_quer.Enabled = true;
        }

        private void tm_qc_quer_Tick(object sender, EventArgs e)
        {
            tm_qc_quer.Enabled = false;
            if (DateTime.Now.Hour == dtpQuer.Value.Hour && DateTime.Now.Minute == dtpQuer.Value.Minute)
            {
                var result = qiche.SavePrice();
                lbxQuer.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);
            }
            //tm_qc_quer.Enabled = true;
        }

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
            foreach(string str in script)
            {
                if(str.StartsWith("SetOrderType:"))
                {
                    type = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetOrderType:", ""));
                }
                else if(str.StartsWith("SetProvince:"))
                {
                    pro = JsonConvert.DeserializeObject<List<TextValue>>(str.TrimEnd(',').Replace("SetProvince:", ""));
                }
                else if(str.StartsWith("SetLocation:"))
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
            var type = ddlType_YC.SelectedValue.ToString();
            var pro = ddlPro_YC.SelectedValue.ToString();
            var city = ddlCity_YC.SelectedValue.ToString();            
            var htmlDoc = yiche.LoadOrder(type, pro, city);
            var strcount = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"commonarea\"]/ul/li[2]/strong").InnerText.Trim();
            int ordercount = 0;
            int.TryParse(strcount, out ordercount);
            while(ordercount > 0)
            {

            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Tool.userInfo_qc != null)
            {
                Tool.service.UpdateLoginLogByLogOut(Tool.userInfo_qc.Id);
            }

            if(Tool.userInfo_yc != null)
            {
                Tool.service.UpdateLoginLogByLogOut(Tool.userInfo_yc.Id);
            }
        }

        private void btnStop_YC_Click(object sender, EventArgs e)
        {
            yiche.SavePrice();
        }

        private void btnSetting_YC_Click(object sender, EventArgs e)
        {
            dal.AddJob(new Job { JobName = "易车网报价", Time = dtpQuer_YC.Value.ToString("HH:mm:ss") });
            lblState_YC.Text = "已设置";
            tm_yc_query.Enabled = true;
        }

        private void tm_yc_query_Tick(object sender, EventArgs e)
        {
            tm_yc_query.Enabled = false;
            if (DateTime.Now.Hour == dtpQuer_YC.Value.Hour && DateTime.Now.Minute == dtpQuer_YC.Value.Minute)
            {
                var result = yiche.SavePrice();
                lbxQuer_YC.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + result.Message);
            }
            //tm_yc_query.Enabled = true;
        }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
