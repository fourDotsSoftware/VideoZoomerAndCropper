using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace VideoZoomerAndCropper
{
    public partial class frmMain : VideoZoomerAndCropper.CustomForm
    {
        public static frmMain Instance = null;

        public VideoScaler VideoScaler = null;
        public string DurationStr = "";
        public int DurationMsecs = 0;

        public string errstr = "";

        public Process psFFMpeg = null;
        BackgroundWorker bwConvert = new BackgroundWorker();

        public bool ConversionStopped = false;
        public bool ConversionStarted = false;
        public bool ConversionPaused = false;
        public int ConversionProgressTime = 0;

        public int CompletedSecs = -1;

        public string LastFFMpegOutput = "";

        int beforeCompletedSecs = 0;

        bool time_str_found = false;

        string last_line = "";

        public string ExplicitOutputFilepath = "";
        public bool OutputFileActionRepeat = false;

        private string _FirstOutputFile = "";

        public string FirstOutputFile
        {
            get { return _FirstOutputFile; }
            set
            {
                if (value != string.Empty)
                {
                    tiToolsCopyPathOutput.Enabled = true;
                    tiToolsExploreOutput.Enabled = true;
                    tiToolsOpenOutput.Enabled = true;
                    tiToolsVideoInfoOutput.Enabled = true;

                    _FirstOutputFile = value;
                }
            }
        }

        public string Filepath = "";

        public List<VWRClip> VWRClips = new List<VWRClip>();

        public frmMain()
        {
            InitializeComponent();

            Instance = this;
        }

        PictureBox ActivePic = null;
        bool PicMouseDown = false;

        PictureBox ActivePicB = null;
        bool PicMouseDownB = false;

        private void pic1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pic1_MouseDown(object sender, MouseEventArgs e)
        {
            ActivePic = (PictureBox)sender;
            PicMouseDown = true;

            // picMovie.Focus();
        }

        private int picMX = 0;
        private int picMY = 0;

        private int picMXB = 0;
        private int picMYB = 0;

        private void picMovie_MouseMove(object sender, MouseEventArgs e)
        {
            if (!picMoveRect)
            {
                Rectangle rect = new Rectangle(pic1.Left, pic1.Top, pic2.Left - pic1.Left, pic3.Top - pic2.Top);

                Point p = new Point(e.X, e.Y);

                if (rect.Contains(p))
                {
                    picMovie.Cursor = Cursors.SizeAll;

                    picInRect = true;
                }
                else
                {
                    picMovie.Cursor = null;

                    picInRect = false;
                }
            }
            else
            {
                nudX.Value = Math.Min(nudX.Maximum, Math.Max(nudX.Minimum, nudX.Value + VideoScaler.ScalePosPicToVideo(e.X) - picMX));
                nudY.Value = Math.Min(nudY.Maximum, Math.Max(nudY.Minimum, nudY.Value + VideoScaler.ScalePosPicToVideo(e.Y) - picMY));

                picMX = VideoScaler.ScalePosPicToVideo(e.X);
                picMY = VideoScaler.ScalePosPicToVideo(e.Y);
            }
                
        }

        private void FillPicRectangle()
        {
            picMovie.Invalidate();
            /*
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //picMovie.BackgroundImage = picMovie.Image;

            //handle size
            int hdsize=(pic1.Width)/2;

            Graphics g = picMovie.CreateGraphics();
            
            //g.Clear(picMovie.BackColor);
            
            g.DrawImage(Properties.Resources.DSC00059, 0, 0, picMovie.Width, picMovie.Height);

            System.Drawing.Drawing2D.HatchBrush hb=new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,Color.Red);

            g.FillRectangle(hb, pic1.Left -picMovie.Left + hdsize, pic1.Top -picMovie.Top + hdsize, pic2.Left - pic1.Left, pic3.Top - pic2.Top);

            picMovie.Update();*/
        }

        private void picMovie_MouseUp(object sender, MouseEventArgs e)
        {
            PicMouseDown = false;
            picMovie.Cursor = null;
            picMoveRect = false;

            nudX_ValueChanged(null, null);
            nudY_ValueChanged(null, null);

            picMovie.Invalidate();
        }

        private void pic1_MouseUp(object sender, MouseEventArgs e)
        {
            PicMouseDown = false;

            //ActivePic = null;

            UpdatePicFromControls();
        }

        private void pic1_MouseMove_1(object sender, MouseEventArgs e)
        {

        }

        private void pic1_MouseMove_2(object sender, MouseEventArgs e)
        {
            if (!PicMouseDown) return;

            //MessageBox.Show("left=" + ActivePic.Left.ToString() + " x=" + e.X.ToString());          


            if ((ActivePic.Left + e.X) > VideoScaler.PicMaxWidth)
            {
                return;
            }

            if ((ActivePic.Top + e.Y) > VideoScaler.PicMaxHeight)
            {
                return;
            }

            ActivePic.Left += e.X;
            ActivePic.Top += e.Y;

            if (ActivePic == pic1)
            {
                pic2.Top = pic1.Top;
                pic5.Top = pic1.Top;
                pic4.Left = pic1.Left;
                pic8.Left = pic1.Left;
            }
            else if (ActivePic == pic2)
            {
                pic1.Top = pic2.Top;
                pic5.Top = pic2.Top;
                pic3.Left = pic2.Left;
                pic6.Left = pic2.Left;
            }
            else if (ActivePic == pic3)
            {
                pic4.Top = pic3.Top;
                pic2.Left = pic3.Left;

                pic6.Left = pic3.Left;
                pic7.Top = pic3.Top;

            }
            else if (ActivePic == pic4)
            {
                pic1.Left = pic4.Left;
                pic3.Top = pic4.Top;
                pic8.Left = pic4.Left;
                pic7.Top = pic4.Top;
            }
            else if (ActivePic == pic5)
            {
                pic1.Top = pic5.Top;
                pic2.Top = pic5.Top;
            }
            else if (ActivePic == pic6)
            {
                pic2.Left = pic6.Left;
                pic3.Left = pic6.Left;
            }
            else if (ActivePic == pic7)
            {
                pic3.Top = pic7.Top;
                pic4.Top = pic7.Top;
            }
            else if (ActivePic == pic8)
            {
                pic1.Left = pic8.Left;
                pic4.Left = pic8.Left;
            }

            pic5.Left = pic1.Left + (int)((pic2.Left - pic1.Left) / 2);
            pic7.Left = pic4.Left + (int)((pic3.Left - pic4.Left) / 2);
            pic6.Top = pic2.Top + (int)((pic3.Top - pic2.Top) / 2);
            pic8.Top = pic1.Top + (int)((pic4.Top - pic1.Top) / 2);

            pic1.Left = Math.Max(1, Math.Min(pic1.Left, VideoScaler.PicMaxWidth));
            pic2.Left = Math.Max(1, Math.Min(pic2.Left, VideoScaler.PicMaxWidth));
            pic3.Left = Math.Max(1, Math.Min(pic3.Left, VideoScaler.PicMaxWidth));
            pic4.Left = Math.Max(1, Math.Min(pic4.Left, VideoScaler.PicMaxWidth));

            pic1.Top = Math.Max(1, Math.Min(pic1.Top, VideoScaler.PicMaxHeight));
            pic2.Top = Math.Max(1, Math.Min(pic2.Top, VideoScaler.PicMaxHeight));
            pic3.Top = Math.Max(1, Math.Min(pic3.Top, VideoScaler.PicMaxHeight));
            pic4.Top = Math.Max(1, Math.Min(pic4.Top, VideoScaler.PicMaxHeight));

            FillPicRectangle();
            UpdatePosAndSize();
        }

        private bool picMoveRect = false;
        private bool picInRect = false;

        private bool picMoveRectB = false;
        private bool picInRectB = false;

        private void picMovie_MouseDown(object sender, MouseEventArgs e)
        {
            if (picInRect)
            {
                picMoveRect = true;

                picMX = VideoScaler.ScalePosPicToVideo(e.X);
                picMY = VideoScaler.ScalePosPicToVideo(e.Y);

                /*
                picMX = e.X;
                picMY = e.Y;
                */
            }
        }

        #region Share

        private void tsiFacebook_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareFacebook();
        }

        private void tsiTwitter_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareTwitter();
        }

        private void tsiGooglePlus_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareGooglePlus();
        }

        private void tsiLinkedIn_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareLinkedIn();
        }

        private void tsiEmail_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareEmail();
        }

        #endregion 

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetEnabled(bool enabled)
        {
            tsbZoomCrop.Enabled = enabled;
            compressVideosToolStripMenuItem.Enabled = enabled;
            tsbPreviewImage.Enabled = enabled;
        }

        private void tsdbOpen_ButtonClick(object sender, EventArgs e)
        {
            openFileDialog1.Filter = Module.OpenFilesFilter;

            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AddFile(openFileDialog1.FileName);

            }
        }

        private void AddFile(string filepath)
        {
            VideoScaler = new VideoZoomerAndCropper.VideoScaler(filepath);

            picMovie.PicLogoVisible = true;
            picMovieB.PicLogoVisible = true;
            
            Filepath = filepath;
            UpdateImg("00:00:00.00");
            RecentFilesHelper.AddRecentFile(filepath);
            tlblVideoFile.Text = filepath;

            tbStartTime.Value = 0;
            tbEndTime.Value = 0;

            tbStartTime.Maximum = DurationMsecs;
            tbEndTime.Maximum = DurationMsecs;

            nudStartTime.Value = 0;
            nudEndTime.Value = 0;
            nudStartTime.Maximum = DurationMsecs;
            nudEndTime.Maximum = DurationMsecs;

            nudEndTime.Value = DurationMsecs;

            tbEndTime.Value = DurationMsecs;

            mskStart.Text = "00:00:00.00";
            mskEnd.Text = DurationStr;

            tslDuration.Text = DurationStr;

            cmbClip.Items.Clear();

            VWRClips.Clear();

            VWRClip vwr = new VWRClip();
            vwr.X = (int)nudX.Value;
            vwr.Y = (int)nudY.Value;
            vwr.Width = (int)nudWidth.Value;
            vwr.Height = (int)nudHeight.Value;

            vwr.XB = (int)nudX.Value;
            vwr.YB = (int)nudY.Value;
            vwr.WidthB = (int)nudWidth.Value;
            vwr.HeightB = (int)nudHeight.Value;

            vwr.StartTime = tbStartTime.Value;
            vwr.EndTime = tbEndTime.Value;

            VWRClips.Add(vwr);

            cmbClip.Items.Add(TranslateHelper.Translate("Clip") + " #" + (cmbClip.Items.Count + 1).ToString());

            cmbClip.SelectedIndex = 0;

            LastVWRSelectedIndex = 0;

            SetEnabled(true);

        }

        private void UpdateImg(string timepos)
        {
            if (!picMovie.PicLogoVisible) return;

            VideoThumbnail vth = new VideoThumbnail(Filepath, timepos);
            picMovie.Thumbnail = vth.ThumbnailImage;

            picMovieB.Thumbnail = vth.ThumbnailImage;

            VideoScaler.UpdateScale();
            picMovie.UpdateImg();
            picMovie.Invalidate();

            picMovieB.UpdateImg();
            picMovieB.Invalidate();

        }

        private void tsdbOpen_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            AddFile(e.ClickedItem.Text);
        }

        #region Help Menu

        private void helpGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Module.HelpURL);
        }

        private void tiHelpFeedback_Click(object sender, EventArgs e)
        {
            /*
            frmUninstallQuestionnaire f = new frmUninstallQuestionnaire(false);
            f.ShowDialog();
            */

            System.Diagnostics.Process.Start("https://www.4dots-software.com/support/bugfeature.php?app=" + System.Web.HttpUtility.UrlEncode(Module.ShortApplicationTitle));
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout2 f = new frmAbout2();
            f.ShowDialog();
        }

        private void followUsOnTwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.twitter.com/4dotsSoftware");
        }

        private void visit4dotsSoftwareWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com");
        }
        #endregion

        #region Localization

        private void AddLanguageMenuItems()
        {
            for (int k = 0; k < frmLanguage.LangCodes.Count; k++)
            {
                ToolStripMenuItem ti = new ToolStripMenuItem();
                ti.Text = frmLanguage.LangDesc[k];
                ti.Tag = frmLanguage.LangCodes[k];
                ti.Image = frmLanguage.LangImg[k];

                if (Properties.Settings.Default.Language == frmLanguage.LangCodes[k])
                {
                    ti.Checked = true;
                }

                ti.Click += new EventHandler(tiLang_Click);

                if (k < 25)
                {
                    languages1ToolStripMenuItem.DropDownItems.Add(ti);
                }
                else
                {
                    languages2ToolStripMenuItem.DropDownItems.Add(ti);
                }

                //languageToolStripMenuItem.DropDownItems.Add(ti);
            }
        }

        void tiLang_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ti = (ToolStripMenuItem)sender;
            string langcode = ti.Tag.ToString();
            ChangeLanguage(langcode);

            //for (int k = 0; k < languageToolStripMenuItem.DropDownItems.Count; k++)
            for (int k = 0; k < languages1ToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem til = (ToolStripMenuItem)languages1ToolStripMenuItem.DropDownItems[k];
                if (til == ti)
                {
                    til.Checked = true;
                }
                else
                {
                    til.Checked = false;
                }
            }

            for (int k = 0; k < languages2ToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem til = (ToolStripMenuItem)languages2ToolStripMenuItem.DropDownItems[k];
                if (til == ti)
                {
                    til.Checked = true;
                }
                else
                {
                    til.Checked = false;
                }
            }
        }

        private bool InChangeLanguage = false;

        private void ChangeLanguage(string language_code)
        {
            try
            {
                InChangeLanguage = true;

                Properties.Settings.Default.Language = language_code;
                frmLanguage.SetLanguage();

                Properties.Settings.Default.Save();
                Module.ShowMessage("Please restart the application !");
                Application.Exit();
                return;

                bool maximized = (this.WindowState == FormWindowState.Maximized);
                this.WindowState = FormWindowState.Normal;

                /*
                RegistryKey key = Registry.CurrentUser;
                RegistryKey key2 = Registry.CurrentUser;

                try
                {
                    key = key.OpenSubKey("Software\\4dots Software", true);

                    if (key == null)
                    {
                        key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\4dots Software");
                    }

                    key2 = key.OpenSubKey(frmLanguage.RegKeyName, true);

                    if (key2 == null)
                    {
                        key2 = key.CreateSubKey(frmLanguage.RegKeyName);
                    }

                    key = key2;

                    //key.SetValue("Language", language_code);
                    key.SetValue("Menu Item Caption", TranslateHelper.Translate("Compress with Simple Video Compress"));
                }
                catch (Exception ex)
                {
                    Module.ShowError(ex);
                    return;
                }
                finally
                {
                    key.Close();
                    key2.Close();
                }
                */

                //1SaveSizeLocation();

                SaveSizeLocation();

                this.Controls.Clear();

                InitializeComponent();

                SetupOnLoad();

                if (maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }

                this.ResumeLayout(true);

            }
            finally
            {
                InChangeLanguage = false;
            }
        }

        #endregion

        #region Set Size and Location

        private void SaveSizeLocation()
        {
            Properties.Settings.Default.Maximized = (this.WindowState == FormWindowState.Maximized);
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Save();

        }

        private void AdjustSizeLocation()
        {
            if (Properties.Settings.Default.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {

                if (Properties.Settings.Default.Width == -1)
                {
                    this.CenterToScreen();
                    return;
                }
                else
                {
                    this.Width = Properties.Settings.Default.Width;
                }
                if (Properties.Settings.Default.Height != 0)
                {
                    this.Height = Properties.Settings.Default.Height;
                }

                if (Properties.Settings.Default.Left != -1)
                {
                    this.Left = Properties.Settings.Default.Left;
                }

                if (Properties.Settings.Default.Top != -1)
                {
                    this.Top = Properties.Settings.Default.Top;
                }

                if (this.Width < 300)
                {
                    this.Width = 300;
                }

                if (this.Height < 300)
                {
                    this.Height = 300;
                }

                if (this.Left < 0)
                {
                    this.Left = 0;
                }

                if (this.Top < 0)
                {
                    this.Top = 0;
                }
            }
        }

        #endregion

        private void SetupOnLoad()
        {
            pic1.Parent = picMovie;
            pic2.Parent = picMovie;
            pic3.Parent = picMovie;
            pic4.Parent = picMovie;

            pic5.Parent = picMovie;
            pic6.Parent = picMovie;
            pic7.Parent = picMovie;
            pic8.Parent = picMovie;

            pic5.Cursor = Cursors.SizeNS;
            pic6.Cursor = Cursors.SizeWE;
            pic7.Cursor = Cursors.SizeNS;
            pic8.Cursor = Cursors.SizeWE;

            /////////////////////////////

            picB1.Parent = picMovieB;
            picB2.Parent = picMovieB;
            picB3.Parent = picMovieB;
            picB4.Parent = picMovieB;

            picB5.Parent = picMovieB;
            picB6.Parent = picMovieB;
            picB7.Parent = picMovieB;
            picB8.Parent = picMovieB;

            picB5.Cursor = Cursors.SizeNS;
            picB6.Cursor = Cursors.SizeWE;
            picB7.Cursor = Cursors.SizeNS;
            picB8.Cursor = Cursors.SizeWE;

            /////////////////////////////

            //this.Icon = Properties.Resources.video_logo_remover_48;

            this.Text = Module.ApplicationTitle;

            AddLanguageMenuItems();

            DownloadSuggestionsHelper ds = new DownloadSuggestionsHelper();
            ds.SetupDownloadMenuItems(downloadToolStripMenuItem);

            AdjustSizeLocation();

            UpdatePicFromControls();

            UpdatePicFromControlsB();

            RecentFilesHelper.FillMenuRecentFile();

            //3enterLicenseKeyToolStripMenuItem.Visible = frmPurchase.RenMove;
            //3buyToolStripMenuItem.Visible = frmPurchase.RenMove;

            SetEnabled(false);

            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Shutdown"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Sleep"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Hibernate"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Logoff"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Lock Workstation"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Restart"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Exit Application"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Explore Output Video"));
            whenFinishedToolStripMenuItem.DropDownItems.Add(TranslateHelper.Translate("Do Nothing"));

            foreach (ToolStripMenuItem ti in whenFinishedToolStripMenuItem.DropDownItems)
            {
                ti.Click += tiWhenFinished_Click;
            }

            if (Properties.Settings.Default.WhenFinishedIndex != -1)
            {
                ToolStripMenuItem ti = (ToolStripMenuItem)whenFinishedToolStripMenuItem.DropDownItems[Properties.Settings.Default.WhenFinishedIndex];
                ti.Checked = true;
            }

            ///3
            
            /*
            if (Properties.Settings.Default.Price != string.Empty && !buyApplicationToolStripMenuItem.Text.EndsWith(Properties.Settings.Default.Price))
            {
                buyApplicationToolStripMenuItem.Text = buyApplicationToolStripMenuItem.Text + " " + Properties.Settings.Default.Price;
            }
            */

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            if (Properties.Settings.Default.FFMPEG64Bit == 0)
            {
                if (Module.IsWindows64Bit)
                {
                    useFFMPEG64bitToolStripMenuItem_Click(null, null);
                }
                else
                {
                    useFFMPEG32bitToolStripMenuItem_Click(null, null);
                }
            }
            else if (Properties.Settings.Default.FFMPEG64Bit == 1)
            {
                useFFMPEG64bitToolStripMenuItem_Click(null, null);
            }
            else if (Properties.Settings.Default.FFMPEG64Bit == 2)
            {
                useFFMPEG32bitToolStripMenuItem_Click(null, null);
            }

            copyEXIFInformationFromSourceVideoToolStripMenuItem.Checked = Properties.Settings.Default.CopyEXIF;

            keepCreationDateFromSourceVideoToolStripMenuItem.Checked = Properties.Settings.Default.KeepCreationDate;

            keepLastModificationDateFromSourceVideoToolStripMenuItem.Checked = Properties.Settings.Default.KeepLastModDate;

            grpStartTime.MouseUp += tbStartTime_MouseUp;

            grpEndTime.MouseUp += tbEndTime_MouseUp;

            chkKeepAspectRatio.Checked = Properties.Settings.Default.KeepAspectRatio;

            rdCrop.Checked = Properties.Settings.Default.Crop;

            checkForNewVersionEachWeekToolStripMenuItem.Checked = Properties.Settings.Default.CheckWeek;

            showMessageOnSucessToolStripMenuItem.Checked = Properties.Settings.Default.ShowMsgOnSuccess;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bwConvert.DoWork += bwConvert_DoWork;
            bwConvert.RunWorkerCompleted += bwConvert_RunWorkerCompleted;
            bwConvert.ProgressChanged += bwConvert_ProgressChanged;
            bwConvert.WorkerReportsProgress = true;
            bwConvert.WorkerSupportsCancellation = true;

            SetupOnLoad();

            if (Properties.Settings.Default.CheckWeek)
            {
                UpdateHelper.InitializeCheckVersionWeek();
            }

            if (Module.args != null && Module.args.Length > 0 && System.IO.File.Exists(Module.args[0]))
            {
                AddFile(Module.args[0]);
            }

            ResizeControls();
        }


        public void UpdatePicFromControls()
        {
            if (VideoScaler == null) return;

            nudX_ValueChanged(null, null);
            nudY_ValueChanged(null, null);
            nudWidth_ValueChanged(null, null);
            nudHeight_ValueChanged(null, null);
        }

        public bool InUpdatePosAndSize = false;

        private void UpdatePosAndSize()
        {
            try
            {
                InUpdatePosAndSize = true;

                if (VideoScaler == null) return;

                int oldx = (int)nudX.Value;
                int oldy = (int)nudY.Value;
                int oldh = (int)nudHeight.Value;
                int oldw = (int)nudWidth.Value;

                int w = VideoScaler.ScalePosPicToVideo(pic2.Left - pic1.Left);
                int h = VideoScaler.ScalePosPicToVideo(pic4.Top - pic1.Top);

                pic1.Left = Math.Max(1, Math.Min(pic1.Left, VideoScaler.PicMaxWidth));
                pic2.Left = Math.Max(1, Math.Min(pic2.Left, VideoScaler.PicMaxWidth));
                pic3.Left = Math.Max(1, Math.Min(pic3.Left, VideoScaler.PicMaxWidth));
                pic4.Left = Math.Max(1, Math.Min(pic4.Left, VideoScaler.PicMaxWidth));

                pic1.Top = Math.Max(1, Math.Min(pic1.Top, VideoScaler.PicMaxHeight));
                pic2.Top = Math.Max(1, Math.Min(pic2.Top, VideoScaler.PicMaxHeight));
                pic3.Top = Math.Max(1, Math.Min(pic3.Top, VideoScaler.PicMaxHeight));
                pic4.Top = Math.Max(1, Math.Min(pic4.Top, VideoScaler.PicMaxHeight));

                w = VideoScaler.ScalePosPicToVideo(pic2.Left - pic1.Left);
                h = VideoScaler.ScalePosPicToVideo(pic4.Top - pic1.Top);

                nudX.Minimum = 1;
                nudX.Maximum = VideoScaler.Width - w;

                nudY.Minimum = 1;
                nudY.Maximum = VideoScaler.Height - h;

                nudWidth.Minimum = 1;
                nudWidth.Maximum = VideoScaler.Width - VideoScaler.ScalePosPicToVideo(pic1.Left);

                nudHeight.Minimum = 1;
                nudHeight.Maximum = VideoScaler.Height - VideoScaler.ScalePosPicToVideo(pic1.Top);

                int nx = 0;
                try
                {
                    nx = VideoScaler.ScalePosPicToVideo(pic1.Left);

                    if (nx < 0)
                    {
                        nx = 0;
                    }
                    else if (nx > VideoScaler.Width - w)
                    {
                        nx = VideoScaler.Width - w;
                    }
                    else if (nx > nudX.Maximum)
                    {
                        nx = (int)nudX.Maximum;
                    }

                    nudX.Value = nx;
                }
                catch
                {
                    nudX.Value = oldx;


                }

                int ny = 0;
                try
                {
                    ny = VideoScaler.ScalePosPicToVideo(pic1.Top);

                    if (ny < 0)
                    {
                        ny = 0;
                    }
                    else if (ny > VideoScaler.Height - h)
                    {
                        ny = VideoScaler.Height - h;
                    }
                    else if (ny > nudY.Maximum)
                    {
                        ny = (int)nudY.Maximum;
                    }

                    nudY.Value = ny;
                }
                catch
                {
                    nudY.Value = oldy;
                }

                try
                {
                    int nw = VideoScaler.ScalePosPicToVideo(pic2.Left - pic1.Left);
                    if (nw < 1)
                    {
                        nw = 1;
                    }
                    else if (nw + nx > VideoScaler.Width)
                    {
                        nw = VideoScaler.Width - nx;
                    }
                    else if (nw > nudWidth.Maximum)
                    {
                        nw = (int)nudWidth.Maximum;
                    }

                    nudWidth.Value = nw;
                }
                catch
                {
                    nudWidth.Value = oldw;
                }

                try
                {
                    int nh = VideoScaler.ScalePosPicToVideo(pic4.Top - pic1.Top);

                    if (nh < 1)
                    {
                        nh = 1;
                    }
                    else if (nh + ny > VideoScaler.Height)
                    {
                        nh = VideoScaler.Height - ny;
                    }
                    else if (nh > nudHeight.Maximum)
                    {
                        nh = (int)nudHeight.Maximum;
                    }

                    nudHeight.Value = nh;
                }
                catch
                {
                    nudHeight.Value = oldh;
                }
            }
            finally
            {
                InUpdatePosAndSize = false;
            }
        }

        private void UpdatePos()
        {
            try
            {
                InUpdatePosAndSize = true;

                if (VideoScaler == null) return;

                int oldx = (int)nudX.Value;
                int oldy = (int)nudY.Value;
                int oldh = (int)nudHeight.Value;
                int oldw = (int)nudWidth.Value;

                int w = VideoScaler.ScalePosPicToVideo(pic2.Left - pic1.Left);
                int h = VideoScaler.ScalePosPicToVideo(pic4.Top - pic1.Top);

                int nx = 0;
                try
                {
                    nx = VideoScaler.ScalePosPicToVideo(pic1.Left);

                    if (nx < 0)
                    {
                        nx = 0;
                    }
                    else if (nx > VideoScaler.Width - w)
                    {
                        nx = VideoScaler.Width - w;
                    }
                    else if (nx > nudX.Maximum)
                    {
                        nx = (int)nudX.Maximum;
                    }

                    nudX.Value = nx;
                }
                catch
                {
                    nudX.Value = oldx;


                }

                int ny = 0;
                try
                {
                    ny = VideoScaler.ScalePosPicToVideo(pic1.Top);

                    if (ny < 0)
                    {
                        ny = 0;
                    }
                    else if (ny > VideoScaler.Height - h)
                    {
                        ny = VideoScaler.Height - h;
                    }
                    else if (ny > nudY.Maximum)
                    {
                        ny = (int)nudY.Maximum;
                    }

                    nudY.Value = ny;
                }
                catch
                {
                    nudY.Value = oldy;
                }
            }
            finally
            {
                InUpdatePosAndSize = false;
            }
        }

        private void UpdatePosB()
        {
            try
            {
                InUpdatePosAndSize = true;

                if (VideoScaler == null) return;

                int oldx = (int)nudXB.Value;
                int oldy = (int)nudYB.Value;
                int oldh = (int)nudHeightB.Value;
                int oldw = (int)nudWidthB.Value;

                int w = VideoScaler.ScalePosPicToVideo(picB2.Left - picB1.Left);
                int h = VideoScaler.ScalePosPicToVideo(picB4.Top - picB1.Top);

                int nx = 0;
                try
                {
                    nx = VideoScaler.ScalePosPicToVideo(picB1.Left);

                    if (nx < 0)
                    {
                        nx = 0;
                    }
                    else if (nx > VideoScaler.Width - w)
                    {
                        nx = VideoScaler.Width - w;
                    }
                    else if (nx > nudXB.Maximum)
                    {
                        nx = (int)nudXB.Maximum;
                    }

                    nudXB.Value = nx;
                }
                catch
                {
                    nudXB.Value = oldx;


                }

                int ny = 0;
                try
                {
                    ny = VideoScaler.ScalePosPicToVideo(picB1.Top);

                    if (ny < 0)
                    {
                        ny = 0;
                    }
                    else if (ny > VideoScaler.Height - h)
                    {
                        ny = VideoScaler.Height - h;
                    }
                    else if (ny > nudYB.Maximum)
                    {
                        ny = (int)nudYB.Maximum;
                    }

                    nudYB.Value = ny;
                }
                catch
                {
                    nudYB.Value = oldy;
                }
            }
            finally
            {
                InUpdatePosAndSize = false;
            }
        }

        private void UpdatePosAndSizeB()
        {
            try
            {
                InUpdatePosAndSize = true;

                if (VideoScaler == null) return;

                int oldx = (int)nudXB.Value;
                int oldy = (int)nudYB.Value;
                int oldh = (int)nudHeightB.Value;
                int oldw = (int)nudWidthB.Value;

                int w = VideoScaler.ScalePosPicToVideo(picB2.Left - picB1.Left);
                int h = VideoScaler.ScalePosPicToVideo(picB4.Top - picB1.Top);

                picB1.Left = Math.Max(1, Math.Min(picB1.Left, VideoScaler.PicMaxWidth));
                picB2.Left = Math.Max(1, Math.Min(picB2.Left, VideoScaler.PicMaxWidth));
                picB3.Left = Math.Max(1, Math.Min(picB3.Left, VideoScaler.PicMaxWidth));
                picB4.Left = Math.Max(1, Math.Min(picB4.Left, VideoScaler.PicMaxWidth));

                picB1.Top = Math.Max(1, Math.Min(picB1.Top, VideoScaler.PicMaxHeight));
                picB2.Top = Math.Max(1, Math.Min(picB2.Top, VideoScaler.PicMaxHeight));
                picB3.Top = Math.Max(1, Math.Min(picB3.Top, VideoScaler.PicMaxHeight));
                picB4.Top = Math.Max(1, Math.Min(picB4.Top, VideoScaler.PicMaxHeight));

                w = VideoScaler.ScalePosPicToVideo(picB2.Left - picB1.Left);
                h = VideoScaler.ScalePosPicToVideo(picB4.Top - picB1.Top);

                nudXB.Minimum = 1;
                nudXB.Maximum = VideoScaler.Width - w;

                nudYB.Minimum = 1;
                nudYB.Maximum = VideoScaler.Height - h;

                nudWidthB.Minimum = 1;
                nudWidthB.Maximum = VideoScaler.Width - VideoScaler.ScalePosPicToVideo(picB1.Left);

                nudHeightB.Minimum = 1;
                nudHeightB.Maximum = VideoScaler.Height - VideoScaler.ScalePosPicToVideo(picB1.Top);

                int nx = 0;
                try
                {
                    nx = VideoScaler.ScalePosPicToVideo(picB1.Left);

                    if (nx < 0)
                    {
                        nx = 0;
                    }
                    else if (nx > VideoScaler.Width - w)
                    {
                        nx = VideoScaler.Width - w;
                    }
                    else if (nx > nudXB.Maximum)
                    {
                        nx = (int)nudXB.Maximum;
                    }

                    nudXB.Value = nx;
                }
                catch
                {
                    nudXB.Value = oldx;


                }

                int ny = 0;
                try
                {
                    ny = VideoScaler.ScalePosPicToVideo(picB1.Top);

                    if (ny < 0)
                    {
                        ny = 0;
                    }
                    else if (ny > VideoScaler.Height - h)
                    {
                        ny = VideoScaler.Height - h;
                    }
                    else if (ny > nudYB.Maximum)
                    {
                        ny = (int)nudYB.Maximum;
                    }

                    nudYB.Value = ny;
                }
                catch
                {
                    nudYB.Value = oldy;
                }

                try
                {
                    int nw = VideoScaler.ScalePosPicToVideo(picB2.Left - picB1.Left);
                    if (nw < 1)
                    {
                        nw = 1;
                    }
                    else if (nw + nx > VideoScaler.Width)
                    {
                        nw = VideoScaler.Width - nx;
                    }
                    else if (nw > nudWidthB.Maximum)
                    {
                        nw = (int)nudWidthB.Maximum;
                    }

                    nudWidthB.Value = nw;
                }
                catch
                {
                    nudWidthB.Value = oldw;
                }

                try
                {
                    int nh = VideoScaler.ScalePosPicToVideo(picB4.Top - picB1.Top);

                    if (nh < 1)
                    {
                        nh = 1;
                    }
                    else if (nh + ny > VideoScaler.Height)
                    {
                        nh = VideoScaler.Height - ny;
                    }
                    else if (nh > nudHeightB.Maximum)
                    {
                        nh = (int)nudHeightB.Maximum;
                    }

                    nudHeightB.Value = nh;
                }
                catch
                {
                    nudHeightB.Value = oldh;
                }
            }
            finally
            {
                InUpdatePosAndSize = false;
            }
        }
        private void nudX_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int x = VideoScaler.ScalePosVideoToPic((int)nudX.Value);

            int width = pic2.Left - pic1.Left;
            int height = pic4.Top - pic1.Top;

            pic1.Left = x;
            pic4.Left = x;

            pic5.Left = x + width / 2;
            pic6.Left = x + width;
            pic7.Left = x + width / 2;
            pic8.Left = x;

            pic2.Left = x + width;
            pic3.Left = x + width;

            nudWidth.Maximum = VideoScaler.Width - (int)nudX.Value;
            nudWidth.Minimum = 1;

            picMovie.Invalidate();

        }

        private void nudY_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int y = VideoScaler.ScalePosVideoToPic((int)nudY.Value);
            int width = pic2.Left - pic1.Left;
            int height = pic4.Top - pic1.Top;

            pic1.Top = y;
            pic2.Top = y;
            pic3.Top = y + height;
            pic4.Top = y + height;

            pic5.Top = y;
            pic6.Top = y + height / 2;
            pic7.Top = y + height;
            pic8.Top = y + height / 2;

            nudHeight.Maximum = VideoScaler.Height - (int)nudY.Value;
            nudHeight.Minimum = 1;

            picMovie.Invalidate();
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int x = VideoScaler.ScalePosVideoToPic((int)nudWidth.Value);
            pic2.Left = pic1.Left + x;
            pic3.Left = pic1.Left + x;

            pic5.Left = pic1.Left + (int)((pic2.Left - pic1.Left) / 2);
            pic6.Left = pic1.Left + x;
            pic7.Left = pic1.Left + (int)((pic2.Left - pic1.Left) / 2);

            nudX.Maximum = VideoScaler.Width - (int)nudWidth.Value;
            nudX.Minimum = 1;

            picMovie.Invalidate();
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int y = VideoScaler.ScalePosVideoToPic((int)nudHeight.Value);
            pic3.Top = pic1.Top + y;
            pic4.Top = pic1.Top + y;

            pic6.Top = pic1.Top + (y / 2);
            pic7.Top = pic1.Top + y;
            pic8.Top = pic1.Top + (y / 2);

            nudY.Maximum = VideoScaler.Height - (int)nudHeight.Value;
            nudY.Minimum = 1;

            picMovie.Invalidate();
        }

        #region nud B changed

        private void nudXB_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int x = VideoScaler.ScalePosVideoToPic((int)nudXB.Value);

            int width = picB2.Left - picB1.Left;
            int height = picB4.Top - picB1.Top;

            picB1.Left = x;
            picB4.Left = x;

            picB5.Left = x + width / 2;
            picB6.Left = x + width;
            picB7.Left = x + width / 2;
            picB8.Left = x;

            picB2.Left = x + width;
            picB3.Left = x + width;

            nudWidthB.Maximum = VideoScaler.Width - (int)nudXB.Value;
            nudWidthB.Minimum = 1;

            picMovieB.Invalidate();

        }

        private void nudYB_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int y = VideoScaler.ScalePosVideoToPic((int)nudYB.Value);
            int width = picB2.Left - picB1.Left;
            int height = picB4.Top - picB1.Top;

            picB1.Top = y;
            picB2.Top = y;
            picB3.Top = y + height;
            picB4.Top = y + height;

            picB5.Top = y;
            picB6.Top = y + height / 2;
            picB7.Top = y + height;
            picB8.Top = y + height / 2;

            nudHeightB.Maximum = VideoScaler.Height - (int)nudYB.Value;
            nudHeightB.Minimum = 1;

            picMovieB.Invalidate();
        }

        private bool InKeepAspectRatio = false;        
        
        private void nudWidthB_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int x = VideoScaler.ScalePosVideoToPic((int)nudWidthB.Value);
            picB2.Left = picB1.Left + x;
            picB3.Left = picB1.Left + x;

            picB5.Left = picB1.Left + (int)((picB2.Left - picB1.Left) / 2);
            picB6.Left = picB1.Left + x;
            picB7.Left = picB1.Left + (int)((picB2.Left - picB1.Left) / 2);

            nudXB.Maximum = VideoScaler.Width - (int)nudWidthB.Value;
            nudXB.Minimum = 1;

            picMovieB.Invalidate();

            if (!DraggingHeight && (chkKeepAspectRatio.Checked && !InKeepAspectRatio))
            {
                decimal d1 = (decimal)nudWidth.Value;
                decimal d2 = (decimal)nudHeight.Value;

                // width height
                // width2 x; height

                decimal d3 = (decimal)nudWidthB.Value;

                decimal d = (d3 * d2 )/ d1;

                int ival = (int)d;

                InKeepAspectRatio = true;

                nudHeightB.Value = Math.Max(nudHeightB.Minimum, Math.Min(ival, nudHeightB.Maximum));                

                nudHeightB_ValueChanged(null, null);

                InKeepAspectRatio = false;

                DraggingHeight = false;
            }            
        }

        private void nudHeightB_ValueChanged(object sender, EventArgs e)
        {
            if (InUpdatePosAndSize) return;

            int y = VideoScaler.ScalePosVideoToPic((int)nudHeightB.Value);
            picB3.Top = picB1.Top + y;
            picB4.Top = picB1.Top + y;

            picB6.Top = picB1.Top + (y / 2);
            picB7.Top = picB1.Top + y;
            picB8.Top = picB1.Top + (y / 2);

            nudYB.Maximum = VideoScaler.Height - (int)nudHeightB.Value;
            nudYB.Minimum = 1;

            picMovieB.Invalidate();

            if (DraggingHeight && (chkKeepAspectRatio.Checked && !InKeepAspectRatio))
            {
                decimal d1 = (decimal)nudWidth.Value;
                decimal d2 = (decimal)nudHeight.Value;

                // width height
                // x width2 heightb

                decimal d3 = (decimal)nudHeightB.Value;

                decimal d = (d3 * d1) / d2;

                int ival = (int)d;

                InKeepAspectRatio = true;

                nudWidthB.Value = Math.Max(nudWidthB.Minimum, Math.Min(ival, nudWidthB.Maximum));                               

                nudWidthB_ValueChanged(null, null);

                InKeepAspectRatio = false;
            }
        }

        #endregion

        public static bool WasInFormClosing = false;

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WasInFormClosing) return;

            WasInFormClosing = true;
            /*
            if (!InChangeLanguage)
            {
                if (frmPurchase.RenMove && frmPurchase.Msg != DisplayMessageType1.License_Expired)
                {
                    if (frmPurchase.Datefrom != string.Empty)
                    {
                        frmPurchase f = new frmPurchase(frmPurchase.Msg, frmPurchase.Datefrom, frmPurchase.Datelast);
                        f.ShowDialog();
                    }
                    else
                    {
                        frmPurchase f = new frmPurchase(frmPurchase.Msg);
                    }
                }
            }
            */

            int ichecked = -1;

            for (int k = 0; k < whenFinishedToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem ti = (ToolStripMenuItem)whenFinishedToolStripMenuItem.DropDownItems[k];

                if (ti.Checked)
                {
                    ichecked = k;
                    break;
                }
            }

            Properties.Settings.Default.WhenFinishedIndex = ichecked;

            Properties.Settings.Default.KeepAspectRatio = chkKeepAspectRatio.Checked;

            Properties.Settings.Default.Crop = rdCrop.Checked;

            Properties.Settings.Default.CheckWeek = checkForNewVersionEachWeekToolStripMenuItem.Checked;

            Properties.Settings.Default.ShowMsgOnSuccess = showMessageOnSucessToolStripMenuItem.Checked;

            Properties.Settings.Default.Save();
        }

        private void btnStartUpdateImage_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateImg(mskStart.Text);

                grpStartTime.BackColor = Color.FromArgb(251, 251, 66);
                grpEndTime.BackColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not update image !", ex);
            }
        }

        private void btnEndUpdateImage_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateImg(mskEnd.Text);

                grpEndTime.BackColor = Color.FromArgb(251, 251, 66);
                grpStartTime.BackColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not update image !", ex);
            }
        }

        private bool Scrolling = false;

        private void tbStartTime_Scroll(object sender, EventArgs e)
        {
            Scrolling = true;

            mskStart.Text = FFMpegVideoInfo.TimeMsecsToString(tbStartTime.Value);
            nudStartTime.Value = tbStartTime.Value;
        }

        private void tbEndTime_Scroll(object sender, EventArgs e)
        {
            Scrolling = true;

            mskEnd.Text = FFMpegVideoInfo.TimeMsecsToString(tbEndTime.Value);
            nudEndTime.Value = tbEndTime.Value;
        }

        private void tbStartTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (e == null) return;

            if (!Scrolling)
            {
                mskStart.Text = FFMpegVideoInfo.TimeMsecsToString(tbStartTime.Value);
                nudStartTime.Value = tbStartTime.Value;
            }

            btnStartUpdateImage_Click(null, null);

            Scrolling = false;
        }

        private void tbEndTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (e == null) return;

            if (!Scrolling)
            {
                mskEnd.Text = FFMpegVideoInfo.TimeMsecsToString(tbEndTime.Value);
                nudEndTime.Value = tbEndTime.Value;
            }

            btnEndUpdateImage_Click(null, null);
        }

        private void mskStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!(ValidateTime(mskStart.Text)))
                {
                    e.Handled = true;
                    return;
                }

                btnStartUpdateImage_Click(null, null);
                tbStartTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskStart.Text);
                nudStartTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskStart.Text);
            }
        }

        private void mskStart_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int msecs = FFMpegVideoInfo.TimeStringToMsecs(mskStart.Text);

                if (msecs > DurationMsecs)
                {
                    Module.ShowMessage("The Value exceeds Video Duration !");
                    e.Cancel = true;
                    return;
                }

                btnStartUpdateImage_Click(null, null);
                tbStartTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskStart.Text);
                nudStartTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskStart.Text);
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not set Value !", ex);
                e.Cancel = true;
                return;
            }

        }

        private bool ValidateTime(string str)
        {
            try
            {
                int msecs = FFMpegVideoInfo.TimeStringToMsecs(str);

                if (msecs > DurationMsecs)
                {
                    Module.ShowMessage("The Value exceeds Video Duration !");

                    return false;
                }
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not set Value !", ex);

                return false;
            }

            return true;
        }

        private void mskEnd_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int msecs = FFMpegVideoInfo.TimeStringToMsecs(mskEnd.Text);

                if (msecs > DurationMsecs)
                {
                    Module.ShowMessage("The Value exceeds Video Duration !");
                    e.Cancel = true;
                    return;
                }

                btnEndUpdateImage_Click(null, null);
                tbEndTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskEnd.Text);
                nudEndTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskEnd.Text);
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not set Value !", ex);
                e.Cancel = true;
                return;
            }
        }

        private void mskEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!(ValidateTime(mskEnd.Text)))
                {
                    e.Handled = true;
                    return;
                }

                btnEndUpdateImage_Click(null, null);
                tbEndTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskEnd.Text);
                nudEndTime.Value = FFMpegVideoInfo.TimeStringToMsecs(mskEnd.Text);
            }
        }

        private void nudStartTime_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void nudStartTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudEndTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudStartTime_Click(object sender, EventArgs e)
        {
            if (nudStartTime.Focused)
            {
                tbStartTime.Value = (int)nudStartTime.Value;
                tbStartTime_Scroll(null, null);
                tbStartTime_MouseUp(null, null);
            }
        }

        private void nudEndTime_Click(object sender, EventArgs e)
        {
            if (nudEndTime.Focused)
            {
                tbEndTime.Value = (int)nudEndTime.Value;
                tbEndTime_Scroll(null, null);
                tbEndTime_MouseUp(null, null);
            }
        }

        private string GetArgs()
        {
            string str = "";

            str += " -y -i  \"" + Filepath + "\" ";

            str += " -ss " + mskStart.Text;

            int endval = tbEndTime.Value - tbStartTime.Value;

            int endvalsecs = endval / 100;
            int endvalmsecs = endval % 100;

            str += " -t " + endvalsecs.ToString() + "." + endvalmsecs.ToString();

            str += " -vf delogo=x=" + nudX.Value.ToString() + ":y=" + nudY.Value.ToString() +
                //5.2017 ":w="+nudWidth.Value.ToString()+":h="+nudHeight.Value.ToString()+":t="+"4"+":show=0 ";
                ":w=" + nudWidth.Value.ToString() + ":h=" + nudHeight.Value.ToString() + ":show=0 ";

            if (System.IO.Path.GetExtension(Filepath).ToLower() == ".3gp")
            {
                str += " -r 20 -s 352x288 -b 400k -strict experimental -c:a aac -ac 1 -ar 8000 -ab 24k ";
            }
            else if (System.IO.Path.GetExtension(Filepath).ToLower() == ".wmv")
            {
                str += " -q:a 2 -q:v 2 -vcodec msmpeg4 -acodec wmav2 ";
            }

            string outfilename = "\"" + System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Filepath), System.IO.Path.GetFileNameWithoutExtension(Filepath) + "_removed_logo" +
                System.IO.Path.GetExtension(Filepath)) + "\"";

            FirstOutputFile = outfilename;

            str += outfilename;

            return str;
        }
        /*
        private void tsbRemoveLogo_Click(object sender, EventArgs e)
        {
            if (tbEndTime.Value <= tbStartTime.Value)
            {
                Module.ShowMessage("The selected Start Time exceeds End Time !");
                return;
            }

            try
            {
                if (psFFMpeg != null && !psFFMpeg.HasExited)
                {
                    psFFMpeg.Kill();
                }
            }
            catch { }

            try
            {

                psFFMpeg = new Process();

                psFFMpeg.StartInfo.FileName = "ffmpeg ";
                psFFMpeg.StartInfo.CreateNoWindow = true;
                psFFMpeg.StartInfo.UseShellExecute = false;
                //psFFMpeg.StartInfo.RedirectStandardInput = true;
                //psFFMpeg.StartInfo.RedirectStandardOutput = true;
                psFFMpeg.StartInfo.RedirectStandardError = true;
                psFFMpeg.StartInfo.Arguments = GetArgs();

                psFFMpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                CompletedSecs = 0;
                ConversionStopped = false;
                errstr = "";

                this.Enabled = false;

                frmProgress.Instance.progressBar1.Value = 0;
                frmProgress.Instance.Secs = 0;
                frmProgress.Instance.timTime.Enabled = true;
                frmProgress.Instance.progressBar1.Maximum = tbEndTime.Value - tbStartTime.Value;

                frmProgress.Instance.Show(this);

                bwRemoveLogo.RunWorkerAsync();

                while (bwRemoveLogo.IsBusy)
                {
                    Application.DoEvents();
                }

                frmProgress.Instance.HideForm();

                this.Enabled = true;

                if (ConversionStopped)
                {
                    Module.ShowMessage("Operation Cancelled !");
                    return;
                }

                if (!ConversionStopped)
                {
                    ProcessWhenFinished();

                    if (errstr != String.Empty)
                    {
                        frmError fer = new frmError(TranslateHelper.Translate("Operation completed with Errors !"), errstr);
                        fer.ShowDialog();

                        //Module.ShowMessage(TranslateHelper.Translate("Operation completed with Errors !") + "\n\n" + errstr);
                    }
                    else
                    {
                        Module.ShowMessage("Operation completed successfully !");
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }
        */
        /*
        private void ProcessWhenFinished()
        {
            string args = string.Format("/e, /select, \"{0}\"", FirstOutputFile);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.UseShellExecute = true;
            info.Arguments = args;
            Process.Start(info);
        }*/

        private void bwRemoveLogo_DoWork(object sender, DoWorkEventArgs e)
        {
            psFFMpeg.Start();

            System.Threading.Thread.Sleep(300);

            while (true)
            {
                string line;

                int beforeCompletedSecs = CompletedSecs;

                bool time_str_found = false;

                string last_line = "";

                while ((line = psFFMpeg.StandardError.ReadLine()) != null)
                {
                    last_line = line;

                    Application.DoEvents();

                    //0sw.WriteLine(line);
                    //0sw.Flush();

                    if (bwRemoveLogo.CancellationPending)
                    {
                        //psFFMpeg.Kill();
                        break;
                    }

                    if (line != null && line.Contains("time="))
                    {
                        time_str_found = true;

                        try
                        {
                            int spos = line.LastIndexOf("time=") + "time=".Length;
                            int epos = line.IndexOf(" ", spos);

                            string time = line.Substring(spos, epos - spos + 1);

                            //0sw.WriteLine("time=" + time);

                            TimeSpan ts = new TimeSpan(0, int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(3, 2)),
                                int.Parse(time.Substring(6, 2)), int.Parse(time.Substring(9, 2)));

                            int completed_secs = (int)ts.TotalMilliseconds;

                            //0sw.WriteLine("parsed time="+ts.ToString() + "." + msecs.ToString());

                            //0sw.WriteLine("before completed secs=" + CompletedSecs.ToString());

                            CompletedSecs = beforeCompletedSecs + completed_secs;

                            //0sw.WriteLine("completed secs="+CompletedSecs.ToString());

                            //int totalsex = LastCutArgs.TotalDuration / 10;

                            //1int progress = (int)((decimal)CompletedSecs * 100 / (decimal)(LastCutArgs.TotalDuration));

                            //1bwConvert.ReportProgress(progress);
                            bwRemoveLogo.ReportProgress(0, CompletedSecs);
                        }
                        catch { }

                    }
                    else if (line != null && line.ToLower().StartsWith("error"))
                    {
                        errstr += line;
                    }
                }

                //if no time= string was found this means that an error occured
                if (!time_str_found)
                {
                    errstr += last_line;
                }

                if (psFFMpeg.HasExited) break;
            }
        }

        private void bwRemoveLogo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pg = (int)e.UserState;

            if (pg >= 0 && pg <= frmProgress.Instance.progressBar1.Maximum)
            {
                frmProgress.Instance.progressBar1.Value = pg;
            }
        }

        private void bwRemoveLogo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private bool IsDragging = true;

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                IsDragging = true;
                e.Effect = DragDropEffects.All;

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] filez = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    AddFile(filez[0]);
                }
                finally
                {
                    this.Cursor = null;
                }
            }
        }

        private void frmMain_DragOver(object sender, DragEventArgs e)
        {
            IsDragging = true;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;
            }

        }

        #region Help - Register

        private void buyApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Module.BuyURL);
        }

        private void enterLicenseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pleaseDonateMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com/donate.php");
        }

        private void dotsSoftwarePRODUCTCATALOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com/downloads/4dots-Software-PRODUCT-CATALOG.pdf");
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }

        private void tiToolsOpenOutput_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(FirstOutputFile))
            {
                Module.ShowMessage(TranslateHelper.Translate("Output File does not exist !") + "\n\n" + FirstOutputFile);
                return;
            }

            System.Diagnostics.Process.Start(FirstOutputFile);
        }

        private void tiToolsExploreOutput_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(FirstOutputFile))
            {
                Module.ShowMessage(TranslateHelper.Translate("Output File does not exist !") + "\n\n" + FirstOutputFile);
                return;
            }

            string filepath = FirstOutputFile;

            string args = string.Format("/e, /select, \"{0}\"", filepath);

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.UseShellExecute = true;
            info.Arguments = args;
            Process.Start(info);
        }

        private void tiToolsCopyPathOutput_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(FirstOutputFile))
            {
                Module.ShowMessage(TranslateHelper.Translate("Output File does not exist !") + "\n\n" + FirstOutputFile);
                return;
            }

            string filepath = FirstOutputFile;

            Clipboard.Clear();

            Clipboard.SetText(filepath);
        }

        private void tiToolsVideoInfoOutput_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(FirstOutputFile))
            {
                Module.ShowMessage(TranslateHelper.Translate("Output File does not exist !") + "\n\n" + FirstOutputFile);
                return;
            }

            frmVideoInfo f = new frmVideoInfo(FirstOutputFile);

            f.ShowDialog();
        }

        #region Rotation Function

        void bwConvert_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pg = (int)e.UserState;

            /*1
            if (pg >= 0 && pg <= pbarCut.Maximum)
            {
                pbarCut.Value = pg;
            }
            */

            if (frmProgress.Instance != null && frmProgress.Instance.Visible)
            {
                if (pg >= 0 && pg <= frmProgress.Instance.progressBar1.Maximum)
                {
                    frmProgress.Instance.progressBar1.Value = pg;

                    decimal d1 = (decimal)frmProgress.Instance.progressBar1.Value;
                    decimal d2 = (decimal)frmProgress.Instance.progressBar1.Maximum;

                    decimal dprog = (d1 / d2) * 100;

                    int iprog = (int)dprog;

                    frmProgress.Instance.progressBar1.lblText = iprog.ToString() + "%";

                    int totalprog = frmProgress.Instance.PreviousTotalProgress + pg;

                    if (totalprog >= 0 && totalprog <= frmProgress.Instance.pbarTotal.Maximum)
                    {
                        frmProgress.Instance.pbarTotal.Value = totalprog;

                        decimal dt1 = (decimal)frmProgress.Instance.pbarTotal.Value;
                        decimal dt2 = (decimal)frmProgress.Instance.pbarTotal.Maximum;

                        decimal dtprog = (dt1 / dt2) * 100;

                        int itprog = (int)dtprog;

                        frmProgress.Instance.pbarTotal.lblText = itprog.ToString() + "%";
                    }
                }
            }
        }

        void bwConvert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if no time= string was found this means that an error occured
            if (!time_str_found)
            {
                errstr += last_line;
            }
        }

        void bwConvert_DoWork(object sender, DoWorkEventArgs e)
        {
            //0sw.WriteLine("ARGS=");
            //0sw.WriteLine(psFFMpeg.StartInfo.Arguments);

            LastFFMpegOutput = "";

            beforeCompletedSecs = CompletedSecs;

            time_str_found = false;

            last_line = "";

            Console.WriteLine("ARGS=" + psFFMpeg.StartInfo.Arguments);

            psFFMpeg.Start();

            psFFMpeg.BeginErrorReadLine();
            psFFMpeg.BeginOutputReadLine();

            psFFMpeg.WaitForExit();

            psFFMpeg.Close();

            psFFMpeg.Dispose();
            psFFMpeg = null;


            /*
            if (!time_str_found)
            {
                errstr += last_line;
            }*/

        }

        public void tsbStopJoin_Click(object sender, EventArgs e)
        {
            ConversionStopped = true;
            ConversionStarted = false;
            ConversionPaused = false;

            //1tsbStopCut.Visible = false;
            //1tsbCutVideo.Image = Properties.Resources.cut1;
            //1tsbCutVideo.Text = TranslateHelper.Translate("Cut Video");

            try
            {
                psFFMpeg.Kill();
            }
            catch { }

            bwConvert.CancelAsync();

            /*
            for (int k = 0; k < lstProcessConvert.Count; k++)
            {
                try
                {
                    lstProcessConvert[k].Kill();
                }
                catch { }
            }*/

            if (frmProgress.Instance != null && frmProgress.Instance.Visible)
            {
                frmProgress.Instance.Close();
            }

            if (ConversionStopped)
            {
                Module.ShowMessage("Watermark removal process stopped !");
                return;
            }
        }

        /*
         
         if (tbEndTime.Value <= tbStartTime.Value)
            {
                Module.ShowMessage("The selected Start Time exceeds End Time !");
                return;
            }

            try
            {
                if (psFFMpeg != null && !psFFMpeg.HasExited)
                {
                    psFFMpeg.Kill();
                }
            }
            catch { }

            try
            {

                psFFMpeg = new Process();

                psFFMpeg.StartInfo.FileName = "ffmpeg ";
                psFFMpeg.StartInfo.CreateNoWindow = true;
                psFFMpeg.StartInfo.UseShellExecute = false;
                //psFFMpeg.StartInfo.RedirectStandardInput = true;
                //psFFMpeg.StartInfo.RedirectStandardOutput = true;
                psFFMpeg.StartInfo.RedirectStandardError = true;
                psFFMpeg.StartInfo.Arguments = GetArgs();

                psFFMpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                CompletedSecs = 0;
                ConversionStopped = false;
                errstr = "";

                this.Enabled = false;

                frmProgress.Instance.progressBar1.Value = 0;
                frmProgress.Instance.Secs = 0;
                frmProgress.Instance.timTime.Enabled = true;
                frmProgress.Instance.progressBar1.Maximum = tbEndTime.Value - tbStartTime.Value;

                frmProgress.Instance.Show(this);

                bwRemoveLogo.RunWorkerAsync();

                while (bwRemoveLogo.IsBusy)
                {
                    Application.DoEvents();
                }

                frmProgress.Instance.HideForm();

                this.Enabled = true;

                if (ConversionStopped)
                {
                    Module.ShowMessage("Operation Cancelled !");
                    return;
                }

                if (!ConversionStopped)
                {
                    ProcessWhenFinished();

                    if (errstr != String.Empty)
                    {
                        frmError fer = new frmError(TranslateHelper.Translate("Operation completed with Errors !"), errstr);
                        fer.ShowDialog();

                        //Module.ShowMessage(TranslateHelper.Translate("Operation completed with Errors !") + "\n\n" + errstr);
                    }
                    else
                    {
                        Module.ShowMessage("Operation completed successfully !");
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
         */

        private bool CheckVWRClips()
        {
            bool asked = false;

            List<VWRClip> lst = new List<VWRClip>();

            for (int k = 0; k < VWRClips.Count; k++)
            {
                lst.Add(VWRClips[k]);
            }

            lst.Sort();

            for (int k = 0; k < lst.Count; k++)
            {
                if (k > 0)
                {
                    if (lst[k].StartTime <= lst[k - 1].EndTime)
                    {
                        if (!asked)
                        {
                            asked = true;

                            DialogResult dres = Module.ShowQuestionDialogYesFocus(TranslateHelper.Translate("Start Time of Clip is before the End Time of Previous Clip ! Would you like to fix Start Time ?"), TranslateHelper.Translate("Fix Start Time of Clip ?"));

                            if (dres == DialogResult.Yes)
                            {
                                int ik = VWRClips.IndexOf(lst[k]);

                                VWRClips[ik].StartTime = VWRClips[ik - 1].EndTime + 1;

                            }
                            else
                            {
                                Module.ShowMessage("Please fix the problem with Start Times of Clips !");

                                return false;
                            }
                        }

                    }
                }
            }

            return true;
        }

        public void tsbConvert_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowMsgOnSuccess = showMessageOnSucessToolStripMenuItem.Checked;

            string outputformat = "";
            string outputext = ".mp4";
            string outputparams = " ";

            JoinArgs res = null;

            if (ConversionStarted)
            {
                if (!psFFMpeg.HasExited)
                {
                    SuspendResumeThread.SuspendProcess(psFFMpeg.Id);
                    ConversionPaused = true;
                    ConversionStarted = false;
                    timJoinVideos.Enabled = false;

                    if (frmProgress.Instance != null)
                    {
                        frmProgress.Instance.btnPause.Image = Properties.Resources.flash1;
                        frmProgress.Instance.btnPause.Text = TranslateHelper.Translate("Resume");
                        frmProgress.Instance.timTime.Enabled = false;
                    }
                }
            }
            else if (ConversionPaused)
            {
                ConversionStarted = true;
                ConversionPaused = true;
                timJoinVideos.Enabled = true;
                SuspendResumeThread.ResumeProcess(psFFMpeg.Id);

                if (frmProgress.Instance != null)
                {
                    frmProgress.Instance.btnPause.Image = Properties.Resources.media_pause;
                    frmProgress.Instance.btnPause.Text = TranslateHelper.Translate("Pause");
                    frmProgress.Instance.timTime.Enabled = true;
                }
            }
            else
            {
                try
                {
                    ConversionStopped = false;
                    ConversionStarted = true;
                    errstr = "";
                    FirstOutputFile = "";

                    CompletedSecs = 0;

                    if (frmMain.Instance.Filepath == string.Empty)
                    {
                        Module.ShowMessage("Please open a Media File first !");
                        return;
                    }

                    SaveLastVWRClip();

                    Properties.Settings.Default.Crop = rdCrop.Checked;

                    for (int m = 0; m < VWRClips.Count; m++)
                    {
                        if (VWRClips[m].EndTime <= VWRClips[m].StartTime)
                        {
                            Module.ShowMessage("The selected Start Time exceeds End Time !");

                            cmbClip.SelectedIndex = m;

                            return;
                        }
                    }

                    bool cok = CheckVWRClips();

                    if (!cok) return;

                    List<VWRClip> lstvwr = new List<VWRClip>();

                    for (int k = 0; k < VWRClips.Count; k++)
                    {
                        lstvwr.Add(VWRClips[k]);
                    }

                    lstvwr.Sort();

                    frmOutputFormat fo = new frmOutputFormat();

                    if (fo.ShowDialog() == DialogResult.Cancel)
                    {
                        ConversionStopped = true;

                        return;
                    }

                    outputext = fo.Extension;
                    outputparams = fo.FFMpegParameters + " " + Properties.Settings.Default.OFAdditionalParameters;

                    List<string> lstArgs = new List<string>();
                    List<string> lstInputFiles = new List<string>();

                    string joinFile = "";

                    List<string> FilesToDelete = new List<string>();

                    JoinArgs res2 = null;

                    List<JoinArgs> lstres = new List<JoinArgs>();

                    int totallstresdur = 0;
                    int totalclipdur = 0;

                    List<string> joinfp = new List<string>();

                    for (int m = 0; m < lstvwr.Count; m++)
                    {
                        // 1. For Crop
                        // For start time to end time
                        // get xb,yb,widthb,heightb
                        // create Crop Files with normal filenames

                        // 2. For Zoom
                        // For start time to end time
                        // get xb,yb,widthb,heightb
                        // create Crop Files with temp filenames
                        // create Overlay Files with temp filenames
                        // create join for each start to end time with temp filenames from overlay output files and main video

                        // 3. For Join Crop
                        // create join with pad and main video and crop parts

                        string startTime = FFMpegVideoInfo.TimeMsecsToString(lstvwr[m].StartTime);
                        int startmsecs = TimeUpDownControl.StringToMsecs(startTime);
                        int endmsecs = TimeUpDownControl.StringToMsecs(FFMpegVideoInfo.TimeMsecsToString(lstvwr[m].EndTime));
                        string endTime = FFMpegVideoInfo.TimeMsecsToString(lstvwr[m].EndTime);

                        int durationmsecs = endmsecs - startmsecs;

                        totalclipdur += durationmsecs;

                        string duration = TimeUpDownControl.MsecsToSecsString(durationmsecs);

                        int x = (int)lstvwr[m].X;
                        int y = (int)lstvwr[m].Y;
                        int width = (int)lstvwr[m].Width;
                        int height = (int)lstvwr[m].Height;

                        if (width % 2 == 1)
                        {
                            width = width - 1;
                        }

                        if (height % 2 == 1)
                        {
                            height = height - 1;
                        }

                        int xb = (int)lstvwr[m].XB;
                        int yb = (int)lstvwr[m].YB;
                        int widthb = (int)lstvwr[m].WidthB;
                        int heightb = (int)lstvwr[m].HeightB;

                        if (widthb % 2 == 1)
                        {
                            widthb = widthb - 1;
                        }

                        if (heightb % 2 == 1)
                        {
                            heightb = heightb - 1;
                        }


                        JoinArgsHelper jhelper = new JoinArgsHelper();

                        res2 = jhelper.GetZoomArgs(Filepath, outputext, outputparams, Properties.Settings.Default.OF1stPassParameters, Properties.Settings.Default.OF2ndPassParameters,
                        Properties.Settings.Default.OFVideoBitrate, Properties.Settings.Default.OFFrameRate,
                        Properties.Settings.Default.OFVideoSize, Properties.Settings.Default.OFAspectRatio,
                        Properties.Settings.Default.OFTwoPass, Properties.Settings.Default.OFDeinterlace,
                        Properties.Settings.Default.OFAudioBitrate, Properties.Settings.Default.OFSampleRate,
                        Properties.Settings.Default.OFAudioChannels, Properties.Settings.Default.OFVolume,
                        Properties.Settings.Default.OFAudioNormalize, Properties.Settings.Default.OFMute,
                        Properties.Settings.Default.OFKeepMetadata, startTime, endTime, x, y, width, height,
                        xb, yb, widthb, heightb,
                        DurationStr, DurationMsecs, m, lstvwr);

                        lstres.Add(res2);

                        totallstresdur += res2.DurationMsecs;
                    }


                    /*
                            joinFile = res.JoinFile;                            
                        

                        if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(res.JoinFile)))
                        {
                            try
                            {
                                System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(res.JoinFile));
                            }
                            catch
                            {
                                Module.ShowMessage("Error. Could not create Output File Directory !");
                                return;
                            }
                        }

                        Console.WriteLine("=====================");

                        Console.WriteLine(res.Args);

                        string argsout = "";

                        argsout += res.Args + outputparams + "\"" + joinFile + "\"";

                        //File.WriteAllText(@"c:\1\vje.txt", argsout);

                        if (FirstOutputFile == string.Empty)
                        {
                            FirstOutputFile = res.JoinFile;

                            if (System.IO.File.Exists(FirstOutputFile))
                            {
                                System.IO.File.Delete(FirstOutputFile);
                            }
                        }

                        lstArgs.Add(res2.Args);
                    }
                    */

                    //if (Properties.Settings.Default.JoinOverlayParts && !(Properties.Settings.Default.Crop && !Properties.Settings.Default.JoinOverlayParts))
                    if ((!Properties.Settings.Default.Crop && Properties.Settings.Default.JoinOverlayParts)
                || (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts))
                    {
                        totallstresdur += DurationMsecs / 100;
                    }

                    if (((!Properties.Settings.Default.Crop && Properties.Settings.Default.JoinOverlayParts)
                || (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts)))
                    {
                        totallstresdur += DurationMsecs / 100;
                    }
                    else
                    {
                        totallstresdur += totalclipdur/100;
                    }

                    timJoinVideos.Enabled = true;
                    //lblElapsedTime.Visible = true;

                    decimal current_total_time = 0;

                    EnableDisableForm(false);

                    frmProgress f = new frmProgress();
                    f.Show(this);
                    f.timTime.Enabled = true;
                    //this.Enabled = false;

                    //3f.progressBar1.Maximum = res.DurationMsecs * VWRClips.Count;

                    f.progressBar1.Maximum = totallstresdur;
                    f.progressBar1.Value = 0;
                    // f.lblOutputFile.Text = System.IO.Path.GetFileName(res.JoinFile);

                    if (ConversionStopped) return;

                    //for (int m = 0; m < VWRClips.Count; m++)

                    for (int m = 0; m < lstres.Count; m++)
                    {
                        if (ConversionStopped) return;

                        CreateFFMpegProcess();

                        //Clipboard.Clear();
                        //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                        psFFMpeg.StartInfo.Arguments = lstres[m].ArgsCrop;

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        if (ConversionStopped) return;

                        if (!Properties.Settings.Default.Crop)
                        {
                            CreateFFMpegProcess();

                            //Clipboard.Clear();
                            //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                            psFFMpeg.StartInfo.Arguments = lstres[m].ArgsOverlayCut;

                            bwConvert.RunWorkerAsync();

                            while (bwConvert.IsBusy)
                            {
                                Application.DoEvents();
                            }

                            if (ConversionStopped) return;

                            CreateFFMpegProcess();

                            //Clipboard.Clear();
                            //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                            psFFMpeg.StartInfo.Arguments = lstres[m].ArgsOverlay;

                            bwConvert.RunWorkerAsync();

                            while (bwConvert.IsBusy)
                            {
                                Application.DoEvents();
                            }

                            if (ConversionStopped) return;
                        }

                        if (lstres[m].NonZoomPartsArgsStart != string.Empty)
                        {
                            CreateFFMpegProcess();

                            //Clipboard.Clear();
                            //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                            psFFMpeg.StartInfo.Arguments = lstres[m].NonZoomPartsArgsStart;

                            bwConvert.RunWorkerAsync();

                            while (bwConvert.IsBusy)
                            {
                                Application.DoEvents();
                            }

                            if (ConversionStopped) return;

                            joinfp.Add(lstres[m].NonZoomPartsFilepathStart);

                        }

                        if (!Properties.Settings.Default.Crop)
                        {
                            joinfp.Add(lstres[m].OverlayOutputFilepath);
                        }
                        else
                        {
                            joinfp.Add(lstres[m].CropOutputFilepath);
                        }

                        if (lstres[m].NonZoomPartsArgsEnd != string.Empty)
                        {
                            CreateFFMpegProcess();

                            //Clipboard.Clear();
                            //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                            psFFMpeg.StartInfo.Arguments = lstres[m].NonZoomPartsArgsEnd;

                            bwConvert.RunWorkerAsync();

                            while (bwConvert.IsBusy)
                            {
                                Application.DoEvents();
                            }

                            if (ConversionStopped) return;

                            joinfp.Add(lstres[m].NonZoomPartsFilepathEnd);

                        }


                        if (Properties.Settings.Default.Crop && !Properties.Settings.Default.JoinCropParts)
                        {
                            FirstOutputFile = lstres[0].JoinFile;

                            if (System.IO.File.Exists(lstres[m].CropOutputFilepath))
                            {
                                if (System.IO.File.Exists(lstres[m].JoinFile))
                                {
                                    System.IO.File.Delete(lstres[m].JoinFile);
                                }

                                System.IO.File.Move(lstres[m].CropOutputFilepath, lstres[m].JoinFile);

                                ApplyVideoExifAndDates(Filepath, lstres[m].JoinFile,outputparams);
                            }
                        }
                        else if (!Properties.Settings.Default.Crop && !Properties.Settings.Default.JoinOverlayParts)
                        {
                            FirstOutputFile = lstres[0].JoinFile;

                            if (System.IO.File.Exists(lstres[m].OverlayOutputFilepath))
                            {
                                if (System.IO.File.Exists(lstres[m].JoinFile))
                                {
                                    System.IO.File.Delete(lstres[m].JoinFile);
                                }

                                System.IO.File.Move(lstres[m].OverlayOutputFilepath, lstres[m].JoinFile);

                                ApplyVideoExifAndDates(Filepath, lstres[m].JoinFile, outputparams);
                            }
                        }
                    }

                    //if (Properties.Settings.Default.JoinOverlayParts && !(Properties.Settings.Default.Crop && !Properties.Settings.Default.JoinOverlayParts))
                    if ((!Properties.Settings.Default.Crop && Properties.Settings.Default.JoinOverlayParts)
                || (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts))
                    {                        
                        string fn = "";
                        string joinfile = "";
                        string outfolder = System.IO.Path.GetDirectoryName(Filepath);

                        if (Properties.Settings.Default.OutputFolderIndex != 0)
                        {
                            outfolder = Properties.Settings.Default.OutputFolder;
                        }

                        fn = System.IO.Path.GetFileNameWithoutExtension(Filepath);

                        joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

                        FirstOutputFile = System.IO.Path.Combine(outfolder, joinfile + outputext);

                        string argsjoin = "";

                        string argsjoin2 = "";

                        string argsjoin3 = "";

                        for (int m = 0; m < joinfp.Count; m++)
                        {
                            argsjoin += " -i \"" + joinfp[m] + "\" ";

                            //argsjoin2 += "[" + m.ToString() + ":v:0]setsar=1:1,setdar=1:1[v"+m.ToString()+"];";

                            argsjoin2 += "[" + m.ToString() + ":v:0]copy[v" + m.ToString() + "];";

                            argsjoin3 += "[v" + m.ToString() + "][" + m.ToString() + ":a:0]";

                            //argsjoin3 += "[" + m.ToString() + ":v:0][" + m.ToString() + ":a:0]";
                        }

                        //argsjoin2 = "";

                        string metadata_args = "";

                        if (Properties.Settings.Default.OFKeepMetadata) metadata_args = " -map_metadata 0 ";

                        argsjoin += " -filter_complex \"" + argsjoin2 + argsjoin3 + "concat=n=" + joinfp.Count.ToString() + ":v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\" " + outputparams + metadata_args + " -y \"" + FirstOutputFile + "\"";

                        CreateFFMpegProcess();

                        //Clipboard.Clear();
                        //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                        psFFMpeg.StartInfo.Arguments = argsjoin;

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        if (ConversionStopped) return;

                        ApplyVideoExifAndDates(Filepath, FirstOutputFile, outputparams);
                    }

                    for (int k = 0; k < Module.FilesToDelete.Count; k++)
                    {
                        if (System.IO.File.Exists(Module.FilesToDelete[k]))
                        {
                            try
                            {
                                System.IO.File.Delete(Module.FilesToDelete[k]);

                            }
                            catch { }
                        }
                    }

                    Module.FilesToDelete.Clear();

                    /*
                    if (Properties.Settings.Default.OFTwoPass)
                    {
                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = lstArgs[m] + " -pass 1 " + Properties.Settings.Default.OF1stPassParameters + " NUL ";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        if (ConversionStopped) return;

                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = lstArgs[m] + " -pass 2 " + Properties.Settings.Default.OF2ndPassParameters + " \"" + joinFile + "\"";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }
                    else
                    {
                        if (ConversionStopped) return;

                        CreateFFMpegProcess();

                        //Clipboard.Clear();
                        //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                        psFFMpeg.StartInfo.Arguments = lstArgs[m] + " \"" + joinFile + "\"";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }

                    if (ConversionStopped) return;
                }
                */

                    /*
                    if (res.NormalizeArgs1 != string.Empty)
                    {
                        Module.MoveFile(joinFile, res.NormalizeFile);

                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = res.NormalizeArgs1;

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        res.NormalizeArgs2 = JoinArgsHelper.GetNormalizeArgs(res.NormalizeArgs2, LastFFMpegOutput);

                        if (res.NormalizeArgs2 != string.Empty)
                        {
                            if (ConversionStopped) return;

                            CreateFFMpegProcess();

                            psFFMpeg.StartInfo.Arguments = res.NormalizeArgs2 + " \"" + joinFile + "\"";

                            bwConvert.RunWorkerAsync();

                            while (bwConvert.IsBusy)
                            {
                                Application.DoEvents();
                            }
                        }
                        else
                        {
                            // no need to normalize use the joinnofilter file and the proper args

                            //res.AudioVolumeArgs = res.AudioVolumeArgsNoNormalize;

                            if (ConversionStopped) return;

                            Module.MoveFile(res.NormalizeFile, joinFile);

                            res.NormalizeFile = "";
                        }
                    }

                    if (res.JoinFileWithNoFilter != string.Empty)
                    {
                        if (ConversionStopped) return;

                        Module.MoveFile(res.JoinFile, res.JoinFileWithNoFilter);

                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = res.JoinArgsWithNoFilter + " \"" + joinFile + "\"";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }
                    }
                    
                    */
                }
                finally
                {
                    timJoinVideos.Enabled = false;
                    ConversionProgressTime = 0;
                    ConversionStarted = false;
                    //8OutputFileActionRepeat = false;                    

                    if (frmProgress.Instance != null && frmProgress.Instance.Visible)
                    {
                        frmProgress.Instance.Close();
                    }

                    //this.Enabled = true;

                    EnableDisableForm(true);

                    if (!ConversionStopped)
                    {
                        if (errstr == string.Empty)
                        {
                            ProcessWhenFinished();
                        }

                        if (errstr != String.Empty)
                        {
                            if (System.IO.File.Exists(FirstOutputFile))
                            {
                                frmError fer = new frmError(TranslateHelper.Translate("Output Video was produced but operation was completed with Errors !"), errstr);
                                fer.ShowDialog(this);
                            }
                            else
                            {
                                frmError fer = new frmError(TranslateHelper.Translate("Operation completed with Errors !"), errstr);
                                fer.ShowDialog(this);
                            }
                            //Module.ShowMessage(TranslateHelper.Translate("Operation completed with Errors !") + "\n\n" + errstr);
                        }
                        else
                        {
                            if (Properties.Settings.Default.ShowMsgOnSuccess)
                            {
                                Module.ShowMessage("Operation completed successfully !");
                            }
                        }
                    }

                    if (res != null)
                    {
                        if (res.NormalizeFile != string.Empty)
                        {
                            try
                            {
                                if (System.IO.File.Exists(res.NormalizeFile))
                                {
                                    System.IO.File.Delete(res.NormalizeFile);
                                }
                            }
                            catch
                            {
                                errstr += TranslateHelper.Translate("Error. Could not Delete File") + " : " + res.NormalizeFile + "\n\n";
                            }
                        }

                        if (res.JoinFileWithNoFilter != string.Empty)
                        {
                            try
                            {
                                if (System.IO.File.Exists(res.JoinFileWithNoFilter))
                                {
                                    System.IO.File.Delete(res.JoinFileWithNoFilter);
                                }
                            }
                            catch
                            {
                                errstr += TranslateHelper.Translate("Error. Could not Delete File") + " : " + res.JoinFileWithNoFilter + "\n\n";
                            }
                        }


                    }
                }
            }
        }

        private void EnableDisableForm(bool enable)
        {
            foreach (Control co in this.Controls)
            {
                co.Enabled = enable;
            }
        }

        private void ApplyVideoExifAndDates(string inputFilepath, string outputFilepath,string outputparams)
        {
            if (System.IO.File.Exists(outputFilepath))
            {
                string dtextargs = "";                

                if (Properties.Settings.Default.CopyEXIF)
                {
                    System.Diagnostics.Process pr = new Process();
                    pr.StartInfo.FileName = "\"" + System.IO.Path.Combine(Application.StartupPath, "exiftool.exe") + "\"";
                    pr.StartInfo.Arguments = "-tagsfromfile \"" + inputFilepath + "\" -exif \"" + outputFilepath + "\"";

                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pr.StartInfo.UseShellExecute = true;
                    pr.Start();

                    while (!pr.HasExited)
                    {
                        Application.DoEvents();
                    }

                    if (System.IO.File.Exists(outputFilepath + "_original"))
                    {
                        try
                        {
                            System.IO.File.Delete(outputFilepath + "_original");
                        }
                        catch
                        {

                        }
                    }

                    pr.Dispose();
                    pr = null;
                }

                if (Properties.Settings.Default.KeepCreationDate)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(inputFilepath);

                    System.IO.FileInfo fi2 = new System.IO.FileInfo(outputFilepath);

                    fi2.CreationTime = fi.CreationTime;

                    fi2.CreationTimeUtc = fi.CreationTimeUtc;
                }

                if (Properties.Settings.Default.KeepLastModDate)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(inputFilepath);

                    System.IO.FileInfo fi2 = new System.IO.FileInfo(outputFilepath);

                    fi2.LastWriteTime = fi.LastWriteTime;

                    fi2.LastWriteTimeUtc = fi.LastWriteTimeUtc;
                }
            }
        }

        private void ProcessWhenFinished()
        {
            int ichecked = -1;


            for (int k = 0; k < whenFinishedToolStripMenuItem.DropDownItems.Count - 1; k++)
            {
                ToolStripMenuItem ti = (ToolStripMenuItem)whenFinishedToolStripMenuItem.DropDownItems[k];
                if (ti.Checked)
                {
                    ichecked = k;
                    break;
                }
            }


            if (ichecked == 0)
            {
                ShutdownHelper.Shutdown();
            }
            else if (ichecked == 1)
            {
                ShutdownHelper.Sleep();
            }
            else if (ichecked == 2)
            {
                ShutdownHelper.Hibernate();
            }
            else if (ichecked == 3)
            {
                ShutdownHelper.Logoff();
            }
            else if (ichecked == 4)
            {
                ShutdownHelper.Lock();
            }
            else if (ichecked == 5)
            {
                ShutdownHelper.Restart();
            }
            else if (ichecked == 6)
            {
                Application.Exit();
            }
            else if (ichecked == 7)
            {
                string args = string.Format("/e, /select, \"{0}\"", FirstOutputFile);
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "explorer";
                info.UseShellExecute = true;
                info.Arguments = args;
                Process.Start(info);
            }

        }

        private void timJoinVideos_Tick(object sender, EventArgs e)
        {

        }        

        public void CreateFFMpegProcess()
        {
            System.Threading.Thread.Sleep(300);

            try
            {
                if (psFFMpeg != null) // && psFFMpeg.SynchronizingObject!=null &&  !psFFMpeg.HasExited)
                {
                    // psFFMpeg.Kill();
                }
            }
            catch { }

            System.Threading.Thread.Sleep(300);

            psFFMpeg = new Process();

            //psFFMpeg.StartInfo.FileName = "ffmpeg ";
            if (Properties.Settings.Default.FFMPEG64Bit == 1)
            {
                psFFMpeg.StartInfo.FileName = "ffmpeg64 ";
            }
            else
            {
                psFFMpeg.StartInfo.FileName = "ffmpeg32 ";
            }

            psFFMpeg.StartInfo.CreateNoWindow = true;
            psFFMpeg.StartInfo.UseShellExecute = false;
            psFFMpeg.StartInfo.RedirectStandardError = true;
            psFFMpeg.StartInfo.RedirectStandardOutput = true;

            psFFMpeg.OutputDataReceived += psFFMpeg_OutputDataReceived;
            psFFMpeg.ErrorDataReceived += psFFMpeg_ErrorDataReceived;

            psFFMpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //System.Threading.Thread.Sleep(500);            
        }

        void psFFMpeg_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string line = e.Data;

            Console.WriteLine(line);

            if (line != null)
            {
                last_line = line;
            }

            LastFFMpegOutput += line + "\r\n";

            Application.DoEvents();

            if (line != null && line.Contains("time="))
            {
                time_str_found = true;

                try
                {
                    int spos = line.LastIndexOf("time=") + "time=".Length;
                    int epos = line.IndexOf(" ", spos);

                    string time = line.Substring(spos, epos - spos + 1);

                    //0sw.WriteLine("time=" + time);

                    TimeSpan ts = new TimeSpan(int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(3, 2)),
                        int.Parse(time.Substring(6, 2)));

                    int completed_secs = (int)ts.TotalSeconds;
                    int msecs = int.Parse(time.Substring(9, 1));

                    //0sw.WriteLine("parsed time="+ts.ToString() + "." + msecs.ToString());

                    //0sw.WriteLine("before completed secs=" + CompletedSecs.ToString());

                    CompletedSecs = beforeCompletedSecs + (completed_secs * 10 + msecs);

                    //0sw.WriteLine("completed secs="+CompletedSecs.ToString());

                    //int totalsex = LastCutArgs.TotalDuration / 10;

                    //1int progress = (int)((decimal)CompletedSecs * 100 / (decimal)(LastCutArgs.TotalDuration));

                    //1bwConvert.ReportProgress(progress);
                    bwConvert.ReportProgress(0, CompletedSecs);
                }
                catch { }

            }
            else if (line != null && line.ToLower().StartsWith("error"))
            {
                if (!time_str_found)
                {
                    errstr += line;
                }
            }
        }

        void psFFMpeg_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //Console.WriteLine("OUTPUT:"+e.Data);
        }

        #endregion

        #region Options

        void tiWhenFinished_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem ti2 in whenFinishedToolStripMenuItem.DropDownItems)
            {
                ti2.Checked = false;
            }

            ToolStripMenuItem ti = (ToolStripMenuItem)sender;
            ti.Checked = true;

        }

        #endregion

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions f = new frmOptions();
            f.ShowDialog();
        }

        private void btnAddClip_Click(object sender, EventArgs e)
        {           
            UpdatePosAndSizeB();

            VWRClip vwr = new VWRClip();

            vwr.X = (int)nudX.Value;
            vwr.Y = (int)nudY.Value;
            vwr.Width = (int)nudWidth.Value;
            vwr.Height = (int)nudHeight.Value;

            vwr.XB = (int)nudXB.Value;
            vwr.YB = (int)nudYB.Value;
            vwr.WidthB = (int)nudWidthB.Value;
            vwr.HeightB = (int)nudHeightB.Value;

            vwr.StartTime = tbStartTime.Value;
            vwr.EndTime = tbEndTime.Value;

            vwr.MaxX = (int)nudX.Maximum;
            vwr.MaxY = (int)nudY.Maximum;
            vwr.MaxWidth = (int)nudWidth.Maximum;
            vwr.MaxHeight = (int)nudHeight.Maximum;

            vwr.MaxXB = (int)nudXB.Maximum;
            vwr.MaxYB = (int)nudYB.Maximum;
            vwr.MaxWidthB = (int)nudWidthB.Maximum;
            vwr.MaxHeightB = (int)nudHeightB.Maximum;

            VWRClips.Add(vwr);

            cmbClip.Items.Add(TranslateHelper.Translate("Clip") + " #" + (cmbClip.Items.Count + 1).ToString());

            cmbClip.SelectedIndex = cmbClip.Items.Count - 1;
        }

        private bool InRemoveClip = false;

        private void btnRemoveClip_Click(object sender, EventArgs e)
        {
            try
            {

                InRemoveClip = true;

                if (VWRClips.Count < 2) return;

                int sel = cmbClip.SelectedIndex;

                VWRClips.RemoveAt(sel);

                cmbClip.Items.Clear();

                for (int k = 0; k < VWRClips.Count; k++)
                {
                    cmbClip.Items.Add(TranslateHelper.Translate("Clip") + " #" + (cmbClip.Items.Count + 1).ToString());
                }

                if (VWRClips.Count > sel)
                {
                    cmbClip.SelectedIndex = sel;
                }
                else
                {
                    cmbClip.SelectedIndex = sel - 1;
                }
            }
            finally
            {
                InRemoveClip = false;
            }
        }

        private void SaveLastVWRClip()
        {
            VWRClips[LastVWRSelectedIndex].X = (int)nudX.Value;
            VWRClips[LastVWRSelectedIndex].Y = (int)nudY.Value;
            VWRClips[LastVWRSelectedIndex].Width = (int)nudWidth.Value;
            VWRClips[LastVWRSelectedIndex].Height = (int)nudHeight.Value;

            VWRClips[LastVWRSelectedIndex].XB = (int)nudXB.Value;
            VWRClips[LastVWRSelectedIndex].YB = (int)nudYB.Value;
            VWRClips[LastVWRSelectedIndex].WidthB = (int)nudWidthB.Value;
            VWRClips[LastVWRSelectedIndex].HeightB = (int)nudHeightB.Value;

            VWRClips[LastVWRSelectedIndex].StartTime = tbStartTime.Value;
            VWRClips[LastVWRSelectedIndex].EndTime = tbEndTime.Value;

            VWRClips[LastVWRSelectedIndex].MaxX = (int)nudX.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxY = (int)nudY.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxWidth = (int)nudWidth.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxHeight = (int)nudHeight.Maximum;

            VWRClips[LastVWRSelectedIndex].MaxXB = (int)nudXB.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxYB = (int)nudYB.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxWidthB = (int)nudWidthB.Maximum;
            VWRClips[LastVWRSelectedIndex].MaxHeightB = (int)nudHeightB.Maximum;

            VWRClips[LastVWRSelectedIndex].ZoomOrPan = rdZoom.Checked;
            //VWRClips[LastVWRSelectedIndex].ZoomFactor = nudZoomFactor.Value;
        }

        private int LastVWRSelectedIndex = 0;

        private void cmbClip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!InRemoveClip)
            {
                VWRClips[LastVWRSelectedIndex].X = (int)nudX.Value;
                VWRClips[LastVWRSelectedIndex].Y = (int)nudY.Value;
                VWRClips[LastVWRSelectedIndex].Width = (int)nudWidth.Value;
                VWRClips[LastVWRSelectedIndex].Height = (int)nudHeight.Value;

                VWRClips[LastVWRSelectedIndex].XB = (int)nudXB.Value;
                VWRClips[LastVWRSelectedIndex].YB = (int)nudYB.Value;
                VWRClips[LastVWRSelectedIndex].WidthB = (int)nudWidthB.Value;
                VWRClips[LastVWRSelectedIndex].HeightB = (int)nudHeightB.Value;


                VWRClips[LastVWRSelectedIndex].StartTime = tbStartTime.Value;
                VWRClips[LastVWRSelectedIndex].EndTime = tbEndTime.Value;

                VWRClips[LastVWRSelectedIndex].MaxX = (int)nudX.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxY = (int)nudY.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxWidth = (int)nudWidth.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxHeight = (int)nudHeight.Maximum;

                VWRClips[LastVWRSelectedIndex].MaxXB = (int)nudXB.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxYB = (int)nudYB.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxWidthB = (int)nudWidthB.Maximum;
                VWRClips[LastVWRSelectedIndex].MaxHeightB = (int)nudHeightB.Maximum;

                VWRClips[LastVWRSelectedIndex].ZoomOrPan = rdZoom.Checked;
                //VWRClips[LastVWRSelectedIndex].ZoomFactor = nudZoomFactor.Value;
            }

            int sel = cmbClip.SelectedIndex;

            LastVWRSelectedIndex = cmbClip.SelectedIndex;

            VWRClip vwr = VWRClips[LastVWRSelectedIndex];

            nudX.Maximum = vwr.MaxX;
            nudY.Maximum = vwr.MaxY;
            nudWidth.Maximum = vwr.MaxWidth;
            nudHeight.Maximum = vwr.MaxHeight;

            nudXB.Maximum = vwr.MaxXB;
            nudYB.Maximum = vwr.MaxYB;
            nudWidthB.Maximum = vwr.MaxWidthB;
            nudHeightB.Maximum = vwr.MaxHeightB;

            nudX.Value = vwr.X;
            nudY.Value = vwr.Y;
            nudWidth.Value = vwr.Width;
            nudHeight.Value = vwr.Height;

            nudXB.Value = vwr.XB;
            nudYB.Value = vwr.YB;
            nudWidthB.Value = vwr.WidthB;
            nudHeightB.Value = vwr.HeightB;
            
            tbStartTime.Value = vwr.StartTime;
            tbEndTime.Value = vwr.EndTime;

            rdZoom.Checked = vwr.ZoomOrPan;
            rdCrop.Checked = !vwr.ZoomOrPan;

            //nudZoomFactor.Value = vwr.ZoomFactor;

            tbStartTime_Scroll(null, null);
            tbEndTime_Scroll(null, null);

            btnStartUpdateImage_Click(null, null);

            UpdatePicFromControls();
            UpdatePicFromControlsB();

        }

        private void useFFMPEG64bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useFFMPEG64bitToolStripMenuItem.Checked = true;
            useFFMPEG32bitToolStripMenuItem.Checked = false;

            Properties.Settings.Default.FFMPEG64Bit = 1;
        }

        private void useFFMPEG32bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useFFMPEG64bitToolStripMenuItem.Checked = false;
            useFFMPEG32bitToolStripMenuItem.Checked = true;

            Properties.Settings.Default.FFMPEG64Bit = 2;
        }        

        private void copyEXIFInformationFromSourceVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CopyEXIF = copyEXIFInformationFromSourceVideoToolStripMenuItem.Checked;            
        }

        private void keepCreationDateFromSourceVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeepCreationDate = keepCreationDateFromSourceVideoToolStripMenuItem.Checked;
        }

        private void keepLastModificationDateFromSourceVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeepLastModDate = keepLastModificationDateFromSourceVideoToolStripMenuItem.Checked;
        }

        private void grpStartTime_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, grpStartTime.Width - 1, grpStartTime.Height - 1));
        }

        private void grpEndTime_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, grpEndTime.Width - 1, grpEndTime.Height - 1));
        }

        private void picB1_MouseDown(object sender, MouseEventArgs e)
        {
            ActivePicB = (PictureBox)sender;
            PicMouseDownB = true;
        }

        private bool DraggingHeight = false;

        private void picB1_MouseUp(object sender, MouseEventArgs e)
        {
            PicMouseDownB = false;

            //ActivePic = null;            

            if (sender == picB5 || sender == picB7)
            {
                DraggingHeight = true;
            }
            else
            {
                DraggingHeight = false;
            }

            UpdatePicFromControlsB();            
        }

        private void picB1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!PicMouseDownB) return;

            //MessageBox.Show("left=" + ActivePic.Left.ToString() + " x=" + e.X.ToString());          


            if ((ActivePicB.Left + e.X) > VideoScaler.PicMaxWidth)
            {
                return;
            }

            if ((ActivePicB.Top + e.Y) > VideoScaler.PicMaxHeight)
            {
                return;
            }

            ActivePicB.Left += e.X;
            ActivePicB.Top += e.Y;

            if (ActivePicB == picB1)
            {
                picB2.Top = picB1.Top;
                picB5.Top = picB1.Top;
                picB4.Left = picB1.Left;
                picB8.Left = picB1.Left;
            }
            else if (ActivePicB == picB2)
            {
                picB1.Top = picB2.Top;
                picB5.Top = picB2.Top;
                picB3.Left = picB2.Left;
                picB6.Left = picB2.Left;
            }
            else if (ActivePicB == picB3)
            {
                picB4.Top = picB3.Top;
                picB2.Left = picB3.Left;

                picB6.Left = picB3.Left;
                picB7.Top = picB3.Top;

            }
            else if (ActivePicB == picB4)
            {
                picB1.Left = picB4.Left;
                picB3.Top = picB4.Top;
                picB8.Left = picB4.Left;
                picB7.Top = picB4.Top;
            }
            else if (ActivePicB == picB5)
            {
                picB1.Top = picB5.Top;
                picB2.Top = picB5.Top;
            }
            else if (ActivePicB == picB6)
            {
                picB2.Left = picB6.Left;
                picB3.Left = picB6.Left;
            }
            else if (ActivePicB == picB7)
            {
                picB3.Top = picB7.Top;
                picB4.Top = picB7.Top;
            }
            else if (ActivePicB == picB8)
            {
                picB1.Left = picB8.Left;
                picB4.Left = picB8.Left;
            }

            picB5.Left = picB1.Left + (int)((picB2.Left - picB1.Left) / 2);
            picB7.Left = picB4.Left + (int)((picB3.Left - picB4.Left) / 2);
            picB6.Top = picB2.Top + (int)((picB3.Top - picB2.Top) / 2);
            picB8.Top = picB1.Top + (int)((picB4.Top - picB1.Top) / 2);

            picB1.Left = Math.Max(1, Math.Min(picB1.Left, VideoScaler.PicMaxWidth));
            picB2.Left = Math.Max(1, Math.Min(picB2.Left, VideoScaler.PicMaxWidth));
            picB3.Left = Math.Max(1, Math.Min(picB3.Left, VideoScaler.PicMaxWidth));
            picB4.Left = Math.Max(1, Math.Min(picB4.Left, VideoScaler.PicMaxWidth));

            picB1.Top = Math.Max(1, Math.Min(picB1.Top, VideoScaler.PicMaxHeight));
            picB2.Top = Math.Max(1, Math.Min(picB2.Top, VideoScaler.PicMaxHeight));
            picB3.Top = Math.Max(1, Math.Min(picB3.Top, VideoScaler.PicMaxHeight));
            picB4.Top = Math.Max(1, Math.Min(picB4.Top, VideoScaler.PicMaxHeight));

            FillPicRectangleB();
            UpdatePosAndSizeB();
        }

        private void FillPicRectangleB()
        {
            picMovieB.Invalidate();
            /*
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //picMovie.BackgroundImage = picMovie.Image;

            //handle size
            int hdsize=(pic1.Width)/2;

            Graphics g = picMovie.CreateGraphics();
            
            //g.Clear(picMovie.BackColor);
            
            g.DrawImage(Properties.Resources.DSC00059, 0, 0, picMovie.Width, picMovie.Height);

            System.Drawing.Drawing2D.HatchBrush hb=new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,Color.Red);

            g.FillRectangle(hb, pic1.Left -picMovie.Left + hdsize, pic1.Top -picMovie.Top + hdsize, pic2.Left - pic1.Left, pic3.Top - pic2.Top);

            picMovie.Update();*/
        }

        public void UpdatePicFromControlsB()
        {
            if (VideoScaler == null) return;

            nudXB_ValueChanged(null, null);
            nudYB_ValueChanged(null, null);
            nudWidthB_ValueChanged(null, null);
            nudHeightB_ValueChanged(null, null);
        }

        private void picMovieDest_MouseDown(object sender, MouseEventArgs e)
        {
            if (picInRectB)
            {
                picMoveRectB = true;

                picMXB = VideoScaler.ScalePosPicToVideo(e.X);
                picMYB = VideoScaler.ScalePosPicToVideo(e.Y);

                /*
                picMXB = e.X;
                picMYB = e.Y;
                */
            }
        }

        private void picMovieDest_MouseMove(object sender, MouseEventArgs e)
        {
            if (!picMoveRectB)
            {
                Rectangle rect = new Rectangle(picB1.Left, picB1.Top, picB2.Left - picB1.Left, picB3.Top - picB2.Top);

                Point p = new Point(e.X, e.Y);

                if (rect.Contains(p))
                {
                    picMovieB.Cursor = Cursors.SizeAll;

                    picInRectB = true;
                }
                else
                {
                    picMovieB.Cursor = null;

                    picInRectB = false;
                }
            }
            else
            {
                //3nudX.Value = Math.Min(nudX.Maximum, Math.Max(nudX.Minimum, nudX.Value + VideoScaler.ScalePosPicToVideo(e.X) - picMX));
                //3nudY.Value = Math.Min(nudY.Maximum, Math.Max(nudY.Minimum, nudY.Value + VideoScaler.ScalePosPicToVideo(e.Y) - picMY));

                nudXB.Value = Math.Min(nudXB.Maximum, Math.Max(nudXB.Minimum, nudXB.Value + VideoScaler.ScalePosPicToVideo(e.X) - picMXB));
                nudYB.Value = Math.Min(nudYB.Maximum, Math.Max(nudYB.Minimum, nudYB.Value + VideoScaler.ScalePosPicToVideo(e.Y) - picMYB));

                picMXB = VideoScaler.ScalePosPicToVideo(e.X);
                picMYB = VideoScaler.ScalePosPicToVideo(e.Y);
            }
        }                

        private void picMovieDest_MouseUp(object sender, MouseEventArgs e)
        {
            PicMouseDownB = false;
            picMovieB.Cursor = null;
            picMoveRectB = false;

            nudXB_ValueChanged(null, null);
            nudYB_ValueChanged(null, null);

            picMovieB.Invalidate();
        }

        private void chkKeepAspectRatio_Click(object sender, EventArgs e)
        {
            DraggingHeight = false;

            nudWidthB_ValueChanged(null, null);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            frmZoomCropOptions f = new frmZoomCropOptions();
            f.ShowDialog(this);
        }

        private void zoomAndCropOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZoomCropOptions f = new frmZoomCropOptions();
            f.ShowDialog(this);
        }

        private void rdZoom_Click(object sender, EventArgs e)
        {
            //picMovieB.Visible = rdZoom.Checked;
        }

        private void rdCrop_Click(object sender, EventArgs e)
        {
            //picMovieB.Visible = rdZoom.Checked;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) return;

            picMovie.Width=this.ClientRectangle.Width / 2-7;
            picMovie.Left = 0;
            picMovieB.Left = picMovie.Right + 7;
            picMovieB.Width= this.ClientRectangle.Width / 2-7;

            picMovie.Height = grpStartTime.Top - picMovie.Top;
            picMovieB.Height = grpStartTime.Top - picMovie.Top;

            picMovie.UpdateImg();
            picMovieB.UpdateImg();
        }

        private void tsbPreviewImage_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //this.Enabled = false;                

                EnableDisableForm(false);

                if (frmMain.Instance.Filepath == string.Empty)
                {
                    Module.ShowMessage("Please open a Media File first !");
                    return;
                }

                SaveLastVWRClip();

                Properties.Settings.Default.Crop = rdCrop.Checked;

                string outputext = ".mp4";
                string outputparams = " -frames:v 1";

                string startTime = FFMpegVideoInfo.TimeMsecsToString(VWRClips[LastVWRSelectedIndex].StartTime);
                int startmsecs = TimeUpDownControl.StringToMsecs(startTime);
                int endmsecs = TimeUpDownControl.StringToMsecs(FFMpegVideoInfo.TimeMsecsToString(VWRClips[LastVWRSelectedIndex].EndTime));
                string endTime = FFMpegVideoInfo.TimeMsecsToString(VWRClips[LastVWRSelectedIndex].EndTime);

                int durationmsecs = endmsecs - startmsecs;
                string duration = TimeUpDownControl.MsecsToSecsString(durationmsecs);

                int x = (int)VWRClips[LastVWRSelectedIndex].X;
                int y = (int)VWRClips[LastVWRSelectedIndex].Y;
                int width = (int)VWRClips[LastVWRSelectedIndex].Width;
                int height = (int)VWRClips[LastVWRSelectedIndex].Height;

                if (width % 2 == 1)
                {
                    width = width - 1;
                }

                if (height % 2 == 1)
                {
                    height = height - 1;
                }

                int xb = (int)VWRClips[LastVWRSelectedIndex].XB;
                int yb = (int)VWRClips[LastVWRSelectedIndex].YB;
                int widthb = (int)VWRClips[LastVWRSelectedIndex].WidthB;
                int heightb = (int)VWRClips[LastVWRSelectedIndex].HeightB;

                if (widthb % 2 == 1)
                {
                    widthb = widthb - 1;
                }

                if (heightb % 2 == 1)
                {
                    heightb = heightb - 1;
                }


                List<VWRClip> lstvwr = new List<VWRClip>();
                lstvwr.Add(VWRClips[LastVWRSelectedIndex]);

                JoinArgsHelper jhelper = new JoinArgsHelper();

                JoinArgs res = jhelper.GetZoomArgs(Filepath, outputext, outputparams, Properties.Settings.Default.OF1stPassParameters, Properties.Settings.Default.OF2ndPassParameters,
                Properties.Settings.Default.OFVideoBitrate, Properties.Settings.Default.OFFrameRate,
                Properties.Settings.Default.OFVideoSize, Properties.Settings.Default.OFAspectRatio,
                Properties.Settings.Default.OFTwoPass, Properties.Settings.Default.OFDeinterlace,
                Properties.Settings.Default.OFAudioBitrate, Properties.Settings.Default.OFSampleRate,
                Properties.Settings.Default.OFAudioChannels, Properties.Settings.Default.OFVolume,
                Properties.Settings.Default.OFAudioNormalize, Properties.Settings.Default.OFMute,
                Properties.Settings.Default.OFKeepMetadata, startTime, endTime, x, y, width, height,
                xb, yb, widthb, heightb,
                DurationStr, DurationMsecs, 0, lstvwr);

                CreateFFMpegProcess();

                //Clipboard.Clear();
                //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                psFFMpeg.StartInfo.Arguments = res.ArgsCrop;

                bwConvert.RunWorkerAsync();

                while (bwConvert.IsBusy)
                {
                    Application.DoEvents();
                }

                if (!Properties.Settings.Default.Crop)
                {
                    CreateFFMpegProcess();

                    //Clipboard.Clear();
                    //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                    psFFMpeg.StartInfo.Arguments = res.ArgsOverlayCut;

                    bwConvert.RunWorkerAsync();

                    while (bwConvert.IsBusy)
                    {
                        Application.DoEvents();
                    }

                    CreateFFMpegProcess();

                    //Clipboard.Clear();
                    //Clipboard.SetText(res.Args + " \"" + res.JoinFile + "\"");

                    psFFMpeg.StartInfo.Arguments = res.ArgsOverlay;

                    bwConvert.RunWorkerAsync();

                    while (bwConvert.IsBusy)
                    {
                        Application.DoEvents();
                    }


                    string tempimg = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

                    if (System.IO.File.Exists(res.OverlayOutputFilepath))
                    {
                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = " -i \"" + res.OverlayOutputFilepath + "\" \"" + tempimg + "\"";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        this.Cursor = null;
                        //this.Enabled = true;

                        EnableDisableForm(true);

                        frmPreviewImage f = new frmPreviewImage(tempimg);
                        f.Show(this);
                    }
                }
                else
                {
                    string tempimg = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

                    if (System.IO.File.Exists(res.CropOutputFilepath))
                    {
                        CreateFFMpegProcess();

                        psFFMpeg.StartInfo.Arguments = " -i \"" + res.CropOutputFilepath + "\" \"" + tempimg + "\"";

                        bwConvert.RunWorkerAsync();

                        while (bwConvert.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        this.Cursor = null;
                        //this.Enabled = true;

                        EnableDisableForm(true);

                        frmPreviewImage f = new frmPreviewImage(tempimg);
                        f.Show(this);
                    }
                }


            }
            catch (Exception ex)
            {
                Module.ShowError(ex);
            }
            finally
            {
                this.Cursor = null;
                //this.Enabled = true;

                EnableDisableForm(true);
            }
        }

        private void btnSelectEntireArea_Click(object sender, EventArgs e)
        {
            nudX.Value = 1;
            nudY.Value = 1;
            nudWidth.Value = nudWidth.Maximum;
            nudHeight.Value = nudHeight.Maximum;
        }

        private void btnSelectEntireAreaB_Click(object sender, EventArgs e)
        {
            nudXB.Value = 1;
            nudYB.Value = 1;
            nudWidthB.Value = nudWidthB.Maximum;
            nudHeightB.Value = nudHeightB.Maximum;
        }

        private void timJoinVideos_Tick_1(object sender, EventArgs e)
        {

        }        
    }
}
