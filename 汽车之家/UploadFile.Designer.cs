namespace Aide
{
    partial class UploadFile
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pbxImg = new System.Windows.Forms.PictureBox();
            this.lblImgPath = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ucPager = new Aide.UcPagerEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            //this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(581, 313);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.btnUpload);
            this.tabPage1.Controls.Add(this.pbxImg);
            this.tabPage1.Controls.Add(this.lblImgPath);
            this.tabPage1.Controls.Add(this.btnOpenFile);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "上传新图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(105, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "支持jpg、jpeg、gif、png、bmp格式，图片大小限2M内";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(472, 258);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(363, 258);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pbxImg
            // 
            this.pbxImg.Location = new System.Drawing.Point(50, 77);
            this.pbxImg.Name = "pbxImg";
            this.pbxImg.Size = new System.Drawing.Size(235, 183);
            this.pbxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImg.TabIndex = 3;
            this.pbxImg.TabStop = false;
            // 
            // lblImgPath
            // 
            this.lblImgPath.AutoSize = true;
            this.lblImgPath.Location = new System.Drawing.Point(210, 30);
            this.lblImgPath.Name = "lblImgPath";
            this.lblImgPath.Size = new System.Drawing.Size(89, 12);
            this.lblImgPath.TabIndex = 2;
            this.lblImgPath.Text = "未选择任何文件";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(107, 25);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "选择文件";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "上传图片:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCancel);
            this.tabPage2.Controls.Add(this.btnOK);
            this.tabPage2.Controls.Add(this.ucPager);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已传图片";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(504, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(431, 252);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(58, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucPager
            // 
            this.ucPager.Location = new System.Drawing.Point(8, 244);
            this.ucPager.Name = "ucPager";
            this.ucPager.PageIndex = 1;
            this.ucPager.PageSize = 15;
            this.ucPager.PreviousPage = 0;
            this.ucPager.RecordCount = 0;
            this.ucPager.Size = new System.Drawing.Size(417, 38);
            this.ucPager.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(6, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 229);
            this.panel1.TabIndex = 0;
            // 
            // UploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 318);
            this.Controls.Add(this.tabControl1);
            this.Name = "UploadFile";
            this.Text = "选择图片";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private UcPagerEx ucPager;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.PictureBox pbxImg;
        private System.Windows.Forms.Label lblImgPath;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}