using System;
using System.Collections.Generic;
using System.Text;

namespace VideoZoomerAndCropper
{
    public class picLogo : System.Windows.Forms.PictureBox
    {
        public System.Drawing.Image Thumbnail = null;
        public bool _PicLogoVisible = false;

        public bool PicLogoVisible
        {
            get
            {
                return _PicLogoVisible;
            }
            set
            {
                if (value)
                {
                    _PicLogoVisible = value;

                    frmMain.Instance.pic1.Visible = value;
                    frmMain.Instance.pic2.Visible = value;
                    frmMain.Instance.pic3.Visible = value;
                    frmMain.Instance.pic4.Visible = value;                    
                    frmMain.Instance.pic5.Visible = value;
                    frmMain.Instance.pic6.Visible = value;
                    frmMain.Instance.pic7.Visible = value;
                    frmMain.Instance.pic8.Visible = value;

                    frmMain.Instance.picB1.Visible = value;
                    frmMain.Instance.picB2.Visible = value;
                    frmMain.Instance.picB3.Visible = value;
                    frmMain.Instance.picB4.Visible = value;
                    frmMain.Instance.picB5.Visible = value;
                    frmMain.Instance.picB6.Visible = value;
                    frmMain.Instance.picB7.Visible = value;
                    frmMain.Instance.picB8.Visible = value;
                }
            }
        }
        
        public picLogo() : base()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            //this.BackgroundImage = Properties.Resources.DSC00059;
            PicLogoVisible = false;
        }

        public void UpdateImg()
        {
            if (frmMain.Instance != null && frmMain.Instance.VideoScaler != null && frmMain.Instance.Filepath!=string.Empty)
            {
                frmMain.Instance.VideoScaler.UpdateScale();

                int newwidth = frmMain.Instance.VideoScaler.ScalePosVideoToPic(Thumbnail.Width);
                int newheight = frmMain.Instance.VideoScaler.ScalePosVideoToPic(Thumbnail.Height);

                System.Drawing.Bitmap bmp2=(System.Drawing.Bitmap)Thumbnail.Clone();

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bmp2, newwidth, newheight);

                this.BackgroundImage = (System.Drawing.Image)bmp.Clone();

                frmMain.Instance.UpdatePicFromControls();
                frmMain.Instance.UpdatePicFromControlsB();
            }
        }

        protected override void OnResize(EventArgs e)
        {            
            base.OnResize(e);

            if (Thumbnail != null)
            {
                //UpdateImg();   
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {          

            if (frmMain.Instance == null || !PicLogoVisible)
            {
                base.OnPaint(pe);
                return;
            }

            if (this == frmMain.Instance.picMovie)
            {
                //base.OnPaint(pe);

                //handle size
                int hdsize = (frmMain.Instance.pic1.Width) / 2;

                System.Drawing.Graphics g = pe.Graphics;

                //g.Clear(this.BackColor);

                //g.DrawImage(Properties.Resources.DSC00059, 0, 0, this.Width, this.Height);

                System.Drawing.Drawing2D.HatchBrush hb = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.Red, System.Drawing.Color.Transparent);

                //g.FillRectangle(hb, frmMain.Instance.pic1.Left - this.Left + hdsize, frmMain.Instance.pic1.Top - this.Top + hdsize, frmMain.Instance.pic2.Left - frmMain.Instance.pic1.Left, frmMain.Instance.pic3.Top - frmMain.Instance.pic2.Top);
                g.FillRectangle(hb, frmMain.Instance.pic1.Left + hdsize, frmMain.Instance.pic1.Top + hdsize, frmMain.Instance.pic2.Left - frmMain.Instance.pic1.Left, frmMain.Instance.pic3.Top - frmMain.Instance.pic2.Top);
            }
            else if (this == frmMain.Instance.picMovieB)
            {
                //base.OnPaint(pe);

                //handle size
                int hdsize = (frmMain.Instance.picB1.Width) / 2;

                System.Drawing.Graphics g = pe.Graphics;

                //g.Clear(this.BackColor);

                //g.DrawImage(Properties.Resources.DSC00059, 0, 0, this.Width, this.Height);

                System.Drawing.Drawing2D.HatchBrush hb = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.LawnGreen, System.Drawing.Color.Transparent);

                //g.FillRectangle(hb, frmMain.Instance.pic1.Left - this.Left + hdsize, frmMain.Instance.pic1.Top - this.Top + hdsize, frmMain.Instance.pic2.Left - frmMain.Instance.pic1.Left, frmMain.Instance.pic3.Top - frmMain.Instance.pic2.Top);
                g.FillRectangle(hb, frmMain.Instance.picB1.Left + hdsize, frmMain.Instance.picB1.Top + hdsize, frmMain.Instance.picB2.Left - frmMain.Instance.picB1.Left, frmMain.Instance.picB3.Top - frmMain.Instance.picB2.Top);
            }
        }
    }

    public class PicHandle : System.Windows.Forms.PictureBox
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);

            pe.Graphics.DrawRectangle(System.Drawing.Pens.White, new System.Drawing.Rectangle(1, 1, this.Width - 3, this.Height - 3));
        }
    }

    public class PreviewTrackBar : System.Windows.Forms.TrackBar
    {
        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);        

            // w maxval
            // e.x ;x

            decimal w = (decimal)this.ClientRectangle.Width - 2 * this.Margin.Left;
            decimal ex = (decimal)e.X;
            decimal maxval = this.Maximum;

            decimal x = (ex * maxval) / w;
            int ix = (int)x;            

                try
                {
                    this.Value = ix;
                }
                catch { }
        }            
    }

    public class VWRClip : IComparable<VWRClip>
    {
        public int X = 0;
        public int Y = 0;
        public int Width = 1;
        public int Height = 1;

        public int XB = 0;
        public int YB = 0;
        public int WidthB = 1;
        public int HeightB = 1;

        public int StartTime = -1;
        public int EndTime = -1;

        public int MaxX = 0;
        public int MaxY = -0;
        public int MaxWidth = 1;
        public int MaxHeight = 1;

        public int MaxXB = 0;
        public int MaxYB = 0;
        public int MaxWidthB = 1;
        public int MaxHeightB = 1;

        public bool ZoomOrPan = true;
        public decimal ZoomFactor = (decimal)1;

        public VWRClip()
        {

        }

        public int CompareTo(VWRClip vwr)
        {
            return this.StartTime.CompareTo(vwr.StartTime);
        }

    }
}

