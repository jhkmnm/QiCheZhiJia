using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aide
{
    public partial class FormQCYP : Form
    {
        public List<Merchandise> SelectedMerchandises
        {
            get
            {
                return (merchandiseBindingSource.DataSource as List<Merchandise>)
                    .Where(w => w.IsCheck)
                    .ToList();
            }
        }

        private List<Merchandise> Source;

        public FormQCYP(List<Merchandise> Source)
        {
            InitializeComponent();
            this.Source = Source;

            LoadData();
        }

        public void LoadData()
        {
            var priceA = string.IsNullOrWhiteSpace(txtPriceA.Text) ? 0 : Convert.ToDecimal(txtPriceA.Text);
            var priceB = string.IsNullOrWhiteSpace(txtPriceB.Text) ? 999999 : Convert.ToDecimal(txtPriceB.Text);;
            var name = txtName.Text;

            var source = Source
                            .Where(w => (Convert.ToDecimal(w.Price) >= priceA && Convert.ToDecimal(w.Price) <= priceB) && (string.IsNullOrWhiteSpace(name) || w.name.Contains(name)))
                            .ToList();

            merchandiseBindingSource.DataSource = source;
            dataGridView1.Refresh();
        }

        private void txtPriceA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)  // 允许输入退格键
            {
                e.Handled = true;   // 经过判断为数字，可以输入
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
