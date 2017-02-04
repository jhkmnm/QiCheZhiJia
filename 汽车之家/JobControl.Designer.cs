namespace Aide
{
    partial class JobControl
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbtQuote_A_QC = new System.Windows.Forms.RadioButton();
            this.p_A_QC = new System.Windows.Forms.Panel();
            this.dtpQuer = new System.Windows.Forms.DateTimePicker();
            this.rbtQuote_B_QC = new System.Windows.Forms.RadioButton();
            this.p_B_QC = new System.Windows.Forms.Panel();
            this.dtpQuote_S_QC = new System.Windows.Forms.DateTimePicker();
            this.dtpQuote_E_QC = new System.Windows.Forms.DateTimePicker();
            this.nudQuote_QC = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.ddlQuote_QC = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpJobDate_Quote = new System.Windows.Forms.DateTimePicker();
            this.dtpQuoteTime_QC = new System.Windows.Forms.DateTimePicker();
            this.ddlPalnType = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSetting_QC = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.p_A_QC.SuspendLayout();
            this.p_B_QC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuote_QC)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.ddlPalnType);
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 181);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "计划";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbtQuote_A_QC);
            this.panel4.Controls.Add(this.p_A_QC);
            this.panel4.Controls.Add(this.rbtQuote_B_QC);
            this.panel4.Controls.Add(this.p_B_QC);
            this.panel4.Location = new System.Drawing.Point(130, 49);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(257, 126);
            this.panel4.TabIndex = 33;
            // 
            // rbtQuote_A_QC
            // 
            this.rbtQuote_A_QC.AutoSize = true;
            this.rbtQuote_A_QC.Location = new System.Drawing.Point(6, 12);
            this.rbtQuote_A_QC.Name = "rbtQuote_A_QC";
            this.rbtQuote_A_QC.Size = new System.Drawing.Size(119, 16);
            this.rbtQuote_A_QC.TabIndex = 23;
            this.rbtQuote_A_QC.TabStop = true;
            this.rbtQuote_A_QC.Text = "执行一次，时间为";
            this.rbtQuote_A_QC.UseVisualStyleBackColor = true;
            this.rbtQuote_A_QC.CheckedChanged += new System.EventHandler(this.rbtQuote_CheckedChanged);
            // 
            // p_A_QC
            // 
            this.p_A_QC.Controls.Add(this.dtpQuer);
            this.p_A_QC.Location = new System.Drawing.Point(127, 5);
            this.p_A_QC.Name = "p_A_QC";
            this.p_A_QC.Size = new System.Drawing.Size(96, 27);
            this.p_A_QC.TabIndex = 22;
            // 
            // dtpQuer
            // 
            this.dtpQuer.CustomFormat = "HH:mm:ss";
            this.dtpQuer.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQuer.Location = new System.Drawing.Point(4, 4);
            this.dtpQuer.Name = "dtpQuer";
            this.dtpQuer.ShowUpDown = true;
            this.dtpQuer.Size = new System.Drawing.Size(87, 21);
            this.dtpQuer.TabIndex = 2;
            // 
            // rbtQuote_B_QC
            // 
            this.rbtQuote_B_QC.AutoSize = true;
            this.rbtQuote_B_QC.Location = new System.Drawing.Point(6, 44);
            this.rbtQuote_B_QC.Name = "rbtQuote_B_QC";
            this.rbtQuote_B_QC.Size = new System.Drawing.Size(71, 16);
            this.rbtQuote_B_QC.TabIndex = 24;
            this.rbtQuote_B_QC.TabStop = true;
            this.rbtQuote_B_QC.Text = "执行间隔";
            this.rbtQuote_B_QC.UseVisualStyleBackColor = true;
            this.rbtQuote_B_QC.CheckedChanged += new System.EventHandler(this.rbtQuote_CheckedChanged);
            // 
            // p_B_QC
            // 
            this.p_B_QC.Controls.Add(this.dtpQuote_S_QC);
            this.p_B_QC.Controls.Add(this.dtpQuote_E_QC);
            this.p_B_QC.Controls.Add(this.nudQuote_QC);
            this.p_B_QC.Controls.Add(this.label12);
            this.p_B_QC.Controls.Add(this.ddlQuote_QC);
            this.p_B_QC.Controls.Add(this.label10);
            this.p_B_QC.Location = new System.Drawing.Point(97, 36);
            this.p_B_QC.Name = "p_B_QC";
            this.p_B_QC.Size = new System.Drawing.Size(156, 87);
            this.p_B_QC.TabIndex = 31;
            // 
            // dtpQuote_S_QC
            // 
            this.dtpQuote_S_QC.CustomFormat = "HH:mm:ss";
            this.dtpQuote_S_QC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQuote_S_QC.Location = new System.Drawing.Point(64, 34);
            this.dtpQuote_S_QC.Name = "dtpQuote_S_QC";
            this.dtpQuote_S_QC.ShowUpDown = true;
            this.dtpQuote_S_QC.Size = new System.Drawing.Size(87, 21);
            this.dtpQuote_S_QC.TabIndex = 29;
            // 
            // dtpQuote_E_QC
            // 
            this.dtpQuote_E_QC.CustomFormat = "HH:mm:ss";
            this.dtpQuote_E_QC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQuote_E_QC.Location = new System.Drawing.Point(64, 60);
            this.dtpQuote_E_QC.Name = "dtpQuote_E_QC";
            this.dtpQuote_E_QC.ShowUpDown = true;
            this.dtpQuote_E_QC.Size = new System.Drawing.Size(87, 21);
            this.dtpQuote_E_QC.TabIndex = 30;
            // 
            // nudQuote_QC
            // 
            this.nudQuote_QC.Location = new System.Drawing.Point(7, 8);
            this.nudQuote_QC.Name = "nudQuote_QC";
            this.nudQuote_QC.Size = new System.Drawing.Size(61, 21);
            this.nudQuote_QC.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "结束时间";
            // 
            // ddlQuote_QC
            // 
            this.ddlQuote_QC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlQuote_QC.FormattingEnabled = true;
            this.ddlQuote_QC.Items.AddRange(new object[] {
            "分钟",
            "小时"});
            this.ddlQuote_QC.Location = new System.Drawing.Point(74, 9);
            this.ddlQuote_QC.Name = "ddlQuote_QC";
            this.ddlQuote_QC.Size = new System.Drawing.Size(77, 20);
            this.ddlQuote_QC.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "开始时间";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpJobDate_Quote);
            this.panel3.Controls.Add(this.dtpQuoteTime_QC);
            this.panel3.Location = new System.Drawing.Point(130, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(217, 27);
            this.panel3.TabIndex = 32;
            // 
            // dtpJobDate_Quote
            // 
            this.dtpJobDate_Quote.CustomFormat = "yyyy-MM-dd";
            this.dtpJobDate_Quote.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpJobDate_Quote.Location = new System.Drawing.Point(6, 3);
            this.dtpJobDate_Quote.Name = "dtpJobDate_Quote";
            this.dtpJobDate_Quote.Size = new System.Drawing.Size(116, 21);
            this.dtpJobDate_Quote.TabIndex = 3;
            // 
            // dtpQuoteTime_QC
            // 
            this.dtpQuoteTime_QC.CustomFormat = "HH:mm:ss";
            this.dtpQuoteTime_QC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQuoteTime_QC.Location = new System.Drawing.Point(128, 3);
            this.dtpQuoteTime_QC.Name = "dtpQuoteTime_QC";
            this.dtpQuoteTime_QC.ShowUpDown = true;
            this.dtpQuoteTime_QC.Size = new System.Drawing.Size(87, 21);
            this.dtpQuoteTime_QC.TabIndex = 2;
            // 
            // ddlPalnType
            // 
            this.ddlPalnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPalnType.FormattingEnabled = true;
            this.ddlPalnType.Items.AddRange(new object[] {
            "选择计划类型",
            "执行一次",
            "重复执行"});
            this.ddlPalnType.Location = new System.Drawing.Point(6, 20);
            this.ddlPalnType.Name = "ddlPalnType";
            this.ddlPalnType.Size = new System.Drawing.Size(110, 20);
            this.ddlPalnType.TabIndex = 31;
            this.ddlPalnType.SelectedIndexChanged += new System.EventHandler(this.ddlPalnType_SelectedIndexChanged);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(30, 194);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(41, 12);
            this.lblState.TabIndex = 35;
            this.lblState.Text = "未设置";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(162, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "每天";
            // 
            // btnSetting_QC
            // 
            this.btnSetting_QC.Location = new System.Drawing.Point(314, 189);
            this.btnSetting_QC.Name = "btnSetting_QC";
            this.btnSetting_QC.Size = new System.Drawing.Size(75, 23);
            this.btnSetting_QC.TabIndex = 34;
            this.btnSetting_QC.Text = "设置";
            this.btnSetting_QC.UseVisualStyleBackColor = true;
            this.btnSetting_QC.Click += new System.EventHandler(this.btnSetting_QC_Click);
            // 
            // JobControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSetting_QC);
            this.Name = "JobControl";
            this.Size = new System.Drawing.Size(399, 215);
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.p_A_QC.ResumeLayout(false);
            this.p_B_QC.ResumeLayout(false);
            this.p_B_QC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuote_QC)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbtQuote_A_QC;
        private System.Windows.Forms.Panel p_A_QC;
        private System.Windows.Forms.DateTimePicker dtpQuer;
        private System.Windows.Forms.RadioButton rbtQuote_B_QC;
        private System.Windows.Forms.Panel p_B_QC;
        private System.Windows.Forms.DateTimePicker dtpQuote_S_QC;
        private System.Windows.Forms.DateTimePicker dtpQuote_E_QC;
        private System.Windows.Forms.NumericUpDown nudQuote_QC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox ddlQuote_QC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpJobDate_Quote;
        private System.Windows.Forms.DateTimePicker dtpQuoteTime_QC;
        private System.Windows.Forms.ComboBox ddlPalnType;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSetting_QC;
    }
}
