namespace Aide
{
    partial class PhotoSelectNew
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlCarColors = new System.Windows.Forms.ComboBox();
            this.ddlCarStyle = new System.Windows.Forms.ComboBox();
            this.ddCarYear = new System.Windows.Forms.ComboBox();
            this.tbcImg = new System.Windows.Forms.TabControl();
            this.lbtn1 = new System.Windows.Forms.TabPage();
            this.lbtn2 = new System.Windows.Forms.TabPage();
            this.lbtn3 = new System.Windows.Forms.TabPage();
            this.lbtn4 = new System.Windows.Forms.TabPage();
            this.lbtn5 = new System.Windows.Forms.TabPage();
            this.lbtn6 = new System.Windows.Forms.TabPage();
            this.lbtn7 = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ucPager = new Aide.UcPagerEx();
            this.groupBox1.SuspendLayout();
            this.tbcImg.SuspendLayout();
            this.lbtn1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlCarColors);
            this.groupBox1.Controls.Add(this.ddlCarStyle);
            this.groupBox1.Controls.Add(this.ddCarYear);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择品牌图片";
            // 
            // ddlCarColors
            // 
            this.ddlCarColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCarColors.FormattingEnabled = true;
            this.ddlCarColors.Location = new System.Drawing.Point(317, 30);
            this.ddlCarColors.Name = "ddlCarColors";
            this.ddlCarColors.Size = new System.Drawing.Size(121, 20);
            this.ddlCarColors.TabIndex = 2;
            // 
            // ddlCarStyle
            // 
            this.ddlCarStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCarStyle.FormattingEnabled = true;
            this.ddlCarStyle.Location = new System.Drawing.Point(173, 30);
            this.ddlCarStyle.Name = "ddlCarStyle";
            this.ddlCarStyle.Size = new System.Drawing.Size(121, 20);
            this.ddlCarStyle.TabIndex = 1;
            // 
            // ddCarYear
            // 
            this.ddCarYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCarYear.FormattingEnabled = true;
            this.ddCarYear.Location = new System.Drawing.Point(32, 30);
            this.ddCarYear.Name = "ddCarYear";
            this.ddCarYear.Size = new System.Drawing.Size(121, 20);
            this.ddCarYear.TabIndex = 0;
            // 
            // tbcImg
            // 
            this.tbcImg.Controls.Add(this.lbtn1);
            this.tbcImg.Controls.Add(this.lbtn2);
            this.tbcImg.Controls.Add(this.lbtn3);
            this.tbcImg.Controls.Add(this.lbtn4);
            this.tbcImg.Controls.Add(this.lbtn5);
            this.tbcImg.Controls.Add(this.lbtn6);
            this.tbcImg.Controls.Add(this.lbtn7);
            this.tbcImg.Location = new System.Drawing.Point(6, 80);
            this.tbcImg.Name = "tbcImg";
            this.tbcImg.SelectedIndex = 0;
            this.tbcImg.Size = new System.Drawing.Size(645, 303);
            this.tbcImg.TabIndex = 1;
            this.tbcImg.SelectedIndexChanged += new System.EventHandler(this.tbcImg_SelectedIndexChanged);
            // 
            // lbtn1
            // 
            this.lbtn1.Controls.Add(this.label1);
            this.lbtn1.Location = new System.Drawing.Point(4, 22);
            this.lbtn1.Name = "lbtn1";
            this.lbtn1.Padding = new System.Windows.Forms.Padding(3);
            this.lbtn1.Size = new System.Drawing.Size(637, 277);
            this.lbtn1.TabIndex = 0;
            this.lbtn1.Text = "外观";
            this.lbtn1.UseVisualStyleBackColor = true;
            // 
            // lbtn2
            // 
            this.lbtn2.Location = new System.Drawing.Point(4, 22);
            this.lbtn2.Name = "lbtn2";
            this.lbtn2.Padding = new System.Windows.Forms.Padding(3);
            this.lbtn2.Size = new System.Drawing.Size(637, 277);
            this.lbtn2.TabIndex = 1;
            this.lbtn2.Text = "内饰";
            this.lbtn2.UseVisualStyleBackColor = true;
            // 
            // lbtn3
            // 
            this.lbtn3.Location = new System.Drawing.Point(4, 22);
            this.lbtn3.Name = "lbtn3";
            this.lbtn3.Size = new System.Drawing.Size(637, 277);
            this.lbtn3.TabIndex = 2;
            this.lbtn3.Text = "内部空间";
            this.lbtn3.UseVisualStyleBackColor = true;
            // 
            // lbtn4
            // 
            this.lbtn4.Location = new System.Drawing.Point(4, 22);
            this.lbtn4.Name = "lbtn4";
            this.lbtn4.Size = new System.Drawing.Size(637, 277);
            this.lbtn4.TabIndex = 3;
            this.lbtn4.Text = "行驶";
            this.lbtn4.UseVisualStyleBackColor = true;
            // 
            // lbtn5
            // 
            this.lbtn5.Location = new System.Drawing.Point(4, 22);
            this.lbtn5.Name = "lbtn5";
            this.lbtn5.Size = new System.Drawing.Size(637, 277);
            this.lbtn5.TabIndex = 4;
            this.lbtn5.Text = "创意图";
            this.lbtn5.UseVisualStyleBackColor = true;
            // 
            // lbtn6
            // 
            this.lbtn6.Location = new System.Drawing.Point(4, 22);
            this.lbtn6.Name = "lbtn6";
            this.lbtn6.Size = new System.Drawing.Size(637, 277);
            this.lbtn6.TabIndex = 5;
            this.lbtn6.Text = "图说";
            this.lbtn6.UseVisualStyleBackColor = true;
            // 
            // lbtn7
            // 
            this.lbtn7.Location = new System.Drawing.Point(4, 22);
            this.lbtn7.Name = "lbtn7";
            this.lbtn7.Size = new System.Drawing.Size(637, 277);
            this.lbtn7.TabIndex = 6;
            this.lbtn7.Text = "其他";
            this.lbtn7.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(457, 395);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(560, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // ucPager
            // 
            this.ucPager.Location = new System.Drawing.Point(10, 389);
            this.ucPager.Name = "ucPager";
            this.ucPager.PageIndex = 1;
            this.ucPager.PageSize = 15;
            this.ucPager.PreviousPage = 0;
            this.ucPager.RecordCount = 0;
            this.ucPager.Size = new System.Drawing.Size(417, 38);
            this.ucPager.TabIndex = 2;
            // 
            // PhotoSelectNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 456);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucPager);
            this.Controls.Add(this.tbcImg);
            this.Controls.Add(this.groupBox1);
            this.Name = "PhotoSelectNew";
            this.Text = "PhotoSelectNew";
            this.groupBox1.ResumeLayout(false);
            this.tbcImg.ResumeLayout(false);
            this.lbtn1.ResumeLayout(false);
            this.lbtn1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ddlCarColors;
        private System.Windows.Forms.ComboBox ddlCarStyle;
        private System.Windows.Forms.ComboBox ddCarYear;
        private System.Windows.Forms.TabControl tbcImg;
        private System.Windows.Forms.TabPage lbtn1;
        private System.Windows.Forms.TabPage lbtn2;
        private System.Windows.Forms.TabPage lbtn3;
        private System.Windows.Forms.TabPage lbtn4;
        private System.Windows.Forms.TabPage lbtn5;
        private System.Windows.Forms.TabPage lbtn6;
        private System.Windows.Forms.TabPage lbtn7;
        private UcPagerEx ucPager;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}