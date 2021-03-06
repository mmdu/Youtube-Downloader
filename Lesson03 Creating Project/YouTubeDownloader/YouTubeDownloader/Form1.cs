﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;
using System.Diagnostics;

namespace YouTubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        private object folderBrowserDiaglod1;

        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0; // set video as first choice  Combo
            //  gets path to mydocuments
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // set the path of browser dialog box 
            folderBrowserDialog1.SelectedPath = folder;
        }


        private void btnDownloadFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();  //  cannot seen , need check 
            if (result == DialogResult.OK)
                txtDownloadFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            Tuple<bool, string> isLinkGood = ValidateLink();  // get link validation results
            if (true == isLinkGood.Item1)
            {
                RestrictAccessability();  // call this to ensure controls not working during download 
                Download(isLinkGood.Item2);
                //System.Threading.Thread.Sleep(6000);
                //EnableAccessability();
                //// pass the validated link into the download mthod
                //// so it can be assigned to a property in the youtubue  vidieo model object 
                //MessageBox.Show("Is it good link ?  :  " + isLinkGood.Item1 + " ;  Link is : " + isLinkGood.Item2);

            }



            //// Our test youtube link ************************************************************************          
            ////string link = "https://www.youtube.com/watch?v=xjo1FCW9_kM&list=RDpHdm3K2qEzU&index=22";
            //string link = "https://www.youtube.com/watch?v=pv-6rweZR_s";


            ///*
            // * Get the available video formats.
            // * We'll work with them in the video and audio download examples.
            // */
            //IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);


            ////*Select the first .mp4 video with 360p resolution

            //VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            ///*
            // * If the video has a decrypted signature, decipher it
            // */
            //if (video.RequiresDecryption)
            //{
            //    DownloadUrlResolver.DecryptDownloadUrl(video);
            //}

            ///*
            // * Create the video downloader.
            // * The first argument is the video to download.
            // * The second argument is the path to save the video file.
            // */
            //var videoDownloader = new VideoDownloader(video,
            //    Path.Combine(txtDownloadFolder.Text, video.Title + video.VideoExtension));

            //// Register the ProgressChanged event and print the current progress
            //// videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            ///*
            // * Execute the video downloader.
            // * For GUI applications note, that this method runs synchronously.
            // */
            //videoDownloader.Execute();

            //*******************************************************************************
            //******************************************************************************

            //           // Our test youtube link
            //           string link = "https://www.youtube.com/watch?v=pHdm3K2qEzU&list=RDpHdm3K2qEzU";

            //           /*
            //            * Get the available video formats.
            //            * We'll work with them in the video and audio download examples.
            //            */
            //           IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            //           /*
            //* We want the first extractable video with the highest audio quality.
            //*/
            //           VideoInfo video = videoInfos
            //               .Where(info => info.CanExtractAudio)
            //               .OrderByDescending(info => info.AudioBitrate)
            //               .FirstOrDefault();

            //           /*
            //            * If the video has a decrypted signature, decipher it
            //            */
            //           if (video.RequiresDecryption)
            //           {
            //               DownloadUrlResolver.DecryptDownloadUrl(video);
            //           }

            //           /*
            //            * Create the audio downloader.
            //            * The first argument is the video where the audio should be extracted from.
            //            * The second argument is the path to save the audio file.
            //            */
            //           var audioDownloader = new AudioDownloader(video, Path.Combine(txtDownloadFolder.Text, video.Title + video.AudioExtension));

            //           // Register the progress events. We treat the download progress as 85% of the progress and the extraction progress only as 15% of the progress,
            //           // because the download will take much longer than the audio extraction.
            //           //audioDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage * 0.85);
            //           //audioDownloader.AudioExtractionProgressChanged += (sender, args) => Console.WriteLine(85 + args.ProgressPercentage * 0.15);

            //           /*
            //            * Execute the audio downloader.
            //            * For GUI applications note, that this method runs synchronously.
            //            */
            //           audioDownloader.Execute();
            //       }

            //       private void frmYTDownloader_Load(object sender, EventArgs e)
            //       {

            //       }

            //       private void txtLink_TextChanged(object sender, EventArgs e)
            //       {

            //       }

            //       private void cboFileType_SelectedIndexChanged(object sender, EventArgs e)
            //       {

            //       }

            //       private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
            //       {

            //       }

            // Our test youtube link

        }

        private void Download(string validatedLink )
        {
            if (cboFileType.SelectedIndex == 0)
            {
                YouTubeVideoModel   videoDownloader  = new YouTubeVideoModel();   // set  video downloader object, 
                videoDownloader.Link = validatedLink;
                videoDownloader.FolderPath = txtDownloadFolder.Text;
                DownloadVideo(videoDownloader);
            }
            else
            {
                YouTubeAudioModel audioDownloader =new YouTubeAudioModel();
                audioDownloader.Link = validatedLink;
                audioDownloader.FolderPath = txtDownloadFolder.Text;
           //     DownloadAudio(audioDownloader);
            }
        }

        private void DownloadVideo(YouTubeVideoModel videoDownloader)
        {
            try
            {

                videoDownloader.VideoInfo = FileDownloader.GetVideoInfos((videoDownloader));   // store video info object  common part , list , in model 
                videoDownloader.Video = FileDownloader.GetVideoInfo(videoDownloader); // store  only videoinfo object in model 
                UpdateLabel(videoDownloader.Video.Title + videoDownloader.Video.VideoExtension);  // chang lable tname 

                videoDownloader.FilePath = FileDownloader.GetPath(videoDownloader); // store filpath in model 
                videoDownloader.FilePath += videoDownloader.Video.VideoExtension;    // store filepath extension from youtube extracter  in model 
                videoDownloader.VideoDownloaderType = FileDownloader.GetVideoDownloader(videoDownloader);

                videoDownloader.VideoDownloaderType.DownloadFinished += (sender, args) => EnableAccessability();   // Enable button when download complete 
                videoDownloader.VideoDownloaderType.DownloadFinished +=(sender,args) => OpenFolder(videoDownloader.FilePath);   // open folder with download file selected 
                videoDownloader.VideoDownloaderType.DownloadProgressChanged +=
                    (sender, args) => pgDownload.Value = (int )args.ProgressPercentage;

                CheckForIllegalCrossThreadCalls = false;
                //donload video 
                FileDownloader.DownloadVideo(videoDownloader);
            }
            catch (Exception )
            {
                MessageBox.Show("Download Cancelled");
                EnableAccessability();

            }
        }

        private void UpdateLabel(string titleandExtension)
        {
            lblFileName.Text = titleandExtension;
        }

        private void OpenFolder(string filePath)
        {
            string argument = " /select, \" " + filePath.Length + "\"";
            if (chkOpenAfterDownload.Checked == true)
            {
                Process.Start("explorer.exe", argument);
            }
        }

        private void VideoDownloaderType_DownloadFinished(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EnableAccessability()
        {
            lblFileName.Text = "";// clear file name lable 
            txtLink.Text = ""; // clear link from link text box 
            btnDownload.Enabled = true; // enable the donwload buttton 
            btnDownloadFolder.Enabled = true; // enable to choose folders
            cboFileType.Enabled = true; // enable combo box 
            pgDownload.Value = 0; // zero out progress bar 

        }

        private void RestrictAccessability()
        {
            btnDownload.Enabled = false;
            cboFileType.Enabled = false;
            btnDownloadFolder.Enabled = false;
            txtDownloadFolder.Enabled = false;
            txtLink.Enabled = false;

        }

        private Tuple<bool, string> ValidateLink()
        {
            string normalURL;
            if (!Directory.Exists(txtDownloadFolder.Text))
            {
                MessageBox.Show("please enter a valid folder.");  // block runs when folder not fould
                return Tuple.Create(false, "bad fold path ");

            }
            else if(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text,out normalURL))
            {
                return Tuple.Create(true, normalURL); // return true and acutla link if succesfull

            }
            else
            {
                MessageBox.Show("Please enter a valid youtube link ");
                return Tuple.Create(false, "not a valid link "); // return false bad link if link isnot good
            }
        }

    }
}
