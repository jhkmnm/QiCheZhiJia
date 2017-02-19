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
    public partial class ImgControl1 : UserControl
    {
        public ImgControl1()
        {
            InitializeComponent();
        }

        private void lblFocusImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lblFocusImg.Tag.ToString());
        }
    }
}
