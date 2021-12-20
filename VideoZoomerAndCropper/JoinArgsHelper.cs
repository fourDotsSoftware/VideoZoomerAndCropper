using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace VideoZoomerAndCropper
{
    public class JoinArgsHelper
    {        
        public JoinArgsHelper()
        {

        }        

        public string GetSampleRate(string sample_rate)
        {
            try
            {
                return sample_rate.ToLower().Replace("khz", "").Replace("hz", "");
            }
            catch
            {
                return "";
            }
        }        

        public string GetFrameRate(string frame_rate)
        {
            try
            {
                int spos = frame_rate.IndexOf("(");

                if (spos >= 0)
                {
                    int epos = frame_rate.IndexOf(")", spos);

                    frame_rate = frame_rate.Substring(0, spos) + frame_rate.Substring(epos+1);                    
                }

                frame_rate = frame_rate.Replace("fps", "").Trim();                                

                return frame_rate;
            }
            catch
            {
                return "";
            }
        }

        public string GetVideoSize(string video_size)
        {
            try
            {
                if (video_size.IndexOf("(") >= 0)
                {
                    int spos = video_size.IndexOf("(");
                    int epos = video_size.IndexOf(")", spos);

                    video_size = video_size.Substring(spos + 1, epos - spos - 1);
                }

                return video_size.Replace("*", "x");
            }
            catch
            {
                return "";
            }
        }

        /*
        public JoinArgs GetJoinArgs(string filepath, OutputFormatResult res,  bool timeWasSet, string startTime, string endTime)
        {

            JoinArgs jargs = GetJoinArgs(filepath, res.extension,
            res.ffmpegParameters,
            res.firstPassArgs,
            res.secondPassArgs,
            res.videoBitRate,
            res.videoFrameRate,
            res.videoSize,
            res.videoAspectRatio,
            res.videoTwoPass,
            res.videoDeinterlace,
            res.audioBitRate,
            res.audioSampleRate,
            res.audioChannels,
            res.audioVolume,
            res.audioNormalize,
            res.audioMute,
            res.copyMetadata,
            timeWasSet, startTime, endTime);


            jargs.OutputFormatResult = res;

            return jargs;
        }        
        */

        public JoinArgs GetZoomArgs(string filepath, string outputext, string outputparams, string firstPassArgs, string secondPassArgs,
            string videoBitRate, string videoFrameRate, string videoSize, string videoAspectRatio, bool videoTwoPass, bool videoDeinterlace,
            string audioBitRate, string audioSampleRate, string audioChannels, string audioVolume, bool audioNormalize, bool audioMute, bool copyMetadata,
            string startTime, string endTime, int x, int y, int width, int height, int xb,int yb, int widthb,int heightb, string totalDuration, int durationMsecs,int vwrm,List<VWRClip> lstvwr)
        {
            JoinArgs joinargs = new JoinArgs();

            if (videoBitRate != string.Empty) outputparams += " -b:v " + videoBitRate;
            if (videoFrameRate != string.Empty) outputparams += " -r " + GetFrameRate(videoFrameRate);
            if (videoSize != string.Empty) outputparams += " -s " + GetVideoSize(videoSize);
            if (videoAspectRatio != string.Empty) outputparams += " -aspect " + videoAspectRatio;
            if (audioBitRate != string.Empty) outputparams += " -b:a " + audioBitRate;
            if (audioSampleRate != string.Empty) outputparams += " -ar " + GetSampleRate(audioSampleRate);
            if (audioChannels != string.Empty) outputparams += " -ac " + audioChannels;
            if (audioMute) outputparams += " -an ";

            string metadata_args = "";

            if (copyMetadata) metadata_args = " -map_metadata 0 ";

            string outputparams_original = outputparams;

            bool has_filter_params = HasParameter("-vf", outputparams);

            outputparams = RemoveVideoFilterArgsFromArgs(outputparams);

            //3string fargs = " -vf \"";

            string audioVolumeParams = "";

            if (audioVolume != string.Empty)
            {
                if (audioVolume.IndexOf("%") > 0)
                {
                    audioVolume = audioVolume.Replace("%", "");

                    decimal decVol1 = decimal.Parse(audioVolume);
                    decimal decVol2 = (decimal)100;
                    decimal decVol = decVol1 / decVol2;

                    audioVolumeParams += ",volume=" + decVol.ToString() + "";
                }
                else
                {
                    audioVolumeParams += ",volume=" + audioVolume + "";
                }
            }

            string deinterlaceParams = "";

            if (videoDeinterlace)
            {
                deinterlaceParams = ",yadif";
            }

            //3fargs += audioVolumeParams + deinterlaceParams;            

            //3string args = " -ss " + startTime + " -t " + duration + " -i \"" + filepath + "\" ";
            

            string args = " -i \"" + filepath + "\" -filter_complex \"";

            string starttime = "00:00:00.00";
            //string endtime = "99:59:59";
            string endtime = totalDuration;

            string timefrom = (startTime == string.Empty) ? "00:00:00.00" : startTime;
            string timeto = (endTime == string.Empty) ? totalDuration : endTime;

            int smsecs = TimeUpDownControl.StringToMsecs(timefrom);
            int emsecs = TimeUpDownControl.StringToMsecs(timeto);

            timefrom = timefrom.Replace(":", @"\:");
            timeto = timeto.Replace(":", @"\:");
            starttime = starttime.Replace(":", @"\:");
            endtime = endtime.Replace(":", @"\:");

            int vwrstartmsecs = lstvwr[vwrm].StartTime;
            int vwrendmsecs = lstvwr[vwrm].EndTime;

            if (((!Properties.Settings.Default.Crop && Properties.Settings.Default.JoinOverlayParts)
                || (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts))                                
                && (vwrstartmsecs != 0 && vwrstartmsecs>1000 && vwrm==0))
            {
                string tempfilenz = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + outputext);

                Module.FilesToDelete.Add(tempfilenz);

                string argsnz=" -i \"" + filepath + "\" -filter_complex \"";

                string nzstart = "00:00:00.00";

                int nzs = 0;
                int nze = lstvwr[vwrm].StartTime;

                if (vwrm > 0)
                {
                    nzstart = FFMpegVideoInfo.TimeMsecsToString(lstvwr[vwrm - 1].EndTime);
                    nzs = lstvwr[vwrm - 1].EndTime;
                }

                string nzend = FFMpegVideoInfo.TimeMsecsToString(lstvwr[vwrm].StartTime);

                nzstart=nzstart.Replace(":", @"\:");
                nzend = nzend.Replace(":", @"\:");

                joinargs.DurationMsecs += nze - nzs;

                argsnz += "[0:v:0]trim='" + nzstart + ":" + nzend + "'" + deinterlaceParams  +
                    //",setsar=1:1,setdar=1:1,setpts=PTS-STARTPTS[v1];" +                    
                    ",setpts=PTS-STARTPTS[v1];" +                    
                    "[0:a:0]atrim='" + nzstart + ":" + nzend + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];";
                argsnz += "[v1][a1]concat=n=1:v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\" " + outputparams + metadata_args + " -y \"" + tempfilenz + "\"";

                joinargs.NonZoomPartsArgsStart=argsnz;

                joinargs.NonZoomPartsFilepathStart=tempfilenz;
            }

            if (
                ((!Properties.Settings.Default.Crop && Properties.Settings.Default.JoinOverlayParts)
                || (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts))
                && (vwrendmsecs != durationMsecs && (durationMsecs-vwrendmsecs)>1000))
            {
                string tempfilenz = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + outputext);

                Module.FilesToDelete.Add(tempfilenz);

                string argsnz = " -i \"" + filepath + "\" -filter_complex \"";

                string nzstart = FFMpegVideoInfo.TimeMsecsToString(lstvwr[vwrm].EndTime);

                string nzend = totalDuration;

                int nzs = lstvwr[vwrm].EndTime;
                int nze = durationMsecs;

                if (vwrm <= lstvwr.Count-2)
                {
                    nzend = FFMpegVideoInfo.TimeMsecsToString(lstvwr[vwrm + 1].StartTime);
                    nze = lstvwr[vwrm + 1].StartTime;                
                }                

                nzstart = nzstart.Replace(":", @"\:");
                nzend = nzend.Replace(":", @"\:");

                joinargs.DurationMsecs += nze - nzs;

                argsnz += "[0:v:0]trim='" + nzstart + ":" + nzend + "'" + deinterlaceParams +
                    ",setpts=PTS-STARTPTS[v1];" +
                    "[0:a:0]atrim='" + nzstart + ":" + nzend + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];";
                argsnz += "[v1][a1]concat=n=1:v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\" " + outputparams + metadata_args + " -y \"" + tempfilenz + "\"";

                joinargs.NonZoomPartsArgsEnd = argsnz;

                joinargs.NonZoomPartsFilepathEnd = tempfilenz;
            }

                string fn = "";
                string joinfile = "";
                string outfolder=System.IO.Path.GetDirectoryName(filepath);

                if (Properties.Settings.Default.OutputFolderIndex!=0)
                {
                    outfolder=Properties.Settings.Default.OutputFolder;
                }
                
                fn = System.IO.Path.GetFileNameWithoutExtension(filepath)+timefrom.Replace(@"\:","_")+"-"+timeto.Replace(@"\:","_");

                joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

               joinargs.JoinFile = System.IO.Path.Combine(outfolder, joinfile + outputext);            

            string tempfileCrop=System.IO.Path.Combine(System.IO.Path.GetTempPath(),Guid.NewGuid().ToString()+outputext);
            string tempfileOverlay=System.IO.Path.Combine(System.IO.Path.GetTempPath(),Guid.NewGuid().ToString()+outputext);

            Module.FilesToDelete.Add(tempfileCrop);

            Module.FilesToDelete.Add(tempfileOverlay);

            if (Properties.Settings.Default.Crop && !Properties.Settings.Default.JoinCropParts)
            {
                //tempfileCrop=joinargs.JoinFile;

                joinargs.CropOutputFilepath=tempfileCrop;              
            }
            else if (Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts)
            {
                joinargs.CropOutputFilepath=tempfileCrop;              
            }
            else if (!Properties.Settings.Default.Crop)
            {
                joinargs.CropOutputFilepath=tempfileCrop;
                joinargs.OverlayOutputFilepath=tempfileOverlay;

                joinargs.OverlayCutOutputFilepath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + outputext);

                Module.FilesToDelete.Add(joinargs.OverlayCutOutputFilepath);
            }

            if (!audioMute)
            {
                string boxargs = ",drawbox=x=0:y=0:w="+widthb.ToString()+":h="+heightb.ToString()+":c=" + Module.HexConverter(Properties.Settings.Default.BoxColor)
                    + ":t=" + Properties.Settings.Default.BoxThickness.ToString();                

                string joincropargs = ",pad=iw:ih:";                

                switch (Properties.Settings.Default.CropJoinVideoAlign)
                {
                    case 0:
                        joincropargs += "iw/2-" + widthb.ToString() + "/2:ih/2-" + heightb.ToString() + "/2";
                        break;
                    case 1:
                        joincropargs += "0:0";
                        break;
                    case 2:
                        joincropargs += "iw/2-" + widthb.ToString() + "/2:0";
                        break;
                    case 3:
                        joincropargs += "iw-" + widthb.ToString() + ":0";
                        break;
                    case 4:
                        joincropargs += "0:ih-" + heightb.ToString();
                        break;
                    case 5:
                        joincropargs += "iw-" + widthb.ToString() + ":ih/2-" + heightb.ToString() + "/2";                        
                        break;
                    case 6:
                        joincropargs += "0:ih-" + heightb.ToString();                        
                        break;
                    case 7:
                        joincropargs += "iw/2-" + widthb.ToString() + "/2:ih-" + heightb.ToString();                        
                        break;
                    case 8:
                        joincropargs += "iw-" + widthb.ToString() + ":ih-" + heightb.ToString();                        
                        break;
                    /*
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Top Right"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Middle Right"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Left"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Center"));
            cmbImageAlign.Items.Add(TranslateHelper.Translate("Bottom Right"));*/
                        
                }

                joincropargs += ":" + Module.HexConverter(Properties.Settings.Default.CropPaddingColor);

                joincropargs = joincropargs.Replace("iw", frmMain.Instance.VideoScaler.Width.ToString()).Replace
                            ("ih", frmMain.Instance.VideoScaler.Height.ToString());

                args += "[0:v:0]trim='" + timefrom + ":" + timeto + "'" + (Properties.Settings.Default.Crop?deinterlaceParams:"") + 
                    ",crop=" + width.ToString() + ":" + height.ToString() + ":" + x.ToString() + ":" + y.ToString() +
                    ",setpts=PTS-STARTPTS[v1];" +
                    "[v1]scale=" + widthb.ToString() + ":" + heightb.ToString() + ":0:0"+
                    ((!Properties.Settings.Default.Crop && Properties.Settings.Default.DrawBox)?boxargs:"")+
                    ((Properties.Settings.Default.Crop && Properties.Settings.Default.JoinCropParts)?
                    joincropargs:"")+
                    "[v2];"+
                    "[0:a:0]atrim='" + timefrom + ":" + timeto + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];";

                args += "[v2][a1]concat=n=1:v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\" " + outputparams + metadata_args + " -y \"" + tempfileCrop + "\"";
            }
            else
            {
                string zoomargs = ",crop=" + width.ToString() + ":" + height.ToString() + ":" + x.ToString() + ":" + y.ToString() + "[v1];[v1]" +
                //"scale=" + widthb.ToString() + ":" + heightb.ToString() + ":" + xb.ToString() + ":" + yb.ToString() + "[v2];";
                "scale=" + widthb.ToString() + ":" + heightb.ToString() + ":0:0[v2];";


                args += "[0:v:0]trim='" + timefrom + ":" + timefrom + "'" + deinterlaceParams + zoomargs + ",setpts=PTS-STARTPTS[v1];";
                args += " -map \"[v2]\" " + outputparams + metadata_args + " -y \"" + tempfileCrop + "\"";
            }

            joinargs.ArgsCrop=args;

            if (!Properties.Settings.Default.Crop)
            {
                string args3 = " -i \"" + filepath + "\" -filter_complex \"[0:v:0]trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams +
                    (Properties.Settings.Default.Blur?",boxblur=8:1":"")+",setpts=PTS-STARTPTS[v1];" +
                    "[0:a:0]atrim='" + timefrom + ":" + timeto + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];"
                    + "[v1][a1]concat=n=1:v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\" " + outputparams + metadata_args + " -y \"" + joinargs.OverlayCutOutputFilepath + "\"";

                joinargs.ArgsOverlayCut = args3;

                string args2 = " -i \"" + joinargs.OverlayCutOutputFilepath + "\" -i \"" + tempfileCrop + "\" -filter_complex overlay=" + xb.ToString() + ":" + yb.ToString()
               + " " + outputparams + metadata_args + " -y \"" + tempfileOverlay + "\"";

                joinargs.DurationMsecs += 2*(emsecs - smsecs);

                joinargs.ArgsOverlay=args2;

            }

            joinargs.DurationMsecs += emsecs - smsecs;

            if (audioNormalize)
            {
                joinargs.NormalizeFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(joinargs.JoinFile), Guid.NewGuid().ToString() + outputext);

                joinargs.NormalizeArgs1 = " -i \"" + joinargs.NormalizeFile + "\" -af \"volumedetect\" -f null NUL ";

                joinargs.NormalizeArgs2 = " -y -i \"" + joinargs.NormalizeFile + "\" " + outputparams + metadata_args + " -af \"volume=" + "[REPLACE_MAXVOL]" + "dB\" -y ";

                //3joinargs.DurationMsecs += 2 * durationMsecs;

                joinargs.DurationMsecs += 2 * (emsecs - smsecs);
            }

            //joinargs.Args = args + " " + outputparams + metadata_args + " -y ";

            if (has_filter_params)
            {
                joinargs.JoinFileWithNoFilter = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(joinargs.JoinFile), Guid.NewGuid().ToString() + outputext);

                joinargs.JoinArgsWithNoFilter = " -i \"" + joinargs.JoinFileWithNoFilter + "\" " + outputparams_original + metadata_args + " -y ";                

                joinargs.DurationMsecs += (emsecs - smsecs);//durationMsecs;

            }

            joinargs.DurationMsecs = joinargs.DurationMsecs / 100;

            joinargs.InputVideoFiles.Add(filepath);            

            return joinargs;
        }

        public JoinArgs GetJoinArgs(string filepath, string outputext, string outputparams, string firstPassArgs, string secondPassArgs,
            string videoBitRate, string videoFrameRate, string videoSize, string videoAspectRatio, bool videoTwoPass, bool videoDeinterlace,
            string audioBitRate, string audioSampleRate, string audioChannels, string audioVolume, bool audioNormalize, bool audioMute, bool copyMetadata,
            string startTime, string endTime, int x,int y,int width,int height,string totalDuration,int durationMsecs)
        {
            JoinArgs joinargs = new JoinArgs();

            if (videoBitRate != string.Empty) outputparams += " -b:v " + videoBitRate;
            if (videoFrameRate != string.Empty) outputparams += " -r " + GetFrameRate(videoFrameRate);
            if (videoSize != string.Empty) outputparams += " -s " + GetVideoSize(videoSize);
            if (videoAspectRatio != string.Empty) outputparams += " -aspect " + videoAspectRatio;
            if (audioBitRate != string.Empty) outputparams += " -b:a " + audioBitRate;
            if (audioSampleRate != string.Empty) outputparams += " -ar " + GetSampleRate(audioSampleRate);
            if (audioChannels != string.Empty) outputparams += " -ac " + audioChannels;
            if (audioMute) outputparams += " -an ";

            string metadata_args = "";

            if (copyMetadata) metadata_args = " -map_metadata 0 ";
            
            string outputparams_original = outputparams;            

            bool has_filter_params = HasParameter("-vf", outputparams);

            outputparams = RemoveVideoFilterArgsFromArgs(outputparams);
            
            //3string fargs = " -vf \"";

            string audioVolumeParams = "";

            if (audioVolume != string.Empty)
            {
                if (audioVolume.IndexOf("%") > 0)
                {
                    audioVolume = audioVolume.Replace("%", "");

                    decimal decVol1 = decimal.Parse(audioVolume);
                    decimal decVol2 = (decimal)100;
                    decimal decVol = decVol1 / decVol2;

                    audioVolumeParams += ",volume=" + decVol.ToString() + "";
                }
                else
                {
                    audioVolumeParams += ",volume=" + audioVolume + "";
                }
            }

            string deinterlaceParams = "";

            if (videoDeinterlace)
            {
                deinterlaceParams = ",yadif";
            }

            //3fargs += audioVolumeParams + deinterlaceParams;            

            //FFMPEGInfo firstinfo = new FFMPEGInfo(filepath);

            joinargs.JoinFile = GetJoinFile(filepath, outputext);           

            //3string args = " -ss " + startTime + " -t " + duration + " -i \"" + filepath + "\" ";

            string args = " -i \"" + filepath + "\" -filter_complex \"";

            string starttime = "00:00:00.00";
            //string endtime = "99:59:59";
            string endtime = totalDuration;

            string timefrom = (startTime == string.Empty) ? "00:00:00.00" : startTime;
            string timeto = (endTime == string.Empty) ? totalDuration : endTime;

            timefrom = timefrom.Replace(":", @"\:");
            timeto = timeto.Replace(":", @"\:");
            starttime = starttime.Replace(":", @"\:");
            endtime = endtime.Replace(":", @"\:");           

            string logoargs = "delogo=x=" + x.ToString() + ":y=" + y.ToString() +
                //5.2017":w=" + width.ToString() + ":h=" + height.ToString() + ":t=" + "4" + ":show=0";
                ":w=" + width.ToString() + ":h=" + height.ToString() + ":show=0";

            if (!audioMute)
            {
                if (starttime != timefrom && endtime != timeto)
                {
                    args += "[0:v:0]trim='" + starttime + ":" + timefrom + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v1];" +
                    "[0:a:0]atrim='" + starttime + ":" + timefrom + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];" +
                    "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +
                    "[0:a:0]atrim='" + timefrom + ":" + timeto + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a2];" +
                    "[0:v:0]trim='" + timeto + ":" + endtime + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v3];" +
                    "[0:a:0]atrim='" + timeto + ":" + endtime + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a3];" +
                    "[v1][a1][v2][a2][v3][a3] concat=n=3:v=1:a=1 [v][a]\" -map \"[v]\" -map \"[a]\" ";
                }
                else if (starttime == timefrom && endtime != timeto)
                {
                    args += "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +
                    "[0:a:0]atrim='" + timefrom + ":" + timeto + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a2];" +
                    "[0:v:0]trim='" + timeto + ":" + endtime + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v3];" +
                    "[0:a:0]atrim='" + timeto + ":" + endtime + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a3];" +
                    "[v2][a2][v3][a3] concat=n=2:v=1:a=1 [v][a]\" -map \"[v]\" -map \"[a]\" ";
                }
                else if (starttime != timefrom && endtime == timeto)
                {
                    args += "[0:v:0]trim='" + starttime + ":" + timefrom + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v1];" +
                    "[0:a:0]atrim='" + starttime + ":" + timefrom + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a1];" +
                    "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +
                    "[0:a:0]atrim='" + timefrom + ":" + timeto + "'" + audioVolumeParams + ",asetpts=PTS-STARTPTS[a2];" +
                    "[v1][a1][v2][a2] concat=n=2:v=1:a=1 [v][a]\" -map \"[v]\" -map \"[a]\" ";
                }
                else if (starttime == timefrom && endtime == timeto)
                {
                    args += logoargs + deinterlaceParams + audioVolumeParams + "\"";
                }
            }
            else
            {
                if (starttime != timefrom && endtime != timeto)
                {
                    args += "[0:v:0]trim='" + starttime + ":" + timefrom + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v1];" +                    
                    "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +                    
                    "[0:v:0]trim='" + timeto + ":" + endtime + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v3];" +                    
                    "[v1][v2][v3] concat=n=3:v=1:a=0 [v]\" -map \"[v]\" ";
                }
                else if (starttime == timefrom && endtime != timeto)
                {
                    args += "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +                    
                    "[0:v:0]trim='" + timeto + ":" + endtime + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v3];" +                    
                    "[v2][v3] concat=n=2:v=1:a=0 [v]\" -map \"[v]\" ";
                }
                else if (starttime != timefrom && endtime == timeto)
                {
                    args += "[0:v:0]trim='" + starttime + ":" + timefrom + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS[v1];" +                    
                    "[0:v:0]" + logoargs + ",trim='" + timefrom + ":" + timeto + "'" + deinterlaceParams + ",setpts=PTS-STARTPTS" + "[v2];" +                    
                    "[v1][v2] concat=n=2:v=1:a=0 [v]\" -map \"[v]\" ";
                }
                else if (starttime == timefrom && endtime == timeto)
                {
                    args += logoargs + deinterlaceParams + audioVolumeParams + "\" -an ";
                }
            }
                /*
                fargs+=",delogo=x=" + x.ToString() + ":y=" + y.ToString() +
                    //5.2017":w=" + width.ToString() + ":h=" + height.ToString() + ":t=" + "4" + ":show=0";
                    ":w=" + width.ToString() + ":h=" + height.ToString() + ":show=0";
            
                if (fargs.StartsWith(" -vf \","))
                {
                    fargs = "-vf \"" + fargs.Substring(" -vf \",".Length) + "\"";
                }

                fargs = args + fargs;

                */

                joinargs.DurationMsecs = durationMsecs;

            if (audioNormalize)
            {
                joinargs.NormalizeFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(joinargs.JoinFile), Guid.NewGuid().ToString() + outputext);

                joinargs.NormalizeArgs1 = " -i \"" + joinargs.NormalizeFile + "\" -af \"volumedetect\" -f null NUL ";

                joinargs.NormalizeArgs2 = " -y -i \"" + joinargs.NormalizeFile + "\" " + outputparams + metadata_args + " -af \"volume=" + "[REPLACE_MAXVOL]" + "dB\" -y ";

                joinargs.DurationMsecs += 2 * durationMsecs;
            }

            joinargs.Args = args + " "+ outputparams+ metadata_args + " -y ";

            if (has_filter_params)
            {
                joinargs.JoinFileWithNoFilter = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(joinargs.JoinFile), Guid.NewGuid().ToString() + outputext);

                joinargs.JoinArgsWithNoFilter = " -i \"" + joinargs.JoinFileWithNoFilter + "\" " + outputparams_original + metadata_args + " -y ";

                joinargs.Args = args+ " " + outputparams + metadata_args + " -y ";

                joinargs.DurationMsecs += durationMsecs;

            }

            joinargs.DurationMsecs = joinargs.DurationMsecs / 100;

            joinargs.InputVideoFiles.Add(filepath);

            return joinargs;
        }                               

        /* REMOVE COMMENTS 16.11.2017
        public JoinArgs GetJoinArgs(OutputFormatResult res)
        {            
            return GetJoinArgsDifferentType(res);
            
        }
        */                
        
        public string GetJoinFilePrefix(string filepath, string ext, string prefix)
        {
            string fn = "";
            string joinfile = "";
            string outfolder = System.IO.Path.GetDirectoryName(filepath);

            if (Properties.Settings.Default.OutputFolderIndex != 0)
            {
                outfolder = Properties.Settings.Default.OutputFolder;
            }

            fn = System.IO.Path.GetFileNameWithoutExtension(filepath);

            joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

            string jfp = System.IO.Path.Combine(outfolder, joinfile + ext);

            if (System.IO.File.Exists(jfp))
            {
                int k = 1;
                bool found = false;

                while (!found)
                {
                    jfp = System.IO.Path.Combine(outfolder,
                        prefix.Replace("[N]", k.ToString()) + joinfile + ext);

                    if (!System.IO.File.Exists(jfp))
                    {
                        return jfp;
                    }
                    else
                    {
                        k++;
                    }
                }
            }
            else
            {
                return jfp;
            }

            return "-1";
        }
        public string GetJoinFileSuffix(string filepath, string ext,string suffix)
        {
            string fn = "";
            string joinfile = "";
            string outfolder = System.IO.Path.GetDirectoryName(filepath);

            if (Properties.Settings.Default.OutputFolderIndex != 0)
            {
                outfolder = Properties.Settings.Default.OutputFolder;
            }

            fn = System.IO.Path.GetFileNameWithoutExtension(filepath);

            joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

            string jfp = System.IO.Path.Combine(outfolder, joinfile + ext);

            if (System.IO.File.Exists(jfp))
            {
                int k = 1;
                bool found = false;

                while (!found)
                {
                    jfp = System.IO.Path.Combine(outfolder,
                        joinfile + suffix.Replace("[N]", k.ToString()) + ext);

                    if (!System.IO.File.Exists(jfp))
                    {
                        return jfp;
                    }
                    else
                    {
                        k++;
                    }
                }
            }
            else
            {
                return jfp;
            }

            return "-1";
        }
        
        public string GetJoinFileSkip(string filepath, string ext)
        {
            string fn = "";
            string joinfile = "";
            string outfolder = System.IO.Path.GetDirectoryName(filepath);

            if (Properties.Settings.Default.OutputFolderIndex != 0)
            {
                outfolder = Properties.Settings.Default.OutputFolder;
            }

            fn = System.IO.Path.GetFileNameWithoutExtension(filepath);

            joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

            string jfp = System.IO.Path.Combine(outfolder, joinfile + ext);

            if (!System.IO.File.Exists(jfp))
            {
                return jfp;
            }
            else
            {
                return "-1";
            }

        }

        public string GetJoinFile(string filepath,string ext)
        {
            if (frmMain.Instance.ExplicitOutputFilepath != string.Empty)
            {
                if (!System.IO.Path.IsPathRooted(frmMain.Instance.ExplicitOutputFilepath))
                {                    
                    string outfolder = System.IO.Path.GetDirectoryName(filepath);

                    if (Properties.Settings.Default.OutputFolderIndex != 0)
                    {
                        outfolder = Properties.Settings.Default.OutputFolder;
                    }

                    return System.IO.Path.Combine(outfolder, frmMain.Instance.ExplicitOutputFilepath);
                }
                else
                {
                    return frmMain.Instance.ExplicitOutputFilepath;
                }
            }
            else
            {
                string fn = "";
                string joinfile = "";
                string outfolder=System.IO.Path.GetDirectoryName(filepath);

                if (Properties.Settings.Default.OutputFolderIndex!=0)
                {
                    outfolder=Properties.Settings.Default.OutputFolder;
                }
                
                fn = System.IO.Path.GetFileNameWithoutExtension(filepath);                

                joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

                if (Properties.Settings.Default.OutputWhenExists == 0)
                {
                    return System.IO.Path.Combine(outfolder, joinfile + ext);
                }
                else if (Properties.Settings.Default.OutputWhenExists == 1)
                {
                    return GetJoinFileSkip(filepath, ext);       
                }
                else if (Properties.Settings.Default.OutputWhenExists == 2)
                {
                    string jfp = System.IO.Path.Combine(outfolder, joinfile + ext);

                    if (!System.IO.File.Exists(jfp))
                    {
                        return jfp;
                    }
                    else
                    {
                        if (!frmMain.Instance.OutputFileActionRepeat)
                        {
                            frmOutputFileAsk f = new frmOutputFileAsk(filepath , outfolder,jfp);

                            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                return frmOutputFileAsk.CalculateOutputFileRepeatAction(filepath,
                                    jfp, outfolder);
                            }
                            else
                            {
                                return "-1";
                            }
                        }
                        else
                        {
                            return frmOutputFileAsk.CalculateOutputFileRepeatAction(filepath,jfp, outfolder);
                        }
                    }
                }
                else if (Properties.Settings.Default.OutputWhenExists == 3)
                {
                    return GetJoinFileSuffix(filepath, ext, Properties.Settings.Default.OutputSuffix);
                }
                else if (Properties.Settings.Default.OutputWhenExists == 4)
                {
                    return GetJoinFilePrefix(filepath, ext, Properties.Settings.Default.OutputPrefix);
                }
            }

            return "-1";
        }

        public string GetJoinFile(string filepath,string ext,int m)
        {
            if (frmMain.Instance.ExplicitOutputFilepath != string.Empty)
            {
                if (!System.IO.Path.IsPathRooted(frmMain.Instance.ExplicitOutputFilepath))
                {                    
                    string outfolder = System.IO.Path.GetDirectoryName(filepath);

                    if (Properties.Settings.Default.OutputFolderIndex != 0)
                    {
                        outfolder = Properties.Settings.Default.OutputFolder;
                    }

                    return System.IO.Path.Combine(outfolder, frmMain.Instance.ExplicitOutputFilepath);
                }
                else
                {
                    return frmMain.Instance.ExplicitOutputFilepath;
                }
            }
            else
            {
                string fn = "";
                string joinfile = "";
                string outfolder=System.IO.Path.GetDirectoryName(filepath);

                if (Properties.Settings.Default.OutputFolderIndex!=0)
                {
                    outfolder=Properties.Settings.Default.OutputFolder;
                }
                
                fn = System.IO.Path.GetFileNameWithoutExtension(filepath)+" - "+m.ToString();

                joinfile = Properties.Settings.Default.OutputFilenamePattern.Replace("[FILENAME]", fn);

                if (true || Properties.Settings.Default.OutputWhenExists == 0)
                {
                    return System.IO.Path.Combine(outfolder, joinfile + ext);
                }
                else if (Properties.Settings.Default.OutputWhenExists == 1)
                {
                    return GetJoinFileSkip(filepath, ext);       
                }
                else if (Properties.Settings.Default.OutputWhenExists == 2)
                {
                    string jfp = System.IO.Path.Combine(outfolder, joinfile + ext);

                    if (!System.IO.File.Exists(jfp))
                    {
                        return jfp;
                    }
                    else
                    {
                        if (!frmMain.Instance.OutputFileActionRepeat)
                        {
                            frmOutputFileAsk f = new frmOutputFileAsk(filepath , outfolder,jfp);

                            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                return frmOutputFileAsk.CalculateOutputFileRepeatAction(filepath,
                                    jfp, outfolder);
                            }
                            else
                            {
                                return "-1";
                            }
                        }
                        else
                        {
                            return frmOutputFileAsk.CalculateOutputFileRepeatAction(filepath,jfp, outfolder);
                        }
                    }
                }
                else if (Properties.Settings.Default.OutputWhenExists == 3)
                {
                    return GetJoinFileSuffix(filepath, ext, Properties.Settings.Default.OutputSuffix);
                }
                else if (Properties.Settings.Default.OutputWhenExists == 4)
                {
                    return GetJoinFilePrefix(filepath, ext, Properties.Settings.Default.OutputPrefix);
                }
            }

            return "-1";
        }
            

        private bool HasParameter(string parameter, string args)
        {
            return (args.IndexOf(" "+parameter+" ")>0);
        }

        private string RemoveVideoFilterArgsFromArgs(string args)
        {
            string str = RemoveParameterFromArgs("-vf", args);

            str = RemoveParameterFromArgs("-af", str);

            return str;
        }

        private string RemoveParameterFromArgs(string parameter,string args)
        {
            if (args.IndexOf(" "+parameter+" ") > 0)
            {
                string param = " " + parameter + " ";

                int spos1 = args.IndexOf(param);

                int spos = args.IndexOf(param) + param.Length;

                if (args.Substring(spos, 1) == "\"")
                {
                    int epos = args.IndexOf("\"", spos + 1);

                    return " "+args.Substring(0, spos1) + args.Substring(epos + 1);
                }
                else if (args.Substring(spos, 1) == "'")
                {
                    int epos = args.IndexOf("'", spos + 1);

                    return " "+args.Substring(0, spos1) + args.Substring(epos+1);
                }
                else
                {
                    int epos = args.IndexOf(" ", spos);

                    if (epos < 0)
                    {
                        epos = args.Length - 1;

                        return " "+args.Substring(0, spos1);
                    }
                    else
                    {
                        return " "+args.Substring(0, spos1) + args.Substring(epos);
                    }
                }
            }
            else
            {
                return args;
            }
        }

        public static string GetNormalizeArgs(string initial_args, string ffmpeg_output)
        {
            string maxvol="";

            using (System.IO.StringReader sr = new System.IO.StringReader(ffmpeg_output))
            {
                string line = null;

                while ((line = sr.ReadLine()) != null)
                {

                    if (line.Contains(" max_volume: "))
                    {
                        int spos = line.IndexOf(" max_volume: ") + " max_volume: ".Length;
                        int epos = line.IndexOf(" ", spos);

                        maxvol = line.Substring(spos, epos - spos);
                    }
                }

                if (maxvol != "0.0") // if it is not normalized
                {
                    if (maxvol.StartsWith("-"))
                    {
                        maxvol = maxvol.Substring(1);
                    }

                    return initial_args.Replace("[REPLACE_MAXVOL]", maxvol);
                }
                else
                {
                    return "";
                }
            }
        }
    }

    public class JoinArgs
    {
        public string Args = "";

        //public List<string> CropOutputFilepaths = new List<string>();
        //public List<string> OverlayOutputFilepaths = new List<string>();

        public string CropOutputFilepath="";
        public string OverlayCutOutputFilepath = "";
        public string OverlayOutputFilepath="";

        public string NonZoomPartsFilepathStart = "";
        public string NonZoomPartsFilepathEnd = "";

        public string ArgsCrop="";
        public string ArgsOverlayCut = "";
        public string ArgsOverlay="";

        public string NonZoomPartsArgsStart = "";
        public string NonZoomPartsArgsEnd = "";

        public List<string> ResizeOutputFilepaths = new List<string>();
        public List<string> FadeOutputFilepaths = new List<string>();
        public List<string> ResizeFadeOutputArgs = new List<string>();

        public string JoinArgsWithNoFilter = "";
        public string JoinFileWithNoFilter = "";

        public string AudioVolumeArgs = "";
        public string AudioVolumeArgsNoNormalize = "";
        public string AudioVolumeFile = "";

        public string NormalizeArgs1 = "";
        public string NormalizeArgs2 = "";
        public string NormalizeFile = "";

        public string DeinterlaceArgs = "";
        public string DeinterlaceFile = "";

        public int DurationMsecs = 0;
        public string JoinFile = "";                

        public List<string> InputVideoFiles = new List<string>();

        public OutputFormatResult OutputFormatResult = null;
    }    
}