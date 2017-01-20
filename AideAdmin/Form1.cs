using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AideAdmin
{
    public partial class Form1 : Form
    {        
        int type = -1;
        int status = -1;
        int due = -1;

        public Form1()
        {
            InitializeComponent();

            var utype = new List<TextValue>();
            var ustatus = new List<TextValue>();

            utype.AddRange(new[] { new TextValue { Text = "试用", Value = 0 }, new TextValue { Text = "付费", Value = 1 } });
            ustatus.AddRange(new[] { new TextValue { Text = "离线", Value = 0 }, new TextValue { Text = "运行", Value = 1 } });

            textValueBindingSource.DataSource = utype;
            textValueBindingSource1.DataSource = ustatus;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            var data = new List<localhost.User>();
            data.AddRange(Tool.service.GetUserList(type, status, due));
            UserList = data;
            InitData();
        }

        private int GetUserID()
        {
            int id = 0;
            int.TryParse(txtID.Text, out id);
            if (id == 0)
            {
                MessageBox.Show("请选择用户");                
            }
            return id;
        }

        private void btnUpDuetime_Click(object sender, EventArgs e)
        {
            int id = GetUserID();
            if (id > 0)
            {
                DateTime dt = dtpDueTime.Value;

                var v = Tool.service.UpdUserDueTime(id, dt);
            }
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            int id = GetUserID();
            if (id > 0)
            {
                var r = Tool.service.DelUser(id);

                if (r > 0)
                {
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
        }

        private void btnUpdType_Click(object sender, EventArgs e)
        {
            int id = GetUserID();
            if (id > 0)
            {
                var r = Tool.service.UpdateUserType(id);

                if (r > 0)
                {
                    MessageBox.Show("设置成功");
                }
                else
                {
                    MessageBox.Show("设置失败");
                }
            }
        }

        private void rbtType_CheckedChanged(object sender, EventArgs e)
        {
            switch(((RadioButton)sender).Name)
            {
                case "rbtType_a":
                    type = -1;
                    break;
                case "rbtType_b":
                    type = 0;
                    break;
                case "rbtType_c":
                    type = 1;
                    break;
            }
        }

        private void rbtStatus_CheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "rbtStatus_a":
                    status = -1;
                    break;
                case "rbtStatus_b":
                    status = 0;
                    break;
                case "rbtStatus_c":
                    status = 1;
                    break;
            }
        }

        private void rbtDue_CheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "rbtDue_a":
                    due = -1;
                    break;
                case "rbtDue_b":
                    due = 0;
                    break;
                case "rbtDue_c":
                    due = 1;
                    break;
            }
        }

        private void btnDic_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            InitData();
        }

        private void InitData()
        {
            if (SelectedUser == null) return;

            txtID.Text = SelectedUser.Id.ToString();
            txtSiteName.Text = SelectedUser.SiteName;
            txtUserName.Text = SelectedUser.UserName;
            txtPassword.Text = SelectedUser.PassWord;
            txtCompany.Text = SelectedUser.Company;
            txtLinkman.Text = SelectedUser.LinkInfo;
            dtpDueTime.Value = SelectedUser.DueTime == null ? DateTime.Today : SelectedUser.DueTime.Value;
            txtType.Text = SelectedUser.UserType == 0 ? "试用" : "付费";
            txtStatus.Text = SelectedUser.Status == 0 ? "离线" : "运行";
            txtLastAllTime.Text = SelectedUser.LastAllotTime == null ? "" : SelectedUser.LastAllotTime.Value.ToString();
            txtLastQuote.Text = SelectedUser.LastQuoteTime == null ? "" : SelectedUser.LastQuoteTime.Value.ToString();
            txtLastNews.Text = SelectedUser.LastNewsTime == null ? "" : SelectedUser.LastNewsTime.Value.ToString();
        }

        /// <summary>
        /// 分配用户-角色下的用户列表
        /// </summary>
        private List<localhost.User> UserList
        {
            get
            {
                return userBindingSource.DataSource as List<localhost.User>;
            }
            set
            {
                if (value == null)
                {
                    userBindingSource.Clear();
                }
                else
                {
                    userBindingSource.DataSource = value;
                    dgvData.Refresh();
                }
            }
        }

        private localhost.User SelectedUser
        {
            get { return userBindingSource.Current as localhost.User; }
        }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
