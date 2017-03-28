using ExcelApp = Microsoft.Office.Interop.Excel;
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
            timer1.Enabled = true;
        }

        private void LoadUserData()
        {
            var data = new List<localhost.User>();
            data.AddRange(Tool.service.GetUserList(type, status, due));
            UserList = new BindingCollection<localhost.User>(data);
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
            if(!chkSendOrder.Checked && !chkQuery.Checked && !chkNews.Checked)
            {
                MessageBox.Show("请至少选择一种付费方式");
                return;
            }

            int id = GetUserID();
            if (id > 0)
            {
                var r = Tool.service.UpdateUserType(id, chkSendOrder.Checked, chkQuery.Checked, chkNews.Checked);

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
            chkSendOrder.Checked = SelectedUser.SendOrder;
            chkQuery.Checked = SelectedUser.Query;
            chkNews.Checked = SelectedUser.News;
        }

        /// <summary>
        /// 分配用户-角色下的用户列表
        /// </summary>
        private BindingCollection<localhost.User> UserList
        {
            get
            {
                return userBindingSource.DataSource as BindingCollection<localhost.User>;
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

        private void btnExprot_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog file = new SaveFileDialog())
            {
                file.Title = "导出Excel";
                file.DefaultExt = ".xls";
                file.Filter = "Excel文件|*.xls";
                file.FileName = "用户数据" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (file.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                ExportExcel(file.FileName, dgvData);
            }
        }

        public static void ExportExcel(string fileName, DataGridView myDGV)
        {
            if (myDGV.Rows.Count > 0)
            {
                ExcelApp.Application xlApp = new ExcelApp.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }

                ExcelApp.Workbooks workbooks = xlApp.Workbooks;
                ExcelApp.Workbook workbook = workbooks.Add(ExcelApp.XlWBATemplate.xlWBATWorksheet);
                ExcelApp.Worksheet worksheet = (ExcelApp.Worksheet)workbook.Worksheets[1];//取得sheet1  

                //写入标题
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < myDGV.Rows.Count; r++)
                {
                    for (int i = 0; i < myDGV.ColumnCount; i++)
                    {
                        if (myDGV.Columns[i].HeaderText == "类型")
                            worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value.ToString() == "0" ? "试用" : "付费";
                        else if(myDGV.Columns[i].HeaderText == "状态")
                            worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value.ToString() == "0" ? "离线" : "运行";
                        else
                            worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                    }
                    Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();

                if (fileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }
                }
                xlApp.Quit();
                GC.Collect();
                MessageBox.Show(fileName + "保存成功", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            var userlist = Tool.service.GetUserList(type, status, due);
            foreach(DataGridViewRow row in dgvData.Rows)
            {
                var userid = row.Cells[colID.Name].Value.ToString();
                foreach(localhost.User user in userlist)
                {
                    if(user.Id.ToString() == userid)
                    {
                        row.Cells[statusDataGridViewTextBoxColumn.Name].Value = user.Status;
                        row.Cells[lastAllotTimeDataGridViewTextBoxColumn.Name].Value = user.LastAllotTime;
                        row.Cells[colQuote.Name].Value = user.LastQuoteTime;
                        row.Cells[colNews.Name].Value = user.LastNewsTime;
                        InitData();
                        break;
                    }
                }
            }
            timer1.Enabled = true;
        }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
