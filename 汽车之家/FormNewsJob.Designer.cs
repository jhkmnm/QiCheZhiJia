namespace Aide
{
    partial class FormNewsJob
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
            this.dtpJobDate_Quote = new System.Windows.Forms.DateTimePicker();
            this.dtpQuoteTime_QC = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpJobDate_Quote
            // 
            this.dtpJobDate_Quote.CustomFormat = "yyyy-MM-dd";
            this.dtpJobDate_Quote.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpJobDate_Quote.Location = new System.Drawing.Point(2, 1);
            this.dtpJobDate_Quote.Name = "dtpJobDate_Quote";
            this.dtpJobDate_Quote.Size = new System.Drawing.Size(116, 21);
            this.dtpJobDate_Quote.TabIndex = 5;
            // 
            // dtpQuoteTime_QC
            // 
            this.dtpQuoteTime_QC.CustomFormat = "HH:mm:ss";
            this.dtpQuoteTime_QC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQuoteTime_QC.Location = new System.Drawing.Point(122, 1);
            this.dtpQuoteTime_QC.Name = "dtpQuoteTime_QC";
            this.dtpQuoteTime_QC.ShowUpDown = true;
            this.dtpQuoteTime_QC.Size = new System.Drawing.Size(87, 21);
            this.dtpQuoteTime_QC.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormNewsJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 45);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpJobDate_Quote);
            this.Controls.Add(this.dtpQuoteTime_QC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewsJob";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormNewsJob";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpJobDate_Quote;
        private System.Windows.Forms.DateTimePicker dtpQuoteTime_QC;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}