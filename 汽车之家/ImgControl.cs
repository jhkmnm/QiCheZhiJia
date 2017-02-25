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

        public string CSID { get; set; }
        public YiChe yiche { get; set; }
        public string ImageUpload { get; set; }


        public string ImgSelectID { get; set; }

        public ImgControl()
        {
            InitializeComponent();
        }

        private void llbInclude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new PhotoSelectNew(yiche, CSID);
            if(form.ShowDialog() == DialogResult.OK)
            {
                ptbImg.ImageLocation = form.SelectedImgPath;
            }
        }

        private void llbUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new UploadFile(yiche, ImageUpload);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ptbImg.ImageLocation = form.CurrentSelected.pbxImg.ImageLocation;
            }
        }
    }
}
