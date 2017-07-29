﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;
using System.IO;

namespace YouTubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0;//set video as first choice
            //line below gets path to my documents
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //sets the path of the browser dialog box 
            folderBrowserDialog1.SelectedPath = folder;
        }

        private void btnDownloadFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) //if user clicks ok
                //set selected path to display in download folder text box
                txtDownloadFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> isLinkGood = ValidateLink(); //get link validation results
            if(isLinkGood.Item1==true)
            {
                RestrictAccessibility();//call this to ensure controls don't work during a download
                Download(isLinkGood.Item2);
            }           
        }

        private void Download(string validatedLink)
        {
            if(cboFileType.SelectedIndex ==0)
            {
                YouTubeVideoModel videoDownloader = new YouTubeVideoModel();
                videoDownloader.Link = validatedLink;
                videoDownloader.FolderPath = txtDownloadFolder.Text;
                DownloadVideo(videoDownloader);
            }
            else
            {
                YouTubeAudioModel audioDownloader = new YouTubeAudioModel();
                audioDownloader.Link = validatedLink;
                audioDownloader.FolderPath = txtDownloadFolder.Text;
                //DownloadAudio(audioDownloader);
            }
        }

        private void DownloadVideo(YouTubeVideoModel videoDownloader)
        {
            try
            {
                //Store VideoInfo object in model
                videoDownloader.VideoInfo = FileDownloader.GetVideoInfos(videoDownloader);
                //Stores VideoInfo object in model
                videoDownloader.Video = FileDownloader.GetVideoInfo(videoDownloader);
                //Stores FilePath in model
                videoDownloader.FilePath = FileDownloader.GetPath(videoDownloader);
                videoDownloader.FilePath += videoDownloader.Video.VideoExtension;
                //Stores VideoDownloaderType object in model
                videoDownloader.VideoDownloaderType = FileDownloader.GetVideoDownloader(videoDownloader);
                //Download video
                FileDownloader.DownloadVideo(videoDownloader);
            }
            catch (Exception)
            {
               MessageBox.Show("Download canceled.");
               EnableAccessibility();
            }
        }
        private void EnableAccessibility()
        {
            lblFileName.Text = "";//clear file name label
            txtLink.Text = "";//clear the link from the link text box
            btnDownload.Enabled = true;//reenable the download button
            btnDownloadFolder.Enabled = true;//enable button for choosing folders
            txtLink.Enabled = true;//enable link box
            txtDownloadFolder.Enabled = true;//enable download folder text box
            cboFileType.Enabled = true;//enable combo box
            pgDownload.Value = 0;//zero out progress bar
        }
        private void RestrictAccessibility()
        {
            btnDownload.Enabled = false;
            cboFileType.Enabled = false;
            btnDownloadFolder.Enabled = false;
            txtDownloadFolder.Enabled = false;
            txtLink.Enabled = false;
        }

        private Tuple<bool, string> ValidateLink()
        {
            string normalURL;//normalized URL from YouTube
            if (!Directory.Exists(txtDownloadFolder.Text))
            {
                MessageBox.Show("Please enter a valid folder."); //block runs when folder cannot be found
                return Tuple.Create(false, "");
            }
            else if(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text,out normalURL))
            {
                return Tuple.Create(true, normalURL);//return true and actual link if successful
            }
            else
            {
                MessageBox.Show("Please enter a valid YouTube link.");
                return Tuple.Create(false, "");//return nothing if the link is not good
            }
        }
    }
}
