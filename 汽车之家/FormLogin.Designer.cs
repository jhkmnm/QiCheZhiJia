namespace Aide
{
    partial class FormLogin
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
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSavePass = new System.Windows.Forms.CheckBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.pbCode = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRefImg = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtQC = new System.Windows.Forms.RadioButton();
            this.rbtYC = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("宋体", 12F);
            this.txtUserName.Location = new System.Drawing.Point(90, 104);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(211, 26);
            this.txtUserName.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(118, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 9;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(20, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "验证码:";
            // 
            // chkSavePass
            // 
            this.chkSavePass.AutoSize = true;
            this.chkSavePass.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSavePass.Location = new System.Drawing.Point(211, 214);
            this.chkSavePass.Name = "chkSavePass";
            this.chkSavePass.Size = new System.Drawing.Size(155, 20);
            this.chkSavePass.TabIndex = 12;
            this.chkSavePass.Text = "记住用户名、密码";
            this.chkSavePass.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 12F);
            this.txtCode.Location = new System.Drawing.Point(90, 176);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 26);
            this.txtCode.TabIndex = 11;
            // 
            // pbCode
            // 
            this.pbCode.Location = new System.Drawing.Point(196, 176);
            this.pbCode.Name = "pbCode";
            this.pbCode.Size = new System.Drawing.Size(88, 25);
            this.pbCode.TabIndex = 7;
            this.pbCode.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(20, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "密  码:";
            // 
            // btnRefImg
            // 
            this.btnRefImg.Font = new System.Drawing.Font("宋体", 12F);
            this.btnRefImg.Location = new System.Drawing.Point(290, 176);
            this.btnRefImg.Name = "btnRefImg";
            this.btnRefImg.Size = new System.Drawing.Size(104, 26);
            this.btnRefImg.TabIndex = 16;
            this.btnRefImg.Text = "刷新验证码";
            this.btnRefImg.UseVisualStyleBackColor = true;
            this.btnRefImg.Click += new System.EventHandler(this.btnRefImg_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPassword.Location = new System.Drawing.Point(90, 140);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(211, 26);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(20, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "用户名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(4, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "选择站点:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtYC);
            this.panel1.Controls.Add(this.rbtQC);
            this.panel1.Location = new System.Drawing.Point(90, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 38);
            this.panel1.TabIndex = 18;
            // 
            // rbtQC
            // 
            this.rbtQC.AutoSize = true;
            this.rbtQC.Checked = true;
            this.rbtQC.Location = new System.Drawing.Point(8, 10);
            this.rbtQC.Name = "rbtQC";
            this.rbtQC.Size = new System.Drawing.Size(71, 16);
            this.rbtQC.TabIndex = 0;
            this.rbtQC.TabStop = true;
            this.rbtQC.Text = "汽车之家";
            this.rbtQC.UseVisualStyleBackColor = true;
            this.rbtQC.CheckedChanged += new System.EventHandler(this.rbtQC_CheckedChanged);
            // 
            // rbtYC
            // 
            this.rbtYC.AutoSize = true;
            this.rbtYC.Location = new System.Drawing.Point(121, 10);
            this.rbtYC.Name = "rbtYC";
            this.rbtYC.Size = new System.Drawing.Size(59, 16);
            this.rbtYC.TabIndex = 1;
            this.rbtYC.Text = "易车网";
            this.rbtYC.UseVisualStyleBackColor = true;
            this.rbtYC.CheckedChanged += new System.EventHandler(this.rbtQC_CheckedChanged);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 275);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkSavePass);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.pbCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefImg);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Name = "FormLogin";
            this.Text = "登录";
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSavePass;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.PictureBox pbCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefImg;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtYC;
        private System.Windows.Forms.RadioButton rbtQC;
    }
}