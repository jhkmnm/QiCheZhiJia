using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AideAdmin
{
    public partial class Form2 : Form
    {
        List<localhost.Dictionaries> data = new List<localhost.Dictionaries>();

        public Form2()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                data.AddRange(Tool.service.GetDic());
                dgvData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                data.ForEach(item => Tool.service.UpdateDic(item.Key, item.Value));
                MessageBox.Show("保存成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
