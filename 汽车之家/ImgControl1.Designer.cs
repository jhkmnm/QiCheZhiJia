namespace Aide
{
    partial class ImgControl1
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
            this.pbxImg = new System.Windows.Forms.PictureBox();
            this.lblFocusImg = new System.Windows.Forms.LinkLabel();
            this.lblDel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxImg
            // 
            this.pbxImg.Location = new System.Drawing.Point(1, 1);
            this.pbxImg.Name = "pbxImg";
            this.pbxImg.Size = new System.Drawing.Size(120, 80);
            this.pbxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImg.TabIndex = 0;
            this.pbxImg.TabStop = false;
            // 
            // lblFocusImg
            // 
            this.lblFocusImg.AutoSize = true;
            this.lblFocusImg.Location = new System.Drawing.Point(3, 84);
            this.lblFocusImg.Name = "lblFocusImg";
            this.lblFocusImg.Size = new System.Drawing.Size(53, 12);
            this.lblFocusImg.TabIndex = 1;
            this.lblFocusImg.TabStop = true;
            this.lblFocusImg.Text = "查看原图";
            this.lblFocusImg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFocusImg_LinkClicked);
            // 
            // lblDel
            // 
            this.lblDel.AutoSize = true;
            this.lblDel.Location = new System.Drawing.Point(88, 84);
            this.lblDel.Name = "lblDel";
            this.lblDel.Size = new System.Drawing.Size(29, 12);
            this.lblDel.TabIndex = 2;
            this.lblDel.TabStop = true;
            this.lblDel.Text = "删除";
            // 
            // ImgControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDel);
            this.Controls.Add(this.lblFocusImg);
            this.Controls.Add(this.pbxImg);
            this.Name = "ImgControl1";
            this.Size = new System.Drawing.Size(122, 98);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox pbxImg;
        public System.Windows.Forms.LinkLabel lblFocusImg;
        public System.Windows.Forms.LinkLabel lblDel;
    }
}
