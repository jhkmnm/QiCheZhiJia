namespace Aide
{
    partial class FormOrderCondition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.areaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.isCheckDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sPecNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.specBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chkAllCity = new System.Windows.Forms.CheckBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.isCheckDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnYes = new System.Windows.Forms.Button();
            this.chkTypeAll = new System.Windows.Forms.CheckBox();
            this.chkSpceAll = new System.Windows.Forms.CheckBox();
            this.isCheckedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.specBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCheckedDataGridViewCheckBoxColumn,
            this.colPro,
            this.cityDataGridViewTextBoxColumn,
            this.cityIdDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.areaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(8, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 21;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(223, 423);
            this.dataGridView1.TabIndex = 0;
            // 
            // areaBindingSource
            // 
            this.areaBindingSource.DataSource = typeof(Model.Area);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCheckDataGridViewCheckBoxColumn,
            this.sPecNameDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.specBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(237, 173);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 21;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(193, 261);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.Visible = false;
            // 
            // isCheckDataGridViewCheckBoxColumn
            // 
            this.isCheckDataGridViewCheckBoxColumn.DataPropertyName = "IsCheck";
            this.isCheckDataGridViewCheckBoxColumn.HeaderText = "选择";
            this.isCheckDataGridViewCheckBoxColumn.Name = "isCheckDataGridViewCheckBoxColumn";
            this.isCheckDataGridViewCheckBoxColumn.Width = 40;
            // 
            // sPecNameDataGridViewTextBoxColumn
            // 
            this.sPecNameDataGridViewTextBoxColumn.DataPropertyName = "SPecName";
            this.sPecNameDataGridViewTextBoxColumn.HeaderText = "车系";
            this.sPecNameDataGridViewTextBoxColumn.Name = "sPecNameDataGridViewTextBoxColumn";
            this.sPecNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // specBindingSource
            // 
            this.specBindingSource.DataSource = typeof(Model.Spec);
            // 
            // chkAllCity
            // 
            this.chkAllCity.AutoSize = true;
            this.chkAllCity.Location = new System.Drawing.Point(13, 16);
            this.chkAllCity.Name = "chkAllCity";
            this.chkAllCity.Size = new System.Drawing.Size(15, 14);
            this.chkAllCity.TabIndex = 2;
            this.chkAllCity.UseVisualStyleBackColor = true;
            this.chkAllCity.CheckedChanged += new System.EventHandler(this.chkAllCity_CheckedChanged);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCheckDataGridViewCheckBoxColumn1,
            this.typeNameDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn1});
            this.dataGridView3.DataSource = this.orderTypeBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(237, 12);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 21;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(193, 150);
            this.dataGridView3.TabIndex = 3;
            // 
            // isCheckDataGridViewCheckBoxColumn1
            // 
            this.isCheckDataGridViewCheckBoxColumn1.DataPropertyName = "IsCheck";
            this.isCheckDataGridViewCheckBoxColumn1.HeaderText = "选择";
            this.isCheckDataGridViewCheckBoxColumn1.Name = "isCheckDataGridViewCheckBoxColumn1";
            this.isCheckDataGridViewCheckBoxColumn1.Width = 40;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "类型";
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDDataGridViewTextBoxColumn1
            // 
            this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn1.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
            this.iDDataGridViewTextBoxColumn1.Visible = false;
            // 
            // orderTypeBindingSource
            // 
            this.orderTypeBindingSource.DataSource = typeof(Model.OrderType);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(436, 16);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // chkTypeAll
            // 
            this.chkTypeAll.AutoSize = true;
            this.chkTypeAll.Location = new System.Drawing.Point(241, 16);
            this.chkTypeAll.Name = "chkTypeAll";
            this.chkTypeAll.Size = new System.Drawing.Size(15, 14);
            this.chkTypeAll.TabIndex = 5;
            this.chkTypeAll.UseVisualStyleBackColor = true;
            this.chkTypeAll.CheckedChanged += new System.EventHandler(this.chkTypeAll_CheckedChanged);
            // 
            // chkSpceAll
            // 
            this.chkSpceAll.AutoSize = true;
            this.chkSpceAll.Location = new System.Drawing.Point(241, 177);
            this.chkSpceAll.Name = "chkSpceAll";
            this.chkSpceAll.Size = new System.Drawing.Size(15, 14);
            this.chkSpceAll.TabIndex = 6;
            this.chkSpceAll.UseVisualStyleBackColor = true;
            this.chkSpceAll.CheckedChanged += new System.EventHandler(this.chkSpceAll_CheckedChanged);
            // 
            // isCheckedDataGridViewCheckBoxColumn
            // 
            this.isCheckedDataGridViewCheckBoxColumn.DataPropertyName = "IsChecked";
            this.isCheckedDataGridViewCheckBoxColumn.HeaderText = "选择";
            this.isCheckedDataGridViewCheckBoxColumn.Name = "isCheckedDataGridViewCheckBoxColumn";
            this.isCheckedDataGridViewCheckBoxColumn.Width = 40;
            // 
            // colPro
            // 
            this.colPro.DataPropertyName = "Pro";
            this.colPro.HeaderText = "省";
            this.colPro.Name = "colPro";
            this.colPro.Width = 75;
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.cityDataGridViewTextBoxColumn.HeaderText = "城市";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            this.cityDataGridViewTextBoxColumn.ReadOnly = true;
            this.cityDataGridViewTextBoxColumn.Width = 80;
            // 
            // cityIdDataGridViewTextBoxColumn
            // 
            this.cityIdDataGridViewTextBoxColumn.DataPropertyName = "CityId";
            this.cityIdDataGridViewTextBoxColumn.HeaderText = "cityid";
            this.cityIdDataGridViewTextBoxColumn.Name = "cityIdDataGridViewTextBoxColumn";
            this.cityIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.cityIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // FormOrderCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 446);
            this.Controls.Add(this.chkSpceAll);
            this.Controls.Add(this.chkTypeAll);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.chkAllCity);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOrderCondition";
            this.Text = "抢单条件选择";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.specBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource areaBindingSource;
        private System.Windows.Forms.CheckBox chkAllCity;
        private System.Windows.Forms.BindingSource specBindingSource;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.BindingSource orderTypeBindingSource;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.CheckBox chkTypeAll;
        private System.Windows.Forms.CheckBox chkSpceAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sPecNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPro;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityIdDataGridViewTextBoxColumn;
    }
}