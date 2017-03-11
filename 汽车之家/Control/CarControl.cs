using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aide
{
    public partial class CarControl : UserControl
    {
        List<TextValue> StoreStateList = new List<TextValue>();
        PromotionCars _promotioncars;

        public PromotionCars CarDataSource
        {
            get
            {
                return _promotioncars;
            }
            set
            {
                _promotioncars = value;
                if (value == null)
                {
                    carBindingSource.Clear();
                }
                else
                {
                    carBindingSource.DataSource = value.Cars;
                    bindingSource1.DataSource = value.PublishCarList;
                    InitYearType();
                    label1.Text = value.Note;
                    splitContainer1.Panel2Collapsed = true;
                }
            }
        }

        public List<string> YearTypeList
        {
            get
            {
                var list = new List<string>();
                foreach(Control con in panel1.Controls)
                {
                    var chk = con as CheckBox;
                    if(chk != null && chk.Checked)
                    {
                        list.Add(chk.Text);
                    }
                }
                return list;
            }
        }

        public List<TextValue> Colors { get; set; }

        /// <summary>
        /// 是否显示明细
        /// </summary>
        /// <param name="isDetail"></param>
        public void ShowType(bool isDetail)
        {            
            colPushedCount.Visible = !isDetail;
            colCarReferPrice.Visible = isDetail;
            colPromotionPrice.Visible = isDetail;
            colStoreState.Visible = isDetail;
            colSubsidies.Visible = isDetail;
            colFavorablePrice.Visible = isDetail;
            colColorName.Visible = isDetail;
            colTypeName.Width = isDetail ? 100 : 300;

            txtDiscount.Visible = isDetail;
            txtFavorablePrice.Visible = isDetail;
            ddlStoreState.Visible = isDetail;
            llbColor.Visible = isDetail;
            colAction.Visible = isDetail;
        }

        public CarControl()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;

            var state = new TextValue[4];

            StoreStateList.AddRange(new[] {
                new TextValue { Text = "库存状态", Value = "-1" },
                new TextValue { Text = "现车充足", Value = "1" },
                new TextValue { Text = "少量现车", Value = "2" },
                new TextValue { Text = "需提前预定", Value = "3" }
            });
            StoreStateList.CopyTo(state);

            ddlStoreState.DataSource = StoreStateList;
            ddlStoreState.DisplayMember = "Text";
            ddlStoreState.ValueMember = "Value";

            StoreStateBindingSource.DataSource = state.ToList();
        }

        public void InitYearType()
        {
            this.panel1.Controls.Clear();
            int xstep = 79;
            int ystep = 22;
            int xstart = 7;
            int ystart = 5;
            for (int i = 0; i < CarDataSource.YearType.Count; i++)
            {               
                int x = xstart + (xstep * i);
                int y = ystart;

                if (i > 2)
                {
                    x = xstart + (xstep * (i - 3));
                    y = ystart + ystep;
                }

                var chk = new CheckBox();
                chk.Location = new Point(x, y);
                chk.Name = "chk" + i;
                chk.Text = CarDataSource.YearType[i].Text;
                chk.Checked = CarDataSource.YearType[i].IsChecked;
                chk.CheckedChanged += Chk_CheckedChanged;
                chk.AutoSize = true;                
                chk.Size = new Size(72, 16);
                chk.UseVisualStyleBackColor = true;
                this.panel1.Controls.Add(chk);                
            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            _promotioncars.Cars.FindAll(w => w.YearType == chk.Text).ForEach(f => f.IsCheck = chk.Checked);
            dgvCar.Refresh();
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (txt.Text.Trim() == "")
            {
                txt.Text = txt.Tag.ToString().Trim();
                txt.ForeColor = Color.Gray;
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (txt.Text == txt.Tag.ToString())
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(linkLabel1.Text == "查看")
            {
                linkLabel1.Text = "收起";
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                linkLabel1.Text = "查看";
                splitContainer1.Panel2Collapsed = true;
            }
        }        

        private void txtFavorablePrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal price = 0;
                decimal.TryParse(txtFavorablePrice.Text, out price);
                _promotioncars.Cars.ForEach(f => f.FavorablePrice = price);
                dgvCar.Refresh();
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int price = 0;
                int.TryParse(txtDiscount.Text, out price);
                if(price > 35)
                {
                    MessageBox.Show("请输入0-35之间的整数");
                    return;
                }
                _promotioncars.Cars.ForEach(f => f.FavorablePrice = f.CarReferPrice * (price / 100.0m));
                dgvCar.Refresh();
            }
        }

        private void dgvCar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if(e.ColumnIndex == colIsCheck.Index)
            {
                _promotioncars.Cars[e.RowIndex].IsCheck = !_promotioncars.Cars[e.RowIndex].IsCheck;
                dgvCar.Rows[e.RowIndex].ReadOnly = _promotioncars.Cars[e.RowIndex].IsCheck;
            }
            else if(e.ColumnIndex == colAction.Index)
            {
                var form = new FormColor(Colors);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var allcolor = form.SelectedColros.FirstOrDefault(w => w.Text == "颜色齐全");
                    string colorname = "";
                    if (allcolor != null)
                        colorname = "颜色齐全";
                    else
                    {
                        colorname = string.Join(",", form.SelectedColros.Select(s => s.Text));
                    }
                    _promotioncars.Radlst = form.SelectedColros.Select(s => s.Value).ToList();

                    ((Car)carBindingSource.Current).ColorName = colorname;
                    dgvCar.Refresh();
                }
            }
        }

        private void llbColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new FormColor(Colors);
            if(form.ShowDialog() == DialogResult.OK)
            {
                var allcolor = form.SelectedColros.FirstOrDefault(w => w.Text == "颜色齐全");
                string colorname = "";
                if (allcolor != null)
                    colorname = "颜色齐全";
                else
                {
                    colorname = string.Join(",", form.SelectedColros.Select(s => s.Text));
                }
                _promotioncars.Radlst = form.SelectedColros.Select(s => s.Value).ToList();

                _promotioncars.Cars.ForEach(f => f.ColorName = colorname);
                dgvCar.Refresh();
            }
        }
    }
}
