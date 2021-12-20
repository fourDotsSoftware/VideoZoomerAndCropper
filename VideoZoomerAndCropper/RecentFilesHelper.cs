using System;
using System.Collections.Generic;
using System.Text;

namespace VideoZoomerAndCropper
{
    class RecentFilesHelper
    {
        public static void FillMenuRecentFile()
        {
            string[] str = Properties.Settings.Default.OpenRecent.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);

            for (int k = 0; k < str.Length; k++)
            {
                frmMain.Instance.tsdbOpen.DropDownItems.Add(str[k]);                
            }
        }
                
        public static void AddRecentFile(string filepath)
        {
            string[] str = Properties.Settings.Default.OpenRecent.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> strl = ArrayToListString(str);

            if (strl.IndexOf(filepath) >= 0)
            {
                strl.RemoveAt(strl.IndexOf(filepath));
            }
            
            strl.Insert(0, filepath);

            frmMain.Instance.tsdbOpen.DropDownItems.Clear();

            string newrec = "";

            for (int k = 0; k < strl.Count && k <= 12; k++)
            {
                frmMain.Instance.tsdbOpen.DropDownItems.Add(strl[k]);
                newrec += strl[k] + "|||";
            }

            Properties.Settings.Default.OpenRecent = newrec;
        }              

        public static List<string> ArrayToListString(string[] str)
        {
            List<string> strl = new List<string>();

            for (int k = 0; k < str.Length; k++)
            {
                strl.Add(str[k]);
            }

            return strl;
        }
    }
}
