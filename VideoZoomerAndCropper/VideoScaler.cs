using System;
using System.Collections.Generic;
using System.Text;

namespace VideoZoomerAndCropper
{
    public class VideoScaler
    {
        private int _Width = 0;
        private int _Height = 0;

        public double Scale = 0.0d;

        public int PicMaxWidth
        {
            get
            {
                return (int)(Width * Scale);
            }
        }

        public int PicMaxHeight
        {
            get
            {
                return (int)(Height * Scale);
            }
        }
        public int Width
        {
            get { return _Width; }

            set
            {
               // video max pic width
               // 
                _Width = value;

                frmMain.Instance.nudX.Maximum = value;
                frmMain.Instance.nudWidth.Maximum = value;

                frmMain.Instance.nudXB.Maximum = value;
                frmMain.Instance.nudWidthB.Maximum = value;
            }
        }

        public int Height
        {
            get { return _Height; }

            set
            {
                // video max pic width
                // 
                _Height = value;

                frmMain.Instance.nudY.Maximum = value;
                frmMain.Instance.nudHeight.Maximum = value;

                frmMain.Instance.nudYB.Maximum = value;
                frmMain.Instance.nudHeightB.Maximum = value;
            }

        }

        public VideoScaler(string filepath)
        {
            frmMain.Instance.InUpdatePosAndSize = true;
            //3FFMpegVideoInfo fvi = new FFMpegVideoInfo(filepath);

            FFMPEGInfo fvi = new FFMPEGInfo(filepath);

            Width = fvi.VideoWidth;
            Height = fvi.VideoHeight;

            //Width = fvi.Width;
            //Height = fvi.Height;

            frmMain.Instance.DurationStr = fvi.DurationStr;
            frmMain.Instance.DurationMsecs = fvi.DurationMsecs;

            frmMain.Instance.InUpdatePosAndSize = false;
        }

        public void UpdateScale()
        {
            // Width Height
            // picMovie.Width x
            
            // x=picmovie.width*Height/Width

            double dh = (double)Height;
            double dw = (double)Width;
            double ddw = (double)(frmMain.Instance.picMovie.Width);
            double ddh = (double)(frmMain.Instance.picMovie.Height);

            double nh = (ddw * dh )/ dw;
            double nw = (ddh * dw) / dh;

            if (nh <= frmMain.Instance.picMovie.Height)
            {
                Scale = ddw / dw;
            }
            else
            {
                Scale = ddh / dh;
            }

            //Scale = (double)frmMain.Instance.picMovie.Width / (double)Width;

            //3Scale = (double)frmMain.Instance.picMovie.Height / (double)Height;

            //3
            /*
            if (Width > Height)
            {
                Scale = (double)frmMain.Instance.picMovie.Height / (double)Height;                
            }
            else
            {
                Scale = (double)frmMain.Instance.picMovie.Width / (double)Width;
            }
            */
            
        }
       
        public int ScalePosVideoToPic(int pix)
        {
            // video max pic max
            //  pix    ; x


            return (int)(pix * Scale);
        }

        public int ScalePosPicToVideo(int pix)
        {
            return (int)((double)pix / (double)Scale);
        }


    }
}
