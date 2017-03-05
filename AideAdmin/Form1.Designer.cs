namespace AideAdmin
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLinkMan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.textValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.textValueBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lastAllotTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNews = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDic = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExprot = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtDue_c = new System.Windows.Forms.RadioButton();
            this.rbtDue_b = new System.Windows.Forms.RadioButton();
            this.rbtDue_a = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtStatus_c = new System.Windows.Forms.RadioButton();
            this.rbtStatus_b = new System.Windows.Forms.RadioButton();
            this.rbtStatus_a = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtType_c = new System.Windows.Forms.RadioButton();
            this.rbtType_b = new System.Windows.Forms.RadioButton();
            this.rbtType_a = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkNews = new System.Windows.Forms.CheckBox();
            this.chkQuery = new System.Windows.Forms.CheckBox();
            this.chkSendOrder = new System.Windows.Forms.CheckBox();
            this.txtLastNews = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLastQuote = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDelUser = new System.Windows.Forms.Button();
            this.btnUpdType = new System.Windows.Forms.Button();
            this.btnUpDuetime = new System.Windows.Forms.Button();
            this.dtpDueTime = new System.Windows.Forms.DateTimePicker();
            this.txtLastAllTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLinkman = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textValueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textValueBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colSiteName,
            this.colUserName,
            this.colCompany,
            this.colLinkMan,
            this.colDueTime,
            this.userTypeDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.lastAllotTimeDataGridViewTextBoxColumn,
            this.colQuote,
            this.colNews});
            this.dgvData.DataSource = this.userBindingSource;
            this.dgvData.Location = new System.Drawing.Point(4, 5);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 21;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(614, 669);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "Id";
            this.colID.HeaderText = "序号";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 55;
            // 
            // colSiteName
            // 
            this.colSiteName.DataPropertyName = "SiteName";
            this.colSiteName.HeaderText = "网站";
            this.colSiteName.Name = "colSiteName";
            this.colSiteName.ReadOnly = true;
            this.colSiteName.Width = 80;
            // 
            // colUserName
            // 
            this.colUserName.DataPropertyName = "UserName";
            this.colUserName.HeaderText = "用户名";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            this.colUserName.Width = 80;
            // 
            // colCompany
            // 
            this.colCompany.DataPropertyName = "Company";
            this.colCompany.HeaderText = "公司名称";
            this.colCompany.Name = "colCompany";
            this.colCompany.ReadOnly = true;
            // 
            // colLinkMan
            // 
            this.colLinkMan.DataPropertyName = "LinkInfo";
            this.colLinkMan.HeaderText = "联系人";
            this.colLinkMan.Name = "colLinkMan";
            this.colLinkMan.ReadOnly = true;
            // 
            // colDueTime
            // 
            this.colDueTime.DataPropertyName = "DueTime";
            this.colDueTime.HeaderText = "到期时间";
            this.colDueTime.Name = "colDueTime";
            this.colDueTime.ReadOnly = true;
            this.colDueTime.Width = 90;
            // 
            // userTypeDataGridViewTextBoxColumn
            // 
            this.userTypeDataGridViewTextBoxColumn.DataPropertyName = "UserType";
            this.userTypeDataGridViewTextBoxColumn.DataSource = this.textValueBindingSource;
            this.userTypeDataGridViewTextBoxColumn.DisplayMember = "Text";
            this.userTypeDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.userTypeDataGridViewTextBoxColumn.HeaderText = "类型";
            this.userTypeDataGridViewTextBoxColumn.Name = "userTypeDataGridViewTextBoxColumn";
            this.userTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.userTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.userTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.userTypeDataGridViewTextBoxColumn.ValueMember = "Value";
            this.userTypeDataGridViewTextBoxColumn.Width = 60;
            // 
            // textValueBindingSource
            // 
            this.textValueBindingSource.DataSource = typeof(AideAdmin.TextValue);
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.DataSource = this.textValueBindingSource1;
            this.statusDataGridViewTextBoxColumn.DisplayMember = "Text";
            this.statusDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.statusDataGridViewTextBoxColumn.HeaderText = "状态";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.statusDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.statusDataGridViewTextBoxColumn.ValueMember = "Value";
            this.statusDataGridViewTextBoxColumn.Width = 60;
            // 
            // textValueBindingSource1
            // 
            this.textValueBindingSource1.DataSource = typeof(AideAdmin.TextValue);
            // 
            // lastAllotTimeDataGridViewTextBoxColumn
            // 
            this.lastAllotTimeDataGridViewTextBoxColumn.DataPropertyName = "LastAllotTime";
            this.lastAllotTimeDataGridViewTextBoxColumn.HeaderText = "抢单时间";
            this.lastAllotTimeDataGridViewTextBoxColumn.Name = "lastAllotTimeDataGridViewTextBoxColumn";
            this.lastAllotTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // colQuote
            // 
            this.colQuote.DataPropertyName = "LastQuoteTime";
            this.colQuote.HeaderText = "报价时间";
            this.colQuote.Name = "colQuote";
            this.colQuote.ReadOnly = true;
            // 
            // colNews
            // 
            this.colNews.DataPropertyName = "LastNewsTime";
            this.colNews.HeaderText = "发布资讯时间";
            this.colNews.Name = "colNews";
            this.colNews.ReadOnly = true;
            // 
            // userBindingSource
            // 
            this.userBindingSource.DataSource = typeof(AideAdmin.localhost.User);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDic);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnExprot);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(624, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选";
            // 
            // btnDic
            // 
            this.btnDic.Location = new System.Drawing.Point(114, 135);
            this.btnDic.Name = "btnDic";
            this.btnDic.Size = new System.Drawing.Size(75, 29);
            this.btnDic.TabIndex = 10;
            this.btnDic.Text = "参数设置";
            this.btnDic.UseVisualStyleBackColor = true;
            this.btnDic.Click += new System.EventHandler(this.btnDic_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(10, 135);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 29);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "获取用户";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExprot
            // 
            this.btnExprot.Location = new System.Drawing.Point(217, 135);
            this.btnExprot.Name = "btnExprot";
            this.btnExprot.Size = new System.Drawing.Size(75, 29);
            this.btnExprot.TabIndex = 8;
            this.btnExprot.Text = "导出";
            this.btnExprot.UseVisualStyleBackColor = true;
            this.btnExprot.Click += new System.EventHandler(this.btnExprot_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtDue_c);
            this.groupBox5.Controls.Add(this.rbtDue_b);
            this.groupBox5.Controls.Add(this.rbtDue_a);
            this.groupBox5.Location = new System.Drawing.Point(8, 92);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(286, 40);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // rbtDue_c
            // 
            this.rbtDue_c.AutoSize = true;
            this.rbtDue_c.Location = new System.Drawing.Point(223, 17);
            this.rbtDue_c.Name = "rbtDue_c";
            this.rbtDue_c.Size = new System.Drawing.Size(59, 16);
            this.rbtDue_c.TabIndex = 5;
            this.rbtDue_c.Text = "未到期";
            this.rbtDue_c.UseVisualStyleBackColor = true;
            this.rbtDue_c.CheckedChanged += new System.EventHandler(this.rbtDue_CheckedChanged);
            // 
            // rbtDue_b
            // 
            this.rbtDue_b.AutoSize = true;
            this.rbtDue_b.Location = new System.Drawing.Point(125, 17);
            this.rbtDue_b.Name = "rbtDue_b";
            this.rbtDue_b.Size = new System.Drawing.Size(47, 16);
            this.rbtDue_b.TabIndex = 4;
            this.rbtDue_b.Text = "到期";
            this.rbtDue_b.UseVisualStyleBackColor = true;
            this.rbtDue_b.CheckedChanged += new System.EventHandler(this.rbtDue_CheckedChanged);
            // 
            // rbtDue_a
            // 
            this.rbtDue_a.AutoSize = true;
            this.rbtDue_a.Checked = true;
            this.rbtDue_a.Location = new System.Drawing.Point(14, 17);
            this.rbtDue_a.Name = "rbtDue_a";
            this.rbtDue_a.Size = new System.Drawing.Size(47, 16);
            this.rbtDue_a.TabIndex = 3;
            this.rbtDue_a.TabStop = true;
            this.rbtDue_a.Text = "不限";
            this.rbtDue_a.UseVisualStyleBackColor = true;
            this.rbtDue_a.CheckedChanged += new System.EventHandler(this.rbtDue_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtStatus_c);
            this.groupBox4.Controls.Add(this.rbtStatus_b);
            this.groupBox4.Controls.Add(this.rbtStatus_a);
            this.groupBox4.Location = new System.Drawing.Point(8, 52);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 40);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // rbtStatus_c
            // 
            this.rbtStatus_c.AutoSize = true;
            this.rbtStatus_c.Location = new System.Drawing.Point(221, 16);
            this.rbtStatus_c.Name = "rbtStatus_c";
            this.rbtStatus_c.Size = new System.Drawing.Size(47, 16);
            this.rbtStatus_c.TabIndex = 5;
            this.rbtStatus_c.Text = "运行";
            this.rbtStatus_c.UseVisualStyleBackColor = true;
            this.rbtStatus_c.CheckedChanged += new System.EventHandler(this.rbtStatus_CheckedChanged);
            // 
            // rbtStatus_b
            // 
            this.rbtStatus_b.AutoSize = true;
            this.rbtStatus_b.Location = new System.Drawing.Point(123, 16);
            this.rbtStatus_b.Name = "rbtStatus_b";
            this.rbtStatus_b.Size = new System.Drawing.Size(47, 16);
            this.rbtStatus_b.TabIndex = 4;
            this.rbtStatus_b.Text = "离线";
            this.rbtStatus_b.UseVisualStyleBackColor = true;
            this.rbtStatus_b.CheckedChanged += new System.EventHandler(this.rbtStatus_CheckedChanged);
            // 
            // rbtStatus_a
            // 
            this.rbtStatus_a.AutoSize = true;
            this.rbtStatus_a.Checked = true;
            this.rbtStatus_a.Location = new System.Drawing.Point(14, 16);
            this.rbtStatus_a.Name = "rbtStatus_a";
            this.rbtStatus_a.Size = new System.Drawing.Size(47, 16);
            this.rbtStatus_a.TabIndex = 3;
            this.rbtStatus_a.TabStop = true;
            this.rbtStatus_a.Text = "不限";
            this.rbtStatus_a.UseVisualStyleBackColor = true;
            this.rbtStatus_a.CheckedChanged += new System.EventHandler(this.rbtStatus_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtType_c);
            this.groupBox3.Controls.Add(this.rbtType_b);
            this.groupBox3.Controls.Add(this.rbtType_a);
            this.groupBox3.Location = new System.Drawing.Point(8, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // rbtType_c
            // 
            this.rbtType_c.AutoSize = true;
            this.rbtType_c.Location = new System.Drawing.Point(221, 17);
            this.rbtType_c.Name = "rbtType_c";
            this.rbtType_c.Size = new System.Drawing.Size(47, 16);
            this.rbtType_c.TabIndex = 5;
            this.rbtType_c.Text = "付费";
            this.rbtType_c.UseVisualStyleBackColor = true;
            this.rbtType_c.CheckedChanged += new System.EventHandler(this.rbtType_CheckedChanged);
            // 
            // rbtType_b
            // 
            this.rbtType_b.AutoSize = true;
            this.rbtType_b.Location = new System.Drawing.Point(123, 17);
            this.rbtType_b.Name = "rbtType_b";
            this.rbtType_b.Size = new System.Drawing.Size(47, 16);
            this.rbtType_b.TabIndex = 4;
            this.rbtType_b.Text = "试用";
            this.rbtType_b.UseVisualStyleBackColor = true;
            this.rbtType_b.CheckedChanged += new System.EventHandler(this.rbtType_CheckedChanged);
            // 
            // rbtType_a
            // 
            this.rbtType_a.AutoSize = true;
            this.rbtType_a.Checked = true;
            this.rbtType_a.Location = new System.Drawing.Point(14, 17);
            this.rbtType_a.Name = "rbtType_a";
            this.rbtType_a.Size = new System.Drawing.Size(47, 16);
            this.rbtType_a.TabIndex = 3;
            this.rbtType_a.TabStop = true;
            this.rbtType_a.Text = "不限";
            this.rbtType_a.UseVisualStyleBackColor = true;
            this.rbtType_a.CheckedChanged += new System.EventHandler(this.rbtType_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkNews);
            this.groupBox2.Controls.Add(this.chkQuery);
            this.groupBox2.Controls.Add(this.chkSendOrder);
            this.groupBox2.Controls.Add(this.txtLastNews);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtLastQuote);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtSiteName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtID);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnDelUser);
            this.groupBox2.Controls.Add(this.btnUpdType);
            this.groupBox2.Controls.Add(this.btnUpDuetime);
            this.groupBox2.Controls.Add(this.dtpDueTime);
            this.groupBox2.Controls.Add(this.txtLastAllTime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtStatus);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtLinkman);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCompany);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(624, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 501);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用户信息";
            // 
            // chkNews
            // 
            this.chkNews.AutoSize = true;
            this.chkNews.Location = new System.Drawing.Point(128, 367);
            this.chkNews.Name = "chkNews";
            this.chkNews.Size = new System.Drawing.Size(48, 16);
            this.chkNews.TabIndex = 33;
            this.chkNews.Text = "资讯";
            this.chkNews.UseVisualStyleBackColor = true;
            // 
            // chkQuery
            // 
            this.chkQuery.AutoSize = true;
            this.chkQuery.Location = new System.Drawing.Point(72, 367);
            this.chkQuery.Name = "chkQuery";
            this.chkQuery.Size = new System.Drawing.Size(48, 16);
            this.chkQuery.TabIndex = 32;
            this.chkQuery.Text = "报价";
            this.chkQuery.UseVisualStyleBackColor = true;
            // 
            // chkSendOrder
            // 
            this.chkSendOrder.AutoSize = true;
            this.chkSendOrder.Location = new System.Drawing.Point(16, 367);
            this.chkSendOrder.Name = "chkSendOrder";
            this.chkSendOrder.Size = new System.Drawing.Size(48, 16);
            this.chkSendOrder.TabIndex = 31;
            this.chkSendOrder.Text = "抢单";
            this.chkSendOrder.UseVisualStyleBackColor = true;
            // 
            // txtLastNews
            // 
            this.txtLastNews.Location = new System.Drawing.Point(95, 443);
            this.txtLastNews.Name = "txtLastNews";
            this.txtLastNews.ReadOnly = true;
            this.txtLastNews.Size = new System.Drawing.Size(197, 21);
            this.txtLastNews.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 447);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 29;
            this.label12.Text = "最近资讯时间";
            // 
            // txtLastQuote
            // 
            this.txtLastQuote.Location = new System.Drawing.Point(95, 418);
            this.txtLastQuote.Name = "txtLastQuote";
            this.txtLastQuote.ReadOnly = true;
            this.txtLastQuote.Size = new System.Drawing.Size(197, 21);
            this.txtLastQuote.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 422);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "最近报价时间";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(193, 18);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.ReadOnly = true;
            this.txtSiteName.Size = new System.Drawing.Size(99, 21);
            this.txtSiteName.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(158, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "网站";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(69, 18);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(62, 21);
            this.txtID.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "序号";
            // 
            // btnDelUser
            // 
            this.btnDelUser.Location = new System.Drawing.Point(95, 469);
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Size = new System.Drawing.Size(197, 29);
            this.btnDelUser.TabIndex = 22;
            this.btnDelUser.Text = "删除用户";
            this.btnDelUser.UseVisualStyleBackColor = true;
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // btnUpdType
            // 
            this.btnUpdType.Location = new System.Drawing.Point(193, 360);
            this.btnUpdType.Name = "btnUpdType";
            this.btnUpdType.Size = new System.Drawing.Size(99, 29);
            this.btnUpdType.TabIndex = 21;
            this.btnUpdType.Text = "设置为付费用户";
            this.btnUpdType.UseVisualStyleBackColor = true;
            this.btnUpdType.Click += new System.EventHandler(this.btnUpdType_Click);
            // 
            // btnUpDuetime
            // 
            this.btnUpDuetime.Location = new System.Drawing.Point(69, 303);
            this.btnUpDuetime.Name = "btnUpDuetime";
            this.btnUpDuetime.Size = new System.Drawing.Size(225, 29);
            this.btnUpDuetime.TabIndex = 11;
            this.btnUpDuetime.Text = "修改到期时间";
            this.btnUpDuetime.UseVisualStyleBackColor = true;
            this.btnUpDuetime.Click += new System.EventHandler(this.btnUpDuetime_Click);
            // 
            // dtpDueTime
            // 
            this.dtpDueTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpDueTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueTime.Location = new System.Drawing.Point(69, 279);
            this.dtpDueTime.Name = "dtpDueTime";
            this.dtpDueTime.ShowUpDown = true;
            this.dtpDueTime.Size = new System.Drawing.Size(225, 21);
            this.dtpDueTime.TabIndex = 20;
            // 
            // txtLastAllTime
            // 
            this.txtLastAllTime.Location = new System.Drawing.Point(95, 393);
            this.txtLastAllTime.Name = "txtLastAllTime";
            this.txtLastAllTime.ReadOnly = true;
            this.txtLastAllTime.Size = new System.Drawing.Size(197, 21);
            this.txtLastAllTime.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 396);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "最近分配时间";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(220, 335);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(74, 21);
            this.txtStatus.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "软件状态";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(69, 335);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(81, 21);
            this.txtType.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 338);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "用户类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "到期时间";
            // 
            // txtLinkman
            // 
            this.txtLinkman.Location = new System.Drawing.Point(10, 136);
            this.txtLinkman.Multiline = true;
            this.txtLinkman.Name = "txtLinkman";
            this.txtLinkman.ReadOnly = true;
            this.txtLinkman.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLinkman.Size = new System.Drawing.Size(284, 139);
            this.txtLinkman.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "联系人";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(69, 93);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(225, 21);
            this.txtCompany.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "公司名";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(69, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(225, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(69, 43);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(225, 21);
            this.txtUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 678);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvData);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textValueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textValueBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDic;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExprot;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtDue_c;
        private System.Windows.Forms.RadioButton rbtDue_b;
        private System.Windows.Forms.RadioButton rbtDue_a;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtStatus_c;
        private System.Windows.Forms.RadioButton rbtStatus_b;
        private System.Windows.Forms.RadioButton rbtStatus_a;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtType_c;
        private System.Windows.Forms.RadioButton rbtType_b;
        private System.Windows.Forms.RadioButton rbtType_a;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdType;
        private System.Windows.Forms.Button btnUpDuetime;
        private System.Windows.Forms.DateTimePicker dtpDueTime;
        private System.Windows.Forms.TextBox txtLastAllTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLinkman;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDelUser;
        private System.Windows.Forms.BindingSource userBindingSource;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.BindingSource textValueBindingSource;
        private System.Windows.Forms.BindingSource textValueBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLinkMan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn userTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastAllotTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNews;
        private System.Windows.Forms.TextBox txtLastNews;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLastQuote;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkNews;
        private System.Windows.Forms.CheckBox chkQuery;
        private System.Windows.Forms.CheckBox chkSendOrder;
    }
}

