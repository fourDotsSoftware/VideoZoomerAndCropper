using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VideoZoomerAndCropper
{
    public class DownloadFFMPEG32bit
    {
        public static bool CheckDownloadFFMPEG32bit()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "get32or64bit.exe");
            p.Start();
            p.WaitForExit();

            if (p.ExitCode == 64)
            {
                return true;
            }
            else if (p.ExitCode == 32)
            {
                if (!System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "DownloadedFFMPEG32bit.txt")))
                {
                    frmDownloadRequiredUpdate f = new frmDownloadRequiredUpdate("ffmpeg.exe",
                    "https://www.4dots-software.com/ffmpeg/32bit/ffmpeg.exe",
                    System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe"),
                    System.IO.Path.Combine(Application.StartupPath, "DownloadedFFMPEG32bit.txt"),
                    TranslateHelper.Translate("Downloading required FFMPEG 32bit Version"));

                    f.ShowDialog(frmMain.Instance);
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }
}
