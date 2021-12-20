namespace VideoZoomerAndCropper
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bwRemoveLogo = new System.ComponentModel.BackgroundWorker();
            this.timJoinVideos = new System.Windows.Forms.Timer(this.components);
            this.btnRemoveClip = new System.Windows.Forms.Button();
            this.btnAddClip = new System.Windows.Forms.Button();
            this.cmbClip = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudEndTime = new System.Windows.Forms.NumericUpDown();
            this.nudStartTime = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlblVideoFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslDuration = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.btnSelectEntireArea = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEndUpdateImage = new System.Windows.Forms.Button();
            this.btnStartUpdateImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.mskStart = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mskEnd = new System.Windows.Forms.MaskedTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsdbOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbShare = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsiFacebook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiGooglePlus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiLinkedIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPreviewImage = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomCrop = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomAndCropOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenFinishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.useFFMPEG64bitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useFFMPEG32bitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEXIFInformationFromSourceVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepCreationDateFromSourceVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepLastModificationDateFromSourceVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMessageOnSucessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiToolsPlayOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.compressVideosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tiToolsOpenOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tiToolsExploreOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tiToolsCopyPathOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tiToolsVideoInfoOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languages1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languages2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pleaseDonateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForNewVersionEachWeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiHelpFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.followUsOnTwitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visit4dotsSoftwareWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpStartTime = new System.Windows.Forms.GroupBox();
            this.tbStartTime = new VideoZoomerAndCropper.PreviewTrackBar();
            this.grpEndTime = new System.Windows.Forms.GroupBox();
            this.tbEndTime = new VideoZoomerAndCropper.PreviewTrackBar();
            this.pic7 = new VideoZoomerAndCropper.PicHandle();
            this.pic8 = new VideoZoomerAndCropper.PicHandle();
            this.pic6 = new VideoZoomerAndCropper.PicHandle();
            this.pic5 = new VideoZoomerAndCropper.PicHandle();
            this.pic4 = new VideoZoomerAndCropper.PicHandle();
            this.pic3 = new VideoZoomerAndCropper.PicHandle();
            this.pic2 = new VideoZoomerAndCropper.PicHandle();
            this.pic1 = new VideoZoomerAndCropper.PicHandle();
            this.picMovie = new VideoZoomerAndCropper.picLogo();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOptions = new System.Windows.Forms.Button();
            this.rdCrop = new System.Windows.Forms.RadioButton();
            this.rdZoom = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.picMovieB = new VideoZoomerAndCropper.picLogo();
            this.picB6 = new VideoZoomerAndCropper.PicHandle();
            this.picB8 = new VideoZoomerAndCropper.PicHandle();
            this.picB4 = new VideoZoomerAndCropper.PicHandle();
            this.picB2 = new VideoZoomerAndCropper.PicHandle();
            this.picB7 = new VideoZoomerAndCropper.PicHandle();
            this.picB5 = new VideoZoomerAndCropper.PicHandle();
            this.picB3 = new VideoZoomerAndCropper.PicHandle();
            this.picB1 = new VideoZoomerAndCropper.PicHandle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSelectEntireAreaB = new System.Windows.Forms.Button();
            this.nudHeightB = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudWidthB = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudYB = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudXB = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.chkKeepAspectRatio = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartTime)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpStartTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStartTime)).BeginInit();
            this.grpEndTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMovie)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMovieB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXB)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bwRemoveLogo
            // 
            this.bwRemoveLogo.WorkerReportsProgress = true;
            this.bwRemoveLogo.WorkerSupportsCancellation = true;
            this.bwRemoveLogo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRemoveLogo_DoWork);
            this.bwRemoveLogo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRemoveLogo_ProgressChanged);
            this.bwRemoveLogo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRemoveLogo_RunWorkerCompleted);
            // 
            // timJoinVideos
            // 
            this.timJoinVideos.Interval = 1000;
            this.timJoinVideos.Tick += new System.EventHandler(this.timJoinVideos_Tick_1);
            // 
            // btnRemoveClip
            // 
            resources.ApplyResources(this.btnRemoveClip, "btnRemoveClip");
            this.btnRemoveClip.Image = global::VideoZoomerAndCropper.Properties.Resources.delete;
            this.btnRemoveClip.Name = "btnRemoveClip";
            this.btnRemoveClip.UseVisualStyleBackColor = true;
            this.btnRemoveClip.Click += new System.EventHandler(this.btnRemoveClip_Click);
            // 
            // btnAddClip
            // 
            resources.ApplyResources(this.btnAddClip, "btnAddClip");
            this.btnAddClip.Image = global::VideoZoomerAndCropper.Properties.Resources.add;
            this.btnAddClip.Name = "btnAddClip";
            this.btnAddClip.UseVisualStyleBackColor = true;
            this.btnAddClip.Click += new System.EventHandler(this.btnAddClip_Click);
            // 
            // cmbClip
            // 
            resources.ApplyResources(this.cmbClip, "cmbClip");
            this.cmbClip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClip.FormattingEnabled = true;
            this.cmbClip.Name = "cmbClip";
            this.cmbClip.SelectedIndexChanged += new System.EventHandler(this.cmbClip_SelectedIndexChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Name = "label7";
            // 
            // nudEndTime
            // 
            resources.ApplyResources(this.nudEndTime, "nudEndTime");
            this.nudEndTime.Name = "nudEndTime";
            this.nudEndTime.ValueChanged += new System.EventHandler(this.nudEndTime_ValueChanged);
            this.nudEndTime.Click += new System.EventHandler(this.nudEndTime_Click);
            // 
            // nudStartTime
            // 
            resources.ApplyResources(this.nudStartTime, "nudStartTime");
            this.nudStartTime.Name = "nudStartTime";
            this.nudStartTime.ValueChanged += new System.EventHandler(this.nudStartTime_ValueChanged);
            this.nudStartTime.Click += new System.EventHandler(this.nudStartTime_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlblVideoFile,
            this.lblElapsedTime,
            this.tslDuration});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tlblVideoFile
            // 
            this.tlblVideoFile.ForeColor = System.Drawing.Color.DimGray;
            this.tlblVideoFile.Name = "tlblVideoFile";
            resources.ApplyResources(this.tlblVideoFile, "tlblVideoFile");
            this.tlblVideoFile.Spring = true;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.Name = "lblElapsedTime";
            resources.ApplyResources(this.lblElapsedTime, "lblElapsedTime");
            // 
            // tslDuration
            // 
            this.tslDuration.Name = "tslDuration";
            resources.ApplyResources(this.tslDuration, "tslDuration");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.nudHeight);
            this.groupBox1.Controls.Add(this.btnSelectEntireArea);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nudWidth);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // nudHeight
            // 
            resources.ApplyResources(this.nudHeight, "nudHeight");
            this.nudHeight.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // btnSelectEntireArea
            // 
            resources.ApplyResources(this.btnSelectEntireArea, "btnSelectEntireArea");
            this.btnSelectEntireArea.ForeColor = System.Drawing.Color.Black;
            this.btnSelectEntireArea.Name = "btnSelectEntireArea";
            this.btnSelectEntireArea.UseVisualStyleBackColor = true;
            this.btnSelectEntireArea.Click += new System.EventHandler(this.btnSelectEntireArea_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Name = "label6";
            // 
            // nudWidth
            // 
            resources.ApplyResources(this.nudWidth, "nudWidth");
            this.nudWidth.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // nudY
            // 
            resources.ApplyResources(this.nudY, "nudY");
            this.nudY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Name = "label4";
            // 
            // nudX
            // 
            resources.ApplyResources(this.nudX, "nudX");
            this.nudX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // btnEndUpdateImage
            // 
            resources.ApplyResources(this.btnEndUpdateImage, "btnEndUpdateImage");
            this.btnEndUpdateImage.Name = "btnEndUpdateImage";
            this.btnEndUpdateImage.UseVisualStyleBackColor = true;
            this.btnEndUpdateImage.Click += new System.EventHandler(this.btnEndUpdateImage_Click);
            // 
            // btnStartUpdateImage
            // 
            resources.ApplyResources(this.btnStartUpdateImage, "btnStartUpdateImage");
            this.btnStartUpdateImage.Name = "btnStartUpdateImage";
            this.btnStartUpdateImage.UseVisualStyleBackColor = true;
            this.btnStartUpdateImage.Click += new System.EventHandler(this.btnStartUpdateImage_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Name = "label2";
            // 
            // mskStart
            // 
            resources.ApplyResources(this.mskStart, "mskStart");
            this.mskStart.HidePromptOnLeave = true;
            this.mskStart.Name = "mskStart";
            this.mskStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskStart_KeyPress);
            this.mskStart.Validating += new System.ComponentModel.CancelEventHandler(this.mskStart_Validating);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Name = "label3";
            // 
            // mskEnd
            // 
            resources.ApplyResources(this.mskEnd, "mskEnd");
            this.mskEnd.HidePromptOnLeave = true;
            this.mskEnd.Name = "mskEnd";
            this.mskEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskEnd_KeyPress);
            this.mskEnd.Validating += new System.ComponentModel.CancelEventHandler(this.mskEnd_Validating);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsdbOpen,
            this.tsbShare,
            this.toolStripSeparator2,
            this.tsbPreviewImage,
            this.tsbZoomCrop});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsdbOpen
            // 
            resources.ApplyResources(this.tsdbOpen, "tsdbOpen");
            this.tsdbOpen.Image = global::VideoZoomerAndCropper.Properties.Resources.folder1;
            this.tsdbOpen.Name = "tsdbOpen";
            this.tsdbOpen.ButtonClick += new System.EventHandler(this.tsdbOpen_ButtonClick);
            this.tsdbOpen.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsdbOpen_DropDownItemClicked);
            // 
            // tsbShare
            // 
            resources.ApplyResources(this.tsbShare, "tsbShare");
            this.tsbShare.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiFacebook,
            this.tsiTwitter,
            this.tsiGooglePlus,
            this.tsiLinkedIn,
            this.tsiEmail});
            this.tsbShare.Image = global::VideoZoomerAndCropper.Properties.Resources.facebook_24;
            this.tsbShare.Name = "tsbShare";
            // 
            // tsiFacebook
            // 
            this.tsiFacebook.Image = global::VideoZoomerAndCropper.Properties.Resources.facebook_24;
            this.tsiFacebook.Name = "tsiFacebook";
            resources.ApplyResources(this.tsiFacebook, "tsiFacebook");
            this.tsiFacebook.Click += new System.EventHandler(this.tsiFacebook_Click);
            // 
            // tsiTwitter
            // 
            this.tsiTwitter.Image = global::VideoZoomerAndCropper.Properties.Resources.twitter_24;
            this.tsiTwitter.Name = "tsiTwitter";
            resources.ApplyResources(this.tsiTwitter, "tsiTwitter");
            this.tsiTwitter.Click += new System.EventHandler(this.tsiTwitter_Click);
            // 
            // tsiGooglePlus
            // 
            this.tsiGooglePlus.Image = global::VideoZoomerAndCropper.Properties.Resources.googleplus_24;
            this.tsiGooglePlus.Name = "tsiGooglePlus";
            resources.ApplyResources(this.tsiGooglePlus, "tsiGooglePlus");
            this.tsiGooglePlus.Click += new System.EventHandler(this.tsiGooglePlus_Click);
            // 
            // tsiLinkedIn
            // 
            this.tsiLinkedIn.Image = global::VideoZoomerAndCropper.Properties.Resources.linkedin_24;
            this.tsiLinkedIn.Name = "tsiLinkedIn";
            resources.ApplyResources(this.tsiLinkedIn, "tsiLinkedIn");
            this.tsiLinkedIn.Click += new System.EventHandler(this.tsiLinkedIn_Click);
            // 
            // tsiEmail
            // 
            this.tsiEmail.Image = global::VideoZoomerAndCropper.Properties.Resources.mail;
            this.tsiEmail.Name = "tsiEmail";
            resources.ApplyResources(this.tsiEmail, "tsiEmail");
            this.tsiEmail.Click += new System.EventHandler(this.tsiEmail_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbPreviewImage
            // 
            resources.ApplyResources(this.tsbPreviewImage, "tsbPreviewImage");
            this.tsbPreviewImage.ForeColor = System.Drawing.Color.Black;
            this.tsbPreviewImage.Image = global::VideoZoomerAndCropper.Properties.Resources.view1;
            this.tsbPreviewImage.Name = "tsbPreviewImage";
            this.tsbPreviewImage.Click += new System.EventHandler(this.tsbPreviewImage_Click);
            // 
            // tsbZoomCrop
            // 
            resources.ApplyResources(this.tsbZoomCrop, "tsbZoomCrop");
            this.tsbZoomCrop.ForeColor = System.Drawing.Color.DarkBlue;
            this.tsbZoomCrop.Image = global::VideoZoomerAndCropper.Properties.Resources.flash1;
            this.tsbZoomCrop.Name = "tsbZoomCrop";
            this.tsbZoomCrop.Click += new System.EventHandler(this.tsbConvert_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem1,
            this.tiToolsPlayOutput,
            this.downloadToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomAndCropOptionsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.whenFinishedToolStripMenuItem,
            this.toolStripMenuItem3,
            this.useFFMPEG64bitToolStripMenuItem,
            this.useFFMPEG32bitToolStripMenuItem,
            this.copyEXIFInformationFromSourceVideoToolStripMenuItem,
            this.keepCreationDateFromSourceVideoToolStripMenuItem,
            this.keepLastModificationDateFromSourceVideoToolStripMenuItem,
            this.showMessageOnSucessToolStripMenuItem});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            resources.ApplyResources(this.optionsToolStripMenuItem1, "optionsToolStripMenuItem1");
            // 
            // zoomAndCropOptionsToolStripMenuItem
            // 
            this.zoomAndCropOptionsToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.preferences;
            this.zoomAndCropOptionsToolStripMenuItem.Name = "zoomAndCropOptionsToolStripMenuItem";
            resources.ApplyResources(this.zoomAndCropOptionsToolStripMenuItem, "zoomAndCropOptionsToolStripMenuItem");
            this.zoomAndCropOptionsToolStripMenuItem.Click += new System.EventHandler(this.zoomAndCropOptionsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // whenFinishedToolStripMenuItem
            // 
            this.whenFinishedToolStripMenuItem.Name = "whenFinishedToolStripMenuItem";
            resources.ApplyResources(this.whenFinishedToolStripMenuItem, "whenFinishedToolStripMenuItem");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // useFFMPEG64bitToolStripMenuItem
            // 
            this.useFFMPEG64bitToolStripMenuItem.Name = "useFFMPEG64bitToolStripMenuItem";
            resources.ApplyResources(this.useFFMPEG64bitToolStripMenuItem, "useFFMPEG64bitToolStripMenuItem");
            this.useFFMPEG64bitToolStripMenuItem.Click += new System.EventHandler(this.useFFMPEG64bitToolStripMenuItem_Click);
            // 
            // useFFMPEG32bitToolStripMenuItem
            // 
            this.useFFMPEG32bitToolStripMenuItem.Name = "useFFMPEG32bitToolStripMenuItem";
            resources.ApplyResources(this.useFFMPEG32bitToolStripMenuItem, "useFFMPEG32bitToolStripMenuItem");
            this.useFFMPEG32bitToolStripMenuItem.Click += new System.EventHandler(this.useFFMPEG32bitToolStripMenuItem_Click);
            // 
            // copyEXIFInformationFromSourceVideoToolStripMenuItem
            // 
            this.copyEXIFInformationFromSourceVideoToolStripMenuItem.CheckOnClick = true;
            this.copyEXIFInformationFromSourceVideoToolStripMenuItem.Name = "copyEXIFInformationFromSourceVideoToolStripMenuItem";
            resources.ApplyResources(this.copyEXIFInformationFromSourceVideoToolStripMenuItem, "copyEXIFInformationFromSourceVideoToolStripMenuItem");
            this.copyEXIFInformationFromSourceVideoToolStripMenuItem.Click += new System.EventHandler(this.copyEXIFInformationFromSourceVideoToolStripMenuItem_Click);
            // 
            // keepCreationDateFromSourceVideoToolStripMenuItem
            // 
            this.keepCreationDateFromSourceVideoToolStripMenuItem.CheckOnClick = true;
            this.keepCreationDateFromSourceVideoToolStripMenuItem.Name = "keepCreationDateFromSourceVideoToolStripMenuItem";
            resources.ApplyResources(this.keepCreationDateFromSourceVideoToolStripMenuItem, "keepCreationDateFromSourceVideoToolStripMenuItem");
            this.keepCreationDateFromSourceVideoToolStripMenuItem.Click += new System.EventHandler(this.keepCreationDateFromSourceVideoToolStripMenuItem_Click);
            // 
            // keepLastModificationDateFromSourceVideoToolStripMenuItem
            // 
            this.keepLastModificationDateFromSourceVideoToolStripMenuItem.CheckOnClick = true;
            this.keepLastModificationDateFromSourceVideoToolStripMenuItem.Name = "keepLastModificationDateFromSourceVideoToolStripMenuItem";
            resources.ApplyResources(this.keepLastModificationDateFromSourceVideoToolStripMenuItem, "keepLastModificationDateFromSourceVideoToolStripMenuItem");
            this.keepLastModificationDateFromSourceVideoToolStripMenuItem.Click += new System.EventHandler(this.keepLastModificationDateFromSourceVideoToolStripMenuItem_Click);
            // 
            // showMessageOnSucessToolStripMenuItem
            // 
            this.showMessageOnSucessToolStripMenuItem.CheckOnClick = true;
            this.showMessageOnSucessToolStripMenuItem.Name = "showMessageOnSucessToolStripMenuItem";
            resources.ApplyResources(this.showMessageOnSucessToolStripMenuItem, "showMessageOnSucessToolStripMenuItem");
            // 
            // tiToolsPlayOutput
            // 
            this.tiToolsPlayOutput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compressVideosToolStripMenuItem,
            this.toolStripSeparator3,
            this.tiToolsOpenOutput,
            this.tiToolsExploreOutput,
            this.tiToolsCopyPathOutput,
            this.tiToolsVideoInfoOutput});
            this.tiToolsPlayOutput.Name = "tiToolsPlayOutput";
            resources.ApplyResources(this.tiToolsPlayOutput, "tiToolsPlayOutput");
            // 
            // compressVideosToolStripMenuItem
            // 
            resources.ApplyResources(this.compressVideosToolStripMenuItem, "compressVideosToolStripMenuItem");
            this.compressVideosToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.flash;
            this.compressVideosToolStripMenuItem.Name = "compressVideosToolStripMenuItem";
            this.compressVideosToolStripMenuItem.Click += new System.EventHandler(this.tsbConvert_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tiToolsOpenOutput
            // 
            resources.ApplyResources(this.tiToolsOpenOutput, "tiToolsOpenOutput");
            this.tiToolsOpenOutput.Name = "tiToolsOpenOutput";
            this.tiToolsOpenOutput.Click += new System.EventHandler(this.tiToolsOpenOutput_Click);
            // 
            // tiToolsExploreOutput
            // 
            resources.ApplyResources(this.tiToolsExploreOutput, "tiToolsExploreOutput");
            this.tiToolsExploreOutput.Name = "tiToolsExploreOutput";
            this.tiToolsExploreOutput.Click += new System.EventHandler(this.tiToolsExploreOutput_Click);
            // 
            // tiToolsCopyPathOutput
            // 
            resources.ApplyResources(this.tiToolsCopyPathOutput, "tiToolsCopyPathOutput");
            this.tiToolsCopyPathOutput.Name = "tiToolsCopyPathOutput";
            this.tiToolsCopyPathOutput.Click += new System.EventHandler(this.tiToolsCopyPathOutput_Click);
            // 
            // tiToolsVideoInfoOutput
            // 
            resources.ApplyResources(this.tiToolsVideoInfoOutput, "tiToolsVideoInfoOutput");
            this.tiToolsVideoInfoOutput.Name = "tiToolsVideoInfoOutput";
            this.tiToolsVideoInfoOutput.Click += new System.EventHandler(this.tiToolsVideoInfoOutput_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            resources.ApplyResources(this.downloadToolStripMenuItem, "downloadToolStripMenuItem");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languages1ToolStripMenuItem,
            this.languages2ToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // languages1ToolStripMenuItem
            // 
            this.languages1ToolStripMenuItem.Name = "languages1ToolStripMenuItem";
            resources.ApplyResources(this.languages1ToolStripMenuItem, "languages1ToolStripMenuItem");
            // 
            // languages2ToolStripMenuItem
            // 
            this.languages2ToolStripMenuItem.Name = "languages2ToolStripMenuItem";
            resources.ApplyResources(this.languages2ToolStripMenuItem, "languages2ToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpGuideToolStripMenuItem,
            this.toolStripMenuItem2,
            this.pleaseDonateMenuItem,
            this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem,
            this.checkForNewVersionEachWeekToolStripMenuItem,
            this.tiHelpFeedback,
            this.toolStripMenuItem1,
            this.followUsOnTwitterToolStripMenuItem,
            this.visit4dotsSoftwareWebsiteToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpGuideToolStripMenuItem
            // 
            this.helpGuideToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.help2;
            this.helpGuideToolStripMenuItem.Name = "helpGuideToolStripMenuItem";
            resources.ApplyResources(this.helpGuideToolStripMenuItem, "helpGuideToolStripMenuItem");
            this.helpGuideToolStripMenuItem.Click += new System.EventHandler(this.helpGuideToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // pleaseDonateMenuItem
            // 
            this.pleaseDonateMenuItem.BackColor = System.Drawing.Color.Gold;
            resources.ApplyResources(this.pleaseDonateMenuItem, "pleaseDonateMenuItem");
            this.pleaseDonateMenuItem.Name = "pleaseDonateMenuItem";
            this.pleaseDonateMenuItem.Click += new System.EventHandler(this.pleaseDonateMenuItem_Click);
            // 
            // dotsSoftwarePRODUCTCATALOGToolStripMenuItem
            // 
            resources.ApplyResources(this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem, "dotsSoftwarePRODUCTCATALOGToolStripMenuItem");
            this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem.ForeColor = System.Drawing.Color.DarkBlue;
            this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem.Name = "dotsSoftwarePRODUCTCATALOGToolStripMenuItem";
            this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem.Click += new System.EventHandler(this.dotsSoftwarePRODUCTCATALOGToolStripMenuItem_Click);
            // 
            // checkForNewVersionEachWeekToolStripMenuItem
            // 
            this.checkForNewVersionEachWeekToolStripMenuItem.CheckOnClick = true;
            this.checkForNewVersionEachWeekToolStripMenuItem.Name = "checkForNewVersionEachWeekToolStripMenuItem";
            resources.ApplyResources(this.checkForNewVersionEachWeekToolStripMenuItem, "checkForNewVersionEachWeekToolStripMenuItem");
            // 
            // tiHelpFeedback
            // 
            this.tiHelpFeedback.Image = global::VideoZoomerAndCropper.Properties.Resources.edit;
            this.tiHelpFeedback.Name = "tiHelpFeedback";
            resources.ApplyResources(this.tiHelpFeedback, "tiHelpFeedback");
            this.tiHelpFeedback.Click += new System.EventHandler(this.tiHelpFeedback_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // followUsOnTwitterToolStripMenuItem
            // 
            this.followUsOnTwitterToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.twitter_24;
            this.followUsOnTwitterToolStripMenuItem.Name = "followUsOnTwitterToolStripMenuItem";
            resources.ApplyResources(this.followUsOnTwitterToolStripMenuItem, "followUsOnTwitterToolStripMenuItem");
            this.followUsOnTwitterToolStripMenuItem.Click += new System.EventHandler(this.followUsOnTwitterToolStripMenuItem_Click);
            // 
            // visit4dotsSoftwareWebsiteToolStripMenuItem
            // 
            this.visit4dotsSoftwareWebsiteToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.earth;
            this.visit4dotsSoftwareWebsiteToolStripMenuItem.Name = "visit4dotsSoftwareWebsiteToolStripMenuItem";
            resources.ApplyResources(this.visit4dotsSoftwareWebsiteToolStripMenuItem, "visit4dotsSoftwareWebsiteToolStripMenuItem");
            this.visit4dotsSoftwareWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visit4dotsSoftwareWebsiteToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::VideoZoomerAndCropper.Properties.Resources.about;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // grpStartTime
            // 
            resources.ApplyResources(this.grpStartTime, "grpStartTime");
            this.grpStartTime.BackColor = System.Drawing.Color.LightGray;
            this.grpStartTime.Controls.Add(this.nudStartTime);
            this.grpStartTime.Controls.Add(this.btnStartUpdateImage);
            this.grpStartTime.Controls.Add(this.tbStartTime);
            this.grpStartTime.Controls.Add(this.label2);
            this.grpStartTime.Controls.Add(this.mskStart);
            this.grpStartTime.Name = "grpStartTime";
            this.grpStartTime.TabStop = false;
            this.grpStartTime.Paint += new System.Windows.Forms.PaintEventHandler(this.grpStartTime_Paint);
            // 
            // tbStartTime
            // 
            resources.ApplyResources(this.tbStartTime, "tbStartTime");
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbStartTime.Scroll += new System.EventHandler(this.tbStartTime_Scroll);
            this.tbStartTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbStartTime_MouseUp);
            // 
            // grpEndTime
            // 
            resources.ApplyResources(this.grpEndTime, "grpEndTime");
            this.grpEndTime.BackColor = System.Drawing.Color.LightGray;
            this.grpEndTime.Controls.Add(this.nudEndTime);
            this.grpEndTime.Controls.Add(this.btnEndUpdateImage);
            this.grpEndTime.Controls.Add(this.tbEndTime);
            this.grpEndTime.Controls.Add(this.label3);
            this.grpEndTime.Controls.Add(this.mskEnd);
            this.grpEndTime.Name = "grpEndTime";
            this.grpEndTime.TabStop = false;
            this.grpEndTime.Paint += new System.Windows.Forms.PaintEventHandler(this.grpEndTime_Paint);
            // 
            // tbEndTime
            // 
            resources.ApplyResources(this.tbEndTime, "tbEndTime");
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbEndTime.Scroll += new System.EventHandler(this.tbEndTime_Scroll);
            this.tbEndTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbEndTime_MouseUp);
            // 
            // pic7
            // 
            this.pic7.BackColor = System.Drawing.Color.Black;
            this.pic7.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic7, "pic7");
            this.pic7.Name = "pic7";
            this.pic7.TabStop = false;
            this.pic7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic8
            // 
            this.pic8.BackColor = System.Drawing.Color.Black;
            this.pic8.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic8, "pic8");
            this.pic8.Name = "pic8";
            this.pic8.TabStop = false;
            this.pic8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic6
            // 
            this.pic6.BackColor = System.Drawing.Color.Black;
            this.pic6.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic6, "pic6");
            this.pic6.Name = "pic6";
            this.pic6.TabStop = false;
            this.pic6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic5
            // 
            this.pic5.BackColor = System.Drawing.Color.Black;
            this.pic5.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic5, "pic5");
            this.pic5.Name = "pic5";
            this.pic5.TabStop = false;
            this.pic5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic4
            // 
            this.pic4.BackColor = System.Drawing.Color.Black;
            this.pic4.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            resources.ApplyResources(this.pic4, "pic4");
            this.pic4.Name = "pic4";
            this.pic4.TabStop = false;
            this.pic4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic3
            // 
            this.pic3.BackColor = System.Drawing.Color.Black;
            this.pic3.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic3, "pic3");
            this.pic3.Name = "pic3";
            this.pic3.TabStop = false;
            this.pic3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic2
            // 
            this.pic2.BackColor = System.Drawing.Color.Black;
            this.pic2.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            resources.ApplyResources(this.pic2, "pic2");
            this.pic2.Name = "pic2";
            this.pic2.TabStop = false;
            this.pic2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // pic1
            // 
            this.pic1.BackColor = System.Drawing.Color.Black;
            this.pic1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.pic1, "pic1");
            this.pic1.Name = "pic1";
            this.pic1.TabStop = false;
            this.pic1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove_2);
            this.pic1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // picMovie
            // 
            resources.ApplyResources(this.picMovie, "picMovie");
            this.picMovie.Name = "picMovie";
            this.picMovie.PicLogoVisible = false;
            this.picMovie.TabStop = false;
            this.picMovie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMovie_MouseDown);
            this.picMovie.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMovie_MouseMove);
            this.picMovie.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMovie_MouseUp);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnOptions);
            this.groupBox2.Controls.Add(this.rdCrop);
            this.groupBox2.Controls.Add(this.rdZoom);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnOptions
            // 
            resources.ApplyResources(this.btnOptions, "btnOptions");
            this.btnOptions.ForeColor = System.Drawing.Color.Black;
            this.btnOptions.Image = global::VideoZoomerAndCropper.Properties.Resources.preferences;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // rdCrop
            // 
            resources.ApplyResources(this.rdCrop, "rdCrop");
            this.rdCrop.Name = "rdCrop";
            this.rdCrop.UseVisualStyleBackColor = true;
            this.rdCrop.Click += new System.EventHandler(this.rdCrop_Click);
            // 
            // rdZoom
            // 
            resources.ApplyResources(this.rdZoom, "rdZoom");
            this.rdZoom.Checked = true;
            this.rdZoom.Name = "rdZoom";
            this.rdZoom.TabStop = true;
            this.rdZoom.UseVisualStyleBackColor = true;
            this.rdZoom.Click += new System.EventHandler(this.rdZoom_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Name = "label9";
            // 
            // picMovieB
            // 
            resources.ApplyResources(this.picMovieB, "picMovieB");
            this.picMovieB.Name = "picMovieB";
            this.picMovieB.PicLogoVisible = false;
            this.picMovieB.TabStop = false;
            this.picMovieB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMovieDest_MouseDown);
            this.picMovieB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMovieDest_MouseMove);
            this.picMovieB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMovieDest_MouseUp);
            // 
            // picB6
            // 
            this.picB6.BackColor = System.Drawing.Color.Black;
            this.picB6.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB6, "picB6");
            this.picB6.Name = "picB6";
            this.picB6.TabStop = false;
            this.picB6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB8
            // 
            this.picB8.BackColor = System.Drawing.Color.Black;
            this.picB8.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB8, "picB8");
            this.picB8.Name = "picB8";
            this.picB8.TabStop = false;
            this.picB8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB4
            // 
            this.picB4.BackColor = System.Drawing.Color.Black;
            this.picB4.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            resources.ApplyResources(this.picB4, "picB4");
            this.picB4.Name = "picB4";
            this.picB4.TabStop = false;
            this.picB4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB2
            // 
            this.picB2.BackColor = System.Drawing.Color.Black;
            this.picB2.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            resources.ApplyResources(this.picB2, "picB2");
            this.picB2.Name = "picB2";
            this.picB2.TabStop = false;
            this.picB2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB7
            // 
            this.picB7.BackColor = System.Drawing.Color.Black;
            this.picB7.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB7, "picB7");
            this.picB7.Name = "picB7";
            this.picB7.TabStop = false;
            this.picB7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB5
            // 
            this.picB5.BackColor = System.Drawing.Color.Black;
            this.picB5.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB5, "picB5");
            this.picB5.Name = "picB5";
            this.picB5.TabStop = false;
            this.picB5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB3
            // 
            this.picB3.BackColor = System.Drawing.Color.Black;
            this.picB3.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB3, "picB3");
            this.picB3.Name = "picB3";
            this.picB3.TabStop = false;
            this.picB3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // picB1
            // 
            this.picB1.BackColor = System.Drawing.Color.Black;
            this.picB1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            resources.ApplyResources(this.picB1, "picB1");
            this.picB1.Name = "picB1";
            this.picB1.TabStop = false;
            this.picB1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseDown);
            this.picB1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseMove);
            this.picB1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picB1_MouseUp);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnSelectEntireAreaB);
            this.groupBox3.Controls.Add(this.nudHeightB);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.nudWidthB);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.nudYB);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.nudXB);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnSelectEntireAreaB
            // 
            resources.ApplyResources(this.btnSelectEntireAreaB, "btnSelectEntireAreaB");
            this.btnSelectEntireAreaB.ForeColor = System.Drawing.Color.Black;
            this.btnSelectEntireAreaB.Name = "btnSelectEntireAreaB";
            this.btnSelectEntireAreaB.UseVisualStyleBackColor = true;
            this.btnSelectEntireAreaB.Click += new System.EventHandler(this.btnSelectEntireAreaB_Click);
            // 
            // nudHeightB
            // 
            resources.ApplyResources(this.nudHeightB, "nudHeightB");
            this.nudHeightB.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudHeightB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeightB.Name = "nudHeightB";
            this.nudHeightB.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudHeightB.ValueChanged += new System.EventHandler(this.nudHeightB_ValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Name = "label10";
            // 
            // nudWidthB
            // 
            resources.ApplyResources(this.nudWidthB, "nudWidthB");
            this.nudWidthB.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudWidthB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidthB.Name = "nudWidthB";
            this.nudWidthB.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudWidthB.ValueChanged += new System.EventHandler(this.nudWidthB_ValueChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.DarkBlue;
            this.label12.Name = "label12";
            // 
            // nudYB
            // 
            resources.ApplyResources(this.nudYB, "nudYB");
            this.nudYB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudYB.Name = "nudYB";
            this.nudYB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudYB.ValueChanged += new System.EventHandler(this.nudYB_ValueChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Name = "label13";
            // 
            // nudXB
            // 
            resources.ApplyResources(this.nudXB, "nudXB");
            this.nudXB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudXB.Name = "nudXB";
            this.nudXB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudXB.ValueChanged += new System.EventHandler(this.nudXB_ValueChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.DarkBlue;
            this.label14.Name = "label14";
            // 
            // chkKeepAspectRatio
            // 
            resources.ApplyResources(this.chkKeepAspectRatio, "chkKeepAspectRatio");
            this.chkKeepAspectRatio.BackColor = System.Drawing.Color.Transparent;
            this.chkKeepAspectRatio.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkKeepAspectRatio.Name = "chkKeepAspectRatio";
            this.chkKeepAspectRatio.UseVisualStyleBackColor = false;
            this.chkKeepAspectRatio.Click += new System.EventHandler(this.chkKeepAspectRatio_Click);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.chkKeepAspectRatio);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.picB6);
            this.Controls.Add(this.picB8);
            this.Controls.Add(this.picB4);
            this.Controls.Add(this.picB2);
            this.Controls.Add(this.picB7);
            this.Controls.Add(this.picB5);
            this.Controls.Add(this.picB3);
            this.Controls.Add(this.picB1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.picMovieB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grpStartTime);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpEndTime);
            this.Controls.Add(this.btnRemoveClip);
            this.Controls.Add(this.btnAddClip);
            this.Controls.Add(this.cmbClip);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pic7);
            this.Controls.Add(this.pic8);
            this.Controls.Add(this.pic6);
            this.Controls.Add(this.pic5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pic4);
            this.Controls.Add(this.pic3);
            this.Controls.Add(this.pic2);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.picMovie);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.ShowInTaskbar = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMain_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmMain_DragOver);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nudEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartTime)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpStartTime.ResumeLayout(false);
            this.grpStartTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStartTime)).EndInit();
            this.grpEndTime.ResumeLayout(false);
            this.grpEndTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMovie)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMovieB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public PicHandle pic1;
        public PicHandle pic2;
        public PicHandle pic3;
        public PicHandle pic4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbZoomCrop;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiHelpFeedback;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem followUsOnTwitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visit4dotsSoftwareWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tsbShare;
        private System.Windows.Forms.ToolStripMenuItem tsiFacebook;
        private System.Windows.Forms.ToolStripMenuItem tsiTwitter;
        private System.Windows.Forms.ToolStripMenuItem tsiGooglePlus;
        private System.Windows.Forms.ToolStripMenuItem tsiLinkedIn;
        private System.Windows.Forms.ToolStripMenuItem tsiEmail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripSplitButton tsdbOpen;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.MaskedTextBox mskStart;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.MaskedTextBox mskEnd;
        private PreviewTrackBar tbStartTime;
        private PreviewTrackBar tbEndTime;
        private System.Windows.Forms.Button btnStartUpdateImage;
        private System.Windows.Forms.Button btnEndUpdateImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        public picLogo picMovie;
        public System.Windows.Forms.NumericUpDown nudHeight;
        public System.Windows.Forms.NumericUpDown nudWidth;
        public System.Windows.Forms.NumericUpDown nudY;
        public System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlblVideoFile;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsedTime;
        private System.Windows.Forms.NumericUpDown nudStartTime;
        private System.Windows.Forms.NumericUpDown nudEndTime;
        public System.ComponentModel.BackgroundWorker bwRemoveLogo;
        private System.Windows.Forms.ToolStripMenuItem languages1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languages2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pleaseDonateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dotsSoftwarePRODUCTCATALOGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiToolsPlayOutput;
        private System.Windows.Forms.ToolStripMenuItem compressVideosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tiToolsOpenOutput;
        private System.Windows.Forms.ToolStripMenuItem tiToolsExploreOutput;
        private System.Windows.Forms.ToolStripMenuItem tiToolsCopyPathOutput;
        private System.Windows.Forms.ToolStripMenuItem tiToolsVideoInfoOutput;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem whenFinishedToolStripMenuItem;
        private System.Windows.Forms.Timer timJoinVideos;
        public PicHandle pic5;
        public PicHandle pic6;
        public PicHandle pic8;
        public PicHandle pic7;
        private System.Windows.Forms.ToolStripStatusLabel tslDuration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbClip;
        private System.Windows.Forms.Button btnAddClip;
        private System.Windows.Forms.Button btnRemoveClip;
        private System.Windows.Forms.GroupBox grpStartTime;
        private System.Windows.Forms.GroupBox grpEndTime;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem useFFMPEG64bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useFFMPEG32bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyEXIFInformationFromSourceVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepCreationDateFromSourceVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepLastModificationDateFromSourceVideoToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdCrop;
        private System.Windows.Forms.RadioButton rdZoom;
        private System.Windows.Forms.ToolStripButton tsbPreviewImage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public picLogo picMovieB;
        public PicHandle picB6;
        public PicHandle picB8;
        public PicHandle picB4;
        public PicHandle picB2;
        public PicHandle picB7;
        public PicHandle picB5;
        public PicHandle picB3;
        public PicHandle picB1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.NumericUpDown nudHeightB;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.NumericUpDown nudWidthB;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.NumericUpDown nudYB;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.NumericUpDown nudXB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkKeepAspectRatio;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.ToolStripMenuItem zoomAndCropOptionsToolStripMenuItem;
        private System.Windows.Forms.Button btnSelectEntireArea;
        private System.Windows.Forms.Button btnSelectEntireAreaB;
        private System.Windows.Forms.ToolStripMenuItem checkForNewVersionEachWeekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMessageOnSucessToolStripMenuItem;
    }
}
