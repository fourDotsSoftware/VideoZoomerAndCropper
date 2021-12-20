using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VideoZoomerAndCropper
{
    class Module
    {
        public static string ApplicationName = "Video Zoomer and Cropper";
        public static string Version = "1.2";

        public static string Ver = "3";

        public static string ShortApplicationTitle = ApplicationName + " V" + Version;
        public static string ApplicationTitle = ShortApplicationTitle + " - 4dots Software";

        public static string VersionURL = "http://cssspritestool.com/versions/video-zoomer-and-cropper.txt";

        public static string DownloadURL = "http://www.4dots-software.com/d/VideoZoomerAndCropper/";
        public static string HelpURL = "http://www.4dots-software.com/video-zoomer-and-cropper/how-to-use.php";
        public static string LangURL = "http://www.4dots-software.com/video-zoomer-and-cropper/lang/";
        public static string ProductWebpageURL = "http://www.4dots-software.com/video-zoomer-and-cropper/";

        public static string BuyURL = "http://www.4dots-software.com/store/buy-video-zoomer-and-cropper.php";

        public static string TipText = "Great Application to Zoom and Crop Videos !";

        public static string OpenFilesFilter =
            "All Supported Video Files (*.avi;*.mp4;*.mpeg;*.mpg;*.mov;*.mkv;*.flv;*.wmv;*.3gp;*.vob;*.swf;*.3g2;*.asf;*.m2p;*.m2ts;*.mts;*.m2v;*.m4v;*.mp2;*.ogm;*.ogv;*.qt;*.rm;*.rmvb;*.ts;*.webm)|" +
            "*.avi;*.mp4;*.mpeg;*.mpg;*.mov;*.mkv;*.flv;*.wmv;*.3gp;*.vob;*.swf;*.3g2;*.asf;*.m2p;*.m2ts;*.mts;*.m2v;*.m4v;*.mp2;" +
            "*.ogm;*.ogv;*.qt;*.rm;*.rmvb;*.ts;*.webm|" +
            "AVI Files (*.avi)|*.avi|MP4 Files (*.mp4)|*.mp4|MPEG Files (*.mpeg;*.mpg)|*.mpeg;*.mpg|MOV Files (*.mov)|*.mov|MKV Files (*.mkv)|*.mkv|" +
            "FLV Files (*.flv)|*.flv|WMV Files (*.wmv)|*.wmv|3GP 3G2 Files (*.3gp;*.3g2)|*.3gp;*.3g2|VOB Files (*.vob)|*.vob|" +
            "SWF Files (*.swf)|*.swf|Quicktime Files (*.qt)|*.qt|Real Media Files (*.rm;*.rmvb)|*.rm;*.rmvb|" +
            "Webm Files (*.webm)|*.webm|Other Video Files (*.asf;*.m2p;*.m2ts;*.m2v;*.m4v;*.mp2;*.ogm;*.ogv;*.ts)|*.asf;" +
            "*.m2p;*.m2ts;*.m2v;*.m4v;*.mp2;*.ogm;*.ogv;*.ts|All Files (*.*)|*.*";

        public static List<string> FilesToDelete = new List<string>();

        public static string StoreUrl = "http://www.pdfdocmerge.com/store/";
        public static string SelectedLanguage = "en-US";

        public static string[] args = null;
        public static bool IsCommandLine = false;
        public static bool IsFromWindowsExplorer = false;
                
        public static string UserDocumentsFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\4dots Free PDF Compress";

        public static bool CmdCompress = true;        
        public static string CmdUserPassword = "";
        public static string CmdOutputFile = "";
        public static string CmdOutputFolder = "";
        public static bool CmdOverwritePDF = false;
        public static bool CmdKeepBackup = false;
        public static int CmdImageQuality = 25;
        public static bool CmdCompressImages = true;
        public static string CmdLogFile = "";
        public static string CmdImportListFile = "";
        public static StreamWriter CmdLogFileWriter = null;

        
        public static List<string> CmdFiles = new List<string>();

        public static string DialogFilesFilter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

        [DllImport("shell32.dll")]
		public static extern Int32 SHParseDisplayName(
			[MarshalAs(UnmanagedType.LPWStr)]
			String pszName,
			IntPtr pbc,
			out IntPtr ppidl,
			UInt32 sfgaoIn,
			out UInt32 psfgaoOut);

        [DllImport("shell32.dll", ExactSpelling=true, SetLastError=true, CharSet=CharSet.Unicode)]
        public static extern Int32 SHOpenFolderAndSelectItems(IntPtr pidlFolder , UInt32 cidl, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl , UInt32 dwFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam,
        int lParam);

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        public static string _ProfilesFile
        = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ApplicationName + "\\ffmpeg_profiles.xml";

        public static string ProfilesFile
        {
            get
            {
                if (!System.IO.File.Exists(_ProfilesFile))
                {
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(_ProfilesFile)))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_ProfilesFile));
                    }

                    System.IO.File.Copy(System.IO.Path.Combine(Application.StartupPath, "ffmpeg_profiles.xml"),
                        _ProfilesFile, true);
                }

                return _ProfilesFile;
            }
        }

        public static string _VideoOptionsFile
        = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ApplicationName + "\\video_options.xml";

        public static string VideoOptionsFile
        {
            get
            {
                if (!System.IO.File.Exists(_VideoOptionsFile))
                {
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(_VideoOptionsFile)))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_VideoOptionsFile));
                    }

                    System.IO.File.Copy(System.IO.Path.Combine(Application.StartupPath, "video_options.xml"),
                        _VideoOptionsFile, true);
                }

                return _VideoOptionsFile;
            }
        }

        public static String HexConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        }

                
        public static string GetRelativePath(string mainDirPath, string absoluteFilePath)
        {
            string[] firstPathParts = mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] secondPathParts = absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int sameCounter = 0;
            for (int i = 0; i < Math.Min(firstPathParts.Length,
            secondPathParts.Length); i++)
            {
                if (
                !firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
                {
                    break;
                }
                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = String.Empty;
            for (int i = sameCounter; i < firstPathParts.Length; i++)
            {
                if (i > sameCounter)
                {
                    newPath += Path.DirectorySeparatorChar;
                }
                newPath += "..";
            }
            if (newPath.Length == 0)
            {
                newPath = ".";
            }
            for (int i = sameCounter; i < secondPathParts.Length; i++)
            {
                newPath += Path.DirectorySeparatorChar;
                newPath += secondPathParts[i];
            }
            return newPath;
        }

        public static void ShowMessage(string msg)
        {
            if (Module.IsCommandLine)
            {
                Console.WriteLine(TranslateHelper.Translate(msg));
            }
            else
            {
                MessageBox.Show(TranslateHelper.Translate(msg));
            }
        }

        public static DialogResult ShowQuestionDialog(string msg, string caption)
        {
            return MessageBox.Show(TranslateHelper.Translate(msg), TranslateHelper.Translate(caption), MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
        }


        public static void ShowError(Exception ex)
        {
            ShowError("Error", ex);
        }

        public static void ShowError(string msg)
        {
            if (Module.IsCommandLine)
            {
                Console.WriteLine("Error:" + msg);
            }
            else
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public static void ShowError(string msg, Exception ex)
        {
            ShowError(msg + "\n\n" + ex.Message);
        }

        public static void ShowError(string msg, string exstr)
        {
            ShowError(msg + "\n\n" + exstr);
        }

        public static DialogResult ShowQuestionDialogYesFocus(string msg, string caption)
        {
            return MessageBox.Show(TranslateHelper.Translate(msg), TranslateHelper.Translate(caption), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }


        public static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {           
            
        }

        public static bool IsWindows64Bit
        {
            get
            {
                try
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
                        return false;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
                
        public static int _Modex64 = -1;

        public static bool Modex64
        {
            get
            {
                if (_Modex64 == -1)
                {
                    if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                    {
                        _Modex64 = 1;
                        return true;
                    }
                    else
                    {
                        _Modex64 = 0;
                        return false;
                    }
                }
                else if (_Modex64 == 1)
                {
                    return true;
                }
                else if (_Modex64 == 0)
                {
                    return false;
                }
                return false;
            }
        }
                     
        public static bool CmdAddSubdirectories
        {
            get
            {
                for (int k = 0; k < Module.args.Length; k++)
                {
                    if (Module.args[k].ToLower().Trim() == "-subdirs"
                                || Module.args[k].ToLower().Trim() == "/subdirs")
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static bool IsLegalFilename(string name)
        {
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void MoveFile(string InputFile, string OutputFile)
        {
            if (System.IO.File.Exists(OutputFile))
            {                
               System.IO.File.Delete(OutputFile);                
            }

            System.IO.File.Move(InputFile, OutputFile);
        }

        public static bool DeleteApplicationSettingsFile()
        {
            try
            {
                string settingsFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();

                System.IO.FileInfo fi = new FileInfo(settingsFile);
                fi.Attributes = System.IO.FileAttributes.Normal;
                fi.Delete();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class GetTitleAndNumberOfPagesResult
    {
        public string Title = "";
        public int NumberOfPages = -1;
    }

    
}
