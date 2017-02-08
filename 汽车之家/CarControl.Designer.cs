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
            this.dgvCar = new System.Windows.Forms.DataGridView();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label33 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.llbsetMoney = new System.Windows.Forms.LinkLabel();
            this.llbsetRate = new System.Windows.Forms.LinkLabel();
            this.llbStoryState = new System.Windows.Forms.LinkLabel();
            this.llbColor = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarReferPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubsidies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStoreState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.selectColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCar
            // 
            this.dgvCar.AllowUserToAddRows = false;
            this.dgvCar.AllowUserToDeleteRows = false;
            this.dgvCar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.iDDataGridViewTextBoxColumn,
            this.yearTypeDataGridViewTextBoxColumn,
            this.colTypeName,
            this.colCarReferPrice,
            this.colMoney,
            this.colSubsidies,
            this.colPromotionPrice,
            this.colStoreState,
            this.selectColorDataGridViewTextBoxColumn});
            this.dgvCar.DataSource = this.carBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCar.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCar.Location = new System.Drawing.Point(2, 53);
            this.dgvCar.Name = "dgvCar";
            this.dgvCar.RowHeadersVisible = false;
            this.dgvCar.RowHeadersWidth = 21;
            this.dgvCar.RowTemplate.Height = 23;
            this.dgvCar.Size = new System.Drawing.Size(706, 271);
            this.dgvCar.TabIndex = 70;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(7, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 69;
            this.checkBox2.Tag = "1";
            this.checkBox2.Text = "设置置换";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(86, 5);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 68;
            this.checkBox3.Tag = "2";
            this.checkBox3.Text = "设置参考";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(5, 8);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(35, 12);
            this.label33.TabIndex = 67;
            this.label33.Text = "年款:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Location = new System.Drawing.Point(39, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 46);
            this.panel1.TabIndex = 71;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(319, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 72;
            this.button1.Text = "选择增配车";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // llbsetMoney
            // 
            this.llbsetMoney.AutoSize = true;
            this.llbsetMoney.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbsetMoney.Location = new System.Drawing.Point(317, 35);
            this.llbsetMoney.Name = "llbsetMoney";
            this.llbsetMoney.Size = new System.Drawing.Size(53, 12);
            this.llbsetMoney.TabIndex = 73;
            this.llbsetMoney.TabStop = true;
            this.llbsetMoney.Text = "优惠金额";
            // 
            // llbsetRate
            // 
            this.llbsetRate.AutoSize = true;
            this.llbsetRate.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbsetRate.Location = new System.Drawing.Point(374, 35);
            this.llbsetRate.Name = "llbsetRate";
            this.llbsetRate.Size = new System.Drawing.Size(65, 12);
            this.llbsetRate.TabIndex = 74;
            this.llbsetRate.TabStop = true;
            this.llbsetRate.Text = "优惠折扣率";
            // 
            // llbStoryState
            // 
            this.llbStoryState.AutoSize = true;
            this.llbStoryState.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbStoryState.Location = new System.Drawing.Point(443, 35);
            this.llbStoryState.Name = "llbStoryState";
            this.llbStoryState.Size = new System.Drawing.Size(53, 12);
            this.llbStoryState.TabIndex = 75;
            this.llbStoryState.TabStop = true;
            this.llbStoryState.Text = "库存状态";
            // 
            // llbColor
            // 
            this.llbColor.AutoSize = true;
            this.llbColor.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbColor.Location = new System.Drawing.Point(500, 35);
            this.llbColor.Name = "llbColor";
            this.llbColor.Size = new System.Drawing.Size(53, 12);
            this.llbColor.TabIndex = 76;
            this.llbColor.TabStop = true;
            this.llbColor.Text = "车身颜色";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 328);
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
            this.linkLabel1.Location = new System.Drawing.Point(289, 328);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 78;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "查看";
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // yearTypeDataGridViewTextBoxColumn
            // 
            this.yearTypeDataGridViewTextBoxColumn.DataPropertyName = "YearType";
            this.yearTypeDataGridViewTextBoxColumn.HeaderText = "YearType";
            this.yearTypeDataGridViewTextBoxColumn.Name = "yearTypeDataGridViewTextBoxColumn";
            this.yearTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // colTypeName
            // 
            this.colTypeName.DataPropertyName = "TypeName";
            this.colTypeName.HeaderText = "车款";
            this.colTypeName.Name = "colTypeName";
            // 
            // colCarReferPrice
            // 
            this.colCarReferPrice.DataPropertyName = "CarReferPrice";
            this.colCarReferPrice.HeaderText = "指导价(万)";
            this.colCarReferPrice.Name = "colCarReferPrice";
            this.colCarReferPrice.Width = 90;
            // 
            // colMoney
            // 
            this.colMoney.DataPropertyName = "Money";
            this.colMoney.HeaderText = "优惠金额(万)";
            this.colMoney.Name = "colMoney";
            this.colMoney.Width = 110;
            // 
            // colSubsidies
            // 
            this.colSubsidies.DataPropertyName = "Subsidies";
            this.colSubsidies.HeaderText = "新能源车补贴(万)";
            this.colSubsidies.Name = "colSubsidies";
            this.colSubsidies.Width = 130;
            // 
            // colPromotionPrice
            // 
            this.colPromotionPrice.DataPropertyName = "PromotionPrice";
            this.colPromotionPrice.HeaderText = "优惠价(万)";
            this.colPromotionPrice.Name = "colPromotionPrice";
            this.colPromotionPrice.Width = 90;
            // 
            // colStoreState
            // 
            this.colStoreState.DataPropertyName = "StoreState";
            this.colStoreState.HeaderText = "库存状态";
            this.colStoreState.Name = "colStoreState";
            this.colStoreState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colStoreState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colStoreState.Width = 90;
            // 
            // selectColorDataGridViewTextBoxColumn
            // 
            this.selectColorDataGridViewTextBoxColumn.DataPropertyName = "SelectColor";
            this.selectColorDataGridViewTextBoxColumn.HeaderText = "车身颜色";
            this.selectColorDataGridViewTextBoxColumn.Name = "selectColorDataGridViewTextBoxColumn";
            this.selectColorDataGridViewTextBoxColumn.Width = 90;
            // 
            // carBindingSource
            // 
            this.carBindingSource.DataSource = typeof(Aide.Car);
            // 
            // CarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.llbColor);
            this.Controls.Add(this.llbStoryState);
            this.Controls.Add(this.llbsetRate);
            this.Controls.Add(this.llbsetMoney);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvCar);
            this.Controls.Add(this.label33);
            this.Name = "CarControl";
            this.Size = new System.Drawing.Size(710, 345);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCar;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel llbsetMoney;
        private System.Windows.Forms.LinkLabel llbsetRate;
        private System.Windows.Forms.LinkLabel llbStoryState;
        private System.Windows.Forms.LinkLabel llbColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource carBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarReferPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubsidies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionPrice;
        private System.Windows.Forms.DataGridViewComboBoxColumn colStoreState;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectColorDataGridViewTextBoxColumn;
    }
}
