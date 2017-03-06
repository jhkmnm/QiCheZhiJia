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
            chkSendOrder.Checked = SelectedUser.SendOrder;
            chkQuery.Checked = SelectedUser.Query;
            chkNews.Checked = SelectedUser.News;
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

        private void btnExprot_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog file = new SaveFileDialog())
            {
                file.Title = "导出Excel";
                file.DefaultExt = ".xls";
                file.Filter = "Excel文件|*.xls";
                file.FileName = "用户数据" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (file.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //SaveExcelFile(dgvData, file.FileName, ".xls", "sheet", null);
            }
        }

        private static int CalculateCountOfPage(int total, int pageSize)
        {
            int remainder;
            int countOfPage = Division(total, pageSize, out remainder);
            if (remainder > 0)
            {
                countOfPage++;
            }
            return countOfPage;
        }

        private static int Division(int divided, int divisor, out int remainder)
        {
            int quotient = divided / divisor;
            remainder = divided - quotient * divisor;
            return quotient;
        }
        /// <summary>
        /// 童荣辉增加 20130724 抽取出共用的代码，以适应与弹出保存框区分开
        /// DataGridView数据展出到Excel
        /// </summary>
        private static bool SaveExcelFile(DataGridView gridView, string strfileName, string strVersion, string sheetName, List<string> nonColumns)
        {
            return true;
            //int maxLength = 65535;
            //if (strVersion.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            //{
            //    maxLength = 1048575;
            //}
            //string saveFileName = strfileName;
            //System.Reflection.Missing miss = System.Reflection.Missing.Value;
            ////创建EXCEL对象appExcel,Workbook对象,Worksheet对象,Range对象
            //ExcelApp.Application appExcel = new ExcelApp.Application();
            //ExcelApp.Workbook workbookData = appExcel.Workbooks.Add(ExcelApp.XlWBATemplate.xlWBATWorksheet);
            //ExcelApp.Worksheet worksheetData = null;
            //ExcelApp.Range rangedata;
            ////设置对象不可见
            //appExcel.Visible = false;
            //int countOfSheets = CalculateCountOfPage(gridView.RowCount, maxLength);
            ///* 在调用Excel应用程序，或创建Excel工作簿之前，记着加上下面的两行代码
            // * 这是因为Excel有一个Bug，如果你的操作系统的环境不是英文的，而Excel就会在执行下面的代码时，报异常。
            // */
            //for (int ipage = 1; ipage <= countOfSheets; ipage++)
            //{
            //    if (worksheetData == null)
            //    {
            //        worksheetData = (ExcelApp.Worksheet)workbookData.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            //    }
            //    else
            //    {
            //        worksheetData = (ExcelApp.Worksheet)workbookData.Worksheets.Add(Type.Missing, worksheetData, 1, Type.Missing);
            //    }
            //    //当前页数据条数
            //    int currentPageNums = 0;
            //    //给工作表赋名称
            //    if (countOfSheets == 1)
            //    {
            //        worksheetData.Name = sheetName;
            //        currentPageNums = gridView.RowCount;
            //    }
            //    else
            //    {
            //        worksheetData.Name = string.Format("{0}{1}", sheetName, ipage);
            //        if (ipage == countOfSheets)
            //        {
            //            currentPageNums = gridView.RowCount - maxLength * (ipage - 1);
            //        }
            //        else
            //        {
            //            currentPageNums = maxLength;
            //        }
            //    }
            //    //新建一个字典来控制Visible的列不导出
            //    Dictionary<int, int> dictionary = new Dictionary<int, int>();
            //    int iVisible = 0;
            //    //清零计数并开始计数
            //    // 保存到WorkSheet的表头，你应该看到，是一个Cell一个Cell的存储，这样效率特别低，解决的办法是，使用Rang，一块一块地存储到Excel
            //    for (int i = 0; i < gridView.ColumnCount; i++)
            //    {
            //        if (gridView.Columns[i].Visible &&
            //            ((nonColumns != null && !nonColumns.Contains(gridView.Columns[i].Name)) || nonColumns == null))
            //        {
            //            worksheetData.Cells[1, iVisible + 1] = gridView.Columns[i].HeaderText.ToString();
            //            var range = worksheetData.Cells[1, iVisible + 1];
            //            range.Font.Bold = true;
            //            range.Font.Size = 12;
            //            dictionary.Add(iVisible, i);
            //            iVisible++;
            //        }
            //    }
            //    //先给Range对象一个范围为A2开始，Range对象可以给一个CELL的范围，也可以给例如A1到H10这样的范围
            //    //因为第一行已经写了表头，所以所有数据都应该从A2开始
            //    rangedata = worksheetData.get_Range("A2", miss);
            //    Microsoft.Office.Interop.Excel.Range xlRang = null;

            //    //iColumnAccount为实际列数，最大列数
            //    int iColumnAccount = iVisible;
            //    //在内存中声明一个iEachSize×iColumnAccount的数组，iEachSize是每次最大存储的行数，iColumnAccount就是存储的实际列数
            //    //object[,] objVal = new object[currentPageNums, iColumnAccount];
            //    //每次最大导入数据量
            //    int perMaxCount = 2000;
            //    //当前行
            //    int iParstedRow = 0;
            //    int times = CalculateCountOfPage(currentPageNums, perMaxCount);
            //    for (int ti = 0; ti < times; ti++)
            //    {
            //        //当前循环的得到的数
            //        int currentTimeNum = 0;
            //        if ((currentPageNums - ti * perMaxCount) < perMaxCount)
            //        {
            //            currentTimeNum = currentPageNums - ti * perMaxCount;
            //        }
            //        else
            //        {
            //            currentTimeNum = perMaxCount;
            //        }
            //        object[,] objVal = new object[currentTimeNum, iColumnAccount];
            //        for (int i = 0; i < currentTimeNum; i++)
            //        {
            //            for (int j = 0; j < iColumnAccount; j++)
            //            {
            //                int numOfColumn;
            //                if (!dictionary.TryGetValue(j, out numOfColumn))
            //                {
            //                    throw new KeyNotFoundException("导出Excel异常，未找到列！");
            //                }
            //                object cellValue = gridView[numOfColumn, ti * perMaxCount + i + (ipage - 1) * maxLength].Value;
            //                if (cellValue != null && (cellValue.GetType() == typeof(string) || cellValue.GetType() == typeof(DateTime)))
            //                {
            //                    cellValue = "'" + cellValue.ToString();
            //                }
            //                objVal[i, j] = cellValue;
            //            }
            //            System.Windows.Forms.Application.DoEvents();
            //        }
            //        xlRang = worksheetData.get_Range("A" + (iParstedRow + 2).ToString(), (ExcelColumnNameEnum.A + iColumnAccount - 1).ToString() + (currentTimeNum + iParstedRow + 1).ToString());
            //        // 调用Range的Value2属性，把内存中的值赋给Excel
            //        xlRang.Value2 = objVal;

            //        iParstedRow += currentTimeNum;
            //    }
            //}
            //try
            //{
            //    workbookData.Saved = true;
            //    workbookData.SaveCopyAs(saveFileName);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return false;
            //}
            //finally
            //{
            //    QuitExcel(appExcel);
            //}
        }

        private static void QuitExcel(ExcelApp.Application application)
        {
            application.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
        }

        public enum ExcelColumnNameEnum
        {
            A = 1, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
            AA, AB, AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AM, AN, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
            BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BM, BN, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ,
            CA, CB, CC, CD, CE, CF, CG, CH, CI, CJ, CK, CL, CM, CN, CO, CP, CQ, CR, CS, CT, CU, CV, CW, CX, CY, CZ
        }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
