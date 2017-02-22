using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace Aide
{
    public partial class FormGift : Form
    {
        GiftInfo info;

        public FormGift(GiftInfo info)
        {
            InitializeComponent();
            this.info = info;
            HideError();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)  // 允许输入退格键
            {
                e.Handled = true;   // 经过判断为数字，可以输入
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            HideError();
            bool iserror = false;
            int price = 0;
            if (!string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                int.TryParse(txtPrice.Text, out price);
                if (price > 0 && price < 500)
                {
                    lblPrice.Visible = true;
                    iserror = true;
                }
            }

            if(chkgift1.Checked && pqiche.Controls.Count == 0)
            {
                lblqiche.Visible = true;
                iserror = true;
            }

            if(chkgift2.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtOilCar.Text) || Convert.ToInt32(txtOilCar.Text) < 100)
                {
                    label10.Visible = true;
                    iserror = true;
                }                
            }

            if (chkgift6.Checked)
            {
                if (rbtnMaintenanceInfo1.Checked && (string.IsNullOrWhiteSpace(txtMMoney.Text) || Convert.ToInt32(txtMMoney.Text) < 100))
                {
                    label11.Visible = true;
                    iserror = true;
                }
                else if (rbtnMaintenanceInfo2.Checked && (string.IsNullOrWhiteSpace(txtMTimes.Text) || (Convert.ToInt32(txtMTimes.Text) < 1 || Convert.ToInt32(txtMTimes.Text) > 50)))
                {
                    label12.Visible = true;
                    iserror = true;
                }
                else if (rbtnMaintenanceInfo3.Checked && (string.IsNullOrWhiteSpace(txtMYear.Text) || string.IsNullOrWhiteSpace(txtMMile.Text)))
                {
                    label13.Visible = true;
                    iserror = true;
                }
            }

            if(!iserror)
            {
                info.Price = price;
                info.QCYPIsCheck = chkgift1.Checked;
                //汽车用户列表
                info.YKIsCheck = chkgift2.Checked;
                info.YKValue = string.IsNullOrWhiteSpace(txtOilCar.Text) ? 0 : Convert.ToInt32(txtOilCar.Text);
                info.SYXIsCheck = chkgift3.Checked;
                info.SYXValue = Convert.ToInt32(ddlBusinessTax.SelectedValue);
                info.JQXIsCheck = chkgift4.Checked;
                info.JQXValue = Convert.ToInt32(ddlTrafficTax.SelectedValue);
                info.GZSIsCheck = chkgift5.Checked;
                info.BAOYANGIsCheck = chkgift6.Checked;
                //info.BAOYANGValue = 
                info.OtherInfoIsCheck = chkgift7.Checked;
                info.OtherInfoValue = txtOtherInfo.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void HideError()
        {
            lblPrice.Visible = false;
            lblqiche.Visible = false;
            label10.Visible = false;
            label13.Visible = false;
            label12.Visible = false;
            label11.Visible = false;
        }
    }
}
