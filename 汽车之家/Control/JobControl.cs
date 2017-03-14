using System;
using System.Windows.Forms;
using Model;

namespace Aide
{
    public partial class JobControl : UserControl
    {
        Job job;
        public delegate void delSetJob(Job job);
        public event delSetJob SetJobEvent;

        public void SendResult(Job job)
        {
            if(SetJobEvent != null)
            {
                SetJobEvent(job);
            }            
        }

        public JobControl()
        {
            InitializeComponent();
            ddlPalnType.SelectedIndex = 0;
            ddlQuote_QC.SelectedIndex = 0;
        }

        public void SetJob(Job job)
        {
            this.job = job;
            if(this.job != null)
                InitJob();
        }

        private void InitJob()
        {
            ddlPalnType.SelectedIndex = job.JobType.Value;
            lblState.Text = job.Message();
            if (job.JobType == 1)
            {
                dtpJobDate_Quote.Value = Convert.ToDateTime(job.JobDate);
                dtpQuoteTime_QC.Value = Convert.ToDateTime(job.Time);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(job.Time))
                {
                    p_A_QC.Enabled = true;
                    p_B_QC.Enabled = false;
                    rbtQuote_A_QC.Checked = true;
                    dtpQuer.Value = Convert.ToDateTime(job.Time);
                }
                else
                {
                    p_A_QC.Enabled = false;
                    p_B_QC.Enabled = true;
                    rbtQuote_B_QC.Checked = true;                    
                    ddlQuote_QC.SelectedIndex = job.Space.Value / 1000 / 60 / 60 >= 1 ? 1 : 0;
                    nudQuote_QC.Value = ddlQuote_QC.SelectedIndex == 1 ? job.Space.Value / 1000 / 60 / 60 : job.Space.Value / 1000 / 60;
                    dtpQuote_S_QC.Value = Convert.ToDateTime(job.StartTime);
                    dtpQuote_E_QC.Value = Convert.ToDateTime(job.EndTime);
                }
                SendResult(job);
            }
        }

        private void btnSetting_QC_Click(object sender, EventArgs e)
        {
            DateTime dtnow = DateTime.Now;
            if (ddlPalnType.Text == "选择计划类型")
            {
                MessageBox.Show("先选择计划类型");
                return;
            }

            if(job == null)
                job = new Job();
            if (ddlPalnType.Text == "执行一次")
            {
                job.JobType = 1;
                job.JobDate = dtpJobDate_Quote.Value.ToString("yyyy-MM-dd");
                job.Time = dtpQuoteTime_QC.Value.ToString("HH:mm:ss");
                DateTime dt = Convert.ToDateTime(job.JobDate + " " + job.Time);
                if ((dtnow - dt).TotalSeconds > 0)
                {
                    MessageBox.Show("设置的时间无效");
                    return;
                }
                lblState.Text = "只执行一次，时间是：" + job.JobDate + " " + job.Time;
            }
            else
            {
                job.JobType = 2;
                if (rbtQuote_A_QC.Checked)
                {
                    job.Time = dtpQuer.Value.ToString("HH:mm:ss");
                    job.StartTime = "00:00:00";
                    job.EndTime = "23:59:59";
                    int interval = 24 * 60 * 60 * 1000;
                    job.Space = interval;
                    lblState.Text = "每天执行一次，时间是：" + job.Time;
                }
                else
                {
                    if(nudQuote_QC.Value == 0)
                    {
                        MessageBox.Show("间隔时间必须大于0");
                        return;
                    }

                    job.StartTime = dtpQuote_S_QC.Value.ToString("HH:mm:ss");
                    job.EndTime = dtpQuote_E_QC.Value.ToString("HH:mm:ss");
                    int interval = ddlQuote_QC.Text == "分钟" ? Convert.ToInt32(nudQuote_QC.Value) * 60 * 1000 : Convert.ToInt32(nudQuote_QC.Value) * 60 * 60 * 1000;
                    job.Space = interval;
                    lblState.Text = string.Format("在每天的{0}到{1}，每隔{2}{3}执行一次", job.StartTime, job.EndTime, nudQuote_QC.Value, ddlQuote_QC.Text);
                }
            }
            SendResult(job);
        }

        private void ddlPalnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPalnType.Text == "执行一次")
            {
                panel4.Enabled = false;
                panel3.Enabled = true;
            }
            else if (ddlPalnType.Text == "重复执行")
            {
                panel4.Enabled = true;
                panel3.Enabled = false;
            }
            else
            {
                panel4.Enabled = false;
                panel3.Enabled = false;
            }
        }

        private void rbtQuote_CheckedChanged(object sender, EventArgs e)
        {
            var obj = (RadioButton)sender;
            if (obj.Checked)
            {
                if (obj == rbtQuote_A_QC)
                {
                    p_A_QC.Enabled = true;
                    p_B_QC.Enabled = false;
                    rbtQuote_B_QC.Checked = false;
                }
                else
                {
                    p_A_QC.Enabled = false;
                    p_B_QC.Enabled = true;
                    rbtQuote_A_QC.Checked = false;
                }
            }
        }
    }
}
