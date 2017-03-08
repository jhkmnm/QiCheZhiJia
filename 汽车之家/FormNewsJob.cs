using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Aide
{
    public partial class FormNewsJob : Form
    {
        private Job job;
        private string newsID;
        private DAL dal = new DAL();

        public Job Job { get { return job; } }

        public FormNewsJob(string newsID)
        {
            InitializeComponent();
            InitJob();
            this.newsID = newsID;
        }

        private void InitJob()
        {
            job = dal.GetJob(newsID);

            if (job != null && job.JobType == 1)
            {
                dtpJobDate_Quote.Value = Convert.ToDateTime(job.JobDate);
                dtpQuoteTime_QC.Value = Convert.ToDateTime(job.Time);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (job == null)
            {
                job = new Job();
                job.JobName = newsID;
            }
                
            job.JobType = 1;
            job.JobDate = dtpJobDate_Quote.Value.ToString("yyyy-MM-dd");
            job.Time = dtpQuoteTime_QC.Value.ToString("HH:mm:ss");
            //lblState.Text = "只执行一次，时间是：" + job.JobDate + " " + job.Time;
            dal.AddJob(job);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
