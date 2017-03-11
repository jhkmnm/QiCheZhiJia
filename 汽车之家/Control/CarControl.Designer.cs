namespace Aide
{
    partial class CarControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCar = new System.Windows.Forms.DataGridView();
            this.colIsCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarReferPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFavorablePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubsidies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStoreState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StoreStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colColorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPushedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewLinkColumn();
            this.YearType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label33 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.llbColor = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtFavorablePrice = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.ddlStoreState = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvCar2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreStateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCar
            // 
            this.dgvCar.AllowUserToAddRows = false;
            this.dgvCar.AllowUserToDeleteRows = false;
            this.dgvCar.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIsCheck,
            this.colTypeName,
            this.colCarReferPrice,
            this.colFavorablePrice,
            this.colSubsidies,
            this.colPromotionPrice,
            this.colStoreState,
            this.colColorName,
            this.colPushedCount,
            this.colAction,
            this.YearType});
            this.dgvCar.DataSource = this.carBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCar.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCar.Location = new System.Drawing.Point(0, 0);
            this.dgvCar.Name = "dgvCar";
            this.dgvCar.RowHeadersVisible = false;
            this.dgvCar.RowHeadersWidth = 21;
            this.dgvCar.RowTemplate.Height = 23;
            this.dgvCar.Size = new System.Drawing.Size(600, 122);
            this.dgvCar.TabIndex = 70;
            this.dgvCar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCar_CellContentClick);
            // 
            // colIsCheck
            // 
            this.colIsCheck.DataPropertyName = "IsCheck";
            this.colIsCheck.HeaderText = "";
            this.colIsCheck.Name = "colIsCheck";
            this.colIsCheck.Width = 21;
            // 
            // colTypeName
            // 
            this.colTypeName.DataPropertyName = "TypeName";
            this.colTypeName.HeaderText = "车款";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.ReadOnly = true;
            // 
            // colCarReferPrice
            // 
            this.colCarReferPrice.DataPropertyName = "CarReferPrice";
            this.colCarReferPrice.HeaderText = "指导价(万)";
            this.colCarReferPrice.Name = "colCarReferPrice";
            this.colCarReferPrice.ReadOnly = true;
            this.colCarReferPrice.Width = 90;
            // 
            // colFavorablePrice
            // 
            this.colFavorablePrice.DataPropertyName = "FavorablePrice";
            this.colFavorablePrice.HeaderText = "优惠金额(万)";
            this.colFavorablePrice.Name = "colFavorablePrice";
            // 
            // colSubsidies
            // 
            this.colSubsidies.DataPropertyName = "Subsidies";
            this.colSubsidies.HeaderText = "新能源车补贴(万)";
            this.colSubsidies.Name = "colSubsidies";
            this.colSubsidies.ReadOnly = true;
            this.colSubsidies.Width = 130;
            // 
            // colPromotionPrice
            // 
            this.colPromotionPrice.DataPropertyName = "PromotionPrice";
            this.colPromotionPrice.HeaderText = "优惠价(万)";
            this.colPromotionPrice.Name = "colPromotionPrice";
            this.colPromotionPrice.ReadOnly = true;
            this.colPromotionPrice.Width = 90;
            // 
            // colStoreState
            // 
            this.colStoreState.DataPropertyName = "StoreState";
            this.colStoreState.DataSource = this.StoreStateBindingSource;
            this.colStoreState.DisplayMember = "Text";
            this.colStoreState.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colStoreState.HeaderText = "库存状态";
            this.colStoreState.Name = "colStoreState";
            this.colStoreState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colStoreState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colStoreState.ValueMember = "Value";
            this.colStoreState.Width = 90;
            // 
            // StoreStateBindingSource
            // 
            this.StoreStateBindingSource.DataSource = typeof(Aide.TextValue);
            // 
            // colColorName
            // 
            this.colColorName.DataPropertyName = "ColorName";
            this.colColorName.HeaderText = "车身颜色";
            this.colColorName.Name = "colColorName";
            // 
            // colPushedCount
            // 
            this.colPushedCount.DataPropertyName = "PushedCount";
            this.colPushedCount.HeaderText = "已发促销";
            this.colPushedCount.Name = "colPushedCount";
            // 
            // colAction
            // 
            this.colAction.DataPropertyName = "Action";
            this.colAction.HeaderText = "操作";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            // 
            // YearType
            // 
            this.YearType.DataPropertyName = "YearType";
            this.YearType.HeaderText = "YearType";
            this.YearType.Name = "YearType";
            this.YearType.Visible = false;
            // 
            // carBindingSource
            // 
            this.carBindingSource.DataSource = typeof(Aide.Car);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(3, 12);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(35, 12);
            this.label33.TabIndex = 67;
            this.label33.Text = "年款:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(39, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 46);
            this.panel1.TabIndex = 71;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 72;
            this.button1.Text = "选择增配车";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // llbColor
            // 
            this.llbColor.AutoSize = true;
            this.llbColor.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbColor.Location = new System.Drawing.Point(509, 33);
            this.llbColor.Name = "llbColor";
            this.llbColor.Size = new System.Drawing.Size(53, 12);
            this.llbColor.TabIndex = 76;
            this.llbColor.TabStop = true;
            this.llbColor.Text = "车身颜色";
            this.llbColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbColor_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 77;
            this.label1.Text = "年款:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(369, 306);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 78;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "查看";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtFavorablePrice
            // 
            this.txtFavorablePrice.ForeColor = System.Drawing.Color.Gray;
            this.txtFavorablePrice.Location = new System.Drawing.Point(285, 29);
            this.txtFavorablePrice.Name = "txtFavorablePrice";
            this.txtFavorablePrice.Size = new System.Drawing.Size(60, 21);
            this.txtFavorablePrice.TabIndex = 79;
            this.txtFavorablePrice.Tag = "优惠金额";
            this.txtFavorablePrice.Text = "优惠金额";
            this.txtFavorablePrice.Enter += new System.EventHandler(this.textBox_Enter);
            this.txtFavorablePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFavorablePrice_KeyDown);
            this.txtFavorablePrice.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // txtDiscount
            // 
            this.txtDiscount.ForeColor = System.Drawing.Color.Gray;
            this.txtDiscount.Location = new System.Drawing.Point(351, 29);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(68, 21);
            this.txtDiscount.TabIndex = 80;
            this.txtDiscount.Tag = "优惠折扣率";
            this.txtDiscount.Text = "优惠折扣率";
            this.txtDiscount.Enter += new System.EventHandler(this.textBox_Enter);
            this.txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscount_KeyDown);
            this.txtDiscount.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // ddlStoreState
            // 
            this.ddlStoreState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStoreState.FormattingEnabled = true;
            this.ddlStoreState.Location = new System.Drawing.Point(425, 29);
            this.ddlStoreState.Name = "ddlStoreState";
            this.ddlStoreState.Size = new System.Drawing.Size(80, 20);
            this.ddlStoreState.TabIndex = 81;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 56);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvCar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCar2);
            this.splitContainer1.Size = new System.Drawing.Size(600, 245);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 82;
            // 
            // dgvCar2
            // 
            this.dgvCar2.AllowUserToAddRows = false;
            this.dgvCar2.AllowUserToDeleteRows = false;
            this.dgvCar2.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCar2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCar2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCar2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn7});
            this.dgvCar2.DataSource = this.bindingSource1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCar2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCar2.Location = new System.Drawing.Point(0, 0);
            this.dgvCar2.Name = "dgvCar2";
            this.dgvCar2.ReadOnly = true;
            this.dgvCar2.RowHeadersVisible = false;
            this.dgvCar2.RowHeadersWidth = 21;
            this.dgvCar2.RowTemplate.Height = 23;
            this.dgvCar2.Size = new System.Drawing.Size(600, 119);
            this.dgvCar2.TabIndex = 71;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TypeName";
            this.dataGridViewTextBoxColumn2.HeaderText = "车款";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "PushedCount";
            this.dataGridViewTextBoxColumn7.HeaderText = "已发促销";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Aide.Car);
            // 
            // CarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label33);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ddlStoreState);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtFavorablePrice);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.llbColor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "CarControl";
            this.Size = new System.Drawing.Size(606, 323);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StoreStateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCar;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel llbColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource carBindingSource;        
        private System.Windows.Forms.TextBox txtFavorablePrice;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.ComboBox ddlStoreState;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvCar2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource StoreStateBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarReferPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFavorablePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubsidies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionPrice;
        private System.Windows.Forms.DataGridViewComboBoxColumn colStoreState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPushedCount;
        private System.Windows.Forms.DataGridViewLinkColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn YearType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}
