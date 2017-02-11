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
    public partial class ImgControl : UserControl
    {
        public string ImgUrl { 
            get { return ptbImg.ImageLocation; } 
            set { ptbImg.ImageLocation = value; } 
        }

        public string ImgSelectID { get; set; }

        public ImgControl()
        {
            InitializeComponent();
        }
    }
}
