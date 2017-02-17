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
        }

        public CarControl()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;

            StoreStateList.AddRange(new[] {
                new TextValue { Text = "库存状态", Value = "-1" },
                new TextValue { Text = "现车充足", Value = "1" },
                new TextValue { Text = "少量现车", Value = "2" },
                new TextValue { Text = "需提前预定", Value = "3" }
            });

            ddlStoreState.DataSource = StoreStateList;
            ddlStoreState.DisplayMember = "Text";
            ddlStoreState.ValueMember = "Value";
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
                chk.Text = CarDataSource.YearType[i];

                chk.CheckedChanged += Chk_CheckedChanged;
                chk.AutoSize = true;
                chk.Checked = true;
                chk.CheckState = CheckState.Checked;
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
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                linkLabel1.Text = "查看";
                splitContainer1.Panel2Collapsed = true;
            }
        }
    }
}
