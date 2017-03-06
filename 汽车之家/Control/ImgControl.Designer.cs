namespace Aide
{
    partial class ImgControl
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
            this.ptbImg = new System.Windows.Forms.PictureBox();
            this.llbUp = new System.Windows.Forms.LinkLabel();
            this.llbInclude = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ptbImg)).BeginInit();
            this.SuspendLayout();
            // 
            // ptbImg
            // 
            this.ptbImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptbImg.Location = new System.Drawing.Point(0, 0);
            this.ptbImg.Name = "ptbImg";
            this.ptbImg.Size = new System.Drawing.Size(300, 200);
            this.ptbImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbImg.TabIndex = 0;
            this.ptbImg.TabStop = false;
            // 
            // llbUp
            // 
            this.llbUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llbUp.AutoSize = true;
            this.llbUp.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbUp.Location = new System.Drawing.Point(3, 206);
            this.llbUp.Name = "llbUp";
            this.llbUp.Size = new System.Drawing.Size(41, 12);
            this.llbUp.TabIndex = 1;
            this.llbUp.TabStop = true;
            this.llbUp.Text = "已上传";
            this.llbUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbUp_LinkClicked);
            // 
            // llbInclude
            // 
            this.llbInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llbInclude.AutoSize = true;
            this.llbInclude.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbInclude.Location = new System.Drawing.Point(53, 206);
            this.llbInclude.Name = "llbInclude";
            this.llbInclude.Size = new System.Drawing.Size(29, 12);
            this.llbInclude.TabIndex = 2;
            this.llbInclude.TabStop = true;
            this.llbInclude.Text = "图库";
            this.llbInclude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbInclude_LinkClicked);
            // 
            // ImgControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llbInclude);
            this.Controls.Add(this.llbUp);
            this.Controls.Add(this.ptbImg);
            this.Name = "ImgControl";
            this.Size = new System.Drawing.Size(300, 224);
            ((System.ComponentModel.ISupportInitialize)(this.ptbImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbImg;
        private System.Windows.Forms.LinkLabel llbInclude;
        private System.Windows.Forms.LinkLabel llbUp;
    }
}
