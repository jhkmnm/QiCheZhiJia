namespace Aide
{
    partial class FormLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbCode = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.chkSavePass = new System.Windows.Forms.CheckBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnRefImg = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbcJob_Query_QC = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ddlProvince = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.ddlCity = new System.Windows.Forms.ComboBox();
            this.dgvOrder = new System.Windows.Forms.DataGridView();
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSaleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSendOrder = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.ddlSeries = new System.Windows.Forms.ComboBox();
            this.ddlOrderType = new System.Windows.Forms.ComboBox();
            this.lbxSendOrder = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.jct_QC_Query = new Aide.JobControl();
            this.lbxQuer = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.jct_QC_News = new Aide.JobControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbxQuer_YC = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ddlPro_YC = new System.Windows.Forms.ComboBox();
            this.btnStop_YC = new System.Windows.Forms.Button();
            this.ddlCity_YC = new System.Windows.Forms.ComboBox();
            this.btnStart_YC = new System.Windows.Forms.Button();
            this.ddlType_YC = new System.Windows.Forms.ComboBox();
            this.lbxSendOrder_YC = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblUserType = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tm_qc_quer = new System.Windows.Forms.Timer(this.components);
            this.tm_yc_query = new System.Windows.Forms.Timer(this.components);
            this.jct_YC_Query = new Aide.JobControl();
            this.jct_YC_News = new Aide.JobControl();
            this.tm_qc_news = new System.Windows.Forms.Timer(this.components);
            this.tm_yc_news = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tbcJob_Query_QC.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCode
            // 
            this.pbCode.Location = new System.Drawing.Point(180, 84);
            this.pbCode.Name = "pbCode";
            this.pbCode.Size = new System.Drawing.Size(88, 25);
            this.pbCode.TabIndex = 0;
            this.pbCode.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(102, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 12F);
            this.txtCode.Location = new System.Drawing.Point(74, 84);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 26);
            this.txtCode.TabIndex = 2;
            // 
            // chkSavePass
            // 
            this.chkSavePass.AutoSize = true;
            this.chkSavePass.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSavePass.Location = new System.Drawing.Point(195, 122);
            this.chkSavePass.Name = "chkSavePass";
            this.chkSavePass.Size = new System.Drawing.Size(155, 20);
            this.chkSavePass.TabIndex = 3;
            this.chkSavePass.Text = "记住用户名、密码";
            this.chkSavePass.UseVisualStyleBackColor = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("宋体", 12F);
            this.txtUserName.Location = new System.Drawing.Point(74, 12);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(211, 26);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPassword.Location = new System.Drawing.Point(74, 48);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(211, 26);
            this.txtPassword.TabIndex = 5;
            // 
            // btnRefImg
            // 
            this.btnRefImg.Font = new System.Drawing.Font("宋体", 12F);
            this.btnRefImg.Location = new System.Drawing.Point(274, 84);
            this.btnRefImg.Name = "btnRefImg";
            this.btnRefImg.Size = new System.Drawing.Size(104, 26);
            this.btnRefImg.TabIndex = 6;
            this.btnRefImg.Text = "刷新验证码";
            this.btnRefImg.UseVisualStyleBackColor = true;
            this.btnRefImg.Click += new System.EventHandler(this.btnRefImg_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(915, 572);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbcJob_Query_QC);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(907, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "汽车之家";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbcJob_Query_QC
            // 
            this.tbcJob_Query_QC.Controls.Add(this.tabPage3);
            this.tbcJob_Query_QC.Controls.Add(this.tabPage4);
            this.tbcJob_Query_QC.Controls.Add(this.tabPage5);
            this.tbcJob_Query_QC.Location = new System.Drawing.Point(45, 34);
            this.tbcJob_Query_QC.Name = "tbcJob_Query_QC";
            this.tbcJob_Query_QC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbcJob_Query_QC.SelectedIndex = 0;
            this.tbcJob_Query_QC.Size = new System.Drawing.Size(814, 498);
            this.tbcJob_Query_QC.TabIndex = 22;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ddlProvince);
            this.tabPage3.Controls.Add(this.btnStop);
            this.tabPage3.Controls.Add(this.ddlCity);
            this.tabPage3.Controls.Add(this.dgvOrder);
            this.tabPage3.Controls.Add(this.btnSendOrder);
            this.tabPage3.Controls.Add(this.chkAll);
            this.tabPage3.Controls.Add(this.ddlSeries);
            this.tabPage3.Controls.Add(this.ddlOrderType);
            this.tabPage3.Controls.Add(this.lbxSendOrder);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(806, 472);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "抢单";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ddlProvince
            // 
            this.ddlProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProvince.FormattingEnabled = true;
            this.ddlProvince.Location = new System.Drawing.Point(10, 15);
            this.ddlProvince.Name = "ddlProvince";
            this.ddlProvince.Size = new System.Drawing.Size(110, 20);
            this.ddlProvince.TabIndex = 11;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(190, 159);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(46, 76);
            this.btnStop.TabIndex = 19;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ddlCity
            // 
            this.ddlCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCity.FormattingEnabled = true;
            this.ddlCity.Location = new System.Drawing.Point(126, 15);
            this.ddlCity.Name = "ddlCity";
            this.ddlCity.Size = new System.Drawing.Size(110, 20);
            this.ddlCity.TabIndex = 12;
            // 
            // dgvOrder
            // 
            this.dgvOrder.AllowUserToAddRows = false;
            this.dgvOrder.AllowUserToDeleteRows = false;
            this.dgvOrder.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colSaleName,
            this.colSaleID});
            this.dgvOrder.Location = new System.Drawing.Point(10, 67);
            this.dgvOrder.Name = "dgvOrder";
            this.dgvOrder.ReadOnly = true;
            this.dgvOrder.RowHeadersWidth = 21;
            this.dgvOrder.RowTemplate.Height = 23;
            this.dgvOrder.Size = new System.Drawing.Size(174, 168);
            this.dgvOrder.TabIndex = 15;
            // 
            // colSelected
            // 
            this.colSelected.DataPropertyName = "IsSelected";
            this.colSelected.FalseValue = "";
            this.colSelected.HeaderText = "";
            this.colSelected.Name = "colSelected";
            this.colSelected.ReadOnly = true;
            this.colSelected.TrueValue = "";
            this.colSelected.Width = 40;
            // 
            // colSaleName
            // 
            this.colSaleName.DataPropertyName = "saleName";
            this.colSaleName.HeaderText = "姓名";
            this.colSaleName.Name = "colSaleName";
            this.colSaleName.ReadOnly = true;
            this.colSaleName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSaleName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSaleID
            // 
            this.colSaleID.DataPropertyName = "saleID";
            this.colSaleID.HeaderText = "SaleID";
            this.colSaleID.Name = "colSaleID";
            this.colSaleID.ReadOnly = true;
            this.colSaleID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSaleID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSaleID.Visible = false;
            // 
            // btnSendOrder
            // 
            this.btnSendOrder.Location = new System.Drawing.Point(190, 67);
            this.btnSendOrder.Name = "btnSendOrder";
            this.btnSendOrder.Size = new System.Drawing.Size(46, 76);
            this.btnSendOrder.TabIndex = 18;
            this.btnSendOrder.Text = "开始";
            this.btnSendOrder.UseVisualStyleBackColor = true;
            this.btnSendOrder.Click += new System.EventHandler(this.btnSendOrder_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(45, 71);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 16;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // ddlSeries
            // 
            this.ddlSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSeries.FormattingEnabled = true;
            this.ddlSeries.Location = new System.Drawing.Point(10, 41);
            this.ddlSeries.Name = "ddlSeries";
            this.ddlSeries.Size = new System.Drawing.Size(110, 20);
            this.ddlSeries.TabIndex = 13;
            // 
            // ddlOrderType
            // 
            this.ddlOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrderType.FormattingEnabled = true;
            this.ddlOrderType.Location = new System.Drawing.Point(126, 41);
            this.ddlOrderType.Name = "ddlOrderType";
            this.ddlOrderType.Size = new System.Drawing.Size(110, 20);
            this.ddlOrderType.TabIndex = 14;
            // 
            // lbxSendOrder
            // 
            this.lbxSendOrder.FormattingEnabled = true;
            this.lbxSendOrder.ItemHeight = 12;
            this.lbxSendOrder.Location = new System.Drawing.Point(242, 15);
            this.lbxSendOrder.Name = "lbxSendOrder";
            this.lbxSendOrder.Size = new System.Drawing.Size(543, 220);
            this.lbxSendOrder.TabIndex = 17;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.jct_QC_Query);
            this.tabPage4.Controls.Add(this.lbxQuer);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(806, 472);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "报价";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // jct_QC_Query
            // 
            this.jct_QC_Query.Location = new System.Drawing.Point(2, 11);
            this.jct_QC_Query.Name = "jct_QC_Query";
            this.jct_QC_Query.Size = new System.Drawing.Size(399, 215);
            this.jct_QC_Query.TabIndex = 21;
            // 
            // lbxQuer
            // 
            this.lbxQuer.FormattingEnabled = true;
            this.lbxQuer.ItemHeight = 12;
            this.lbxQuer.Location = new System.Drawing.Point(405, 21);
            this.lbxQuer.Name = "lbxQuer";
            this.lbxQuer.Size = new System.Drawing.Size(395, 244);
            this.lbxQuer.TabIndex = 20;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.jct_QC_News);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(806, 472);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "新闻";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // jct_QC_News
            // 
            this.jct_QC_News.Location = new System.Drawing.Point(18, 14);
            this.jct_QC_News.Name = "jct_QC_News";
            this.jct_QC_News.Size = new System.Drawing.Size(399, 215);
            this.jct_QC_News.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 657);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.jct_YC_News);
            this.tabPage2.Controls.Add(this.jct_YC_Query);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(907, 546);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "易车网";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbxQuer_YC);
            this.groupBox5.Location = new System.Drawing.Point(2, 269);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(433, 125);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "报价";
            // 
            // lbxQuer_YC
            // 
            this.lbxQuer_YC.FormattingEnabled = true;
            this.lbxQuer_YC.ItemHeight = 12;
            this.lbxQuer_YC.Location = new System.Drawing.Point(238, 10);
            this.lbxQuer_YC.Name = "lbxQuer_YC";
            this.lbxQuer_YC.Size = new System.Drawing.Size(186, 112);
            this.lbxQuer_YC.TabIndex = 20;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ddlPro_YC);
            this.groupBox4.Controls.Add(this.btnStop_YC);
            this.groupBox4.Controls.Add(this.ddlCity_YC);
            this.groupBox4.Controls.Add(this.btnStart_YC);
            this.groupBox4.Controls.Add(this.ddlType_YC);
            this.groupBox4.Controls.Add(this.lbxSendOrder_YC);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(433, 249);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "抢单";
            // 
            // ddlPro_YC
            // 
            this.ddlPro_YC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPro_YC.FormattingEnabled = true;
            this.ddlPro_YC.Location = new System.Drawing.Point(6, 20);
            this.ddlPro_YC.Name = "ddlPro_YC";
            this.ddlPro_YC.Size = new System.Drawing.Size(110, 20);
            this.ddlPro_YC.TabIndex = 11;
            // 
            // btnStop_YC
            // 
            this.btnStop_YC.Location = new System.Drawing.Point(122, 102);
            this.btnStop_YC.Name = "btnStop_YC";
            this.btnStop_YC.Size = new System.Drawing.Size(101, 41);
            this.btnStop_YC.TabIndex = 19;
            this.btnStop_YC.Text = "停止";
            this.btnStop_YC.UseVisualStyleBackColor = true;
            this.btnStop_YC.Click += new System.EventHandler(this.btnStop_YC_Click);
            // 
            // ddlCity_YC
            // 
            this.ddlCity_YC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCity_YC.FormattingEnabled = true;
            this.ddlCity_YC.Location = new System.Drawing.Point(122, 20);
            this.ddlCity_YC.Name = "ddlCity_YC";
            this.ddlCity_YC.Size = new System.Drawing.Size(110, 20);
            this.ddlCity_YC.TabIndex = 12;
            // 
            // btnStart_YC
            // 
            this.btnStart_YC.Location = new System.Drawing.Point(6, 102);
            this.btnStart_YC.Name = "btnStart_YC";
            this.btnStart_YC.Size = new System.Drawing.Size(101, 41);
            this.btnStart_YC.TabIndex = 18;
            this.btnStart_YC.Text = "开始";
            this.btnStart_YC.UseVisualStyleBackColor = true;
            this.btnStart_YC.Click += new System.EventHandler(this.btnStart_YC_Click);
            // 
            // ddlType_YC
            // 
            this.ddlType_YC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlType_YC.FormattingEnabled = true;
            this.ddlType_YC.Location = new System.Drawing.Point(6, 46);
            this.ddlType_YC.Name = "ddlType_YC";
            this.ddlType_YC.Size = new System.Drawing.Size(110, 20);
            this.ddlType_YC.TabIndex = 13;
            // 
            // lbxSendOrder_YC
            // 
            this.lbxSendOrder_YC.FormattingEnabled = true;
            this.lbxSendOrder_YC.ItemHeight = 12;
            this.lbxSendOrder_YC.Location = new System.Drawing.Point(238, 20);
            this.lbxSendOrder_YC.Name = "lbxSendOrder_YC";
            this.lbxSendOrder_YC.Size = new System.Drawing.Size(186, 220);
            this.lbxSendOrder_YC.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblCode);
            this.groupBox1.Controls.Add(this.lblUserType);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(4, 581);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(915, 141);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户信息";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("宋体", 12F);
            this.lblEnd.Location = new System.Drawing.Point(390, 79);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(80, 16);
            this.lblEnd.TabIndex = 7;
            this.lblEnd.Text = "到期时间:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 12F);
            this.lblUserName.Location = new System.Drawing.Point(104, 79);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(80, 16);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "登录账号:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(304, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "用户类型:";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCode.Location = new System.Drawing.Point(104, 35);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(80, 16);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "软件编号:";
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("宋体", 12F);
            this.lblUserType.Location = new System.Drawing.Point(390, 35);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(80, 16);
            this.lblUserType.TabIndex = 3;
            this.lblUserType.Text = "用户类型:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(304, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "到期时间:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(18, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "登录账号:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "软件编号:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(5, 577);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 257);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.txtUserName);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.chkSavePass);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.pbCode);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnRefImg);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(252, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 150);
            this.panel2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(4, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "验证码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(4, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            // 
            // tm_qc_quer
            // 
            this.tm_qc_quer.Interval = 30000;
            this.tm_qc_quer.Tick += new System.EventHandler(this.tm_qc_quer_Tick);
            // 
            // tm_yc_query
            // 
            this.tm_yc_query.Interval = 30000;
            this.tm_yc_query.Tick += new System.EventHandler(this.tm_yc_query_Tick);
            // 
            // jct_YC_Query
            // 
            this.jct_YC_Query.Location = new System.Drawing.Point(467, 36);
            this.jct_YC_Query.Name = "jct_YC_Query";
            this.jct_YC_Query.Size = new System.Drawing.Size(399, 215);
            this.jct_YC_Query.TabIndex = 23;
            // 
            // jct_YC_News
            // 
            this.jct_YC_News.Location = new System.Drawing.Point(453, 279);
            this.jct_YC_News.Name = "jct_YC_News";
            this.jct_YC_News.Size = new System.Drawing.Size(399, 215);
            this.jct_YC_News.TabIndex = 24;
            // 
            // tm_qc_news
            // 
            this.tm_qc_news.Interval = 30000;
            // 
            // tm_yc_news
            // 
            this.tm_yc_news.Interval = 30000;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 724);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormLogin";
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tbcJob_Query_QC.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.CheckBox chkSavePass;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnRefImg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ddlOrderType;
        private System.Windows.Forms.ComboBox ddlSeries;
        private System.Windows.Forms.ComboBox ddlCity;
        private System.Windows.Forms.ComboBox ddlProvince;
        private System.Windows.Forms.DataGridView dgvOrder;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSendOrder;
        private System.Windows.Forms.ListBox lbxSendOrder;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleID;
        private System.Windows.Forms.ListBox lbxQuer;
        private System.Windows.Forms.Timer tm_qc_quer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox ddlPro_YC;
        private System.Windows.Forms.Button btnStop_YC;
        private System.Windows.Forms.ComboBox ddlCity_YC;
        private System.Windows.Forms.Button btnStart_YC;
        private System.Windows.Forms.ComboBox ddlType_YC;
        private System.Windows.Forms.ListBox lbxSendOrder_YC;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lbxQuer_YC;
        private System.Windows.Forms.Timer tm_yc_query;
        private System.Windows.Forms.TabControl tbcJob_Query_QC;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private JobControl jct_QC_Query;
        private JobControl jct_QC_News;
        private JobControl jct_YC_Query;
        private JobControl jct_YC_News;
        private System.Windows.Forms.Timer tm_qc_news;
        private System.Windows.Forms.Timer tm_yc_news;
    }
}

