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

        public PromotionCars CarDataSource
        {
            get
            {
                return carBindingSource.DataSource as PromotionCars;
            }
            set
            {
                if (value == null)
                    carBindingSource.Clear();
                else
                {
                    carBindingSource.DataSource = value;
                    InitYearType();
                }
            }
        }

        public CarControl()
        {
            InitializeComponent();

            StoreStateList.AddRange(new[] {
                new TextValue { Text = "库存状态", Value = "-1" },
                new TextValue { Text = "库存状态", Value = "-1" },
                new TextValue { Text = "库存状态", Value = "-1" },
                new TextValue { Text = "库存状态", Value = "-1" }
            });

            ddlStoreState.DataSource = StoreStateList;
            ddlStoreState.DisplayMember = "Text";
            ddlStoreState.ValueMember = "Value";
        }

        public void InitYearType()
        {
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
            //throw new NotImplementedException();
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
    }
}
