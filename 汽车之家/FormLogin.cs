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
            yiche = new YiChe();            
            InitUser();

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
                //if (!result.Exit)
                //    return;
                //this.DialogResult = DialogResult.No;
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
                }
                else
                {
                    if (chkSavePass.Checked)
                    {
                        yiche.SavePw();
                    }                    
                    LoadUser(Tool.userInfo_yc);
                }
                //this.DialogResult = DialogResult.OK;
                
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

        private void InitDDL()
        {
            ddlProvince.DisplayMember = "ProId";
            ddlProvince.ValueMember = "Pro";
            ddlCity.Text = "全部省份";

            ddlCity.DisplayMember = "CityId";
            ddlCity.ValueMember = "City";
            ddlCity.Text = "全部城市";

            ddlSeries.DisplayMember = "Text";
            ddlSeries.ValueMember = "Value";
            ddlSeries.Text = "全部车系";

            ddlOrderType.DisplayMember = "Text";
            ddlOrderType.ValueMember = "Value";
            ddlSeries.Text = "全部类型";
        }

        private void LoadOrder_QC()
        {
            ddlProvince.DisplayMember = "ProId";
            ddlProvince.ValueMember = "Pro";

            ddlCity.DisplayMember = "CityId";
            ddlCity.ValueMember = "City";

            ddlSeries.DisplayMember = "Text";
            ddlSeries.ValueMember = "Value";

            ddlOrderType.DisplayMember = "Text";
            ddlOrderType.ValueMember = "Value";

            //var province = dal.GetProvince();
            //province.Insert(0, new Area { ProId = "-1", Pro = "全部省份" });
            //ddlProvince.DataSource = province;
            //ddlProvince.SelectedIndex = 0;

            ddlCity.Items.Add("全部城市");

            var doc = qiche.LoadOrder();
            var series = doc.DocumentNode.SelectNodes("//*[@id=\"sel_series\"]");
            var ordertype = doc.DocumentNode.SelectNodes("//*[@id=\"sel_orderType\"]");

            List<TextValue> seriesList = new List<TextValue>();
            foreach(HtmlNode node in series[0].ChildNodes)
            {
                if(node.Name == "option")
                {
                    seriesList.Add(new TextValue{
                        Text = node.NextSibling.OuterHtml.Replace("&nbsp", ""),
                        Value = node.GetAttributeValue("value", "")
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
        }

        private void SendOrder_QC()
        {
            qiche.pid = ddlProvince.SelectedValue.ToString();
            qiche.cid = ddlCity.SelectedValue.ToString();
            qiche.sid = ddlSeries.SelectedValue.ToString();
            qiche.oid = ddlOrderType.SelectedValue.ToString();

            qiche.SendOrderEvent += qiche_SendOrderEvent;

            Thread th = new Thread(qiche.SendOrder);
            th.Start();
        }

        void qiche_SendOrderEvent(ViewResult vr)
        {
            this.Invoke(new Action(() => {
                Console.WriteLine(vr.Message);
            }));
        }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
