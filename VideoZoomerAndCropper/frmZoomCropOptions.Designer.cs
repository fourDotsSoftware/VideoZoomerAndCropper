namespace VideoZoomerAndCropper
{
    partial class frmZoomCropOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZoomCropOptions));
            this.chkJoinOverlayParts = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkDrawBox = new System.Windows.Forms.CheckBox();
            this.nudBoxThickness = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBoxColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbImageAlign = new System.Windows.Forms.ComboBox();
            this.chkJoinCropParts = new System.Windows.Forms.CheckBox();
            this.btnCropPaddingColor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkBlur = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // chkJoinOverlayParts
            // 
            resources.ApplyResources(this.chkJoinOverlayParts, "chkJoinOverlayParts");
            this.chkJoinOverlayParts.BackColor = System.Drawing.Color.Transparent;
            this.chkJoinOverlayParts.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkJoinOverlayParts.Name = "chkJoinOverlayParts";
            this.chkJoinOverlayParts.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::VideoZoomerAndCropper.Properties.Resources.exit;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Image = global::VideoZoomerAndCropper.Properties.Resources.check;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkDrawBox
            // 
            resources.ApplyResources(this.chkDrawBox, "chkDrawBox");
            this.chkDrawBox.BackColor = System.Drawing.Color.Transparent;
            this.chkDrawBox.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkDrawBox.Name = "chkDrawBox";
            this.chkDrawBox.UseVisualStyleBackColor = false;
            // 
            // nudBoxThickness
            // 
            resources.ApplyResources(this.nudBoxThickness, "nudBoxThickness");
            this.nudBoxThickness.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudBoxThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBoxThickness.Name = "nudBoxThickness";
            this.nudBoxThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Name = "label2";
            // 
            // btnBoxColor
            // 
            resources.ApplyResources(this.btnBoxColor, "btnBoxColor");
            this.btnBoxColor.Name = "btnBoxColor";
            this.btnBoxColor.UseVisualStyleBackColor = true;
            this.btnBoxColor.Click += new System.EventHandler(this.btnBoxColor_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Name = "label3";
            // 
            // cmbImageAlign
            // 
            this.cmbImageAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbImageAlign, "cmbImageAlign");
            this.cmbImageAlign.FormattingEnabled = true;
            this.cmbImageAlign.Name = "cmbImageAlign";
            // 
            // chkJoinCropParts
            // 
            resources.ApplyResources(this.chkJoinCropParts, "chkJoinCropParts");
            this.chkJoinCropParts.BackColor = System.Drawing.Color.Transparent;
            this.chkJoinCropParts.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkJoinCropParts.Name = "chkJoinCropParts";
            this.chkJoinCropParts.UseVisualStyleBackColor = false;
            // 
            // btnCropPaddingColor
            // 
            resources.ApplyResources(this.btnCropPaddingColor, "btnCropPaddingColor");
            this.btnCropPaddingColor.Name = "btnCropPaddingColor";
            this.btnCropPaddingColor.UseVisualStyleBackColor = true;
            this.btnCropPaddingColor.Click += new System.EventHandler(this.btnCropPaddingColor_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Name = "label4";
            // 
            // chkBlur
            // 
            resources.ApplyResources(this.chkBlur, "chkBlur");
            this.chkBlur.BackColor = System.Drawing.Color.Transparent;
            this.chkBlur.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkBlur.Name = "chkBlur";
            this.chkBlur.UseVisualStyleBackColor = false;
            // 
            // frmZoomCropOptions
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.chkBlur);
            this.Controls.Add(this.btnCropPaddingColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkJoinCropParts);
            this.Controls.Add(this.cmbImageAlign);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBoxColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudBoxThickness);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDrawBox);
            this.Controls.Add(this.chkJoinOverlayParts);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZoomCropOptions";
            this.Load += new System.EventHandler(this.frmZoomCropOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBoxThickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkJoinOverlayParts;
        private System.Windows.Forms.CheckBox chkDrawBox;
        public System.Windows.Forms.NumericUpDown nudBoxThickness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBoxColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbImageAlign;
        private System.Windows.Forms.CheckBox chkJoinCropParts;
        private System.Windows.Forms.Button btnCropPaddingColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkBlur;
    }
}
