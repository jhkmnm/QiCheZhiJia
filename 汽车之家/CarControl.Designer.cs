﻿namespace Aide
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCar = new System.Windows.Forms.DataGridView();
            this.label33 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.llbStoryState = new System.Windows.Forms.LinkLabel();
            this.llbColor = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtFavorablePrice = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.ddlStoreState = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvCar2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFavorablePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPushedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarReferPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubsidies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStoreState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCar
            // 
            this.dgvCar.AllowUserToAddRows = false;
            this.dgvCar.AllowUserToDeleteRows = false;
            this.dgvCar.AutoGenerateColumns = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yearTypeDataGridViewTextBoxColumn,
            this.colIsCheck,
            this.colTypeName,
            this.colCarReferPrice,
            this.colFavorablePrice,
            this.colSubsidies,
            this.colPromotionPrice,
            this.colStoreState,
            this.colColorName,
            this.colPushedCount});
            this.dgvCar.DataSource = this.carBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCar.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCar.Location = new System.Drawing.Point(0, 0);
            this.dgvCar.Name = "dgvCar";
            this.dgvCar.RowHeadersVisible = false;
            this.dgvCar.RowHeadersWidth = 21;
            this.dgvCar.RowTemplate.Height = 23;
            this.dgvCar.Size = new System.Drawing.Size(600, 122);
            this.dgvCar.TabIndex = 70;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(3, 8);
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
            // 
            // llbStoryState
            // 
            this.llbStoryState.AutoSize = true;
            this.llbStoryState.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbStoryState.Location = new System.Drawing.Point(452, 7);
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
            this.llbColor.Location = new System.Drawing.Point(509, 7);
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
            this.linkLabel1.Location = new System.Drawing.Point(289, 306);
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
            this.txtDiscount.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // ddlStoreState
            // 
            this.ddlStoreState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStoreState.FormattingEnabled = true;
            this.ddlStoreState.Location = new System.Drawing.Point(425, 30);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCar2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCar2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCar2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn7});
            this.dgvCar2.DataSource = this.bindingSource1;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCar2.DefaultCellStyle = dataGridViewCellStyle8;
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
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "PushedCount";
            this.dataGridViewTextBoxColumn7.HeaderText = "已发促销";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // colIsCheck
            // 
            this.colIsCheck.DataPropertyName = "IsCheck";
            this.colIsCheck.HeaderText = "";
            this.colIsCheck.Name = "colIsCheck";
            this.colIsCheck.Width = 21;
            // 
            // colFavorablePrice
            // 
            this.colFavorablePrice.DataPropertyName = "FavorablePrice";
            this.colFavorablePrice.HeaderText = "优惠金额(万)";
            this.colFavorablePrice.Name = "colFavorablePrice";
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
            this.colPromotionPrice.ReadOnly = true;
            this.colPromotionPrice.Width = 90;
            // 
            // colStoreState
            // 
            this.colStoreState.DataPropertyName = "StoreState";
            this.colStoreState.HeaderText = "库存状态";
            this.colStoreState.Name = "colStoreState";
            this.colStoreState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colStoreState.Width = 90;
            // 
            // carBindingSource
            // 
            this.carBindingSource.DataSource = typeof(Aide.Car);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "YearType";
            this.dataGridViewTextBoxColumn1.HeaderText = "YearType";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TypeName";
            this.dataGridViewTextBoxColumn2.HeaderText = "车款";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
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
            this.Controls.Add(this.llbStoryState);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "CarControl";
            this.Size = new System.Drawing.Size(606, 323);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCar;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel llbStoryState;
        private System.Windows.Forms.LinkLabel llbColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.BindingSource carBindingSource;        
        private System.Windows.Forms.TextBox txtFavorablePrice;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.ComboBox ddlStoreState;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvCar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarReferPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFavorablePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubsidies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStoreState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPushedCount;
    }
}
