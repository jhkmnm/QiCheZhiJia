using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aide
{
    public partial class FormLogin : Form
    {
        public QiCheZhiJia qiche;
        public YiChe yiche;

        public FormLogin(Site site)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            Tool.site = site;
            LoadLogin();
        }

        private void LoadLogin()
        {
            if (Tool.site == Aide.Site.Qiche)
            {
                rbtQC.Checked = true;
                string path = AppDomain.CurrentDomain.BaseDirectory + "js.lyt";
                if (!File.Exists(path))
                {
                    MessageBox.Show("js.lyt文件缺失，建议重新解压软件解决！");
                    base.Close();
                }
                qiche = new QiCheZhiJia(File.ReadAllText(path));
            }
            else
            {
                rbtYC.Checked = true;
                yiche = new YiChe();
            }

            ClearText();
            LoadPw();
            LoadValidateCode();
        }

        private void ClearText()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtCode.Text = "";
        }

        /// <summary>
        /// 加载密码
        /// </summary>
        private void LoadPw()
        {
            string[] pw = new string[3];
            if (Tool.site == Aide.Site.Qiche)
            {
                pw = qiche.LoadPw();                
            }
            else
            {
                pw = yiche.LoadPw();
            }

            if (!string.IsNullOrWhiteSpace(pw[0]))
            {
                chkSavePass.Checked = true;
                txtUserName.Text = pw[0];
                txtPassword.Text = pw[1];
            }
        }

        /// <summary>
        /// 加载验证码
        /// </summary>
        private void LoadValidateCode()
        {
            if (Tool.site == Aide.Site.Qiche)
            {
                var str = qiche.GotoLoginPage();
                if (string.IsNullOrWhiteSpace(str))
                    pbCode.Image = qiche.LoadValidateCode();
                else
                    MessageBox.Show(str);
            }
            else
            {
                yiche.GotoLoginPage();
                pbCode.Image = yiche.LoadValidateCode();
            }
        }

        private void btnRefImg_Click(object sender, EventArgs e)
        {
            LoadValidateCode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewResult result = new ViewResult();
            if (Tool.site == Aide.Site.Qiche)
                result = qiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);
            else
                result = yiche.Login(txtUserName.Text, txtPassword.Text, txtCode.Text);

            if (!result.Result)
            {
                MessageBox.Show(result.Message);
                if (result.Message.Contains("验证码输入有误"))
                {
                    LoadValidateCode();
                }
            }
            else
            {
                if (chkSavePass.Checked)
                {
                    if (Tool.site == Aide.Site.Qiche)
                    {
                        qiche.SavePw();
                    }
                    else
                    {
                        yiche.SavePw();
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void rbtQC_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            if (rbt.Checked)
            {
                if(rbt.Name == rbtQC.Name)
                    Tool.site = Aide.Site.Qiche;
                else
                    Tool.site = Aide.Site.Yiche;

                LoadLogin();
            }
        }
    }

    public enum Site
    {
        Qiche,
        Yiche
    }
}
