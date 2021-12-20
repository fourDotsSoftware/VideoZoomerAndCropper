using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VideoZoomerAndCropper
{
    public partial class frmZoomCropOptions : VideoZoomerAndCropper.CustomForm
    {
        public frmZoomCropOptions()
        {
            InitializeComponent();
        }

        private void frmZoomCropOptions_Load(object sender, EventArgs e)
        {
            chkJoinOverlayParts.Checked = Properties.Settings.Default.JoinOverlayParts;

            chkJoinCropParts.Checked = Properties.Settings.Default.JoinCropParts;

            chkDrawBox.Checked = Properties.Settings.Default.DrawBox;

            nudBoxThickness.Value = Properties.Settings.Default.BoxThickness;

            btnBoxColor.BackColor = Properties.Settings.Default.BoxColor;

            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Right"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Right"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Right"));

            cmbImageAlign.SelectedIndex = Properties.Settings.Default.CropJoinVideoAlign;

            btnCropPaddingColor.BackColor = Properties.Settings.Default.CropPaddingColor;

            chkBlur.Checked = Properties.Settings.Default.Blur;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.JoinOverlayParts = chkJoinOverlayParts.Checked;

            Properties.Settings.Default.JoinCropParts = chkJoinCropParts.Checked;

            Properties.Settings.Default.DrawBox = chkDrawBox.Checked;

            Properties.Settings.Default.BoxThickness = (int)nudBoxThickness.Value;

            Properties.Settings.Default.BoxColor = btnBoxColor.BackColor;

            Properties.Settings.Default.CropJoinVideoAlign = cmbImageAlign.SelectedIndex;

            Properties.Settings.Default.CropPaddingColor = btnCropPaddingColor.BackColor;

            Properties.Settings.Default.Blur = chkBlur.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnBoxColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.FullOpen = true;

            cd.Color = btnBoxColor.BackColor;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnBoxColor.BackColor = cd.Color;
            }
        }

        private void btnCropPaddingColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.FullOpen = true;

            cd.Color = btnCropPaddingColor.BackColor;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnCropPaddingColor.BackColor = cd.Color;
            }
        }
    }
}
