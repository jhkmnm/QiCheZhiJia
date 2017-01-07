using System;
using System.Text;
using System.Windows.Forms;
using CsharpHttpHelper;
using System.IO;
using System.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Model;

namespace Aide
{
    public partial class FormLogin : Form
    {
        QiCheZhiJia qiche;
        YiChe yiche;
        string site;

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
            site = "车";

            string path = AppDomain.CurrentDomain.BaseDirectory + "js.lyt";
            if (!File.Exists(path))
            {
                MessageBox.Show("js.lyt文件缺失，建议重新解压软件解决！");
                base.Close();
            }
            qiche = new QiCheZhiJia(File.ReadAllText(path));
            yiche = new YiChe();
            this.LoadPw();
            LoadValidateCode();

#if DEBUG
            txtUserName.Text = "晋江嘉华雷克萨斯";
            txtPassword.Text = "qzzs8888.";
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = qiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            if (!result.Result)
            {
                MessageBox.Show(result.Message);
                if (!result.Exit)
                    return;
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                if (chkSavePass.Checked)
                    qiche.SavePw();
            }

            this.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            var result = yiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            if (!result.Result)
            {
                MessageBox.Show(result.Message);
                if (!result.Exit)
                    return;
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                if (chkSavePass.Checked)
                    qiche.SavePw();
            }

            this.Close();
        }
    }
}
