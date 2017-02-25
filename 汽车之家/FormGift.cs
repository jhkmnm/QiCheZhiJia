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
        public GiftInfo info { get; set; }
        private List<Merchandise> merchandise;

        public FormGift(GiftInfo info, List<Merchandise> merchandise)
        {
            InitializeComponent();
            this.info = info;
            this.merchandise = merchandise;
            HideError();
            InitDDL();
            LoadData();
        }

        private void LoadData()
        {
            if(info != null)
            {
                txtPrice.Text = info.Price;
                chkgift1.Checked = info.QCYPIsCheck;
                LoadMerchandise();
                chkgift2.Checked = info.YKIsCheck;
                txtOilCar.Text = info.YKValue;
                chkgift3.Checked = info.SYXIsCheck;
                ddlBusinessTax.SelectedValue = string.IsNullOrWhiteSpace(info.SYXValue) ? "" : info.SYXValue;
                chkgift4.Checked = info.JQXIsCheck;
                ddlTrafficTax.SelectedValue = string.IsNullOrWhiteSpace(info.JQXValue) ? "" : info.JQXValue;
                chkgift5.Checked = info.GZSIsCheck;
                if (info.GZSValue == "1")
                    rbtnPurchaseTax1.Checked = true;
                else
                    rbtnPurchaseTax2.Checked = true;
                chkgift6.Checked = info.BaoYangIsCheck;
                if (info.BaoYangType == "1")
                {
                    rbtnMaintenanceInfo1.Checked = true;
                    txtMMoney.Text = string.IsNullOrWhiteSpace(info.BaoYangValue) ? "" : info.BaoYangValue;
                }
                else if (info.BaoYangType == "2")
                {
                    rbtnMaintenanceInfo2.Checked = true;
                    txtMTimes.Text = string.IsNullOrWhiteSpace(info.BaoYangValue) ? "" : info.BaoYangValue;
                }
                else
                {
                    rbtnMaintenanceInfo3.Checked = true;
                    txtMYear.Text = string.IsNullOrWhiteSpace(info.BaoYangValue) ? "" : info.BaoYangValue;
                    txtMMile.Text = string.IsNullOrWhiteSpace(info.BaoYangValue2) ? "" : info.BaoYangValue2;
                }
                chkgift7.Checked = info.OtherInfoIsCheck;
                txtOtherInfo.Text = string.IsNullOrWhiteSpace(info.OtherInfoValue) ? "" : info.OtherInfoValue;
            }
        }

        private void LoadMerchandise()
        {
            if (info.Merchandises != null)
            {
                merchandiseBindingSource.DataSource = info.Merchandises;
                dataGridView1.Refresh();
            }
        }

        private void InitDDL()
        {
            var data = new List<TextValue>();
            var data2 = new List<TextValue>();
            for(int i=1;i<10;i++)
            {
                data.Add(new TextValue { Text = i + "年", Value = i.ToString() });
                data2.Add(new TextValue { Text = i + "年", Value = i.ToString() });
            }

            ddlBusinessTax.DataSource = data;
            ddlBusinessTax.DisplayMember = "Text";
            ddlBusinessTax.ValueMember = "Value";

            ddlTrafficTax.DataSource = data2;
            ddlTrafficTax.DisplayMember = "Text";
            ddlTrafficTax.ValueMember = "Value";
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

            if (chkgift1.Checked && dataGridView1.Rows.Count == 0)
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
                if (info == null)
                    info = new GiftInfo();

                info.IsCheck = price > 0;
                foreach(Control con in this.Controls)
                {
                    var chk = con as CheckBox;
                    if(chk != null && chk.Checked)
                    {
                        info.IsCheck = true;
                        break;
                    }
                }
                
                info.Price = price.ToString();
                info.QCYPIsCheck = chkgift1.Checked;
                info.Merchandises = merchandiseBindingSource.DataSource as List<Merchandise>;
                info.YKIsCheck = chkgift2.Checked;
                info.YKValue = string.IsNullOrWhiteSpace(txtOilCar.Text) ? "" : txtOilCar.Text;
                info.SYXIsCheck = chkgift3.Checked;
                info.SYXValue = ddlBusinessTax.SelectedValue.ToString();
                info.JQXIsCheck = chkgift4.Checked;
                info.JQXValue = ddlTrafficTax.SelectedValue.ToString();
                info.GZSIsCheck = chkgift5.Checked;
                info.GZSValue = rbtnPurchaseTax1.Checked ? "1" : "2";
                info.BaoYangIsCheck = chkgift6.Checked;
                if (rbtnMaintenanceInfo1.Checked)
                {
                    info.BaoYangType = "1";
                    info.BaoYangValue = txtMMoney.Text;
                }                    
                else if (rbtnMaintenanceInfo1.Checked)
                {
                    info.BaoYangType = "2";
                    info.BaoYangValue = txtMTimes.Text;
                }                    
                else
                {
                    info.BaoYangType = "3";
                    info.BaoYangValue = txtMYear.Text;
                    info.BaoYangValue2 = txtMMile.Text;
                }                    
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

        private void lbladdMerChandise_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new FormQCYP(merchandise);
            if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                merchandiseBindingSource.DataSource = form.SelectedMerchandises;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if(e.ColumnIndex == Column1.Index)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
