using System.Collections.Generic;
using System.Windows.Forms;
using Model;

namespace Aide
{
    public partial class FormOrderCondition : Form
    {
        private DAL dal = new DAL();
        private Site site;

        public FormOrderCondition(Site site)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.site = site;
            areaBindingSource.DataSource = dal.GetArea(Tool.site.ToString());
            dataGridView1.Refresh();
            orderTypeBindingSource.DataSource = dal.GetOrderTypes(Tool.site.ToString());
            dataGridView3.Refresh();
            
            if(Tool.site == Aide.Site.Qiche)
            {
                chkSpceAll.Visible = true;
                dataGridView2.Visible = true;
                specBindingSource.DataSource = dal.GetSpecs();
                dataGridView2.Refresh();
            }
        }

        private void btnYes_Click(object sender, System.EventArgs e)
        {
            var area = areaBindingSource.DataSource as List<Area>;
            var order = orderTypeBindingSource.DataSource as List<OrderType>;
            dal.UpdateAreaChecked(area);
            dal.UpdateOrderTypeChecked(order);

            if (Tool.site == Aide.Site.Qiche)
            {
                var spec = specBindingSource.DataSource as List<Spec>;
                dal.UpdateSpecsChecked(spec);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void chkAllCity_CheckedChanged(object sender, System.EventArgs e)
        {
            var area = areaBindingSource.DataSource as List<Area>;
            area.ForEach(f => f.IsChecked = chkAllCity.Checked);
            dataGridView1.Refresh();
        }

        private void chkTypeAll_CheckedChanged(object sender, System.EventArgs e)
        {
            var order = orderTypeBindingSource.DataSource as List<OrderType>;
            order.ForEach(f => f.IsCheck = chkTypeAll.Checked);
            dataGridView3.Refresh();
        }

        private void chkSpceAll_CheckedChanged(object sender, System.EventArgs e)
        {
            var spec = specBindingSource.DataSource as List<Spec>;
            spec.ForEach(f => f.IsCheck = chkSpceAll.Checked);
            dataGridView2.Refresh();
        }
    }
}
