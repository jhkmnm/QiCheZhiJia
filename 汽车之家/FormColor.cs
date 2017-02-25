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
    public partial class FormColor : Form
    {
        List<TextValue> Colors;
        public List<TextValue> SelectedColros { get; set; }
        
        public FormColor(List<TextValue> color)
        {
            InitializeComponent();
            Colors = color;
            LoadCoror();
            SelectedColros = new List<TextValue>();
        }

        private void LoadCoror()
        {
            int xstep = 180;
            int ystep = 28;
            int xstart = 12;
            int ystart = 51;
            for (int i = 0; i < Colors.Count; i++)
            {
                int x = xstart;
                int y = ystart;

                if (i > 0)
                {
                    x = xstart + (xstep * (i % 2));
                    y = ystart + (ystep * (i / 2));
                }

                var chk = new CheckBox();
                chk.Location = new System.Drawing.Point(x, y);
                chk.Name = "chk" + Colors[i].Value;
                chk.Tag = Colors[i].Value;
                chk.Text = Colors[i].Text;
                chk.AutoSize = true;
                chk.Size = new System.Drawing.Size(72, 16);
                chk.TabStop = true;
                chk.UseVisualStyleBackColor = true;
                this.Controls.Add(chk);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control con in this.Controls)
            {
                var chk = con as CheckBox;
                if(chk != null)
                {
                    chk.Checked = chkAll.Checked;
                    chk.Enabled = !chkAll.Checked;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            foreach (Control con in this.Controls)
            {
                var chk = con as CheckBox;
                if (chk != null && chk.Checked)
                {
                    SelectedColros.Add(new TextValue { Text = chk.Text, Value = chk.Tag.ToString() });
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
