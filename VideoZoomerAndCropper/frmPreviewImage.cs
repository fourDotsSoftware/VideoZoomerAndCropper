using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VideoZoomerAndCropper
{
    public partial class frmPreviewImage : VideoZoomerAndCropper.CustomForm
    {
        public frmPreviewImage(string filepath)
        {
            InitializeComponent();

            this.Text = filepath;

            //3Image img = ImageHelper.LoadImage(filepath);

            Image img = Image.FromFile(filepath);

            picImage.Image = img;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
