using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace VideoZoomerAndCropper
{
    public class FFMpegVideoInfo
    {
        public int Width = 0;
        public int Height = 0;
        public string DurationStr = "";
        public int DurationMsecs = 0;

        public FFMpegVideoInfo(string filepath)
        {
            try
            {
                //frmClip.Instance.Cursor = Cursors.WaitCursor;

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

                psFFMpeg.StartInfo.Arguments = " -i \"" + filepath;

                psFFMpeg.Start();

                psFFMpeg.BeginErrorReadLine();
                psFFMpeg.BeginOutputReadLine();

                psFFMpeg.WaitForExit();

                psFFMpeg.Close();

                System.IO.StringReader sr = new System.IO.StringReader(LastFFMpegOutput);

                string line = null;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("Stream #0"))
                    {
                        // if already set , continue
                        if (Width != 0 || Height != 0)
                        {
                            continue;
                        }
                        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(
@"(?<!\([^()]*)[^,]+(?![^()]*\))");
                        string wh = "";

                        if (rex.IsMatch(line))
                        {
                            System.Text.RegularExpressions.MatchCollection macol = rex.Matches(line);
                            wh = macol[2].Value;

                        }
                        /*
                        string[] props = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        string wh = props[2];
                        */
                        int spos = wh.IndexOf("x");
                        Width = int.Parse(wh.Substring(0, spos));
                        int epos = wh.IndexOf(" ", spos);
                        if (epos < 0)
                        {
                            epos = wh.Length-1;
                        }
                        
                        //700x200 [sar
                        //01234 : 1,4-1
                        //700x200
                        //0123 : 1,4-1
                        Height = int.Parse(wh.Substring(spos + 1,epos-spos));

                        //System.Windows.Forms.MessageBox.Show(Width.ToString() + "x" + Height.ToString());
                        break;

                    }
                    else if (line.Trim().StartsWith("Duration: "))
                    {
                        line=line.Trim();
                        int epos=line.IndexOf(",");
                        DurationStr=line.Substring("Duration: ".Length,epos-"Duration: ".Length);

                        //3DurationMsecs = TimeStringToMsecs(DurationStr)-3; //fix bug of ffmpeg
                        DurationMsecs = TimeStringToMsecs(DurationStr) - 10; //fix bug of ffmpeg
                        DurationStr = TimeMsecsToString(DurationMsecs);
                    }
                }

                if (Width == 0 && Height == 0)
                {
                    Module.ShowMessage("Error could not find Video Width and Height !");

                }

                if (DurationMsecs == 0)
                {
                    Module.ShowMessage("Error could not find Duration of Video !");
                }
            }
            catch (Exception ex)
            {
                Module.ShowError("Error could not get Width and Height of Video !", ex);
            }
            finally
            {
                
            }
        }

        public static int TimeStringToMsecs(string str)
        {
            TimeSpan ts = new TimeSpan(0, int.Parse(str.Substring(0, 2)),
                            int.Parse(str.Substring(3, 2)),
                            int.Parse(str.Substring(6, 2)),
                                int.Parse(str.Substring(9, 2)));

            return (int)ts.TotalMilliseconds;            
        }

        public static string TimeMsecsToString(int msecs)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, msecs);

            string str = ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2") + "." + ts.Milliseconds.ToString("D2");

            return str;
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
