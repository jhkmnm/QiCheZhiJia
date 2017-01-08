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
            this.pbCode = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.chkSavePass = new System.Windows.Forms.CheckBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnRefImg = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ddlOrderType = new System.Windows.Forms.ComboBox();
            this.ddlSeries = new System.Windows.Forms.ComboBox();
            this.ddlCity = new System.Windows.Forms.ComboBox();
            this.ddlProvince = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCode
            // 
            this.pbCode.Location = new System.Drawing.Point(180, 63);
            this.pbCode.Name = "pbCode";
            this.pbCode.Size = new System.Drawing.Size(88, 21);
            this.pbCode.TabIndex = 0;
            this.pbCode.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(57, 63);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(117, 21);
            this.txtCode.TabIndex = 2;
            // 
            // chkSavePass
            // 
            this.chkSavePass.AutoSize = true;
            this.chkSavePass.Location = new System.Drawing.Point(226, 94);
            this.chkSavePass.Name = "chkSavePass";
            this.chkSavePass.Size = new System.Drawing.Size(120, 16);
            this.chkSavePass.TabIndex = 3;
            this.chkSavePass.Text = "记住用户名、密码";
            this.chkSavePass.UseVisualStyleBackColor = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(57, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(211, 21);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(57, 33);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(211, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // btnRefImg
            // 
            this.btnRefImg.Location = new System.Drawing.Point(274, 63);
            this.btnRefImg.Name = "btnRefImg";
            this.btnRefImg.Size = new System.Drawing.Size(75, 23);
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
            this.tabControl1.Size = new System.Drawing.Size(915, 436);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ddlOrderType);
            this.tabPage1.Controls.Add(this.ddlSeries);
            this.tabPage1.Controls.Add(this.ddlCity);
            this.tabPage1.Controls.Add(this.ddlProvince);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(907, 410);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "汽车之家";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ddlOrderType
            // 
            this.ddlOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrderType.FormattingEnabled = true;
            this.ddlOrderType.Location = new System.Drawing.Point(540, 41);
            this.ddlOrderType.Name = "ddlOrderType";
            this.ddlOrderType.Size = new System.Drawing.Size(121, 20);
            this.ddlOrderType.TabIndex = 14;
            // 
            // ddlSeries
            // 
            this.ddlSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSeries.FormattingEnabled = true;
            this.ddlSeries.Location = new System.Drawing.Point(389, 41);
            this.ddlSeries.Name = "ddlSeries";
            this.ddlSeries.Size = new System.Drawing.Size(121, 20);
            this.ddlSeries.TabIndex = 13;
            // 
            // ddlCity
            // 
            this.ddlCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCity.FormattingEnabled = true;
            this.ddlCity.Location = new System.Drawing.Point(227, 41);
            this.ddlCity.Name = "ddlCity";
            this.ddlCity.Size = new System.Drawing.Size(121, 20);
            this.ddlCity.TabIndex = 12;
            // 
            // ddlProvince
            // 
            this.ddlProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProvince.FormattingEnabled = true;
            this.ddlProvince.Location = new System.Drawing.Point(70, 41);
            this.ddlProvince.Name = "ddlProvince";
            this.ddlProvince.Size = new System.Drawing.Size(121, 20);
            this.ddlProvince.TabIndex = 11;
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(3, 385);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(901, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(907, 410);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "易车网";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.groupBox1.Location = new System.Drawing.Point(4, 486);
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
            this.panel1.Location = new System.Drawing.Point(4, 455);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 118);
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
            this.panel2.Location = new System.Drawing.Point(280, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 119);
            this.panel2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "验证码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 629);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormLogin";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
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
    }
}

