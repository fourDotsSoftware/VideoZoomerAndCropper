using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace VideoZoomerAndCropper
{
    class VideoThumbnail
    {
        public Bitmap ThumbnailImage = null;

        public VideoThumbnail(string filepath,string time_position)
        {
            try
            {
                //frmClip.Instance.Cursor = Cursors.WaitCursor;
                frmMain.Instance.Cursor = Cursors.WaitCursor;

                string iotempfile = System.IO.Path.GetTempFileName();

                System.IO.File.Delete(iotempfile);

                iotempfile += ".jpg";

                Process psFFMpeg = new Process();

                if (Properties.Settings.Default.FFMPEG64Bit == 1)
                {
                    psFFMpeg.StartInfo.FileName = "ffmpeg64 ";
                }
                else
                {
                    psFFMpeg.StartInfo.FileName = "ffmpeg32 ";
                }

                psFFMpeg.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;

                psFFMpeg.StartInfo.CreateNoWindow = true;
                psFFMpeg.StartInfo.UseShellExecute = false;
                psFFMpeg.StartInfo.RedirectStandardError = true;
                psFFMpeg.StartInfo.RedirectStandardOutput = true;

                psFFMpeg.OutputDataReceived += psFFMpeg_OutputDataReceived;
                psFFMpeg.ErrorDataReceived += psFFMpeg_ErrorDataReceived;

                psFFMpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                LastFFMpegOutput = "";

                last_line = "";            
                //1psImage.StartInfo.Arguments = " -ss "+time_position+" -i \"" + filepath + "\" -y -s 128x65 -f image2 -vcodec mjpeg -vframes 1 \"" + iotempfile + "\"";
                psFFMpeg.StartInfo.Arguments = " -ss " + time_position + " -i \"" + filepath + "\" -y -f image2 -vcodec mjpeg -vframes 1 \"" + iotempfile + "\"";

                psFFMpeg.Start();

                psFFMpeg.BeginErrorReadLine();
                psFFMpeg.BeginOutputReadLine();

                psFFMpeg.WaitForExit();

                psFFMpeg.Close();

                System.Threading.Thread.Sleep(500);

                Image img = Image.FromFile(iotempfile);
                Image img2 = (Image)img.Clone();

                /*
                int iwidth = img2.Width;
                int iheight = img2.Height;
                int newheight = 0;
                int newwidth = 0;

                if (iwidth > iheight)
                {
                    newheight = (int)(((float)picPlayer.Width / (float)img2.Width) * img2.Height);
                    newwidth = picPlayer.Width;

                }
                else
                {
                    newwidth = (int)(((float)picPlayer.Height / (float)img2.Height) * img2.Width);
                    newheight = picPlayer.Height;
                }
                Bitmap bmp = new Bitmap(img2, newwidth, newheight);
                 
                */

                Bitmap bmp = new Bitmap(img2);
                img.Dispose();
                img2.Dispose();
                ThumbnailImage = bmp;


                try
                {
                    System.IO.File.Delete(iotempfile);
                }
                catch { }

                GC.Collect();

                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                frmMain.Instance.Cursor = null;
                Module.ShowError("Error could not get Thumbnail !", ex);
            }
            finally
            {
                //frmClip.Instance.Cursor = null;

                frmMain.Instance.Cursor = null;
            }
        }

        string LastFFMpegOutput = "";
        string last_line = "";

        void psFFMpeg_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string line = e.Data;

            Console.WriteLine(line);

            if (line != null)
            {
                last_line = line;
            }

            LastFFMpegOutput += line + "\r\n";
        }

        void psFFMpeg_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

        }
    }
}
